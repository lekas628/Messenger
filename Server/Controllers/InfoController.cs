﻿using System;
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
        [HttpGet("MessageCount")]
        public int Get()
        {
            return Program.message.GetCountMessages();
        }

    }
}