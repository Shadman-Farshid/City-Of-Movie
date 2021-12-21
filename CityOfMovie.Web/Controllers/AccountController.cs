using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CityOfMovie.Core.Convertors;
using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Generators;
using CityOfMovie.Core.Security;
using CityOfMovie.Core.Senders;
using CityOfMovie.Core.Services;
using CityOfMovie.Core.Services.Interfaces;
using CityOfMovie.Data.Context;
using CityOfMovie.Data.Entities.User;
using CityOfMovie.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CityOfMovie.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;

        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        #region Register
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userService.IsEmailExist(FixedStrings.Fixed(register.Email)))
            {
                ModelState.AddModelError("Email", errorMessage: "ایمیل وارد شده تکراری می باشد");
                return View(register);
            }

            if (_userService.IsUsernameExist(FixedStrings.Fixed(register.UserName)))
            {
                ModelState.AddModelError("UserName", errorMessage: "کلمه کاربری تکراری می باشد");
                return View(register);
            }

            Data.Entities.User.User user = new User()
            {
                UserName = register.UserName,
                Email = FixedStrings.Fixed(register.Email),
                Password = HashPasswordGenerator.EncodePassword(register.Password),
                ActiveCode = ActiveCodeGeneratore.Generator(),
                IsActive = false,
                RegisterDate = DateTime.Now,
                UserAvatar = "Default.webp"

            };
            _userService.AddUser(user);

            #region Send Activation Email

            string emailBody = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی حساب کاربری", emailBody);
            #endregion

            return View("SuccessRegister");

        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginVm login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.LoginUser(login);
            if (user == null)
            {
                ModelState.AddModelError("User", "نام کاربری یا کلمه عبور اشتباه می باشد");
                return View();
            }

            if (user.IsActive)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe
                };
                HttpContext.SignInAsync(principal, properties);

            }
            else
            {
                ModelState.AddModelError("User", "حساب کاربری وارد شده فعال نمی باشد");
                return View();
            }


            ViewBag.isSucced = true;
            return Redirect("/Home/Home");
        }

        #endregion  

        #region Logout

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Home");
        }

        #endregion

        #region ActiveUser

        public IActionResult ActiveUser(string id)
        {
            ViewBag.isActive = _userService.ActiveUser(id);
            return View();
        }

        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordVm forgot)
        {
            var user = _userService.GetUserWithEmail(FixedStrings.Fixed(forgot.Email));
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (user == null)
            {
                ModelState.AddModelError("Email", errorMessage: "کاربری با ایمیل وارد شده یافت نشد");
                return View();
            }


            var emailBody = _viewRender.RenderToStringAsync("_RecoveryPaaaword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور", emailBody);
            ViewBag.isSucced = true;


            return View();
        }
        #endregion

        #region Change Password
        public IActionResult ChangePassword(string activeCode)
        {
            ChangePasswordVm changePasswordVm = new ChangePasswordVm()
            {
                ActiveCode = activeCode
            };
            return View(changePasswordVm);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVm changePassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.GetUserWithActiveCode(changePassword.ActiveCode);
            user.Password = HashPasswordGenerator.EncodePassword(changePassword.Password);
            _userService.UpdateUser(user);
            return Redirect("Login");
        }
        #endregion
    }

}
