using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.WebApiClient.SDK.RayEssayNotesWebApiClientMicroServiceA.Interfaces;
using WebApiClientCore;

namespace Ray.EssayNotes.WebApiClient.SDK.RayEssayNotesWebApiClientMicroServiceA
{
    public static class MicroServiceAServiceCollectionExtension
    {
        public static string ClientName = "Ray.EssayNotes.WebApiClient.MicroServiceA";

        public static void AddMicroServiceAServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection("MicroServicesClientHost")
                .Get<List<MicroServicesClientHostOption>>()
                .First(x => x.ServiceName == ClientName);

            services.Configure<MicroServicesClientHostOption>(ClientName, configuration.GetSection($"MicroServicesClientHost:{ClientName}"));

            services.AddRayClientApi<IAccountApi>(ClientName);
        }
    }
}
