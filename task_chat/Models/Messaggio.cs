using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace task_chat.Models
{
    public class Messaggio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MessageId { get; set; }

        [BsonElement("sender")]
        public string? UtenteInviante { get; set; }
        [BsonElement("text")]
        public string? Contenuto { get; set; }

        [BsonElement("dataInvio")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime DataMessaggio { get; set; } = DateTime.Now.Date;

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId StanzaId { get; set; }

        internal static object Find(FilterDefinition<Messaggio> filter)
        {
            throw new NotImplementedException();
        }
    }
}
