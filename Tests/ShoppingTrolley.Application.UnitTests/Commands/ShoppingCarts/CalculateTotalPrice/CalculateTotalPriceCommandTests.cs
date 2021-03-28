using AutoMapper;
using FluentAssertions;
using ShoppingTrolley.Application.Common.Exceptions;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ShoppingTrolley.Application.ViewModels;

namespace ShoppingTrolley.Application.UnitTests.Commands.ShoppingCarts.AddItem
{
    public class CalculateTotalPriceCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CalculateTotalPriceCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_UnknownShpppingCartId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CalculateTotalPriceCommand
            {
                ShoppingCartId = 99
            };
            var handler = new CalculateTotalPriceCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownShpppingCartId_ShouldReturnTotalPriceViewModel()
        {
            // Arrange
            var command = new CalculateTotalPriceCommand
            {
                ShoppingCartId = 2
            };
            var handler = new CalculateTotalPriceCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<TotalPriceViewModel>();
        }
    }
}
