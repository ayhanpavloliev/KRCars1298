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
            AllAdsListViewModel[] adsViewModels = new AllAdsListViewModel[ads.Length];

            for (int i = 0; i < ads.Length; i++)
            {
                adsViewModels[i] = new AllAdsListViewModel
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

                adsViewModels[i].City = this.dbContext.Users.FirstOrDefault(u => u.Id == ads[i].User.Id).City;
            }

            return View(adsViewModels);
        }

        [Authorize(Roles="User")]
        public async Task<IActionResult> MyAds()
        {
            string currentUserName = base.User.Identity.Name;

            Ad[] ads = this.dbContext.Ads.Include(a => a.User).Where(a => a.User.UserName == currentUserName).ToArray();
            AdsListBaseViewModel[] adsViewModels = new AdsListBaseViewModel[ads.Length];

            for (int i = 0; i < ads.Length; i++)
            {
                adsViewModels[i] = new AdsListBaseViewModel
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

            var ad = await this.dbContext.Ads.Include(a => a.Model).ThenInclude(m => m.Brand)
                .FirstOrDefaultAsync(a => a.Id == id);

            AdDetailsBaseViewModel adViewModel = new AdDetailsBaseViewModel()
            {
                Id = ad.Id,
                BrandName = ad.Model.Brand.Name,
                ModelName = ad.Model.Name,
                ImageUrl= ad.ImageUrl,
                Year = ad.Year,
                Fuel = ad.Fuel,
                Description = ad.Description,
                Price = ad.Price
            };

            if (ad == null)
            {
                return NotFound();
            }

            return View(adViewModel);
        }

        // GET: Ad/Details/5
        public async Task<IActionResult> PublicDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await this.dbContext.Ads.Include(a => a.Model).ThenInclude(m => m.Brand)
                                                .Include(a => a.User)
                                                .FirstOrDefaultAsync(a => a.Id == id);

            AdFullDetailsViewModel adViewModel = new AdFullDetailsViewModel()
            {
                Id = ad.Id,
                BrandName = ad.Model.Brand.Name,
                ModelName = ad.Model.Name,
                ImageUrl = ad.ImageUrl,
                Year = ad.Year,
                Fuel = ad.Fuel,
                Description = ad.Description,
                Price = ad.Price,
                FirstName = ad.User.FirstName,
                LastName = ad.User.LastName,
                City = ad.User.City,
                PhoneNumber = ad.User.PhoneNumber
            };

            if (ad == null)
            {
                return NotFound();
            }

            return View(adViewModel);
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
                string currentUser = base.User.Identity.Name;

                ad.User = this.dbContext.Users.FirstOrDefault(u => u.UserName == currentUser);

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
