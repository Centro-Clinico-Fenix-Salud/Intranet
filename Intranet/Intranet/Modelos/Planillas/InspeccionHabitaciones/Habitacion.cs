using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using static MudBlazor.Colors;

namespace Intranet.Modelos.Planillas.InspeccionHabitaciones
{
    public class Habitacion : IValidatableObject
    {
        public string Espejo { get; set; }
        public string Televisor { get; set; }
        public string ControlAireAcondicionado { get; set; }
        public string ControlTvCable { get; set; }
        public string ControlRemotoTv { get; set; }
        public string Ducha { get; set; }
        public string DuchaTelefono { get; set; }
        public string Sanitario { get; set; }
        public string Lavamanos { get; set; }
        public string Lamparas { get; set; }
        public string ParedesPintura { get; set; }
        public string JuegosSabanas { get; set; }
        public string Escabel { get; set; }
        public string CamaHospitalaria { get; set; }
        public string Mesa { get; set; }
        public string Divan { get; set; }
        public string ParalMedicamentos { get; set; }
        public string Papelera { get; set; }
        public string Cobija { get; set; }
        public string Almohada { get; set; }
        public string Observaciones { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Observaciones != null)
            if (Observaciones.Length > 200)
            {
                yield return new ValidationResult($"Hay {Observaciones.Length} caracteres y no debe exceder de 200 caracteres", new[] { nameof(Observaciones) });
            }
        }
    }
    
}
