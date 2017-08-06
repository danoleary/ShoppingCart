using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }
        public virtual Item Item {get; set;}
        public int Quantity {get; set;}
    }
}