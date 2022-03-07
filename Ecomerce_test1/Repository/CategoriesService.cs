
using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Ecomerce_test1.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ecomerce_test1.Repository
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Category newObjectOfT)
        {
            dbContext.Add(newObjectOfT);
            return dbContext.SaveChanges();
        }

        public int Delete(long id)
        {
            dbContext.Remove(GetById((long)id));
            return dbContext.SaveChanges();
        }

  

        public List<Category> GetAll()
        {
            return dbContext.Categories.ToList();
        }

        public Category GetById(long id)
        {
            return dbContext.Categories.Include(s=> s.ProductCategories).FirstOrDefault(p => p.Id == id);
        }

        public Category GetByName(string name)
        {
            return dbContext.Categories.FirstOrDefault(p => p.Name == name);
        }

        public int Update(long id, Category newObjectOfT)
        {
            Category p = GetById(id);
            p.Name = newObjectOfT.Name;
            
            return dbContext.SaveChanges();
        }


    }
}