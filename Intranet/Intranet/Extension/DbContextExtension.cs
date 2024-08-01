using Intranet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Intranet.Extension
{
    public static class DbContextExtension
    {
        public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<IntranetContext>(opt =>
            {
                // For Linux dotenv
                // var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                //  var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
                //  var dbUser = Environment.GetEnvironmentVariable("DATABASE_USER");
                //  var dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
                //  var dbName = Environment.GetEnvironmentVariable("DB_NAME");

                //PostgreSQL
                // var connectionString = $"Server={dbHost};port={dbPort};user id={dbUser};password={dbPassword};database={dbName};pooling=true";

                //MS SQL
                //var connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";

                //For appsettings.json
                //string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                string connectionString = builder.Configuration.GetConnectionString("TestDb");

                //For MS SQL
                 opt.UseSqlServer(connectionString);
                //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //For PostgreSQL
                //opt.UseNpgsql(connectionString);
            }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<IntranetContext>(
            sp => sp.GetRequiredService<IDbContextFactory<IntranetContext>>()
            .CreateDbContext());

            return builder;
        }

        public static void ExecuteMigrations(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            serviceScope
              .ServiceProvider
              .GetRequiredService<IDbContextFactory<IntranetContext>>()
              .CreateDbContext()
              .Database
              .Migrate();
        }
    }
}
