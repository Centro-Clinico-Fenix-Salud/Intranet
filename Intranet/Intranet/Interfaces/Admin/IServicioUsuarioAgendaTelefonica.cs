using Intranet.Modelos.Agenda;

namespace Intranet.Interfaces.Admin
{
    public interface IServicioUsuarioAgendaTelefonica
    {
        Task<List<U2_UsuarioAgendaTelefonica>> ObtenerListaUsuario();
        Task<bool> ConsultarNombreUsuario(U2_UsuarioAgendaTelefonica usuario);
        Task<bool> GuardarUsuario(U2_UsuarioAgendaTelefonica Usuario);
        Task<bool> ActualizarUsuario(U2_UsuarioAgendaTelefonica nuevoUsuario);
        Task<bool> ConsultarAntesActualizarUsuario(U2_UsuarioAgendaTelefonica EditarUsuario);
        Task<bool> ConsultarUsuario(U2_UsuarioAgendaTelefonica nuevoUsuario);
        Task<bool> EliminarUsuario(U2_UsuarioAgendaTelefonica UsuarioData);
        Task<bool> ConsultarAfiliacionUsuario(U2_UsuarioAgendaTelefonica UsuarioData);

    }
}
