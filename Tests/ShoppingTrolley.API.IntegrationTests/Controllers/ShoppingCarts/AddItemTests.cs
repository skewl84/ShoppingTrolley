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
    public class AddItemTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public AddItemTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddItem_WithValidCustomerId_And_ValidProductId()
        {
            // Arrange
            var client = _factory.CreateClient();
            Guid existingCustomerId = Guid.Parse(TestConstants.CustomerIdEmptyCart);
            var command = new AddItemCommand
            {
                CustomerId = existingCustomerId,
                ProductId = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/additem", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().BePositive();
            content.CustomerId.Should().Equals(existingCustomerId);
            content.ItemsCount.Should().Be(1);
        }

        [Fact]
        public async Task AddItem_WithNoCustomerId_NoProductId()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new AddItemCommand
            {
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/additem", requestContent);

            response.StatusCode.Equals(HttpStatusCode.BadRequest);

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(0);
            content.CustomerId.Should().Equals(Guid.Empty);
        }

        [Fact]
        public async Task AddItem_WithValidCustomerId_NoProductId()
        {
            // Arrange
            var client = _factory.CreateClient();

            Guid existingCustomerId = Guid.Parse(TestConstants.CustomerIdEmptyCart);
            var command = new AddItemCommand
            {
                CustomerId = existingCustomerId
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/additem", requestContent);

            response.StatusCode.Equals(HttpStatusCode.BadRequest);

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(0);
            content.CustomerId.Should().Equals(Guid.Empty);
            content.ItemsCount.Should().Be(0);
        }

        [Fact]
        public async Task AddItem_WithNoCustomerId_ValidProductId()
        {
            // Arrange
            var client = _factory.CreateClient();

            Guid existingCustomerId = Guid.Parse(TestConstants.CustomerIdEmptyCart);
            var command = new AddItemCommand
            {
                ProductId = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/additem", requestContent);

            response.StatusCode.Equals(HttpStatusCode.BadRequest);

            var content = await GetResponseContent<ShoppingCartViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(0);
            content.CustomerId.Should().Equals(Guid.Empty);
            content.ItemsCount.Should().Be(0);
        }
    }
}
