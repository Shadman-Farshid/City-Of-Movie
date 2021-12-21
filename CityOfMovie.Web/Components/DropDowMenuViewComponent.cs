using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityOfMovie.Web.Components
{
    public class DropDowMenuViewComponent:ViewComponent
    {
        private IUserService _userService;   
       public DropDowMenuViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(LoginVm login)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserBalance = _userService.UserWalletBalance(User.Identity.Name);
            }
            
            return  View("/Views/ViewComponents/DropDownMenu.cshtml",login);
        }
    }
}
