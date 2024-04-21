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
        public string ?ImagePath { get; set; }
        //[ForeignKey(nameof(Uploader))]
        public Guid UploaderId { get; set; }
        [Required]
        public virtual ApplicationUser Uploader { get; set; } = null!;
        [Required]
        public string UploaderName { get; set; } = null!;
        public int Likes { get; set; }
        public DateTime UploadedOn { get; set; }


    }
}
