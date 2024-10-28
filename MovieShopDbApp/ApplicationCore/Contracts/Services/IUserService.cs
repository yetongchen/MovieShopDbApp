using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(UserProfileModel userProfile);
        Task<bool> IsEmailRegisteredAsync(string email);
    }
}
