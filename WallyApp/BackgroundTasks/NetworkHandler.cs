using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WallyApp.BackgroundTasks
{
    class NetworkHandler
    {
        public bool checkInternetConnection()
        {
            bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            
            Debug.WriteLine($"internet is {isConnected}");
            return isConnected;
        }
    }
}
