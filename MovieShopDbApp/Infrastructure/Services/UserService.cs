using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UserService(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileModel> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfileModel userProfile)
        {
            var user = new User
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                DateOfBirth = userProfile.DateOfBirth,
                PhoneNumber = userProfile.PhoneNumber,
                ProfilePictureUrl = userProfile.ProfilePictureUrl
            };

            return await _userRepository.UpdateAsync(user) > 0;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email) != null;
        }
    }
}
