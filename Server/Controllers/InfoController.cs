using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {

        // GET api/<GetCountMessagesController>/5
<<<<<<< HEAD
        [HttpGet("{id}")]
        public int Get()
        {
            return Program.message.GetCountMessages();
        }
=======
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


>>>>>>> daa133b73be6ca4e50669179ff8d91c2387f2fc9
    }
}
