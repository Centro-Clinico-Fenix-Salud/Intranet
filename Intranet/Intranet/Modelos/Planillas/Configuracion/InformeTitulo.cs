using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Planillas.Configuracion
{
    public class InformeTitulo
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public string? Nombre { get; set; }
    }
}
