using static MudBlazor.CategoryTypes;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using static MudBlazor.Colors;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intranet.Pages
{
    public partial class Agenda
    {
        private string searchTerm;
        private bool _resizeColumn = true;
        private bool mostrarModalEliminar = false;
        private bool mostrarModalNuevo = false;
        private string RegistroEliminar = string.Empty;
        AgendaCreate CreateAgenda = new AgendaCreate();
        private List<string> ListUnidad = new List<string>();
        private List<string> ListUbicacion = new List<string>();
        public IQueryable<AgendaModel> MaestroDireccionTelefonica { get; set; } = null;
        public IQueryable<AgendaModel> DireccionTelefonica { get; set; } = null;
        //quitar 
        private bool resetValueOnEmptyText;
        private bool coerceText;
        private bool coerceValue;
        bool success;
        private bool UbicacionSeleccionadaValid = true;
        private EditContext editContext;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MaestroDireccionTelefonica = GetDireccionTelefonica().AsQueryable();
            DireccionTelefonica = MaestroDireccionTelefonica;
            await obtenerUnidadAgenda();
            await obtenerUbicacionAgenda();

        }

        private async Task obtenerUnidadAgenda() 
        {
            ListUnidad.Add("Admision");
            ListUnidad.Add("Adminstracion");
            ListUnidad.Add("Comunicaciones");
            ListUnidad.Add("Facturacion");
            ListUnidad.Add("Seguridad");
            ListUnidad.Add("Tecnologia");
            ListUnidad.Add("Almacen Central");
            ListUnidad.Add("Ambulatorio piso 4");
            ListUnidad.Add("Ambulatorio piso 5");
            ListUnidad.Add("Compras");
            ListUnidad.Add("Contabilidad");
            ListUnidad.Add("Farmacia");
            ListUnidad.Add("Hoteleria");


            ListUnidad.OrderBy(x => x).ToList();    

        }

        private async Task obtenerUbicacionAgenda()
        {
            ListUbicacion.Add("Torre quirurgica");
            ListUbicacion.Add("Anexo");
            ListUbicacion.Add("Torre norte");
            ListUbicacion.Add("Parque Cristal");
            ListUbicacion.Add("C. Convenciones");

            ListUbicacion.OrderBy(x => x).ToList();
        }

        public void Editar(MudBlazor.CellContext<AgendaModel> direccionTelefonica)
        {
            

        }
        public void Eliminar (MudBlazor.CellContext<AgendaModel> direccionTelefonica)
        {
            RegistroEliminar = direccionTelefonica.Item.Nombre + " - " + direccionTelefonica.Item.Unidad;
            mostrarModalEliminar = true;
        }

        public void Nuevo()
        {
            CreateAgenda = new AgendaCreate();
            mostrarModalNuevo = true;
        }
        public void NuevoRegistro()
        {
            //validacion
            if (string.IsNullOrEmpty(CreateAgenda.Unidad)) {
                ChangeVariant("campo Unidad es requerido", Variant.Filled);
                return;
            }
            if (string.IsNullOrEmpty(CreateAgenda.Ubicacion))
            {
                ChangeVariant("campo Ubicacion es requerido", Variant.Filled);
                return;
            }
            editContext = new EditContext(CreateAgenda);

            editContext.Validate();

            if (editContext.Validate())
            {
                return;
            }


            mostrarModalNuevo = false;
        }
        void CerrarModalEliminar()
        {
            mostrarModalEliminar = false;
        }
        void CerrarModalNuevo()
        {
            mostrarModalNuevo = false;
           
        }
        void Cancel() => MudDialog.CancelAll();

        void EliminarRegistro()
        {
            // Lógica para eliminar el registro aquí
            mostrarModalEliminar = false;
        }

        public List<AgendaModel> GetDireccionTelefonica()
        {
            var resultado = new List<AgendaModel>();

            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Luis Trujillo", Unidad = "Lavanderia", Ubicacion = "Anexo", Extension = "32" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Juan De Sousa", Unidad = "tecnologia", Ubicacion = "Torre quirurgica", Extension = "85" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Ivan Quintana", Unidad = "Compras", Ubicacion = "C. Convenciones", Extension = "965" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Giovanni Liguori", Unidad = "Farmacia", Ubicacion = "Anexo", Extension = "130" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Karyn Barreto", Unidad = "Contabilidad", Ubicacion = "Torre quirurgica", Extension = "89" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Omar Acosta", Unidad = "Consultoria", Ubicacion = "C. Convenciones", Extension = "74" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Maria Barreto", Unidad = "Trasporte", Ubicacion = "Anexo", Extension = "20" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Pedro Cario", Unidad = "Ambulatorio", Ubicacion = "Torre quirurgica", Extension = "69" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Carmen Serrano", Unidad = "Seguridad", Ubicacion = "C. Convenciones", Extension = "98" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Jose jorges", Unidad = "Alamacen central", Ubicacion = "Anexo", Extension = "5" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Saray Rada", Unidad = "Talento Humano", Ubicacion = "Torre quirurgica", Extension = "36" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Andry Lanza", Unidad = "Administracion", Ubicacion = "C. Convenciones", Extension = "38" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Yorbelys Romero", Unidad = "Talento Humano", Ubicacion = "Anexo", Extension = "Ext 1" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Carmen Medina", Unidad = "Cocina", Ubicacion = "Torre quirurgica", Extension = "16" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Betty Melendez", Unidad = "tecnologia", Ubicacion = "C. Convenciones", Extension = "17" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Jesus Soto", Unidad = "Hoteleria", Ubicacion = "Anexo", Extension = "55" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Carlos Gonzalez", Unidad = "Seguridad", Ubicacion = "Torre quirurgica", Extension = "59" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Jaira Villegas", Unidad = "Talento Humano", Ubicacion = "C. Convenciones", Extension = "45" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Jesus Aparicio", Unidad = "Seguridad", Ubicacion = "Anexo", Extension = "82" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Nelso Colmenares", Unidad = "tecnologia", Ubicacion = "Torre quirurgica", Extension = "80" });
            resultado.Add(new AgendaModel { Id = Guid.NewGuid(), Nombre = "Orlando Mujica", Unidad = "Trasporte", Ubicacion = "C. Convenciones", Extension = "63" });

            return resultado.OrderBy(a => a.Nombre).ToList(); 
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
                DireccionTelefonica = GetDireccionTelefonica().AsQueryable();
            else
            {
                DireccionTelefonica = MaestroDireccionTelefonica.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Unidad.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.Ubicacion.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Extension.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(p => p.Nombre); ;

            }          
        }

        private async Task<IEnumerable<string>> Search2(string value)
       {
            IEnumerable<string> result = new List<string>();
            try {
                if (string.IsNullOrEmpty(value))
                {
                    return new List<string>();
                }
                result = ListUnidad.Where(x => x.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
            } catch(Exception e) {
            
            }
            

            return result;
        }
        private void OnUbicacionSeleccionadaChanged(string value)
        {
            if(!string.IsNullOrEmpty(value))
            CreateAgenda.Ubicacion = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void ChangeVariant(string message, Variant variant)
        {
            Snackbar.Configuration.SnackbarVariant = variant;
            Snackbar.Configuration.MaxDisplayedSnackbars = 10;
            Snackbar.Add($"Error {message}", Severity.Error);
        }

        private void OnValidSubmit(EditContext context)
        {
            var listAgenda = GetDireccionTelefonica();
            AgendaModel agendaModel = new AgendaModel();
            agendaModel.Id = Guid.NewGuid();
            agendaModel.Nombre = CreateAgenda.Nombre;
            agendaModel.Unidad = CreateAgenda.Unidad;
            agendaModel.Ubicacion = CreateAgenda.Ubicacion;
            agendaModel.Extension = CreateAgenda.Extension.ToString();

            listAgenda.Add(agendaModel);
            MaestroDireccionTelefonica = DireccionTelefonica = listAgenda.AsQueryable();
            mostrarModalNuevo = false;
            StateHasChanged();
        }

    }

}
