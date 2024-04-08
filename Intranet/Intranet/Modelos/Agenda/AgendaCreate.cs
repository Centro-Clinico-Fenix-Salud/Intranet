using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Agenda
{
    public class AgendaCreate
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Unidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? Extension { get; set; }
    }
}
