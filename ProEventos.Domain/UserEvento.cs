namespace ProEventos.Domain
{
    public class UserEvento
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}