using KRCars1298.Data.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KRCars1298.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.OrderBy(r => r.Name);

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole roleModel)
        {
            bool exists = this.roleManager.RoleExistsAsync(roleModel.Name).GetAwaiter().GetResult();
            if (!exists)
            {
                await this.roleManager.CreateAsync(new IdentityRole(roleModel.Name));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);


            if (role != null)
            {
                RoleViewModel roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };

                return View(roleViewModel);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]string id, [Bind("Name,Id")] RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IdentityRole role = await this.roleManager.FindByIdAsync(id);
                role.Name = roleViewModel.Name;

                var result = await this.roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest();
            }

            return ValidationProblem();
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleViewModel);
        }

        // POST: Model/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromRoute]string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);

            var result = await this.roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
    }
}
