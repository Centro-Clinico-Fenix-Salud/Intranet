using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Intranet.Modelos.Agenda;
using System.IO;
using MudBlazor;
using static MudBlazor.CategoryTypes;
using Intranet.Modelos.Noticia;
using System.Xml.Linq;
using MudBlazor.Charts.SVG.Models;
using static Azure.Core.HttpHeader;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Intranet.Interfaces;
using System.Collections.Generic;
using Intranet.Services;
using System.Security.Claims;
using Serilog;

namespace Intranet.Pages
{
    public partial class Noticia : Microsoft.AspNetCore.Components.ComponentBase
    {
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
        private bool hidePageNumber;
        private bool hidePagination;
        private bool hideRowsPerPage;
        private string rowsPerPageString = "Rows per page:";
        private string infoFormat = "{first_item}-{last_item} of {all_items}";
        private string allItemsText = "All";
        private string texto = "Además de los datos clínicos que tengan relación con la situación actual del paciente, incorpora los datos de sus antecedentes personales y familiares, sus hábitos y todo aquello vinculado con su salud biopsicosocial. También incluye el proceso evolutivo, tratamiento y recuperación. La historia clínica no se limita a ser una narración o exposición de hechos simplemente, sino que incluye en una sección aparte los juicios, documentos, procedimientos, informaciones y consentimiento informado. El consentimiento informado del paciente, que se origina en el principio de autonomía, es un documento donde el paciente deja registrado y firmado su reconocimiento y aceptación sobre su situación de salud y/o enfermedad y participa en la toma de decisiones del profesional de la salud.";
        private bool mostrarModalNoticia = false;
        private bool mostrarModalNuevaNoticia = false;
        private bool mostrarModalEditarNoticia = false;
        private IEnumerable<NoticiaDataTable> Elements = new List<NoticiaDataTable>();
        private List<DataImagen> ImagenModalNoticia;
        private string TituloModalNoticia;
        private string TextoModalNoticia;
        private DateTime? FechaModalNoticia;
        private bool arrows = true;
        private bool bullets = true;
        private bool enableSwipeGesture = true;
        private bool autocycle = true;
        private Transition transition = Transition.Slide;
        private bool mostrarModalEliminar = false;
        CreateNoticia CreateNoticia { get; set; }
        EditarNoticia EditarNoticia { get; set; }
        private string imagenSeleccionada;
        private string NombreimagenSeleccionada;
        private string imagenSeleccionadaCargada;
        private string NombreimagenSeleccionadaCargada;
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Parameter]
        public string? parametro { get; set; }
        private string RegistroEliminar = string.Empty;
        private Guid IdELiminarNoticia;
        private List<DataImagen> ImagenAELiminar = new List<DataImagen>();
        private List<ListaImagenCargada> listaImagenCargada = new List<ListaImagenCargada>();
        [Inject]
        private IServicioNoticias servicioNoticias { get; set; }
 
        [Inject]
        private IWebHostEnvironment Environment { get; set; }
        [Inject]
        private IConfiguration configuration { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private string ruta = string.Empty;

        protected override async Task OnInitializedAsync()
        {          
            await RefrescarDataGrid();
            CreateNoticia = new CreateNoticia();
            
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
            }
           
        }

        private async Task HandleFileUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;

