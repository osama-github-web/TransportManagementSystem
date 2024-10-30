using IMS.Application.EFCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Entities;
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
                return Json(new {Message = "User is Empty"});

            var response = await _userService.RemoveUserAsync(applicationUser);
            return Json(response);
        }
    }
}
