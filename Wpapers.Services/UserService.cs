using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.Data;
using Wpapers.Data.Models;
using Wpapers.Services.Interfaces;
using Wpapers.ViewModels.User;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<UserProfileViewModel> GetUserProfile(string id)
        {
			Wallpaper wallpaper = await this._dbContext.Wallpapers
			.FirstAsync(w => w.Id.ToString() == id);
            ApplicationUser user = await this._dbContext.Users
                .FirstAsync(u => u.Id == wallpaper.UploaderId);

			IEnumerable<WallpaperViewModel> wallpapers = await this._dbContext.Wallpapers
                .Where(w => w.UploaderId == user.Id)
                .Select(w => new WallpaperViewModel
                {
                    Id = w.Id,
                    Ttitle = w.Title,
                    ImagePath = w.ImagePath,
                    UploaderId = w.UploaderId.ToString(),
                    UploaderName = w.UploaderName,
                }).ToListAsync();

            UserProfileViewModel profile = new UserProfileViewModel()
            {
                User = user,
                Wallpapers = wallpapers,
            };
            
            return profile;
        }
    }
}
