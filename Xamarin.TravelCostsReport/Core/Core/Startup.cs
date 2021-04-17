using System;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

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
                    .ConfigureServices()
                    .ConfigureViewModels()
                    .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }
}
