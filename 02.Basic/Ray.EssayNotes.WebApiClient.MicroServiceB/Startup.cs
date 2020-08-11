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

            #region ע��swagger
            services.AddSwaggerGen();
            #endregion

            #region ע�����A��SdkApi
            //ע��A����
            string aClientName = "Ray.EssayNotes.WebApiClient.MicroServiceA";
            services.AddOptions()
                .Configure<MicroServicesClientHostOption>(aClientName, Configuration.GetSection($"MicroServicesClientHost:{aClientName}"));
            services.AddMicroServiceAServiceClient();
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
