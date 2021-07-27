using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
  public class PalestrantePersist : IPalestrantePersist
  {
    private readonly ProEventosContext _context;

    public PalestrantePersist(ProEventosContext context)
    {
      _context = context;
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includesEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedesSociais);

        if (includesEventos){
            query = query
                .Include(p => p.PalestrantesEventos)
                .ThenInclude(pe => pe.Evento);
        }
    
        query = query.AsNoTracking().OrderBy(p => p.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includesEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedesSociais);

        if (includesEventos){
            query = query
                .Include(p => p.PalestrantesEventos)
                .ThenInclude(pe => pe.Evento);
        }
    
        query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

        return await query.ToArrayAsync();
    }


    public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includesEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedesSociais);

        if (includesEventos){
            query = query
                .Include(p => p.PalestrantesEventos)
                .ThenInclude(pe => pe.Evento);
        }
    
        query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(p => p.Id == palestranteId);

        return await query.FirstOrDefaultAsync();
    }

   
  }
}