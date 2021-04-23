using BusinnesLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();

            return services;
        }
    }
}
