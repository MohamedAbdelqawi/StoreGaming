using GamePlay.Models;
using GamePlay.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp;

namespace GamePlay.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly SignInManager<ApplicationIdentityUser> signInManager;

        public AccountController(UserManager<ApplicationIdentityUser> userManager,SignInManager<ApplicationIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {   
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterVm UserVM)
         {
            if(ModelState.IsValid)
            {
                var usermodel = new ApplicationIdentityUser();
                usermodel.UserName=UserVM.UserName; 
                usermodel.Address = UserVM.Address;
                usermodel.PasswordHash = UserVM.Password;
                IdentityResult result =await userManager.CreateAsync(usermodel,UserVM.Password);
                if(result.Succeeded)
                {
                   await signInManager.SignInAsync(usermodel, false);

                    return RedirectToAction("Index", "Games");

                }
                else
                {

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    
                }
            }
            return View();


        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM UserVm)
        {

            if (ModelState.IsValid)
            {
            ApplicationIdentityUser  UserModel= await userManager.FindByNameAsync(UserVm.UserName);
                if(UserModel is not null)
                {
                    bool found=await userManager.CheckPasswordAsync(UserModel,UserVm.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(UserModel, UserVm.RememberMe);
                        return RedirectToAction("Index", "Games");
                    }

                }
               
                    ModelState.AddModelError("","Name Or Password Incorrect");
                
            }
           
            return View(UserVm);
        }


        public IActionResult Signout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin( RegisterVm UserVM)

        {
            if (ModelState.IsValid)
            {
                var usermodel = new ApplicationIdentityUser();
                usermodel.UserName = UserVM.UserName;
                usermodel.Address = UserVM.Address;
                usermodel.PasswordHash = UserVM.Password;
                IdentityResult result = await userManager.CreateAsync(usermodel, UserVM.Password);
                if (result.Succeeded)
                {
                  await  userManager.AddToRoleAsync(usermodel, "Admin");
                    await signInManager.SignInAsync(usermodel, false);

                    return RedirectToAction("Index", "Games");

                }
                else
                {

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }
            }
            return View();
        }
    }
}
