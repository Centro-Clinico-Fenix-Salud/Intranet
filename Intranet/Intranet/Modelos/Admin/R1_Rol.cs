using Intranet.Pages;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class R1_Rol
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public List<U1_Usuario> Usuarios { get; set; }
    }
}
