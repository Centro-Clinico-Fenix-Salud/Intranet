using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Noticia
{
    public class NoticiaModel
    {
        public string? Imagen { get; set; }
        public string? TextoNoticia { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
