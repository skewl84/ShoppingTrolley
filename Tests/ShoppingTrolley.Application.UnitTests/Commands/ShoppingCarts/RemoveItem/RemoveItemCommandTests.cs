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
    public class RemoveItemCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public RemoveItemCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenCustomerId_And_UnknownProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new RemoveItemCommand
            {
                CustomerId = Guid.NewGuid(),
                ProductId = 100
            };
            var handler = new RemoveItemCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownCustomerId_And_KnownProductId_ShouldReturnAddItemViewModel_And_ItemsCount()
        {
            // Arrange
            var command = new RemoveItemCommand
            {
                CustomerId = new Guid(TestConstants.CustomerIdNonEmptyCart),
                ProductId = 1
            };
            var handler = new RemoveItemCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShoppingCartViewModel>();
            response.ItemsCount.Should().Be(1);
        }
    }
}
