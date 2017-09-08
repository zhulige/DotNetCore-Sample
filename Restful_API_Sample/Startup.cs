using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Moon.AspNetCore.Authentication.Basic;
using Restful_API_Sample.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restful_API_Sample
{
    public class Startup
    {
        private const string password = "zhulige";

        public static ApiClient _apiClient;

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
            #region 压缩 gzip

            services.AddResponseCompression();  //压缩 gzip

            #endregion 压缩 gzip

            #region 发现服务 Consul

            services.AddSingleton<IHostedService, ConsulHostedService>();
            services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = Configuration["consulConfig:address"];
                consulConfig.Address = new Uri(address);
            }));

            #endregion 发现服务 Consul

            #region 安全 HTTPBasic

            services
                .AddAuthentication("Basic")
                .AddBasic(o =>
                {
                    o.Realm = "Password: password";

                    o.Events = new BasicAuthenticationEvents
                    {
                        OnSignIn = OnSignIn
                    };
                });
            #endregion 安全 HTTPBasic

            #region MVC

            services.AddMvc();

            #endregion MVC

            #region 跨域 Cors

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            #endregion 跨域 Cors

            #region Swagger

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

            #endregion Swagger

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //压缩 gzip
            app.UseResponseCompression();
            
            //日志
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //ApiClient
            _apiClient = new ApiClient(Configuration);
            
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API");
            });

            //HTTPBasic
            app.UseAuthentication();

            //静态 wwwroot
            app.UseStaticFiles();

            //MVC
            app.UseMvc();

            //跨域 Cors
            app.UseCors("AllowAll");
        }


        private Task OnSignIn(BasicSignInContext context)
        {
            if (context.Password == password)
            {
                var claims = new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName) };
                var identity = new ClaimsIdentity(claims, context.Scheme.Name);
                context.Principal = new ClaimsPrincipal(identity);
            }

            return Task.CompletedTask;
        }
    }
}
