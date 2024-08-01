namespace Intranet.Modelos.Admin
{
    public class TipoDeRol
    {
        public List<C1_Categoria> Categorias { get; set; }
        public List<S1_SubCategoria> SubCategorias { get; set; }
        public List<P1_Permiso> Permisos { get; set; }
        public List<Permisos_SubCategoria> Permisos_SubCategorias { get; set; }
        public List<Categoria_SubCategoria> Categoria_SubCategoria { get; set; }

    }
}
