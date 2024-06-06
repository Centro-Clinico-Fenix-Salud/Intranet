using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Agenda
{
    public class AgendaCreate : IValidatableObject
    {
        public Guid Id { get; set; }
        public string? Usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Unidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? numeroTelefonico { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Extension { get; set; }
        public Guid Concurrencia { get; set; }
        public string? UsuarioModificador { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Extension != null)
                if (Extension.Count() != 3)
                {
                    yield return new ValidationResult($"La extension debe tener 3 digitos", new[] { nameof(Extension) });
                }
        }
    }
}
