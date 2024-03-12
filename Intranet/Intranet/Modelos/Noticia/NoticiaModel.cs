﻿using System.ComponentModel.DataAnnotations;

namespace Intranet.Modelos.Noticia
{
    public class NoticiaModel
    {
        public Guid Id { get; set; }
        public string? Imagen { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
