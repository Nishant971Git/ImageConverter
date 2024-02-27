using ImageConverter.Interface;
using ImageConverter.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using System.Configuration;

namespace ImageConverter
{
    public class Startup
    {


        public void ConfigureServices(IServiceCollection services)
        {
            object p = services.AddControllersWithViews();

            services.AddMvc(o => o.EnableEndpointRouting = false);

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            

            services.AddScoped<IUpload, UploadService>();

            

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                //options.AllowSynchronousIO = true;
                options.AutomaticAuthentication = false;

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(o => {
                   o.LoginPath = new PathString("/login");
                   o.AccessDeniedPath = new PathString("/unauthorized-access");
                   o.LogoutPath = new PathString("/logout");
               });


           
        }

        public void Configure(IApplicationBuilder app)
        {

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/clear-image-maker";
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "areas",
                        template: "{area:exists}/{controller=home}/{action=ImageConverter}/{id?}");


                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=ImageConverter}/{id?}");
            });
        }
    }
}
