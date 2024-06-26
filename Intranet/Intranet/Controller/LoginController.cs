using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.DirectoryServices;
using System.ComponentModel.DataAnnotations;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Intranet.Modelos.LoginModel;
using Intranet.Interfaces.Admin;
using Intranet.Pages;

namespace Intranet.Controller
{
    public class LoginController : ControllerBase
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private IServicioAdmin ServicioAdmin { get; set; }
        private IConfiguration configuration;

        public LoginController(IServicioAdmin ServicioAdmin, IConfiguration Configuration) {
            this.ServicioAdmin = ServicioAdmin;
            this.configuration = Configuration;
        }


        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(LoginDTO credentials)
        {
            //Indicamos el dominio en el que vamos a buscar al usuario
            string path = configuration["ConexionLDAP"];
            var UsuarioSuperAdmin = configuration["usuarioAdmin"];
            var PasswordSuperAdmin = configuration["password"];

            try
            {
                if (UsuarioSuperAdmin == credentials.Usuario && PasswordSuperAdmin == credentials.Clave) 
                {
                    var claimsList = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, "Super Admin"),
                                new Claim(ClaimTypes.Surname, "SuperAdmin"),
                                new Claim(ClaimTypes.Role, "SuperAdmin")
                    };


                    var claims = claimsList.ToArray();

                    //Creamos el principal
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    //Generamos la cookie. SignInAsync es un método de extensión del contexto.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return LocalRedirect("/tablero");
                }
                    

                using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path, credentials.Usuario, credentials.Clave))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        //Buscamos por la propiedad SamAccountName
                        searcher.Filter = "(samaccountname=" + credentials.Usuario + ")";
                        //Buscamos el usuario con la cuenta indicada
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            string role = "";
                            List<string> permisos = new List<string>();
                            Guid id = Guid.Empty ;
                            string nombreUsuario = "";

                            List<string> key = new List<string>();
                            List<string> valor = new List<string>();
                            int i = 0;

                            //Comporbamos las propiedades del usuario
                            ResultPropertyCollection fields = result.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    i++;
                                    key.Add(i.ToString() + " - " + ldapField.ToString());
                                    valor.Add(i.ToString() + " - " + myCollection.ToString());
                                    if (ldapField == "name")
                                      nombreUsuario = myCollection.ToString();

                                    if (ldapField == "objectguid")
                                        id = new Guid((byte[])myCollection);
                                }
                            }


                            //setear rol a usuario
                            role = ServicioAdmin.BuscarRolDeUsuario(id);
                            permisos = await ServicioAdmin.ObtenerPermisosDeUsuario(id);
                            //Añadimos los claims Usuario y Rol para tenerlos disponibles en la Cookie
                            //Podríamos obtenerlos de una base de datos.

                            var claimsList = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                                new Claim(ClaimTypes.Name, nombreUsuario),
                                new Claim(ClaimTypes.Surname, credentials.Usuario),
                                new Claim(ClaimTypes.Role, role)
                            };

                            foreach (var permiso in permisos)
                            {
                                claimsList.Add(new Claim(ClaimTypes.Role, permiso));
                            }

                            var claims = claimsList.ToArray();

                            //Creamos el principal
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            //Generamos la cookie. SignInAsync es un método de extensión del contexto.
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                            //Redirigimos a la Home
                           // Snackbar.Add("logueo exitoso", Severity.Info);
                            return LocalRedirect("/tablero");

                        }
                        else {

                           // Snackbar.Add("logueo fallido", Severity.Error);
                            return LocalRedirect("/Error al ingresar");
                        }
                           
                    }
                }

            }
            catch (Exception ex)
            {
                //return LocalRedirect("/login/Usuario o Clave inválida");
                return LocalRedirect("/invalido/Credenciales Incorrectas");
            }
        }


        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }

    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
