﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KRCars1298.Data;
using KRCars1298.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using KRCars1298.Data.Models.ViewModels;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KRCars1298.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Model
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Models.Include(m => m.Brand);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Model/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            return View();
        }

        // POST: Model/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManageModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Brand brand = this._context.Brands.FirstOrDefault(b => b.Name == viewModel.BrandName);

                if (brand == null)
                {
                    return NotFound();
                }

                Model model = new Model()
                {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    Brand = brand,
                    BrandId = brand.Id,
                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", model.BrandId);
            return View(viewModel);
        }

        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Models.Include(m => m.Brand).FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            ManageModelViewModel viewModel = new ManageModelViewModel()
            {
                Name = model.Name,
                BrandName = model.Brand.Name
            };

            //ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", model.BrandId);
            return View(viewModel);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ManageModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Brand brand = _context.Brands.FirstOrDefault(b => b.Name == viewModel.BrandName);

                    if (brand == null)
                    {
                        return View(viewModel);
                    }

                    Model model = _context.Models.Include(m => m.Brand).FirstOrDefault(m => m.Id == id);

                    if (model == null)
                    {
                        return View(viewModel);
                    }

                    model.Name = viewModel.Name;
                    model.Brand = model.Brand;
                    model.BrandId = model.Brand.Id;

                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(id))
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
            //ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", viewModel.BrandId);
            return View(viewModel);
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var model = await _context.Models.FindAsync(id);
            Ad[] adsToBeDeleted = _context.Ads.Where(a => a.ModelId == id).ToArray();

            _context.Ads.RemoveRange(adsToBeDeleted);
            _context.Models.Remove(model);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(Guid id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
