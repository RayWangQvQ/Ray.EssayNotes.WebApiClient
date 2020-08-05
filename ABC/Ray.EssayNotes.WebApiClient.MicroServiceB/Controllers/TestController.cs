using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.WebApiClient.SDK;

namespace Ray.EssayNotes.WebApiClient.MicroServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            //通过协议调用服务A的登录接口
            string result = _accountApi.LoginAsync(name, pwd).GetAwaiter().GetResult();

            return $"ServiceOfB: {result}";
        }
    }
}
