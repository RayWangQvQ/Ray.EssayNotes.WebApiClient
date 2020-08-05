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
        /// <summary>
        /// 模拟登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public string Login([FromQuery]string name, [FromQuery]string pwd)
        {
            return "ServiceOfA: This is jwt";//返回token
        }
    }
}
