using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Bussiness
{
    public class GlobalVariables
    {
        private static string _MainConnectionString;

        public static string MainConnectionString
        {
            get { return _MainConnectionString; }
            set { _MainConnectionString = value; }
        }

    }
}
