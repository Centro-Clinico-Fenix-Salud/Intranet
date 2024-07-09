using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Agenda
{
    public class U2_UsuarioAgendaTelefonica
    {
        public Guid Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        public Guid UsuarioModificador { get; set; }
        [ConcurrencyCheck]
        public Guid Concurrencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
