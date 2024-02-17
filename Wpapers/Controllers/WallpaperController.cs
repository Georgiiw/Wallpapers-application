using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wpapers.Extensions;
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
        public async Task<IActionResult> All([FromQuery] WallpaperQueryModel model, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            WallpaperQueryModel serviceModel =
                await _wallpaperService.AllAsync(model, page);
            model.Wallpapers = serviceModel.Wallpapers;
            model.TotalWallpapers = serviceModel.TotalWallpapersCount;
            model.Pages = (int)Math.Ceiling((double)model.TotalWallpapers / model.PageSize);

            return View(model);

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
            var userId = this.User.Id();
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
        public async Task<IActionResult> MyUploads()
        {
            var userId = this.User.Id();
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<WallpaperViewModel> myUploads =
                await this._wallpaperService.MyUploadsAsync(userId);

            return View(myUploads);
        }

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await this._wallpaperService.DeleteWallpaperAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("MyUploads", "Wallpaper");
        }
    }
}
