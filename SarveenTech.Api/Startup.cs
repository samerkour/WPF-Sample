using AutoWrapper;
using HealthChecks.UI.Client;
using Hs.CrossCutting;
using Hs.Infrastructure;
using Hs.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using IdentityServer4.Shared.Authentication;
using IdentityServer4.Shared.Configuration;
//using AuditLogging.EntityFramework.Entities;
using IdentityServer4.Shared.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SarveenTech.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var adminApiConfiguration = Configuration.GetSection(nameof(AdminApiConfiguration)).Get<AdminApiConfiguration>();
            services.AddSingleton(adminApiConfiguration);


            //SQL Server 
            services.AddEntityFrameworkSqlServer()
             .AddDbContext<SampleDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")))
             .AddUnitOfWork<SampleDbContext>(); // Note the App prefix 

            //PostgreSQL 
            //services.AddEntityFrameworkSqlServer().AddDbContext<SmartCattleContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")))
            //.AddUnitOfWork<SmartCattleContext>(); // Note the App prefix 

            //enabling in memory caching
            //http://www.binaryintellect.net/articles/a7d9edfd-1f86-45f8-a668-64cc86d8e248.aspx
            services.AddMemoryCache();

            services.AddOptions();
            //var rabbitMqConfiguration = Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
            //services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            //services.AddTransient(typeof(IRabittMqSender<>), typeof(RabittMqSender<>));



            services.AddSignalR();
           


            //Refere to Tutorial https://www.talkingdotnet.com/3-ways-to-use-httpclientfactory-in-asp-net-core-2-1/
            services.AddHttpClient();
            services.AddHttpClient("BackendService", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("BackendService:BaseAddress"));
                //c.DefaultRequestHeaders.Add("Authorization", "bearer " + Configuration.GetSection("appSettings").GetSection("AccessToken").Value);
                //c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                //c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                //c.DefaultRequestHeaders.Add("Content-Type", "application/form-data");
            });

            //**************************************
            // TODO
            // Add DbContexts
            //RegisterDbContexts(services);

            //**************************************
            // TODO
            //services.AddDataProtection()
            //    .SetApplicationName("IdentityServer4")
            //    .PersistKeysToDbContext<IdentityServerDataProtectionDbContext>();

            // Add email senders which is currently setup for SendGrid and SMTP
            services.AddEmailSenders(Configuration);

            //services.AddScoped<ControllerExceptionFilterAttribute>();

            //**************************************
            // TODO
            // Add authentication services
            //RegisterAuthentication(services);

            // Add authorization services
            //RegisterAuthorization(services);

            //var profileTypes = new HashSet<Type>
            //{
            //    typeof(IdentityMapperProfile<IdentityRoleDto, IdentityUserRolesDto, string, IdentityUserClaimsDto, IdentityUserClaimDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto, IdentityRoleClaimDto, IdentityRoleClaimsDto>)
            //};

            //services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext,
            //    IdentityUserDto, IdentityRoleDto, UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
            //    UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
            //    IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
            //    IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
            //    IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(profileTypes);

            //services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();

            //**************************************
            // TODO
            //services.AddAdminApiCors(adminApiConfiguration);

            //services.AddMvcServices<IdentityUserDto, IdentityRoleDto,
            //    UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
            //    UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
            //    IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
            //    IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
            //    IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>();




            services.AddHttpContextAccessor();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllers().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });


            //Adding URL based versioning 
            //for another type of versioning refere to https://www.c-sharpcorner.com/article/api-versioning-in-asp-net-core-web-api/
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                //x.ApiVersionReader = ApiVersionReader.Combine(
                //new QueryStringApiVersionReader("v"),
                //new HeaderApiVersionReader("v"), 
                //new UrlSegmentApiVersionReader(), 
                //new HeaderApiVersionReader("api-version"));
                x.ApiVersionReader = new UrlSegmentApiVersionReader();
            });


            services.AddSwaggerGen(options =>
            {

                foreach (var apiVersion in adminApiConfiguration.ApiVersions)
                {
                    options.SwaggerDoc(
                           apiVersion.Version,
                           new OpenApiInfo
                           {
                               Title = adminApiConfiguration.ApiName,
                               Version = apiVersion.Version,
                               Description = apiVersion.Description,
                               //,TermsOfService = apiVersion.TermsOfService 
                               Contact = new OpenApiContact
                               {
                                   Name = "Sarveen Technology",
                                   Email = "koursamer@gmail.com",
                                   //Url = new Uri("http://sarveentech.ir/"),
                               },
                               License = new OpenApiLicense
                               {
                                   Name = "Smart Cattle API Backend",
                                   //Url = new Uri("http://sarveentech.ir/license"),
                               }
                           }
                    );

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                }

                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                options.EnableAnnotations();
                options.DocInclusionPredicate((version, desc) =>
                {
                    var versions = desc.CustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = desc.CustomAttributes()
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                });


                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token"),
                            Scopes = new Dictionary<string, string> {
                                { adminApiConfiguration.OidcApiName, adminApiConfiguration.ApiName }
                            }
                        }
                    }
                });


            });

            //**************************************
            // TODO
            //services.AddAuditEventLogging<AdminAuditLogDbContext, AuditLog>(Configuration);
            //services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>(Configuration, adminApiConfiguration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AdminApiConfiguration adminApiConfiguration, IMemoryCache cache)
        {
            //**************************************
            // TODO
            //app.AddForwardHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint(
                //$"{adminApiConfiguration.ApiBaseUrl}/swagger/v1/swagger.json", adminApiConfiguration.ApiName);
                foreach (var apiVersion in adminApiConfiguration.ApiVersions)
                {
                    c.SwaggerEndpoint(
                        $"{adminApiConfiguration.ApiBaseUrl}/swagger/{apiVersion.Version}/swagger.json", $"{apiVersion.Version}"
                        );
                }


                c.OAuthClientId(adminApiConfiguration.OidcSwaggerUIClientId);
                c.OAuthAppName(adminApiConfiguration.ApiName);
                c.OAuthUsePkce();
            });


            var supportedCultures = new[] { "en-US", "fa-IR" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);



            //Refere to https://vmsdurano.com/autowrapper-prettify-your-asp-net-core-apis-with-meaningful-responses/
            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    ShowApiVersion = true,
                    ShowStatusCode = true,
                    IsApiOnly = false
                });

            app.UseRouting();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            UseAuthentication(app);
            app.UseCors();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapHealthChecks("/health", new HealthCheckOptions
                //{
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
            });
        }


        public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = new OpenApiPaths();
                foreach (var path in swaggerDoc.Paths)
                {
                    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
                }
                swaggerDoc.Paths = paths;
            }
        }

        //**************************************
        // TODO
        //public virtual void RegisterDbContexts(IServiceCollection services)
        //{
        //    services.AddDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>(Configuration);
        //}

        //public virtual void RegisterAuthentication(IServiceCollection services)
        //{
        //    services.AddApiAuthentication<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
        //}

        //public virtual void RegisterAuthorization(IServiceCollection services)
        //{
        //    services.AddAuthorizationPolicies();
        //}

        public virtual void UseAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
