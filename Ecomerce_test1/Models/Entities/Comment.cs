using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class Comment
    {
      
        [Key]
        public long Id { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApllecationUser User { get; set; }
    }
}