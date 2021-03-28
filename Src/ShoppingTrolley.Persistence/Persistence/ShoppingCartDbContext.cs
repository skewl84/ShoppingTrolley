using Microsoft.EntityFrameworkCore;
using ShoppingTrolley.Application.Common.Interfaces;
using ShoppingTrolley.Domain.Entities;
using System;

namespace ShoppingTrolley.Infrastructure.Persistence
{
    public class ShoppingCartDbContext : DbContext, IShoppingCartDbContext
    {       
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
            //if(!isDataSeeded)
            //{
            //    SeedData();
            //    isDataSeeded = true;
            //}
        }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
