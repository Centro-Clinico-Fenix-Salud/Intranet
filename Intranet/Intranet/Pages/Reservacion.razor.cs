using Intranet.Modelos.Agenda;
using Intranet.Modelos.Noticia;
using Intranet.Modelos.Reservacion;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Extensions;
using Serilog;
using System.Text.Json;
using static MudBlazor.Colors;

namespace Intranet.Pages
{
    public partial class Reservacion : ComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime Js { get; set; }
        List<Event> eventsData = new List<Event>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Event> EventsSalaDeJunta1 { get; set; } = new List<Event>();
        public List<Event> EventsSalaDeJunta2 { get; set; } = new List<Event>();
        public List<Event> EventsSalaDeCallCenter { get; set; } = new List<Event>();
        public List<Event> EventsSalaTecnica { get; set; } = new List<Event>();
        ReservacionCreate CreateReservacion = new ReservacionCreate();
        private string eventosJson;
        private bool mostrarModal { get; set; }
        public Reservacion reservacionComponente ;
        static int numero = 10;
        private string textoRandom = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen.";
        private bool SalaReunionSeleccionadaValid = true;
        private bool mostrarCalendario { get; set; }
        private bool mostrarModalNuevo { get; set; }
        private string SalaReunion { get; set; }
        private List<string> ListSalaReunion = new List<string>();
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string validationError = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await obtenerUbicacionAgenda();

            reservacionComponente = new Reservacion();

            EventsSalaDeJunta1.Add(new Event { title = "Curso mediQ", start = "2024-03-09 10:00:00", end = "2024-03-09 12:00:00", description = textoRandom, createdBy = "Aitor Blanco" });
            EventsSalaDeJunta1.Add(new Event { title = "Curso Primeros Auxilio", start = "2024-03-04 10:00:00", end = "2024-03-04 10:30:00", description = textoRandom, createdBy = "Ilay Blanco" });
            EventsSalaDeJunta1.Add(new Event { title = "induccion fenix", start = "2024-03-04 08:00:00", end = "2024-03-04 10:00:00", description = textoRandom,  createdBy = "Orlando Blanco" });
            EventsSalaDeJunta1.Add(new Event { title = "planificacion de inaguracion", start = "2024-03-04 13:30:00", end = "2024-03-04 15:30:00", description = textoRandom, createdBy = "Elizabeth Blanco" });
            EventsSalaDeJunta1.Add(new Event { title = "reunion trimestral", start = "2024-03-01 14:00:00", end = "2024-03-01 16:00:00", description = textoRandom, createdBy = "Ronald Blanco" });

            EventsSalaDeJunta2.Add(new Event { title = "Curso mediQ", start = "2024-03-11 10:00:00", end = "2024-03-11 12:00:00", description = textoRandom, createdBy = "Aitor Blanco" });
            EventsSalaDeJunta2.Add(new Event { title = "Curso Primeros Auxilio", start = "2024-03-06 10:00:00", end = "2024-03-06 10:30:00", description = textoRandom, createdBy = "Ilay Blanco" });
            EventsSalaDeJunta2.Add(new Event { title = "induccion fenix", start = "2024-03-06 08:00:00", end = "2024-03-06 10:00:00", description = textoRandom, createdBy = "Orlando Blanco" });
            EventsSalaDeJunta2.Add(new Event { title = "planificacion de inaguracion", start = "2024-03-06 13:30:00", end = "2024-03-06 15:30:00", description = textoRandom, createdBy = "Elizabeth Blanco" });
            EventsSalaDeJunta2.Add(new Event { title = "reunion trimestral", start = "2024-03-03 14:00:00", end = "2024-03-03 16:00:00", description = textoRandom, createdBy = "Ronald Blanco" });

            EventsSalaDeCallCenter.Add(new Event { title = "Curso mediQ", start = "2024-03-13 10:00:00", end = "2024-03-13 12:00:00", description = textoRandom, createdBy = "Aitor Blanco" });
            EventsSalaDeCallCenter.Add(new Event { title = "Curso Primeros Auxilio", start = "2024-03-08 10:00:00", end = "2024-03-08 10:30:00", description = textoRandom, createdBy = "Ilay Blanco" });
            EventsSalaDeCallCenter.Add(new Event { title = "induccion fenix", start = "2024-03-08 08:00:00", end = "2024-03-08 10:00:00", description = textoRandom, createdBy = "Orlando Blanco" });
            EventsSalaDeCallCenter.Add(new Event { title = "planificacion de inaguracion", start = "2024-03-08 13:30:00", end = "2024-03-08 15:30:00", description = textoRandom, createdBy = "Elizabeth Blanco" });
            EventsSalaDeCallCenter.Add(new Event { title = "reunion trimestral", start = "2024-03-05 14:00:00", end = "2024-03-05 16:00:00", description = textoRandom, createdBy = "Ronald Blanco" });

