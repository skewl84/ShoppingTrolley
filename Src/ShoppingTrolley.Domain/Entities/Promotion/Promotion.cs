using ShoppingTrolley.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTrolley.Domain.Entities.Promotion
{
    public class Promotion
    {
        public Promotion()
        {
        }

        [Key]
        public long PromotionId { get; set; }

        public string PromotionName { get; set; }
    }
}
