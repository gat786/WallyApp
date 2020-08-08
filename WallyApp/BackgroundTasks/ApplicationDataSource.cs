using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WallyApp.BackgroundTasks
{
    class ApplicationDataSource
    {
        ApplicationDataContainer container = ApplicationData.Current.LocalSettings;

        public void SetCurrentSetting(string SettingName,string SettingData)
        {
            var CurrentSetting = container.Values[$"#{SettingName}"];
            Debug.WriteLine($"{SettingName} is set to {SettingData}");
            CurrentSetting = SettingData;
        }

        public string GetCurrentSetting(string SettingName)
        {
            var CurrentSetting = container.Values[$"#{SettingName}"];
            return CurrentSetting.ToString();
        }
    }
}
