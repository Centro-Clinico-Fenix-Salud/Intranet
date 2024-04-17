using Blazored.SessionStorage;
using Intranet.Extension;
using Intranet.Modelos.LoginModel;
using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Intranet.Pages
{
    public partial class Loggin
    {
        [Inject]
        private ISessionStorageService _sessionStorage { get; set; }
        private async Task IniciarSesion()
        {
            SesionDTO sesionUsuario = new SesionDTO();
            sesionUsuario.Nombre = "Super Admin";
            sesionUsuario.Usuario = "Admin";
            sesionUsuario.Rol = "superAdmin";

            await _sessionStorage.GuardarLogin(true);
            var autenticacionExt = (AutenticacionExtension)autenticacionProvider;
            await autenticacionExt.ActualizarEstadoAutenticacion(sesionUsuario);
            NavigationManager.NavigateTo("/tablero");
        }
    }
}
