using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.WebMVC.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IGenreService genreService) : base(genreService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Profile(int userId)
        {
            var userProfile = await _userService.GetUserProfileAsync(userId);
            if (userProfile == null)
                return NotFound();

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserProfileModel userProfile)
        {
            if (ModelState.IsValid)
            {
                bool updated = await _userService.UpdateUserProfileAsync(userProfile);
                if (updated)
                    return RedirectToAction("Profile", new { userId = userProfile.Id });
            }

            return View(userProfile);
        }
    }
}
