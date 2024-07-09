using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Planillas.InspeccionHabitaciones;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Intranet.Pages
{
    public partial class Permisos
    {
        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        public IQueryable<PermisosDataGrid> PermisosData { get; set; } = null;
        public IQueryable<PermisosDataGrid> MasterPermisosData { get; set; } = null;
        private CrudPermiso NuevoRegistro = new CrudPermiso();
        private CrudPermiso EditarRegistro = new CrudPermiso();
        private CrudPermiso RegistroEliminar = new CrudPermiso();
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private bool mostrarModalEliminar = false;
        private bool CategoriaSeleccionadaValid = true;
        private List<C1_Categoria> ListaCategoria = new List<C1_Categoria>();
        private List<SubCategoriaDataGrid> ListaSubCategoria = new List<SubCategoriaDataGrid>();
        protected override async Task OnInitializedAsync()
        {
           await RefrescarDataGrid();
             NuevoRegistro = new CrudPermiso();
             EditarRegistro = new CrudPermiso();
             RegistroEliminar = new CrudPermiso();
             ListaCategoria = new List<C1_Categoria>();
             ListaSubCategoria = new List<SubCategoriaDataGrid>();

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

            MasterPermisosData = PermisosData = resultado;
        }

        public async Task<List<PermisosDataGrid>> Data()
        {
            var resultado = new List<PermisosDataGrid>();

            resultado = await ServicioAdmin.ObtenerListaPermisos();
         
            return resultado.OrderBy(a => a.Nombre).ToList();
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                PermisosData = MasterPermisosData;

            }else
             {
                PermisosData = MasterPermisosData.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ).OrderBy(p => p.Nombre);

             }
        }
        public async Task Editar(MudBlazor.CellContext<PermisosDataGrid> PermisoData)
        {
            await obtenerListaCategoria();
            EditarRegistro.Id = PermisoData.Item.Id;
            EditarRegistro.Nombre = PermisoData.Item.Nombre;
            EditarRegistro.NombreCategoria = PermisoData.Item.Categoria.Nombre;
            EditarRegistro.NombreSubCategoria = PermisoData.Item.SubCategoria.Nombre;

            mostrarModalEditar = true;
        }
        public void Eliminar(MudBlazor.CellContext<PermisosDataGrid> PermidoData)
        {
            mostrarModalEliminar = true;
            RegistroEliminar.Nombre = PermidoData.Item.Nombre;
            RegistroEliminar.Id = PermidoData.Item.Id;
            RegistroEliminar.NombreCategoria = PermidoData.Item.Categoria.Nombre;
            RegistroEliminar.NombreSubCategoria = PermidoData.Item.SubCategoria.Nombre;

        }
    }
}
