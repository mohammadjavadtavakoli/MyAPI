using Autofac;
using Autofac.Extensions.DependencyInjection;
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

            containerBuilder.RegisterType<JwtService>().As<IJwtService>();
            containerBuilder.RegisterType<IUserRepository>().As<UserRepository>();
            containerBuilder.RegisterGeneric(typeof(IRepository<>)).As(typeof(Repository<>));
        }
        public static IServiceProvider BuildAutoFacServiceProvider(this IServiceCollection service)
        {
            var ContainerBuilder = new ContainerBuilder();
            ContainerBuilder.Populate(service);

            var container = ContainerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
