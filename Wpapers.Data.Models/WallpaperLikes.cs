using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpapers.Data.Models
{
    public class WallpaperLikes
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Wallpaper))]
        public int PhotoId { get; set; }
        public Wallpaper Wallpaper { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public bool HasLiked { get; set; }
    }
}
