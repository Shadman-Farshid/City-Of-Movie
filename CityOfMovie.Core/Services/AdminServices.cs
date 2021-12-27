using CityOfMovie.Core.DTOs;
using CityOfMovie.Core.Services.Interfaces;
using CityOfMovie.Data.Context;
using CityOfMovie.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.Services
{
    public class AdminServices : IAdminServices
    {
        private CityOfMovieContext _context;
        public AdminServices(CityOfMovieContext context)
        {
            _context = context;
        }

        public UserListViewModel GetUsers(int pageId=1, string username="", string email="")
        {
            IQueryable<User> UserList = _context.Users;
            if (!string.IsNullOrEmpty(username))
            {
                 UserList = UserList.Where(u=>u.UserName.Contains(username));
            }
            if (!string.IsNullOrEmpty(email))
            {
                UserList = UserList.Where(u => u.Email.Contains(email));
            }

            //Paging--------
            var take =8;
            var skip = (pageId-1)*take;
            UserListViewModel userListVM = new UserListViewModel()
            {
               
                CurentPage = pageId,
                PageCount = UserList.Count() / take,
                userList = UserList.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList()
            };
            return userListVM;
        }
    }
}
