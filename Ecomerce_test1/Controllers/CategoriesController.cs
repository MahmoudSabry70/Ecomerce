using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Ecomerce_test1.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Ecomerce_test1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoriesController(ICategoriesService _categoriesService, IWebHostEnvironment _webHostEnvironment)
        {
            categoriesService = _categoriesService;
            this.webHostEnvironment = _webHostEnvironment;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(categoriesService.GetAll());
        }

        // GET: Categories/Details/5
        public   IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = categoriesService.GetById((long)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category category , IFormFile postedFile)
        {
            if (ModelState.IsValid)
            {
                #region product Images 
                string imgPath = ""; 
                if (postedFile != null) 
                {
                    string wwwPath = this.webHostEnvironment.WebRootPath;
                    string contentPath = this.webHostEnvironment.ContentRootPath;

                    string path = Path.Combine(this.webHostEnvironment.WebRootPath, "img", "categories");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { postedFile.CopyTo(stream);  }
                    imgPath = fileName;
                }
                #endregion

                category.image = imgPath;
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;

                categoriesService.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public  IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = categoriesService.GetById((long)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(long id, Category category,IFormFile postedFile)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    #region product Images 
                    string imgPath = "";
                    if (postedFile != null)
                    {
                        string wwwPath = this.webHostEnvironment.WebRootPath;
                        string contentPath = this.webHostEnvironment.ContentRootPath;

                        string path = Path.Combine(this.webHostEnvironment.WebRootPath,"img","categories");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileName = Path.GetFileName(postedFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        { postedFile.CopyTo(stream); }
                        imgPath = fileName;
                    }
                    #endregion
                    category.image = imgPath;
                    category.UpdatedAt = DateTime.Now;
                    categoriesService.Update(id, category);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = categoriesService.GetById((long)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public   IActionResult DeleteConfirmed(long id)
        {
          categoriesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            Category category = categoriesService.GetById((long)id);
            if (category != null ) {
                return true;
            }
          return false;
        }
    }
}
