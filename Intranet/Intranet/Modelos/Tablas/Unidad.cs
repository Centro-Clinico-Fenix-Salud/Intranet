using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Tablas
{
    public class Unidad
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        public List<AgendaTelefonica> agendaTelefonicas { get; set; }
    }
}
