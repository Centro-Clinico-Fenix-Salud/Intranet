namespace Intranet.Modelos.Noticia
{
    public class CreateNoticia
    {
        public Guid Id { get; set; }
        public List<string> Imagen { get; set; }
        public string? TituloNoticia { get; set; }
        public string? TextoNoticia { get; set; }
        public DateTime? FechaNoticia { get; set; }
    }
}
