using GamePlay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> New(RoleVM NewRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole Role = new IdentityRole();
                Role.Name = NewRole.Name;
              IdentityResult result=await  roleManager.CreateAsync(Role);
                if (result.Succeeded)
                {
                    return View() ;
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
           
            return View(NewRole);
        }
    }
}
