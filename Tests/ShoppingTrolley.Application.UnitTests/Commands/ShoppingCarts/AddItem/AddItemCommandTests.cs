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
    public class AddItemCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public AddItemCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenCustomerId_And_UnknownProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AddItemCommand
            {
                CustomerId = Guid.NewGuid(),
                ProductId = 100
            };
            var handler = new AddItemCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownCustomerId_And_KnownProductId_ShouldReturnAddItemViewModel()
        {
            // Arrange
            var command = new AddItemCommand
            {
                CustomerId = new Guid(TestConstants.CustomerIdEmptyCart),
                ProductId = 1
            };
            var handler = new AddItemCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShoppingCartViewModel>();
        }
    }
}
