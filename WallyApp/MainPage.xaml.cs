﻿using System;
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
using Windows.ApplicationModel.VoiceCommands;

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

        BitmapImage ImageBitmap = new BitmapImage();
        Stream stream;
        IWallpaperHandler potd = new BingPotd();


        private void SaveImageCheckBox_Click(object sender, RoutedEventArgs e)
        {

            ApplicationDataSource source = new ApplicationDataSource();
            if (SaveImageCheckBox.IsChecked.Value)
            {
                source.SetCurrentSetting(StringResources.SAVE_IMAGE_TO_LOCAL, SaveImageCheckBox.IsChecked.Value.ToString());    
            }
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

        private void WallsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImageandSetImageBrush(potd);
        }

        async private void LoadImageandSetImageBrush(IWallpaperHandler potd) {
            var networkHandler = new NetworkHandler();
            if (networkHandler.checkInternetConnection())
            {
                InternetUnavailableMessage.Visibility = Visibility.Collapsed;
                ImageBrush imageBrush = new ImageBrush();
                stream = await potd.GetStreamOfImage();
                await ImageBitmap.SetSourceAsync(stream.AsRandomAccessStream());
                imageBrush.ImageSource = ImageBitmap;
                WallsPage.Background = imageBrush;
            }
            else
            {
                InternetUnavailableMessage.Visibility = Visibility.Visible;
            }
        }       
    }
}
