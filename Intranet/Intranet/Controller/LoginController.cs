using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.DirectoryServices;
using System.ComponentModel.DataAnnotations;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace Intranet.Controller
{
    public class LoginController : ControllerBase
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            //Indicamos el dominio en el que vamos a buscar al usuario
             string path = "LDAP://fenixsalud.local";

            try
            {
                using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path, credentials.Username, credentials.Password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        //Buscamos por la propiedad SamAccountName
                        searcher.Filter = "(samaccountname=" + credentials.Username + ")";
                        //Buscamos el usuario con la cuenta indicada
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            string role = "";
                            //Comporbamos las propiedades del usuario
                            ResultPropertyCollection fields = result.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    if (ldapField == "employeetype")
                                        role = myCollection.ToString().ToLower();
                                }
                            }

                            //Añadimos los claims Usuario y Rol para tenerlos disponibles en la Cookie
                            //Podríamos obtenerlos de una base de datos.
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.Name, credentials.Username),
                                new Claim(ClaimTypes.Role, role)
                            };

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
                            return LocalRedirect("/Invalid credentials");
                        }
                           
                    }
                }

            }
            catch (Exception ex)
            {
               // Snackbar.Add("logueo fallido", Severity.Error);
                return LocalRedirect("/Invalid credentials");
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
