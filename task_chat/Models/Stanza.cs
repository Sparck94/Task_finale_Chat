using MongoDB.Bson;

namespace task_chat.Models
{
    public class Stanza
    {
        public ObjectId StanzaId { get; set; }
        public string NomeStanza { get; set; } = null!;
        public string?  Descrizione { get; set; }
        public List<Utente>? Utentes { get; set; }
        public List<Messaggio>? Messaggios { get; set; }
        public List<ObjectId>? MessaggioId { get; set; }
    }
}
