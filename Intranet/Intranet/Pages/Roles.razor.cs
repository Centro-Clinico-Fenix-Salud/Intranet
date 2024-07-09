using Intranet.Services.Admin;
using MudBlazor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Client;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using System.Drawing.Text;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components.Forms;


namespace Intranet.Pages
{
    public partial class Roles
    {
        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        public IQueryable<R1_Rol> DataRoles { get; set; } = null;
        public IQueryable<R1_Rol> MasterRoles { get; set; } = null;
        R1_Rol NuevoRegistro = new R1_Rol();
        R1_Rol EditarRegistro = new R1_Rol();
        R1_Rol RegistroEliminar = new R1_Rol();
        private bool _resizeColumn = true;
        private bool mostrarModalNuevo = false;
        private bool mostrarModalEditar = false;
        private bool mostrarModalEliminar = false;

        protected override async Task OnInitializedAsync()
        {
            await RefrescarDataGrid();
     
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
        async Task AgregarMostrarModal()
        {
            mostrarModalNuevo = true;

           
        }
        private void CerrarModalNuevo()
        {

            mostrarModalNuevo = false;
            NuevoRegistro = new R1_Rol();

        }

        private async Task Guardar(EditContext context)
        {

            if (await ServicioAdmin.ConsultarNombreRol(NuevoRegistro))
            {
                Snackbar.Add("Rol ya existe", Severity.Error);
                return;
            }

            if (await ServicioAdmin.GuardarRol(NuevoRegistro))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Registro exitoso", Severity.Info);
                CerrarModalNuevo();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                DataRoles = MasterRoles;

            }
            else
            {
                DataRoles = MasterRoles.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(p => p.Nombre);

            }
        }

        private async Task Actualizar(EditContext context)
        {

            if (await ServicioAdmin.ActualizarRol(EditarRegistro))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Actualización exitosa", Severity.Info);
                CerrarModalEditar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }
        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarRegistro = new R1_Rol();

        }

        public async Task Editar(MudBlazor.CellContext<R1_Rol> rolData)
        {

            EditarRegistro.Id = rolData.Item.Id;
            EditarRegistro.Nombre = rolData.Item.Nombre;

            mostrarModalEditar = true;

        }
        public void Eliminar(MudBlazor.CellContext<R1_Rol> RolData)
        {
            mostrarModalEliminar = true;
            RegistroEliminar.Nombre = RolData.Item.Nombre;
            RegistroEliminar.Id = RolData.Item.Id;

        }
        private void CerrarModalEliminar()
        {

            mostrarModalEliminar = false;

        }
        private async Task EliminarRegistro()
        {
            if (!await ServicioAdmin.ConsultarRol(RegistroEliminar))
            {
                Snackbar.Add("El Rol no existe", Severity.Error);
                return;
            }

            if (!await ServicioAdmin.ConsultarRolEnUsuario(RegistroEliminar))
            {
                Snackbar.Add("El rol no puede ser eliminado por esta asiganado a un usuario", Severity.Error);
                return;
            }

            if (await ServicioAdmin.EliminarRol(RegistroEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Rol Eliminada", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);


        }
    }
}
