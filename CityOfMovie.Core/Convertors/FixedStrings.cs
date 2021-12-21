using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.Convertors
{
  public class FixedStrings
    {
        public static string Fixed(string text)
        {
            return text.Trim().ToLower();
        }
    }
}
