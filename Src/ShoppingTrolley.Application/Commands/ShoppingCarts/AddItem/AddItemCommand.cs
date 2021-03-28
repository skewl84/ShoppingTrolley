using ShoppingTrolley.Application.ViewModels;
using MediatR;
using System;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class AddItemCommand : IRequest<ShoppingCartViewModel>
    {
        public Guid CustomerId { get; set; }
        public long ProductId { get; set; }
    }
}
