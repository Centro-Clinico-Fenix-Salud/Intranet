using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components.Forms;

namespace Intranet.Interfaces
{
    public interface IServicioNoticias
    {
        Task SubirImagenes(List<ListaImagenCargada> listaImagenCargada, Guid NoticiaId);
        Task<Guid> GuardarNoticia(CreateNoticia CreateNoticia);
        Task<List<NoticiaDataTable>> ObtenerListaNoticias();
        Task<bool> EliminarNoticia(Guid NoticiaId);
        Task<bool> EliminarArchivoNoticia(List<DataImagen> ListNombreArchivo);
        Task ActualizarNoticia(EditarNoticia editarNoticia);
    }
    
}
