using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class Basket
    {
        [Key]
        public long Id { get; set; }

        public virtual ICollection<BasketItem> Items { get; set; }
    }
}
