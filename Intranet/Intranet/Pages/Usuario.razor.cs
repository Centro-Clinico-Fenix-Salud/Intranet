using Intranet.Services.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Client;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using System.Drawing.Text;
using Intranet.Modelos.Agenda;
using Microsoft.AspNetCore.Components.Forms;


namespace Intranet.Pages
{
    public partial class Usuario
    {
        bool procesando = false;

        [Inject]
        private IServicioAdmin ServicioAdmin { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        private string searchTerm;
        public IQueryable<CrudUsuario> Usuarios { get; set; } = null;
        public IQueryable<CrudUsuario> MasterUsuarios { get; set; } = null;
        private bool _resizeColumn = true;
        public string parametro { get; set; }
        private bool mostrarModalEditar = false;
        CrudUsuario EditarUsuario = new CrudUsuario();
        private bool UbicacionSeleccionadaValid = true;
        private List<string> ListRoles = new List<string>();
        private List<R1_Rol> roles = new List<R1_Rol>();

        protected override async Task OnInitializedAsync()
        {
            await RefrescarDataGrid();

            await obtenerRoles();

        }

        private async Task RefrescarDataGrid() 
        {
            var resultado = (await Data()).AsQueryable();

            MasterUsuarios = Usuarios = resultado;
        }

        async Task ActualizarUsuarios()
        {
            bool result = false; 
            procesando = true;
            await Task.Delay(100);
            result = await ServicioAdmin.ActualizarUsuario();
            
            procesando = false;
            await RefrescarDataGrid();
            if (result)
            Snackbar.Add("actualización de usuarios exitoso", Severity.Info);
            else
                Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        public void Eliminar(MudBlazor.CellContext<U1_Usuario> usuario)
        {
            //RegistroEliminar = direccionTelefonica.Item.Nombre + " - " + direccionTelefonica.Item.Unidad;
            //IdELiminarAgenda = direccionTelefonica.Item.Id;
            //mostrarModalEliminar = true;
        }

        public async Task Editar(MudBlazor.CellContext<CrudUsuario> Usuario)
        {

            EditarUsuario.Id = Usuario.Item.Id;
            //EditarUsuario.Rol = Usuario.Item.Rol;
            EditarUsuario.NombreRolId = Usuario.Item.NombreRolId;
            EditarUsuario.R1_RolId = Usuario.Item.R1_RolId;
            EditarUsuario.FirstName = Usuario.Item.FirstName;
            EditarUsuario.LastName = Usuario.Item.LastName;
            EditarUsuario.Active = Usuario.Item.Active;
            EditarUsuario.Username = Usuario.Item.Username;
            EditarUsuario.CreatedAt = Usuario.Item.CreatedAt;
            EditarUsuario.UpdatedAt = Usuario.Item.UpdatedAt;
            EditarUsuario.Email = Usuario.Item.Email;

            mostrarModalEditar = true;

        }

        private async Task obtenerRoles()
        {

            roles = await ServicioAdmin.ObtenerRolesAsync();
            
            foreach (var role in roles)
            {
                ListRoles.Add(role.Nombre);
            }
            //ListRoles.Add("SuperAdmin");
            //ListRoles.Add("Basico");

            ListRoles.OrderBy(x => x).ToList();
        }

        private void OnUbicacionSeleccionadaChanged(string value)
        {
            if (!string.IsNullOrEmpty(value)) {
                //EditarUsuario.Rol.Nombre = value;
                var IdRol = roles.Where(x => x.Nombre == value).FirstOrDefault();
                if (IdRol != null) 
                {
                    EditarUsuario.R1_RolId = IdRol.Id;
                    EditarUsuario.NombreRolId = value;
                   // EditarUsuario.RolId = IdRol.Id;
                }
                  
            }            

            UbicacionSeleccionadaValid = !string.IsNullOrEmpty(value);
        }


        private async Task OnValidSubmit(EditContext context)
        {
            U1_Usuario editarUsuario = new U1_Usuario();
            editarUsuario.Id = EditarUsuario.Id ;           
            editarUsuario.R1_RolId = EditarUsuario.R1_RolId ;
            editarUsuario.FirstName = EditarUsuario.FirstName ;
            editarUsuario.LastName = EditarUsuario.LastName ;
            editarUsuario.Active = EditarUsuario.Active;
            editarUsuario.Username = EditarUsuario.Username ;
            editarUsuario.CreatedAt = EditarUsuario.CreatedAt ;
            editarUsuario.UpdatedAt = EditarUsuario.UpdatedAt ;
            editarUsuario.Email = EditarUsuario.Email ;

            var resultado = await ServicioAdmin.GuardarUsuario(editarUsuario);

            
            if (resultado) {
                await RefrescarDataGrid();
                Snackbar.Add("Modificación exitosa", Severity.Info);
                CerrarModalEditar();
            }                
            else   
            Snackbar.Add("Ocurrio un error", Severity.Error);
        }

        private void CerrarModalEditar()
        {

            mostrarModalEditar = false;
            EditarUsuario = new CrudUsuario();

        }

        async Task ProcesarSolicitud()
        {
            procesando = true;

            // Aquí puedes realizar la lógica de procesamiento de la solicitud

            // Simulación de espera con Task.Delay
          //  await Task.Delay(3000); // Espera de 3 segundos

            procesando = false;
        }

        public class listaPropiedad 
        {
            public string key { get; set; }
            public string valor { get; set; }
        }

        public void Nuevo()
        {
            //CreateAgenda = new AgendaCreate();
            //mostrarModalNuevo = true;
        }
        public async Task<List<CrudUsuario>> Data()
        {
            var resultado = new List<CrudUsuario>();

            resultado = await ServicioAdmin.ObtenerListaUsuario();

            return resultado.OrderBy(a => a.FirstName).ToList();
        }
        private async Task Buscar()
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                Usuarios = MasterUsuarios;

            }

            else
            {
                Usuarios = MasterUsuarios.Where(p => p.FirstName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Username.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.NombreRolId.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ).OrderBy(p => p.FirstName);

            }
        }
    }
}
