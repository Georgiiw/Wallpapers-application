using Azure;
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
            ApplicationUser? user = await this._dbContext.Users
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
                UploaderName = user.Nickname,
                UploaderId = user.Id,
                ImagePath = model.ImagePath,
                UploadedOn = DateTime.Now

            };
            await this._dbContext.AddAsync(wp);
            await this._dbContext.SaveChangesAsync();
        }

     

        public async Task<WallpaperQueryModel> AllAsync(WallpaperQueryModel model, int page, string searchString)
        {
            IQueryable<Wallpaper> wallpaperQuery = this._dbContext.Wallpapers.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                wallpaperQuery = wallpaperQuery.Where(w => w.Title.ToLower().Contains(searchString));
            }

            int totalWallpapers = wallpaperQuery.Count();
            model.PageSize = 5;
            model.CurrentPage = page;
                   
           

            IEnumerable<WallpaperViewModel> allWallpapersPaged = await wallpaperQuery
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize)
                .Select(w => new WallpaperViewModel
                {
                    Id = w.Id,
                    Ttitle = w.Title,
                    ImagePath = w.ImagePath,
                    UploaderId = w.UploaderId.ToString(),
                    UploaderName = w.UploaderName,
                    Likes = w.Likes,
                    UploadedOn = w.UploadedOn
                })
                .ToListAsync();       
            
            var wallpapers = new WallpaperQueryModel
            {              

                Wallpapers = allWallpapersPaged,
                TotalWallpapersCount = totalWallpapers,
                SearchString = searchString
            };
      
            return wallpapers;
        }

        public async Task DeleteWallpaperAsync(int wallpaperId )
        {
 
            Wallpaper wallpaper = await this._dbContext.Wallpapers
                .Where(w => w.Id == wallpaperId)
                .FirstOrDefaultAsync();

             this._dbContext.Remove(wallpaper);
            await this._dbContext.SaveChangesAsync();

        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            bool exists = await this._dbContext.Wallpapers
                .AnyAsync(w => w.Id.ToString() == id);
            return exists;
        }

        public async Task LikeWallpaperAsync(int wpId, string userId)
        {
            ApplicationUser? user = await this._dbContext.Users
                .Where(u => u.Id.ToString() == userId)
                .FirstOrDefaultAsync();
            Wallpaper? wp = await this._dbContext.Wallpapers
                .Where(w => w.Id == wpId)
                .FirstOrDefaultAsync();
            
            WallpaperLikes? wpLikes = await this._dbContext.WallpaperLikes
                .FirstOrDefaultAsync(w => w.PhotoId == wp.Id && w.UserId == user.Id);

            if (wpLikes == null)
            {
                WallpaperLikes likes = new WallpaperLikes()
                {
                    PhotoId = wpId,
                    UserId = user.Id,
                    HasLiked = true
                };
                wp.Likes++;
                await this._dbContext.WallpaperLikes.AddAsync(likes);
                
            }
            else
            {
                if (!wpLikes.HasLiked)
                {
                    wp.Likes++;
                    wpLikes.HasLiked = true;
                }
                else if (wpLikes.HasLiked)
                {
                    wp.Likes--;
                    wpLikes.HasLiked = false;
                }
            }
                    
            await this._dbContext.SaveChangesAsync();
        }

        //public async Task<WallpaperViewModel> GetDetailsByIdAsync(string id)
        //{
        //    Wallpaper wallpaper = await this._dbContext.Wallpapers
        //        .FirstAsync(w => w.Id.ToString() == id);
        //    WallpaperViewModel model = new WallpaperViewModel
        //    {
        //        Id = wallpaper.Id,
        //        ImagePath = wallpaper.ImagePath,
        //        Ttitle = wallpaper.Title,
        //        UploaderName = wallpaper.UploaderName,
        //        UploaderId = wallpaper.UploaderId.ToString()
        //    };
        //    return model;
        //}

        public async Task<IEnumerable<WallpaperViewModel>> MyUploadsAsync(string userId)
        {
            ApplicationUser? user = await this._dbContext.Users
                .Where(u => u.Id.ToString() == userId)
                .FirstOrDefaultAsync();

            IEnumerable<WallpaperViewModel> myUploads = await this._dbContext
                .Wallpapers
                .Where(w => w.UploaderId == user.Id)
                .Select(w => new WallpaperViewModel
                {
                    Id = w.Id,
                    Ttitle = w.Title,
                    ImagePath = w.ImagePath,
                    UploaderId = w.UploaderId.ToString(),
                    UploaderName = w.UploaderName,
                    UploadedOn = w.UploadedOn,
                }).ToListAsync();

               return myUploads;
        }
    }
}
