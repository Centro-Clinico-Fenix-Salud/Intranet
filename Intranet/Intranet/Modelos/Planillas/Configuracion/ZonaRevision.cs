using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Planillas.Configuracion
{
    public class ZonaRevision
    {
        public string Nombre { get; set; }
        public List<MaterialRevision>? materialRevision { get; set; } = new List<MaterialRevision>();
        public List<TipoZonaRevision>? tipoZonaRevision { get; set; } = new List<TipoZonaRevision>();
    }

}

