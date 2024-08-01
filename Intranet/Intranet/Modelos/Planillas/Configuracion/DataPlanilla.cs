namespace Intranet.Modelos.Planillas.Configuracion
{
    public class DataPlanilla
    {
        public string? Titulo { get; set; }
        public bool AgruparCuerpos { get; set; }
        public List<Cuerpo>? Cuerpo { get; set; } = new List<Cuerpo>();
        public string? UsuarioCreador { get; set; }
        public string? FechaCreacion { get; set; }

    }
}
