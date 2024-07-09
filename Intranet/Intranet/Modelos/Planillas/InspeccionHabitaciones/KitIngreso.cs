using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Planillas.InspeccionHabitaciones
{
    public class KitIngreso : IValidatableObject
    {
        public bool Jarra { get; set; }
        public bool Ponchera { get; set; }
        public bool Rinonera { get; set; }
        public bool PapelSanitario { get; set; }
        public bool Vaso { get; set; }
        public bool Bandeja { get; set; }
        public bool Pato { get; set; }
        public bool Jabonera { get; set; }
        public bool Pito { get; set; }
        public bool KitEspecial { get; set; }
        public bool Esquinero { get; set; }
        public bool Cobija { get; set; }
        public bool Almohada { get; set; }
        public bool Toalla { get; set; }
        public string Cama { get; set; }
        public string Piso { get; set; }
        public string Paredes { get; set; }
        public string Banos { get; set; }
 
        public string Observaciones { get; set; }
        public string DatosCamarera1 { get; set; }
        public string DatosCamarera2 { get; set; }
        public string DatosCamarera3 { get; set; }
        public string Turno { get; set; }
        public bool Jabon { get; set; }
        public bool Cloro { get; set; }
        public bool Desengrasante { get; set; }
        public bool AntimohoCal { get; set; }
        public string Otro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Observaciones != null)
                if (Observaciones.Length > 84)
            {
                yield return new ValidationResult($"Hay {Observaciones.Length} caracteres y no debe exceder de 84 caracteres", new[] { nameof(Observaciones) });
            }

            if (DatosCamarera1 != null)
                if (DatosCamarera1.Length > 111)
                {
                    yield return new ValidationResult($"Hay {DatosCamarera1.Length} caracteres y no debe exceder de 111 caracteres", new[] { nameof(DatosCamarera1) });
                }

            if (Otro != null)
                if (Otro.Length > 49)
                {
                    yield return new ValidationResult($"Hay {Otro.Length} caracteres y no debe exceder de 49 caracteres", new[] { nameof(Otro) });
                }
        }
    }
   
}
