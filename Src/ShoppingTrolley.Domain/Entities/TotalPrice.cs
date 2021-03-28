using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTrolley.Domain.Entities
{
    public class TotalPrice
    {
        public TotalPrice()
        {
        }

        [Key]
        public int TotalPriceId { get; set; }
        public double TotalAmount { get; set; }
        public long ShoppingCartId { get; set; }


    }
}
