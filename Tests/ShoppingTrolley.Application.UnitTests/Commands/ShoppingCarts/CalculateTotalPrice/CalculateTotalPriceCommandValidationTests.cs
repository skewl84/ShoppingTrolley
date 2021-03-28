using FluentValidation.TestHelper;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.UnitTests.Common;
using System;
using Xunit;

namespace ShoppingTrolley.Application.UnitTests.Commands.ShoppingCarts.AddItem
{
    public class CalculateTotalPriceCommandValidationTests : CommandTestBase
    {
        private readonly CalculateTotalPriceCommandValidator _validator;

        public CalculateTotalPriceCommandValidationTests()
        {
            _validator = new CalculateTotalPriceCommandValidator(_context);
        }

        [Fact]
        public void GivenInvalidShoppingCartId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CalculateTotalPriceCommand { ShoppingCartId = -1 };

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
            var command = new CalculateTotalPriceCommand
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
