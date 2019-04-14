using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Brorep.Persistence;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Reflection;
using System.Text;
using MediatR;
using Brorep.Application.Identity.Commands;
using Brorep.Application.Infrastructure;
using Brorep.Application.Infrastructure.AutoMapper;
using Brorep.WebUI.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Brorep.WebUI.Filters;
using Microsoft.AspNetCore.Authorization;
using Brorep.Application.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Collections.Generic;
using Brorep.WebUI.Conventions;
using NSwag.SwaggerGeneration.Processors.Security;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.WebApi;
using Brorep.Domain.Entities;
using Brorep.Common;
using Brorep.Infrastructure;

namespace Brorep.WebUI
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
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddMediatR(typeof(CreateIdentityCommand).GetTypeInfo().Assembly);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Add DbContext using SQL Server Provider
            services.AddDbDependencies(Configuration.GetConnectionString("BrorepDatabase"));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("apipolicy", b =>
                {
                    b.RequireAuthenticatedUser();
                    b.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });
            });

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<BrorepDbContext>()
                .AddDefaultTokenProviders();

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "Sample API";
                c.Version = "v1";
                c.Description = "The sample API documentation description.";
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = SwaggerSecurityApiKeyLocation.Header

                }));           
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));

                c.PostProcess = document =>
                {
                    document.Info.Contact = new SwaggerContact
                    {
                        Name = "My Company",
                        Email = "info@mycompany.com",
                        Url = "https://www.mycompany.com"
                    };
                };
            });

            services.AddSingleton(typeof(ISettings), new Settings(appSettings.Secret));

            services
                .AddMvc(options => {
                    options.Conventions.Add(new AddAuthorizeFiltersControllerConvention());
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateIdentityCommandValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger(settings =>
            {
                settings.DocumentName = "Jesus";
            });
            
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
