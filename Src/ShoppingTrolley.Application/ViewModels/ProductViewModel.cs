using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using ShoppingTrolley.Domain.Entities;

namespace ShoppingTrolley.Application.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public double ProductSalePrice { get; set; }

        public double? ProductDiscountPrice { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductViewModel>();
        }
    }
}
