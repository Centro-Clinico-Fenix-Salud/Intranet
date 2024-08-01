using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Modelos.Admin
{
    public class P1_Permiso
    {
        public Guid Id { get; set; }
        public String Nombre { get; set; }
        public List<Rol_Permiso> Rol_Permisos { get; set; }
        [NotMapped]
        public bool Activo { get; set; }
    }
}
