using MongoDB.Bson;
using task_chat.DTO;
using task_chat.Models;
using task_chat.Repos;

namespace task_chat.Services
{
    public class StanzaService
    {
        private readonly StanzaRepo _repo;
        public StanzaService(StanzaRepo repository)
        {
            _repo = repository;
        }
        public bool CreaStanza(StanzaDTO oSta)
        {
            if(oSta is not null)
            {
                Stanza sta = new Stanza()
                {
                    NomeStanza = oSta.Nom,
                    Descrizione = oSta.Des,
                    Utentes = oSta.Ute,
                    Messaggios = oSta.Mex
                };
                return _repo.Create(sta);
            }
            return false;
            
        }
        private List<MessaggioDTO>? ConvertMsgToDto(List<Messaggio>? messages)
        {
            List<MessaggioDTO> elenco = new List<MessaggioDTO>();
            if (messages is not null)
            {
                foreach (Messaggio m in messages)
                {
                    MessaggioDTO msg = new MessaggioDTO()
                    {                        
                        Cont = m.Contenuto,
                        Data = m.DataMessaggio,
                        Ute = m.UtenteInviante,                       
                    };
                    elenco.Add(msg);
                }
            }
            return elenco;

        }
       
    }
}
