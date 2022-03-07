using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Ecomerce_test1.Models.viewModel;
using Ecomerce_test1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ecomerce_test1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IProductService productService;
        private readonly ICategoriesService categoryService;
        private readonly IColorsService colorsService;
        private IWebHostEnvironment webHostEnvironment;
        public ProductController(ApplicationDbContext _applicationDbContext ,
                                    IProductService _productService,
                                    ICategoriesService _categoryService,
                                    IColorsService _colorsService,
                                    IWebHostEnvironment _webHostEnvironment )
        {
            this.applicationDbContext= _applicationDbContext;
            this.productService= _productService;
            this.categoryService = _categoryService;
            this.colorsService = _colorsService;
            this.webHostEnvironment= _webHostEnvironment;
        }




        // GET: ProductController
        public ActionResult Index()
        {
           List<Product> products= applicationDbContext.Products.Include(s=>s.ProductImages).ToList();
            List<Category> categories = categoryService.GetAll();
            List<Colors> colors = colorsService.GetAll(); 
                ViewData["Colors"] = colors;
            ViewData["categories"] = categories;
                return View(products);

        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product  product = applicationDbContext.Products.
                Include(s => s.ProductImages).Include(s=>s.ProductColor).Include(s=>s.ProductCategories).Where(s=>s.Id==id).FirstOrDefault();

            List<Category> categories = 
                applicationDbContext.ProductCategories.Include(s=>s.Category).Where(s => s.ProductId == id).Select(s=>s.Category).ToList();
            if (categories!=null) {
                ViewData["categories"] = categories;
                ViewData["fourProduct"] = productService.GetAll().Take(4).ToList();
                return View(product);
            }
            ViewData["fourProduct"] = productService.GetAll().Take(4).ToList();
            List<Product> products = applicationDbContext.Products.Include(s => s.ProductImages).ToList();
            return View(products);

        }





        // GET: ProductController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel() ;
            
           
            ViewData["ColorsAdd"] = applicationDbContext.Colors.ToList();
            ViewData["CategoryAdd"] = applicationDbContext.Categories.ToList();

            return View(productViewModel);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductViewModel productViewModel , List<IFormFile> postedFiles)
        {
            if (!ModelState.IsValid && productViewModel == null && productViewModel.CatagorysId.Count<1)
            {
                List<Colors> Colors = applicationDbContext.Colors.ToList();
                List<Category> Category = applicationDbContext.Categories.ToList();

                ViewData["Colors"] = Colors;
                ViewData["Category"] = Category;

                return View(productViewModel);
            }
            Product product = new Product();
            product.Id = 0;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;
            product.CreatedAt = System.DateTime.Now;
            product.UpdatedAt = System.DateTime.Now;
            applicationDbContext.Products.Add(product);
            applicationDbContext.SaveChanges();

            long id = applicationDbContext.Products.Where(s=> s.CreatedAt==product.CreatedAt && s.Name == product.Name && s.Price==product.Price).FirstOrDefault().Id;

            #region product Images 

            if (postedFiles != null)
            {
                string wwwPath = this.webHostEnvironment.WebRootPath;
                string contentPath = this.webHostEnvironment.ContentRootPath;

                string path = Path.Combine(this.webHostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


               
                foreach (IFormFile postedFile in postedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    ProductImage productImage = new ProductImage();
                    productImage.Id = 0;
                    productImage.image = fileName;
                    productImage.ProductId= id;
                    applicationDbContext.Add(productImage);
                    applicationDbContext.SaveChanges();


                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        
                    }
                }

            }

            #endregion

            foreach (int cataId in productViewModel.CatagorysId)
            {
                
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.CategoryId = applicationDbContext.Categories.Where(c => c.Id == cataId).Select(s => s.Id).SingleOrDefault();
                    productCategory.ProductId = id;
                    applicationDbContext.ProductCategories.Add(productCategory);
                    applicationDbContext.SaveChanges();
              
            }
           

            List<Colors> ColorsNew = new List<Colors>();
            foreach (var colorId in productViewModel.Colores)
            {
               Colors color = new Colors();
               color = applicationDbContext.Colors.Find(colorId);
                ColorsNew.Add(color);
            }
            product.ProductColor = ColorsNew;

  

            applicationDbContext.Entry(product).State = EntityState.Modified;
            applicationDbContext.SaveChanges();

            return RedirectToAction("Index");
           
        }


        public ActionResult GetAllByCategory(int categoryId)
        {
            List<Category> categories = categoryService.GetAll();
            List<Colors> colors = colorsService.GetAll();
            ViewData["Colors"] = colors;
            ViewData["categories"] = categories;
            ViewData["categoryName"] = categoryService.GetById(categoryId).Name;
            return View("Index", applicationDbContext.ProductCategories.Where(s=>s.CategoryId==categoryId).Select(s=>s.Product).ToList());

        }

        public ActionResult Search(string search ) 
        {
            List<Product> products = applicationDbContext.Products.Where(s => s.Name.Contains($"{search}")).ToList();
            List<Category> categories = categoryService.GetAll();
            List<Colors> colors = colorsService.GetAll();
            ViewData["Colors"] = colors;
            ViewData["categories"] = categories;
            ViewData["search"] = search;
            return View("Index",products);
        }

        #region Ajax
        [HttpPost]
        public IActionResult ShopByColors(int[] selectedColors)
        {

            return PartialView(productService.GetProductsByColors(selectedColors));
        }
        [HttpPost]
        public IActionResult ShopByPrice(int min, int max)
        {
            return PartialView("ShopByColors", productService.GetProductsByPrice(min, max));

        }
        [HttpPost]
        public IActionResult ShopByCategory(int[] Categorys)
        {
            if (Categorys.Count() == 0)
            {
                return PartialView("ShopByColors", productService.GetAll());
            }
            if (Categorys.Length == 1 && Categorys[0] == 0)
            {
                return PartialView("ShopByColors", productService.GetAll());
            }
            else
            {
                return PartialView("ShopByColors", productService.GetProductsByCategory(Categorys));
            }

            //List<Product> products =productService.GetProductsByCategory(Categorys);
            ////products.Add(productService.GetById(1));
            ////return PartialView("ShopByColors", products);
            //return PartialView("ShopByColors", products);

        }



        #endregion












        //// GET: ProductController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProductController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

   

    }
}
