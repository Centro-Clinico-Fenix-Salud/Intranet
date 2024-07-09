﻿using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Planillas.InspeccionHabitaciones
{
    public class DatosPaciente
    {
        [Required(ErrorMessage = "El campo Nombre y Apellido es requerido")]
        public string NombreApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(90, ErrorMessage = "no puede exceder de 90 caracteres")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Edad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(7, ErrorMessage = "Debe tener un minimo de 7 digitos")]
        [MaxLength(8, ErrorMessage = "Acepta un maximo de 8 digitos")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(10, ErrorMessage = "Debe tener un minimo de 10 digitos")]
        [MaxLength(15, ErrorMessage = "Acepta un maximo de 15 digitos")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo Nro. de habitación es requerido")]
        [MaxLength(3, ErrorMessage = "Acepta un maximo de 3 digitos")]
        public string NumeroHabitacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2, ErrorMessage = "Acepta un maximo de 3 digitos")]
        public string Piso { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo Fecha Egreso es requerido")]
        public DateTime? FechaEgreso { get; set; }

    }
}
