using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Api
{
    public class TextService
    {
        public static bool IsBadText(string text)
        {
            if (text.Contains("Bad word"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
