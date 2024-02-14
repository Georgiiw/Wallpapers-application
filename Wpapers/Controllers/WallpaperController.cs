using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wpapers.Services.Interfaces;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Controllers
{
    [Authorize]
    public class WallpaperController : Controller
    {
        private readonly IWallpaperService _wallpaperService;
        public WallpaperController(IWallpaperService wallpaperService)
        {
            this._wallpaperService = wallpaperService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<WallpaperViewModel> allWallpapers =
                await this._wallpaperService.AllWpAsync();

            return View(allWallpapers);
        }

        public IActionResult Add()
        {
            try
            {
                AddWallpaperFormModel wpModel = new AddWallpaperFormModel();

                return View(wpModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Wallpaper");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddWallpaperFormModel model)
        {
            var userId = this.User.Claims
                .FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!ModelState.IsValid)
            {
                throw new Exception();
            }
            try
            {
                await this._wallpaperService.AddWallpaperAsync(model, userId);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("All","Wallpaper");
        }
    }
}
