using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.WebApiClient.SDK;

namespace Ray.EssayNotes.WebApiClient.MicroServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAccountApi _accountApi;

        public TestController(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        [HttpGet]
        public string Test([FromQuery]string name, [FromQuery]string pwd)
        {
            return _accountApi.LoginAsync(name, pwd).GetAwaiter().GetResult();
        }
    }
}
