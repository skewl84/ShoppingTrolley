using ShoppingTrolley.Domain.Entities.Promotion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShoppingTrolley.Domain.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            ShoppingCartPromotions = AddDefaultShoppingCartPromotions();
        }

        [Key]
        public long ShoppingCartId { get; set; }
        public Guid CustomerId { get; set; }
        
        public int ItemsCount { get; set; }

        [NotMapped]
        public double TotalSalePrice
        {
            get
            {
                return CalculateTotalSalePrice();
            }
        }

        [NotMapped]
        public double TotalDiscountedPrice
        {
            get
            {
                return CalculateTotalDiscountedPrice();
            }
        }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; private set; }
        public ICollection<ShoppingCartPromotion> ShoppingCartPromotions { get; set; }

        private ICollection<ShoppingCartPromotion> AddDefaultShoppingCartPromotions()
        {
            return new[]
            {
                new ShoppingCartPromotion { ShoppingCartPromotionDefinition = Enums.ShoppingCartPromotionDefinition.BOGOF,PromotionName = "Buy one and get one free", PromotionProductId = 3 },
                new ShoppingCartPromotion { ShoppingCartPromotionDefinition =  Enums.ShoppingCartPromotionDefinition.SAS, PromotionName = "Spend $50 and $5 off the total"}
            };
        }

        public void AddItem(Product product)
        {
            var item = this.ShoppingCartItems
               .Where(item => item.Product.ProductId == product.ProductId)
               .FirstOrDefault();

            if (item == null)
            {
                // Add new item
                ShoppingCartItem newItem = new ShoppingCartItem
                {
                    Product = product,
                    Quantity = 1
                };

                this.ShoppingCartItems.Add(newItem);
            }
            else
            {
                //update Quantity
                item.Quantity += 1;
            }

            // update the cart items count
            this.ItemsCount += 1;
        }

        public void RemoveItem(Product product)
        {
            var item = this.ShoppingCartItems
               .Where(item => item.Product.ProductId == product.ProductId)
               .FirstOrDefault();

            if (item != null)
            {
                if (item.Quantity > 0)
                {
                    item.Quantity -= 1;

                    if (item.Quantity == 0)
                        this.ShoppingCartItems.Remove(item);

                    this.ItemsCount -= 1;
                }

            }
        }

        private double CalculateTotalSalePrice()
        {
            // calculate the total amount
            double totalAmount = 0.0;

            var shoppingCartItems = this.ShoppingCartItems
                .Where(m => m.ShoppingCartId == this.ShoppingCartId);

            foreach (var item in shoppingCartItems)
            {
                totalAmount += item.Product.ProductSalePrice * item.Quantity;
            }

            return Math.Round(totalAmount, 2); // round up to 2 decimal points
        }

        private double CalculateTotalDiscountedPrice()
        {
            // Total discounted price = (sum of all discounted product prices or else sale price) - (sum of all shopping cart discounts)           
            double totalProductDiscountedPrice = 0.0;
            if (this.ShoppingCartItems.Count > 0)
            {
                foreach (var item in this.ShoppingCartItems)
                {
                    totalProductDiscountedPrice += (item.Product.ProductDiscountPrice ?? item.Product.ProductSalePrice) * item.Quantity;
                }
            }

            double totalShoppingCartDiscounts = CalculateShoppingCartDiscounts();

            return Math.Round((totalProductDiscountedPrice - totalShoppingCartDiscounts), 2);
        }

        private double CalculateShoppingCartDiscounts()
        {
            double totalShoppingCartDiscounts = 0.0;
            if (this.ShoppingCartPromotions?.Count > 0)
            {
                foreach (var promotion in this.ShoppingCartPromotions)
                {
                    totalShoppingCartDiscounts += promotion.GetPromotionDiscount(this);
                }
            }
            return totalShoppingCartDiscounts;
        }
    }
}
