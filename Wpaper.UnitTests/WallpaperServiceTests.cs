using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Wpapers.Data.Models;
using Wpapers.Services;
using Wpapers.Services.Interfaces;
using Wpapers.UnitTests;
using Wpapers.ViewModels.Wallpaper;
using Assert = NUnit.Framework.Assert;

namespace Wpaper.UnitTests
{
    [TestFixture]
    public class WallpaperServiceTests
    {
        private IWallpaperService _wallpaperService;

        [Test]
        public async Task AddWallpaperToDb()
        {
            using var data = DbMock.Instance;
            this._wallpaperService = new WallpaperService(data);
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Nickname = "pesho"
            };
            var wallpaper = new Wallpaper()
            {
                Id = 1,
                ImagePath = "imagePath",
                Title = "Title",
                UploaderId = user.Id,
                UploaderName = user.Nickname
            };
            var model = new AddWallpaperFormModel()
            {
                ImagePath = wallpaper.ImagePath,
                Title = wallpaper.Title,
            };
            await data.Users.AddAsync(user);
            await data.Wallpapers.AddAsync(wallpaper);
            await data.SaveChangesAsync();

            await this._wallpaperService.AddWallpaperAsync(model, user.Id.ToString());
            var dbWallpaper = data.Wallpapers.FirstOrDefaultAsync(w => w.Id == 1);
            Assert.That(dbWallpaper, Is.Not.Null);

        }

        [Test]
        public async Task ShowAllWallpapers()
        {
            using var data = DbMock.Instance;
            this._wallpaperService = new WallpaperService(data);
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Nickname = "pesho"
            };
            var wallpaper = new Wallpaper()
            {
                Id = 1,
                ImagePath = "imagePath",
                Title = "Title",
                UploaderId = user.Id,
                UploaderName = user.Nickname
            };
            var wallpaper2 = new Wallpaper()
            {
                Id = 2,
                ImagePath = "imagePath2",
                Title = "Title2",
                UploaderId = user.Id,
                UploaderName = user.Nickname
            };
            var wallpapersToAdd = new List<Wallpaper>();
            wallpapersToAdd.Add(wallpaper);
            wallpapersToAdd.Add(wallpaper2);

            await data.Users.AddAsync(user);
            await data.Wallpapers.AddAsync(wallpaper);
            await data.Wallpapers.AddAsync(wallpaper2);
            await data.SaveChangesAsync();

            List<WallpaperViewModel> wallappers = await data.Wallpapers
                .Select(w => new WallpaperViewModel
                {
                    Id = w.Id,
                    Ttitle = w.Title,
                    ImagePath = w.ImagePath,
                    UploaderId = w.UploaderId.ToString(),
                    UploaderName= w.UploaderName,
                    

                } ).ToListAsync();

            var model = new WallpaperQueryModel()
            {
                Wallpapers = wallappers,
                TotalWallpapers = wallappers.Count,
            };
            await this._wallpaperService.AllAsync(model, 1, searchString: null);        
            Assert.That(data.Wallpapers.Count() == wallpapersToAdd.Count());
            Assert.That(data.Wallpapers.FirstOrDefault() == wallpapersToAdd.FirstOrDefault());
        }

    }
}