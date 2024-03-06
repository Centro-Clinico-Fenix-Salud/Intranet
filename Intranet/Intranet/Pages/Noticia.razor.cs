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

        private IEnumerable<NoticiaModel> Elements = new List<NoticiaModel>();     
        //public IQueryable<Noticia> Elements { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Elements = Data().AsQueryable();
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
                { }
                
            }
        }

        private void eliminar()
        {
            string fileName = "fotoEmpleado.jpg";
            string filePath = Path.Combine(Environment.WebRootPath, "Files", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                // Manejar el caso en el que el archivo no existe
            }
        }

        public List<NoticiaModel> Data()
        {
            var resultado = new List<NoticiaModel>();

            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto + texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });
            resultado.Add(new NoticiaModel { Imagen = "Files/fotoEmpleado.jpg", TextoNoticia = texto, FechaNoticia = DateTime.Now });




            return resultado.OrderBy(a => a.FechaNoticia).ToList();
        }

    }
}
