using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dto
{
  public class LoginDto
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório!"),
     Display(Name = "e-mail"),
     EmailAddress(ErrorMessage = "Necessita ser um e-mail válido!")]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }

  }
}