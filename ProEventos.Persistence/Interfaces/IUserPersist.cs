using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IUserPersist
    {
         Task<User[]> GetAllUsersAsync();
         Task<User[]> GetUsersByNameAsync(string nome);
         Task<User> GetUserByEmailAsync(string email);
         Task<User> GetUserByIdAsync(int userId);
    
    }
}