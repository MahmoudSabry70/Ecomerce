using Ecomerce_test1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ecomerce_test1.Models.viewModel
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            Colores = new List<int>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }


        [Required]
        [MaxLength(50, ErrorMessage = "must be less then 15 ")]
        [MinLength(3, ErrorMessage = "must be more then 3  ")]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "must be more then 20  ")]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "Publish At")]
        public DateTime PublishAt { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    


        public List<int> Colores { get; set; }
        public List<int> CatagorysId { get; set; }

        List<string> uploadedFiles = new List<string>();
    }
}
