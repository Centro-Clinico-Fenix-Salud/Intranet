using Intranet.Modelos.Agenda;
using Intranet.Modelos.LoginModel;
using Intranet.Modelos.Noticia;
using Intranet.Modelos.Reservacion;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Intranet.Modelos
{
    public class intranetContext : DbContext
    {

        // public DbSet<AgendaModel> agenda { get; set; }
        //public DbSet<Event> evento { get; set; }
        public DbSet<Usuario> usuario { get; set; }        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("TestDb");       
            optionsBuilder.UseSqlServer(connectionString);

        }
    }
}
