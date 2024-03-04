using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Cumpleanos
{
    public class Cumpleanero
    {    
        public string? Imagen { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Departamento { get; set; }
    }
}
