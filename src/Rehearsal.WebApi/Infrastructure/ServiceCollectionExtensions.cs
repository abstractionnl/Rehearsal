using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Rehearsal.WebApi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupWebApi(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(new ValidateActionParametersAttribute());
                    options.Filters.Add(new ValidateModelStateFilter());
                })
                .AddFluentValidation();
        }

        public static void UseWebApi(this IApplicationBuilder app)
        {
            var jwtOptions = app.ApplicationServices.GetService<IOptions<JwtOptions>>().Value;
            
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.SigningCredentials.Key,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                }
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}