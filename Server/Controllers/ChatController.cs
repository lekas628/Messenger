using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class chatController : ControllerBase
    {
        // GET api/<chatController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string Get(int id)
        {
            string json = "Not found";
            if ((id < Program.message.GetCountMessages()) && (id >= 0))
            {
                json = JsonSerializer.Serialize(Program.message.Get(id));
                return json.ToString();
            }
            return json;
        }

        // POST api/<chatController>
        [HttpPost]
        public void Post([FromBody] Message message)
        {
            if (message.Name == "God#")
            {
                if (message.Text[0] == '~')
                {
                    Program.message.RemoveAt(int.Parse(message.Text[1].ToString()));
                }
            }
            else
            {
                Program.message.Add(message);
                message.show();
            }
        }
    }
}
