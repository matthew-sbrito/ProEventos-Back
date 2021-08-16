using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ProEventos.API.Controllers
{

  public class UserToken
  {
    public string Token { get; set; }
    // public DateTime Expiration { get; set; }
  }
  public class Login
  {
    public string Email { get; set; }
    public string Senha { get; set; }
  }

  [ApiController]
  [Route("[controller]")]
  public class AuthenticateController : ControllerBase
  {
    public IUserService _userService;
    public IConfiguration _configuration;
    public AuthenticateController(IUserService userService, IConfiguration configuration)
    {
      _userService = userService;
      _configuration = configuration;
    }

    [HttpPost("user")]
    public async Task<ActionResult> GetUserByToken(UserToken model)
    {
      var decode = DecodeToken(model.Token);

      var user = await _userService.GetUserByEmailAsync(decode.Email);

      if (user == null)
      {
        BadRequest("Dados inválidos!");
      }

      var AuthLogin = (decode.Senha == user.Senha);

      if (!AuthLogin)
      {
        BadRequest("Dados inválidos!");
      }

      return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(Login model)
    {
      try
      {
        var authenticate = await validateLogin(model);

        if (!authenticate) BadRequest("Dados inválidos!");

        Login token = new Login { Email = model.Email, Senha = model.Senha };

        return Ok(BuildToken(token));

      }
      catch (Exception ex)
      {
        return this.StatusCode(500, $"Erro realizar login! {ex.Message}");
      }
    }
    private UserToken BuildToken(Login user)
    {
      var claimsData = new[] {
				new Claim("Email", user.Email),
				new Claim("Senha", user.Senha)
			};
      
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var signInCred = new SigningCredentials(key,  SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        expires: DateTime.Now.AddMinutes(5),
        claims: claimsData,
        signingCredentials: signInCred
      );
      var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

      return new UserToken{ Token = jwtToken };

    }
    private Login DecodeToken(string token)
    {
      string secret = _configuration["Jwt:Key"];
      var key = Encoding.ASCII.GetBytes(secret);
      var handler = new JwtSecurityTokenHandler();
      var validations = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
      };
      var claims = handler.ValidateToken(token, validations, out var tokenSecure);

      string email = claims.FindFirstValue("Email");;
      string senha = claims.FindFirstValue("Senha");;

      Login model = new Login { Email = email, Senha = senha };

      return model;
    }
    private async Task<Boolean> validateLogin(Login model)
    {
      var user = await _userService.GetUserByEmailAsync(model.Email);

      if (user == null)
      {
        return false;
      }

      var AuthLogin = (model.Senha == user.Senha);

      if (!AuthLogin)
      {
        return false;
      }

      return true;
    }
  }
}
