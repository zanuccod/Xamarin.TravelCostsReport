using BusinnesLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();

            return services;
        }
    }
}
