using MudBlazor;

namespace Intranet.Modelos.Admin
{
    public class Rol_Permiso
    {
        public Guid Id { get; set; }
        public Guid R1_RolId { get; set; }
        public Guid P1_PermisoId { get; set; }
        //public R1_Rol Rol { get; set; }
        //public P1_Permiso Permiso { get; set; }
    }
}
