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

namespace Intranet.Pages
{
    public partial class Noticia
    {
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
        private bool hidePageNumber ;
        private bool hidePagination ;
        private bool hideRowsPerPage ;
        private string rowsPerPageString = "Rows per page:";
        private string infoFormat = "{first_item}-{last_item} of {all_items}";
        private string allItemsText = "All";
        private string texto = "Además de los datos clínicos que tengan relación con la situación actual del paciente, incorpora los datos de sus antecedentes personales y familiares, sus hábitos y todo aquello vinculado con su salud biopsicosocial. También incluye el proceso evolutivo, tratamiento y recuperación. La historia clínica no se limita a ser una narración o exposición de hechos simplemente, sino que incluye en una sección aparte los juicios, documentos, procedimientos, informaciones y consentimiento informado. El consentimiento informado del paciente, que se origina en el principio de autonomía, es un documento donde el paciente deja registrado y firmado su reconocimiento y aceptación sobre su situación de salud y/o enfermedad y participa en la toma de decisiones del profesional de la salud.";
        private bool mostrarModalNoticia = false;
        private bool mostrarModalNuevaNoticia = false;
        private bool mostrarModalEditarNoticia = false;
        private IEnumerable<NoticiaModel> Elements = new List<NoticiaModel>();
        private string ImagenModalNoticia;
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
        IBrowserFile Archivo { get; set; }
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
        private string ImagenAELiminar = string.Empty;

        //public IQueryable<Noticia> Elements { get; set; } = null;

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

        private void eliminar(string NombreArchivo)
        {
            //string fileName = "fotoEmpleado.jpg";
            //string filePath = Path.Combine(Environment.WebRootPath, "Files", NombreArchivo);
            string filePath = Path.Combine(Environment.WebRootPath, NombreArchivo);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                // Manejar el caso en el que el archivo no existe
                Snackbar.Add("No se encotro el archivo", Severity.Error);
            }
        }

