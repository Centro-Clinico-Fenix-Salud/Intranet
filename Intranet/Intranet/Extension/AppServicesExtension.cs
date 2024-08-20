using Intranet.Data;
using Intranet.Interfaces;
using Intranet.Interfaces.Admin;
using Intranet.Services;
using Intranet.Services.Admin;

namespace Intranet.Extension
{
    public static class AppServicesExtension
    {
        public static void RegisterAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IServicioNoticias, ServicioNoticias>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<IServicioAdmin, ServicioAdmin>();
            builder.Services.AddScoped<IServicioAdmin, ServicioAdmin>();
            builder.Services.AddScoped<IServicioAgendaTelefonica, ServicioAgendaTelefonica>();
            builder.Services.AddScoped<IServicioUsuarioAgendaTelefonica, ServicioUsuarioAgendaTelefonica>();
            builder.Services.AddScoped<IServicioPlanillaDigital, ServicioPlanillaDigital>();
            builder.Services.AddScoped<IServicioDireccionIp, ServicioDireccionIp>();
            builder.Services.AddScoped<IServicioUsuarioDireccion, ServicioUsuarioDireccion>();
            builder.Services.AddScoped<IServicioReservacion, ServicioReservacion>();

        }
    }
}
