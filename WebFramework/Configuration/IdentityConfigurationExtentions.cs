using Common;
using Data;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtentions
    {

        public static void AddElmah(this IServiceCollection services , SiteSettings siteSettings , IConfiguration configuration )
        {
            services.AddElmah<SqlErrorLog>(option =>
            {
                option.Path = siteSettings.ElmahPath;
                option.ConnectionString = configuration.GetConnectionString("SqlServer");
            });
        }
        public static void AddMinimalMVC(this IServiceCollection services)
        {

            //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AuthorizeFilter());

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            })
            .AddApiExplorer()
            .AddAuthorization()
            .AddFormatterMappings()
            .AddDataAnnotations()
            .AddJsonFormatters(/*options =>
            {
                options.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/)
            .AddCors()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add All MVC Services 
            //services.AddMvc(options => {

            //    options.Filters.Add(new AuthorizeFilter());

            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        public static void AddDbContext(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
        }
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(identityOptions =>
             {
                 //Password Settings
                 identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                 identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                 identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                 identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                 identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                 //UserName Settings
                 identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;

                 //Singin Settings
                 //identityOptions.SignIn.RequireConfirmedEmail = false;
                 //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                 //Lockout Settings
                 //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                 //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 //identityOptions.Lockout.AllowedForNewUsers = false;
             }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

    }
}
