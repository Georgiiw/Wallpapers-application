using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Wpapers.Common.EntityValidationsConstants.WallpapersValidations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wpapers.ViewModels.Wallpaper
{
    public class AddWallpaperFormModel
    {
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        
    }
}
