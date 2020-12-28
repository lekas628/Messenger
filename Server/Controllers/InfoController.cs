using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {

        // GET api/<GetCountMessagesController>/5
        [Route("MessageCount")]
        [HttpGet]
        public int GetMessageCount()
        {
            return Program.message.GetCountMessages();
        }

        [Route("Status")]
        [HttpGet]
        public bool GetStatus()
        {
            return true;
        }
    }
}
