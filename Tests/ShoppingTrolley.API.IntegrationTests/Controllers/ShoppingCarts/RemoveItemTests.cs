using FluentAssertions;
using ShoppingTrolley.API.IntegrationTests.Common;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static ShoppingTrolley.API.IntegrationTests.Common.Utilities;

namespace ShoppingTrolley.API.IntegrationTests.Controllers.ShoppingCarts
{
    public class RemoveItemTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public RemoveItemTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task RemoveItem_WithValidCustomerId_With_Valid_And_ExistingProductId()
        {
            // Arrange
            var client = _factory.CreateClient();
            Guid existingCustomerId = Guid.Parse(TestConstants.CustomerIdNonEmptyCart);
            var command = new RemoveItemCommand
            {
                CustomerId = existingCustomerId,
                ProductId = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/removeitem", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().BePositive();
            content.CustomerId.Should().Equals(existingCustomerId);
            content.ItemsCount.Should().Be(1);
        }

        [Fact]
        public async Task RemoveItem_WithValidCustomerId_With_ValidProductId_EmptyCartItems()
        {
            // Arrange
            var client = _factory.CreateClient();
            Guid existingCustomerId = Guid.Parse(TestConstants.CustomerIdEmptyCart);
            var command = new RemoveItemCommand
            {
                CustomerId = existingCustomerId,
                ProductId = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/removeitem", requestContent);

            response.StatusCode.Equals(HttpStatusCode.BadRequest);

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().BePositive();
            content.CustomerId.Should().Equals(existingCustomerId);
            content.ItemsCount.Should().Equals(0);
        }
    }
}
