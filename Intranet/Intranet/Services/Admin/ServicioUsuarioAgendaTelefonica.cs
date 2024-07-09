using Intranet.Data;
using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Intranet.Services.Admin
{
    public class ServicioUsuarioAgendaTelefonica : IServicioUsuarioAgendaTelefonica
    {

        private readonly IntranetContext intranetContext;
        private IConfiguration configuration;
        public ServicioUsuarioAgendaTelefonica(IntranetContext intranetContext, IConfiguration Configuration)
        {

            this.intranetContext = intranetContext;
            this.configuration = Configuration;

        }
        public async Task<List<U2_UsuarioAgendaTelefonica>> ObtenerListaUsuario()
        {
            var result = intranetContext.usuarioAgendaTelefonica.ToList();

            return result;
        }

        public async Task<bool> ConsultarNombreUsuario(U2_UsuarioAgendaTelefonica usuario)
        {

            bool result = false;
            try
            {
                result = intranetContext.usuarioAgendaTelefonica.Any(x => x.Nombre == usuario.Nombre) || 
                    intranetContext.u1_Usuario.Any(x => x.FirstName == usuario.Nombre);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> GuardarUsuario(U2_UsuarioAgendaTelefonica Usuario)
        {

            bool result = false;
            try
            {
                if (!intranetContext.usuarioAgendaTelefonica.Any(x => x.Nombre == Usuario.Nombre))
                {
                    Usuario.Id = Guid.NewGuid();
                    Usuario.FechaCreacion = DateTime.Now;
                    Usuario.Concurrencia = Guid.NewGuid();

                    intranetContext.usuarioAgendaTelefonica.Add(Usuario);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ActualizarUsuario(U2_UsuarioAgendaTelefonica nuevoUsuario)
        {

            bool result = false;
            try
            {
                if(!await ConsultarAntesActualizarUsuario(nuevoUsuario))
                if (!await ConsultarUsuario(nuevoUsuario))
                {
                    var UsuarioBD = intranetContext.usuarioAgendaTelefonica.Where(x => x.Id == nuevoUsuario.Id).FirstOrDefault();
                    UsuarioBD.Nombre = nuevoUsuario.Nombre;
                    intranetContext.Entry(UsuarioBD).State = EntityState.Modified;
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ConsultarUsuario(U2_UsuarioAgendaTelefonica nuevoUsuario)
        {

            bool result = false;
            try
            {
                var UsuarioData = intranetContext.usuarioAgendaTelefonica.Where(x => x.Id == nuevoUsuario.Id).FirstOrDefault();
                if (UsuarioData != null)
                    result = String.Equals(UsuarioData.Nombre, nuevoUsuario.Nombre, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ConsultarAfiliacionUsuario(U2_UsuarioAgendaTelefonica nuevoUsuario)
        {

            bool result = false;
            try
            {
                 result = await intranetContext.agendaTelefonicas.AnyAsync(x => x.Usuario == nuevoUsuario.Id);
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ConsultarAntesActualizarUsuario(U2_UsuarioAgendaTelefonica EditarUsuario)
        {
            bool result = false;
            try
            {
                result = (await intranetContext.u1_Usuario.AnyAsync(x => x.FirstName == EditarUsuario.Nombre) ||
                   await intranetContext.usuarioAgendaTelefonica.AnyAsync(x => x.Nombre == EditarUsuario.Nombre && x.Id != EditarUsuario.Id));
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;
        }
        public async Task<bool> EliminarUsuario(U2_UsuarioAgendaTelefonica UsuarioData)
        {

            bool result = false;
            try
            {
                if (await ConsultarUsuario(UsuarioData) && !await ConsultarAfiliacionUsuario(UsuarioData))
                {

                    var UsuarioELiminar = intranetContext.usuarioAgendaTelefonica.Find(UsuarioData.Id);

                    intranetContext.usuarioAgendaTelefonica.Remove(UsuarioELiminar);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
    }
}
