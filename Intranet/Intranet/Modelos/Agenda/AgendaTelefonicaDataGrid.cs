using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Agenda
{
    public class AgendaTelefonicaDataGrid
    {
        public Guid Id { get; set; }
        public string? Usuario { get; set; }
        public string? Unidad { get; set; }
        public string? Ubicacion { get; set; }
        public string? numeroTelefonico { get; set; }
        public string? Extension { get; set; }
        public string? UsuarioModificador { get; set; }
        public Guid Concurrencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
