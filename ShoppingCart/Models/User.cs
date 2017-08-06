using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
