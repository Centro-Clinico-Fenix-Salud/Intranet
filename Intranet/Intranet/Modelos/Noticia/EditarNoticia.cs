namespace Intranet.Modelos.Noticia
{
    public class EditarNoticia
    {
        public Guid Id { get; set; }
        public List<DataImagen> Imagen { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public Guid IdCreador { get; set; }
        public string? NombreModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
