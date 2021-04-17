using BusinnesLogic.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<ITestRepository, TestRepository>();

            return services;
        }
    }
}
