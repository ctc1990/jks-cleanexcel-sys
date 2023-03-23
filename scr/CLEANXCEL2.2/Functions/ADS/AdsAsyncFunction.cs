using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Functions.ADS
{
    public static class AdsAsyncFunction
    {
        public static async Task<int> AddDeviceNotificationAsync(this TcAdsClient client, string variableName, AdsStream dataStream, int offset, int length, AdsTransMode adsTransMode)
        {
            var notificationHandle = await Task.Run(() => client.AddDeviceNotification(variableName, dataStream, offset, length, adsTransMode, 50, 0, null));
            return notificationHandle;
        }
    }
}
