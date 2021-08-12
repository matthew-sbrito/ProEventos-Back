using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface ILotePersist
    {
         Task<Lote[]> GetLotesByEventoAsync(int eventoId);
         Task<Lote> GetLoteByIdAsync(int eventoId, int loteId);
    
    }
}