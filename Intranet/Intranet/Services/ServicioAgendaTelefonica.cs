using Intranet.Data;
using Intranet.Interfaces;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Tablas;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Services
{
    public class ServicioAgendaTelefonica : IServicioAgendaTelefonica
    {
        private readonly IntranetContext intranetContext;
        private IConfiguration configuration;
        public ServicioAgendaTelefonica(IntranetContext intranetContext, IConfiguration Configuration)
        {
            this.intranetContext = intranetContext;
            this.configuration = Configuration;
        }

        public async Task<List<AgendaTelefonicaDataGrid>> ObtenerListaAgendaTelefonica() 
        {

                return await intranetContext.agendaTelefonicas
                    .Select(agenda => new AgendaTelefonicaDataGrid
                    {
                        Id = agenda.Id,
                        Usuario = intranetContext.u1_Usuario
                            .Where(x => x.Id == agenda.Usuario)
                            .Select(u => u.FirstName)
                            .FirstOrDefault() != null ? intranetContext.u1_Usuario
                            .Where(x => x.Id == agenda.Usuario)
                            .Select(u => u.FirstName)
                            .FirstOrDefault() : intranetContext.usuarioAgendaTelefonica
                            .Where(x => x.Id == agenda.Usuario)
                            .Select(u => u.Nombre)
                            .FirstOrDefault(),
                        Unidad = intranetContext.unidades
                        .Where(x => x.Id ==
                        intranetContext.usuarioDireccion
                        .Where(x => x.Usuario == agenda.Usuario)
                        .Select(i => i.UnidadId).FirstOrDefault()
                            )
                        .Select(u => u.Nombre)
                        .FirstOrDefault() ?? string.Empty,
                        Ubicacion = intranetContext.ubicaciones
                        .Where(x => x.Id ==
                        intranetContext.usuarioDireccion
                        .Where(x => x.Usuario == agenda.Usuario)
                        .Select(i => i.UbicacionId).FirstOrDefault()
                        )
                        .Select(u => u.Nombre)
                        .FirstOrDefault() ?? string.Empty,
                        numeroTelefonico = agenda.numeroTelefonico,
                        Extension = agenda.Extension,
                        UsuarioModificador = intranetContext.u1_Usuario
                            .Where(x => x.Id == agenda.UsuarioModificador)
                            .Select(u => u.FirstName)
                            .FirstOrDefault(),
                        Concurrencia = agenda.Concurrencia,
                        FechaCreacion = agenda.FechaCreacion,
                        FechaModificacion = agenda.FechaModificacion
                    })
                    .ToListAsync();            
        }

        public async Task<List<string>> ObtenerListaUnidadDeAgenda() 
        {
            return await intranetContext.unidades.Select(u => u.Nombre).ToListAsync();

        }
        public async Task<List<string>> ObtenerListaUbicacionDeAgenda()
        {
            return await intranetContext.ubicaciones.Select(u => u.Nombre).ToListAsync();

        }
        public async Task<List<string>> ObtenerListaUsuarioDeAgenda()
        {
            List<string> resultado = new List<string>();

            var listaUsuario = await intranetContext.u1_Usuario
               .Select(usuario => usuario.FirstName + " - " + usuario.Username)
               .ToListAsync();

            var listaUsuarioAgenda = await intranetContext.usuarioAgendaTelefonica
               .Select(usuario => usuario.Nombre)
               .ToListAsync();

            resultado.AddRange(listaUsuario);
            resultado.AddRange(listaUsuarioAgenda);

            return resultado;


        }
        public async Task<bool> GuardarAgendaTelefonica(AgendaCreate NuevoRegistro)
        {

            bool result = false;
            try
            {
               
                if (await ConsultarAntesGuardarAgendaTelefonica(NuevoRegistro) == 0)
                {
                   
                    Guid usuario = await intranetContext.u1_Usuario
                    .Where(x => x.FirstName == NuevoRegistro.Usuario)
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();

                    usuario = usuario != Guid.Empty ? usuario :
                        await intranetContext.usuarioAgendaTelefonica
                            .Where(x => x.Nombre == NuevoRegistro.Usuario)
                            .Select(u => u.Id)
                            .FirstOrDefaultAsync();
                    //var unidad = await intranetContext.unidades.FirstOrDefaultAsync(x => x.Nombre == NuevoRegistro.Unidad);
                    //var ubicacion = await intranetContext.ubicaciones.FirstOrDefaultAsync(x => x.Nombre == NuevoRegistro.Ubicacion);

                    if (usuario != Guid.Empty)
                    {
                        AgendaTelefonica agendaTelefonica = new AgendaTelefonica
                        {
                            Usuario = usuario,
                            //UnidadId = unidad.Id,
                            //UbicacionId = ubicacion.Id,
                            numeroTelefonico = NuevoRegistro.numeroTelefonico,
                            Extension = NuevoRegistro.Extension,
                            UsuarioModificador = Guid.Parse(NuevoRegistro.UsuarioModificador),
                            Concurrencia = Guid.NewGuid(),
                            FechaCreacion = DateTime.Now
                        };

                        intranetContext.agendaTelefonicas.Add(agendaTelefonica);
                        await intranetContext.SaveChangesAsync();
                        result = true;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException); ;
            }

            return result;

        }

        public async Task<int> ConsultarAntesGuardarAgendaTelefonica(AgendaCreate NuevoRegistro)
        {

            int result = 0;
            try
            {
                
                var usuario = await intranetContext.u1_Usuario.FirstOrDefaultAsync(x => x.FirstName == NuevoRegistro.Usuario);
                if (usuario != null && await intranetContext.agendaTelefonicas.AnyAsync(x => x.Usuario == usuario.Id))
                    result = 1;
                else
                {
                    var usuarioAgenda = await intranetContext.usuarioAgendaTelefonica.FirstOrDefaultAsync(x => x.Nombre == NuevoRegistro.Usuario);
                    if (usuarioAgenda != null && await intranetContext.agendaTelefonicas.AnyAsync(x => x.Usuario == usuarioAgenda.Id))
                        result = 1;
                }

                if (await intranetContext.agendaTelefonicas.AnyAsync(x => x.Extension == NuevoRegistro.Extension))
                    result = 2;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ConsultarAgendaTelefonica(Guid IdELiminarAgenda) 
        {
            var result = false;
            try
            {
                result =  await intranetContext.agendaTelefonicas.AnyAsync(x => x.Id == IdELiminarAgenda);                   
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;
        }
        public async Task<bool> EliminarAgendaTelefonica(Guid IdELiminarAgenda) 
        {
            var result = false;
            try
            {
                if (await ConsultarAgendaTelefonica(IdELiminarAgenda))
                {

                    var aELiminarAgendaTelfonica = intranetContext.agendaTelefonicas.Find(IdELiminarAgenda);

                    intranetContext.agendaTelefonicas.Remove(aELiminarAgendaTelfonica);
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

        public async Task<bool> ActualizarAgendaTelefonica(AgendaEditar EditarAgenda)
        {

            bool result = false;
            try
            {
             
                if (await ConsultarAntesActualizarAgendaTelefonica(EditarAgenda) == 0)
                {
                   
                    Guid usuario = await intranetContext.u1_Usuario
                    .Where(x => x.FirstName == EditarAgenda.Usuario)
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();

                    usuario = usuario != Guid.Empty ? usuario :
                        await intranetContext.usuarioAgendaTelefonica
                            .Where(x => x.Nombre == EditarAgenda.Usuario)
                            .Select(u => u.Id)
                            .FirstOrDefaultAsync();
                    //var unidad = await intranetContext.unidades.FirstOrDefaultAsync(x => x.Nombre == EditarAgenda.Unidad);
                    //var ubicacion = await intranetContext.ubicaciones.FirstOrDefaultAsync(x => x.Nombre == EditarAgenda.Ubicacion);

                    if (usuario != Guid.Empty)
                    {
                        var AgendaTelefonicaData = intranetContext.agendaTelefonicas.Where(x => x.Id == EditarAgenda.Id).FirstOrDefault();
                        AgendaTelefonicaData.Extension = EditarAgenda.Extension;
                        AgendaTelefonicaData.UsuarioModificador = Guid.Parse(EditarAgenda.UsuarioModificador);
                        AgendaTelefonicaData.Concurrencia = Guid.NewGuid();
                        AgendaTelefonicaData.Usuario = usuario ;
                        AgendaTelefonicaData.FechaModificacion = DateTime.Now;
                        //AgendaTelefonicaData.UnidadId = unidad.Id;
                        //AgendaTelefonicaData.UbicacionId = ubicacion.Id;
                        AgendaTelefonicaData.numeroTelefonico = EditarAgenda.numeroTelefonico;

                        intranetContext.Entry(AgendaTelefonicaData).State = EntityState.Modified;
                        await intranetContext.SaveChangesAsync();
                        result = true;
                        
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<int> ConsultarAntesActualizarAgendaTelefonica(AgendaEditar EditarAgenda)
        {
            int result = 0;
            try
            {
                var usuario = await intranetContext.u1_Usuario.FirstOrDefaultAsync(x => x.FirstName == EditarAgenda.Usuario);
                if (usuario != null && await intranetContext.agendaTelefonicas.AnyAsync(x => x.Usuario == usuario.Id && x.Id != EditarAgenda.Id))
                    result = 1;
                else
                {
                    var usuarioAgenda = await intranetContext.usuarioAgendaTelefonica.FirstOrDefaultAsync(x => x.Nombre == EditarAgenda.Usuario);
                    if (usuarioAgenda != null && await intranetContext.agendaTelefonicas.AnyAsync(x => x.Usuario == usuarioAgenda.Id && x.Id != EditarAgenda.Id))
                        result = 1;
                }

                if (await intranetContext.agendaTelefonicas.AnyAsync(x => x.Extension == EditarAgenda.Extension && x.Id != EditarAgenda.Id))
                    result = 2;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;
        }



    }
}
