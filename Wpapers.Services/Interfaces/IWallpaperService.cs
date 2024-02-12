﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.Services.Interfaces
{
    public interface IWallpaperService
    {
        Task<IEnumerable<WallpaperViewModel>> AllWpAsync();
        Task AddWallpaperAsync(AddWallpaperFormModel model, string userId);
    }
}
