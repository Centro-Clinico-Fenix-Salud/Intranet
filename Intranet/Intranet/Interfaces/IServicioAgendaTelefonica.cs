using Intranet.Modelos.Admin;
using Intranet.Modelos.Agenda;

namespace Intranet.Interfaces
{
    public interface IServicioAgendaTelefonica
    {
        Task<List<AgendaTelefonicaDataGrid>> ObtenerListaAgendaTelefonica();
        Task<List<string>> ObtenerListaUnidadDeAgenda();
        Task<List<string>> ObtenerListaUbicacionDeAgenda();
        Task<List<string>> ObtenerListaUsuarioDeAgenda();
        Task<bool> GuardarAgendaTelefonica(AgendaCreate NuevoRegistro);
        Task<int> ConsultarAntesGuardarAgendaTelefonica(AgendaCreate NuevoRegistro);
        Task<bool> ConsultarAgendaTelefonica(Guid IdELiminarAgenda);
        Task<bool> EliminarAgendaTelefonica(Guid IdELiminarAgenda);
        Task<bool> ActualizarAgendaTelefonica(AgendaEditar EditarAgenda);
        Task<int> ConsultarAntesActualizarAgendaTelefonica(AgendaEditar EditarAgenda);

    }
}
