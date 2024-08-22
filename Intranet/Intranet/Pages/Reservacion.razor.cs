using Intranet.Interfaces;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Noticia;
using Intranet.Modelos.Reservacion;
using Intranet.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Extensions;
using Serilog;
using System.Security.Claims;
using System.Text.Json;
using static MudBlazor.Colors;

namespace Intranet.Pages
{
    public partial class Reservacion : ComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime Js { get; set; }
        List<EventReservacion> eventsData = new List<EventReservacion>();
        public List<EventReservacion> Events { get; set; } = new List<EventReservacion>();
        public List<EventReservacionData> EventsSalaDeJunta1 { get; set; } = new List<EventReservacionData>();
        public List<EventReservacion> EventsSalaDeJunta2 { get; set; } = new List<EventReservacion>();
        public List<EventReservacion> EventsSalaDeCallCenter { get; set; } = new List<EventReservacion>();
        public List<EventReservacion> EventsSalaTecnica { get; set; } = new List<EventReservacion>();
        ReservacionCreate CreateReservacion = new ReservacionCreate();
        private string eventosJson;
        private bool mostrarModal { get; set; }
        public Reservacion reservacionComponente ;
        static int numero = 10;
        private string textoRandom = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen.";
        private bool SalaReunionSeleccionadaValid = true;
        private bool mostrarCalendario { get; set; }
        private bool mostrarModalNuevo { get; set; }
        //private string salaReunion { get; set; }
        private SalaReunion salaReunion = new SalaReunion();
        //private List<string> ListSalaReunion = new List<string>();
        private List<SalaReunion> ListSalaReunion = new List<SalaReunion>();
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string validationError = string.Empty;
        private bool mostrarModalEliminar = false;
        private string RegistroEliminar = string.Empty;
        private Guid idEvento { get; set; }
        [Inject]
        private IServicioReservacion ServicioReservacion { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await obtenerListaSalaReuniones();
            salaReunion = new SalaReunion();

            reservacionComponente = new Reservacion();
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               
            }

            if(mostrarCalendario)
            await RefrescarCalendario();

        }

        private async Task RefrescarCalendario() 
        {

            await Js.InvokeVoidAsync("DataCalendar2", eventosJson);
        }

        private async Task NuevoModalReservacion()
        {
            CreateReservacion = new ReservacionCreate();
            CreateReservacion.createdBy = Guid.Parse(await IdUsuario());
            //StateHasChanged();
            mostrarModalNuevo = true;

        }
        private async Task BtnMostrarModalEliminar()
        {
            mostrarModalEliminar = true;
            idEvento = await Js.InvokeAsync<Guid>("getIdEventoValue");
            var TituloEvento = await Js.InvokeAsync<string>("getTituloEventoValue");
            var HoraInicioEvento = await Js.InvokeAsync<string>("getHoraInicioEventoValue");
            var HoraFinalEvento = await Js.InvokeAsync<string>("getHoraFinalEventoValue");
            var FechaEvento = await Js.InvokeAsync<string>("getFechaEventoValue");

            RegistroEliminar = "<b> Titulo: </b>" + TituloEvento + "<b> Fecha: </b> " + FechaEvento + "<b> Hora:</b> " + HoraInicioEvento + "<b> a </b>" + HoraFinalEvento;
        }
        private async Task EliminarReservacion()
        {
            if (await ServicioReservacion.ConfirmarAntesEliminar(idEvento, await IdUsuario())) 
            {
                Snackbar.Add("Solo lo puede eliminar el creador del evento", Severity.Warning);
                return;
            }

            // servicio que elimine evento 
              var result = await ServicioReservacion.EliminarReservacion(idEvento) ;
            if (result)
            {
                eventosJson = JsonSerializer.Serialize(await ServicioReservacion.ObtenerEventos(salaReunion.Nombre));
                await RefrescarCalendario();
                Snackbar.Add("Eliminado", Severity.Info);
                CerrarModalEliminar();
            }
            else
            {
                Snackbar.Add("Error al Eliminado Evento", Severity.Error);
            }
        }
        private void CerrarModalNuevo()
        {            
            mostrarModalNuevo = false;
            CreateReservacion = new ReservacionCreate();
        }
        private async Task OnValidSubmit(EditContext context)
        {
            if (CreateReservacion.end <= CreateReservacion.start)
            {
                validationError = "La hora 'Hasta' debe ser posterior a la hora 'Desde'.";
                return;
            }

            if (await ServicioReservacion.ConsultarDisponibilidadEvento(CreateReservacion, salaReunion.Nombre)) 
            {
                Snackbar.Add("Horario ocupado", Severity.Error);
                return;
            }

            bool result = await ServicioReservacion.GuardarReservacion(CreateReservacion, salaReunion.Nombre);
            if (result) {
                eventosJson = JsonSerializer.Serialize(await ServicioReservacion.ObtenerEventos(salaReunion.Nombre));
                await RefrescarCalendario();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalNuevo();
            } else {
                Snackbar.Add("Error al guardar registro", Severity.Error);
            }         
           
        }

      
        private async void OnSalaReunionSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value)) 
            { 
                salaReunion.Nombre = value;
                mostrarCalendario = true;

                eventosJson = JsonSerializer.Serialize(await ServicioReservacion.ObtenerEventos(value));
                await RefrescarCalendario();
            }
            else {
                salaReunion.Nombre = string.Empty;
                mostrarCalendario = false;
            }              

            SalaReunionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task obtenerListaSalaReuniones()
        {

            ListSalaReunion = (await ServicioReservacion.ObtenerListaSalaReuniones())
                  .OrderBy(x => x.Nombre)
                  .ToList();

        }
        private void CerrarModalEliminar()
        {
            //IdELiminarAgenda = Guid.Empty;
            StateHasChanged();
            mostrarModalEliminar = false;

        }

        private async Task<string> IdUsuario()
        {
            return ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
