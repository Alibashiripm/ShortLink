using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortLink.Infra.Data.Context;
using ShortLink.Infra.IoC;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ShortLink.Web.Middelware;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace ShortLink.Web
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            #region IoC
            RegisterService(services);
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] {
                UnicodeRanges.BasicLatin,
                UnicodeRanges.Arabic
            }));
            #endregion

            #region db context
            services.AddDbContext<ShortLinkContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ShortLinkSqlConnection"));
            });
            #endregion

            #region authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/log-Out";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });
            #endregion

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseShortLinkUrlRedirect();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/admin"))
                {
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        context.Response.Redirect("/login");
                    }
                    else if (!bool.Parse(context.User.FindFirstValue("IsAdmin")))
                    {
                        context.Response.Redirect("/login");
                    }
                }


                await next.Invoke();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToController("Index", "Home");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
        #region IoC
        public static void RegisterService(IServiceCollection services)
        {
            DependencyContainer.RegisterService(services);
        }
        #endregion
    }
}
