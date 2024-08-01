namespace Intranet.Modelos.Noticia
{
    public class ArchivosNoticias
    {
        public Guid Id { get; set; }
        public Guid NoticiaId { get; set; }
        public string? NombreArchivo { get; set; }
        public string? NombreFisico { get; set; }
    }
}
