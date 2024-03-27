using Intranet.Modelos.Noticia;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Intranet.Pages
{
    public partial class Reservacion
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime Js { get; set; }
        List<Event> eventsData = new List<Event>();
        public List<Event> Events { get; set; } = new List<Event>();
        private string eventosJson;
        private bool mostrarModal { get; set; }
        public Reservacion reservacionComponente ;
        static int numero = 10;
        private string textoRandom = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen.";
        protected override async Task OnInitializedAsync()
        {
            reservacionComponente = new Reservacion();
            Events.Add(new Event { title = "Curso mediQ", start = "2024-03-09 10:00:00", end = "2024-03-09 12:00:00", description = textoRandom, createdBy = "Aitor Blanco" });
            Events.Add(new Event { title = "Curso Primeros Auxilio", start = "2024-03-04 10:00:00", end = "2024-03-04 10:30:00", description = textoRandom, createdBy = "Ilay Blanco" });
            Events.Add(new Event { title = "induccion fenix", start = "2024-03-04 08:00:00", end = "2024-03-04 10:00:00", description = textoRandom,  createdBy = "Orlando Blanco" });
            Events.Add(new Event { title = "planificacion de inaguracion", start = "2024-03-04 13:30:00", end = "2024-03-04 15:30:00", description = textoRandom, createdBy = "Elizabeth Blanco" });
            Events.Add(new Event { title = "reunion trimestral", start = "2024-03-01 14:00:00", end = "2024-03-01 16:00:00", description = textoRandom, createdBy = "Ronald Blanco" });
   
            eventosJson = JsonSerializer.Serialize(Events);


        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               
            }

            await RefrescarCalendario();

        }

        private async Task RefrescarCalendario() 
        {
            await Js.InvokeVoidAsync("DataCalendar2", eventosJson);
        }

        private void NuevoModalReservacion()
        {
            //CreateNoticia = new CreateNoticia();
            //StateHasChanged();
            //mostrarModalNuevaNoticia = true;

        }

        public class Event
        {
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string description { get; set; }
            public string createdBy { get; set; }

        }

        

    }
}
