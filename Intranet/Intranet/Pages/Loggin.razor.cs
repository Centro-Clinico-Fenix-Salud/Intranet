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
using Intranet.Interfaces.Admin;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;


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

        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private IConfiguration Configuration { get; set; }

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

        }
        private async Task IniciarSesion()
        {
            //Indicamos el dominio en el que vamos a buscar al usuario
            string path = "LDAP://fenixsalud.local";

            try
            {
                if (!(Configuration["usuarioAdmin"] == LoginDTO.Usuario && Configuration["Password"] == LoginDTO.Clave))
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
                                string role2 = "";
                                string nombreUsuario = "";


                                //setear rol a usuario
                                role = ServicioAdmin.BuscarRolDeUsuario(LoginDTO.Usuario);

                                //Comporbamos las propiedades del usuario
                                ResultPropertyCollection fields = result.Properties;
                                foreach (String ldapField in fields.PropertyNames)
                                {
                                    foreach (Object myCollection in fields[ldapField])
                                    {
                                        if (ldapField == "name")
                                            nombreUsuario = myCollection.ToString().ToLower();
                                    }
                                }

                                await EnviarDataSessionStorageYAutenticacion(nombreUsuario, LoginDTO.Usuario, role);

                            }
                            else
                            {
                                errorMessage = "Error al ingresar";
                            }

                        }
                    }
                else
                {
                    await EnviarDataSessionStorageYAutenticacion("Super Usuario", LoginDTO.Usuario, ServicioAdmin.BuscarRolDeUsuario(LoginDTO.Usuario));
                }

            }
            catch (Exception ex)
            {
                errorMessage = "Usuario o Clave inválida";
            }                   
        }

        public async Task  EnviarDataSessionStorageYAutenticacion(string nombreUsuario, string usuario, string role) 
        {

            SesionDTO sesionUsuario = new SesionDTO();
            sesionUsuario.Nombre = nombreUsuario;
            sesionUsuario.Usuario = usuario;
            sesionUsuario.Rol = role;

            await _sessionStorage.GuardarLogin(true);
            var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
            await autenticacionExt.ActualizarEstadoAutenticacion(sesionUsuario);
            NavigationManager.NavigateTo("/tablero");

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
