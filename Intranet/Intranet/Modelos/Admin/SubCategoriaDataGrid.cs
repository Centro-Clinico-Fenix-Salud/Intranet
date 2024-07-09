using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Admin
{
    public class SubCategoriaDataGrid
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public C1_Categoria Categoria { get; set; }
    }
}
