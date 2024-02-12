using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.Data;
using Wpapers.Data.Models;
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

        public async Task AddWallpaperAsync(AddWallpaperFormModel model, string userId)
        {
            IdentityUser? user = await this._dbContext.Users
                .Where(u => u.Id.ToString() == userId)
                .FirstOrDefaultAsync();


            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            Wallpaper wp = new Wallpaper
            {
                Title = model.Title,
                Uploader = user,
                UploaderName = user.UserName,
                UploaderId = user.Id,
                ImagePath = model.ImagePath,
                
            };
            await this._dbContext.AddAsync(wp);
            await this._dbContext.SaveChangesAsync();
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
