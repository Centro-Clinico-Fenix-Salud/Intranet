using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class U1_Usuario
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Username { get; set; }
        public Guid R1_RolId { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }     

    }
}
