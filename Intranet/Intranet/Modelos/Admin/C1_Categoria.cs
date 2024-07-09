using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class C1_Categoria
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
    }
}
