using BusinnesLogic.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class AutomapperExtensions
    {
        public static IServiceCollection ConfigureAutomapperReferences(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CityProfile));

            return services;
        }
    }
}
