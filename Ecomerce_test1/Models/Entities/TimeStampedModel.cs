using System;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce_test1.Models.Entities
{
    public interface ITimeStampedModel
    {
        [Key]
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}