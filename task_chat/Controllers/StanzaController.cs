using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using task_chat.DTO;
using task_chat.Services;
using task_chat.Utils;

namespace task_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StanzaController : Controller
    {
        private readonly StanzaService _service;
        public StanzaController(StanzaService service)
        {
            _service = service;
        }
        [HttpPost("genera/stanza")]
        public ActionResult<Risposta> CreaStanza(StanzaDTO oSta)
        {
            List<string> listaErrore = new List<string>();

            if (oSta.Nom is null || oSta.Nom.Trim().Equals(""))
                listaErrore.Add("Campo Nome stanza vuoto");

            if (_service.CreaStanza(oSta))
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
        [HttpGet("chat/{id}")]
        public IActionResult GetRoomAndMessage(string id)
        {
            return Ok(new Risposta()
            {
                Status = "Success",
                Data = _service.GetRoom(new ObjectId(id))
            });
        }
        [HttpPost("chat/addUser/{id}")]
        public IActionResult AddUserToChatRoom(string id, string username)
        {
            try
            {
                if (_service.InsertUserIntoChatRoom(username, new ObjectId(id)))
                    return Ok(new Risposta()
                    {
                        Status = "Success"
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }
    }
}
