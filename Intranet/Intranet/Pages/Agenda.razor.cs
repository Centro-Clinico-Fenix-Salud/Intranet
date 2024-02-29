using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using static MudBlazor.Colors;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Intranet.Pages
{
    public partial class Agenda : ComponentBase
    {
        private string searchTerm;
        private bool _resizeColumn = true;
        private bool mostrarModalEliminar = false;
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private string RegistroEliminar = string.Empty;
        AgendaCreate CreateAgenda = new AgendaCreate();
        AgendaEditar EditarAgenda = new AgendaEditar();
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
        private Guid IdELiminarAgenda;

        [Parameter]
        public string parametro { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MaestroDireccionTelefonica = Data().AsQueryable();
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
            int extensionInt;
            EditarAgenda.Id = direccionTelefonica.Item.Id;
            EditarAgenda.Nombre = direccionTelefonica.Item.Nombre;
            EditarAgenda.Unidad = direccionTelefonica.Item.Unidad;
            EditarAgenda.Ubicacion = direccionTelefonica.Item.Ubicacion;
            int.TryParse(direccionTelefonica.Item.Extension, out extensionInt);
            EditarAgenda.Extension = extensionInt;

            mostrarModalEditar = true;

        }

        private void CerrarModalEditar()
        {
            GetDireccionTelefonica();
            mostrarModalEditar = false;
            StateHasChanged();
        }
        public void Eliminar (MudBlazor.CellContext<AgendaModel> direccionTelefonica)
        {
            RegistroEliminar = direccionTelefonica.Item.Nombre + " - " + direccionTelefonica.Item.Unidad;
            IdELiminarAgenda = direccionTelefonica.Item.Id;
            mostrarModalEliminar = true;
        }

        public void Nuevo()
        {
            CreateAgenda = new AgendaCreate();
            mostrarModalNuevo = true;
        }
        //public void NuevoRegistro()
        //{
        //    //validacion
        //    if (string.IsNullOrEmpty(CreateAgenda.Unidad)) {
        //        ChangeVariant("campo Unidad es requerido", Variant.Filled);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(CreateAgenda.Ubicacion))
        //    {
        //        ChangeVariant("campo Ubicacion es requerido", Variant.Filled);
        //        return;
        //    }
        //    editContext = new EditContext(CreateAgenda);

        //    editContext.Validate();

        //    if (editContext.Validate())
        //    {
        //        return;
        //    }


        //    mostrarModalNuevo = false;
        //}
        private void CerrarModalEliminar()
        {
            IdELiminarAgenda = Guid.Empty;
            StateHasChanged();
            mostrarModalEliminar = false;

        }
        private void CerrarModalNuevo()
        {
            GetDireccionTelefonica();          
            mostrarModalNuevo = false;
            CreateAgenda = new AgendaCreate();
            StateHasChanged();

        }
       
        private void EliminarRegistro()
        {
            var listAgenda = GetDireccionTelefonica();
            AgendaModel registroParaEliminar = listAgenda.Where(a => a.Id == IdELiminarAgenda).FirstOrDefault();

            if (registroParaEliminar != null)
            {
                listAgenda.Remove(registroParaEliminar);

                MaestroDireccionTelefonica = DireccionTelefonica = listAgenda.AsQueryable();
                CerrarModalEliminar();
                Snackbar.Add("Eliminacion exitosa", Severity.Info);
            }
       
        }

        public List<AgendaModel> Data()
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

        public List<AgendaModel> GetDireccionTelefonica()
        {
            var resultado = MaestroDireccionTelefonica;
          
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
        private void OnUbicacionSeleccionadaEditarChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                EditarAgenda.Ubicacion = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task OnValidSubmit(EditContext context)
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
            CerrarModalNuevo();
            Snackbar.Add("Registro exitoso", Severity.Info);
        }

        private async Task EditarAgente(EditContext context)
        {
            var listAgenda = GetDireccionTelefonica();
            AgendaModel agendaAEditar = listAgenda.Where(a => a.Id == EditarAgenda.Id).FirstOrDefault();

            if (agendaAEditar != null) 
            {
                agendaAEditar.Nombre = EditarAgenda.Nombre;
                agendaAEditar.Unidad = EditarAgenda.Unidad;
                agendaAEditar.Ubicacion = EditarAgenda.Ubicacion;
                agendaAEditar.Extension = EditarAgenda.Extension.ToString();

                MaestroDireccionTelefonica = DireccionTelefonica = listAgenda.AsQueryable();
                CerrarModalEditar();
                Snackbar.Add("Modificación exitosa", Severity.Info);
            }
         
        }

    }

}
