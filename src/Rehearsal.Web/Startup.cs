using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace Rehearsal.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.Assembly("Rehearsal.WebApi");
                    _.AddAllTypesOf<Profile>();
                });

                config.Populate(services);

                config.For<IMapper>().Use("Build automapper using config", CreateAutomapper);
            });

            return container.GetInstance<IServiceProvider>();
        }

        private IMapper CreateAutomapper(IContext context)
        {
            var profiles = context.GetAllInstances<Profile>();
            var config = new MapperConfiguration(x =>
            {
                foreach (var profile in profiles)
                    x.AddProfile(profile);
            });

            return config.CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
