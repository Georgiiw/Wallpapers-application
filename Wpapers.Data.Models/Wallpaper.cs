using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

using static Wpapers.Common.EntityValidationsConstants.WallpapersValidations;

namespace Wpapers.Data.Models
{
    public class Wallpaper
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        [Required]
        public string ImagePath { get; set; } = null!;
        [ForeignKey(nameof(Uploader))]
        public string UploaderId { get; set; }
        [Required]
        public IdentityUser Uploader { get; set; } = null!;
        [Required]
        public string UploaderName { get; set; } = null!;


    }
}
