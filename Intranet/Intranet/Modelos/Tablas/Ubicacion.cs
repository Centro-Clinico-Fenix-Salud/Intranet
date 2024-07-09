using Intranet.Modelos.Agenda;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Tablas
{
    public class Ubicacion
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        public List<AgendaTelefonica> agendaTelefonicas { get; set; }
    }
}
