using EasyHttp.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallyApp.BackgroundTasks
{
    class BingPotd
    {
        async public void GetImage()
        {
            var httpClient = new HttpClient();
            var response = httpClient.Get(StaticData.BING_POTD_URL);
            Debug.WriteLine("Request came back " + response.RawText);
        }
    }
}
