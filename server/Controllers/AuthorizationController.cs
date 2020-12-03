using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        // GET api/<AuthorizationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthorizationController>
        [HttpPost]
        public bool Post([FromBody] DataPerson dataPerson)
        {
            return Program.authorizationClass.CheckLoginAndPassword(dataPerson);
        }
    }
}
