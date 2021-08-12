using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;
using System;

namespace ProEventos.Persistence
{
  public class GeralPersist : IGeralPersist
  {
    private readonly ProEventosContext _context;
    public GeralPersist(ProEventosContext context)
    {
      _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }
    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);

    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public void DeleteRange<T>(T entityArray) where T : class
    {
      _context.RemoveRange(entityArray);
    }
    public async Task<bool> SaveChangesAsync()
    {
      var save = false;
      try
      {
        save = (await _context.SaveChangesAsync()) > 0;
      }
      catch (Exception ex)
      { 
          Console.WriteLine($"Error saving changes {ex}");
      }

      return save;
    }
  }
}