using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Dna;
using Dna.AspNet;

using ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces;
using ASPNet_WPF_ChatApp.Core.Email;
using ASPNet_WPF_ChatApp.WebServer.Data;
using ASPNet_WPF_ChatApp.WebServer.Email;
using ASPNet_WPF_ChatApp.WebServer.Email.SendGrid;
using ASPNet_WPF_ChatApp.WebServer.Email.Templates;
using ASPNet_WPF_ChatApp.WebServer.DependencyInjection;

// Prevent it trying to use Microsoft.AspNetCore.Identity.IEmailSender
using IEmailSender = ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces.IEmailSender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace ASPNet_WPF_ChatApp.WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {                        
            // Add SendGrid email sender
            services.AddSendGridEmailSender();

            // Add general Email template sender
            services.AddEmailTemplateSender();

            // Add ApplicationDbContext to dependency injection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Framework.Construction.Configuration.GetConnectionString("DefaultConnection")));


            // AddIdentity() adds cookie-based authentication
            // It also adds scoped classes for things like UserManager, SignInManager, PasswordHasher, etc.
            // NOTE: Automatically adds the validated user from a cookie to the HttpContext.User.
            // https://github.com/aspnet/Identity/blob/85f8a49aef68bf9763cd9854ce1dd4a26a7c5d3c/src/Identity/IdentityServiceCollectionExtensions.cs
            services.AddIdentity<ApplicationUser, IdentityRole>()
                
                // Adds UserStore and RoleStore from this database context
                // that are consumed by the UserManager and RoleManager.
                // https://github.com/aspnet/Identity/blob/dev/src/EF/IdentityEntityFrameworkBuilderExtensions.cs
                .AddEntityFrameworkStores<ApplicationDbContext>()
                
                // Adds a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes, etc.
                .AddDefaultTokenProviders();


            // Add JWT (JSON Web Token) Authentication for API clients
            services.AddAuthentication(options =>
                { // NOTE: I had to add these three lines to fix the JWT (JSON web token) authentication not working when the Settings_ViewModel.LoadSettingsAsync() method asks the server for info using the JWT token of the user.
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate issuer
                        ValidateIssuer = true,
                        // Validate audience
                        ValidateAudience = true,
                        // Validate expiration
                        ValidateLifetime = true,
                        // Validate signature
                        ValidateIssuerSigningKey = true,

                        // Set issuer
                        ValidIssuer = Framework.Construction.Configuration["Jwt:Issuer"],
                        // Set audience
                        ValidAudience = Framework.Construction.Configuration["Jwt:Audience"],

                        // Set signing key
                        IssuerSigningKey = new SymmetricSecurityKey(
                            // Get our secret key from configuration
                            Encoding.UTF8.GetBytes(Framework.Construction.Configuration["Jwt:SecretKey"])),
                    };
                });
                


            // Change password policy
            services.Configure<IdentityOptions>(options =>
            {
                // Make really weak password possible (for now).
                // NEVER do this in production code!!!
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // Make sure users have unique emails
                options.User.RequireUniqueEmail = true;
            });


            // Alter application cookie info
            services.ConfigureApplicationCookie(options =>
            {
                // Redirect to /login
                options.LoginPath = "/login";

                // Change cookie timeout
                options.ExpireTimeSpan = TimeSpan.FromSeconds(1500);
            });


            services.AddMvc(options =>
            {
                // These two lines would cause the server to use XML serialization instead of JSON serialization.
                //options.InputFormatters.Add(new XmlSerializerInputFormatter());
                //options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            // Use Dna Framework
            app.UseDnaFramework();

            app.UseAuthentication();

            // If in development...
            if (env.IsDevelopment())
                // Show any exceptions in browser when they crash
                app.UseDeveloperExceptionPage();
            // Otherwise...
            else
                // Just show generic error page
                app.UseExceptionHandler("/Home/Error");

            // Serve static files
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Setup MVC routes
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{moreInfo?}");

                // NOTE: This route should possibly actually go in the AboutController later.
                routes.MapControllerRoute(
                    name: "aboutPage",
                    pattern: "more",
                    defaults: new { controller = "About", action = "TellMeMore" });
            });

        }
    }
}