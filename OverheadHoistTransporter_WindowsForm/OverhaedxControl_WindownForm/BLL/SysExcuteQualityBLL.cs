using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc.App;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class SysExcuteQualityBLL
    {
        WebClientManager webClientManager = null;
        readonly string OlsonTimeZoneID = null;
        public SysExcuteQualityBLL(WindownApplication app)
        {
            webClientManager = app.GetWebClientManager();
            OlsonTimeZoneID = WinFromUtility.TimeZoneInfoIDToOlsonTimeZoneID(TimeZoneInfo.Local);
        }

        public System.Drawing.Image getSysExcuteQualityImaget(string filename, string start_time, string end_time)
        {
            string[] action_targets = new string[]
            {
                $"Filename={filename}",
                $"StartTime={start_time}",
                $"EndTime={end_time}",
                $"TimeZone={OlsonTimeZoneID}",
            };
            StringBuilder sb = new StringBuilder();
            var result = webClientManager.GetImageFromServerByCondition(WebClientManager.OHxC_SYSEXCUTEQUALITY_URI, action_targets, sb.ToString());
            return result;
        }

        public int getMapInfoFromHttp(SCAppConstants.SystemExcuteInfoType dataType)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "SystemExcuteInfo"
            };
            StringBuilder sb = new StringBuilder();
            //sb.Append($"{nameof(SCAppConstants.MapInfoDataType)}={dataType}");
            sb.Append(dataType);
            result = webClientManager.GetInfoFromServer(WebClientManager.OHxC_CONTROL_URI, action_targets, sb.ToString());
            int.TryParse(result, out int iresult);
            return iresult;
        }

    }
}
