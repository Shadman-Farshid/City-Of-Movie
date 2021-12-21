using CityOfMovie.Core.Convertors;
using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityOfMovie.Web.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        private IUserService _userService;
        public UserPanelController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("UserPanel")]
        public IActionResult Index()
        {
            var curentUser = _userService.GetUserInformation(User.Identity.Name);
            return View(curentUser);
        }

        #region Edit User Information

        [Route("UserPanel/Edit")]
        public IActionResult EditUserInformation(EditUserPanelVm userPanel)
        {
            userPanel = _userService.editUserPanelInformation(User.Identity.Name);
            return View(userPanel);
        }

        [HttpPost]
        [Route("UserPanel/Edit")]
        public IActionResult UpdateUserInformations(EditUserPanelVm userPanel)
        {
            var curentUser = _userService.GetUserByUsername(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (curentUser.UserName != userPanel.UserName || curentUser.Email != userPanel.Email)
            {
                _userService.UpdateUserInformations(userPanel.UserId, userPanel);
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/Login");
            }
            _userService.UpdateUserInformations(userPanel.UserId, userPanel);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit Password

        [Route("EditPassword")]
        public IActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("EditPassword")]
        public IActionResult EditPassword(EditPasswordVm editPasswordVm)
        {
            var curentUser = _userService.GetUserByUsername(User.Identity.Name);
            var editPassword = _userService.EditPassword(editPasswordVm, curentUser);
            if (!editPassword)
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور وارد شده اشنباه می باشد");
            }
            else
            {
                ViewBag.User = true;
            }
            return View();
        }
        #endregion
    }
}
