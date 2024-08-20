using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Reservacion
{
    public class ReservacionCreate : IValidatableObject
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string title { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public TimeSpan? start { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public TimeSpan? end { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? Fecha { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid createdBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (start.HasValue && end.HasValue && end <= start)
            {
                yield return new ValidationResult("La hora de fin debe ser mayor que la hora de inicio.", new[] { "end" });
            }
        }
    }

}
