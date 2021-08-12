using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
  public class LotePersist : ILotePersist
  {
    private readonly ProEventosContext _context;
    public LotePersist(ProEventosContext context)
    {
      _context = context;
    }

    public async Task<Lote[]> GetLotesByEventoAsync(int eventoId)
    {
      IQueryable<Lote> query = _context.Lotes;
      query = query.AsNoTracking().Where(x => x.EventoId == eventoId);
     
      return await query.ToArrayAsync();
    }

    public async Task<Lote> GetLoteByIdAsync(int eventoId, int loteId)
    {
      IQueryable<Lote> query = _context.Lotes;
      query = query.AsNoTracking()
        .Where(x => x.EventoId == eventoId && x.Id == loteId);
      return await query.FirstOrDefaultAsync();
    }
  }
}