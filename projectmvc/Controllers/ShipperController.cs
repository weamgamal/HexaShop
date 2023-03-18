using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectmvc.Models;
using projectmvc.ViewModel;
using System.Security.Claims;

namespace projectmvc.Controllers
{
    public class ShipperController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // /Shipper/Index
        public IActionResult Index()
        {
            return View();
        }

        public ShipperController
         (UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> signInManager)
        {
            userManager = _userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult ShipperRegistration()
        {
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> ShipperRegistration
            (RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserviewModel.UserName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;

                //save data by creating acount
                IdentityResult result =
                    await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    //assign user to Shipper Role
                   await userManager.AddToRoleAsync(userModel, "Shipper");//insert row UserRole

                   await signInManager.SignInAsync(userModel, false); //session cookie register
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                      //  return View(UserviewModel);
                    }
                }

            }

            return View(UserviewModel);
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel UserFromReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {
                //check valid  account "found in db"
                ApplicationUser userModel =
                    await userManager.FindByNameAsync(UserFromReq.Username);
                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                    //check cookie
                    if (rr.Succeeded)
                        return RedirectToAction("Index");
                   //we need to add else 
                }
                else
                {
                    ModelState.AddModelError("", " Wrong Data Try Again !!");
                }
            }
            return View(UserFromReq);
        }



        public async Task<IActionResult> signOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


       

    }
}
