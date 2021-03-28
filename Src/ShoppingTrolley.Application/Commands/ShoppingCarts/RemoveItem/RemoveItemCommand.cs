using ShoppingTrolley.Application.ViewModels;
using MediatR;
using System;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class RemoveItemCommand : IRequest<ShoppingCartViewModel>
    {
        public Guid CustomerId { get; set; }
        public long ProductId { get; set; }
    }
}
