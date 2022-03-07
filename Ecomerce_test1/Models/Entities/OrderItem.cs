using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }
      
        [ForeignKey("Product")]
        public long ProductId { get; set; }
      
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ApllecationUser User { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}