using ShoppingTrolley.Application.ViewModels;
using MediatR;
using System;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class CalculateTotalPriceCommand : IRequest<TotalPriceViewModel>
    {
        public long ShoppingCartId { get; set; }
    }
}
