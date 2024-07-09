using Intranet.Modelos.Admin;

namespace Intranet.Interfaces.Admin
{
    public interface IServicioAdmin
    {
        public string BuscarRolDeUsuario(Guid id);
        public bool ActualizarUsuarioBD(List<U1_Usuario> listaDiretorioActivo);
        public Task<bool> ActualizarUsuario();
        Task<List<CrudUsuario>> ObtenerListaUsuario();
        Task<List<R1_Rol>> ObtenerRolesAsync();
        Task<bool> GuardarUsuario(U1_Usuario EditarUsuario);
        Task<bool> GuardarRol(R1_Rol CrearRol);
        Task<List<C1_Categoria>> ObtenerListaCategoria();
        Task<bool> GuardarCategoria(C1_Categoria nuevaCategoria);
        Task<bool> ConsultarCategoria(C1_Categoria nuevaCategoria);
        Task<bool> ActualizarCategoria(C1_Categoria nuevaCategoria);
        Task<bool> EliminarCategoria(C1_Categoria CategoriaData);
        Task<List<SubCategoriaDataGrid>> ObtenerListaSubCategoria(string categoriaNombre = null);
        Task<bool> GuardarSubCategoria(CrearSubCategoria nuevaSubCategoria);
        Task<bool> ConsultarSubCategoria(CrearSubCategoria subCategoria);
        Task<bool> ActualizarSubCategoria(CrearSubCategoria SubCategoria);
        Task<bool> EliminarSubCategoria(CrearSubCategoria SubCategoriaData);
        Task<bool> ExistenciaSubCategoria(CrearSubCategoria subCategoria);
        Task <List<string>> ObtenerPermisosDeUsuario(Guid id);
        Task<bool> ConsultarNombreRol(R1_Rol rolData);
        Task<bool> ConsultarNombreCategoria(C1_Categoria CategoriaData);
        Task<bool> ActualizarRol(R1_Rol nuevaRol);
        Task<bool> ConsultarRol(R1_Rol nuevaRol);
        Task<bool> EliminarRol(R1_Rol RolData);
        Task<List<PermisosDataGrid>> ObtenerListaPermisos();
        Task<bool> ConsultarPermiso(CrudPermiso permiso);
        Task<bool> GuardarPermiso(CrudPermiso permiso);
        Task<bool> ActualizarPermiso(CrudPermiso SubCategoria);
        Task<bool> ExistenciaPermiso(CrudPermiso permiso);
        Task<bool> EliminarPermiso(CrudPermiso permiso);
        Task <TipoDeRol> ObtenerTiposDeRoles();
        Task <List<P1_Permiso>> ObtenerListaPermisosDeRol(Guid Id);
        Task<bool> ActualizarTipoRol(R1_Rol RolAEditar,List<P1_Permiso> ListaPermisosgregar, List<P1_Permiso> listaPermisosEliminar);
        Task<bool> ConsultarRolEnUsuario(R1_Rol nuevaRol);

    }
}
