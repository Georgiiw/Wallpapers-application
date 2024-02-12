using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.Data;
using Wpapers.Services.Interfaces;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Services
{
    public class WallpaperService : IWallpaperService
    {
        private readonly ApplicationDbContext _dbContext;
        public WallpaperService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<WallpaperViewModel>> AllWpAsync()
        {
            IEnumerable<WallpaperViewModel> allWallpapers = await _dbContext
                .Wallpapers
                .Select(w => new WallpaperViewModel
                {
                    Id = w.Id,
                    Ttitle = w.Title,
                    ImagePath = w.ImagePath,
                    UploaderId = w.UploaderId,
                    UploaderName = w.UploaderName,
                }).ToListAsync();

            return allWallpapers;
        }
    }
}
