using Core.IViews;
using Core.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ViewModelsExtensions
    {
        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            //services.AddTransient<IViewTest, ViewTestPresenter>();

            return services;
        }
    }
}
