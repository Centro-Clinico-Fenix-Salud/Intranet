﻿using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Planillas.Configuracion
{
    public class ConfiguracionPantalla
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid InformeTituloId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid InformeAreaId { get; set; }
        public string? ConfigPantalla { get; set; }

    }
}