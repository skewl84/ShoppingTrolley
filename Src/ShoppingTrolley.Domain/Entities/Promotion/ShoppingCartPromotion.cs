using ShoppingTrolley.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShoppingTrolley.Domain.Entities.Promotion
{
    public class ShoppingCartPromotion : Promotion
    {
        public ShoppingCartPromotion()
        {
        }

        public ShoppingCartPromotionDefinition ShoppingCartPromotionDefinition { get; set; }
        public long? PromotionProductId { get; set; }

        public double GetPromotionDiscount(ShoppingCart shoppingCart)
        {
            double promtionDiscount = 0.0;
            switch (this.ShoppingCartPromotionDefinition)
            {
                case Enums.ShoppingCartPromotionDefinition.BOGOF:
                    var item = shoppingCart.ShoppingCartItems
                        .Where(x => x.Product.ProductId == this.PromotionProductId)
                        .FirstOrDefault();
                    if (item != null)
                        promtionDiscount = this.GetBOGOFDiscount(item.Quantity, item.Product.ProductSalePrice);
                    break;

                case Enums.ShoppingCartPromotionDefinition.SAS:
                    return shoppingCart.TotalSalePrice >= 50 ? (long)(shoppingCart.TotalSalePrice / 50) * 5 : 0.0;
                default:
                    return 0.0;
            }
            return promtionDiscount;
        }

        private double GetBOGOFDiscount(long itemQuantity, double productSalePrice)
        {
            return (itemQuantity / 2) * productSalePrice;
        }
    }
}
