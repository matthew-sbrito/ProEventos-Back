using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dto
{
    public class EventoDto
    {  
    public int Id { get; set; }
    
    [Required]
    public string Local { get; set; }
    
    [Required]
    public DateTime? DataEvento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!"),
    //  MinLength(4, ErrorMessage = "{0} deve ter no mínimo 4 caracteres."),
    //  MaxLength(50, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")
     StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 4 a 50 caracteres.")
     ]
    public string Tema { get; set; }

    [Required]
    [   Display(Name="Quantidade de pessoas"),
        Range(1, 120000, ErrorMessage="{0} não pode ser maior que 120.000")]
    public int QtdPessoas { get; set; }

    [Required]
    // [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida!")]
    public string ImagemURL { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!"),
     Phone(ErrorMessage = "{0} está inválido!")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!"),
     Display(Name ="e-mail"),
     EmailAddress(ErrorMessage="Necessita ser um e-mail válido!")]
    public string Email { get; set; }
    public IEnumerable<LoteDto> Lotes { get; set; }
    public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    
    }
}