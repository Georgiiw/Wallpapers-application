using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.ViewModels.Enums;

namespace Wpapers.ViewModels.Wallpaper
{
    public class WallpaperQueryModel
    {
        public WallpaperQueryModel()
        {
            this.Wallpapers = new HashSet<WallpaperViewModel>();
        }
        public IEnumerable<WallpaperViewModel> Wallpapers { get; set; }
        public SortBy SortBy { get; set; }
        public int Pages { get; set; }
        public int CurrentPage {  get; set; }
        public int TotalWallpapers { get; set; }
        public int PageSize { get; set; }
        public int TotalWallpapersCount { get; set; }
        public string? SearchString { get; set; }

    }
}
