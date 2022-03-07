
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce_test1.Models.Entities
{
    public class Colors
    {
        public int Id { get; set; }
        [Required]
        public string Color { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

    }
}
