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
using Intranet.Modelos.Planillas.Configuracion;
using static MudBlazor.Icons;
using System.Xml;
using static MudBlazor.CategoryTypes;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Identity.Client;
using Intranet.Data;

namespace Intranet.Pages
{
    public partial class MantenimientoTecnico 
    {
        [Inject]
        private IServicioAgendaTelefonica ServicioAgendaTelefonica { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        private bool mostrarModalEliminar = false;
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private string RegistroEliminar = string.Empty;
        List<MaterialRevision> CreateRegistro = new List<MaterialRevision>();
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
        [Inject]
        private IntranetContext intranetContext { get; set; }

        private ClaimsPrincipal? user {  get; set; }
        private string AreaInforme { get; set; }
        private string zonaRevision { get; set; }
        private string TipozonaSelecionada { get; set; }

        private List<string> ListAreaInforme = new List<string>();
        private bool AreaInformeSeleccionadaValid = true;
        private bool MostrarFormulario { get; set; }
        DataPlanilla configPantalla { get; set; }
        List<TipoZonaRevision> listaTipoZona { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await RefrescarDataGrid();
            await obtenerUnidadAgenda();
            await obtenerUbicacionAgenda();
            await obtenerUsuarioAgenda();
            ListNroTelefono.Add(configuration["NroTelfonicoFenix"]);
            //await crearJson();   
            await obtenerListaAreaInforme();
            configPantalla = new DataPlanilla();
            listaTipoZona = new List<TipoZonaRevision>();
            MostrarFormulario = false;
            TipozonaSelecionada = string.Empty;
        }

        private async Task crearJson()
        {
            try
            {

                DataPlanilla data = new DataPlanilla();

                data.Titulo = "Oficina";
                data.Cuerpo.Add(await CreacionData("Alineado", "radio",true, "Operativo", "radio", true,"Puerta", "Bisagra", "cerradura", "Oficina"));
                data.Cuerpo.Add(await CreacionData("Alineado", "radio", true, "Operativo", "radio", true, "Puerta", "lavamanos", "piso", "Baño"));

                string json =  JsonSerializer.Serialize(data);

                ConfiguracionPantalla configuracion = new ConfiguracionPantalla();

                configuracion.InformeTituloId = Guid.Parse("D517AEEE-D38C-4C82-B4EA-C8FB6235F2D2");
                configuracion.InformeAreaId = Guid.Parse("2B315998-4E75-4D4B-BF6F-571B68E26EA9");
                configuracion.ConfigPantalla = json;

                intranetContext.configuracionPantalla.Add(configuracion);
                await intranetContext.SaveChangesAsync();

                DataPlanilla data2 = JsonSerializer.Deserialize<DataPlanilla>(json);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
        }

        private async Task<Cuerpo> CreacionData(string NombreCondicion1, string TipoCondicion1, bool EstatusCondicion1,
            string NombreCondicion2, string TipoCondicion2, bool EstatusCondicion2, string NombreMaterial, string NombreMaterial2,
            string NombreMaterial3, string NombreZona)
        {
            Cuerpo cuerpo1 = new Cuerpo();
            MaterialRevision material1 = new MaterialRevision();
            MaterialRevision material2 = new MaterialRevision();
            MaterialRevision material3 = new MaterialRevision();
            ZonaRevision zonaRevision1 = new ZonaRevision();
            List<TipoZonaRevision> tipoZonaRevision = new List<TipoZonaRevision>(); 
            Condicion condicion1 = new Condicion();
            Condicion condicion2 = new Condicion();

            condicion1.Nombre = NombreCondicion1;
            condicion1.Deshabilitado = EstatusCondicion1;
            condicion1.Tipo = TipoCondicion1;
            condicion2.Nombre = NombreCondicion2;
            condicion2.Deshabilitado = EstatusCondicion2;
            condicion2.Tipo = TipoCondicion2;

            material1.Nombre = NombreMaterial;
            material1.Propiedad.Add(condicion1);
            material1.Propiedad.Add(condicion2);

            material2.Nombre = NombreMaterial2;
            material2.Propiedad.Add(condicion1);
            material2.Propiedad.Add(condicion2);

            material3.Nombre = NombreMaterial3;
            material3.Propiedad.Add(condicion1);
            material3.Propiedad.Add(condicion2);

            if (NombreZona.Equals("Oficina")) {
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Tecnología" });
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Compras" });
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Recursos Humanos" });
            }
            if (NombreZona.Equals("Baño"))
            {
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Damas" });
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Caballeros" });
            }

            zonaRevision1.Nombre = NombreZona;
            zonaRevision1.tipoZonaRevision = tipoZonaRevision;
            zonaRevision1.materialRevision.Add(material1);
            zonaRevision1.materialRevision.Add(material2);
            zonaRevision1.materialRevision.Add(material3);
            zonaRevision1.materialRevision.Add(material1);
            zonaRevision1.materialRevision.Add(material2);
            zonaRevision1.materialRevision.Add(material3);
            zonaRevision1.materialRevision.Add(material1);
            zonaRevision1.materialRevision.Add(material2);
            zonaRevision1.materialRevision.Add(material3);
            cuerpo1.zonaRevision.Add(zonaRevision1);


            return cuerpo1;
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
            CreateRegistro = new List<MaterialRevision>();
            mostrarModalNuevo = true;
            if (configPantalla != null)
            if (configPantalla.Cuerpo.Count > 1)
            {
                MostrarFormulario = false;
            }
            else
            { MostrarFormulario = true; }                
            
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
            TipozonaSelecionada = string.Empty;
            zonaRevision = string.Empty;
            CreateRegistro = new List<MaterialRevision>();

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
        //private void OnUbicacionSeleccionadaChanged(string value)
        //{
        //    if(!string.IsNullOrEmpty(value))
        //    CreateAgenda.Ubicacion = value;

        //    UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        //}

        //private void OnNroTelefonicoSeleccionadaChanged(string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //        CreateAgenda.numeroTelefonico = value;

        //    UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        //}
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
            //CreateAgenda.Usuario = CreateAgenda.Usuario.Split('-')[0].Trim();
            //switch (await ServicioAgendaTelefonica.ConsultarAntesGuardarAgendaTelefonica(CreateAgenda))
            //{
            //    case 1:
            //        Snackbar.Add("El usuario ya se encuentra registrado", Severity.Error);
            //        return;
            //    case 2:
            //        Snackbar.Add("El número de extension ya se encuentra registrado", Severity.Error);
            //        return;                         
            //}
            //CreateAgenda.UsuarioModificador = await IdUsuario();

            //if (await ServicioAgendaTelefonica.GuardarAgendaTelefonica(CreateAgenda))
            //{
            //    await RefrescarDataGrid();
            //    Snackbar.Add("Registro exitoso", Severity.Info);
            //    CerrarModalNuevo();
            //}
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);
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

        private void OnAreaInformeSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
               AreaInforme = value;

                var area = intranetContext.informeArea.Where(x => x.Nombre == value).FirstOrDefault();
                var confi = intranetContext.configuracionPantalla.Where(x=> x.InformeAreaId == area.Id).FirstOrDefault();
                if(confi != null)
                configPantalla = JsonSerializer.Deserialize<DataPlanilla>(confi.ConfigPantalla);

            }
            else
            {
                AreaInforme = string.Empty;
            //    mostrarCalendario = false;
            }

            AreaInformeSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private void OnZonaRevisionSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //MostrarFormulario = true;
                zonaRevision = value;
                TipozonaSelecionada = string.Empty;
                MostrarFormulario = false;
                List<ZonaRevision> listaZona = new List<ZonaRevision>();
                listaTipoZona = new List<TipoZonaRevision>();
                var listaCuerpo = configPantalla.Cuerpo.ToList();
                foreach (var zona in listaCuerpo) 
                {
                    var CuerpoIndividual = zona;

                    if (CuerpoIndividual.zonaRevision.Any(x => x.Nombre == value)) 
                    {
                        listaZona = CuerpoIndividual.zonaRevision.Where(x => x.Nombre == value).ToList();                      
                    }                
                }

                var zonaIndividual = listaZona.Where(x => x.Nombre == value).FirstOrDefault();
                listaTipoZona = zonaIndividual.tipoZonaRevision.ToList();
                CreateRegistro = zonaIndividual.materialRevision;
               // configZona = zonaIndividual.materialRevision;
                //CreateRegistro = configPantalla.Cuerpo.Where(x=> x.zonaRevision.Where(y=> y.Nombre == value).FirstOrDefault() == "").FirstOrDefault();

                //var area = intranetContext.informeArea.Where(x => x.Nombre == value).FirstOrDefault();
                //var confi = intranetContext.configuracionPantalla.Where(x => x.InformeAreaId == area.Id).FirstOrDefault();
                //if (confi != null)
                //    configPantalla = JsonSerializer.Deserialize<DataPlanilla>(confi.ConfigPantalla);

            }
            else
            {
               // MostrarFormulario = true;
                //AreaInforme = string.Empty;
                ////    mostrarCalendario = false;
            }

            AreaInformeSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private void OnTipoZonaSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //MostrarFormulario = true;
                TipozonaSelecionada = value;
                MostrarFormulario = true;

                //List<ZonaRevision> listaZona = new List<ZonaRevision>();
                //listaTipoZona = new List<TipoZonaRevision>();
                //var listaCuerpo = configPantalla.Cuerpo.ToList();
                //foreach (var zona in listaCuerpo)
                //{
                //    var CuerpoIndividual = zona;

                //    if (CuerpoIndividual.zonaRevision.Any(x => x.Nombre == value))
                //    {
                //        listaZona = CuerpoIndividual.zonaRevision.Where(x => x.Nombre == value).ToList();
                //    }


                //}

                //var zonaIndividual = listaZona.Where(x => x.Nombre == value).FirstOrDefault();
                //listaTipoZona = zonaIndividual.tipoZonaRevision.ToList();
                //CreateRegistro = zonaIndividual.materialRevision;
                // configZona = zonaIndividual.materialRevision;
                //CreateRegistro = configPantalla.Cuerpo.Where(x=> x.zonaRevision.Where(y=> y.Nombre == value).FirstOrDefault() == "").FirstOrDefault();

                //var area = intranetContext.informeArea.Where(x => x.Nombre == value).FirstOrDefault();
                //var confi = intranetContext.configuracionPantalla.Where(x => x.InformeAreaId == area.Id).FirstOrDefault();
                //if (confi != null)
                //    configPantalla = JsonSerializer.Deserialize<DataPlanilla>(confi.ConfigPantalla);

            }
            else
            {
                MostrarFormulario = false;
                //AreaInforme = string.Empty;
                ////    mostrarCalendario = false;
            }

           // AreaInformeSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private async Task obtenerListaAreaInforme()
        {
            ListAreaInforme.Add("Oficina");
            ListAreaInforme.Add("Consultorio área APS");
            ListAreaInforme.Add("Suite");
            ListAreaInforme.Add("Habitacion");

            ListAreaInforme.OrderBy(x => x).ToList();
        }

    }

}
