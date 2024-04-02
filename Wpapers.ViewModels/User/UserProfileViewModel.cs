using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.Data.Models;
using Wpapers.ViewModels.Wallpaper;

namespace Wpapers.ViewModels.User
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            Wallpapers = new HashSet<WallpaperViewModel>();
        }
        public ApplicationUser User { get; set; }      
        public IEnumerable<WallpaperViewModel> Wallpapers { get; set; }
    }
}
