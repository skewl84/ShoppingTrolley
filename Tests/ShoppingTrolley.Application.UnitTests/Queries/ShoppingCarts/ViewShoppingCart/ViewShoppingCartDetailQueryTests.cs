using AutoMapper;
using FluentAssertions;
using ShoppingTrolley.Application.Common.Exceptions;
using ShoppingTrolley.Application.Queries.ShoppingCarts;
using ShoppingTrolley.Application.UnitTests.Common;
using ShoppingTrolley.Application.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingTrolley.Application.UnitTests.Commands.ShoppingCarts.AddItem
{
    public class ViewShoppingCartDetailQueryTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public ViewShoppingCartDetailQueryTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenInvalidShoppingCartId__ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery
            {
                ShoppingCartId = 99
            };
            var handler = new ViewShoppingCartDetailQueryHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenValidShoppingCartId_ShouldReturnShoppingCartDetailViewModel()
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery
            {
                ShoppingCartId = 1
            };
            var handler = new ViewShoppingCartDetailQueryHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShoppingCartDetailViewModel>();
        }

        [Fact]
        public async void Handle_GivenShoppingCartId_ShouldReturnShoppingCartDetailViewModel_WithProductSalePrice_And_ProductDiscountPrice()
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery
            {
                ShoppingCartId = 2
            };
            var handler = new ViewShoppingCartDetailQueryHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShoppingCartDetailViewModel>();

            var product1 = response.ShoppingCartItems
                            .Where(x => x.Product.ProductId == 1)
                            .Select(x => x.Product)
                            .FirstOrDefault();

            product1.ProductSalePrice.Should().BeGreaterThan(0.0);
            product1.ProductDiscountPrice.Should().BeGreaterThan(0.0).And.BeLessThan(product1.ProductSalePrice);
        }



        [Fact]
        public async void Handle_GivenShoppingCartId_ShouldReturnShoppingCartDetailViewModel_WithTotalPrice_And_TotalDiscountPrice()
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery
            {
                ShoppingCartId = 2
            };
            var handler = new ViewShoppingCartDetailQueryHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShoppingCartDetailViewModel>();
            response.TotalSalePrice.Should().BeGreaterThan(0.0);
            response.TotalDiscountedPrice.Should().BeGreaterThan(0.0).And.BeLessThan(response.TotalSalePrice);
        }
    }
}
