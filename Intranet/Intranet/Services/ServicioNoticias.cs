using Intranet.Data;
using Intranet.Interfaces;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Noticia;
using Intranet.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Intranet.Services
{
    public class ServicioNoticias : IServicioNoticias
    {
        private readonly IJSRuntime JSRuntime;
        private readonly IntranetContext intranetContext;
        private readonly NavigationManager navigationManager;
        private IConfiguration configuration;
        private string ruta = string.Empty;
        public ServicioNoticias(IJSRuntime jSRuntime, IntranetContext intranetContext, IConfiguration Configuration, NavigationManager NavigationManager) {
            
            JSRuntime = jSRuntime;
            this.intranetContext = intranetContext;
            this.configuration = Configuration;
            ruta = configuration["RutaArchivosNoticia"];
            this.navigationManager = NavigationManager;
        }
        public async Task SubirImagenes(List<ListaImagenCargada> listaImagenCargada, Guid NoticiaId)
        {
            try 
            {
                ArchivosNoticias archivosNoticias = new ArchivosNoticias();
             
                foreach (var item in listaImagenCargada)
                {

                    byte[] bytes = Convert.FromBase64String(item.imagenSeleccionadaCargada.Split(',')[1]);
                   
                    string rutaArchivo = Path.Combine(ruta, item.NombreFisicoimagenSeleccionadaCargada);                   

                    using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        fileStream.Write(bytes, 0, bytes.Length);
                    }

                    archivosNoticias = new ArchivosNoticias
                    {
                        Id = Guid.NewGuid(),
                        NoticiaId = NoticiaId,
                        NombreArchivo = item.NombreimagenSeleccionadaCargada,
                        NombreFisico = item.NombreFisicoimagenSeleccionadaCargada
                    };

                    intranetContext.archivosNoticias.Add(archivosNoticias);

                }
                intranetContext.SaveChanges();
            } catch (Exception ex)
            {

                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }          

        }

        public async Task<Guid> GuardarNoticia(CreateNoticia CreateNoticia) 
        {
            Guid result = Guid.Empty;
            try 
            {
                Modelos.Noticia.Noticia NuevaNoticia = new Modelos.Noticia.Noticia 
                {
                    Id = Guid.NewGuid(),
                    TituloNoticia = CreateNoticia.TituloNoticia,
                    TextoNoticia = CreateNoticia.TextoNoticia,
                    IdCreador = CreateNoticia.IdCreador,
                    IdModificador = null,
                    FechaNoticia = DateTime.Now,
                    Concurrencia = Guid.NewGuid()
                };

                intranetContext.noticias.Add(NuevaNoticia);
                await intranetContext.SaveChangesAsync();
                result = NuevaNoticia.Id;

            } 
            catch (Exception ex) { Log.Error(ex.Message + ex.StackTrace + ex.InnerException); }

            return result;
        
        
        }
        public async Task<List<NoticiaDataTable>> ObtenerListaNoticias() 
        {
            List<NoticiaDataTable> result = new List<NoticiaDataTable>();

            var listaNoticia = await intranetContext.noticias.ToListAsync();
            List<DataImagen> ListaArchivo = new List<DataImagen>();
            var listaUsuario = await intranetContext.u1_Usuario.ToListAsync();

            foreach (var item in listaNoticia) 
            {
                
                ListaArchivo = new List<DataImagen>();
                item.ArchivosNoticias = await intranetContext.archivosNoticias.Where( x => x.NoticiaId == item.Id).ToListAsync();
                foreach (var imagen in item.ArchivosNoticias) 
                {
                    ListaArchivo.Add(new DataImagen 
                    { 
                        NombreImagen = imagen.NombreArchivo,
                        NombreFisico = Path.Combine(navigationManager.BaseUri, "Noticia/Files", imagen.NombreFisico)
                    });
                }
                               
                result.Add(new NoticiaDataTable 
                { 
                    Id = item.Id, 
                    Imagen = ListaArchivo, 
                    TituloNoticia = item.TituloNoticia, 
                    TextoNoticia = item.TextoNoticia, 
                    FechaNoticia = item.FechaNoticia,
                    NombreModificador = listaUsuario.Where(x => x.Id == item.IdCreador).Select(u => u.FirstName).FirstOrDefault(),
                    FechaModificacion = item.FechaModificacion
                });
            }

            return result;
        }

        public async Task<bool> EliminarNoticia(Guid NoticiaId) 
        {
            bool result = false;
            try
            {
              var noticia = await intranetContext.noticias.Where( x => x.Id == NoticiaId ).FirstOrDefaultAsync();
                
                if (noticia != null) {

                    var ListaArchivosNoticia = await intranetContext.archivosNoticias.Where(x => x.NoticiaId == NoticiaId).ToListAsync();
                    
                    intranetContext.noticias.Remove(noticia);
                    intranetContext.archivosNoticias.RemoveRange(ListaArchivosNoticia);                    
                    await intranetContext.SaveChangesAsync();
                    result = true;
                }
                
            }
            catch (Exception ex) { Log.Error(ex.Message + ex.StackTrace + ex.InnerException); }

            return result;

        }

        public async Task<bool> EliminarArchivoNoticia(List<DataImagen> ListNombreArchivo)
        {
            bool result = false;
            try
            {

                var archivosAEliminar = intranetContext.archivosNoticias
                .AsEnumerable() // Agrega esta línea para cambiar a evaluación en cliente
                .Where(x => ListNombreArchivo.Any(item => item.NombreImagen == x.NombreArchivo))
                .ToList();

                if (archivosAEliminar.Count > 0)
                {
                    intranetContext.archivosNoticias.RemoveRange(archivosAEliminar);
                    await intranetContext.SaveChangesAsync();
                    result = true;
                }


            }
            catch (Exception ex) { Log.Error(ex.Message + ex.StackTrace + ex.InnerException); }

            return result;

        }

        public async Task ActualizarNoticia(EditarNoticia editarNoticia) 
        {
            try
            {

                    var NoticiaBD = intranetContext.noticias.Where(x => x.Id == editarNoticia.Id).FirstOrDefault();
                if (NoticiaBD != null) 
                {
                    NoticiaBD.TextoNoticia = editarNoticia.TextoNoticia;
                    NoticiaBD.TituloNoticia = editarNoticia.TituloNoticia;
                    NoticiaBD.FechaModificacion = DateTime.Now;
                    NoticiaBD.IdModificador = editarNoticia.IdCreador;

                    intranetContext.Entry(NoticiaBD).State = EntityState.Modified;
                    intranetContext.SaveChanges();
                }
                              
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            
        }
    }
}
