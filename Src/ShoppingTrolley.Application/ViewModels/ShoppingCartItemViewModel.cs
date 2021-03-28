using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;
using System;

namespace ShoppingTrolley.Application.ViewModels
{
    public class ShoppingCartItemViewModel : IMapFrom<ShoppingCartItem>
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();
        }
    }
}
