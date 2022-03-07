using Ecomerce_test1.Models;
using Ecomerce_test1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce_test1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IProductService productService;
        private readonly ICategoriesService categoryService;


        public HomeController(IProductService ProductService, ICategoriesService CategoryService, ILogger<HomeController> logger, ApplicationDbContext _dbContext)
        {
            _logger = logger;
            dbContext = _dbContext;
            productService = ProductService;
            categoryService = CategoryService;
        }

        public IActionResult Index()
        {


            List<int> psCategories = new List<int>();

            foreach (var category in categoryService.GetAll())
            {
                int count = dbContext.ProductCategories.Where(s => s.CategoryId == category.Id).Count();
                psCategories.Add(count);
            }
            ViewData["catagories"] = categoryService.GetAll().Take(5).ToList();
            ViewData["count"] = psCategories.ToList();

            ViewData["p24"] = productService.GetAll().Skip(4).ToList();
            ViewData["p34"] = productService.GetAll().Skip(8).ToList();

            return View(productService.GetAll().Take(8));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
