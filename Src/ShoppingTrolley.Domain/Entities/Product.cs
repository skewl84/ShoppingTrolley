using ShoppingTrolley.Domain.Entities.Promotion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingTrolley.Domain.Entities
{
    public class Product
    {
        public Product()
        {
        }

        [Key]
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public double ProductSalePrice { get; set; }

        [NotMapped]
        public double? ProductDiscountPrice
        {
            get
            {
                return CalculateProductDiscountPrice();
            }
        }

        private double? CalculateProductDiscountPrice()
        {
            double? discountPrice = null;
            if(this.ProductPromotion != null)
            {
                switch (this.ProductPromotion.ProductPromotionDefinition)
                {
                    case Enums.ProductPromotionDefinition.TwoDollarsOff:
                        // assumption product price will be greater than 2 to not cause the price to be negative or the product to be free of cost
                        discountPrice = this.ProductSalePrice - 2; 
                        break;
                    default:
                        break;
                }
            }
            return discountPrice;
        }

        public ProductPromotion ProductPromotion { get; set; }
    }
}
