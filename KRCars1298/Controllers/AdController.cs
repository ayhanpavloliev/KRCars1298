using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KRCars1298.Data;
using KRCars1298.Data.Models;
using Microsoft.AspNetCore.Authorization;
using KRCars1298.Data.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KRCars1298.Controllers
{
    public class AdController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public AdController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.dbContext = context;
            this.userManager = userManager;
        }

        // GET: Ad
        public async Task<IActionResult> Index()
        {
            Ad[] ads = this.dbContext.Ads.Include(a => a.User).ToArray();
            AdsListViewModel[] adsViewModels = new AdsListViewModel[ads.Length];

            for (int i = 0; i < ads.Length; i++)
            {
                adsViewModels[i] = new AdsListViewModel
                {
                    Id = ads[i].Id.ToString(),
                    ImageUrl = ads[i].ImageUrl,
                    Year = ads[i].Year,
                    Fuel = ads[i].Fuel,
                    Price = ads[i].Price,
                };

                Model model = this.dbContext.Models.FirstOrDefault(m => m.Id == ads[i].ModelId);
                adsViewModels[i].Model = model.Name;
                adsViewModels[i].Brand = this.dbContext.Brands.FirstOrDefault(b => b.Id == model.BrandId).Name;

                Console.WriteLine(this.dbContext.Users.FirstOrDefault(u => u.Id == ads[i].User.Id).City);
                adsViewModels[i].City = this.dbContext.Users.FirstOrDefault(u => u.Id == ads[i].User.Id).City;
            }

            return View(adsViewModels);
        }

        // GET: Ad/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await this.dbContext.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        [Authorize(Roles = "User")]
        // GET: Ad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,ImageUrl,Year,Fuel,Description,Price,Id")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                ad.Id = Guid.NewGuid();
                this.dbContext.Add(ad);
                await this.dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ad);
        }

        // GET: Ad/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await this.dbContext.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // POST: Ad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ModelId,ImageUrl,Year,Fuel,Description,Price,Id")] Ad ad)
        {
            if (id != ad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.dbContext.Update(ad);
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.Id))
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
            return View(ad);
        }

        // GET: Ad/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await this.dbContext.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ad/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ad = await this.dbContext.Ads.FindAsync(id);
            this.dbContext.Ads.Remove(ad);
            await this.dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(Guid id)
        {
            return this.dbContext.Ads.Any(e => e.Id == id);
        }
    }
}
