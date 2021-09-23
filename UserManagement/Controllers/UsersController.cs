using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagement.Controllers
{
    public class UsersController11 : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public UsersController11(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Username,Email")] IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                await userManager.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Username,Email")] IdentityUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await userManager.FindByIdAsync(id) != null)
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
            return View(user);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(user);

            return View(user);
        }
    }
}
