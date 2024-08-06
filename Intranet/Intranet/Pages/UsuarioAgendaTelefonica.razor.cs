using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Intranet.Modelos.Agenda;
using System.Security.Claims;

namespace Intranet.Pages
{
    public partial class UsuarioAgendaTelefonica
    {
        [Inject]
        private IServicioUsuarioAgendaTelefonica ServicioUsuarioAgendaTelefonica { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        private bool _resizeColumn = true;
        public IQueryable<U2_UsuarioAgendaTelefonica> UsuarioData { get; set; } = null;
        public IQueryable<U2_UsuarioAgendaTelefonica> MasterUsuarioData { get; set; } = null;
        U2_UsuarioAgendaTelefonica NuevoRegistro = new U2_UsuarioAgendaTelefonica();
        U2_UsuarioAgendaTelefonica EditarRegistro = new U2_UsuarioAgendaTelefonica();
        U2_UsuarioAgendaTelefonica RegistroEliminar = new U2_UsuarioAgendaTelefonica();
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
            NuevoRegistro = new U2_UsuarioAgendaTelefonica();

        }
        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarRegistro = new U2_UsuarioAgendaTelefonica();

        }
        private void CerrarModalEliminar()
        {

            mostrarModalEliminar = false;

        }

        private async Task EliminarRegistro()
        {
            if (!await ServicioUsuarioAgendaTelefonica.ConsultarUsuario(RegistroEliminar))
            {
                Snackbar.Add("El Usuario no existe", Severity.Error);
                return;
            }
            if (await ServicioUsuarioAgendaTelefonica.ConsultarAfiliacionUsuario(RegistroEliminar))
            {
                Snackbar.Add("No se puede eliminar usuario, esta asignado a un registro telefónico", Severity.Error);
                return;
            }

            if (await ServicioUsuarioAgendaTelefonica.EliminarUsuario(RegistroEliminar))
            {
                await RefrescarDataGrid();
                Snackbar.Add("Usuario Eliminado", Severity.Info);
                CerrarModalEliminar();
            }
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);

        }

        private async Task Guardar(EditContext context)
        {

            if (await ServicioUsuarioAgendaTelefonica.ConsultarNombreUsuario(NuevoRegistro))
            {
                Snackbar.Add("Nombre de usuario ya existe", Severity.Error);
                return;
            }          
            NuevoRegistro.UsuarioModificador = Guid.Parse(await IdUsuario());
            if (await ServicioUsuarioAgendaTelefonica.GuardarUsuario(NuevoRegistro))
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
          
            if (await ServicioUsuarioAgendaTelefonica.ConsultarAntesActualizarUsuario(EditarRegistro))
            {
                Snackbar.Add("Nombre de usuario ya existe", Severity.Error);
                return;
            }
            if (await ServicioUsuarioAgendaTelefonica.ActualizarUsuario(EditarRegistro))
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

            MasterUsuarioData = UsuarioData = resultado;
        }

        public async Task<List<U2_UsuarioAgendaTelefonica>> Data()
        {
            var resultado = new List<U2_UsuarioAgendaTelefonica>();

            resultado = await ServicioUsuarioAgendaTelefonica.ObtenerListaUsuario();

            return resultado.OrderBy(a => a.Nombre).ToList();
        }

        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                UsuarioData = MasterUsuarioData;

            }
            else
            {
                UsuarioData = MasterUsuarioData.Where(p => p.Nombre.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(p => p.Nombre);

            }
        }
        public async Task Editar(MudBlazor.CellContext<U2_UsuarioAgendaTelefonica> UsuarioaData)
        {

            EditarRegistro.Id = UsuarioaData.Item.Id;
            EditarRegistro.Nombre = UsuarioaData.Item.Nombre;
            EditarRegistro.FechaCreacion = UsuarioaData.Item.FechaCreacion;
            EditarRegistro.UsuarioModificador = Guid.Parse(await IdUsuario());
            EditarRegistro.FechaModificacion = DateTime.Now;
    
            mostrarModalEditar = true;

        }
        public void Eliminar(MudBlazor.CellContext<U2_UsuarioAgendaTelefonica> UsuarioaData)
        {
            mostrarModalEliminar = true;
            RegistroEliminar.Nombre = UsuarioaData.Item.Nombre;
            RegistroEliminar.Id = UsuarioaData.Item.Id;

        }
        private async Task<string> IdUsuario()
        {
            return ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
