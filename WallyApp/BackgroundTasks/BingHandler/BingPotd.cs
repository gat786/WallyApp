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
        async private Task<BingJSON> GetJSONResult()
        {
            var client = new HttpClient();
            var result = await (
                            await client.GetAsync(StaticData.getLocaleSpecific(StaticData.Locales.Italy))
                            ).Content.ReadAsStringAsync();
            Debug.WriteLine("result is " + result);
            var bingResponse = JsonConvert.DeserializeObject<BingJSON>(result);
            return bingResponse;
        }

        async public Task<BitmapImage> getBingPOTDBitmap()
        {
            BingPotd potd = new BingPotd();
            var potdResult = await potd.GetJSONResult();
            var client = new HttpClient();
            var image = new BitmapImage(new Uri(BING_BASE + potdResult.images[0].url));
            return image;
        }
    }
}
