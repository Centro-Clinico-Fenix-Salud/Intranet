using Intranet.Modelos.Cumpleanos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

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
            await Js.InvokeVoidAsync("DataCalendar");
        }

        public async Task ObtenerCumpleanerosHoy()
        {
            cumpleaneros.Add(new Cumpleanero { Imagen = "img/fotoEmpleado3.jpg", Nombre = "Maria", Apellido = "Perez", Departamento = "Adminstracion" });
            cumpleaneros.Add(new Cumpleanero { Imagen = "img/fotoEmpleado2.jpg", Nombre = "Carmen", Apellido = "Jimenez", Departamento = "Recursos humanos" });

        }

    }
}
