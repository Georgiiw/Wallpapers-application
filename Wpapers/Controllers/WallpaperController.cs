using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WallpaperController(IWallpaperService wallpaperService, IWebHostEnvironment webHostEnvironment)
        {
            this._wallpaperService = wallpaperService;
            _webHostEnvironment = webHostEnvironment;

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
        public async Task<IActionResult> Add(AddWallpaperFormModel model, IFormFile? file)
        {
            var userId = this.User.Id();
            try
            {
                string imageRoot = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string wallpaperPath = Path.Combine(imageRoot, @"images\wallpaper");

                    using (var fileStream = new FileStream(Path.Combine(wallpaperPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ImagePath = @"\images\wallpaper\" + fileName;
                }
                await this._wallpaperService.AddWallpaperAsync(model, userId);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("MyUploads","Wallpaper");
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
