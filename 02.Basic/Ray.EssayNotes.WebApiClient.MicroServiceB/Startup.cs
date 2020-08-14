using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ray.EssayNotes.WebApiClient.SDK;
using Ray.EssayNotes.WebApiClient.SDK.RayEssayNotesWebApiClientMicroServiceA;
using Ray.EssayNotes.WebApiClient.SDK.RayEssayNotesWebApiClientMicroServiceA.Interfaces;

namespace Ray.EssayNotes.WebApiClient.MicroServiceB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region 注册swagger
            services.AddSwaggerGen();
            #endregion

            #region 注册服务A的SdkApi
            services.Configure<List<MicroServiceClientHostOptions>>(Configuration.GetSection($"MicroServicesClientHost"));

            //注册A服务的接口
            string aClientName = "Ray.EssayNotes.WebApiClient.MicroServiceA";
            services.AddMicroServiceAServiceClient((options, serviceProvider) =>
            {
                List<MicroServiceClientHostOptions> list = serviceProvider.GetRequiredService<IOptionsMonitor<List<MicroServiceClientHostOptions>>>().CurrentValue;
                //options = list.First(x => x.ServiceName == aClientName);
                options.ServiceHost = "123";
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            #region swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroService B");
            });
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
