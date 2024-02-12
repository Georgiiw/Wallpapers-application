using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpapers.ViewModels.Wallpaper
{
    public class WallpaperViewModel
    {
        public int Id { get; set; }
        public string Ttitle { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string UploaderId { get; set; } = null!;
        public string UploaderName { get; set; } = null!;
    }
}
