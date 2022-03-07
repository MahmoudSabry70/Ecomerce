using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class ProductImage 
    {
        public long Id { get; set; }

        public string image { get; set; }

        [ForeignKey("Product")]
        public virtual long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}