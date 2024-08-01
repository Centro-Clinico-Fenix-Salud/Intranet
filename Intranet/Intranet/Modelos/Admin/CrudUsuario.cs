using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class CrudUsuario
    {       
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid R1_RolId { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NombreRolId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
