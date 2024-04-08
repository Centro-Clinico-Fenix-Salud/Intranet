using Intranet.Interfaces;
using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components.Forms;

namespace Intranet.Services
{
    public class ArchivoImagen : IArchivoImagen
    {
        public async Task SubirImagenes(List<ListaImagenCargada> listaImagenCargada, string ruta)
        {
            
            foreach (var item in listaImagenCargada) 
            {
                byte[] bytes = Convert.FromBase64String(item.imagenSeleccionadaCargada.Split(',')[1]);

                // Ruta donde se guardará el archivo en el servidor
                string rutaArchivo = Path.Combine(ruta, item.NombreFisicoimagenSeleccionadaCargada);

                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                }
            }
            

        }
    }
}
