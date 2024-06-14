namespace Intranet.Modelos.Noticia
{
    public class CreateNoticia
    {
        public Guid Id { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public Guid IdCreador { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
