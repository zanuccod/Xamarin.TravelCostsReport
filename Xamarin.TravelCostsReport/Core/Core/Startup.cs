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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

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
    }
}
