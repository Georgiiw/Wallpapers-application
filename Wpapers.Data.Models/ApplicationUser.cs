using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wpapers.Common.EntityValidationsConstants.ApplicationUserValidations;
namespace Wpapers.Data.Models
{
    public class ApplicationUser : IdentityUser
    {     
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }
    }
}
