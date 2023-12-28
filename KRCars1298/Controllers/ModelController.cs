using System;
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
            var applicationDbContext = _context.Models.Include(m => m.Brand)
                                                      .Include(m => m.VehicleType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Model/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.Include(m => m.Brand)
                                             .Include(m => m.VehicleType)
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
            if (!ModelState.IsValid) return View(viewModel);

            VehicleType vehicleType = await this._context.VehicleTypes.FirstOrDefaultAsync(b => b.Name == viewModel.VehicleType);
            if (vehicleType is null) return NotFound();

            Brand brand = await this._context.Brands.FirstOrDefaultAsync(b => b.Name == viewModel.BrandName);
            if (brand is null) return NotFound();

            Model model = new Model()
            {
                Id = Guid.NewGuid(),
                VehicleType = vehicleType,
                VehicleTypeId = vehicleType.Id,
                Name = viewModel.Name,
                Brand = brand,
                BrandId = brand.Id,
            };

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.Include(m => m.Brand)
                                             .Include(m => m.VehicleType)
                                             .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            ManageModelViewModel viewModel = new ManageModelViewModel()
            {
                VehicleType = model.VehicleType.Name,
                Name = model.Name,
                BrandName = model.Brand.Name
            };

            return View(viewModel);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ManageModelViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                VehicleType vehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(vt => vt.Name == viewModel.VehicleType);
                if (vehicleType == null)
                {
                    return View(viewModel);
                }

                Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == viewModel.BrandName);

                if (brand == null)
                {
                    return View(viewModel);
                }

                Model model = await _context.Models.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);

                if (model == null)
                {
                    return View(viewModel);
                }

                model.VehicleType = model.VehicleType;
                model.VehicleTypeId = model.VehicleType.Id;
                model.Name = viewModel.Name;
                model.Brand = model.Brand;
                model.BrandId = model.Brand.Id;

                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ModelExistsAsync(id))
                {
                    return NotFound();
                }
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.Include(m => m.Brand)
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
            Ad[] adsToBeDeleted = await _context.Ads.Where(a => a.ModelId == id).ToArrayAsync();

            _context.Ads.RemoveRange(adsToBeDeleted);
            _context.Models.Remove(model);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ModelExistsAsync(Guid id)
        {
            return await _context.Models.AnyAsync(e => e.Id == id);
        }
    }
}
