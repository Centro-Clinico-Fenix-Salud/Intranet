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
        
        List<MaterialRevision> CreateRegistro = new List<MaterialRevision>();
        public List<ZonaRevision> ListaCreateRegistro = new List<ZonaRevision>();
        DataPlanilla EditarAgenda = new DataPlanilla();
        private List<string> ListUnidad = new List<string>();
        private List<string> ListUbicacion = new List<string>();
        private List<string> ListNombreUsuario = new List<string>();
        private List<string> ListNroTelefono = new List<string>();
        private EliminarPlanillaDigital eliminarPlanillaDigital = new EliminarPlanillaDigital();   
        public IQueryable<PlanillaDigitalDataGrid> MaestroDireccionTelefonica { get; set; } = null;
        public IQueryable<PlanillaDigitalDataGrid> DireccionTelefonica { get; set; } = null;
        //quitar 
        private bool resetValueOnEmptyText;
        private bool coerceText;
        private bool coerceValue;
        bool success;
        private bool UbicacionSeleccionadaValid = true;
        private EditContext editContext;
        
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
        int cantidadPagina { get; set; }
        int numeroPagina { get; set; }
        
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
            //await crearJson("Consultorio área APS", false);
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
                List<Condicion> condicionAlineadoOperativo = new List<Condicion>();
                condicionAlineadoOperativo.Add(new Condicion { Nombre = "Alineado", Deshabilitado = false, Tipo = "radio" });
                condicionAlineadoOperativo.Add(new Condicion { Nombre = "Operativo", Deshabilitado = false, Tipo = "radio" });

                List<Condicion> condicionOperativo = new List<Condicion>();
                condicionOperativo.Add(new Condicion { Nombre = "Operativo", Deshabilitado = false, Tipo = "radio" });

                MaterialRevision materialPuertaPrincipal = new MaterialRevision();
                materialPuertaPrincipal.Nombre = "Puerta principal";

                MaterialRevision materialBisagras = new MaterialRevision();
                materialBisagras.Nombre = "Bisagras";

                MaterialRevision materialCerradura = new MaterialRevision();
                materialCerradura.Nombre = "Cerradura";

                MaterialRevision materialCamilla = new MaterialRevision();
                materialCamilla.Nombre = "Camilla";

                MaterialRevision materialMesadecomputadora = new MaterialRevision();
                materialMesadecomputadora.Nombre = "Mesa de computadora";

                MaterialRevision materialSilla = new MaterialRevision();
                materialSilla.Nombre = "Silla";

                MaterialRevision materialInterruptordeencendido = new MaterialRevision();
                materialInterruptordeencendido.Nombre = "Interruptor de encendido";

                MaterialRevision materialLamparas = new MaterialRevision();
                materialLamparas.Nombre = "Lámparas";

                MaterialRevision materialTomacorrientes = new MaterialRevision();
                materialTomacorrientes.Nombre = "Tomacorrientes";

                MaterialRevision materialTapasdetomacorrientes = new MaterialRevision();
                materialTapasdetomacorrientes.Nombre = "Tapas de tomacorrientes";

                MaterialRevision materialEquipodeaireacondicionado = new MaterialRevision();
                materialEquipodeaireacondicionado.Nombre = "Equipo de aire acondicionado";

                MaterialRevision materialEscabel = new MaterialRevision();
                materialEscabel.Nombre = "Escabel";

                MaterialRevision materialVentanas = new MaterialRevision();
                materialVentanas.Nombre = "Ventanas";

                MaterialRevision materialLavamanos = new MaterialRevision();
                materialLavamanos.Nombre = "Lavamanos";

                MaterialRevision materialParedespintura = new MaterialRevision();
                materialParedespintura.Nombre = "Paredes (pintura)";

                MaterialRevision materialTechopintura = new MaterialRevision();
                materialTechopintura.Nombre = "Techo (pintura)";

                MaterialRevision materialPuertadebaño = new MaterialRevision();
                materialPuertadebaño.Nombre = "Puerta de baño";

                MaterialRevision materialLlavedelavamanos = new MaterialRevision();
                materialLlavedelavamanos.Nombre = "Llave de lavamanos ";

                MaterialRevision materialCanilladelavamanos = new MaterialRevision();
                materialCanilladelavamanos.Nombre = "Canilla de lavamanos";

                MaterialRevision materialLlavedearresto = new MaterialRevision();
                materialLlavedearresto.Nombre = "Llave de arresto";

                MaterialRevision materialPoceta = new MaterialRevision();
                materialPoceta.Nombre = "Poceta";

                MaterialRevision materialUrinario = new MaterialRevision();
                materialUrinario.Nombre = "Urinario";

                MaterialRevision materialDivisores = new MaterialRevision();
                materialDivisores.Nombre = "Divisores";

                MaterialRevision materialPasadoresdepuertas = new MaterialRevision();
                materialPasadoresdepuertas.Nombre = "Pasadores de puertas";

                MaterialRevision materialCerraduraBano = new MaterialRevision();
                materialCerraduraBano.Nombre = "Cerradura";

                //MaterialRevision material = new MaterialRevision();
                //material.Nombre = "dfdfdf";

                //MaterialRevision material = new MaterialRevision();
                //materialPuertaPrincipal.Nombre = "Puerta";


                DataPlanilla data = new DataPlanilla();

                data.Titulo = titulo;
                data.AgruparCuerpos = agrupados;
                              
                ConfiguracionPantalla configuracion = new ConfiguracionPantalla();

                configuracion.InformeTituloId = Guid.Parse("D517AEEE-D38C-4C82-B4EA-C8FB6235F2D2");

                if (titulo.Equals("Oficina")) 
                {
                    //data.Cuerpo.Add(await CreacionData("Alineado", "radio", false, "Operativo", "radio", false, "Puerta", "Bisagra", "cerradura", titulo));
                    //data.Cuerpo.Add(await CreacionData("Alineado", "radio", false, "Operativo", "radio", false, "Puerta", "Bisagra", "cerradura", titulo));
                    configuracion.InformeAreaId = Guid.Parse("2B315998-4E75-4D4B-BF6F-571B68E26EA9");
                }
                if (titulo.Equals("Habitación"))
                {
                    //data.Cuerpo.Add(await CreacionData("Alineado", "radio", false, "Operativo", "radio", false, "Puerta", "Bisagra", "cerradura", titulo));
                    //data.Cuerpo.Add(await CreacionData("Alineado", "radio", false, "Operativo", "radio", true, "Puerta", "lavamanos", "piso", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("A2C446EA-C961-4623-9671-C30908319E28");
                }
                if (titulo.Equals("Quirófanos"))
                {
                    configuracion.InformeAreaId = Guid.Parse("2FB4601A-348C-4EBB-BEE1-0C3BC2494090");
                }
                if (titulo.Equals("Emergencia (Planta Baja)"))
                {
                    configuracion.InformeAreaId = Guid.Parse("D9486900-9BEA-42F8-980C-223F832C6F40");
                }
                if (titulo.Equals("Suite"))
                {
                    configuracion.InformeAreaId = Guid.Parse("98BCC273-C7A3-49F0-872D-A59B0593E051");
                }
                if (titulo.Equals("Ambulatorio Piso 5"))
                {
                    configuracion.InformeAreaId = Guid.Parse("61A50897-126E-4E0D-8DAB-C7279F931167");
                }
                if (titulo.Equals("Consultorio área APS"))
                {                   
                    List<MaterialRevision> ListaMaterialesConsultorioAreaAPS = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoConsultorioAreaAPS = new List<MaterialRevision>();

                    //consultorio 
                    materialPuertaPrincipal.Propiedad = condicionAlineadoOperativo;
                    materialBisagras.Propiedad = condicionAlineadoOperativo;
                    materialCerradura.Propiedad = condicionOperativo;
                    materialCamilla.Propiedad = condicionOperativo;
                    materialMesadecomputadora.Propiedad = condicionOperativo;
                    materialSilla.Propiedad= condicionOperativo;
                    materialInterruptordeencendido.Propiedad = condicionOperativo;
                    materialLamparas.Propiedad = condicionOperativo;
                    materialTomacorrientes.Propiedad = condicionOperativo;
                    materialEquipodeaireacondicionado.Propiedad = condicionOperativo;
                    materialEscabel.Propiedad = condicionOperativo;
                    materialVentanas.Propiedad = condicionOperativo;
                    materialLavamanos.Propiedad = condicionOperativo;
                    materialParedespintura.Propiedad = condicionOperativo;
                    materialTechopintura.Propiedad = condicionOperativo;

                    //bano consultorio 
                    materialPuertadebaño.Propiedad = condicionAlineadoOperativo;
                    materialCerraduraBano.Propiedad = condicionAlineadoOperativo;
                    materialLlavedelavamanos.Propiedad = condicionOperativo;
                    materialCanilladelavamanos.Propiedad = condicionOperativo;
                    materialLlavedearresto.Propiedad = condicionOperativo;
                    materialPoceta.Propiedad= condicionOperativo;
                    materialUrinario.Propiedad = condicionOperativo;
                    materialDivisores.Propiedad = condicionAlineadoOperativo;
                    materialPasadoresdepuertas.Propiedad = condicionOperativo;


                    ListaMaterialesConsultorioAreaAPS.Add(materialPuertaPrincipal);
                    ListaMaterialesConsultorioAreaAPS.Add(materialBisagras);
                    ListaMaterialesConsultorioAreaAPS.Add(materialCerradura);
                    ListaMaterialesConsultorioAreaAPS.Add(materialCamilla);
                    ListaMaterialesConsultorioAreaAPS.Add(materialMesadecomputadora);
                    ListaMaterialesConsultorioAreaAPS.Add(materialSilla);
                    ListaMaterialesConsultorioAreaAPS.Add(materialInterruptordeencendido);
                    ListaMaterialesConsultorioAreaAPS.Add(materialLamparas);
                    ListaMaterialesConsultorioAreaAPS.Add(materialEquipodeaireacondicionado);
                    ListaMaterialesConsultorioAreaAPS.Add(materialEscabel);
                    ListaMaterialesConsultorioAreaAPS.Add(materialVentanas);
                    ListaMaterialesConsultorioAreaAPS.Add(materialLavamanos);
                    ListaMaterialesConsultorioAreaAPS.Add(materialParedespintura);
                    ListaMaterialesConsultorioAreaAPS.Add(materialTechopintura);
                    ListaMaterialesConsultorioAreaAPS.Add(materialTomacorrientes);

                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialPuertadebaño);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialBisagras);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialCerradura);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialLavamanos);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialLlavedearresto);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialPoceta);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialUrinario);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialDivisores);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialPasadoresdepuertas);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialLamparas);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialParedespintura);
                    ListaMaterialesBanoConsultorioAreaAPS.Add(materialTechopintura);


                    data.Cuerpo.Add(await CreacionData(ListaMaterialesConsultorioAreaAPS, "Consultorio", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoConsultorioAreaAPS, "Consultorio", "Baño"));
                    configuracion.InformeAreaId = Guid.Parse("89956A5E-C2AB-4CEC-AC4A-D332B426867E");
                }
                if (titulo.Equals("Ambulatorio Piso 4"))
                {
                    configuracion.InformeAreaId = Guid.Parse("753F5B4B-8286-48DC-9DBD-E8EFBC106915");
                }

                string json = JsonSerializer.Serialize(data);
                configuracion.ConfigPantalla = json;

                intranetContext.configuracionPantalla.Add(configuracion);
                await intranetContext.SaveChangesAsync();

                //DataPlanilla data2 = JsonSerializer.Deserialize<DataPlanilla>(json);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
        }

        private async Task<Cuerpo> CreacionData(List<MaterialRevision> ListaMateriales, string NombreZona, string? BanoDeZona)
        {
            Cuerpo cuerpo1 = new Cuerpo();
            //MaterialRevision material1 = new MaterialRevision();
            //MaterialRevision material2 = new MaterialRevision();
            //MaterialRevision material3 = new MaterialRevision();
            ZonaRevision zonaRevision1 = new ZonaRevision();
            List<TipoZonaRevision> tipoZonaRevision = new List<TipoZonaRevision>(); 
            //Condicion condicion1 = new Condicion();
            //Condicion condicion2 = new Condicion();

            //condicion1.Nombre = NombreCondicion1;
            //condicion1.Deshabilitado = EstatusCondicion1;
            //condicion1.Tipo = TipoCondicion1;
            //condicion2.Nombre = NombreCondicion2;
            //condicion2.Deshabilitado = EstatusCondicion2;
            //condicion2.Tipo = TipoCondicion2;

            //material1.Nombre = NombreMaterial;
            //material1.Propiedad.Add(condicion1);
            //material1.Propiedad.Add(condicion2);

            //material2.Nombre = NombreMaterial2;
            //material2.Propiedad.Add(condicion1);
            //material2.Propiedad.Add(condicion2);

            //material3.Nombre = NombreMaterial3;
            //material3.Propiedad.Add(condicion1);
            //material3.Propiedad.Add(condicion2);

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
            if (NombreZona.Equals("Consultorio"))
            {
                if (string.IsNullOrEmpty(BanoDeZona))
                {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "1" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "2" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "3" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "4" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "5" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "6" });
                }
                else {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Damas " });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Caballeros" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Medicos en APS" });
                }
               
            }

            zonaRevision1.Nombre = string.IsNullOrEmpty(BanoDeZona) ? NombreZona: BanoDeZona;
            zonaRevision1.tipoZonaRevision = tipoZonaRevision;

            foreach (var material in ListaMateriales) 
            {
                zonaRevision1.materialRevision.Add(material);
            }

            //zonaRevision1.materialRevision.Add(material1);
            //zonaRevision1.materialRevision.Add(material2);
            //zonaRevision1.materialRevision.Add(material3);
            //zonaRevision1.materialRevision.Add(material1);
            //zonaRevision1.materialRevision.Add(material2);
            //zonaRevision1.materialRevision.Add(material3);
            //zonaRevision1.materialRevision.Add(material1);
            //zonaRevision1.materialRevision.Add(material2);
            //zonaRevision1.materialRevision.Add(material3);

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
            EditarAgenda.UsuarioCreador = planillaDigital.Item.UsuarioCreador;
            EditarAgenda.FechaCreacion = planillaDigital.Item.FechaCreacion.ToString();

            mostrarModalEditar = true;

        }

        private void CerrarModalEditar()
        {          
            mostrarModalEditar = false;
            EditarAgenda = new DataPlanilla();
            StateHasChanged();
        }
        public async void Eliminar (MudBlazor.CellContext<PlanillaDigitalDataGrid> planillaDigital)
        {

     
            if (planillaDigital.Item.FechaCreacion.ToIsoDateString() != DateTime.Now.ToIsoDateString())
            {
                Snackbar.Add("Solo se puede eliminar registro el dia de la creación", Severity.Error);
                  return; 
            }

            if (!await ServicioPlanillaDigital.ConfirmarEliminarRegistroPorUsuario(planillaDigital.Item.Id, Guid.Parse( await IdUsuario())))
            {
                Snackbar.Add("Solo puede eliminar el registro el usuario creador", Severity.Error);
                return;
            }


            var repuesta = JsonSerializer.Deserialize<DataPlanilla>(planillaDigital.Item.Respuesta);

            if (repuesta?.Cuerpo?.FirstOrDefault() is { } cuerpo &&
                cuerpo.zonaRevision?.FirstOrDefault() is { } zona &&
                zona.tipoZonaRevision?.FirstOrDefault() is { } tipo)
            {
                eliminarPlanillaDigital.ZonaTipoZona = $"{zona.Nombre} {tipo.Nombre}";
            }

            eliminarPlanillaDigital.Id = planillaDigital.Item.Id;
            eliminarPlanillaDigital.UsuarioCreador = planillaDigital.Item.UsuarioCreador;
            eliminarPlanillaDigital.FechaCreacion = planillaDigital.Item.FechaCreacion.ToString();
            

             mostrarModalEliminar = true;
        }

        public void Nuevo()
        {
            CreateRegistro = new List<MaterialRevision>();
            mostrarModalNuevo = true;
            if (configPantalla != null)
            if (configPantalla.Cuerpo.Count > 1)
            {
                    numeroPagina = 0;
                    MostrarFormulario = false;
            }
            else
            { MostrarFormulario = true; }                
            
        }

        private void CerrarModalEliminar()
        {
            eliminarPlanillaDigital = new EliminarPlanillaDigital();
            StateHasChanged();
            mostrarModalEliminar = false;

        }
        private void CerrarModalNuevo()
        {
                      
            mostrarModalNuevo = false;
            TipozonaSelecionada = string.Empty;
            zonaRevision = string.Empty;
            CreateRegistro = new List<MaterialRevision>();
            ListaCreateRegistro = new List<ZonaRevision>();

        }
       
        private async Task EliminarRegistro()
        {

            if (await ServicioPlanillaDigital.EliminarRegistroPlanillaDigital(eliminarPlanillaDigital.Id, Guid.Parse(await IdUsuario())))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro Eliminado", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }


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

                if (!configPantalla.AgruparCuerpos)
                {
                    tipoZonaRevisionData.Nombre = TipozonaSelecionada;
                    zonaRevisionData.Nombre = zonaRevision;
                    zonaRevisionData.materialRevision = CreateRegistro;
                    zonaRevisionData.tipoZonaRevision.Add(tipoZonaRevisionData);

                    cuerpo.zonaRevision.Add(zonaRevisionData);
                    Respuesta.Titulo = AreaInforme;
                    Respuesta.Cuerpo.Add(cuerpo);
                }
                else 
                {
                    TipoZonaRevision tipoZonaRevisionSeleccionada = new TipoZonaRevision { Nombre = TipozonaSelecionada };
                    List<TipoZonaRevision> ListaTipoZonaRevisions = new List<TipoZonaRevision>();
                    ListaTipoZonaRevisions.Add(tipoZonaRevisionSeleccionada);
                    ListaCreateRegistro.Add(new ZonaRevision { materialRevision = CreateRegistro, Nombre = zonaRevision, tipoZonaRevision = ListaTipoZonaRevisions });

                    foreach (var registro in ListaCreateRegistro) 
                    {
                        tipoZonaRevisionData = new TipoZonaRevision();
                        zonaRevisionData = new ZonaRevision();
                        cuerpo = new Cuerpo();

                        tipoZonaRevisionData.Nombre = registro.tipoZonaRevision.Count() > 0? registro.tipoZonaRevision[0].Nombre: string.Empty ;
                        zonaRevisionData.Nombre = registro.Nombre;
                        zonaRevisionData.materialRevision = registro.materialRevision;
                        zonaRevisionData.tipoZonaRevision.Add(tipoZonaRevisionData);

                        cuerpo.zonaRevision.Add(zonaRevisionData);
                        Respuesta.Cuerpo.Add(cuerpo);

                    }
                    Respuesta.Titulo = AreaInforme;
                    Respuesta.AgruparCuerpos = true;


                }

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
                    cantidadPagina = configPantalla.Cuerpo.Count() - 1;
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

                foreach (var materia in zonaIndividual.materialRevision) 
                {
                    foreach (var propiedades in materia.Propiedad)
                    {
                        propiedades.Valor = string.Empty;
                }
                }

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

                List<ZonaRevision> listaZona = new List<ZonaRevision>();
               // listaTipoZona = new List<TipoZonaRevision>();
                var listaCuerpo = configPantalla.Cuerpo.ToList();

                foreach (var zona in listaCuerpo)
                {
                    var CuerpoIndividual = zona;

                    if (CuerpoIndividual.zonaRevision.Any(x => x.Nombre == zonaRevision))
                    {
                        listaZona = CuerpoIndividual.zonaRevision.Where(x => x.Nombre == zonaRevision).ToList();
                    }
                }

                var zonaIndividual = listaZona.Where(x => x.Nombre == zonaRevision).FirstOrDefault();

                CreateRegistro = zonaIndividual.materialRevision;
            }
            else
            {
                MostrarFormularioAgrupado = false;
            }

        }

        private async Task BtnSiguiente()
        {
            try
            {
                TipoZonaRevision tipoZonaRevisionSeleccionada = new TipoZonaRevision{ Nombre = TipozonaSelecionada};
                List<TipoZonaRevision> ListaTipoZonaRevisions = new List<TipoZonaRevision>();
                ListaTipoZonaRevisions.Add(tipoZonaRevisionSeleccionada);
                ListaCreateRegistro.Add(new ZonaRevision { materialRevision = CreateRegistro, Nombre = zonaRevision, tipoZonaRevision = ListaTipoZonaRevisions });

                numeroPagina++;

                zonaRevision = configPantalla.Cuerpo[numeroPagina].zonaRevision.Select(x => x.Nombre).FirstOrDefault();
                var tipoZonaRevisions = configPantalla.Cuerpo[numeroPagina].zonaRevision.Select(x => x.tipoZonaRevision).ToList();
                listaTipoZona = tipoZonaRevisions[0];
                TipozonaSelecionada = string.Empty;

                List<ZonaRevision> listaZona = new List<ZonaRevision>();
                
                var listaCuerpo = configPantalla.Cuerpo.ToList();

                foreach (var zona in listaCuerpo)
                {
                    var CuerpoIndividual = zona;

                    if (CuerpoIndividual.zonaRevision.Any(x => x.Nombre == zonaRevision))
                    {
                        listaZona = CuerpoIndividual.zonaRevision.Where(x => x.Nombre == zonaRevision).ToList();
                    }
                }

                var zonaIndividual = listaZona.Where(x => x.Nombre == zonaRevision).FirstOrDefault();

                if (zonaIndividual.tipoZonaRevision != null)
                    listaTipoZona = zonaIndividual.tipoZonaRevision.ToList();
                else
                    listaTipoZona = new List<TipoZonaRevision>();
                
                 CreateRegistro = zonaIndividual.materialRevision;


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
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
