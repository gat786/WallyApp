using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallyApp.BackgroundTasks.BingHandler;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WallyApp.BackgroundTasks
{
    class WallpaperHandler
    {
        public async Task<bool> setWallpaper(StorageFile imageFile)
        {
            bool success = false;

            Debug.WriteLine(UserProfilePersonalizationSettings.Current.GetType());
            Debug.WriteLine(UserProfilePersonalizationSettings.Current.ToString());
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetWallpaperImageAsync(imageFile);
            }
            return success;
        }

        public async void saveImageToFile(Stream imageStream, StorageFile fileToWrite)
        {
            MemoryStream ms = new MemoryStream();
            imageStream.CopyTo(ms);
            await FileIO.WriteBytesAsync(fileToWrite,
                                         ms.GetBuffer());
        }
    }
}
