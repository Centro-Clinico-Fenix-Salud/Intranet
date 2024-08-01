using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.DireccionIp;
using Intranet.Modelos.Noticia;
using Intranet.Modelos.UsuarioDireccion;
using Intranet.Modelos.Planillas.Configuracion;
using Intranet.Modelos.Reservacion;
using Intranet.Modelos.Tablas;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Intranet.Data
{
    public class IntranetContext : DbContext
    {

        public DbSet<U1_Usuario> u1_Usuario { get; set; }
        public DbSet<R1_Rol> r1_Rol { get; set; }
        public DbSet<P1_Permiso> p1_Permiso { get; set; }
        public DbSet<Rol_Permiso> rol_Permiso { get; set; }
        public DbSet<C1_Categoria> c1_Categorias { get; set; }
        public DbSet<S1_SubCategoria> s1_SubCategorias { get; set; }
        public DbSet<Categoria_SubCategoria> categoria_SubCategorias { get; set; }
        public DbSet<Permisos_SubCategoria> permisos_SubCategorias { get; set; }
        public DbSet<AgendaTelefonica> agendaTelefonicas { get; set; }
        public DbSet<Unidad> unidades { get; set; }
        public DbSet<Ubicacion> ubicaciones { get; set; }
        public DbSet<Noticia> noticias { get; set; }
        public DbSet<ArchivosNoticias> archivosNoticias { get; set; }
        public DbSet<U2_UsuarioAgendaTelefonica> usuarioAgendaTelefonica { get; set; }
        public DbSet<ConfiguracionPantalla> configuracionPantalla { get; set; }       
        public DbSet<InformeTitulo> informeTitulo { get; set; }
        public DbSet<InformeArea> informeArea { get; set; }
        public DbSet<PlanillaDigitalRegistro> planillaDigitalRegistro { get; set; }
        public DbSet<DireccionIp> direccionIp { get; set; }
        public DbSet<UsuarioDireccion> usuarioDireccion { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString  = configuration.GetConnectionString("TestDb");
            optionsBuilder.UseSqlServer(connectionString);

     
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<U2_UsuarioAgendaTelefonica>()
              .HasIndex(u => u.Nombre)
              .IsUnique();

        }


    }
}
