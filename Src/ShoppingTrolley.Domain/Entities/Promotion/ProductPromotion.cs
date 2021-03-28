using ShoppingTrolley.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTrolley.Domain.Entities.Promotion
{
    public class ProductPromotion: Promotion
    {
        public ProductPromotion()
        {
        }

        public ProductPromotionDefinition ProductPromotionDefinition { get; set; }
    }
}
