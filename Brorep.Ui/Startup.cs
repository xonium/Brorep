using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Brorep.Ui.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Brorep.Common;
using Brorep.Infrastructure;
using MediatR;
using Brorep.Application.Infrastructure;
using Brorep.Application.Identity.Commands;
using System.Reflection;
using Brorep.Application.Infrastructure.AutoMapper;
using AutoMapper;
using Brorep.Persistence;
using Brorep.Domain.Entities;
using Brorep.Application.Workout.Commands;

namespace Brorep.Ui
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
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddMediatR(typeof(SubmitWorkoutCommand).GetTypeInfo().Assembly);

            services.AddDbContext<BrorepDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BrorepDatabase")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<BrorepDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, BrorepDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();
            app.UseIdentityServer();

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
