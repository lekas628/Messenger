using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        // POST api/<RegistrationController>
        [HttpPost]
        public bool Post([FromBody] DataPerson dataPerson)
        {
            bool status = Program.authorizationClass.Registration(dataPerson);
            if (status) Program.authorizationClass.SaveToFile();
            return status;
        }
    }
}
