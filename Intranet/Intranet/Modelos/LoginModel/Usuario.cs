using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.LoginModel
{
    public class Usuario
    {       
            public Guid Id { get; set; }
            [StringLength(50)]
            public string Username { get; set; }
            [StringLength(255)]
            public string Password { get; set; }
            [StringLength(100)]
            public string Email { get; set; }
            [StringLength(50)]
            public string FirstName { get; set; }
            [StringLength(50)]
            public string LastName { get; set; }
            public bool Active { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        
    }
}
