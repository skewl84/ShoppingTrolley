using FluentAssertions;
using ShoppingTrolley.API.IntegrationTests.Common;
using ShoppingTrolley.Application.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static ShoppingTrolley.API.IntegrationTests.Common.Utilities;

namespace ShoppingTrolley.API.IntegrationTests.Controllers.ShoppingCarts
{
    public class ViewTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public ViewTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ViewCart_WithValidShoppingCartId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/shoppingcart/view/1");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShoppingCartDetailViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(1);
            content.CustomerId.Should().NotBe(Guid.Empty);
            content.ShoppingCartItems.Should().NotBeNull();
        }


        [Fact]
        public async Task ViewCart_WithValidShoppingCartId_WithProductSalePrice_And_ProductDiscountPrice()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/shoppingcart/view/2");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShoppingCartDetailViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(2);
            content.CustomerId.Should().NotBe(Guid.Empty);
            content.ShoppingCartItems.Should().NotBeNull();

            var product1 = content.ShoppingCartItems
                            .Where(x => x.Product.ProductId == 1)
                            .Select(x => x.Product)
                            .FirstOrDefault();

            product1.ProductSalePrice.Should().BeGreaterThan(0.0);
            product1.ProductDiscountPrice.Should().BeGreaterThan(0.0).And.BeLessThan(product1.ProductSalePrice);
        }


        [Fact]
        public async Task ViewCart_WithValidShoppingCartId_WithTotalPrice_WithTotalDiscountedPrice()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/shoppingcart/view/2");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShoppingCartDetailViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(2);
            content.CustomerId.Should().NotBe(Guid.Empty);
            content.ShoppingCartItems.Should().NotBeNull();
            content.TotalSalePrice.Should().BeGreaterThan(0.0);
            content.TotalDiscountedPrice.Should().BeGreaterThan(0.0).And.BeLessThan(content.TotalSalePrice);
        }

        [Fact]
        public async Task ViewCart_WithInValidShoppingCartId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/shoppingcart/view/10"); // 10 being non-existent shopping cart id

            response.StatusCode.Equals(HttpStatusCode.NotFound);

            var content = await GetResponseContent<ShoppingCartDetailViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(0);
            content.CustomerId.Should().Be(Guid.Empty);
            content.ShoppingCartItems.Should().BeNull();
        }
    }
}
