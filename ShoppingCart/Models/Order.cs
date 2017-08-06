using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        public virtual User User {get; set;}

        public virtual ICollection<OrderItem> Items {get; set;}
    }
}
