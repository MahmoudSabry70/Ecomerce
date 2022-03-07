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
using Microsoft.AspNetCore.Authorization;

namespace Ecomerce_test1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ColorsController : Controller
    {
        private readonly IColorsService colorsService;

        public ColorsController(IColorsService _colorsService)
        {
            colorsService = _colorsService;
        }

        // GET: Colors
        public IActionResult Index()
        {
            return View( colorsService.GetAll());
        }

        // GET: Colors/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = colorsService.GetById((long)id);
               
            if (colors == null)
            {
                return NotFound();
            }

            return View(colors);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Color")] Colors colors)
        {
            if (ModelState.IsValid)
            {
                colorsService.Add(colors);
                return RedirectToAction(nameof(Index));
            }
            return View(colors);
        }

        // GET: Colors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = colorsService.GetById((long)id);
            if (colors == null)
            {
                return NotFound();
            }
            return View(colors);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] Colors colors)
        {
            if (id != colors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  colorsService.Update(id,colors);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorsExists(colors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(colors);
        }

        // GET: Colors/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colors = colorsService.GetById((long)id);
            if (colors == null)
            {
                return NotFound();
            }

            return View(colors);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           colorsService?.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ColorsExists(int id)
        {
            if(colorsService.GetById((long)id)!=null)
            return true;
            else
                return false;
        }
    }
}
