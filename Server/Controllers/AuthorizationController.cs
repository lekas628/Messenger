﻿using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        // POST api/<AuthorizationController>
        [HttpPost]
        public bool Post([FromBody] DataPerson dataPerson)
        {
            return Program.authorizationClass.CheckLoginAndPassword(dataPerson);
        }
    }
}
