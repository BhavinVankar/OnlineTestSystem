using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.DataAccess.Repository;
using OnlineTestSystem.Services.Abstraction;
using OnlineTestSystem.Services.Repository;
using System.Text;
using OnlineTestSystem.Models.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OnlineTestSystem.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using OnlineTestSystem.Services.AutoMapperConfiguration;
using OnlineTestSystem.Services;
using Microsoft.Extensions.Configuration;

namespace OnlineTestSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "Token"; // Set the cookie name
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.LoginPath = "/Account/SignIn"; // Set the login path
                options.SlidingExpiration = true; // Enable sliding expiration
            });

            //string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            services.Configure<RepositoryOptions>(Configuration.GetSection("ConnectionStrings"));

            services.AddServiceLayer(Configuration);

            services.AddMvc(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute { NoStore = true, Location = ResponseCacheLocation.None });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                pattern: "{controller=Account}/{action=SignIn}/{id?}");
            });
        }
    }
}
