using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class PermisosDataGrid
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public C1_Categoria Categoria { get; set; }
        public S1_SubCategoria SubCategoria { get; set; }
    }
}
