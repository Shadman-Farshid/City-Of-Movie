using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityOfMovie.Web.Areas.AdminPanel.Components
{
    public class UserFiltersViewComponent:ViewComponent
    {
        private IAdminServices _adminservices;
        public UserFiltersViewComponent(IAdminServices adminservices)
        {
            _adminservices = adminservices;
        }
        public async Task<IViewComponentResult> InvokeAsync(UserFilterViewModel userFilter)
        {
            return View("ViewComponents/UserFilters.cshtml", userFilter);
        }
    }
}