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
using Microsoft.OpenApi.Models;
using Autofac;
using Winc.Api.Helpers;
using Winc.Library.DbConfigurations;

namespace Winc.Api
{
    public class Startup
    {
        protected string _dllPrefix;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _dllPrefix = "Winc";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureSwagger(services);

            ConfigureDi(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("api", "api/{controller}/{id?}");
            });

            ConfigureSwaggerUi(app);
        }




        protected virtual void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Winc API", Version = "v1" });
            });
        }

        protected virtual void ConfigureSwaggerUi(IApplicationBuilder app)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

        }


        protected virtual IContainer ConfigureDi(IServiceCollection services)
        {
            //Responsible for IhttpContext to be injected through IHttpContextAccessor interface
            services.AddHttpContextAccessor();

            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            var container = DiHelper.Configure(services, _dllPrefix, true);

            return container;
        }
    }
}
