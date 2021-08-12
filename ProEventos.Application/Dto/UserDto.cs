using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dto
{
  public class UserDto
  {
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!"),
     Display(Name = "e-mail"),
     EmailAddress(ErrorMessage = "Necessita ser um e-mail válido!")]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
    [Required]
    public int IsAdmin { get; set; }
    [Required]
    public int IsPalest { get; set; }
    public IEnumerable<EventoDto> Eventos { get; set; }

  }
}