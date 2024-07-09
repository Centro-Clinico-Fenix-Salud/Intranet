using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Planillas.InspeccionHabitaciones;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Linq;

namespace Intranet.Pages
{
    public partial class TipoRol
    {
        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        public IQueryable<R1_Rol> DataRoles { get; set; } = null;
        public IQueryable<R1_Rol> MasterRoles { get; set; } = null;
        private CrudPermiso NuevoRegistro = new CrudPermiso();
        private CrudPermiso EditarRegistro = new CrudPermiso();
        private CrudPermiso RegistroEliminar = new CrudPermiso();
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private bool mostrarModalEliminar = false;
        private bool CategoriaSeleccionadaValid = true;
        private List<C1_Categoria> ListaCategoria = new List<C1_Categoria>();
        private List<SubCategoriaDataGrid> ListaSubCategoria = new List<SubCategoriaDataGrid>();
        TipoDeRol tipoDeRolData = new TipoDeRol();
        List<P1_Permiso> ListaPermisosDeRol { get; set; }
        private R1_Rol RolAEditar { get; set; }
        protected override async Task OnInitializedAsync()
        {
           await RefrescarDataGrid();
             tipoDeRolData = await ServicioAdmin.ObtenerTiposDeRoles();
             NuevoRegistro = new CrudPermiso();
             EditarRegistro = new CrudPermiso();
             RegistroEliminar = new CrudPermiso();
             ListaCategoria = new List<C1_Categoria>();
             ListaSubCategoria = new List<SubCategoriaDataGrid>();
             ListaPermisosDeRol = new List<P1_Permiso>();
             RolAEditar = new R1_Rol();
        }


        async Task AgregarMostrarModal()
        {
            await obtenerListaCategoria();
            mostrarModalNuevo = true;
 
        }
        private void CerrarModalNuevo()
        {

            mostrarModalNuevo = false;
            NuevoRegistro = new CrudPermiso();
            ListaSubCategoria = new List<SubCategoriaDataGrid>();

        }
        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarRegistro = new CrudPermiso();

        }

        private async Task EditarTipoRol()
        {

            //List<P1_Permiso> ListaPermisosModificada = new List<P1_Permiso>();
            //List<P1_Permiso> ListaPermisosgregar = new List<P1_Permiso>();
            //foreach (var Permiso in tipoDeRolData.Permisos)
            //{
            //    ListaPermisosModificada.Add(Permiso);
            //}

            //HashSet<Guid> idsModificados = new HashSet<Guid>(ListaPermisosModificada.Select(p => p.Id));

            //List<P1_Permiso> listaPermisosEliminar = ListaPermisosDeRol.Where(p => !idsModificados.Contains(p.Id)).ToList();

            //foreach (var PermisoNuevo in ListaPermisosModificada) 
            //{
            //    if(!ListaPermisosDeRol.Any(x => x.Nombre == PermisoNuevo.Nombre))
            //    ListaPermisosgregar.Add(PermisoNuevo);
            //}

            List<P1_Permiso> ListaPermisosModificada = new List<P1_Permiso>();
            List<P1_Permiso> ListaPermisosgregar = new List<P1_Permiso>();
            foreach (var Permiso in tipoDeRolData.Permisos)
            {
                if(Permiso.Activo)
                ListaPermisosModificada.Add(Permiso);
            }

            foreach (var PermisoNuevo in ListaPermisosModificada)
            {
                if (!ListaPermisosDeRol.Any(x => x.Nombre == PermisoNuevo.Nombre))
                    ListaPermisosgregar.Add(PermisoNuevo);
            }
            ListaPermisosModificada = new List<P1_Permiso>();
            foreach (var Permiso in tipoDeRolData.Permisos)
            {
                if (!Permiso.Activo)
                    ListaPermisosModificada.Add(Permiso);
            }


            HashSet<Guid> idsModificados = new HashSet<Guid>(ListaPermisosModificada.Select(p => p.Id));

            List<P1_Permiso> listaPermisosEliminar = ListaPermisosDeRol.Where(p => idsModificados.Contains(p.Id)).ToList();


            
            //var result = await ServicioAdmin.ActualizarTipoRol(RolAEditar, ListaPermisosgregar, listaPermisosEliminar);

            if (await ServicioAdmin.ActualizarTipoRol(RolAEditar, ListaPermisosgregar, listaPermisosEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Tipo de Rol Modificado", Severity.Info);
                CerrarModalEditar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

           
        }
        private void CerrarModalEliminar()
        {

            mostrarModalEliminar = false;

        }
        private async Task OnCategoriaSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                NuevoRegistro.NombreCategoria = value;
                ListaSubCategoria = await ServicioAdmin.ObtenerListaSubCategoria(value);          
            }
               
            CategoriaSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void OnSubCategoriaSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                NuevoRegistro.NombreSubCategoria = value;     
            }

            CategoriaSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private void OnSubCategoriaSeleccionadaEditarChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                EditarRegistro.NombreSubCategoria = value;
            }

            CategoriaSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private async Task OnCategoriaSeleccionadaEditarChanged(string value)
        {
            ListaSubCategoria = new List<SubCategoriaDataGrid>();
            if (!string.IsNullOrEmpty(value)) {
                EditarRegistro.NombreCategoria = value;
                ListaSubCategoria = await ServicioAdmin.ObtenerListaSubCategoria(value);
            }              
            CategoriaSeleccionadaValid = !string.IsNullOrEmpty(value);
        }

        private async Task obtenerListaCategoria()
        {
            ListaCategoria = await ServicioAdmin.ObtenerListaCategoria();
            if(ListaCategoria.Count > 0)
            ListaCategoria = ListaCategoria.OrderBy(x => x.Nombre).ToList();
        }
        private async Task EliminarRegistro()
        {
            if (!await ServicioAdmin.ExistenciaPermiso(RegistroEliminar))
            {
                Snackbar.Add("El permiso no existe", Severity.Error);
                return;
            }

            if (await ServicioAdmin.EliminarPermiso(RegistroEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Permiso Eliminado", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }

        private async Task Guardar(EditContext context)
        {

            if (await ServicioAdmin.ConsultarPermiso(NuevoRegistro))
            {
                Snackbar.Add("El Permiso ya existe", Severity.Error);
                return;
            }
            
            if (await ServicioAdmin.GuardarPermiso(NuevoRegistro))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalNuevo();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        private async Task Actualizar(EditContext context)
        {
          
            if (await ServicioAdmin.ActualizarPermiso(EditarRegistro))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Actualización exitosa", Severity.Info);
                CerrarModalEditar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        private async Task RefrescarDataGrid()
        {
            var resultado = (await Data()).AsQueryable();

            MasterRoles = DataRoles = resultado;
        }

        public async Task<List<R1_Rol>> Data()
        {
            var resultado = new List<R1_Rol>();

            resultado = await ServicioAdmin.ObtenerRolesAsync();
         
            return resultado.OrderBy(a => a.Nombre).ToList();
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                DataRoles = MasterRoles;

            }else
             {
                DataRoles = MasterRoles.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ).OrderBy(p => p.Nombre);

             }
        }
        public async Task Editar(MudBlazor.CellContext<R1_Rol> RolData)
        {
            RolAEditar.Nombre = RolData.Item.Nombre;
            RolAEditar.Id = RolData.Item.Id;

            ListaPermisosDeRol = await ServicioAdmin.ObtenerListaPermisosDeRol(RolData.Item.Id);
            var permisosIds = ListaPermisosDeRol.Select(p => p.Id).ToHashSet();

            foreach (var PermisoEstatus in tipoDeRolData.Permisos)
            {
                PermisoEstatus.Activo = permisosIds.Contains(PermisoEstatus.Id);
            }

            mostrarModalEditar = true;
        }
        public async Task Clonar(MudBlazor.CellContext<R1_Rol> RolData)
        {

        }
    }
}
