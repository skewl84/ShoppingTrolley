using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTrolley.Domain.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
        }

        [Key]
        public int ShoppingCartItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public long ShoppingCartId { get; set; }


    }
}
