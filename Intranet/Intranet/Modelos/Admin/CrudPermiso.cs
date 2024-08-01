using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class CrudPermiso
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        public string NombreCategoria { get; set; }
        [Required]
        public string NombreSubCategoria { get; set; }
    }
}
