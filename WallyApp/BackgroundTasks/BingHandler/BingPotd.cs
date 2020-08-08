using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WallyApp.BackgroundTasks.BingHandler;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;

namespace WallyApp.BackgroundTasks
{
    public class BingPotd : WallpaperResource
    {
        string jsonResponse;
        BingJSON jsonResponseObject;
        MemoryStream stream = new MemoryStream();
        byte[] buffer;
        BitmapImage bitmapImage = new BitmapImage();

        public BingPotd()
        {
        }

        public async Task<string>  GetJsonResponse()
        {
            var client = new HttpClient();
            jsonResponse = await(
                            await client.GetAsync(StaticData.BING_POTD_URL)
                            ).Content.ReadAsStringAsync();
            jsonResponseObject = JsonConvert.DeserializeObject<BingJSON>(jsonResponse);
            Debug.WriteLine("result is " + jsonResponse);
            return jsonResponse;
        }

        public async Task<Stream> GetStreamOfImage()
        {
            if (jsonResponseObject == null)
            {
                await GetJsonResponse();
            }
            var client = new HttpClient();
            Stream iStream = await client.GetStreamAsync(StaticData.BING_BASE + jsonResponseObject.images[0].url);
            await iStream.CopyToAsync(stream);
            stream.Position = 0;
            return stream;
        }

        public async Task<BitmapImage> GetBitmapImage()
        {
            if (stream == null)
            {
                await GetStreamOfImage();
            }
            await bitmapImage.SetSourceAsync(stream.AsRandomAccessStream());
            return bitmapImage;
        }

        public async Task<byte[]> GetImageBuffer()
        {
            if (stream == null)
            {
                await GetStreamOfImage();
            }
            buffer = stream.GetBuffer();
            return buffer;
        }

        public string GetFileName()
        {
            return jsonResponseObject.images[0].title.Replace(' ', '-') + ".jpg";
        }
    }
}
