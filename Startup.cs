using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TourMarket.Context;
using TourMarket.Helpers;
using TourMarket.Services;

namespace TourMarket
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Startup
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TourMarket API",
                    Description = "ASP.NET Core Web API for TourMarket"
                });
                var filePath = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, filePath));
            });

            services.AddDbContext<MarketContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MarketDb")));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var mailSettingsSection = Configuration.GetSection("EmailConfiguration");

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<EmailConfiguration>(mailSettingsSection);
            services.AddSingleton(mailSettingsSection.Get<EmailConfiguration>());
           
            var key = Encoding.ASCII.GetBytes(appSettingsSection.Get<AppSettings>().Secret);

            services.AddMyAuthentication(key);
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddTransient<DbContext, MarketContext>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<TourRepository>();
            services.AddScoped<TouristsRepository>();            
            
            services.AddScoped<IUserService, ManagerService>();
            services.AddScoped<OrderRepository>();                 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();            
           
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "DefaultAPI",
                    template: "api/{controller}/{action}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TourMarket API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
