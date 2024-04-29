using Blazored.SessionStorage;
using Intranet.Data;
using Intranet.Extension;
using Intranet.Interfaces;
using Intranet.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.RegisterDbContext();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.RegisterAppServices();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
//builder.Services.AddSingleton<IArchivoImagen, ArchivoImagen>();
builder.Services.AddControllers();
builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthenticationCore();

var app = builder.Build();
app.ExecuteMigrations();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// insercion de super admin si no hay usuarios registrados
new CreacionSuperAdmin(builder.Configuration["usuarioAdmin"]);

app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
