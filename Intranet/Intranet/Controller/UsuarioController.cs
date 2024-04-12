using Intranet.Modelos.LoginModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login) { 
        
            SesionDTO sesionDTO = new SesionDTO();
            if (login.Usuario == "admin" && login.Clave == "1234")
            {
                sesionDTO.Usuario = "admin";
                sesionDTO.Nombre = "super usuario";
                sesionDTO.Rol = "SuperUsuario";

                return StatusCode(StatusCodes.Status200OK, sesionDTO);

            }
            else {
                return StatusCode(StatusCodes.Status400BadRequest, sesionDTO);

            }


        }
    }
}