            // Verificar si se seleccionó un archivo
            if (file != null)
            {
                var savePath = System.IO.Path.Combine(Environment.WebRootPath,"Files", file.Name);
               
                // Guardar el archivo en disco
                 try{
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.OpenReadStream().CopyToAsync(stream);
                    }
                }catch (Exception ex) 
                {
                    Log.Error(ex.Message);
                    Snackbar.Add("Ocurrio un error", Severity.Error);
                }
                
            }
        }

        private void eliminarImagen(List<DataImagen> ListNombreArchivo)
        {
            var rutaArchivo = configuration["RutaArchivosNoticia"];

            foreach (var NombreArchivo in ListNombreArchivo)
            {
                string identificadorArchivo = NombreArchivo.NombreFisico.Substring(NombreArchivo.NombreFisico.IndexOf("Files\\") + "Files\\".Length);
                string filePath3 = Path.Combine(rutaArchivo, identificadorArchivo);

                try
                {
                    File.Delete(filePath3);

                }
                catch (FileNotFoundException)
                {
                    Snackbar.Add("No se encontró el archivo " + identificadorArchivo, Severity.Error);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Snackbar.Add("Error al eliminar el archivo: " + ex.Message, Severity.Error);
                }
            }
            
        }

        public async Task<List<NoticiaDataTable>> Data()
        {

            var resultado = await servicioNoticias.ObtenerListaNoticias();

            return resultado.OrderByDescending(a => a.FechaNoticia).ToList();
        }
        private async Task RefrescarDataGrid()
        {
            Elements = (await Data()).AsQueryable();
          
        }

        private void CerrarModalNoticia()
        {
           
            StateHasChanged();
            mostrarModalNoticia = false;
            ImagenModalNoticia = new List<DataImagen>();
            TituloModalNoticia = string.Empty;
            TextoModalNoticia = string.Empty;
            FechaModalNoticia = null;

        }

        private void AbrirModalNoticia(NoticiaDataTable Noticia)
        {
            ImagenModalNoticia = Noticia.Imagen;
            TituloModalNoticia = Noticia.TituloNoticia;
            TextoModalNoticia = Noticia.TextoNoticia;
            FechaModalNoticia = Noticia.FechaNoticia;

            
            StateHasChanged();
            mostrarModalNoticia = true;

        }

        private async Task NuevoModalNoticia()
        {
            CreateNoticia = new CreateNoticia();
            CreateNoticia.IdCreador = Guid.Parse(await IdUsuario());
            StateHasChanged();
            mostrarModalNuevaNoticia = true;

        }
        private async Task CerrarModalNuevaNoticia()
        {
            await RefrescarDataGrid();
            imagenSeleccionada = string.Empty;
            NombreimagenSeleccionada = string.Empty;
            listaImagenCargada = new List<ListaImagenCargada>();
            mostrarModalNuevaNoticia = false;
 
        }
        private void CerrarModalEditarNoticia()
        {

            listaImagenCargada = new List<ListaImagenCargada>();
            StateHasChanged();
            mostrarModalEditarNoticia = false;


        }

        private async Task UploadFilesNuevo(IBrowserFile archivo)
        {
            int tamano = 2 * 1024 * 1024;
            if (archivo != null)
            {
                if (archivo.Size < 2 * 1024 * 1024)
                {
                   
                    try
                    {

                        using var stream = archivo.OpenReadStream(2 * 1024 * 1024);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);

                        listaImagenCargada.Add(new ListaImagenCargada { imagenSeleccionadaCargada = $"data:{archivo.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}", 
                            NombreimagenSeleccionadaCargada = archivo.Name,                         
                            NombreFisicoimagenSeleccionadaCargada = Guid.NewGuid().ToString() + Path.GetExtension(archivo.Name)
                        });

                    }
                    catch (Exception ex)
                    {

                        Log.Error(ex.Message);
                        listaImagenCargada = new List<ListaImagenCargada>();
                        Snackbar.Add("Ocurrio un error", Severity.Error);

                    }
                }
                else
                {
                    Snackbar.Add("la imagen excede el tamaño, debe ser igual o menor a : " + tamano, Severity.Error);
                }

            }
            else
            {

                listaImagenCargada = new List<ListaImagenCargada>();
            }

        }

        private async Task UploadFilesEditar(IBrowserFile archivo)
        {


        }


        private async Task GuardarNuevaNoticia(EditContext context)
        {
            List<string> ListNombreArchivo = new List<string>();

            try
            {
                var NoticiaId = await servicioNoticias.GuardarNoticia(CreateNoticia);
                ruta = string.Empty;
                await servicioNoticias.SubirImagenes(listaImagenCargada, NoticiaId);

                foreach (var item in listaImagenCargada) 
                {
                    ListNombreArchivo.Add(item.NombreFisicoimagenSeleccionadaCargada);
                }
                await RefrescarDataGrid();
                await CerrarModalNuevaNoticia();             
                Snackbar.Add("Se registro noticia con exito", Severity.Info);
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message);
                Snackbar.Add("Ocurrio un error al guardar imagen " + ex.Message, Severity.Error);
            }
          
        }

        public void guardarImagen(IBrowserFile file) 
        { 
        
        }

        public async Task Editar(NoticiaDataTable NoticiaEditar)
        {
            EditarNoticia = new EditarNoticia();
            EditarNoticia.Id = NoticiaEditar.Id;
            EditarNoticia.Imagen = NoticiaEditar.Imagen;
            EditarNoticia.TituloNoticia = NoticiaEditar.TituloNoticia;
            EditarNoticia.TextoNoticia = NoticiaEditar.TextoNoticia;
            EditarNoticia.FechaNoticia = NoticiaEditar.FechaNoticia;
            EditarNoticia.IdCreador = Guid.Parse(await IdUsuario());
            EditarNoticia.NombreModificacion = NoticiaEditar.NombreModificador;
            EditarNoticia.FechaModificacion = NoticiaEditar.FechaModificacion;


            foreach (var item in NoticiaEditar.Imagen) 
            {
                listaImagenCargada.Add(new ListaImagenCargada { NombreFisicoimagenSeleccionadaCargada = item.NombreFisico, 
                    NombreimagenSeleccionadaCargada = item.NombreImagen
                 });
            }
            
            mostrarModalEditarNoticia = true;

        }
        public void Eliminar(NoticiaDataTable NoticiaELiminar)
        {
            ImagenAELiminar = NoticiaELiminar.Imagen;
            RegistroEliminar = NoticiaELiminar.TituloNoticia ;
            IdELiminarNoticia = NoticiaELiminar.Id;
            mostrarModalEliminar = true;

        }

        public void eliminarImagenCargada(ListaImagenCargada item) 
        {
            listaImagenCargada.Remove(item);

        }

        public void eliminarImagenCargadaNuevo(ListaImagenCargada item)
        {
            listaImagenCargada.Remove(item);
         
        }

        private void CerrarModalEliminar()
        {
            IdELiminarNoticia = Guid.Empty;
            RegistroEliminar = string.Empty;
            ImagenAELiminar = new List<DataImagen>();
            StateHasChanged();
            mostrarModalEliminar = false;

        }
        private async Task EliminarNoticia()
        {
            
                try{
                if(await servicioNoticias.EliminarNoticia(IdELiminarNoticia))
                    eliminarImagen(ImagenAELiminar);
                //Elements = listNoticia.AsQueryable();
                    await RefrescarDataGrid();
                    CerrarModalEliminar();
                    Snackbar.Add("Eliminacion exitosa", Severity.Info);
                }
                catch(Exception ex) 
                {
                    Log.Error(ex.Message);
                    Snackbar.Add("Error a Eliminar noticia", Severity.Error);
                }
                

        }

        private async Task GuardarEditarNoticia(EditContext context)
        {
            if (listaImagenCargada.Count > 0)
            {
                List<DataImagen> listaImagenModificada = new List<DataImagen>();
                List<DataImagen> DataOriginal = EditarNoticia.Imagen;
                List<string> ListNombreArchivo = new List<string>();
                List<string> ListNombreArchivoOriginal = new List<string>();

                foreach (var item in listaImagenCargada)
                {
                    listaImagenModificada.Add(new DataImagen { NombreImagen = item.NombreimagenSeleccionadaCargada, 
                        NombreFisico = item.NombreFisicoimagenSeleccionadaCargada });
                }
                if (!listaImagenModificada.SequenceEqual(EditarNoticia.Imagen))
                {

                    List<DataImagen> listaImagenAEliminar = new List<DataImagen>();
                    List<ListaImagenCargada> listaImagenAgregar = new List<ListaImagenCargada>();

                    foreach (var item in DataOriginal) 
                    {
                        if (!listaImagenModificada.Any(x => x.NombreImagen == item.NombreImagen))
                            listaImagenAEliminar.Add(item);
                    }

                    if (listaImagenAEliminar.Count > 0) 
                    {
                        eliminarImagen(listaImagenAEliminar);
                        await servicioNoticias.EliminarArchivoNoticia(listaImagenAEliminar);
                    }
                       
                    foreach (var item in listaImagenCargada)
                        {
                            if(item.imagenSeleccionadaCargada != null)
                            listaImagenAgregar.Add(item);
                        }

                        if (listaImagenAgregar.Count > 0)
                        await servicioNoticias.SubirImagenes(listaImagenAgregar, EditarNoticia.Id);
                }

                await servicioNoticias.ActualizarNoticia(EditarNoticia);
                await RefrescarDataGrid();
                CerrarModalEditarNoticia();
                Snackbar.Add("Modificación exitosa", Severity.Info);

            }
            else
            {
                Snackbar.Add("La noticia debe tener al menos una imagen", Severity.Error);
            }
        }

        private async Task<string> IdUsuario()
        {
            return ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
