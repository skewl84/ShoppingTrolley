using ShoppingTrolley.Application.ViewModels;
using MediatR;
using System;

namespace ShoppingTrolley.Application.Queries.ShoppingCarts
{
    public class ViewShoppingCartDetailQuery : IRequest<ShoppingCartDetailViewModel>
    {
        public long ShoppingCartId { get; set; }
    }
}
