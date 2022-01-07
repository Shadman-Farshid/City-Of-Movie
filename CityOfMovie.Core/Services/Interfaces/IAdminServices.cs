
using CityOfMovie.Core.DTOs;
using CityOfMovie.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.Services.Interfaces
{
    public interface IAdminServices
    {
        public UserFilterViewModel GetUsers(int pageId, string username, string email);
        public List<User> GetAllUserJson();
    }
}
