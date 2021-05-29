using System;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            var serviceProvider =
                new ServiceCollection()
                    .ConfigureRepository()
                    .ConfigureAutomapperReferences()
                    .ConfigureServices()
                    .ConfigureViewModels()
                    .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }

        public static T GetService<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }
}
