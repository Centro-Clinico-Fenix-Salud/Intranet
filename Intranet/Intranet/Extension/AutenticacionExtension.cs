using Blazored.SessionStorage;
using Intranet.Modelos.LoginModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Intranet.Extension
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly ISessionStorageService _sessionStorage;

        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Surname,sesionUsuario.Usuario),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));

                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            SesionDTO sesionUsuario = new SesionDTO();
            if (await _sessionStorage.ObtenerLogin())
             sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("sesionUsuario");
            else
                return await Task.FromResult(new AuthenticationState(_sinInformacion));
            //sesionUsuario.Nombre = "Super Admin";
            //sesionUsuario.Usuario = "Admin";
            // sesionUsuario.Rol = "superAdmin";

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Surname,sesionUsuario.Usuario),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));


            return await Task.FromResult(new AuthenticationState(claimPrincipal));

        }


    }
}
