using ShoppingTrolley.Application.Common.Interfaces;
using FluentValidation;
using System;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class RemoveItemCommandValidator : AbstractValidator<RemoveItemCommand>
    {
        private readonly IShoppingCartDbContext _context;
        public RemoveItemCommandValidator(IShoppingCartDbContext context)
        {
            _context = context;
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id must be supplied")                
                .Custom((customerId, validationContext) =>
                {
                    var command = validationContext.InstanceToValidate as RemoveItemCommand;
                    var customer = _context.Customers.Find(command.CustomerId);
                    if (customer == null)
                    {
                        validationContext.AddFailure($"Customer with customer id {command.CustomerId} was not found. Please provide a valid customer id");
                    }
                });

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Product Id must be supplied")
                .Custom((productId, validationContext) =>
                {
                    var command = validationContext.InstanceToValidate as RemoveItemCommand;
                    var product = _context.Products.Find(command.ProductId);
                    if (product == null)
                    {
                        validationContext.AddFailure($" Product with Product Id {command.ProductId} was not found. Please provide a valid customer id");
                    }
                });

        }
    }
}
