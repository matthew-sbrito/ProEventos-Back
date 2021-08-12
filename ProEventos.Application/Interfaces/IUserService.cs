using System.Threading.Tasks;
using ProEventos.Application.Dto;

namespace ProEventos.Application.Interfaces
{
  public interface IUserService
  {
    Task<UserDto[]> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> NewUser(UserDto model);
    Task<UserDto> UpdateUser(int userId, UserDto model);
    Task<bool> DeleteUser(int userId);
    
  }
}