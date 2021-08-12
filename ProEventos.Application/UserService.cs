using System;
using ProEventos.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Interfaces;
using ProEventos.Application.Dto;
using AutoMapper;

namespace ProEventos.Application
{
  public class UserService : IUserService
  {
    private readonly IGeralPersist _geralPersist;
    private readonly IUserPersist _userPersist;
    private readonly IMapper _mapper;

    public UserService(
      IGeralPersist geralPersist,
      IUserPersist userPersist,
      IMapper mapper)
    {
      _geralPersist = geralPersist;
      _userPersist = userPersist;
      _mapper = mapper;
    }

    public async Task<UserDto[]> GetAllUsersAsync()
    {
      try
      {
        var users = await _userPersist.GetAllUsersAsync();
        if (users == null) return null;

        var resultado = _mapper.Map<UserDto[]>(users);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
      try
      {
        var user = await _userPersist.GetUserByIdAsync(userId);
        if (user == null) return null;

        var resultado = _mapper.Map<UserDto>(user);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
     public async Task<UserDto> GetUserByEmailAsync(string email)
    {
      try
      {
        var user = await _userPersist.GetUserByEmailAsync(email);
        if (user == null) return null;

        var resultado = _mapper.Map<UserDto>(user);

        return resultado;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<UserDto> NewUser(UserDto model)
    {
       try
      {
        var user = _mapper.Map<User>(model);
        _geralPersist.Add<User>(user);
        if (await _geralPersist.SaveChangesAsync())
        {
          var userReturn = await _userPersist.GetUserByIdAsync(user.Id);
          
          return _mapper.Map<UserDto>(userReturn);
        };
        return null;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    public async Task<UserDto> UpdateUser(int userId, UserDto model)
    {
    try
      {
        var user = await _userPersist.GetUserByIdAsync(userId);
        if (user == null) return null;
        
        model.Id = user.Id;
       _mapper.Map(model, user);

        _geralPersist.Update<User>(user);
        if (await _geralPersist.SaveChangesAsync())
        {
          var eventReturn = await _userPersist.GetUserByIdAsync(user.Id);
          return _mapper.Map<UserDto>(eventReturn);
        }
        return null;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
    
    public async Task<bool> DeleteUser(int userId)
    {
       try
      {
        var user = await _userPersist.GetUserByIdAsync(userId);
        if (user == null) throw new Exception("Usuário não encontrado!");

        _geralPersist.Delete<User>(user);

        return await _geralPersist.SaveChangesAsync();

      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
  }
}