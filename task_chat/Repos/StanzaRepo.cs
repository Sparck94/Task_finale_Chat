using MongoDB.Bson;
using MongoDB.Driver;
using task_chat.Models;

namespace task_chat.Repos
{
    public class StanzaRepo : IRepo<Stanza>
    {
        
        private IMongoCollection<Stanza> stanze;
        private readonly ILogger _logger;
        private readonly MessaggioRepo _messaggioRepo;
        public StanzaRepo(IConfiguration configuration, ILogger<StanzaRepo> logger, MessaggioRepo repo)
        {
            this._logger = logger;
            _messaggioRepo = repo;

            string? connessioneLocale = configuration.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = configuration.GetValue<string>("MongoDbSettings:MongoDbName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            stanze = _database.GetCollection<Stanza>("Stanze");

        }
        public bool Create(Stanza entity)
        {
            try
            {
                if (stanze.Find(s => s.NomeStanza == entity.NomeStanza).ToList().Count > 0)
                    return false;

                stanze.InsertOne(entity);
                _logger.LogInformation("Inserimento effettuato con successo");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Stanza? Get(ObjectId id)
        {
            try
            {
                return stanze.Find(i => i.StanzaId == id).ToList()[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public IEnumerable<Stanza> GetAll()
        {
            return stanze.Find(p => true).ToList();
        }

        public bool Update(Stanza entity)
        {
            throw new NotImplementedException();
        }
        public Stanza? GetRoom(ObjectId id)
        {
            Stanza room = stanze.Find(i => i.StanzaId == id).ToList()[0];
            room.Messaggios = new List<Messaggio>();
            room.Messaggios = _messaggioRepo.GetMessaggi(id);
            return room;
        }

        public Stanza? Get(int id)
        {
            throw new NotImplementedException();
        }
        public Stanza? GetById(ObjectId id)
        {
            try
            {
                return stanze.Find(i => i.StanzaId == id).ToList()[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public bool InsertUserIntoChatRoom(string username, ObjectId roomId)
        {
            Stanza? temp = GetById(roomId);
            if (temp != null)
            {
                //verificare presenza dell'utente nella lista
                temp.Utentes.Add(username);

                var filter = Builders<Stanza>.Filter.Eq(c => c.MessaggioId, temp.MessaggioId);
                try
                {
                    stanze.ReplaceOne(filter, temp);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return false;
        }
    }
}
