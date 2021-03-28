using ShoppingTrolley.Application.ViewModels;
using MediatR;
using System;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class SeedDataCommand : IRequest<SeedDataViewModel>
    {        
    }
}
