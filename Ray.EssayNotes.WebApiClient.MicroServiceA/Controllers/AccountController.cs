using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ray.EssayNotes.WebApiClient.MicroServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("token")]
        public string Login([FromQuery]string name, [FromQuery]string pwd)
        {
            return "This is jwt";
        }
    }
}
