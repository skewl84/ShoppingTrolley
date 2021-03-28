using Microsoft.EntityFrameworkCore;
using ShoppingTrolley.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingTrolley.Application.Common.Interfaces
{

    public interface IShoppingCartDbContext
    {
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
