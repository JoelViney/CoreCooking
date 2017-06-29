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

            // Add cookie authentication.
            // https://github.com/leastprivilege/AspNetCoreSecuritySamples/tree/master/Cookies
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,

                LoginPath = new PathString("/accounts/login")
            });

            app.UseClaimsTransformation(context =>
            {
                if (context.Principal.Identity.IsAuthenticated)
                {
                    context.Principal.Identities.First().AddClaim(new Claim("now", DateTime.Now.ToString()));
                }

                return Task.FromResult(context.Principal);
            });

            app.UseMvc(routes =>
            {
                //
                // /Image
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

                // This needs to be before
                routes.MapRoute(
                    name: "recipesEditOnAdd",
                    template: "Recipes/Edit",
                    defaults: new { controller = "Recipes", action = "Edit" }
                    );

                //
                // Categories
                // 
                // /AddCategory
                routes.MapRoute(
                    name: "categoriesAdd",
                    template: "AddCategory",
                    defaults: new { controller = "Categories", action = "Add" }
                    );

                // /Beef/Edit
                routes.MapRoute(
                    name: "categoriesEdit",
                    template: "{name}/Edit",
                    defaults: new { controller = "Categories", action = "Edit" }
                    );

                // /Beef/Delete
                routes.MapRoute(
                    name: "categoriesDelete",
                    template: "{name}/Delete",
                    defaults: new { controller = "Categories", action = "Delete" }
                    );


                //
                // Recipes
                // 
                // /Beef/AddRecipe
                routes.MapRoute(
                    name: "recipesAdd",
                    template: "{categoryName}/AddRecipe",
                    defaults: new { controller = "Recipes", action = "Add" }
                    );

                // /Beef/Edit
                routes.MapRoute(
                    name: "recipesEdit",
                    template: "{categoryName}/Edit",
                    defaults: new { controller = "Recipes", action = "Edit" }
                    );

                // /Beef/Honey_Chicken
                routes.MapRoute(
                    name: "recipes",
                    template: "{categoryName}/{name}/{action?}",
                    defaults: new { controller = "Recipes", action = "Details" }
                    );

                // localhost/Beef
                routes.MapRoute(
                    name: "categories",
                    template: "{name}/{action?}",
                    defaults: new { controller = "Categories", action = "Details" }
                    );

                // localhost/
                routes.MapRoute(
                        name: "default",
                        template: "{controller=Categories}/{action=Index}");


            });
        }
    }
}
