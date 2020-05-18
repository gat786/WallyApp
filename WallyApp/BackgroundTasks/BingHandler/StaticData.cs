using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallyApp
{
    class StaticData
    {

        public static string BING_BASE = "https://www.bing.com";
        private static string bING_POTD_URL = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1";

        public static string getLocaleSpecific(Locales locale)
        {
            string specificURL = "";
            switch (locale)
            {
                case Locales.UnitedStates:
                    specificURL = BING_POTD_URL + "&mkt=en-us";
                    break;
                case Locales.India:
                    specificURL = BING_POTD_URL + "&mkt=en-in";
                    break;
                case Locales.UnitedKingdom:
                    specificURL = BING_POTD_URL + "&mkt=en-gb";
                    break;
                case Locales.Canada:
                    specificURL = BING_POTD_URL + "&mkt=en-ca";
                    break;
                case Locales.Italy:
                    specificURL = BING_POTD_URL + "&mkt=it-it";
                    break;
                case Locales.Algeria:
                    specificURL = BING_POTD_URL + "&mkt=fr-dz";
                    break;
                case Locales.Ireland:
                    specificURL = BING_POTD_URL + "&mkt=en-ie";
                    break;
                case Locales.Mexico:
                    specificURL = BING_POTD_URL + "&mkt=es-mx";
                    break;
                default:
                    break;
            }
            return specificURL;
        }

        public static string BING_POTD_URL { get => bING_POTD_URL; }

        public enum Locales
        {
            UnitedStates,
            India,
            UnitedKingdom,
            Canada,
            Italy,
            Algeria,
            Ireland,
            Mexico
        }
        
    }
}
