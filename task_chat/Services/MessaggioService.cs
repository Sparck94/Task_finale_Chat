using task_chat.DTO;
using task_chat.Models;
using task_chat.Repos;

namespace task_chat.Services
{
    public class MessaggioService
    {
        private readonly MessaggioRepo _repo;
        
        public MessaggioService(MessaggioRepo repository)
        {
            _repo = repository;
        }
        public bool InviaMessaggio(Messaggio oMex)
        {
            return _repo.Create(oMex);
        }
    }
}
