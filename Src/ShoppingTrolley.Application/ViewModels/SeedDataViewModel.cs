using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingTrolley.Application.ViewModels
{
    public class SeedDataViewModel : IMapFrom<Tuple<IList<long>, IList<Guid>>>
    {
        public List<long> ProductIds { get; set; }

        public List<Guid> CustomerIds { get; set; }       
    }
}
