using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wpapers.Common.EntityValidationsConstants.ApplicationUserValidations;

namespace Wpapers.ViewModels.User
{
    public class RegisterFormModel
    {
        [Required]
        [StringLength(NicknameMaxLength, MinimumLength = NicknameMinLength)]
        public string Nickname { get; set; } = null!;  
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        public string ConfirmPassword { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
