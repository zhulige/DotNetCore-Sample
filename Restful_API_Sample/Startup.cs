using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace Restful_API_Sample
{
    /// <summary>
    /// Represents the startup process for the application.
    /// </summary>
    public class Startup
    {
        //private const string password = "zhulige";

        //public static ApiClient _apiClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The current hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current application configuration.</value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The collection of services to configure the application with.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 压缩 gzip

            services.AddResponseCompression();  //压缩 gzip

            #endregion 压缩 gzip

            #region 发现服务 Consul

            //services.AddSingleton<IHostedService, ConsulHostedService>();
            //services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
            //services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            //{
            //    var address = Configuration["consulConfig:address"];
            //    consulConfig.Address = new Uri(address);
            //}));

            #endregion 发现服务 Consul

            #region 安全 HTTPBasic

            //services
            //    .AddAuthentication("Basic")
            //    .AddBasic(o =>
            //    {
            //        o.Realm = "Password: password";

            //        o.Events = new BasicAuthenticationEvents
            //        {
            //            OnSignIn = OnSignIn
            //        };
            //    });
            #endregion 安全 HTTPBasic

            #region MVC

            //services.AddMvc();

            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

            services.AddMvc();
            services.AddApiVersioning(o => o.ReportApiVersions = true);
            services.AddSwaggerGen(
                options =>
                {
                    // resolve the IApiVersionDescriptionProvider service
                    // note: that we have to build a temporary service provider here because one has not been created yet
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    // add a swagger document for each discovered API version
                    // note: you might choose to skip or document deprecated API versions differently
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                    }

                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });


            #endregion MVC

            #region 跨域 Cors

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //});

            #endregion 跨域 Cors

            #region Swagger

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "Demo API",
            //        Description = "采用 RESTful API 2.0 标准<br/>采用 Swagger 2.0 标准<br/>采用 Basic Authentication 安全验证<br/>采用 UTF8 编码",
            //        Contact=new Contact { Name = "ZhuLige"}
            //        //License = new License { Name = "Created by ZhuLige"}
            //    });

            //    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    var xmlPath = Path.Combine(basePath, "Swagger.xml");
            //    c.IncludeXmlComments(xmlPath);
                
            //});

            #endregion Swagger

        }

        /// <summary>
        /// Configures the application using the provided builder, hosting environment, and logging factory.
        /// </summary>
        /// <param name="app">The current application builder.</param>
        /// <param name="env">The current hosting environment.</param>
        /// <param name="loggerFactory">The logging factory used for instrumentation.</param>
        /// <param name="provider">The API version descriptor provider used to enumerate defined API versions.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            //压缩 gzip
            app.UseResponseCompression();
            
            //日志
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //ApiClient
            //_apiClient = new ApiClient(Configuration);

            //HTTPBasic
            //app.UseAuthentication();

            //静态 wwwroot
            app.UseStaticFiles();

            //MVC
            app.UseMvc();

            //跨域 Cors
            //app.UseCors("AllowAll");

            //Swagger
            app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API");
            //});

            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"Sample API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
                Contact = new Contact() { Name = "ZhuLige" },
                //TermsOfService = "Shareware",
                //License = new License() { Name = "MIT", Url = "https://opensource.org/licenses/MIT" }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        //private Task OnSignIn(BasicSignInContext context)
        //{
        //    if (context.Password == password)
        //    {
        //        var claims = new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName) };
        //        var identity = new ClaimsIdentity(claims, context.Scheme.Name);
        //        context.Principal = new ClaimsPrincipal(identity);
        //    }

        //    return Task.CompletedTask;
        //}
    }
}