            EventsSalaTecnica.Add(new Event { title = "Curso mediQ", start = "2024-03-15 10:00:00", end = "2024-03-15 12:00:00", description = textoRandom, createdBy = "Aitor Blanco" });
            EventsSalaTecnica.Add(new Event { title = "Curso Primeros Auxilio", start = "2024-03-10 10:00:00", end = "2024-03-10 10:30:00", description = textoRandom, createdBy = "Ilay Blanco" });
            EventsSalaTecnica.Add(new Event { title = "induccion fenix", start = "2024-03-10 08:00:00", end = "2024-03-10 10:00:00", description = textoRandom, createdBy = "Orlando Blanco" });
            EventsSalaTecnica.Add(new Event { title = "planificacion de inaguracion", start = "2024-03-10 13:30:00", end = "2024-03-10 15:30:00", description = textoRandom, createdBy = "Elizabeth Blanco" });
            EventsSalaTecnica.Add(new Event { title = "reunion trimestral", start = "2024-03-07 14:00:00", end = "2024-03-07 16:00:00", description = textoRandom, createdBy = "Ronald Blanco" });

            //eventosJson = JsonSerializer.Serialize(Events);

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

        private void NuevoModalReservacion()
        {
            CreateReservacion = new ReservacionCreate();
            CreateReservacion.createdBy = "Aitor Blanco";
            //StateHasChanged();
            mostrarModalNuevo = true;

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

            bool result = await GuardarReservacion(CreateReservacion, SalaReunion);
            if (result) {
                await RefrescarCalendario();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalNuevo();
            } else {
                Snackbar.Add("Error al guardar registro", Severity.Error);
            }         
           
        }

        private async Task<bool> GuardarReservacion(ReservacionCreate createReservacion, string salaReunion) {
            bool result = false;

            try
            {

                string fechaStart = createReservacion .Fecha.ToIsoDateString() + " " + createReservacion.start.ToString();
                string fechaEnd = createReservacion.Fecha.ToIsoDateString() + " " + createReservacion.end.ToString();


                switch (salaReunion)
                {
                    case "Sala de Junta 1":
                        EventsSalaDeJunta1.Add(new Event { title = createReservacion.title, start = fechaStart, end = fechaEnd, description = createReservacion.description, createdBy = createReservacion.createdBy });
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeJunta1);
                        result = true;
                        break;
                    case "Sala de Junta 2":
                        EventsSalaDeJunta2.Add(new Event { title = createReservacion.title, start = fechaStart, end = fechaEnd, description = createReservacion.description, createdBy = createReservacion.createdBy });
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeJunta2);
                        result = true;
                        break;
                    case "Sala de Call center":
                        EventsSalaDeCallCenter.Add(new Event { title = createReservacion.title, start = fechaStart, end = fechaEnd, description = createReservacion.description, createdBy = createReservacion.createdBy });
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeCallCenter);
                        result = true;
                        break;
                    case "Sala Técnica":
                        EventsSalaTecnica.Add(new Event { title = createReservacion.title, start = fechaStart, end = fechaEnd, description = createReservacion.description, createdBy = createReservacion.createdBy });
                        eventosJson = JsonSerializer.Serialize(EventsSalaTecnica);
                        result = true;
                        break;
                    default:

                        break;
                }
            }catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
                Snackbar.Add("Error al guardar registro " + ex.Message, Severity.Error);
            }

            return result;
        }

        private void OnSalaReunionSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value)) 
            { 
                SalaReunion = value;
                mostrarCalendario = true;

                switch (value) 
                {
                    case "Sala de Junta 1":
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeJunta1);
                        break;
                    case "Sala de Junta 2":
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeJunta2);
                        break;
                    case "Sala de Call center":
                        eventosJson = JsonSerializer.Serialize(EventsSalaDeCallCenter);
                        break;
                    case "Sala Técnica":
                        eventosJson = JsonSerializer.Serialize(EventsSalaTecnica);
                        break;
                    default: 
                        
                        break;
                }

            }
            else {
                SalaReunion = string.Empty;
                mostrarCalendario = false;
            }              

            SalaReunionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task obtenerUbicacionAgenda()
        {
            ListSalaReunion.Add("Sala de Junta 1");
            ListSalaReunion.Add("Sala de Junta 2");
            ListSalaReunion.Add("Sala de Call center");
            ListSalaReunion.Add("Sala Técnica");

            ListSalaReunion.OrderBy(x => x).ToList();
        }
              

    }
}
