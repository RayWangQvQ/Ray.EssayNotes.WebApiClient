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
        /// <param name="clientName"></param>
        public static void AddRayClientApi<TInterface>(this IServiceCollection services, IConfiguration configuration, string clientName, MicroServicesClientHostOption clientHostOption) where TInterface : class, IHttpApi
        {
            var configSection = configuration.GetSection($"MicroServicesClientHost:{clientName}");
            services.Configure<MicroServicesClientHostOption>(clientName, configSection);

            services.AddHttpApi<TInterface>((o, serviceProvider) =>
            {
                o.UseParameterPropertyValidate = true;
                o.UseReturnValuePropertyValidate = false;
                o.KeyValueSerializeOptions.IgnoreNullValues = true;
                /*
                var hostUrl = serviceProvider.GetRequiredService<IOptionsMonitor<MicroServicesClientHostOption>>()
                .Get(clientName)
                .ServiceHost;
                */
                o.HttpHost = new Uri(clientHostOption.ServiceHost);
            });
        }

        public static void AddRayClientApi<TInterface>(this IServiceCollection services, Action<MicroServicesClientHostOption, IServiceProvider> configureOptions)
            where TInterface : class, IHttpApi
        {
            services.AddHttpApi<TInterface>((o, serviceProvider) =>
            {
                o.UseParameterPropertyValidate = true;
                o.UseReturnValuePropertyValidate = false;
                o.KeyValueSerializeOptions.IgnoreNullValues = true;
                /*
                var hostUrl = serviceProvider.GetRequiredService<IOptionsMonitor<MicroServicesClientHostOption>>()
                .Get(clientName)
                .ServiceHost;
                */
                o.HttpHost = new Uri(clientHostOption.ServiceHost);
            });
        }

        public static IHttpClientBuilder AddHttpApi1<THttpApi>(this IServiceCollection services, Action<HttpApiOptions, IServiceProvider> configureOptions) where THttpApi : class
        {
            string fullName = typeof(THttpApi).FullName;
            services.AddOptions<HttpApiOptions>(fullName).Configure(configureOptions);
            return services.AddHttpApi<THttpApi>();
        }

        public static IHttpClientBuilder AddHttpApi1<THttpApi>(this IServiceCollection services) where THttpApi : class
        {
            services.AddOptions();
            services.AddMemoryCache();
            //services.TryAddSingleton<IXmlSerializer, XmlSerializer>();
            //services.TryAddSingleton<IJsonSerializer, JsonSerializer>();
            //services.TryAddSingleton<IKeyValueSerializer, KeyValueSerializer>();
            //services.TryAddSingleton<IResponseCacheProvider, ResponseCacheProvider>();
            return services.AddHttpClient(typeof(THttpApi).FullName)
                .AddTypedClient((httpClient, serviceProvider) =>
                {
                    string fullName = typeof(THttpApi).FullName;
                    HttpApiOptions httpApiOptions = serviceProvider.GetRequiredService<IOptionsMonitor<HttpApiOptions>>().Get(fullName);
                    return HttpApi.Create<THttpApi>(httpClient, serviceProvider, httpApiOptions);
                });
        }
    }
}
