using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce_test1.Models.Entities
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Slug { get; set; }
        public DateTime PublishAt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;


        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual  ICollection<Colors> ProductColor { get; set; }
        public virtual  Collection<Comment> Comments { get; set; }
        public virtual  ICollection<ProductImage> ProductImages { get; set; }
    }
}