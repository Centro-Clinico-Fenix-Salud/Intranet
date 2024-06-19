using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Noticia
{
    public class CreateNoticia
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public Guid IdCreador { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
