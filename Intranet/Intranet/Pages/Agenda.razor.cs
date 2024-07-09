using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using static MudBlazor.Colors;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Intranet.Modelos.Admin;
using Intranet.Services.Admin;
using Intranet.Services;
using Intranet.Interfaces.Admin;
using Intranet.Interfaces;
using System.Security.Claims;
using System.Drawing;
using Serilog;

namespace Intranet.Pages
{
    public partial class Agenda : ComponentBase
    {
        [Inject]
        private IServicioAgendaTelefonica ServicioAgendaTelefonica { get; set; }
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
        private List<string> ListNombreUsuario = new List<string>();
        private List<string> ListNroTelefono = new List<string>();
        public IQueryable<AgendaTelefonicaDataGrid> MaestroDireccionTelefonica { get; set; } = null;
        public IQueryable<AgendaTelefonicaDataGrid> DireccionTelefonica { get; set; } = null;
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
        [Inject]
        private IConfiguration configuration { get; set; }

        private ClaimsPrincipal? user {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefrescarDataGrid();
            await obtenerUnidadAgenda();
            await obtenerUbicacionAgenda();
            await obtenerUsuarioAgenda();
            ListNroTelefono.Add(configuration["NroTelfonicoFenix"]);
                  
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // await Js.InvokeVoidAsync("DataCalendar");
            }
           
        }

        private async Task RefrescarDataGrid()
        {
            var resultado = (await Data()).AsQueryable();

            MaestroDireccionTelefonica = DireccionTelefonica = resultado;
        }

        public async Task<List<AgendaTelefonicaDataGrid>> Data()
        {
                      
            var resultado = await ServicioAgendaTelefonica.ObtenerListaAgendaTelefonica();

            return resultado.OrderBy(a => a.Usuario).ToList();
        }

        private async Task obtenerUnidadAgenda() 
        {
            ListUnidad =  await ServicioAgendaTelefonica.ObtenerListaUnidadDeAgenda();

            ListUnidad.OrderBy(x => x).ToList();    

        }

        private async Task obtenerUbicacionAgenda()
        {
            ListUbicacion = await ServicioAgendaTelefonica.ObtenerListaUbicacionDeAgenda();

            ListUbicacion.OrderBy(x => x).ToList();
        }

        private async Task obtenerUsuarioAgenda()
        {
            ListNombreUsuario = await ServicioAgendaTelefonica.ObtenerListaUsuarioDeAgenda();

            ListNombreUsuario.OrderBy(x => x).ToList();
        }

        public async void Editar(MudBlazor.CellContext<AgendaTelefonicaDataGrid> direccionTelefonica)
        {
           
            EditarAgenda.Id = direccionTelefonica.Item.Id;
            EditarAgenda.Usuario = direccionTelefonica.Item.Usuario;
            EditarAgenda.Unidad = direccionTelefonica.Item.Unidad;
            EditarAgenda.Ubicacion = direccionTelefonica.Item.Ubicacion;
            EditarAgenda.Extension = direccionTelefonica.Item.Extension;
            EditarAgenda.numeroTelefonico = direccionTelefonica.Item.numeroTelefonico;
            EditarAgenda.UsuarioModificador = direccionTelefonica.Item.UsuarioModificador;
            EditarAgenda.FechaModificacion = direccionTelefonica.Item.FechaModificacion != null ? direccionTelefonica.Item.FechaModificacion :
                direccionTelefonica.Item.FechaCreacion;

            mostrarModalEditar = true;

        }

        private void CerrarModalEditar()
        {          
            mostrarModalEditar = false;
            EditarAgenda = new AgendaEditar();
            StateHasChanged();
        }
        public void Eliminar (MudBlazor.CellContext<AgendaTelefonicaDataGrid> direccionTelefonica)
        {
            RegistroEliminar = direccionTelefonica.Item.Usuario + " - " + direccionTelefonica.Item.Unidad;
            IdELiminarAgenda = direccionTelefonica.Item.Id;
            mostrarModalEliminar = true;
        }