        public List<NoticiaModel> Data()
        {
            var resultado = new List<NoticiaModel>();

            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Lorem Ipsum 1", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-1) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/repair.jpg", TituloNoticia = "Nunc finibus, massa ac finibus hendrerit", TextoNoticia = texto + " <br><br> " + texto, FechaNoticia = DateTime.Now.AddDays(-2) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Etiam sit amet laoree", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-3) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = " Nullam vitae libero", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-4) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Nam malesuada", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-5) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Vivamus rhoncus", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-6) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Nulla euismod quis nibh", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-7) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Mauris luctus ullamcorper porttitor.", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-8) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Orci varius natoque ", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-9) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = " Nulla a ante bibendum", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-10) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Phasellus ullamcorper tellus vitae elit hendrerit", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-11) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Morbi sagittis", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-12) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Phasellus ipsum neque", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-13) });
            resultado.Add(new NoticiaModel { Id = Guid.NewGuid(), Imagen = "Files/fotoEmpleado.jpg", TituloNoticia = "Aliquam diam dui", TextoNoticia = texto, FechaNoticia = DateTime.Now.AddDays(-14) });




            return resultado.OrderByDescending(a => a.FechaNoticia).ToList();
        }

        private void CerrarModalNoticia()
        {
           
            StateHasChanged();
            mostrarModalNoticia = false;
            ImagenModalNoticia = string.Empty;
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
           
            StateHasChanged();
            mostrarModalNuevaNoticia = true;

        }
        private void CerrarModalNuevaNoticia()
        {
            imagenSeleccionada = string.Empty;
            NombreimagenSeleccionada = string.Empty;
            StateHasChanged();
            mostrarModalNuevaNoticia = false;
 

        }
        private void CerrarModalEditarNoticia()
        {

            imagenSeleccionada = string.Empty;
            NombreimagenSeleccionada = string.Empty;
            StateHasChanged();
            mostrarModalEditarNoticia = false;


        }

        private async Task UploadFiles(IBrowserFile archivo)
        {
            //Archivo = e.File;

            //using var stream = archivo.OpenReadStream(tamañoMaximoArchivo);
            //using var ms = new MemoryStream();
            //await stream.CopyToAsync(ms);
            //imagenes.Add($"data:{archivo.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}");
            if (archivo != null)
            {
                if (archivo.Size < 2 * 1024 * 1024)
                {
                    Archivo = archivo;
                    try
                    {
                        //var resizedImageFile = await archivo.RequestImageFileAsync("image/png", 1024, 1024);
                        //var buffer = new byte[resizedImageFile.Size];
                        //await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                        //imagenSeleccionada = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
                        //NombreimagenSeleccionada = archivo.Name;

                        using var stream = archivo.OpenReadStream(2 * 1024 * 1024);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        imagenSeleccionada = $"data:{archivo.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}";
                        NombreimagenSeleccionada = archivo.Name;

                    }
                    catch (Exception ex)
                    {
                        imagenSeleccionada = string.Empty;
                        NombreimagenSeleccionada = string.Empty;
                        Snackbar.Add("Ocurrio un error", Severity.Error);

                    }
                }
                else 
                {
                    Snackbar.Add("Ocurrio un error", Severity.Error);
                }
               
            }
            else
            {
                imagenSeleccionada = string.Empty;
                NombreimagenSeleccionada = string.Empty;
            }
                


            }

        
        private void GuardarNuevaNoticia()
        {

           //StateHasChanged();
           //mostrarModalNuevaNoticia = false;


        }

        private async Task GuardarNuevaNoticia(EditContext context)
        {
            
            var file = Archivo;

            // Verificar si se seleccionó un archivo
            if (file != null)
            {

                string nombreArchivo = "Files\\" + Guid.NewGuid().ToString()+ Path.GetExtension(file.Name);
                var savePath = System.IO.Path.Combine(Environment.WebRootPath, nombreArchivo);
                //var savePath = System.IO.Path.Combine(Environment.WebRootPath, "Files", file.Name);

                try
                {
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.OpenReadStream().CopyToAsync(stream);
                    }
                    NoticiaModel noticiaModel = new NoticiaModel();
                    noticiaModel.Id = Guid.NewGuid();
                    noticiaModel.FechaNoticia = DateTime.Now;
                    noticiaModel.Imagen = nombreArchivo;
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

        public void Editar(NoticiaModel NoticiaEditar)
        {
            EditarNoticia = new EditarNoticia();
            EditarNoticia.Id = NoticiaEditar.Id;
            EditarNoticia.Imagen = NoticiaEditar.Imagen;
            EditarNoticia.TituloNoticia = NoticiaEditar.TituloNoticia;
            EditarNoticia.TextoNoticia = NoticiaEditar.TextoNoticia;
            EditarNoticia.FechaNoticia = NoticiaEditar.FechaNoticia;
            imagenSeleccionadaCargada = NoticiaEditar.Imagen;
            NombreimagenSeleccionadaCargada = NoticiaEditar.Imagen.Replace("Files/", ""); 

            mostrarModalEditarNoticia = true;

            //int extensionInt;
            //EditarAgenda.Id = direccionTelefonica.Item.Id;
            //EditarAgenda.Nombre = direccionTelefonica.Item.Nombre;
            //EditarAgenda.Unidad = direccionTelefonica.Item.Unidad;
            //EditarAgenda.Ubicacion = direccionTelefonica.Item.Ubicacion;
            //int.TryParse(direccionTelefonica.Item.Extension, out extensionInt);
            //EditarAgenda.Extension = extensionInt;

        }
        public void Eliminar(NoticiaModel NoticiaELiminar)
        {
            ImagenAELiminar = NoticiaELiminar.Imagen;
            RegistroEliminar = NoticiaELiminar.TituloNoticia ;
            IdELiminarNoticia = NoticiaELiminar.Id;
            mostrarModalEliminar = true;

        }

        public void eliminarImagenCargada() 
        {
            imagenSeleccionadaCargada = string.Empty;
            EditarNoticia.Imagen = string.Empty;
        }

        private void CerrarModalEliminar()
        {
            IdELiminarNoticia = Guid.Empty;
            RegistroEliminar = string.Empty;
            ImagenAELiminar = string.Empty;
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
                    eliminar(ImagenAELiminar);
                    //MaestroDireccionTelefonica = DireccionTelefonica = listAgenda.AsQueryable();
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
            var listAgenda = Elements.ToList();
            NoticiaModel noticiaAEditar = listAgenda.Where(a => a.Id == EditarNoticia.Id).FirstOrDefault();

            if (noticiaAEditar != null)
            {
                noticiaAEditar.FechaNoticia = EditarNoticia.FechaNoticia;
                noticiaAEditar.TituloNoticia = EditarNoticia.TituloNoticia;
                noticiaAEditar.TextoNoticia = EditarNoticia.TextoNoticia;
                noticiaAEditar.Imagen = EditarNoticia.Imagen;

                Elements = listAgenda.AsQueryable();
                CerrarModalEditarNoticia();
                Snackbar.Add("Modificación exitosa", Severity.Info);
            }

            //var file = Archivo;

            //// Verificar si se seleccionó un archivo
            //if (file != null)
            //{

            //    string nombreArchivo = "Files\\" + Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
            //    var savePath = System.IO.Path.Combine(Environment.WebRootPath, nombreArchivo);
            //    //var savePath = System.IO.Path.Combine(Environment.WebRootPath, "Files", file.Name);

            //    try
            //    {
            //        using (var stream = new FileStream(savePath, FileMode.Create))
            //        {
            //            await file.OpenReadStream().CopyToAsync(stream);
            //        }
            //        NoticiaModel noticiaModel = new NoticiaModel();
            //        noticiaModel.Id = Guid.NewGuid();
            //        noticiaModel.FechaNoticia = DateTime.Now;
            //        noticiaModel.Imagen = nombreArchivo;
            //        noticiaModel.TituloNoticia = CreateNoticia.TituloNoticia;
            //        noticiaModel.TextoNoticia = CreateNoticia.TextoNoticia;

            //        var listaData = Elements.ToList();
            //        listaData.Add(noticiaModel);
            //        Elements = listaData.AsQueryable().OrderByDescending(a => a.FechaNoticia).ToList(); ;

            //        Snackbar.Add("Se registro noticia con exito", Severity.Info);
            //    }
            //    catch (Exception ex)
            //    {
            //        Snackbar.Add("Ocurrio un error", Severity.Error);
            //    }

            //    CerrarModalNuevaNoticia();
            //}

        }

    }
}
