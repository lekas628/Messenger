using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        static MessagesClass ms = new MessagesClass();

        // GET: api/<MessegesController>
        [HttpGet]
        public List<Message> Get()
        {
            return ms.messages;
        }

        // GET api/<MessegesController>/5
        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return ms.Get(id);
        }

        // POST api/<MessegesController>
        [HttpPost]
        public void Post(Message message)
        {
            ms.Add(message);
        }

        //// PUT api/<MessegesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MessegesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
