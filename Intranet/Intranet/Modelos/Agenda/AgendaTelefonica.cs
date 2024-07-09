using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Modelos.Agenda
{
    public class AgendaTelefonica
    {
        public Guid Id { get; set; }
        public Guid Usuario { get; set; }
        public Guid UnidadId { get; set; }
        public Guid UbicacionId { get; set; }
        [MaxLength(15)]
        public string? numeroTelefonico { get; set; }
        [MaxLength(10)]
        public string? Extension { get; set; }
        public Guid UsuarioModificador { get; set; }
        [ConcurrencyCheck]
        public Guid Concurrencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
