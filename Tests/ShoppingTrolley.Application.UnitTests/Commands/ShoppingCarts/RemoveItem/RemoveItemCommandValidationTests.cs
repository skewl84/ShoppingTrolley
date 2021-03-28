﻿using FluentValidation.TestHelper;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.UnitTests.Common;
using System;
using Xunit;

namespace ShoppingTrolley.Application.UnitTests.Commands.ShoppingCarts.AddItem
{
    public class RemoveItemCommandValidationTests : CommandTestBase
    {
        private readonly RemoveItemCommandValidator _validator;

        public RemoveItemCommandValidationTests()
        {
            _validator = new RemoveItemCommandValidator(_context);
        }

        [Fact]
        public void GivenInvalidCustomerId_And_InvalidProductId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new RemoveItemCommand { CustomerId = Guid.Empty, ProductId = -1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Theory]
        [InlineData("212248cf-4517-478e-ab4b-dac853ca9a64")]
        [InlineData("a95eb8f3-58c4-46b3-b2c1-4219157e4e0c")]
        public void GivenInvalidCustomerId_ShouldHaveValidationError(string customerId)
        {
            // Arrange
            var command = new RemoveItemCommand
            {
                CustomerId = new Guid(customerId),
                ProductId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(10)]
        public void GivenInvalidProductId_ShouldHaveValidationError(int productId)
        {
            // Arrange
            var command = new RemoveItemCommand
            {
                CustomerId = new Guid(TestConstants.CustomerIdNonEmptyCart),
                ProductId = productId
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }
    }
}
