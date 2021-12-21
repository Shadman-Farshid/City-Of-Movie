using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfMovie.Core.Convertors
{
    public static class PriceConvertor
    {
        public static string ToToman(this decimal value)
        {
            return value.ToString("#,00 تومان");
        }
      
    }
}
