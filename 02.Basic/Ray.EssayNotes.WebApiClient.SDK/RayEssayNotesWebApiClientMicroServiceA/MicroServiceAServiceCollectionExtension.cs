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

        public static void AddMicroServiceAServiceClient(this IServiceCollection services, Func<IServiceProvider, MicroServicesClientHostOption> option)
        {
            services.AddRayClientApi<IAccountApi>(configuration, ClientName);
        }

        public static void AddMicroServiceAServiceClient(this IServiceCollection services, IConfiguration configuration, MicroServicesClientHostOption clientHostOption)
        {
            services.AddRayClientApi<IAccountApi>(configuration, ClientName);
        }
    }
}
