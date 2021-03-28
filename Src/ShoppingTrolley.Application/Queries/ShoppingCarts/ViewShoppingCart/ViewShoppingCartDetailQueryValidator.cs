using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.Common.Interfaces;
using FluentValidation;
using System;

namespace ShoppingTrolley.Application.Queries.ShoppingCarts
{
    public class ViewShoppingCartDetailQueryValidator : AbstractValidator<ViewShoppingCartDetailQuery>
    {
        private readonly IShoppingCartDbContext _context;
        public ViewShoppingCartDetailQueryValidator(IShoppingCartDbContext context)
        {
            _context = context;
            RuleFor(x => x.ShoppingCartId)
                .GreaterThan(0)
                .WithMessage("Shopping cart id must be supplied")
                .Custom((shoppingCartId, validationContext) =>
                {
                    var command = validationContext.InstanceToValidate as ViewShoppingCartDetailQuery;
                    var shoppingCart = _context.ShoppingCarts.Find(command.ShoppingCartId);
                    if (shoppingCart == null)
                    {
                        validationContext.AddFailure($"Shopping cart id {command.ShoppingCartId} was not found. Please provide a valid shopping cart id");
                    }
                });

        }
    }
}
