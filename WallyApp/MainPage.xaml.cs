using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WallyApp.BackgroundTasks;
using WallyApp.BackgroundTasks.BingHandler;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            BingPotd potd = new BingPotd();
            var bingBitmap = await potd.getBingPOTDBitmap();
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = bingBitmap;
            WallsPage.Background = imageBrush;

            
        }

        private void SaveFolderTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Debug.WriteLine("Something happened bitch");
        }

        private void SaveImageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SaveImageCheckBox.IsChecked == true)
            {
                SelectSaveFolder.IsEnabled = true;
            }
            else
            {
                SelectSaveFolder.IsEnabled = false;
            }
        }

        private void SaveImageCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (SaveImageCheckBox.IsChecked == true)
            {
                SelectSaveFolder.IsEnabled = true;
            }
            else
            {
                SelectSaveFolder.IsEnabled = false;
            }
        }
    }
}
