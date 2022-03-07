using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce_test1.Models.Entities
{
    public class Category
    {
       
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string image { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        
 
        
    }
}