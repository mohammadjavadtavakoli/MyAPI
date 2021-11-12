using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Data.Repositories;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddService(this ContainerBuilder containerBuilder)
        {

            //containerBuilder.RegisterType<JwtService>().As<IJwtService>();
            //containerBuilder.RegisterType<IUserRepository>().As<UserRepository>();

            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var commonAsembly = typeof(SiteSettings).Assembly;
            var entitiesAsembly = typeof(IEntity).Assembly;
            var dataAsembly = typeof(ApplicationDbContext).Assembly;
            var serviceAsembly = typeof(JwtService).Assembly;

            containerBuilder.RegisterAssemblyTypes(commonAsembly, entitiesAsembly, dataAsembly, serviceAsembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAsembly, entitiesAsembly, dataAsembly, serviceAsembly)
               .AssignableTo<ITransientDependency>()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAsembly, entitiesAsembly, dataAsembly, serviceAsembly)
               .AssignableTo<ISingletonDependency>()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
        public static IServiceProvider BuildAutoFacServiceProvider(this IServiceCollection service)
        {
            var ContainerBuilder = new ContainerBuilder();
            ContainerBuilder.Populate(service);

            //Register Service To AutoFac ContainerBuilder
            ContainerBuilder.AddService();


            var container = ContainerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
