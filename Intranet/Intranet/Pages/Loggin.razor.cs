using Blazored.SessionStorage;
using Intranet.Extension;
using Intranet.Modelos.LoginModel;
using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
<<<<<<< Updated upstream
using MudBlazor;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.DirectoryServices;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;
using Intranet.Interfaces.Admin;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Net.Http;

=======
>>>>>>> Stashed changes

namespace Intranet.Pages
{
    public partial class Loggin
    {
        [Inject]
        private ISessionStorageService _sessionStorage { get; set; }
<<<<<<< Updated upstream
        [Parameter]
        public string ErrorMessage { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private IConfiguration Configuration { get; set; }

       
        private HttpClient HttpClient = new HttpClient();

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

            var response = await HttpClient.PostAsJsonAsync("/account/login", LoginDTO);

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
=======
        private async Task IniciarSesion()
        {
            SesionDTO sesionUsuario = new SesionDTO();
            sesionUsuario.Nombre = "Super Admin";
            sesionUsuario.Usuario = "Admin";
            sesionUsuario.Rol = "superAdmin";
>>>>>>> Stashed changes

            await _sessionStorage.GuardarLogin(true);
            var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
            await autenticacionExt.ActualizarEstadoAutenticacion(sesionUsuario);
            NavigationManager.NavigateTo("/tablero");
        }
    }
}
