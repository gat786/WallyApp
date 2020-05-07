using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallyApp
{
    class StaticData
    {
        private static string bING_POTD_URL = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";

        public static string BING_POTD_URL { get => bING_POTD_URL; }
    }
}
