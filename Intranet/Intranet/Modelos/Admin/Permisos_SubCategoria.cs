namespace Intranet.Modelos.Admin
{
    public class Permisos_SubCategoria
    {
        public Guid Id { get; set; }
        public Guid PermisoId { get; set; }
        public Guid SubCategoriaId { get; set; }
        public P1_Permiso Permiso { get; set; }
        public S1_SubCategoria SubCategoria { get; set; }
    }
}
