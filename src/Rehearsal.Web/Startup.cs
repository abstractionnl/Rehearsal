﻿using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Rehearsal.Data.Infrastructure;
using Rehearsal.WebApi.Infrastructure;
using StructureMap;

namespace Rehearsal.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<DatabaseOptions>(Configuration.GetSection("data"));

            var jwtConfigurationSection = Configuration.GetSection("jwt");
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtConfigurationSection[nameof(JwtOptions.Issuer)];
                options.Audience = jwtConfigurationSection[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

                if (!string.IsNullOrEmpty(jwtConfigurationSection[nameof(JwtOptions.ValidFor)]))
                    options.ValidFor = TimeSpan.Parse(jwtConfigurationSection[nameof(JwtOptions.ValidFor)]);
            });

            services.SetupWebApi();

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssembliesFromApplicationBaseDirectory();
                    _.LookForRegistries();
                });

                config.Populate(services);
                config.For<StartupService>();
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(LogLevel.Debug, includeScopes: true);
                app.UseDeveloperExceptionPage();
            }

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
            app.UseWebApi();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
