using System.Threading.Tasks;
using ProEventos.Application.Dto;

namespace ProEventos.Application.Interfaces
{
  public interface IEventoService
  {
    Task<EventoDto> AddEventos(EventoDto model);
    Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
    Task<bool> DeleteEvento(int eventoId);
    Task<EventoDto[]> GetAllEventosAsync(bool includesPalestrantes = false);
    Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includesPalestrantes = false);
    Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includesPalestrantes = false);
  }
}