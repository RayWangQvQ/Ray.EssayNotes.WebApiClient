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
    /// <summary>
    /// 注册所有服务A的接口
    /// </summary>
    public static class MicroServiceAServiceCollectionExtension
    {
        //public static string ClientName = "RayEssayNotesWebApiClientMicroServiceA";
        private static string ClientName = "MicroServiceA";

        public static void AddMicroServiceAServiceClient(this IServiceCollection services)
        {
            services.AddRayClientApi<IAccountApi>(ClientName);
        }

        public static void AddMicroServiceAServiceClient(this IServiceCollection services, Action<MicroServiceClientHostOptions> options)
        {
            services.AddOptions<MicroServiceClientHostOptions>(ClientName).Configure(options);

            services.AddMicroServiceAServiceClient();
        }

        public static void AddMicroServiceAServiceClient(this IServiceCollection services, Action<MicroServiceClientHostOptions, IServiceProvider> options)
        {
            services.AddOptions<MicroServiceClientHostOptions>(ClientName).Configure(options);

            services.AddMicroServiceAServiceClient();
        }
    }
}
