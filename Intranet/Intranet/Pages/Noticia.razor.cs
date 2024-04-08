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
        private IEnumerable<NoticiaModel> Elements = new List<NoticiaModel>();
        private List<string> ImagenModalNoticia;
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
        private List<string> ImagenAELiminar = new List<string>();
        private List<ListaImagenCargada> listaImagenCargada = new List<ListaImagenCargada>();
        [Inject]
        private IArchivoImagen ArchivoImagen { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Elements = Data().AsQueryable();
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
                // Especificar la ruta de destino en el disco local C:
                //var savePath = @"C:\arhivos de intranet\Noticia\" + file.Name;
                // var savePath = @"C:\Intranet\repo\Intranet\Intranet\Files\" + file.Name;
                var savePath = System.IO.Path.Combine(Environment.WebRootPath,"Files", file.Name);
               
                // Guardar el archivo en disco
                 try{
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.OpenReadStream().CopyToAsync(stream);
                    }
                }catch (Exception ex) 
                {
                    Snackbar.Add("Ocurrio un error", Severity.Error);
                }
                
            }
        }

        private void eliminarImagen(List<string> ListNombreArchivo)
        {
            //string fileName = "fotoEmpleado.jpg";
            //string filePath = Path.Combine(Environment.WebRootPath, "Files", NombreArchivo);
            foreach(var NombreArchivo in ListNombreArchivo) { 
                string filePath = Path.Combine(Environment.WebRootPath, NombreArchivo);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    // Manejar el caso en el que el archivo no existe
                    Snackbar.Add("No se encotro el archivo "+ NombreArchivo, Severity.Error);
                }
            }
        }

        public List<NoticiaModel> Data()
        {
            var resultado = new List<NoticiaModel>();

            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Lorem Ipsum 1", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-1) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/repair.jpg", "Files/fotoEmpleado.jpg" }, TituloNoticia = "Nunc finibus, massa ac finibus hendrerit", TextoNoticia = texto + " <br><br> " + texto, FechaNoticia = DateTime.Now.AddDays(-2) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Etiam sit amet laoree", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-3) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = " Nullam vitae libero", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-4) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Nam malesuada", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-5) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Vivamus rhoncus", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-6) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Nulla euismod quis nibh", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-7) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Mauris luctus ullamcorper porttitor.", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-8) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Orci varius natoque ", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-9) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = " Nulla a ante bibendum", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-10) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Phasellus ullamcorper tellus vitae elit hendrerit", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-11) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Morbi sagittis", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-12) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Phasellus ipsum neque", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-13) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = new List<string> { "Files/fotoEmpleado.jpg", "Files/repair.jpg" }, TituloNoticia = "Aliquam diam dui", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-14) });




            return resultado.OrderByDescending(a => a.FechaNoticia).ToList();
        }

        private void CerrarModalNoticia()
        {
           
            StateHasChanged();
            mostrarModalNoticia = false;
            ImagenModalNoticia = new List<string>();
            TituloModalNoticia = string.Empty;
            TextoModalNoticia = string.Empty;
            FechaModalNoticia = null;

        }

        private void AbrirModalNoticia(NoticiaModel Noticia)
        {
            ImagenModalNoticia = Noticia.Imagen;
            TituloModalNoticia = Noticia.TituloNoticia;
            TextoModalNoticia = Noticia.TextoNoticia;
            FechaModalNoticia = Noticia.FechaNoticia;

            
            StateHasChanged();
            mostrarModalNoticia = true;

        }

        private void NuevoModalNoticia()
        {
           CreateNoticia = new CreateNoticia();
            StateHasChanged();
            mostrarModalNuevaNoticia = true;

        }
        private void CerrarModalNuevaNoticia()
        {
            imagenSeleccionada = string.Empty;
            NombreimagenSeleccionada = string.Empty;
            listaImagenCargada = new List<ListaImagenCargada>();
            StateHasChanged();
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
                            NombreFisicoimagenSeleccionadaCargada = "Files\\" + Guid.NewGuid().ToString() + Path.GetExtension(archivo.Name)
                    });

                    }
                    catch (Exception ex)
                    {
                        
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
                string ruta = Environment.WebRootPath;
                await ArchivoImagen.SubirImagenes(listaImagenCargada, ruta);

                foreach (var item in listaImagenCargada) 
                {
                    ListNombreArchivo.Add(item.NombreFisicoimagenSeleccionadaCargada);
                }

            }
            catch (Exception e) 
            {
                Snackbar.Add("Ocurrio un error al guardar imagen " + e.Message, Severity.Error);
            }

            
            if (listaImagenCargada.Count > 0)
            {

                try
                {

                    NoticiaModel noticiaModel = new NoticiaModel();
                    noticiaModel.Id = Guid.NewGuid();
                    noticiaModel.FechaNoticia = DateTime.Now;
                    noticiaModel.Imagen = ListNombreArchivo;
                    noticiaModel.TituloNoticia = CreateNoticia.TituloNoticia;
                    noticiaModel.TextoNoticia = CreateNoticia.TextoNoticia;

                    var listaData = Elements.ToList();
                    listaData.Add(noticiaModel);
                    Elements = listaData.AsQueryable().OrderByDescending(a => a.FechaNoticia).ToList(); ;

                    Snackbar.Add("Se registro noticia con exito", Severity.Info);
                }
                catch (Exception ex)
                {
                    Snackbar.Add("Ocurrio un error", Severity.Error);
                }

                CerrarModalNuevaNoticia();
            }

        }

        public void guardarImagen(IBrowserFile file) 
        { 
        
        }

        public void Editar(NoticiaModel NoticiaEditar)
        {
            EditarNoticia = new EditarNoticia();
            EditarNoticia.Id = NoticiaEditar.Id;
            EditarNoticia.Imagen = NoticiaEditar.Imagen;
            EditarNoticia.TituloNoticia = NoticiaEditar.TituloNoticia;
            EditarNoticia.TextoNoticia = NoticiaEditar.TextoNoticia;
            EditarNoticia.FechaNoticia = NoticiaEditar.FechaNoticia;

            foreach (var item in NoticiaEditar.Imagen) 
            {
                listaImagenCargada.Add(new ListaImagenCargada { imagenSeleccionadaCargada = item, NombreimagenSeleccionadaCargada = item.Replace("Files/", "") });
            }
            
            mostrarModalEditarNoticia = true;

        }
        public void Eliminar(NoticiaModel NoticiaELiminar)
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
            ImagenAELiminar = new List<string>();
            StateHasChanged();
            mostrarModalEliminar = false;

        }
        private void EliminarNoticia()
        {
            var listNoticia = Elements.ToList();
            NoticiaModel registroParaEliminar = listNoticia.Where(a => a.Id == IdELiminarNoticia).FirstOrDefault();

            if (registroParaEliminar != null)
            {
                try{
                    listNoticia.Remove(registroParaEliminar);
                    eliminarImagen(ImagenAELiminar);                  
                    Elements = listNoticia.AsQueryable();
                    CerrarModalEliminar();
                    Snackbar.Add("Eliminacion exitosa", Severity.Info);
                }
                catch(Exception) 
                {
                    Snackbar.Add("Error a Eliminar noticia", Severity.Error);
                }
                

            }

        }

        private async Task GuardarEditarNoticia(EditContext context)
        {
            if (listaImagenCargada.Count > 0)
            {
                List<string> listaImagenModificada = new List<string>();
                List<string> DataOriginal = EditarNoticia.Imagen;
                List<string> ListNombreArchivo = new List<string>();
                foreach (var item in listaImagenCargada) 
                {
                    listaImagenModificada.Add(item.NombreimagenSeleccionadaCargada);
                }
                if (!listaImagenModificada.SequenceEqual(EditarNoticia.Imagen)) 
                {
                    //respaldo de los archivos antes de eliminar 
                    //foreach (var item in listaImagenCargada) 
                    //{
                    //    item.imagenSeleccionadaCargada = ObtenerBase64DesdeRuta(item.imagenSeleccionadaCargada);
                    //}

                    //eliminar archivos
                    List<string> listaImagenAEliminar = new List<string>();
                    List<ListaImagenCargada> listaImagenAgregar = listaImagenCargada;

                    listaImagenAEliminar = EditarNoticia.Imagen.Except(listaImagenModificada).ToList();

                    //foreach (var item in listaImagenCargada)
                    //{
                    //    listaImagenAEliminar.Remove(item.imagenSeleccionadaCargada);
                    //}
                    
                    if(listaImagenAEliminar.Count > 0)
                    eliminarImagen(listaImagenAEliminar);

                    //cargar los archivos
                    //foreach (var item in DataOriginal) 
                    //{
                    //    listaImagenAgregar.RemoveAll(x => x.imagenSeleccionadaCargada == item);
                    //}

                    listaImagenAgregar = listaImagenCargada.Where(x => !EditarNoticia.Imagen.Contains( x.NombreimagenSeleccionadaCargada)).ToList();

                    string ruta = Environment.WebRootPath;
                    if(listaImagenAgregar.Count > 0)
                    await ArchivoImagen.SubirImagenes(listaImagenAgregar, ruta);
                }
                
                var listAgenda = Elements.ToList();
                NoticiaModel noticiaAEditar = listAgenda.Where(a => a.Id == EditarNoticia.Id).FirstOrDefault();

                foreach (var item in listaImagenCargada)
                {
                    if(string.IsNullOrEmpty(item.NombreFisicoimagenSeleccionadaCargada))
                    ListNombreArchivo.Add(item.NombreimagenSeleccionadaCargada);
                    else
                        ListNombreArchivo.Add(item.NombreFisicoimagenSeleccionadaCargada);
                }

                if (noticiaAEditar != null)
                {
                    noticiaAEditar.FechaNoticia = EditarNoticia.FechaNoticia;
                    noticiaAEditar.TituloNoticia = EditarNoticia.TituloNoticia;
                    noticiaAEditar.TextoNoticia = EditarNoticia.TextoNoticia;
                    //noticiaAEditar.Imagen = EditarNoticia.Imagen;
                    noticiaAEditar.Imagen = ListNombreArchivo;
                    Elements = listAgenda.AsQueryable();
                    CerrarModalEditarNoticia();
                    Snackbar.Add("Modificación exitosa", Severity.Info);
                }

            }
            else {
                Snackbar.Add("La noticia debe tener al menos una imagen", Severity.Error);
            }


        }

    }
}
