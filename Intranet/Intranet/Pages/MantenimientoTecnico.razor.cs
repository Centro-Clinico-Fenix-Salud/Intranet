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
using System.Numerics;
using Intranet.Modelos.Noticia;

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
        private bool mostrarModalFiltro = false;
        private bool mostrarModalNuevo = false;
        private bool mostrarModalConsulta = false;
        private bool mostrarModalFotos = false;

        List<MaterialRevision> CreateRegistro = new List<MaterialRevision>();
        public List<ZonaRevision> ListaCreateRegistro = new List<ZonaRevision>();
        DataPlanilla EditarAgenda = new DataPlanilla();
        private List<string> ListUnidad = new List<string>();
        private List<string> ListUbicacion = new List<string>();
        //private List<string> ListNombreUsuario = new List<string>();
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
        private string ZonaFiltro { get; set; }
        private string TipoZonaFiltro { get; set; }
        private string MaterialFiltro { get; set; }
        private string PropiedadFiltro { get; set; }
        private string EstatusdFiltro { get; set; }
        private Guid AreaInformeId { get; set; }
        private string zonaRevision { get; set; }
        private string TipozonaSelecionada { get; set; }
        private DateTime? FechaDesde = DateTime.Today;
        private DateTime? FechaHasta = DateTime.Today;

        private List<InformeArea> ListAreaInforme = new List<InformeArea>();
        private List<string> ListaZonaFiltro = new List<string>();
        private List<string> ListaTipoZonaFiltro = new List<string>();
        private bool AreaInformeSeleccionadaValid = true;
        private bool MostrarFormulario { get; set; }
        private bool MostrarFormularioAgrupado { get; set; }
        DataPlanilla configPantalla { get; set; }
        List<TipoZonaRevision> listaTipoZona { get; set; }
        int cantidadPagina { get; set; }
        int numeroPagina { get; set; }
        Guid IdRegistroSeleccionado { get; set; }
        private bool arrows = true;
        private bool bullets = true;
        private bool enableSwipeGesture = true;
        private bool autocycle = true;
        private Transition transition = Transition.Slide;
        private List<DataImagen> ImagenModalFotos;
        private List<ListaImagenCargada> listaImagenCargada = new List<ListaImagenCargada>();
        private bool MostrarBotonAgregarYBuscador { get; set; }
        [Inject]
        private IServicioNoticias servicioNoticias { get; set; }
        private string ruta = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            MostrarBotonAgregarYBuscador = true;
            //await RefrescarDataGrid();
            await obtenerUnidadAgenda();
            await obtenerUbicacionAgenda();
            //await obtenerUsuarioAgenda();
            //await crearJson("Habitación", true);
            //await crearJson("Oficina", false);
            //await crearJson("Consultorio área APS", false);
            //await crearJson("Quirófanos", false);
            //await crearJson("Emergencia (Planta Baja)", false);
            //await crearJson("Ambulatorio Piso 5", false);
            //await crearJson("Ambulatorio Piso 4", false);
            //await crearJson("Suite", true);
            await obtenerListaAreaInforme();
            configPantalla = new DataPlanilla();
            listaTipoZona = new List<TipoZonaRevision>();
            MostrarFormulario = false;
            TipozonaSelecionada = string.Empty;
            ruta = configuration["RutaArchivosMantenimientoTecnico"];

        }

        private async Task crearJson(string titulo, bool agrupados)
        {
            try
            {
                List<Condicion> condicionAlineadoOperativo = new List<Condicion>();
                condicionAlineadoOperativo.Add(new Condicion { Nombre = "Alineado", Deshabilitado = false, Tipo = "Radio" });
                condicionAlineadoOperativo.Add(new Condicion { Nombre = "Operativo", Deshabilitado = false, Tipo = "Radio" });

                List<Condicion> condicionOperativo = new List<Condicion>();
                condicionOperativo.Add(new Condicion { Nombre = "Operativo", Deshabilitado = false, Tipo = "Radio" });

                List<Condicion> Observaciones = new List<Condicion>();
                Observaciones.Add(new Condicion { Nombre = "Observaciones (Opcional)", Deshabilitado = false, Tipo = "Texto" });

                MaterialRevision materialPuertaPrincipal = new MaterialRevision();
                materialPuertaPrincipal.Nombre = "Puerta principal";
                materialPuertaPrincipal.Tipo = "Radio";

                MaterialRevision materialBisagras = new MaterialRevision();
                materialBisagras.Nombre = "Bisagras";
                materialBisagras.Tipo = "Radio";

                MaterialRevision materialCerradura = new MaterialRevision();
                materialCerradura.Nombre = "Cerradura";
                materialCerradura.Tipo = "Radio";

                MaterialRevision materialCamilla = new MaterialRevision();
                materialCamilla.Nombre = "Camilla";
                materialCamilla.Tipo = "Radio";

                MaterialRevision materialMesadecomputadora = new MaterialRevision();
                materialMesadecomputadora.Nombre = "Mesa de computadora";
                materialMesadecomputadora.Tipo = "Radio";

                MaterialRevision materialSilla = new MaterialRevision();
                materialSilla.Nombre = "Silla";
                materialSilla.Tipo = "Radio";

                MaterialRevision materialInterruptordeencendido = new MaterialRevision();
                materialInterruptordeencendido.Nombre = "Interruptor de encendido";
                materialInterruptordeencendido.Tipo = "Radio";

                MaterialRevision materialLamparas = new MaterialRevision();
                materialLamparas.Nombre = "Lámparas";
                materialLamparas.Tipo = "Radio";

                MaterialRevision materialTomacorrientes = new MaterialRevision();
                materialTomacorrientes.Nombre = "Tomacorrientes";
                materialTomacorrientes.Tipo = "Radio";

                MaterialRevision materialTapasdetomacorrientes = new MaterialRevision();
                materialTapasdetomacorrientes.Nombre = "Tapas de tomacorrientes";
                materialTapasdetomacorrientes.Tipo = "Radio";

                MaterialRevision materialEquipodeaireacondicionado = new MaterialRevision();
                materialEquipodeaireacondicionado.Nombre = "Equipo de aire acondicionado";
                materialEquipodeaireacondicionado.Tipo = "Radio";

                MaterialRevision materialEscabel = new MaterialRevision();
                materialEscabel.Nombre = "Escabel";
                materialEscabel.Tipo = "Radio";

                MaterialRevision materialVentanas = new MaterialRevision();
                materialVentanas.Nombre = "Ventanas";
                materialVentanas.Tipo = "Radio";

                MaterialRevision materialLavamanos = new MaterialRevision();
                materialLavamanos.Nombre = "Lavamanos";
                materialLavamanos.Tipo = "Radio";

                MaterialRevision materialParedespintura = new MaterialRevision();
                materialParedespintura.Nombre = "Paredes (pintura)";
                materialParedespintura.Tipo = "Radio";

                MaterialRevision materialTechopintura = new MaterialRevision();
                materialTechopintura.Nombre = "Techo (pintura)";
                materialTechopintura.Tipo = "Radio";

                MaterialRevision materialPuertadebaño = new MaterialRevision();
                materialPuertadebaño.Nombre = "Puerta de baño";
                materialPuertadebaño.Tipo = "Radio";

                MaterialRevision materialLlavedelavamanos = new MaterialRevision();
                materialLlavedelavamanos.Nombre = "Llave de lavamanos";
                materialLlavedelavamanos.Tipo = "Radio";

                MaterialRevision materialCanilladelavamanos = new MaterialRevision();
                materialCanilladelavamanos.Nombre = "Canilla de lavamanos";
                materialCanilladelavamanos.Tipo = "Radio";

                MaterialRevision materialLlavedearresto = new MaterialRevision();
                materialLlavedearresto.Nombre = "Llave de arresto";
                materialLlavedearresto.Tipo = "Radio";

                MaterialRevision materialPoceta = new MaterialRevision();
                materialPoceta.Nombre = "Poceta";
                materialPoceta.Tipo = "Radio";

                MaterialRevision materialUrinario = new MaterialRevision();
                materialUrinario.Nombre = "Urinario";
                materialUrinario.Tipo = "Radio";

                MaterialRevision materialDivisores = new MaterialRevision();
                materialDivisores.Nombre = "Divisores";
                materialDivisores.Tipo = "Radio";

                MaterialRevision materialPasadoresdepuertas = new MaterialRevision();
                materialPasadoresdepuertas.Nombre = "Pasadores de puertas";
                materialPasadoresdepuertas.Tipo = "Radio";

                MaterialRevision materialCerraduraBano = new MaterialRevision();
                materialCerraduraBano.Nombre = "Cerradura";
                materialCerraduraBano.Tipo = "Radio";

                MaterialRevision materialLamparacialítica = new MaterialRevision();
                materialLamparacialítica.Nombre = "Lámpara cialítica";
                materialLamparacialítica.Tipo = "Radio";

                MaterialRevision materialPuertasdevidriotemplex = new MaterialRevision();
                materialPuertasdevidriotemplex.Nombre = "Puertas de vidrio templex";
                materialPuertasdevidriotemplex.Tipo = "Radio";

                MaterialRevision materialCestadehacer = new MaterialRevision();
                materialCestadehacer.Nombre = "Cesta de hacer (lavado de manos)";
                materialCestadehacer.Tipo = "Radio";

                MaterialRevision materialCamaclinica = new MaterialRevision();
                materialCamaclinica.Nombre = "Cama clínica";
                materialCamaclinica.Tipo = "Radio";

                MaterialRevision materialEstructuradecortinas = new MaterialRevision();
                materialEstructuradecortinas.Nombre = "Estructura de cortinas";
                materialEstructuradecortinas.Tipo = "Radio";

                MaterialRevision materialSilladeacompanante = new MaterialRevision();
                materialSilladeacompanante.Nombre = "Silla de acompañante";
                materialSilladeacompanante.Tipo = "Radio";

                MaterialRevision materialMesa = new MaterialRevision();
                materialMesa.Nombre = "Mesa";
                materialMesa.Tipo = "Radio";

                MaterialRevision materialTomadeoxigeno = new MaterialRevision();
                materialTomadeoxigeno.Nombre = "Toma de oxigeno";
                materialTomadeoxigeno.Tipo = "Radio";

                MaterialRevision materialDuchatelefono = new MaterialRevision();
                materialDuchatelefono.Nombre = "Ducha teléfono";
                materialDuchatelefono.Tipo = "Radio";

                MaterialRevision materialLlavededucha = new MaterialRevision();
                materialLlavededucha.Nombre = "Llave de ducha";
                materialLlavedearresto.Tipo = "Radio";

                MaterialRevision materialDuchacorona = new MaterialRevision();
                materialDuchacorona.Nombre = "Ducha corona";
                materialDuchacorona.Tipo = "Radio";

                MaterialRevision materialPuertadeduchas = new MaterialRevision();
                materialPuertadeduchas.Nombre = "Puerta de duchas";
                materialPuertadeduchas.Tipo = "Radio";

                MaterialRevision materialGabinete = new MaterialRevision();
                materialGabinete.Nombre = "Gabinete";
                materialGabinete.Tipo = "Radio";

                MaterialRevision materialDivan = new MaterialRevision();
                materialDivan.Nombre = "Diván";
                materialDivan.Tipo = "Radio";

                MaterialRevision materialPuertasdecloset = new MaterialRevision();
                materialPuertasdecloset.Nombre = "Puertas de closet/pomos";
                materialPuertasdecloset.Tipo = "Radio";

                MaterialRevision materialCloset = new MaterialRevision();
                materialCloset.Nombre = "Clóset";
                materialCloset.Tipo = "Radio";

                MaterialRevision materialTelevisor = new MaterialRevision();
                materialTelevisor.Nombre = "Televisor";
                materialTelevisor.Tipo = "Radio";

                MaterialRevision materialParalhospitalario = new MaterialRevision();
                materialParalhospitalario.Nombre = "Paral hospitalario";
                materialParalhospitalario.Tipo = "Radio";

                MaterialRevision materialNeveraejecutiva = new MaterialRevision();
                materialNeveraejecutiva.Nombre = "Nevera ejecutiva";
                materialNeveraejecutiva.Tipo = "Radio";

                MaterialRevision materialExtractordetecho = new MaterialRevision();
                materialExtractordetecho.Nombre = "Extractor de techo";
                materialExtractordetecho.Tipo = "Radio";

                MaterialRevision materialEscritorio = new MaterialRevision();
                materialEscritorio.Nombre = "Escritorio";
                materialEscritorio.Tipo = "Radio";

                MaterialRevision materialCableadocanalizado = new MaterialRevision();
                materialCableadocanalizado.Nombre = "Cableado canalizado";
                materialCableadocanalizado.Tipo = "Radio";

                MaterialRevision materialRegletas = new MaterialRevision();
                materialRegletas.Nombre = "Regletas";
                materialRegletas.Tipo = "Radio";

                MaterialRevision materialPiso = new MaterialRevision();
                materialPiso.Nombre = "Piso";
                materialPiso.Tipo = "Radio";

                MaterialRevision materialArturitosgabinetes = new MaterialRevision();
                materialArturitosgabinetes.Nombre = "Arturitos/ gabinetes";
                materialArturitosgabinetes.Tipo = "Radio";

                MaterialRevision materialAireAcondicionado = new MaterialRevision();
                materialAireAcondicionado.Nombre = "Aire Acondicionado";
                materialAireAcondicionado.Tipo = "Radio";

                MaterialRevision materialObservaciones = new MaterialRevision();
                materialObservaciones.Nombre = "Observaciones (Opcional)";
                materialObservaciones.Tipo = "Texto";



                // General - consultorio
                materialPuertaPrincipal.Propiedad = condicionAlineadoOperativo;
                materialBisagras.Propiedad = condicionAlineadoOperativo;
                materialCerradura.Propiedad = condicionOperativo;
                materialCamilla.Propiedad = condicionOperativo;
                materialMesadecomputadora.Propiedad = condicionOperativo;
                materialSilla.Propiedad = condicionOperativo;
                materialInterruptordeencendido.Propiedad = condicionOperativo;
                materialLamparas.Propiedad = condicionOperativo;
                materialTomacorrientes.Propiedad = condicionOperativo;
                materialEquipodeaireacondicionado.Propiedad = condicionOperativo;
                materialEscabel.Propiedad = condicionOperativo;
                materialVentanas.Propiedad = condicionOperativo;
                materialLavamanos.Propiedad = condicionOperativo;
                materialParedespintura.Propiedad = condicionOperativo;
                materialTechopintura.Propiedad = condicionOperativo;
                materialTapasdetomacorrientes.Propiedad= condicionOperativo;
                materialObservaciones.Propiedad = Observaciones;

                //oficina

                materialEscritorio.Propiedad = condicionOperativo;
                materialCableadocanalizado.Propiedad = condicionOperativo;
                materialRegletas.Propiedad = condicionOperativo;
                materialPiso.Propiedad = condicionOperativo;
                materialArturitosgabinetes.Propiedad = condicionOperativo;
                materialAireAcondicionado.Propiedad = condicionOperativo;


                // quirofanos

                materialLamparacialítica.Propiedad = condicionOperativo;
                materialPuertasdevidriotemplex.Propiedad = condicionOperativo;
                materialCestadehacer.Propiedad = condicionOperativo;

                //Emergencia

                materialEstructuradecortinas.Propiedad = condicionAlineadoOperativo;
                materialCamaclinica.Propiedad = condicionAlineadoOperativo;
                materialSilladeacompanante.Propiedad = condicionOperativo;
                materialMesa.Propiedad = condicionOperativo;
                materialTomadeoxigeno.Propiedad = condicionOperativo;
                materialDuchatelefono.Propiedad = condicionOperativo;
                materialLlavededucha.Propiedad = condicionOperativo;
                materialDuchacorona.Propiedad = condicionOperativo;
                materialPuertadeduchas.Propiedad = condicionOperativo;


                //bano consultorio 
                materialPuertadebaño.Propiedad = condicionAlineadoOperativo;
                materialCerraduraBano.Propiedad = condicionAlineadoOperativo;
                materialLlavedelavamanos.Propiedad = condicionOperativo;
                materialCanilladelavamanos.Propiedad = condicionOperativo;
                materialLlavedearresto.Propiedad = condicionOperativo;
                materialPoceta.Propiedad = condicionOperativo;
                materialUrinario.Propiedad = condicionOperativo;
                materialDivisores.Propiedad = condicionAlineadoOperativo;
                materialPasadoresdepuertas.Propiedad = condicionOperativo;

                //Suite
                materialGabinete.Propiedad = condicionOperativo;
                materialDivan.Propiedad = condicionOperativo;
                materialPuertasdecloset.Propiedad = condicionAlineadoOperativo;
                materialCloset.Propiedad = condicionOperativo;
                materialTelevisor.Propiedad = condicionOperativo;
                materialParalhospitalario.Propiedad = condicionOperativo;
                materialNeveraejecutiva.Propiedad = condicionOperativo;
                materialExtractordetecho.Propiedad = condicionOperativo;


                DataPlanilla data = new DataPlanilla();

                data.Titulo = titulo;
                data.AgruparCuerpos = agrupados;
                              
                ConfiguracionPantalla configuracion = new ConfiguracionPantalla();

                configuracion.InformeTituloId = Guid.Parse("D517AEEE-D38C-4C82-B4EA-C8FB6235F2D2");

                if (titulo.Equals("Oficina")) 
                {
                    List<MaterialRevision> ListaMaterialesOficina = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoOficina = new List<MaterialRevision>();

                    ListaMaterialesOficina.Add(materialPuertaPrincipal);
                    ListaMaterialesOficina.Add(materialBisagras);
                    ListaMaterialesOficina.Add(materialCerraduraBano);
                    ListaMaterialesOficina.Add(materialEscritorio);
                    ListaMaterialesOficina.Add(materialSilla);
                    ListaMaterialesOficina.Add(materialInterruptordeencendido);
                    ListaMaterialesOficina.Add(materialLamparas);
                    ListaMaterialesOficina.Add(materialParedespintura);
                    ListaMaterialesOficina.Add(materialTechopintura);
                    ListaMaterialesOficina.Add(materialTomacorrientes);
                    ListaMaterialesOficina.Add(materialCableadocanalizado);
                    ListaMaterialesOficina.Add(materialRegletas);
                    ListaMaterialesOficina.Add(materialPiso);
                    ListaMaterialesOficina.Add(materialArturitosgabinetes);
                    ListaMaterialesOficina.Add(materialAireAcondicionado);
                    ListaMaterialesOficina.Add(materialObservaciones);

                    ListaMaterialesBanoOficina.Add(materialPuertadebaño);
                    ListaMaterialesBanoOficina.Add(materialBisagras);
                    ListaMaterialesBanoOficina.Add(materialCerraduraBano);
                    ListaMaterialesBanoOficina.Add(materialLavamanos);
                    ListaMaterialesBanoOficina.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoOficina.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoOficina.Add(materialLlavedearresto);
                    ListaMaterialesBanoOficina.Add(materialPoceta);
                    ListaMaterialesBanoOficina.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoOficina.Add(materialLamparas);
                    ListaMaterialesBanoOficina.Add(materialParedespintura);
                    ListaMaterialesBanoOficina.Add(materialTechopintura);
                    ListaMaterialesBanoOficina.Add(materialObservaciones);

                    data.Cuerpo.Add(await CreacionData(ListaMaterialesOficina, "Oficina", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoOficina, "Oficina", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("2B315998-4E75-4D4B-BF6F-571B68E26EA9");
                }
                if (titulo.Equals("Habitación"))
                {
                    List<MaterialRevision> ListaMaterialesHabitacion = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoHabitacion = new List<MaterialRevision>();

                    ListaMaterialesHabitacion.Add(materialPuertaPrincipal);
                    ListaMaterialesHabitacion.Add(materialBisagras);
                    ListaMaterialesHabitacion.Add(materialPuertasdecloset);
                    ListaMaterialesHabitacion.Add(materialCerradura);
                    ListaMaterialesHabitacion.Add(materialGabinete);
                    ListaMaterialesHabitacion.Add(materialCamaclinica);
                    ListaMaterialesHabitacion.Add(materialDivan);
                    ListaMaterialesHabitacion.Add(materialCloset);
                    ListaMaterialesHabitacion.Add(materialTelevisor);
                    ListaMaterialesHabitacion.Add(materialInterruptordeencendido);
                    ListaMaterialesHabitacion.Add(materialLamparas);
                    ListaMaterialesHabitacion.Add(materialTomacorrientes);
                    ListaMaterialesHabitacion.Add(materialTapasdetomacorrientes);
                    ListaMaterialesHabitacion.Add(materialEquipodeaireacondicionado);
                    ListaMaterialesHabitacion.Add(materialEscabel);
                    ListaMaterialesHabitacion.Add(materialVentanas);
                    ListaMaterialesHabitacion.Add(materialTomadeoxigeno);
                    ListaMaterialesHabitacion.Add(materialParalhospitalario);      
                    ListaMaterialesHabitacion.Add(materialParedespintura);
                    ListaMaterialesHabitacion.Add(materialTechopintura);
                    ListaMaterialesHabitacion.Add(materialExtractordetecho);

                    ListaMaterialesBanoHabitacion.Add(materialPuertadebaño);
                    ListaMaterialesBanoHabitacion.Add(materialBisagras);
                    ListaMaterialesBanoHabitacion.Add(materialCerradura);
                    ListaMaterialesBanoHabitacion.Add(materialLavamanos);
                    ListaMaterialesBanoHabitacion.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoHabitacion.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoHabitacion.Add(materialLlavedearresto);
                    ListaMaterialesBanoHabitacion.Add(materialPoceta);
                    ListaMaterialesBanoHabitacion.Add(materialDuchatelefono);
                    ListaMaterialesBanoHabitacion.Add(materialPuertadeduchas);
                    ListaMaterialesBanoHabitacion.Add(materialLlavededucha);
                    ListaMaterialesBanoHabitacion.Add(materialDuchacorona);
                    ListaMaterialesBanoHabitacion.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoHabitacion.Add(materialLamparas);
                    ListaMaterialesBanoHabitacion.Add(materialParedespintura);
                    ListaMaterialesBanoHabitacion.Add(materialTechopintura);

                    data.Cuerpo.Add(await CreacionData(ListaMaterialesHabitacion, "Habitación", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoHabitacion, "Habitación", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("A2C446EA-C961-4623-9671-C30908319E28");
                }
                if (titulo.Equals("Quirófanos"))
                {
                    List<MaterialRevision> ListaMaterialesQuirofanos = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoQuirofanos = new List<MaterialRevision>();

                    ListaMaterialesQuirofanos.Add(materialPuertaPrincipal);
                    ListaMaterialesQuirofanos.Add(materialBisagras);
                    ListaMaterialesQuirofanos.Add(materialCerradura);
                    ListaMaterialesQuirofanos.Add(materialLamparacialítica);
                    ListaMaterialesQuirofanos.Add(materialPuertasdevidriotemplex);
                    ListaMaterialesQuirofanos.Add(materialCestadehacer);
                    ListaMaterialesQuirofanos.Add(materialInterruptordeencendido);
                    ListaMaterialesQuirofanos.Add(materialLamparas);
                    ListaMaterialesQuirofanos.Add(materialTomacorrientes);
                    ListaMaterialesQuirofanos.Add(materialTapasdetomacorrientes);
                    ListaMaterialesQuirofanos.Add(materialEquipodeaireacondicionado);
                    ListaMaterialesQuirofanos.Add(materialEscabel);
                    ListaMaterialesQuirofanos.Add(materialParedespintura);
                    ListaMaterialesQuirofanos.Add(materialTechopintura);

                    ListaMaterialesBanoQuirofanos.Add(materialPuertadebaño);
                    ListaMaterialesBanoQuirofanos.Add(materialBisagras);
                    ListaMaterialesBanoQuirofanos.Add(materialCerradura);

                    data.Cuerpo.Add(await CreacionData(ListaMaterialesQuirofanos, "Quirófano", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoQuirofanos, "Quirófano", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("2FB4601A-348C-4EBB-BEE1-0C3BC2494090");
                }
                if (titulo.Equals("Emergencia (Planta Baja)"))
                {
                    List<MaterialRevision> ListaMaterialesEmergencia = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoEmergencia = new List<MaterialRevision>();

                    ListaMaterialesEmergencia.Add(materialEstructuradecortinas);
                    ListaMaterialesEmergencia.Add(materialCamaclinica);
                    ListaMaterialesEmergencia.Add(materialSilladeacompanante);
                    ListaMaterialesEmergencia.Add(materialMesa);
                    ListaMaterialesEmergencia.Add(materialEscabel);
                    ListaMaterialesEmergencia.Add(materialLamparas);
                    ListaMaterialesEmergencia.Add(materialTomacorrientes);
                    ListaMaterialesEmergencia.Add(materialTapasdetomacorrientes);
                    ListaMaterialesEmergencia.Add(materialParedespintura);
                    ListaMaterialesEmergencia.Add(materialTechopintura);
                    ListaMaterialesEmergencia.Add(materialTomadeoxigeno);

                    ListaMaterialesBanoEmergencia.Add(materialPuertadebaño);
                    ListaMaterialesBanoEmergencia.Add(materialBisagras);
                    ListaMaterialesBanoEmergencia.Add(materialCerraduraBano);
                    ListaMaterialesBanoEmergencia.Add(materialLavamanos);
                    ListaMaterialesBanoEmergencia.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoEmergencia.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoEmergencia.Add(materialLlavedearresto);
                    ListaMaterialesBanoEmergencia.Add(materialPoceta);
                    ListaMaterialesBanoEmergencia.Add(materialDuchatelefono);
                    ListaMaterialesBanoEmergencia.Add(materialPuertadeduchas);
                    ListaMaterialesBanoEmergencia.Add(materialLlavededucha);
                    ListaMaterialesBanoEmergencia.Add(materialDuchacorona);
                    ListaMaterialesBanoEmergencia.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoEmergencia.Add(materialLamparas);
                    ListaMaterialesBanoEmergencia.Add(materialParedespintura);
                    ListaMaterialesBanoEmergencia.Add(materialTechopintura);

                    data.Cuerpo.Add(await CreacionData(ListaMaterialesEmergencia, "Cubículo", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoEmergencia, "Cubículo", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("D9486900-9BEA-42F8-980C-223F832C6F40");
                }
                if (titulo.Equals("Suite"))
                {
                    List<MaterialRevision> ListaMaterialesSuite = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoSuite = new List<MaterialRevision>();

                    ListaMaterialesSuite.Add(materialPuertaPrincipal);
                    ListaMaterialesSuite.Add(materialBisagras);
                    ListaMaterialesSuite.Add(materialPuertasdecloset);
                    ListaMaterialesSuite.Add(materialCerradura);
                    ListaMaterialesSuite.Add(materialGabinete);
                    ListaMaterialesSuite.Add(materialCamaclinica);                  
                    ListaMaterialesSuite.Add(materialDivan);
                    ListaMaterialesSuite.Add(materialCloset);
                    ListaMaterialesSuite.Add(materialTelevisor);
                    ListaMaterialesSuite.Add(materialInterruptordeencendido);
                    ListaMaterialesSuite.Add(materialLamparas);
                    ListaMaterialesSuite.Add(materialTomacorrientes);
                    ListaMaterialesSuite.Add(materialTapasdetomacorrientes);
                    ListaMaterialesSuite.Add(materialEquipodeaireacondicionado);
                    ListaMaterialesSuite.Add(materialEscabel);
                    ListaMaterialesSuite.Add(materialVentanas);
                    ListaMaterialesSuite.Add(materialTomadeoxigeno);
                    ListaMaterialesSuite.Add(materialParalhospitalario);
                    ListaMaterialesSuite.Add(materialNeveraejecutiva);
                    ListaMaterialesSuite.Add(materialParedespintura);
                    ListaMaterialesSuite.Add(materialTechopintura);
                    ListaMaterialesSuite.Add(materialExtractordetecho);

                    ListaMaterialesBanoSuite.Add(materialPuertadebaño);
                    ListaMaterialesBanoSuite.Add(materialBisagras);
                    ListaMaterialesBanoSuite.Add(materialCerradura);
                    ListaMaterialesBanoSuite.Add(materialLavamanos);
                    ListaMaterialesBanoSuite.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoSuite.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoSuite.Add(materialLlavedearresto);
                    ListaMaterialesBanoSuite.Add(materialPoceta);
                    ListaMaterialesBanoSuite.Add(materialDuchatelefono);
                    ListaMaterialesBanoSuite.Add(materialPuertadeduchas);
                    ListaMaterialesBanoSuite.Add(materialLlavededucha);
                    ListaMaterialesBanoSuite.Add(materialDuchacorona); 
                    ListaMaterialesBanoSuite.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoSuite.Add(materialLamparas);
                    ListaMaterialesBanoSuite.Add(materialParedespintura);
                    ListaMaterialesBanoSuite.Add(materialTechopintura);

                    data.Cuerpo.Add(await CreacionData(ListaMaterialesSuite, "Suite", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoSuite, "Suite", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("98BCC273-C7A3-49F0-872D-A59B0593E051");
                }
                if (titulo.Equals("Ambulatorio Piso 5"))
                {
                    List<MaterialRevision> ListaMaterialesAmbulatorio5 = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoAmbulatorio5 = new List<MaterialRevision>();

                    ListaMaterialesAmbulatorio5.Add(materialEstructuradecortinas);
                    ListaMaterialesAmbulatorio5.Add(materialCamaclinica);
                    ListaMaterialesAmbulatorio5.Add(materialSilladeacompanante);
                    ListaMaterialesAmbulatorio5.Add(materialMesa);
                    ListaMaterialesAmbulatorio5.Add(materialEscabel);
                    ListaMaterialesAmbulatorio5.Add(materialLamparas);
                    ListaMaterialesAmbulatorio5.Add(materialTomacorrientes);
                    ListaMaterialesAmbulatorio5.Add(materialTapasdetomacorrientes);
                    ListaMaterialesAmbulatorio5.Add(materialParedespintura);
                    ListaMaterialesAmbulatorio5.Add(materialTechopintura);
                    ListaMaterialesAmbulatorio5.Add(materialTomadeoxigeno);

                    ListaMaterialesBanoAmbulatorio5.Add(materialPuertadebaño);
                    ListaMaterialesBanoAmbulatorio5.Add(materialBisagras);
                    ListaMaterialesBanoAmbulatorio5.Add(materialCerraduraBano);
                    ListaMaterialesBanoAmbulatorio5.Add(materialLavamanos);
                    ListaMaterialesBanoAmbulatorio5.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoAmbulatorio5.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoAmbulatorio5.Add(materialLlavedearresto);
                    ListaMaterialesBanoAmbulatorio5.Add(materialPoceta);
                    ListaMaterialesBanoAmbulatorio5.Add(materialDuchatelefono);
                    ListaMaterialesBanoAmbulatorio5.Add(materialPuertadeduchas);
                    ListaMaterialesBanoAmbulatorio5.Add(materialLlavededucha);
                    ListaMaterialesBanoAmbulatorio5.Add(materialDuchacorona);
                    ListaMaterialesBanoAmbulatorio5.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoAmbulatorio5.Add(materialLamparas);
                    ListaMaterialesBanoAmbulatorio5.Add(materialParedespintura);
                    ListaMaterialesBanoAmbulatorio5.Add(materialTechopintura);


                    data.Cuerpo.Add(await CreacionData(ListaMaterialesAmbulatorio5, "Cubículo", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoAmbulatorio5, "Cubículo", "Baño"));

                    configuracion.InformeAreaId = Guid.Parse("61A50897-126E-4E0D-8DAB-C7279F931167");
                }
                if (titulo.Equals("Consultorio área APS"))
                {
                    List<MaterialRevision> ListaMaterialesConsultorioAreaAPS = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoConsultorioAreaAPS = new List<MaterialRevision>();

                    ListaMaterialesConsultorioAreaAPS.Add(materialPuertaPrincipal);
                    ListaMaterialesConsultorioAreaAPS.Add(materialBisagras);
                    ListaMaterialesConsultorioAreaAPS.Add(materialCerradura);
                    ListaMaterialesConsultorioAreaAPS.Add(materialCamilla);
                    ListaMaterialesConsultorioAreaAPS.Add(materialMesadecomputadora);
                    ListaMaterialesConsultorioAreaAPS.Add(materialSilla);
                    ListaMaterialesConsultorioAreaAPS.Add(materialInterruptordeencendido);
                    ListaMaterialesConsultorioAreaAPS.Add(materialLamparas);
                    ListaMaterialesConsultorioAreaAPS.Add(materialTomacorrientes);
                    ListaMaterialesConsultorioAreaAPS.Add(materialTapasdetomacorrientes);
                    ListaMaterialesConsultorioAreaAPS.Add(materialEquipodeaireacondicionado);
                    ListaMaterialesConsultorioAreaAPS.Add(materialEscabel);
                    ListaMaterialesConsultorioAreaAPS.Add(materialVentanas);
                    ListaMaterialesConsultorioAreaAPS.Add(materialLavamanos);
                    ListaMaterialesConsultorioAreaAPS.Add(materialParedespintura);
                    ListaMaterialesConsultorioAreaAPS.Add(materialTechopintura);


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

                    List<MaterialRevision> ListaMaterialesAmbulatorio4 = new List<MaterialRevision>();
                    List<MaterialRevision> ListaMaterialesBanoAmbulatorio4 = new List<MaterialRevision>();

                    ListaMaterialesAmbulatorio4.Add(materialEstructuradecortinas);
                    ListaMaterialesAmbulatorio4.Add(materialCamaclinica);
                    ListaMaterialesAmbulatorio4.Add(materialSilladeacompanante);
                    ListaMaterialesAmbulatorio4.Add(materialMesa);
                    ListaMaterialesAmbulatorio4.Add(materialEscabel);
                    ListaMaterialesAmbulatorio4.Add(materialLamparas);
                    ListaMaterialesAmbulatorio4.Add(materialTomacorrientes);
                    ListaMaterialesAmbulatorio4.Add(materialTapasdetomacorrientes);
                    ListaMaterialesAmbulatorio4.Add(materialParedespintura);
                    ListaMaterialesAmbulatorio4.Add(materialTechopintura);
                    ListaMaterialesAmbulatorio4.Add(materialTomadeoxigeno);

                    ListaMaterialesBanoAmbulatorio4.Add(materialPuertadebaño);
                    ListaMaterialesBanoAmbulatorio4.Add(materialBisagras);
                    ListaMaterialesBanoAmbulatorio4.Add(materialCerraduraBano);
                    ListaMaterialesBanoAmbulatorio4.Add(materialLavamanos);
                    ListaMaterialesBanoAmbulatorio4.Add(materialLlavedelavamanos);
                    ListaMaterialesBanoAmbulatorio4.Add(materialCanilladelavamanos);
                    ListaMaterialesBanoAmbulatorio4.Add(materialLlavedearresto);
                    ListaMaterialesBanoAmbulatorio4.Add(materialPoceta);
                    ListaMaterialesBanoAmbulatorio4.Add(materialDuchatelefono);
                    ListaMaterialesBanoAmbulatorio4.Add(materialPuertadeduchas);
                    ListaMaterialesBanoAmbulatorio4.Add(materialLlavededucha);
                    ListaMaterialesBanoAmbulatorio4.Add(materialDuchacorona);
                    ListaMaterialesBanoAmbulatorio4.Add(materialInterruptordeencendido);
                    ListaMaterialesBanoAmbulatorio4.Add(materialLamparas);
                    ListaMaterialesBanoAmbulatorio4.Add(materialParedespintura);
                    ListaMaterialesBanoAmbulatorio4.Add(materialTechopintura);


                    data.Cuerpo.Add(await CreacionData(ListaMaterialesAmbulatorio4, "Cubículo", null));
                    data.Cuerpo.Add(await CreacionData(ListaMaterialesBanoAmbulatorio4, "Cubículo", "Baño"));

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

            ZonaRevision zonaRevision1 = new ZonaRevision();
            List<TipoZonaRevision> tipoZonaRevision = new List<TipoZonaRevision>(); 


            if (NombreZona.Equals("Oficina"))
            {
                if (string.IsNullOrEmpty(BanoDeZona))
                {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Tecnologia" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Recursos Humanos" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Compras" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Administracion" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Almacen" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Facturacion" });
                }
                else
                {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Damas " });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Caballeros" });                  
                }
            }
            if (NombreZona.Equals("Habitación"))
            {
                if (string.IsNullOrEmpty(BanoDeZona))
                {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "01" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "02" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "03" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "04" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "05" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "06" });
                }
                else
                {
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Damas " });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Caballeros" });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Medicos en APS" });
                }
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

            if (NombreZona.Equals("Quirófano"))
            {
                if (string.IsNullOrEmpty(BanoDeZona))
                {
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "1" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "2" });
                    tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "3" });
                }
                else
                {
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Damas " });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Caballeros" });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Medicos en APS" });
                }
            }

            if (NombreZona.Equals("Cubículo"))
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
                else
                {
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Damas " });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Caballeros" });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Medicos en APS" });
                }
            }

            if (NombreZona.Equals("Suite"))
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
                else
                {
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Damas " });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "APS Caballeros" });
                    //tipoZonaRevision.Add(new TipoZonaRevision { Nombre = "Medicos en APS" });
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

        //private async Task obtenerUsuarioAgenda()
        //{
        //    ListNombreUsuario = await ServicioAgendaTelefonica.ObtenerListaUsuarioDeAgenda();

        //    ListNombreUsuario.OrderBy(x => x).ToList();
        //}

        public async void Editar(MudBlazor.CellContext<PlanillaDigitalDataGrid> planillaDigital)
        {

            IdRegistroSeleccionado = planillaDigital.Item.Id;
            EditarAgenda = JsonSerializer.Deserialize<DataPlanilla>(planillaDigital.Item.Respuesta);
            EditarAgenda.UsuarioCreador = planillaDigital.Item.UsuarioCreador;
            EditarAgenda.FechaCreacion = planillaDigital.Item.FechaCreacion.ToString();

            mostrarModalConsulta = true;

        }

        private void CerrarModalEditar()
        {
            mostrarModalConsulta = false;
            EditarAgenda = new DataPlanilla();
            StateHasChanged();
        }

        private void CerrarModalFotos()
        {
            ImagenModalFotos = new List<DataImagen>();
            mostrarModalFotos = false;           
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
                    if (configPantalla.AgruparCuerpos)
                    {
                        zonaRevision = configPantalla.Cuerpo[0].zonaRevision.Select(x => x.Nombre).FirstOrDefault();
                    }
                }
                else
                { MostrarFormulario = true; }
                                   
        }

        public void AbrirModalFilrto()
        {
            CreateRegistro = new List<MaterialRevision>();
            if (configPantalla != null)
            {
                var todos = "Todos";
                ListaZonaFiltro = new List<string>();
                ListaZonaFiltro.Add(todos);
                ZonaFiltro = todos;
                TipoZonaFiltro = todos;
                var cuerpo = configPantalla.Cuerpo.ToList();
                foreach (var zonaLista in cuerpo) 
                {
                    foreach (var zona in zonaLista.zonaRevision)
                    {
                        ListaZonaFiltro.Add(zona.Nombre);

                        foreach (var TipoZona in zona.tipoZonaRevision)
                        {
                            ListaTipoZonaFiltro.Add(TipoZona.Nombre);
                        }                       
                    }
                    
                }
                
                mostrarModalFiltro = true;

            }

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
                p.Zona.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.FechaCreacion.ToIsoDateString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                 p.TipoZona.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
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

        //private async Task<IEnumerable<string>> SearchUsuario(string value)
        //{
        //    IEnumerable<string> result = new List<string>();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            return new List<string>();
        //        }
        //        result = ListNombreUsuario.Where(x => x.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
        //    }


        //    return result;
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

                //guardar foto
                await servicioNoticias.SubirImagenes(listaImagenCargada, planillaDigitalRegistro.Id, ruta);

                await RefrescarDataGrid();
                CerrarModalNuevo();

            } catch (Exception ex) 
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

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

                ListAreaInforme = intranetContext.informeArea.Where(x=> x.InformeTituloId == guid).OrderBy(x => x.Nombre).ToList();

            }
            catch (Exception ex)
            { Log.Error(ex.Message + ex.StackTrace + ex.InnerException); }    
            
        }

        private void AbrirModalNoticia()
        {
            //ImagenModalNoticia = Noticia.Imagen;
            //TituloModalNoticia = Noticia.TituloNoticia;
            //TextoModalNoticia = Noticia.TextoNoticia;
            //FechaModalNoticia = Noticia.FechaNoticia;

            //setear ruta de imagenes
            ImagenModalFotos = new List<DataImagen>();
            //StateHasChanged();
            mostrarModalFotos = true;
        }
        public void eliminarImagenCargadaNuevo(ListaImagenCargada item)
        {
            listaImagenCargada.Remove(item);

        }

        private async Task UploadFilesNuevo(IBrowserFile archivo)
        {
            int tamano = 2 * 1024 * 1024;
            if (archivo != null)
            {
                if (archivo.Size < 2 * 1024 * 1024)
                {

                    try
                    {
                        using var stream = archivo.OpenReadStream(2 * 1024 * 1024);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);

                        listaImagenCargada.Add(new ListaImagenCargada
                        {
                            imagenSeleccionadaCargada = $"data:{archivo.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}",
                            NombreimagenSeleccionadaCargada = archivo.Name,
                            NombreFisicoimagenSeleccionadaCargada = Guid.NewGuid().ToString() + Path.GetExtension(archivo.Name)
                        });

                    }
                    catch (Exception ex)
                    {

                        Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
                        listaImagenCargada = new List<ListaImagenCargada>();
                        Snackbar.Add("Ocurrio un error", Severity.Error);

                    }
                }
                else
                {
                    Snackbar.Add("la imagen excede el tamaño, debe ser igual o menor a : " + tamano, Severity.Error);
                }

            }
            else
            {

                listaImagenCargada = new List<ListaImagenCargada>();
            }

        }
    }

}
