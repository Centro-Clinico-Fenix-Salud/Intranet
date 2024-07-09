using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Modelos.Noticia
{
    public class Noticia
    {
        public Guid Id { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public Guid IdCreador { get; set; }
        public Guid? IdModificador { get; set; }
        public DateTime? FechaNoticia { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [ConcurrencyCheck]
        public Guid Concurrencia { get; set; }
        [NotMapped]
        public List<ArchivosNoticias> ArchivosNoticias { get; set; }
    }
}
