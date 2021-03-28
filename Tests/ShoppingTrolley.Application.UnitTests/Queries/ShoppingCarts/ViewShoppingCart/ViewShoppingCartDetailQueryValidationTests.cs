using FluentValidation.TestHelper;
using ShoppingTrolley.Application.Queries.ShoppingCarts;
using ShoppingTrolley.Application.UnitTests.Common;
using System;
using Xunit;

namespace ShoppingTrolley.Application.UnitTests.Commands.ShoppingCarts.AddItem
{
    public class ViewShoppingCartDetailQueryValidationTests : CommandTestBase
    {
        private readonly ViewShoppingCartDetailQueryValidator _validator;

        public ViewShoppingCartDetailQueryValidationTests()
        {
            _validator = new ViewShoppingCartDetailQueryValidator(_context);
        }

        [Fact]
        public void GivenInvalidCustomerId_And_InvalidProductId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery { ShoppingCartId = -1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ShoppingCartId);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(10)]
        public void GivenNonexistingShoppingCartId_ShouldHaveValidationError(int shoppingCartId)
        {
            // Arrange
            var command = new ViewShoppingCartDetailQuery
            {
                ShoppingCartId = shoppingCartId
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ShoppingCartId);
        }
    }
}
