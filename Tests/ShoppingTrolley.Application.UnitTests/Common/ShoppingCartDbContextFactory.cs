using ShoppingTrolley.Domain.Entities;
using ShoppingTrolley.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using ShoppingTrolley.Domain.Enums;
using ShoppingTrolley.Domain.Entities.Promotion;

namespace ShoppingTrolley.Application.UnitTests.Common
{
    public class ShoppingCartDbContextFactory
    {
        public static ShoppingCartDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShoppingCartDbContext(options);

            context.Database.EnsureCreated();


            #region Add Products

            var product1 = new Product
            {
                ProductId = 1,
                ProductName = "Victoria Bitter",
                ProductSalePrice = 21.49,
                ProductPromotion = new ProductPromotion { ProductPromotionDefinition = ProductPromotionDefinition.TwoDollarsOff, PromotionName = "$2.00 off" }
            };
            var product2 = new Product { ProductId = 2, ProductName = "Crown Lager", ProductSalePrice = 22.99 };
            var product3 = new Product { ProductId = 3, ProductName = "Coopers", ProductSalePrice = 20.49 };
            var product4 = new Product { ProductId = 4, ProductName = "Tooheys Extra Dry", ProductSalePrice = 19.99 };

            context.Products.AddRange(new[]
            {
                product1, product2, product3, product4
            });
            #endregion

            #region Setup empty cart customer
            Guid testCustomerIdEmptyCart = new Guid(TestConstants.CustomerIdEmptyCart);
            var customerWithEmptyCart = new Customer
            {
                CustomerId = testCustomerIdEmptyCart,
                CustomerFirstName = "Test",
                CustomerLastName = "User"
            };
            context.Customers.Add(customerWithEmptyCart);

            var shoppingCartEmpty = new ShoppingCart { CustomerId = customerWithEmptyCart.CustomerId };
            context.ShoppingCarts.Add(shoppingCartEmpty);
            #endregion

            #region Setup non-empty cart customer
            Guid testCustomerIdNonEmptyCart = new Guid(TestConstants.CustomerIdNonEmptyCart);
            var customerWithNonEmptyCart = new Customer
            {
                CustomerId = testCustomerIdNonEmptyCart,
                CustomerFirstName = "Test",
                CustomerLastName = "User"
            };
            context.Customers.Add(customerWithNonEmptyCart);


            var shoppingCartNonEmpty = new ShoppingCart { CustomerId = customerWithNonEmptyCart.CustomerId, ItemsCount = 2 };
            context.ShoppingCarts.Add(shoppingCartNonEmpty);

            var shoppingCartItem1 = new ShoppingCartItem { Product = product1, Quantity = 1, ShoppingCartId = shoppingCartNonEmpty.ShoppingCartId };
            var shoppingCartItem2 = new ShoppingCartItem { Product = product2, Quantity = 1, ShoppingCartId = shoppingCartNonEmpty.ShoppingCartId };

            context.ShoppingCartItems.AddRange(new[]{
                shoppingCartItem1,shoppingCartItem2
                });

            #endregion

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ShoppingCartDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
