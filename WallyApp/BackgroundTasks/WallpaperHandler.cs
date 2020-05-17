using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.UserProfile;

namespace WallyApp.BackgroundTasks
{
    class WallpaperHandler
    {
        Boolean setWallpaper()
        {
            bool success = false;

            if (UserProfilePersonalizationSettings.IsSupported())
            {

            }
            return true;
        }
    }
}
