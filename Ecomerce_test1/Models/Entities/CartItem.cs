﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }

        public string color { get; set; }

        public virtual ShoppingSession Session { get; set; }
        public virtual Product Product { get; set; }


    }
}
