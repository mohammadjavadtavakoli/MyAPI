using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data.Repositories;
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

            containerBuilder.RegisterGeneric(typeof(IRepository<>)).As(typeof(Repository<>));

            var commonAsembly = typeof(SiteSettings).Assembly;
            var entitiesAsembly = typeof(SiteSettings).Assembly;
            var dataAsembly = typeof(SiteSettings).Assembly;
            var serviceAsembly = typeof(SiteSettings).Assembly;

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
