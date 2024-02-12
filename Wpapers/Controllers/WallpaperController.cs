using Microsoft.AspNetCore.Mvc;
using Wpapers.Services.Interfaces;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Controllers
{
    public class WallpaperController : Controller
    {
        private readonly IWallpaperService _wallpaperService;
        public WallpaperController(IWallpaperService wallpaperService)
        {
            this._wallpaperService = wallpaperService;
        }
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
    }
}
