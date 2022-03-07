using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_test1.Models.Entities
{
    public class ShoppingSession
    {

        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public decimal Total { get; set; }

        public virtual ApllecationUser User { get; set; }

    }
}
