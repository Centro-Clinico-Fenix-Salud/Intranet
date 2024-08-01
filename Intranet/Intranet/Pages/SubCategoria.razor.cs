using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Intranet.Modelos.Planillas.InspeccionHabitaciones;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Intranet.Pages
{
    public partial class SubCategoria
    {
        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        public IQueryable<SubCategoriaDataGrid> SubCategoriaData { get; set; } = null;
        public IQueryable<SubCategoriaDataGrid> MasterSubCategoriaData { get; set; } = null;
        private CrearSubCategoria NuevoRegistro = new CrearSubCategoria();
        private CrearSubCategoria EditarRegistro = new CrearSubCategoria();
        private CrearSubCategoria RegistroEliminar = new CrearSubCategoria();
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private bool mostrarModalEliminar = false;
        private bool CategoriaSeleccionadaValid = true;
        private C1_Categoria CategoriaSeleccionada =  new C1_Categoria();
        private List<C1_Categoria> ListaCategoria = new List<C1_Categoria>();
        protected override async Task OnInitializedAsync()
        {
           await RefrescarDataGrid();
             NuevoRegistro = new CrearSubCategoria();
             EditarRegistro = new CrearSubCategoria();
             RegistroEliminar = new CrearSubCategoria();
             CategoriaSeleccionada = new C1_Categoria();
             ListaCategoria = new List<C1_Categoria>();
            
        }


        async Task AgregarMostrarModal()
        {
            await obtenerListaCategoria();
            mostrarModalNuevo = true;
 
            //bool result = false;

            //await Task.Delay(100);
            //result = await ServicioAdmin.ActualizarUsuario();

            //await RefrescarDataGrid();
            //if (result)
            //    Snackbar.Add("actualización de usuarios exitoso", Severity.Info);
            //else
            //    Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private void CerrarModalNuevo()
        {

            mostrarModalNuevo = false;
            NuevoRegistro = new CrearSubCategoria();

        }
        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarRegistro = new CrearSubCategoria();

        }
        private void CerrarModalEliminar()
        {

            mostrarModalEliminar = false;

        }
        private void OnCategoriaSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                NuevoRegistro.NombreCategoria = value;

            CategoriaSeleccionadaValid = !string.IsNullOrEmpty(value);
        }
        private void OnCategoriaSeleccionadaEditarChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                EditarRegistro.NombreCategoria = value;

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
            if (!await ServicioAdmin.ExistenciaSubCategoria(RegistroEliminar))
            {
                Snackbar.Add("la Sub-Categoria no existe", Severity.Error);
                return;
            }

            if (await ServicioAdmin.EliminarSubCategoria(RegistroEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Sub-Categoria Eliminada", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        

        }

        private async Task Guardar(EditContext context)
        {

            if (await ServicioAdmin.ConsultarSubCategoria(NuevoRegistro))
            {
                Snackbar.Add("Sub-Categoria ya existe", Severity.Error);
                return;
            }
            
            if (await ServicioAdmin.GuardarSubCategoria(NuevoRegistro))
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
          
            if (await ServicioAdmin.ActualizarSubCategoria(EditarRegistro))
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

            MasterSubCategoriaData = SubCategoriaData = resultado;
        }

        public async Task<List<SubCategoriaDataGrid>> Data()
        {
            var resultado = new List<SubCategoriaDataGrid>();

            resultado = await ServicioAdmin.ObtenerListaSubCategoria();
         
            return resultado.OrderBy(a => a.Nombre).ToList();
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                SubCategoriaData = MasterSubCategoriaData;

            }else
             {
                SubCategoriaData = MasterSubCategoriaData.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ).OrderBy(p => p.Nombre);

             }
        }
        public async Task Editar(MudBlazor.CellContext<SubCategoriaDataGrid> SubcategoriaData)
        {
            await obtenerListaCategoria();
            EditarRegistro.Id = SubcategoriaData.Item.Id;
            EditarRegistro.Nombre = SubcategoriaData.Item.Nombre;
            EditarRegistro.NombreCategoria = SubcategoriaData.Item.Categoria.Nombre;

            mostrarModalEditar = true;

        }
        public void Eliminar(MudBlazor.CellContext<SubCategoriaDataGrid> SubcategoriaData)
        {
            mostrarModalEliminar = true;
            RegistroEliminar.Nombre = SubcategoriaData.Item.Nombre;
            RegistroEliminar.Id = SubcategoriaData.Item.Id;
            RegistroEliminar.NombreCategoria = SubcategoriaData.Item.Categoria.Nombre;

        }
    }
}
