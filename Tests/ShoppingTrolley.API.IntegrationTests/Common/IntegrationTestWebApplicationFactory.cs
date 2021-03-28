using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingTrolley.Application.Common.Interfaces;
using ShoppingTrolley.Domain.Entities;
using ShoppingTrolley.Domain.Entities.Promotion;
using ShoppingTrolley.Domain.Enums;
using ShoppingTrolley.Infrastructure.Persistence;
using System;
using System.Collections.Generic;

namespace ShoppingTrolley.API.IntegrationTests.Common
{
    public class IntegrationTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<ShoppingCartDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped<IShoppingCartDbContext>(provider => provider.GetService<ShoppingCartDbContext>());


                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    SetupShoppingCartTestDB(scopedServices);

                });
        }

        private void SetupShoppingCartTestDB(IServiceProvider scopedServices)
        {
            var shoppingCartContext = scopedServices.GetRequiredService<ShoppingCartDbContext>();
            shoppingCartContext.Database.EnsureCreated();

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

            shoppingCartContext.Products.AddRange(new[]
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
            shoppingCartContext.Customers.Add(customerWithEmptyCart);

            var shoppingCartEmpty = new ShoppingCart { CustomerId = customerWithEmptyCart.CustomerId };


            shoppingCartContext.ShoppingCarts.Add(shoppingCartEmpty);
            #endregion

            #region Setup non-empty cart customer

            Guid testCustomerIdNonEmptyCart = new Guid(TestConstants.CustomerIdNonEmptyCart);
            var customerWithNonEmptyCart = new Customer
            {
                CustomerId = testCustomerIdNonEmptyCart,
                CustomerFirstName = "Test",
                CustomerLastName = "User"
            };
            shoppingCartContext.Customers.Add(customerWithNonEmptyCart);


            var shoppingCartNonEmpty = new ShoppingCart { CustomerId = customerWithNonEmptyCart.CustomerId, ItemsCount = 2 };

            shoppingCartContext.ShoppingCarts.Add(shoppingCartNonEmpty);

            var shoppingCartItem1 = new ShoppingCartItem { Product = product1, Quantity = 1, ShoppingCartId = shoppingCartNonEmpty.ShoppingCartId };
            var shoppingCartItem2 = new ShoppingCartItem { Product = product2, Quantity = 1, ShoppingCartId = shoppingCartNonEmpty.ShoppingCartId };

            shoppingCartContext.ShoppingCartItems.AddRange(new[]{
                shoppingCartItem1,shoppingCartItem2
                });

            #endregion

            shoppingCartContext.SaveChanges();
        }
    }
}
