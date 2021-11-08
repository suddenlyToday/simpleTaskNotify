using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskNotify
{
    public static class Utils
    {
        public static string IfEmptyThenGet(this string src, string returnVal)
        {
            if (src.Length <= 0)
            {
                return returnVal;
            }
            return src;
        }
    }
}
