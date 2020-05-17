using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using WallyApp.BackgroundTasks.BingHandler;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;

namespace WallyApp.BackgroundTasks
{
    class BingPotd
    {
        private const string BING_BASE = "https://www.bing.com";
        async public Task<BingJSON> getBingResponse()
        {
            var client = new HttpClient();
            var result = await (
                            await client.GetAsync(StaticData.BING_POTD_URL)
                            ).Content.ReadAsStringAsync();
            Debug.WriteLine("result is " + result);
            var bingResponse = JsonConvert.DeserializeObject<BingJSON>(result);
            return bingResponse;
        }

        async public Task<BitmapImage> getBingPOTDBitmap()
        {
            BingPotd potd = new BingPotd();
            var potdResult = await potd.getBingResponse();
            var image = new BitmapImage(new Uri(BING_BASE + potdResult.images[0].url));
            return image;
        }

        async public Task<BitmapImage> loadImage(BingJSON imageData)
        {
            Debug.WriteLine(imageData.images[0].title.ToString());
            var image = new BitmapImage(new Uri(BING_BASE + imageData.images[0].url));
            return image;
        }
    }
}
