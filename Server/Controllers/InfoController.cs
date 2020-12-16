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
=======
        [HttpGet("{id}")]
        public int Get()
        {
            return Program.message.GetCountMessages();
        }

>>>>>>> 0ced0e193b5e311c142fe88740fdf4a8620e51ab
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
