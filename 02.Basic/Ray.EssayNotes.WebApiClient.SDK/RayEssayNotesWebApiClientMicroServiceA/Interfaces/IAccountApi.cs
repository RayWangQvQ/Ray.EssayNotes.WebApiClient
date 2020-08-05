using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Ray.EssayNotes.WebApiClient.SDK.RayEssayNotesWebApiClientMicroServiceA.Interfaces
{
    [HttpHost("http://localhost:5000/")]
    public interface IAccountApi : IHttpApi
    {
        [HttpGet("account/token")]
        ITask<string> LoginAsync(string name, string pwd);
    }
}
