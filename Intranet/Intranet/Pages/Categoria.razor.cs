using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Intranet.Pages
{
    public partial class Categoria
    {
        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        public IQueryable<C1_Categoria> CategoriaData { get; set; } = null;
        public IQueryable<C1_Categoria> MasterCategoriaData { get; set; } = null;
        C1_Categoria NuevoRegistro = new C1_Categoria();
        C1_Categoria EditarRegistro = new C1_Categoria();
        C1_Categoria RegistroEliminar = new C1_Categoria();
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private bool mostrarModalEliminar = false;

        protected override async Task OnInitializedAsync()
        {
           await RefrescarDataGrid();

        }


        async Task AgregarMostrarModal()
        {
            mostrarModalNuevo = true;
 
        }
        private void CerrarModalNuevo()
        {

            mostrarModalNuevo = false;
            NuevoRegistro = new C1_Categoria();

        }
        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarRegistro = new C1_Categoria();

        }
        private void CerrarModalEliminar()
        {

            mostrarModalEliminar = false;

        }

        private async Task EliminarRegistro()
        {
            if (!await ServicioAdmin.ConsultarCategoria(RegistroEliminar))
            {
                Snackbar.Add("la Categoria no existe", Severity.Error);
                return;
            }

            if (await ServicioAdmin.EliminarCategoria(RegistroEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Categoria Eliminada", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }

        private async Task Guardar(EditContext context)
        {

            if (await ServicioAdmin.ConsultarNombreCategoria(NuevoRegistro))
            {
                Snackbar.Add("Categoria ya existe", Severity.Error);
                return;
            }
            
            if (await ServicioAdmin.GuardarCategoria(NuevoRegistro))
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
          
            if (await ServicioAdmin.ActualizarCategoria(EditarRegistro))
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

            MasterCategoriaData = CategoriaData = resultado;
        }

        public async Task<List<C1_Categoria>> Data()
        {
            var resultado = new List<C1_Categoria>();

            resultado = await ServicioAdmin.ObtenerListaCategoria();
         
            return resultado.OrderBy(a => a.Nombre).ToList();
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                CategoriaData = MasterCategoriaData;

            }else
             {
                CategoriaData = MasterCategoriaData.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ).OrderBy(p => p.Nombre);

             }
        }
        public async Task Editar(MudBlazor.CellContext<C1_Categoria> categoriaData)
        {

            EditarRegistro.Id = categoriaData.Item.Id;
            EditarRegistro.Nombre = categoriaData.Item.Nombre;

            mostrarModalEditar = true;

        }
        public void Eliminar(MudBlazor.CellContext<C1_Categoria> categoriaData)
        {
            mostrarModalEliminar = true;
            RegistroEliminar.Nombre = categoriaData.Item.Nombre;
            RegistroEliminar.Id = categoriaData.Item.Id;
            
        }
    }
}
