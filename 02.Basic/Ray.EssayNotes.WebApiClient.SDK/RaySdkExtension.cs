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

                IOptionsMonitor<MicroServiceClientHostOptions> s = serviceProvider.GetRequiredService<IOptionsMonitor<MicroServiceClientHostOptions>>();
                var hostUrl = s
                .Get(clienName)
                .ServiceHost;
                o.HttpHost = new Uri(hostUrl);
            });
        }
    }
}
