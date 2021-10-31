using Common;
using Data;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configuration;
using WebFramework.MiddleWare;

namespace MyAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings siteSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            siteSettings = configuration.GetSection("SiteSettings").Get<SiteSettings>();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddMvc(options=> {

                options.Filters.Add(new AuthorizeFilter());

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddElmah<SqlErrorLog>(option=>
            {
                option.Path = siteSettings.ElmahPath;
                option.ConnectionString = Configuration.GetConnectionString("SqlServer");
            });
            services.AddJwtAuthentication(siteSettings.JwtSettings);
            services.AddCustomIdentity(siteSettings.IdentitySettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.CustomExceptionHandler();
 
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseElmah();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();


        }
    }
}
