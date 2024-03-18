using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components.Forms;

namespace Intranet.Interfaces
{
    public interface IArchivoImagen
    {
        Task SubirImagenes(List<ListaImagenCargada> listaImagenCargada, string ruta);
    }
}
