using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ray.EssayNotes.WebApiClient.SDK;
using WebApiClientCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RaySdkExtension
    {
        /// <summary>
        /// 注册一个ClientApi
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="services"></param>
        public static void AddRayClientApi<TInterface>(this IServiceCollection services, string clienName)
            where TInterface : class, IHttpApi
        {
            services.AddHttpApi<TInterface>((o, serviceProvider) =>
            {
                o.UseParameterPropertyValidate = true;
                o.UseReturnValuePropertyValidate = false;
                o.KeyValueSerializeOptions.IgnoreNullValues = true;

                var hostUrl = serviceProvider.GetRequiredService<IOptionsMonitor<MicroServicesClientHostOption>>()
                .Get(clienName)
                .ServiceHost;
                o.HttpHost = new Uri(hostUrl);
            });
        }

        /// <summary>
        /// 注册一个ClientApi
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        public static void AddRayClientApi<TInterface>(this IServiceCollection services, string clienName, Action<MicroServicesClientHostOption> configureOptions)
            where TInterface : class, IHttpApi
        {
            services.AddOptions<MicroServicesClientHostOption>(GetOptionsName<TInterface>())
                .Configure(configureOptions);

            services.AddRayClientApi<TInterface>(clienName);
        }

        public static void AddRayClientApi<TInterface>(this IServiceCollection services, string clienName, Action<MicroServicesClientHostOption, IServiceProvider> configureOptions)
            where TInterface : class, IHttpApi
        {
            services.AddOptions<MicroServicesClientHostOption>(GetOptionsName<TInterface>())
                .Configure(configureOptions);
            services.AddRayClientApi<TInterface>(clienName);
        }

        private static string GetOptionsName<TInterface>()
        {
            return typeof(TInterface).FullName;
        }
    }
}
