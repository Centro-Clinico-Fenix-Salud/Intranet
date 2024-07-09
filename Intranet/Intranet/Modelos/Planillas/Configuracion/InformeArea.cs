using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Modelos.Planillas.Configuracion
{
    public class InformeArea
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid InformeTituloId { get; set; }
        [NotMapped]
        public string? NombreInforme { get; set; }

    }
}
