using CityOfMovie.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.DTOs
{
    public class UserListViewModel
    {
        public List<User> userList { get; set; }
        public int PageCount { get; set; }
        public int CurentPage { get; set; }

    }
    public class UserFilterViewModel
    {
        public int PageId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public List<User> userList { get; set; }
        public int PageCount { get; set; }
        public int CurentPage { get; set; }
        public int UserPerPage { get; set; }

    }
}
