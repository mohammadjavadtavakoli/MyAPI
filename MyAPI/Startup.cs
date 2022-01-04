using AutoMapper;
using Common;
using Data;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities;
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
using MyAPI.Models;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
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


            AutoMapperConfiguration.InitializeAutoMapper();

            #region old Code AutoMapper
            //Mapper.Initialize(config =>
            //{
            //    config.CreateMap<Post, PostDto>().ReverseMap()
            //    .ForMember(p => p.Author, opt => opt.Ignore())
            //    .ForMember(p => p.Category, opt => opt.Ignore());
            //});
            #endregion

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));

            services.AddMinimalMVC();


            services.AddCustomIdentity(siteSettings.IdentitySettings);

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });


            //IOC Continer .Net Core 
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IJwtService, JwtService>();


            //services.AddElmah<SqlErrorLog>(option=>
            //{
            //    option.Path = siteSettings.ElmahPath;
            //    option.ConnectionString = Configuration.GetConnectionString("SqlServer");
            //});

            services.AddJwtAuthentication(siteSettings.JwtSettings);

            services.AddCustomApiVersioning();

            return services.BuildAutoFacServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.CustomExceptionHandler();

            app.UseHsts(env);

            //app.UseElmah();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();


        }
    }
}
