using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Agenda
{
    public class U2_UsuarioAgendaTelefonica : IValidatableObject
    {
        public Guid Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        public Guid UsuarioModificador { get; set; }
        [ConcurrencyCheck]
        public Guid Concurrencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
                if (Nombre.Contains('-'))
                {
                    yield return new ValidationResult($"No se permite guion '-' en el nombre", new[] { nameof(Nombre) });
                }

        }
    }
}
