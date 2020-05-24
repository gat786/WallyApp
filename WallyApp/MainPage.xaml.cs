using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


using WallyApp.BackgroundTasks;
using WallyApp.BackgroundTasks.BingHandler;
using Windows.Storage.Streams;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WallyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        BingJSON jsonImageData;
        BitmapImage bingBitmap = new BitmapImage();
        Stream stream;
        BingPotd potd = new BingPotd();

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush imageBrush = new ImageBrush();
            stream = await potd.GetStreamOfImage();
            await bingBitmap.SetSourceAsync(stream.AsRandomAccessStream());
            imageBrush.ImageSource = bingBitmap;
            WallsPage.Background = imageBrush;
        }

        private void SaveImageCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var imageBuffer = await potd.GetImageBuffer();
                WallpaperHandler handler = new WallpaperHandler();
                
                var wallImage = await handler.saveToAppData(imageBuffer, potd.GetFileName());
                if (SaveImageCheckBox.IsChecked.Value)
                {
                    await handler.saveImageToPicturesLibrary(imageBuffer, potd.GetFileName());
                }
                var result = await handler.setWallpaper(wallImage);
                Debug.WriteLine("Result is " + result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
    }
}
