using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpapers.ViewModels.User;

namespace Wpapers.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetUserProfile(string userId);
    }
}
