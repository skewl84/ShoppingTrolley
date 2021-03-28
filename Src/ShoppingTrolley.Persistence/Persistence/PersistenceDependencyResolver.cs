using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingTrolley.Application.Common.Interfaces;

namespace ShoppingTrolley.Infrastructure.Persistence
{
    public static class PersistenceDependencyResolver
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // Use In-Memory Database for simplicity here, can avoid standing up a db somewhere.
            // Specify a constant name, so we get the same db instance across scoped lifetimes.

            services.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseInMemoryDatabase("ShoppingCartTestDatabase"));

            services.AddScoped<IShoppingCartDbContext>(provider => provider.GetService<ShoppingCartDbContext>());

            return services;
        }
    }
}
