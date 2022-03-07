using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Ecomerce_test1.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace Ecomerce_test1.Repository
{
    
    public class ProductsService: IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesService categoriesService;


        public ProductsService(ApplicationDbContext dbContext , ICategoriesService categoriesService)
        {
            this.dbContext = dbContext;
            this.categoriesService = categoriesService;
           

        }

        public int Add(Product newObjectOfProduct)
        {
            dbContext.Add(newObjectOfProduct);
            return dbContext.SaveChanges();
        }

        public int Delete(long id)
        {
            dbContext.Remove(GetById(id));
            return dbContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return dbContext.Products.
                Include(s => s.ProductImages)
                .Include(s => s.ProductColor)
                .Include(s => s.ProductCategories).ToList();
        }

        public Product GetById(long id)
        {
             
            return dbContext.Products
                .Include(s => s.ProductImages)
                .Include(s => s.ProductColor)
                 .Include(s => s.ProductCategories)
                .FirstOrDefault(p => p.Id == id);
        }

        public Product GetByName(string name)
        {
            return dbContext.Products.FirstOrDefault(p => p.Name == name);
        }

        public int Update(long id, Product newObjectOfT)
        {
            Product p =  GetById(id);
            p.Name = newObjectOfT.Name;
            p.Price = newObjectOfT.Price;
            p.Description = newObjectOfT.Description;
            p.Stock = newObjectOfT.Stock;
            p.ProductImages = newObjectOfT.ProductImages;
            return dbContext.SaveChanges();
        }

        public List<Product> GetProductsByColors(int[] colors)
        {
            List<Product> products = new List<Product>();
            foreach (int colorId in colors)
            {
              List<Product> products1=dbContext.Colors.Include(s => s.Products).FirstOrDefault(c => c.Id == colorId).Products.ToList();
                foreach (Product product in products1)
                {
                    products.Add(GetById(product.Id));
                }
            }
            return products;
        }
        public List<Product> GetProductsByPrice(int min, int max)
        {
            return dbContext.Products
                 .Include(s => s.ProductImages)
                 .Include(s => s.ProductColor)
                 .Include(s => s.ProductCategories)
                 .Where(p=>p.Price >= min && p.Price <= max).ToList();
        }

        public List<Product> GetProductsByCategory(int[] Categorys)
        {
            List<Product> products = new List<Product>();
            foreach (int CategoryId in Categorys)
            {
                List<ProductCategory> productCategorys = categoriesService.GetById(CategoryId).ProductCategories.ToList();
                foreach (ProductCategory productCategory in productCategorys)
                {
                    products.Add(GetById(productCategory.ProductId));
                }
            }


            return products;
        }
    }
}
