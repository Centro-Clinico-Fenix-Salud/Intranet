using Intranet.Interfaces;
using Intranet.Modelos.Cumpleanos;
using Intranet.Modelos.Reservacion;
using Intranet.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Text.Json;

namespace Intranet.Pages
{
    public partial class Cumpleanos : ComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime Js { get; set; }
        private bool arrows = true;
        private bool bullets = true;
        private bool enableSwipeGesture = true;
        private bool autocycle = true;
        private Transition transition = Transition.Slide;
        private List<Cumpleanero> cumpleaneros = new List<Cumpleanero>();
        private string eventosJson;
        [Inject]
        private IServicioReservacion ServicioReservacion { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await ObtenerCumpleanerosHoy();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               // await Js.InvokeVoidAsync("DataCalendar");
            }
            
            await RefrescarCalendario();
        }

        public async Task ObtenerCumpleanerosHoy()
        {
            var lista = JsonSerializer.Serialize(await ServicioReservacion.ObtenerFechaCumpleanosHoy());
            cumpleaneros.Add(new Cumpleanero { Imagen = "img/fotoEmpleado3.jpg", Nombre = "Maria", Apellido = "Perez", Departamento = "Adminstracion" });
            cumpleaneros.Add(new Cumpleanero { Imagen = "img/fotoEmpleado2.jpg", Nombre = "Carmen", Apellido = "Jimenez", Departamento = "Recursos humanos" });
        }

        private async Task RefrescarCalendario()
        {
            eventosJson = JsonSerializer.Serialize(await ServicioReservacion.ObtenerFechaCumpleanos());
            await Js.InvokeVoidAsync("DataCalendar3", eventosJson);
        }

    }
}
