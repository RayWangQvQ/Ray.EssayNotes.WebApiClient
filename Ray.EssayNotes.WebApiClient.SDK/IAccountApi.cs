using System;
using WebApiClient;
using WebApiClient.Attributes;

namespace Ray.EssayNotes.WebApiClient.SDK
{
    [HttpHost("http://localhost:5000/")]
    public interface IAccountApi : IHttpApi
    {
        [HttpGet("account/token")]
        ITask<string> LoginAsync(string name, string pwd);
    }
}
