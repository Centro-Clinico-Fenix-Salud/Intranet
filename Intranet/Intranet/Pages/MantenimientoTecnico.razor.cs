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
using Intranet.Modelos.Planillas.RevisionMantenimientoTecnico;
using MudBlazor.Extensions;

namespace Intranet.Pages
{
    public partial class MantenimientoTecnico 
    {
        [Inject]
        private IServicioAgendaTelefonica ServicioAgendaTelefonica { get; set; }
        [Inject]
        private IServicioPlanillaDigital ServicioPlanillaDigital { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        private bool mostrarModalEliminar = false;
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private string RegistroEliminar = string.Empty;
        List<MaterialRevision> CreateRegistro = new List<MaterialRevision>();
        DataPlanilla EditarAgenda = new DataPlanilla();
        private List<string> ListUnidad = new List<string>();
        private List<string> ListUbicacion = new List<string>();
        private List<string> ListNombreUsuario = new List<string>();
        private List<string> ListNroTelefono = new List<string>();
        public IQueryable<PlanillaDigitalDataGrid> MaestroDireccionTelefonica { get; set; } = null;
        public IQueryable<PlanillaDigitalDataGrid> DireccionTelefonica { get; set; } = null;
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
        private Guid AreaInformeId { get; set; }
        private string zonaRevision { get; set; }
        private string TipozonaSelecionada { get; set; }

        private List<InformeArea> ListAreaInforme = new List<InformeArea>();
        private bool AreaInformeSeleccionadaValid = true;
        private bool MostrarFormulario { get; set; }
        private bool MostrarFormularioAgrupado { get; set; }
        DataPlanilla configPantalla { get; set; }
        List<TipoZonaRevision> listaTipoZona { get; set; }

        private bool MostrarBotonAgregarYBuscador { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MostrarBotonAgregarYBuscador = true;
            //await RefrescarDataGrid();
            await obtenerUnidadAgenda();
            await obtenerUbicacionAgenda();
            await obtenerUsuarioAgenda();            
            //await crearJson("Habitacion", true);
            //await crearJson("Oficina", false);
            await obtenerListaAreaInforme();
            configPantalla = new DataPlanilla();
            listaTipoZona = new List<TipoZonaRevision>();
            MostrarFormulario = false;
            TipozonaSelecionada = string.Empty;
           
        }

        private async Task crearJson(string titulo, bool agrupados)
        {
            try
            {

                DataPlanilla data = new DataPlanilla();

                data.Titulo = titulo;
                data.AgruparCuerpos = agrupados;
                data.Cuerpo.Add(await CreacionData("Alineado", "radio",false, "Operativo", "radio", false, "Puerta", "Bisagra", "cerradura", titulo));
                data.Cuerpo.Add(await CreacionData("Alineado", "radio", false, "Operativo", "radio", true, "Puerta", "lavamanos", "piso", "Baño"));

                string json =  JsonSerializer.Serialize(data);

                ConfiguracionPantalla configuracion = new ConfiguracionPantalla();

                configuracion.InformeTituloId = Guid.Parse("D517AEEE-D38C-4C82-B4EA-C8FB6235F2D2");

                if(titulo.Equals("Oficina"))
                configuracion.InformeAreaId = Guid.Parse("2B315998-4E75-4D4B-BF6F-571B68E26EA9");

                if (titulo.Equals("Habitacion"))
                    configuracion.InformeAreaId = Guid.Parse("A2C446EA-C961-4623-9671-C30908319E28");

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

            if (NombreZona.Equals("Habitacion"))
            {
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "1" });
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "2" });
                tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "3" });
            }
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

        public async Task<List<PlanillaDigitalDataGrid>> Data()
        {

            var resultado = await ServicioPlanillaDigital.ObtenerListaRegistroPlanilla(AreaInformeId);

            return resultado.OrderByDescending(a => a.FechaCreacion).ToList();
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

        public async void Editar(MudBlazor.CellContext<PlanillaDigitalDataGrid> planillaDigital)
        {

            EditarAgenda = JsonSerializer.Deserialize<DataPlanilla>(planillaDigital.Item.Respuesta);
            var titulo = EditarAgenda.Titulo;
            //EditarAgenda.res = direccionTelefonica.Item.Id;
            //EditarAgenda.Usuario = direccionTelefonica.Item.Usuario;
            //EditarAgenda.Unidad = direccionTelefonica.Item.Unidad;
            //EditarAgenda.Ubicacion = direccionTelefonica.Item.Ubicacion;
            //EditarAgenda.Extension = direccionTelefonica.Item.Extension;
            //EditarAgenda.numeroTelefonico = direccionTelefonica.Item.numeroTelefonico;
            //EditarAgenda.UsuarioModificador = direccionTelefonica.Item.UsuarioModificador;
            //EditarAgenda.FechaModificacion = direccionTelefonica.Item.FechaModificacion != null ? direccionTelefonica.Item.FechaModificacion :
            //    direccionTelefonica.Item.FechaCreacion;

            mostrarModalEditar = true;

        }

        private void CerrarModalEditar()
        {          
            mostrarModalEditar = false;
            EditarAgenda = new DataPlanilla();
            StateHasChanged();
        }
        public void Eliminar (MudBlazor.CellContext<PlanillaDigitalDataGrid> planillaDigital)
        {

            string resultado = string.Empty;
            if (planillaDigital.Item.FechaCreacion.ToIsoDateString() != DateTime.Now.ToIsoDateString())
            {
                Snackbar.Add("No se puede eliminar el registro", Severity.Error);
                  return; 
            }
            var repuesta = JsonSerializer.Deserialize<DataPlanilla>(planillaDigital.Item.Respuesta);

            if (repuesta?.Cuerpo?.FirstOrDefault() is { } cuerpo &&
                cuerpo.zonaRevision?.FirstOrDefault() is { } zona &&
                zona.tipoZonaRevision?.FirstOrDefault() is { } tipo)
            {
                resultado = $"{zona.Nombre} {tipo.Nombre}";
            }

            IdELiminarAgenda = planillaDigital.Item.Id;
            RegistroEliminar = "creado por: " + planillaDigital.Item.UsuarioCreador + ", " +resultado + " Fecha de creación: "+planillaDigital.Item.FechaCreacion.ToIsoDateString();
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
        //void CheckForEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        //{
        //    if (e.Key == "Enter")
        //    {
        //        Buscar();
        //    }
        //}

        private async void Buscar2(ChangeEventArgs e)
        {
            // Aquí va tu código para buscar con el valor ingresado
        
            searchTerm = e.Value.ToString();
            if (string.IsNullOrEmpty(searchTerm))
            {
                DireccionTelefonica = MaestroDireccionTelefonica;

            }
            else
            {
                DireccionTelefonica = MaestroDireccionTelefonica.Where(p => p.UsuarioCreador.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.UsuarioRevision.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.InformeArea.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.InformeArea.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                ).OrderBy(p => p.FechaCreacion);

            }
        }


        //private async Task Buscar()
        //{
        //    if (string.IsNullOrEmpty(searchTerm))
        //    {
        //        DireccionTelefonica = MaestroDireccionTelefonica;

        //    }
        //    else
        //    {
        //        DireccionTelefonica = MaestroDireccionTelefonica.Where(p => p.Usuario.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
        //        p.Unidad.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.Ubicacion.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
        //        p.Extension.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
        //        ).OrderBy(p => p.Usuario);

        //    }
             
        //}

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
            //if (!string.IsNullOrEmpty(value))
            //    EditarAgenda.numeroTelefonico = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void OnUbicacionSeleccionadaEditarChanged(string value)
        {
            //if (!string.IsNullOrEmpty(value))
            //    EditarAgenda.Ubicacion = value;

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task GuardarAgenda(EditContext context)
        {
            try
            {
                DataPlanilla Respuesta = new DataPlanilla();
                ZonaRevision zonaRevisionData = new ZonaRevision();
                TipoZonaRevision tipoZonaRevisionData = new TipoZonaRevision();
                Cuerpo cuerpo = new Cuerpo();

                tipoZonaRevisionData.Nombre = TipozonaSelecionada;
                zonaRevisionData.Nombre = zonaRevision;
                zonaRevisionData.materialRevision = CreateRegistro;
                zonaRevisionData.tipoZonaRevision.Add(tipoZonaRevisionData);

                cuerpo.zonaRevision.Add(zonaRevisionData);
                Respuesta.Titulo = AreaInforme;
                Respuesta.Cuerpo.Add(cuerpo);

                string json = JsonSerializer.Serialize(Respuesta);

                //guardar registro
                PlanillaDigitalRegistro planillaDigitalRegistro = new PlanillaDigitalRegistro();
                planillaDigitalRegistro.Id = Guid.NewGuid();
                planillaDigitalRegistro.UsuarioCreador = Guid.Parse(await IdUsuario());
                planillaDigitalRegistro.UsuarioRevision = null;
                planillaDigitalRegistro.Respuesta = json;
                planillaDigitalRegistro.FechaCreacion = DateTime.Now;
                planillaDigitalRegistro.InformeAreaId = ListAreaInforme.Where(x => x.Nombre == AreaInforme).Select(u => u.Id).FirstOrDefault();
                planillaDigitalRegistro.InformeTituloId = Guid.Parse(configuration["GuidRevisionMantenimientoTecnico"]);

                //guardar registro

                intranetContext.planillaDigitalRegistro.Add(planillaDigitalRegistro);
                intranetContext.SaveChanges();

                await RefrescarDataGrid();
                CerrarModalNuevo();

            } catch (Exception ex) 
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }


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
            //EditarAgenda.Usuario = EditarAgenda.Usuario.Split('-')[0].Trim();
            //switch (await ServicioAgendaTelefonica.ConsultarAntesActualizarAgendaTelefonica(EditarAgenda))
            //{
            //    case 1:
            //        Snackbar.Add("El usuario ya se encuentra registrado", Severity.Error);
            //        return;
            //    case 2:
            //        Snackbar.Add("El número de extension ya se encuentra registrado", Severity.Error);
            //        return;
            //}
            //EditarAgenda.UsuarioModificador = await IdUsuario();

            //if (await ServicioAgendaTelefonica.ActualizarAgendaTelefonica(EditarAgenda))
            //{
            //    await RefrescarDataGrid(); 
            //    Snackbar.Add("Registro exitoso", Severity.Info);
            //    CerrarModalEditar();
            //}
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);

        }

        private async Task<string> IdUsuario() {
            return ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private async Task OnAreaInformeSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                zonaRevision = string.Empty;
                TipozonaSelecionada = string.Empty;
                AreaInforme = value;
 
                var area = intranetContext.informeArea.Where(x => x.Nombre == value && x.InformeTituloId == Guid.Parse(configuration["GuidRevisionMantenimientoTecnico"])).FirstOrDefault();

                AreaInformeId = area.Id;

                if (area != null) {
                    var confi = intranetContext.configuracionPantalla.Where(x => x.InformeAreaId == area.Id).FirstOrDefault();

                    if (confi != null)
                        configPantalla = JsonSerializer.Deserialize<DataPlanilla>(confi.ConfigPantalla);
                }

                if (configPantalla.AgruparCuerpos) 
                {
                    zonaRevision = configPantalla.Cuerpo[0].zonaRevision.Select(x => x.Nombre).FirstOrDefault();
                    var tipoZonaRevisions = configPantalla.Cuerpo[0].zonaRevision.Select(x => x.tipoZonaRevision).ToList();
                    listaTipoZona = tipoZonaRevisions[0];
                }
                MostrarBotonAgregarYBuscador = false;
                await RefrescarDataGrid();
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
                TipozonaSelecionada = value;
                MostrarFormulario = true;
            }
            else
            {
                MostrarFormulario = false;
            }

        }

        private void OnTipoZonaAgrupadoSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                TipozonaSelecionada = value;
                MostrarFormularioAgrupado = true;
            }
            else
            {
                MostrarFormularioAgrupado = false;
            }

        }
        private async Task obtenerListaAreaInforme()
        {

            try {
                var guid = Guid.Parse(configuration["GuidRevisionMantenimientoTecnico"]);

                ListAreaInforme = intranetContext.informeArea.Where(x=> x.InformeTituloId == guid).ToList();

                ListAreaInforme.OrderBy(x => x.Nombre).ToList();

            }
            catch (Exception ex)
            { Log.Error(ex.Message + ex.StackTrace + ex.InnerException); }    
            
        }

    }

}
