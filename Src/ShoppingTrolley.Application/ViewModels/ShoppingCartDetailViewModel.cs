using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingTrolley.Application.ViewModels
{
    public class ShoppingCartDetailViewModel : IMapFrom<ShoppingCart>
    {
        public long ShoppingCartId { get; set; }
        public Guid CustomerId { get; set; }

        public List<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }
        public double TotalSalePrice { get; set; }
        public double TotalDiscountedPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartDetailViewModel>();
        }
    }
}
