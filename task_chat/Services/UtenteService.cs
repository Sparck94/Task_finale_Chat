using System.Security.Cryptography;
using task_chat.DTO;
using task_chat.Models;
using task_chat.Repos;

namespace task_chat.Services
{
    public class UtenteService : IService<Utente>
    {
        private readonly UtenteRepo _repository;

        public UtenteService(UtenteRepo repository)
        {
            _repository = repository;
        }

        public IEnumerable<Utente> PrendiTutto()
        {
            return _repository.GetAll();
        }
        public List<UtenteDTO> RestituisciTutti()
        {
            List<UtenteDTO> elenco = this.PrendiTutto().Select(u => new UtenteDTO()
            {
                Cod = u.CodiceUtente,
                User = u.Username
              
            }).ToList();

            return elenco;
        }
        public bool EliminaUtente(UtenteDTO oUte)
        {
            if (oUte.Cod is not null)
            {
                return _repository.DeleteByCodice(oUte.Cod);
            }
            return false;
        }
        public bool InserisciUtente(UtenteDTO oUte)
        {

            if(oUte.User is not null && oUte.Psw is not null)
            {
                string md5Hash = PswMD.HashPassword(oUte.Psw);
                oUte.Psw = md5Hash;
                Utente ute = new Utente()
                {
                    Username = oUte.User,
                    PasswordUtente = oUte.Psw
                };
                return _repository.Create(ute);
            }
            return false;


        }
        public UtenteDTO? RicercaPerCodice(UtenteDTO oUte)
        {
            if (oUte.Cod is not null)
            {
                Utente? ute = _repository.GetByCodice(oUte.Cod);

                if (ute is not null)
                    return new UtenteDTO()
                    {
                        Cod = ute.CodiceUtente,
                        User = ute.Username,
                    };
            }
            return null;
        }
        public bool LoginUtente(UserLogin oUte)
        {
            if(oUte.Password is not null)
            {
                string md5Hash = PswMD.HashPassword(oUte.Password);
                oUte.Password = md5Hash;
            }
            return _repository.CheckLogin(oUte) is not null ? true : false;
        }
    }
}
