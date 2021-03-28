using FluentAssertions;
using ShoppingTrolley.API.IntegrationTests.Common;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.ViewModels;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static ShoppingTrolley.API.IntegrationTests.Common.Utilities;

namespace ShoppingTrolley.API.IntegrationTests.Controllers.ShoppingCarts
{
    public class CalculateTotalPriceTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CalculateTotalPriceTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CalculateTotalPriceTests_WithValidShoppingCartId()
        {
            // Arrange
            var client = _factory.CreateClient();

            var command = new CalculateTotalPriceCommand
            {
                ShoppingCartId = 2
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/calculatetotalprice", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<TotalPriceViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(2);
            content.TotalAmount.Should().Be(44.48);
        }

        [Fact]
        public async Task CalculateTotalPriceTests_WithInValidShoppingCartId()
        {
            // Arrange
            var client = _factory.CreateClient();

            var command = new CalculateTotalPriceCommand
            {
                ShoppingCartId = 99
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/shoppingcart/calculatetotalprice", requestContent);

            response.StatusCode.Equals(HttpStatusCode.NotFound);

            var content = await GetResponseContent<TotalPriceViewModel>(response);

            // Assert
            content.ShoppingCartId.Should().Be(0);
            content.TotalAmount.Should().Be(0.0);
        }
    }
}
