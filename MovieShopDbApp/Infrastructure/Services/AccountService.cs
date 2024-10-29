using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepositoryAsync _userRepository;
        public AccountService(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterModel userRegister)
        {
            var user = new User
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                DateOfBirth = userRegister.DateOfBirth,
                HashedPassword = HashPassword(userRegister.Password)
            };

            var addedUserId = await _userRepository.InsertAsync(user);
            return addedUserId > 0;
        }

        public async Task<UserLoginResponseModel> ValidateUserAsync(UserLoginModel userLogin)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLogin.Email);
            if (user != null && user.UserRoles.FirstOrDefault() != null && VerifyPassword(userLogin.Password, user.HashedPassword))
            {
                return new UserLoginResponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = GenerateJwtToken(user),
                    Role = user.UserRoles.FirstOrDefault().Role.Name
                };
            }
            return null;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordModel resetPassword)
        {
            var user = await _userRepository.GetUserByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                user.HashedPassword = HashPassword(resetPassword.NewPassword);
                return await _userRepository.UpdateAsync(user) > 0;


            }
            return false;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("YourSecretKeyHere");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
