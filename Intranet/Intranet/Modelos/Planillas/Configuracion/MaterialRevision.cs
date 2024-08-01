namespace Intranet.Modelos.Planillas.Configuracion
{
    public class MaterialRevision
    {
        public string? Nombre { get; set; }
        public List<Condicion>? Propiedad { get; set; } = new List<Condicion>();
    }
}
