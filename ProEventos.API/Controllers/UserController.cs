using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dto;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    public readonly IUserService _userService;
    public UserController(IUserService UserService)
    {
      _userService = UserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var users = await _userService.GetAllUsersAsync();
        if (users == null) return NoContent();
        //"Evento não encontrado!"
        return Ok(users);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar Users. Erro: {ex.Message}");
      }
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId)
    {
      try
      {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NoContent();
        //"Evento não encontrado!"
        return Ok(user);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar Users. Erro: {ex.Message}");
      }
    }
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
      try
      {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null) return NoContent();
        return Ok(user);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar recuperar Usuario. Erro: {ex.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDto model)
    {
      try
      {
        var userExist = await _userService.GetUserByEmailAsync(model.Email);

        if (!(userExist == null)) return BadRequest("Email já cadastrado!");

        var user = await _userService.NewUser(model);
        if (user == null) return BadRequest("Erro ao cadastrar usuário!");

        return Ok(user);
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao cadastrar usuário! {ex.Message}");
      }
    }
    [HttpPost("login")]
    public async Task<IActionResult> AuthLogin(LoginDto model)
    {
      try
      {
        var user = await _userService.GetUserByEmailAsync(model.Email);

        if(user == null){
          BadRequest("Dados inválidos!");
        } 

        var AuthLogin = (model.Senha == user.Senha);

        return AuthLogin ? Ok(user) 
                          : BadRequest("Dados inválidos!");
        
      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro realizar login! {ex.Message}");
      }
    }
    [HttpPut("{userId}")]
    public async Task<IActionResult> Put(int userId, UserDto model)
    {
      try
      {
        var userExist = await _userService.GetUserByEmailAsync(model.Email);
        if (userExist != null)
        {
          if (!(userExist.Id == userId))
          {
            return BadRequest("Email já cadastrado!");
          }
        }

        var user = await _userService.UpdateUser(userId, model);
        if (user == null) return BadRequest("Erro ao atualizar User!");
        return Ok(user);

      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar atualizar User. Erro: {ex.Message}");
      }
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(int userId)
    {
      try
      {
        return await _userService.DeleteUser(userId) ?
              Ok(new { message = "Deletado" }) :
              BadRequest("Usuário não deletado");

      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro ao tentar atualizar Lote. Erro: {ex.Message}");
      }
    }
  }
}
