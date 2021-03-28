using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingTrolley.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {            
        }

        [Key]
        public Guid CustomerId { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
