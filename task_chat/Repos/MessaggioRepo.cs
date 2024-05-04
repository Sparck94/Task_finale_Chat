using MongoDB.Bson;
using MongoDB.Driver;
using task_chat.Models;


namespace task_chat.Repos
{
    public class MessaggioRepo
    {
        private IMongoCollection<Messaggio> messaggi;
        private readonly ILogger _logger;
        public MessaggioRepo(IConfiguration configuration, ILogger<MessaggioRepo> logger)
        {
            this._logger = logger;

            string? connessioneLocale = configuration.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = configuration.GetValue<string>("MongoDbSettings:MongoDbName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            messaggi = _database.GetCollection<Messaggio>("Messaggi");

        }
        public bool Create(Messaggio entity)
        {
            try
            {
                messaggi.InsertOne(entity);
                _logger.LogInformation("Inserimento effettuato con successo");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public List<Messaggio> GetMessaggi(ObjectId stanzaId)
        {
            var filter = Builders<Messaggio>.Filter.Eq(m => m.StanzaId, stanzaId);
            return messaggi.Find(filter).ToList();
        }
    }
}
