using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Moon.AspNetCore.Authentication.Basic;
using System.Security.Claims;

namespace Restful_API_Sample
{
    public class Startup
    {
        private const string password = "zhulige";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                })
                .AddAuthorization();

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Demo API",
                    Description = "RESTful API for Demo"
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Swagger.xml");
                c.IncludeXmlComments(xmlPath);
                
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API");
            });

            app.UseBasicAuthentication(new BasicAuthenticationOptions
            {
                Realm = $"Password: {password}",
                Events = new BasicAuthenticationEvents
                {
                    OnSignIn = c =>
                    {
                        if (c.Password == password)
                        {
                            var claims = new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, c.UserName) };
                            var identity = new ClaimsIdentity(claims, c.Options.AuthenticationScheme);
                            c.Principal = new ClaimsPrincipal(identity);
                        }

                        return Task.FromResult(true);
                    }
                }
            });

            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
