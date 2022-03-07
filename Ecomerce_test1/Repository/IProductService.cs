using Ecomerce_test1.Models.Entities;
using System.Collections.Generic;

namespace Ecomerce_test1.Repository
{
    public interface IProductService : IRepository<Product>
    {
        List<Product> GetProductsByColors(int[] Colors);
        List<Product> GetProductsByPrice(int min, int max);
        List<Product> GetProductsByCategory(int[] Categorys);
    }
}
