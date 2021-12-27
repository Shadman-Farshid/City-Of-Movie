using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityOfMovie.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminPanelController : Controller
    {
        private IAdminServices _adminServices;
        public AdminPanelController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        
        [Route("Admin")]
        public IActionResult Index(UserFilterViewModel userFilter)
        {
            return View(_adminServices.GetUsers(1, userFilter.username, userFilter.email));
        }
       

    }
}
