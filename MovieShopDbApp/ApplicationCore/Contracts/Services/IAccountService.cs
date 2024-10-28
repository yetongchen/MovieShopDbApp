using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(UserRegisterModel userRegister);
        Task<UserLoginResponseModel> ValidateUserAsync(UserLoginModel userLogin);
        Task<bool> ResetPasswordAsync(ResetPasswordModel resetPassword);
    }
}
