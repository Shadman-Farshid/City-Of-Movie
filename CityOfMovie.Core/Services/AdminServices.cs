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

        public List<User> GetAllUserJson()
        {
            return _context.Users.ToList();
        }

        public UserFilterViewModel GetUsers(int pageId, string username="", string email="")
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
            if (pageId == 0)
            {
                pageId = 1;
            }
            int take =8;
            var skip = (pageId-1)*take;
            UserFilterViewModel userListVM = new UserFilterViewModel()
            {
                CurentPage = pageId,
                PageCount =(UserList.Count()+take)/take,
                UserPerPage = take,
                userList = UserList.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList()
            };
            return userListVM;
        }
    }
}
