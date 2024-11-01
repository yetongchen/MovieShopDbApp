using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieApp.WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var userProfile = await _userService.GetUserProfileAsync(userId);
                    if (userProfile != null)
                    {
                        return View(userProfile);
                    }
                }
            }

            return RedirectToAction("Login", "Account");
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
