using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Data.Entities.User
{
   public class Role
    {
        public Role()
        {

        }

        public int RoleId { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        #endregion



    }
}