        public void Nuevo()
        {
            CreateAgenda = new AgendaCreate();
            //CreateAgenda.numeroTelefonico = configuration["NroTelfonicoFenix"];
            mostrarModalNuevo = true;
        }

        private void CerrarModalEliminar()
        {
            IdELiminarAgenda = Guid.Empty;
            StateHasChanged();
            mostrarModalEliminar = false;

        }
        private void CerrarModalNuevo()
        {
                      
            mostrarModalNuevo = false;
            CreateAgenda = new AgendaCreate();

        }
       
        private async Task EliminarRegistro()
        {
            if (!await ServicioAgendaTelefonica.ConsultarAgendaTelefonica(IdELiminarAgenda))
            {
                Snackbar.Add("El registro no existe", Severity.Error);
                return;
            }

            if (await ServicioAgendaTelefonica.EliminarAgendaTelefonica(IdELiminarAgenda))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro Eliminada", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }
        void CheckForEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                Buscar();
            }
        }

        private async void Buscar2(ChangeEventArgs e)
        {
            //string valor = e.Value.ToString();
            // Aquí va tu código para buscar con el valor ingresado
            //await Buscar();
            searchTerm = e.Value.ToString();
            if (string.IsNullOrEmpty(searchTerm))
            {
                DireccionTelefonica = MaestroDireccionTelefonica;

            }
            else
            {
                DireccionTelefonica = MaestroDireccionTelefonica.Where(p => p.Usuario.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Unidad.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.Ubicacion.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Extension.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                ).OrderBy(p => p.Usuario);

            }
        }


        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                DireccionTelefonica = MaestroDireccionTelefonica;

            }
            else
            {
                DireccionTelefonica = MaestroDireccionTelefonica.Where(p => p.Usuario.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Unidad.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.Ubicacion.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Extension.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                ).OrderBy(p => p.Usuario);

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
            } catch(Exception ex) {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
            
            return result;
        }

        private async Task<IEnumerable<string>> SearchUsuario(string value)
        {
            IEnumerable<string> result = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return new List<string>();
                }
                result = ListNombreUsuario.Where(x => x.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }


            return result;
        }
        private void OnUbicacionSeleccionadaChanged(string value)
        {
            if(!string.IsNullOrEmpty(value))
            CreateAgenda.Ubicacion = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private void OnNroTelefonicoSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                CreateAgenda.numeroTelefonico = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void OnNroTelefonicoSeleccionadaEditarChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                EditarAgenda.numeroTelefonico = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void OnUbicacionSeleccionadaEditarChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                EditarAgenda.Ubicacion = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task GuardarAgenda(EditContext context)
        {
            CreateAgenda.Usuario = CreateAgenda.Usuario.Split('-')[0].Trim();
            switch (await ServicioAgendaTelefonica.ConsultarAntesGuardarAgendaTelefonica(CreateAgenda))
            {
                case 1:
                    Snackbar.Add("El usuario ya se encuentra registrado", Severity.Error);
                    return;
                case 2:
                    Snackbar.Add("El número de extension ya se encuentra registrado", Severity.Error);
                    return;                         
            }
            CreateAgenda.UsuarioModificador = await IdUsuario();

            if (await ServicioAgendaTelefonica.GuardarAgendaTelefonica(CreateAgenda))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalNuevo();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        private async Task EditarAgente(EditContext context)
        {
            EditarAgenda.Usuario = EditarAgenda.Usuario.Split('-')[0].Trim();
            switch (await ServicioAgendaTelefonica.ConsultarAntesActualizarAgendaTelefonica(EditarAgenda))
            {
                case 1:
                    Snackbar.Add("El usuario ya se encuentra registrado", Severity.Error);
                    return;
                case 2:
                    Snackbar.Add("El número de extension ya se encuentra registrado", Severity.Error);
                    return;
            }
            EditarAgenda.UsuarioModificador = await IdUsuario();

            if (await ServicioAgendaTelefonica.ActualizarAgendaTelefonica(EditarAgenda))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalEditar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }

        private async Task<string> IdUsuario() {
            return ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }

}
