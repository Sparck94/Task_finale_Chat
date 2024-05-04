using task_chat.Models;

namespace task_chat.DTO
{
    public class StanzaDTO
    {
        public string Nom { get; set; } = null!;
        public string? Des{ get; set; }
        public List<Utente>? Ute { get; set; }
        public List<Messaggio>? Mex { get; set; }
    }
}
