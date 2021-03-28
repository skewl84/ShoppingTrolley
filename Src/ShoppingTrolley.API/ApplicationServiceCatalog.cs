using ShoppingTrolley.Application;
using ShoppingTrolley.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingTrolley.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationServiceCatalog
    {
        /// <summary>
        /// Single area for injecting external user services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationLayers(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence();

            return services;
        }
    }
}
