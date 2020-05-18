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
        BitmapImage bingBitmap;

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            BingPotd potd = new BingPotd();
            jsonImageData = await potd.getBingResponse();
            bingBitmap = await potd.loadImage(jsonImageData);
            var imageBrush = new ImageBrush();
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.ImageSource = bingBitmap;
            WallsPage.Background = imageBrush;
        }

        private void SelectSaveFolder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Choosing a folder");
        }

        private void SaveImageCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = KnownFolders.PicturesLibrary;
            StorageFolder wallyFolder;
            try
            {
                wallyFolder = await folder.GetFolderAsync("WallyUp");
                Debug.WriteLine("Folder found");
            }
            catch (FileNotFoundException fnfexception)
            {
                Debug.WriteLine(fnfexception.Message);
                Debug.WriteLine("Folder not found creating one!");
                wallyFolder = await folder.CreateFolderAsync("WallyUp");
            }
            Debug.WriteLine(jsonImageData.images[0].title.ToString());
            try
            {
                var imageFile = await wallyFolder.CreateFileAsync(
                    jsonImageData.images[0].title.ToString().Replace(' ', '-') + ".jpg",
                    CreationCollisionOption.ReplaceExisting
                    );
                var client = new HttpClient();
                var imageStream = await client.GetStreamAsync(StaticData.BING_BASE + jsonImageData.images[0].url);

                WallpaperHandler handler = new WallpaperHandler();
                handler.saveImageToFile(imageStream: imageStream, fileToWrite: imageFile);
                var result = await handler.setWallpaper(imageFile);
                Debug.WriteLine("Result is " + result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
            }
        }
    }
}
