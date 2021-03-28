using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingTrolley.Application.ViewModels
{
    public class ShoppingCartViewModel : IMapFrom<ShoppingCart>
    {
        public long ShoppingCartId { get; set; }
        public Guid CustomerId { get; set; }

        public int ItemsCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartViewModel>();
        }
    }
}
