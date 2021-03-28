using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingTrolley.Application.ViewModels
{
    public class TotalPriceViewModel : IMapFrom<TotalPrice>
    {
        public double TotalAmount { get; set; }
        public long ShoppingCartId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TotalPrice, TotalPriceViewModel>();
        }
    }
}
