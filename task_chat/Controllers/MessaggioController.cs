using Microsoft.AspNetCore.Mvc;
using task_chat.DTO;
using task_chat.Services;
using task_chat.Utils;

namespace task_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessaggioController : Controller
    {
        private readonly MessaggioService _service;
        public MessaggioController(MessaggioService service)
        {
            _service = service;
        }
        [HttpPost("invia/messaggio")]
        public ActionResult<Risposta> InviaMessaggio(MessaggioDTO oMex)
        {
            List<string> listaErrore = new List<string>();

            if (oMex.Ute is null || oMex.Ute.Trim().Equals(""))
                listaErrore.Add("Nome Utente vuoto");
            if (oMex.Cont is null || oMex.Cont.Trim().Equals(""))
                listaErrore.Add("Campo contenuto vuoto");

            if (_service.InviaMessaggio(oMex))
            {
                return Ok(new Risposta()
                {
                    Status = "SUCCESS"
                });
            }
            else
            {
                listaErrore.Add("Inserimento Fallito");
            }
            return Ok(new Risposta()
            {
                Status = "Error",
                Data = listaErrore
            });
        }
    }
}
