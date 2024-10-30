using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Entities;
using TMS.Domain.Enums;
using TMS.Infrastructure.Services;

namespace TMS.Web.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index()
        {
            var _users = await _userService.GetAllUsersWithRolesAsync();
            return View(_users);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                return View(user);

            var _user = await _userService.GetUserByUserNameAsync(user.UserName);
            if (_user is null)
                return View(user);

            var _signInResult = await _signInManager.PasswordSignInAsync(_user.UserName, user.Password, user.IsPersistent, false);
            if (!_signInResult.Succeeded)
                return View(user);
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }



        [HttpGet("Account/Get/{userId}")]
        public async Task<JsonResult> GetAsync(string userId)
        {
            var _user = await _userService.GetUserByIdAsync(userId);
            return Json(_user);
        }

        [HttpPost("Account/Add")]
        public async Task<JsonResult> AddAsync(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                return Json("User is Empty");

            if (applicationUser.Password != applicationUser.ConfirmPassword)
                return Json("Password and Confirm Password does not Match");

            var response = await _userService.CreateUserAsync(applicationUser);
            return Json(response);
        }

        [HttpPost("Account/Delete")]
        public async Task<JsonResult> DeleteAsync(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                return Json(new { Message = "User is Empty" });

            var response = await _userService.RemoveUserAsync(applicationUser);
            return Json(response);
        }
    }
}
