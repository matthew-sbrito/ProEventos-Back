using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
  public class UserPersist : IUserPersist
  {
    private readonly ProEventosContext _context;
    public UserPersist(ProEventosContext context)
    {
      _context = context;
    }
    public async Task<User[]> GetAllUsersAsync()
    {
      IQueryable<User> query = _context.Users;
     
        query = query.AsNoTracking().OrderBy(e => e.Id);

        return await query.ToArrayAsync();
    }

    public async Task<User[]> GetUsersByNameAsync(string nome)
    {
      IQueryable<User> query = _context.Users;
 
      query = query.AsNoTracking().OrderBy(u => u.Id)
          .Where(u => u.Nome.ToLower().Contains(nome.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
      IQueryable<User> query = _context.Users;
      query = query.AsNoTracking()
        .Where(x => x.Id == userId);
      
      query = query
              .Include(u => u.UserEventos)
              .ThenInclude(ue => ue.Evento);
      
      return await query.FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
      IQueryable<User> query = _context.Users;
      query = query.AsNoTracking()
        .Where(x => x.Email == email);
      
      query = query
              .Include(u => u.UserEventos)
              .ThenInclude(ue => ue.Evento);
      
      return await query.FirstOrDefaultAsync();
    }
  }
}