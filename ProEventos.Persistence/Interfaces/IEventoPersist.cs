using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersist
    {
         Task<Evento[]> GetAllEventosAsync(bool includesPalestrantes);
         Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includesPalestrantes);
         Task<Evento> GetEventoByIdAsync(int eventoId, bool includesPalestrantes);
    
    }
}