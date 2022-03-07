using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class ProductCategory
    {
        
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}