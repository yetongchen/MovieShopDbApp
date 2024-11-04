using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IConfiguration _configuration;
        public AccountService(IUserRepositoryAsync userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterModel userRegister)
        {
            var profilePictureUrl = _configuration["DefaultProfilePictureUrl"] ?? "/images/defaults/Default_pfp.png";
            var salt = GenerateSalt();

            var user = new User
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                PhoneNumber = userRegister.PhoneNumber,
                DateOfBirth = userRegister.DateOfBirth,
                ProfilePictureUrl = profilePictureUrl,
                Salt = salt,
                HashedPassword = HashPassword(userRegister.Password, salt)
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
                user.HashedPassword = HashPassword(resetPassword.NewPassword, user.Salt);
                return await _userRepository.UpdateAsync(user) > 0;


            }
            return false;
        }

        private string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"]
                ?? "Movie!Shop@WebApp#Oct2024$VeryLongSecretKeyHereWithAtLeast32Characters%@!#";
            if (secretKey == null)
            {
                throw new InvalidOperationException("JWT SecretKey is not configured.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
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

        private static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
    }
}
