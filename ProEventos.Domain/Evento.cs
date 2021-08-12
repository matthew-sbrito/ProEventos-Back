using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
  [Table("Eventos")]
  public class Evento
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Local { get; set; }
    
    [Required]
    public DateTime? DataEvento { get; set; }

    // [NotMapped]
    // public int contagemDias { get; set; }
  
    [Required]
    [MaxLength(50)]
    public string Tema { get; set; }
    
    [Required]
    public int QtdPessoas { get; set; }
    
    [Required]
    public string ImagemURL { get; set; }
    
    [Required]
    public string Telefone { get; set; }
    
    [Required]
    public string Email { get; set; }
    public IEnumerable<Lote> Lotes { get; set; }
    public IEnumerable<RedeSocial> RedesSociais { get; set; }
    public IEnumerable<UserEvento> UserEventos { get; set; }
    public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    
  }
}