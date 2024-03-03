using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Wpapers.Data.Models;
using Wpapers.ViewModels.User;
using Microsoft.AspNetCore.Authentication;

namespace Wpapers.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Nickname = model.Nickname
            };

            await _userManager.SetEmailAsync(user, model.Email);
            await _userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result = await this._userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return this.View(model);
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this._signInManager
                .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {             
                return this.View(model);
            }           
            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
