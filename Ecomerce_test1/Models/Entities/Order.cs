using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecomerce_test1.Models.Entities
{
    public class Order
    {
    
        [Key]
        public long Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Address")]
        public long AddressId { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;


        public virtual ApllecationUser User { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }

}