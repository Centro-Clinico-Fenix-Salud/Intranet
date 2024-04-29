using Blazored.SessionStorage;
using Intranet.Extension;
using Intranet.Modelos.LoginModel;
using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using MudBlazor;
using System.DirectoryServices;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;


namespace Intranet.Pages
{
    public partial class Loggin
    {
        [Inject]
        private ISessionStorageService _sessionStorage { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }

        LoginDTO LoginDTO { get; set; }

        private string username;
        private string password;
        private string errorMessage;

        bool success;
        string[] errors = { };
        MudTextField<string> pwField1;
        MudForm form;

        protected override async Task OnInitializedAsync()
        {
            LoginDTO = new LoginDTO();

            //var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
            //if (await _sessionStorage.ObtenerLogin())
            //    await autenticacionExt.ActualizarEstadoAutenticacion(null);

        }
        private async Task IniciarSesion()
        {
            //Indicamos el dominio en el que vamos a buscar al usuario
            string path = "LDAP://fenixsalud.local";

            try
            {
                using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path, LoginDTO.Usuario, LoginDTO.Clave))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        //Buscamos por la propiedad SamAccountName
                        //searcher.Filter = "(samaccountname=" + credentials.Username + ")";
                        searcher.Filter = "(samaccountname=" + LoginDTO.Usuario + ")";
                        //Buscamos el usuario con la cuenta indicada
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            string role = "";
                            string nombreUsuario = "";
                            //Comporbamos las propiedades del usuario

                            ResultPropertyCollection fields = result.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    //if (ldapField == "employeetype")
                                    //    role = myCollection.ToString().ToLower();
                                    if (ldapField == "name")
                                        nombreUsuario = myCollection.ToString().ToLower();
                                }
                            }

                            //Añadimos los claims Usuario y Rol para tenerlos disponibles en la Cookie
                            //Podríamos obtenerlos de una base de datos.
                            //var claims = new[]
                            //{
                            //    new Claim(ClaimTypes.Name, credentials.Username),
                            //    new Claim(ClaimTypes.Role, role)
                            //};

                            //Creamos el principal
                            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            //Generamos la cookie. SignInAsync es un método de extensión del contexto.
                            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                            //Redirigimos a la Home
                            // Snackbar.Add("logueo exitoso", Severity.Info);
                            // return LocalRedirect("/tablero");

                            SesionDTO sesionUsuario = new SesionDTO();
                            sesionUsuario.Nombre = nombreUsuario;
                            sesionUsuario.Usuario = LoginDTO.Usuario;
                            sesionUsuario.Rol = "superAdmin";

                            await _sessionStorage.GuardarLogin(true);
                            var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
                            await autenticacionExt.ActualizarEstadoAutenticacion(sesionUsuario);
                            NavigationManager.NavigateTo("/tablero");

                        }
                        else
                        {
                            errorMessage = "Error al ingresar";
                            //Snackbar.Add("Credenciales incorrectas", Severity.Error);
                            // return LocalRedirect("/Invalid credentials");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                //Snackbar.Add("Error al ingresar", Severity.Error);
                //return LocalRedirect("/Invalid credentials");
                errorMessage = "Usuario o Clave inválida";
            }                   
        }
        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;
            }
                      
        }
        private async Task OnValidSubmit(EditContext context)
        {
            await IniciarSesion();

        }
    }
}
