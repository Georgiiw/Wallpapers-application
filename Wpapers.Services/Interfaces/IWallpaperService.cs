using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Services.Interfaces
{
    public interface IWallpaperService
    {
        Task<WallpaperQueryModel> AllAsync(WallpaperQueryModel model, int page, string searchString);
        Task AddWallpaperAsync(AddWallpaperFormModel model, string userId);
        Task <IEnumerable<WallpaperViewModel>> MyUploadsAsync(string userId);
        Task DeleteWallpaperAsync(int wallpaperId);
        Task<bool> ExistsByIdAsync(string id);
        //Task<WallpaperViewModel> GetDetailsByIdAsync(string id);
    }
}
