namespace Intranet.Modelos.Admin
{
    public class Categoria_SubCategoria
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid SubCategoriaId { get; set; }
        public C1_Categoria Categoria { get; set; }
        public S1_SubCategoria SubCategoria { get; set; }
    }
}
