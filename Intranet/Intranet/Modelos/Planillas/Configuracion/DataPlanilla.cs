namespace Intranet.Modelos.Planillas.Configuracion
{
    public class DataPlanilla
    {
        public string? Titulo { get; set; }
        public List<Cuerpo>? Cuerpo { get; set; } = new List<Cuerpo>();
    }
}
