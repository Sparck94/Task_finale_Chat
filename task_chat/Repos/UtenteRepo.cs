using task_chat.Models;

namespace task_chat.Repos
{
    public class UtenteRepo : IRepo<Utente>
    {
        private readonly TaskChatContext _context;

        public UtenteRepo(TaskChatContext context)
        {
            _context = context;
        }
        public bool Create(Utente entity)
        {
            try
            {
                _context.Utentes.Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Utente? temp = Get(id);
                if (temp != null)
                {
                    _context.Utentes.Remove(temp);
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return false;
        }

        public Utente? Get(int id)
        {
            return _context.Utentes.Find(id);
        }

        public IEnumerable<Utente> GetAll()
        {
            return _context.Utentes.ToList();
        }

        public bool Update(Utente entity)
        {
            try
            {
                _context.Utentes.Update(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public Utente? GetByCodice(string codice)
        {
            try
            {
                return _context.Utentes.FirstOrDefault(u => u.CodiceUtente == codice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }
        public bool DeleteByCodice(string codice)
        {
            try
            {
                    _context.Utentes.Remove(_context.Utentes.First(u => u.CodiceUtente == codice));
                    _context.SaveChanges();

                    return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return false;
        }
        public Utente? CheckLogin(UserLogin oUte)
        {

            Utente? temp = _context.Utentes.FirstOrDefault(p => p.Username == oUte.Username && p.PasswordUtente == oUte.Password);
            return temp;
        }
    }
}
