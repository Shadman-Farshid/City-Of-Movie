using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.Generators
{
   public static class ActiveCodeGeneratore
    {
        public static string Generator()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
