using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IPalestrantePersist
    {         
         Task<Palestrante[]> GetAllPalestrantesAsync(bool includesEventos);
         Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includesEventos);
         Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includesEventos);
    }
}