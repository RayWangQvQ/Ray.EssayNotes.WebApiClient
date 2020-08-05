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
        public static void AddRayClientApi<TInterface>(this IServiceCollection services, string clientName) where TInterface : class, IHttpApi
        {
            services.AddHttpApi<TInterface>((o, serviceProvider) =>
            {
                o.UseParameterPropertyValidate = true;
                o.UseReturnValuePropertyValidate = false;
                o.KeyValueSerializeOptions.IgnoreNullValues = true;

                var hostUrl = serviceProvider.GetRequiredService<IOptionsMonitor<MicroServicesClientHostOption>>()
                .Get(clientName)
                .ServiceHost;
                o.HttpHost = new Uri(hostUrl);
            });
        }
    }
}
