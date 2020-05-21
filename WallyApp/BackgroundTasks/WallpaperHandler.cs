using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WallyApp.BackgroundTasks.BingHandler;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WallyApp.BackgroundTasks
{
    class WallpaperHandler
    {
        string getFileName(BingJSON jsonImageData) => jsonImageData.images[0].title.ToString();

        public async Task<StorageFile> saveToAppData(byte[] buffer,BingJSON jsonImageData)
        {
            var fileName = getFileName(jsonImageData);    
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var fileToWrite = await folder.CreateFileAsync(fileName);
            await FileIO.WriteBytesAsync( fileToWrite, buffer);
            return fileToWrite;
        }


        public async Task<bool> setWallpaper(StorageFile imageFile)
        {
            bool success = false;

            if (UserProfilePersonalizationSettings.IsSupported())
            {
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetWallpaperImageAsync(imageFile);
            }
            return success;
        }

        public async Task<StorageFile> saveImageToPicturesLibrary(Stream imageStream,BingJSON jsonImageData)
        {
            StorageFolder wallyFolder;
            var fileName = getFileName(jsonImageData);
            try
            {
                wallyFolder = await KnownFolders.PicturesLibrary.GetFolderAsync("WallyUp");
                Debug.WriteLine("Folder found");
            }
            catch (FileNotFoundException fnfexception)
            {
                Debug.WriteLine(fnfexception.Message);
                Debug.WriteLine("Folder not found creating one!");
                wallyFolder = await KnownFolders.PicturesLibrary.CreateFolderAsync("WallyUp");
            }
            

            var imageFile = await wallyFolder.CreateFileAsync(
                    fileName,
                    CreationCollisionOption.ReplaceExisting
                    );
            
            MemoryStream ms = new MemoryStream();
            imageStream.CopyTo(ms);
            await FileIO.WriteBytesAsync(imageFile,
                                         ms.GetBuffer());

            return imageFile;
        }
    }
}
