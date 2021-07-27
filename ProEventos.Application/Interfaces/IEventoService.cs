using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
  public interface IEventoService
  {
    Task<Evento> AddEventos(Evento model);
    Task<Evento> UpdateEvento(int eventoId, Evento model);
    Task<bool> DeleteEvento(int eventoId);
    Task<Evento[]> GetAllEventosAsync(bool includesPalestrantes = false);
    Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includesPalestrantes = false);
    Task<Evento> GetEventoByIdAsync(int eventoId, bool includesPalestrantes = false);
  }
}