using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace WallyApp.BackgroundTasks
{
    public interface IWallpaperHandler
    {
        Task<string> GetJsonResponse();
        Task<Stream> GetStreamOfImage();
        Task<BitmapImage> GetBitmapImage();
        Task<byte[]> GetImageBuffer();
        string GetFileName();
    }
}
