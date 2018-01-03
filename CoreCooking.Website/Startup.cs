using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CoreCooking.Website
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //services.AddAuthentication("MyCookieAuthenticationScheme").AddCookie(options => 
            //    {
            //        options.AccessDeniedPath = "/accounts/login";
            //        options.LoginPath = "/accounts/login";
            //    });
            services.AddAuthentication("MyCookieAuthenticationScheme")
                .AddCookie("MyCookieAuthenticationScheme", options =>
                {
                    options.AccessDeniedPath = "/accounts/login";
                    options.LoginPath = "/accounts/login";
                });

            var appSettings = Configuration.GetSection("Settings");
            services.Configure<Settings>(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //
                // /Test
                //
                routes.MapRoute(
                    name: "test",
                    template: "Test",
                    defaults: new { controller = "Test", action = "Index" }
                    );

                //
                // /Image
                //
                routes.MapRoute(
                    name: "images",
                    template: "Images/UploadFile/{file?}",
                    defaults: new { controller = "Images", action = "UploadFile" }
                    );

                //
                // /Accounts
                //
                routes.MapRoute(
                    name: "accounts",
                    template: "Accounts/{action}/{returnUrl?}",
                    defaults: new { controller = "Accounts", action = "Login" }
                    );



                //
                // Recipes
                // 

                // /AddRecipe
                routes.MapRoute(
                    name: "recipesAdd",
                    template: "AddRecipe",
                    defaults: new { controller = "Recipes", action = "Add" }
                    );

                // /Beef/AddRecipe
                routes.MapRoute(
                    name: "hashtagRecipesAdd",
                    template: "{hashtag}/AddRecipe",
                    defaults: new { controller = "Recipes", action = "Add" }
                    );


                // /Beef/Edit
                routes.MapRoute(
                    name: "recipesEdit",
                    template: "{hashtag}/Edit",
                    defaults: new { controller = "Recipes", action = "Edit" }
                    );

                // /Beef/Honey_Chicken
                routes.MapRoute(
                    name: "recipes",
                    template: "{hashtag}/{name}/{action?}",
                    defaults: new { controller = "Recipes", action = "Details" }
                    );

                // localhost/Beef
                routes.MapRoute(
                    name: "hashtags",
                    template: "{name}/{action?}",
                    defaults: new { controller = "Hashtags", action = "Details" }
                    );

                // localhost/
                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}");


            });
        }
    }
}
