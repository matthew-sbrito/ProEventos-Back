using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IsAdmin { get; set; }
        public int IsPalest { get; set; }
        public IEnumerable<UserEvento> UserEventos { get; set; }

    }
}