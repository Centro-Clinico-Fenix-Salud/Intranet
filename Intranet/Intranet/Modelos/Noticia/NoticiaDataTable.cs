using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Noticia
{
    public class NoticiaDataTable
    {
        public Guid Id { get; set; }
        public List<DataImagen> Imagen { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public DateTime? FechaNoticia { get; set; }
        public string? NombreCreador { get; set; }
        public string? NombreModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class DataImagen 
    {
        public string? NombreImagen { get; set; }
        public string? NombreFisico { get; set; }
    }
}
