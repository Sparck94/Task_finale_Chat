using Microsoft.AspNetCore.Mvc;
using task_chat.DTO;
using task_chat.Services;
using task_chat.Utils;

namespace task_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtenteController : Controller
    {
        private readonly UtenteService _service;
        public UtenteController(UtenteService service)
        {
            _service = service;
        }
        [HttpPost("registrati")]
        public ActionResult<Risposta> InserisciUtente(UtenteDTO oUte)
        {
            List<string> listaErrore = new List<string>();

            if (oUte.User is null || oUte.User.Trim().Equals(""))
                listaErrore.Add("Campo Username vuoto");

            if (_service.InserisciUtente(oUte))
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
        [HttpDelete("elimina/{varCod}")]
        public ActionResult Delete(string varCod)
        {
            if (_service.EliminaUtente(new UtenteDTO() { Cod = varCod }))
                return Ok(new Risposta()
                {
                    Status = "SUCCESS"
                });
            return Ok(new Risposta()
            {
                Status = "ERROR",
                Data = "Eliminazione non effettuata"
            });
        }
    }
}
