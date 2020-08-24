using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.Common
{
    public class WinFromUtility
    {
        #region Pixels & RealLength 的轉換
        private static int scale = 0;
        public static void setScale(int _scale, int zoon_Factor)
        {
            //scale = _scale * 100;
            scale = _scale * zoon_Factor;
        }
        public static double RealLengthToPixelsWidthByScale(double length)
        {
            double length_cm = lengthTransferByScale(length, scale);//1cm:10m
            double length_mm = length_cm * Math.Pow(10, -2) * Math.Pow(10, 3);
            return MillimetersToPixelsWidth(length_mm);
        }
        public static double PixelsWidthToRealLengthByScale(double pixel)
        {
            double length_mm = PixelsWidthToMillimeters(pixel);//1cm:10m
            double length_cm = length_mm * Math.Pow(10, 2) * Math.Pow(10, -3);
            return lengthTransfer2RealLengthByScale(length_cm, scale);
        }

        public static double lengthTransferByScale(double length, double scale)
        {
            return length / scale;
        }

        public static double lengthTransfer2RealLengthByScale(double length, double scale)
        {
            return length * scale;
        }


        public static double MillimetersToPixelsWidth(double length) //length是mm，1厘米=10毫米
        {
            //System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(p.Handle);
            //IntPtr hdc = g.GetHdc();
            //int width = GetDeviceCaps(hdc, 4);     // HORZRES  物理的寬度
            //int pixels = GetDeviceCaps(hdc, 8);     // BITSPIXEL
            int width = 508;                        // HORZRES  物理的寬度
            int pixels = 1920;                      // BITSPIXEL
            //g.ReleaseHdc(hdc);
            return (((double)pixels / (double)width) * (double)length);
        }

        public static double PixelsWidthToMillimeters(double PixelsWidth) //length是毫米，1厘米=10毫米
        {
            //System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(p.Handle);
            //IntPtr hdc = g.GetHdc();
            //int width = GetDeviceCaps(hdc, 4);     // HORZRES  物理的寬度
            //int pixels = GetDeviceCaps(hdc, 8);     // BITSPIXEL  解析度
            int width = 508;                        // HORZRES  物理的寬度
            int pixels = 1920;                      // BITSPIXEL   解析度
            //g.ReleaseHdc(hdc);

            return (((double)width / (double)pixels) * (double)PixelsWidth);
        }
        #endregion Pixels & RealLength 的轉換
        public static Color ConvStr2Color(string sText)
        {
            Color clrData;
            sText = (sText != null) ? sText.Trim() : sText;
            clrData = Color.FromName(sText);
            if (!clrData.IsKnownColor)
            {
                clrData = Color.FromArgb(int.Parse(sText, NumberStyles.AllowHexSpecifier));
            }

            return (clrData);
        }

        public static void setComboboxDataSource(ComboBox crl_comboBox, string[] data_Source)
        {
            crl_comboBox.DataSource = data_Source;
            if (crl_comboBox.AutoCompleteCustomSource.Count != 0)
            {
                crl_comboBox.AutoCompleteCustomSource.Clear();
            }
            crl_comboBox.AutoCompleteCustomSource.AddRange(data_Source);
            crl_comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            crl_comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        /// <summary>
        /// 解壓縮BytyArray資料
        /// </summary>
        /// <param name="compressString">The compress string.</param>
        /// <returns>System.String.</returns>
        public static byte[] unCompressString(string compressString)
        {
            byte[] zippedData = Convert.FromBase64String(compressString.ToString());
            System.IO.MemoryStream ms = new System.IO.MemoryStream(zippedData);
            System.IO.Compression.GZipStream compressedzipStream = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
            System.IO.MemoryStream outBuffer = new System.IO.MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// Converts an Olson time zone ID to a Windows time zone ID.
        /// </summary>
        /// <param name="olsonTimeZoneId">An Olson time zone ID. See http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/zone_tzid.html. </param>
        /// <returns>
        /// The TimeZoneInfo corresponding to the Olson time zone ID, 
        /// or null if you passed in an invalid Olson time zone ID.
        /// </returns>
        /// <remarks>
        /// See http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/zone_tzid.html
        /// </remarks>
        public static TimeZoneInfo OlsonTimeZoneToTimeZoneInfo(string olsonTimeZoneId)
        {
            var olsonWindowsTimes = new Dictionary<string, string>()
    {
        { "Africa/Bangui", "W. Central Africa Standard Time" },
        { "Africa/Cairo", "Egypt Standard Time" },
        { "Africa/Casablanca", "Morocco Standard Time" },
        { "Africa/Harare", "South Africa Standard Time" },
        { "Africa/Johannesburg", "South Africa Standard Time" },
        { "Africa/Lagos", "W. Central Africa Standard Time" },
        { "Africa/Monrovia", "Greenwich Standard Time" },
        { "Africa/Nairobi", "E. Africa Standard Time" },
        { "Africa/Windhoek", "Namibia Standard Time" },
        { "America/Anchorage", "Alaskan Standard Time" },
        { "America/Argentina/San_Juan", "Argentina Standard Time" },
        { "America/Asuncion", "Paraguay Standard Time" },
        { "America/Bahia", "Bahia Standard Time" },
        { "America/Bogota", "SA Pacific Standard Time" },
        { "America/Buenos_Aires", "Argentina Standard Time" },
        { "America/Caracas", "Venezuela Standard Time" },
        { "America/Cayenne", "SA Eastern Standard Time" },
        { "America/Chicago", "Central Standard Time" },
        { "America/Chihuahua", "Mountain Standard Time (Mexico)" },
        { "America/Cuiaba", "Central Brazilian Standard Time" },
        { "America/Denver", "Mountain Standard Time" },
        { "America/Fortaleza", "SA Eastern Standard Time" },
        { "America/Godthab", "Greenland Standard Time" },
        { "America/Guatemala", "Central America Standard Time" },
        { "America/Halifax", "Atlantic Standard Time" },
        { "America/Indianapolis", "US Eastern Standard Time" },
        { "America/Indiana/Indianapolis", "US Eastern Standard Time" },
        { "America/La_Paz", "SA Western Standard Time" },
        { "America/Los_Angeles", "Pacific Standard Time" },
        { "America/Mexico_City", "Mexico Standard Time" },
        { "America/Montevideo", "Montevideo Standard Time" },
        { "America/New_York", "Eastern Standard Time" },
        { "America/Noronha", "UTC-02" },
        { "America/Phoenix", "US Mountain Standard Time" },
        { "America/Regina", "Canada Central Standard Time" },
        { "America/Santa_Isabel", "Pacific Standard Time (Mexico)" },
        { "America/Santiago", "Pacific SA Standard Time" },
        { "America/Sao_Paulo", "E. South America Standard Time" },
        { "America/St_Johns", "Newfoundland Standard Time" },
        { "America/Tijuana", "Pacific Standard Time" },
        { "Antarctica/McMurdo", "New Zealand Standard Time" },
        { "Atlantic/South_Georgia", "UTC-02" },
        { "Asia/Almaty", "Central Asia Standard Time" },
        { "Asia/Amman", "Jordan Standard Time" },
        { "Asia/Baghdad", "Arabic Standard Time" },
        { "Asia/Baku", "Azerbaijan Standard Time" },
        { "Asia/Bangkok", "SE Asia Standard Time" },
        { "Asia/Beirut", "Middle East Standard Time" },
        { "Asia/Calcutta", "India Standard Time" },
        { "Asia/Colombo", "Sri Lanka Standard Time" },
        { "Asia/Damascus", "Syria Standard Time" },
        { "Asia/Dhaka", "Bangladesh Standard Time" },
        { "Asia/Dubai", "Arabian Standard Time" },
        { "Asia/Irkutsk", "North Asia East Standard Time" },
        { "Asia/Jerusalem", "Israel Standard Time" },
        { "Asia/Kabul", "Afghanistan Standard Time" },
        { "Asia/Kamchatka", "Kamchatka Standard Time" },
        { "Asia/Karachi", "Pakistan Standard Time" },
        { "Asia/Katmandu", "Nepal Standard Time" },
        { "Asia/Kolkata", "India Standard Time" },
        { "Asia/Krasnoyarsk", "North Asia Standard Time" },
        { "Asia/Kuala_Lumpur", "Singapore Standard Time" },
        { "Asia/Kuwait", "Arab Standard Time" },
        { "Asia/Magadan", "Magadan Standard Time" },
        { "Asia/Muscat", "Arabian Standard Time" },
        { "Asia/Novosibirsk", "N. Central Asia Standard Time" },
        { "Asia/Oral", "West Asia Standard Time" },
        { "Asia/Rangoon", "Myanmar Standard Time" },
        { "Asia/Riyadh", "Arab Standard Time" },
        { "Asia/Seoul", "Korea Standard Time" },
        { "Asia/Shanghai", "China Standard Time" },
        { "Asia/Singapore", "Singapore Standard Time" },
        { "Asia/Taipei", "Taipei Standard Time" },
        { "Asia/Tashkent", "West Asia Standard Time" },
        { "Asia/Tbilisi", "Georgian Standard Time" },
        { "Asia/Tehran", "Iran Standard Time" },
        { "Asia/Tokyo", "Tokyo Standard Time" },
        { "Asia/Ulaanbaatar", "Ulaanbaatar Standard Time" },
        { "Asia/Vladivostok", "Vladivostok Standard Time" },
        { "Asia/Yakutsk", "Yakutsk Standard Time" },
        { "Asia/Yekaterinburg", "Ekaterinburg Standard Time" },
        { "Asia/Yerevan", "Armenian Standard Time" },
        { "Atlantic/Azores", "Azores Standard Time" },
        { "Atlantic/Cape_Verde", "Cape Verde Standard Time" },
        { "Atlantic/Reykjavik", "Greenwich Standard Time" },
        { "Australia/Adelaide", "Cen. Australia Standard Time" },
        { "Australia/Brisbane", "E. Australia Standard Time" },
        { "Australia/Darwin", "AUS Central Standard Time" },
        { "Australia/Hobart", "Tasmania Standard Time" },
        { "Australia/Perth", "W. Australia Standard Time" },
        { "Australia/Sydney", "AUS Eastern Standard Time" },
        { "Etc/GMT", "UTC" },
        { "Etc/GMT+11", "UTC-11" },
        { "Etc/GMT+12", "Dateline Standard Time" },
        { "Etc/GMT+2", "UTC-02" },
        { "Etc/GMT-12", "UTC+12" },
        { "Europe/Amsterdam", "W. Europe Standard Time" },
        { "Europe/Athens", "GTB Standard Time" },
        { "Europe/Belgrade", "Central Europe Standard Time" },
        { "Europe/Berlin", "W. Europe Standard Time" },
        { "Europe/Brussels", "Romance Standard Time" },
        { "Europe/Budapest", "Central Europe Standard Time" },
        { "Europe/Dublin", "GMT Standard Time" },
        { "Europe/Helsinki", "FLE Standard Time" },
        { "Europe/Istanbul", "GTB Standard Time" },
        { "Europe/Kiev", "FLE Standard Time" },
        { "Europe/London", "GMT Standard Time" },
        { "Europe/Minsk", "E. Europe Standard Time" },
        { "Europe/Moscow", "Russian Standard Time" },
        { "Europe/Paris", "Romance Standard Time" },
        { "Europe/Sarajevo", "Central European Standard Time" },
        { "Europe/Warsaw", "Central European Standard Time" },
        { "Indian/Mauritius", "Mauritius Standard Time" },
        { "Pacific/Apia", "Samoa Standard Time" },
        { "Pacific/Auckland", "New Zealand Standard Time" },
        { "Pacific/Fiji", "Fiji Standard Time" },
        { "Pacific/Guadalcanal", "Central Pacific Standard Time" },
        { "Pacific/Guam", "West Pacific Standard Time" },
        { "Pacific/Honolulu", "Hawaiian Standard Time" },
        { "Pacific/Pago_Pago", "UTC-11" },
        { "Pacific/Port_Moresby", "West Pacific Standard Time" },
        { "Pacific/Tongatapu", "Tonga Standard Time" }
    };

            var windowsTimeZoneId = default(string);
            var windowsTimeZone = default(TimeZoneInfo);
            if (olsonWindowsTimes.TryGetValue(olsonTimeZoneId, out windowsTimeZoneId))
            {
                try { windowsTimeZone = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId); }
                catch (TimeZoneNotFoundException) { }
                catch (InvalidTimeZoneException) { }
            }
            return windowsTimeZone;
        }

        /// <summary>
        /// Converts a Windows time zone ID to an Olson time zone ID .
        /// </summary>
        /// <param name="olsonTimeZoneId">An Olson time zone ID. See http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/zone_tzid.html. </param>
        /// <returns>
        /// The TimeZoneInfo corresponding to the Olson time zone ID, 
        /// or null if you passed in an invalid Olson time zone ID.
        /// </returns>
        /// <remarks>
        /// See http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/zone_tzid.html
        /// </remarks>
        public static string TimeZoneInfoIDToOlsonTimeZoneID(TimeZoneInfo timeZoneInfo)
        {
            var olsonWindowsTimes = new Dictionary<string, string>()
    {
        { "Africa/Bangui", "W. Central Africa Standard Time" },
        { "Africa/Cairo", "Egypt Standard Time" },
        { "Africa/Casablanca", "Morocco Standard Time" },
        { "Africa/Harare", "South Africa Standard Time" },
        { "Africa/Johannesburg", "South Africa Standard Time" },
        { "Africa/Lagos", "W. Central Africa Standard Time" },
        { "Africa/Monrovia", "Greenwich Standard Time" },
        { "Africa/Nairobi", "E. Africa Standard Time" },
        { "Africa/Windhoek", "Namibia Standard Time" },
        { "America/Anchorage", "Alaskan Standard Time" },
        { "America/Argentina/San_Juan", "Argentina Standard Time" },
        { "America/Asuncion", "Paraguay Standard Time" },
        { "America/Bahia", "Bahia Standard Time" },
        { "America/Bogota", "SA Pacific Standard Time" },
        { "America/Buenos_Aires", "Argentina Standard Time" },
        { "America/Caracas", "Venezuela Standard Time" },
        { "America/Cayenne", "SA Eastern Standard Time" },
        { "America/Chicago", "Central Standard Time" },
        { "America/Chihuahua", "Mountain Standard Time (Mexico)" },
        { "America/Cuiaba", "Central Brazilian Standard Time" },
        { "America/Denver", "Mountain Standard Time" },
        { "America/Fortaleza", "SA Eastern Standard Time" },
        { "America/Godthab", "Greenland Standard Time" },
        { "America/Guatemala", "Central America Standard Time" },
        { "America/Halifax", "Atlantic Standard Time" },
        { "America/Indianapolis", "US Eastern Standard Time" },
        { "America/Indiana/Indianapolis", "US Eastern Standard Time" },
        { "America/La_Paz", "SA Western Standard Time" },
        { "America/Los_Angeles", "Pacific Standard Time" },
        { "America/Mexico_City", "Mexico Standard Time" },
        { "America/Montevideo", "Montevideo Standard Time" },
        { "America/New_York", "Eastern Standard Time" },
        { "America/Noronha", "UTC-02" },
        { "America/Phoenix", "US Mountain Standard Time" },
        { "America/Regina", "Canada Central Standard Time" },
        { "America/Santa_Isabel", "Pacific Standard Time (Mexico)" },
        { "America/Santiago", "Pacific SA Standard Time" },
        { "America/Sao_Paulo", "E. South America Standard Time" },
        { "America/St_Johns", "Newfoundland Standard Time" },
        { "America/Tijuana", "Pacific Standard Time" },
        { "Antarctica/McMurdo", "New Zealand Standard Time" },
        { "Atlantic/South_Georgia", "UTC-02" },
        { "Asia/Almaty", "Central Asia Standard Time" },
        { "Asia/Amman", "Jordan Standard Time" },
        { "Asia/Baghdad", "Arabic Standard Time" },
        { "Asia/Baku", "Azerbaijan Standard Time" },
        { "Asia/Bangkok", "SE Asia Standard Time" },
        { "Asia/Beirut", "Middle East Standard Time" },
        { "Asia/Calcutta", "India Standard Time" },
        { "Asia/Colombo", "Sri Lanka Standard Time" },
        { "Asia/Damascus", "Syria Standard Time" },
        { "Asia/Dhaka", "Bangladesh Standard Time" },
        { "Asia/Dubai", "Arabian Standard Time" },
        { "Asia/Irkutsk", "North Asia East Standard Time" },
        { "Asia/Jerusalem", "Israel Standard Time" },
        { "Asia/Kabul", "Afghanistan Standard Time" },
        { "Asia/Kamchatka", "Kamchatka Standard Time" },
        { "Asia/Karachi", "Pakistan Standard Time" },
        { "Asia/Katmandu", "Nepal Standard Time" },
        { "Asia/Kolkata", "India Standard Time" },
        { "Asia/Krasnoyarsk", "North Asia Standard Time" },
        { "Asia/Kuala_Lumpur", "Singapore Standard Time" },
        { "Asia/Kuwait", "Arab Standard Time" },
        { "Asia/Magadan", "Magadan Standard Time" },
        { "Asia/Muscat", "Arabian Standard Time" },
        { "Asia/Novosibirsk", "N. Central Asia Standard Time" },
        { "Asia/Oral", "West Asia Standard Time" },
        { "Asia/Rangoon", "Myanmar Standard Time" },
        { "Asia/Riyadh", "Arab Standard Time" },
        { "Asia/Seoul", "Korea Standard Time" },
        { "Asia/Shanghai", "China Standard Time" },
        { "Asia/Singapore", "Singapore Standard Time" },
        { "Asia/Taipei", "Taipei Standard Time" },
        { "Asia/Tashkent", "West Asia Standard Time" },
        { "Asia/Tbilisi", "Georgian Standard Time" },
        { "Asia/Tehran", "Iran Standard Time" },
        { "Asia/Tokyo", "Tokyo Standard Time" },
        { "Asia/Ulaanbaatar", "Ulaanbaatar Standard Time" },
        { "Asia/Vladivostok", "Vladivostok Standard Time" },
        { "Asia/Yakutsk", "Yakutsk Standard Time" },
        { "Asia/Yekaterinburg", "Ekaterinburg Standard Time" },
        { "Asia/Yerevan", "Armenian Standard Time" },
        { "Atlantic/Azores", "Azores Standard Time" },
        { "Atlantic/Cape_Verde", "Cape Verde Standard Time" },
        { "Atlantic/Reykjavik", "Greenwich Standard Time" },
        { "Australia/Adelaide", "Cen. Australia Standard Time" },
        { "Australia/Brisbane", "E. Australia Standard Time" },
        { "Australia/Darwin", "AUS Central Standard Time" },
        { "Australia/Hobart", "Tasmania Standard Time" },
        { "Australia/Perth", "W. Australia Standard Time" },
        { "Australia/Sydney", "AUS Eastern Standard Time" },
        { "Etc/GMT", "UTC" },
        { "Etc/GMT+11", "UTC-11" },
        { "Etc/GMT+12", "Dateline Standard Time" },
        { "Etc/GMT+2", "UTC-02" },
        { "Etc/GMT-12", "UTC+12" },
        { "Europe/Amsterdam", "W. Europe Standard Time" },
        { "Europe/Athens", "GTB Standard Time" },
        { "Europe/Belgrade", "Central Europe Standard Time" },
        { "Europe/Berlin", "W. Europe Standard Time" },
        { "Europe/Brussels", "Romance Standard Time" },
        { "Europe/Budapest", "Central Europe Standard Time" },
        { "Europe/Dublin", "GMT Standard Time" },
        { "Europe/Helsinki", "FLE Standard Time" },
        { "Europe/Istanbul", "GTB Standard Time" },
        { "Europe/Kiev", "FLE Standard Time" },
        { "Europe/London", "GMT Standard Time" },
        { "Europe/Minsk", "E. Europe Standard Time" },
        { "Europe/Moscow", "Russian Standard Time" },
        { "Europe/Paris", "Romance Standard Time" },
        { "Europe/Sarajevo", "Central European Standard Time" },
        { "Europe/Warsaw", "Central European Standard Time" },
        { "Indian/Mauritius", "Mauritius Standard Time" },
        { "Pacific/Apia", "Samoa Standard Time" },
        { "Pacific/Auckland", "New Zealand Standard Time" },
        { "Pacific/Fiji", "Fiji Standard Time" },
        { "Pacific/Guadalcanal", "Central Pacific Standard Time" },
        { "Pacific/Guam", "West Pacific Standard Time" },
        { "Pacific/Honolulu", "Hawaiian Standard Time" },
        { "Pacific/Pago_Pago", "UTC-11" },
        { "Pacific/Port_Moresby", "West Pacific Standard Time" },
        { "Pacific/Tongatapu", "Tonga Standard Time" }
    };

            var olsonTimeZoneId = olsonWindowsTimes.FirstOrDefault(q => q.Value == timeZoneInfo.Id).Key;
            return olsonTimeZoneId;
        }

    }






    public static class ObjectPutExtension
    {
        public static void set(this sc.AVEHICLE vh, sc.AVEHICLE new_vh)
        {
            vh.isTcpIpConnect = new_vh.isTcpIpConnect;
            vh.VEHICLE_TYPE = new_vh.VEHICLE_TYPE;
            vh.CUR_ADR_ID = new_vh.CUR_ADR_ID;
            vh.CUR_SEC_ID = new_vh.CUR_SEC_ID;
            vh.SEC_ENTRY_TIME = new_vh.SEC_ENTRY_TIME;
            vh.ACC_SEC_DIST = new_vh.ACC_SEC_DIST;
            vh.MODE_STATUS = new_vh.MODE_STATUS;
            vh.ACT_STATUS = new_vh.ACT_STATUS;
            vh.MCS_CMD = new_vh.MCS_CMD;
            vh.OHTC_CMD = new_vh.OHTC_CMD;
            vh.BLOCK_PAUSE = new_vh.BLOCK_PAUSE;
            vh.CMD_PAUSE = new_vh.CMD_PAUSE;
            vh.OBS_PAUSE = new_vh.OBS_PAUSE;
            vh.HID_PAUSE = new_vh.HID_PAUSE;
            vh.ERROR = new_vh.ERROR;
            vh.OBS_DIST = new_vh.OBS_DIST;
            vh.HAS_CST = new_vh.HAS_CST;
            vh.CST_ID = new_vh.CST_ID;
            vh.UPD_TIME = new_vh.UPD_TIME;
            vh.VEHICLE_ACC_DIST = new_vh.VEHICLE_ACC_DIST;
            vh.MANT_ACC_DIST = new_vh.MANT_ACC_DIST;
            vh.MANT_DATE = new_vh.MANT_DATE;
            vh.GRIP_COUNT = new_vh.GRIP_COUNT;
            vh.GRIP_MANT_COUNT = new_vh.GRIP_MANT_COUNT;
            vh.GRIP_MANT_DATE = new_vh.GRIP_MANT_DATE;
            vh.NODE_ADR = new_vh.NODE_ADR;
            vh.IS_PARKING = new_vh.IS_PARKING;
            vh.PARK_TIME = new_vh.PARK_TIME;
            vh.PARK_ADR_ID = new_vh.PARK_ADR_ID;
            vh.IS_CYCLING = new_vh.IS_CYCLING;
            vh.CYCLERUN_TIME = new_vh.CYCLERUN_TIME;
            vh.CYCLERUN_ID = new_vh.CYCLERUN_ID;

            vh.PredictPath = new_vh.PredictPath;
            vh.CyclingPath = new_vh.CyclingPath;
            vh.startAdr = new_vh.startAdr;
            vh.FromAdr = new_vh.FromAdr;
            vh.ToAdr = new_vh.ToAdr;
            vh.CmdType = new_vh.CmdType;
            vh.vh_CMD_Status = new_vh.vh_CMD_Status;
            vh.VhRecentTranEvent = new_vh.VhRecentTranEvent;

            vh.WillPassSectionID = new_vh.WillPassSectionID;
            vh.procProgress_Percen = new_vh.procProgress_Percen;
        }

        public static void set(this sc.AVEHICLE vh, sc.ProtocolFormat.OHTMessage.VEHICLE_INFO new_vh)
        {
            int vh_type = (int)new_vh.VEHICLETYPE;
            int cmd_type = (int)new_vh.CmdType;
            int cmd_status = (int)new_vh.VhCMDStatus;

            vh.isTcpIpConnect = new_vh.IsTcpIpConnect;
            vh.VEHICLE_TYPE = (sc.E_VH_TYPE)vh_type;
            vh.CUR_ADR_ID = new_vh.CURADRID;
            vh.CUR_SEC_ID = new_vh.CURSECID;
            //vh.SEC_ENTRY_TIME = new_vh.SEC_ENTRY_TIME;
            vh.ACC_SEC_DIST = new_vh.ACCSECDIST;
            vh.MODE_STATUS = new_vh.MODESTATUS;
            vh.ACT_STATUS = new_vh.ACTSTATUS;
            vh.MCS_CMD = new_vh.MCSCMD;
            if (!SCUtility.isMatche(vh.OHTC_CMD, new_vh.OHTCCMD))
            {
                vh.OHTC_CMD = new_vh.OHTCCMD;
                vh.NotifyVhExcuteCMDStatusChange();
            }
            vh.PauseStatus = new_vh.PauseStatus;
            vh.BLOCK_PAUSE = new_vh.BLOCKPAUSE;
            vh.CMD_PAUSE = new_vh.CMDPAUSE;
            vh.OBS_PAUSE = new_vh.OBSPAUSE;
            vh.HID_PAUSE = new_vh.HIDPAUSE;
            vh.SAFETY_DOOR_PAUSE = new_vh.SAFETYDOORPAUSE;
            vh.EARTHQUAKE_PAUSE = new_vh.EARTHQUAKEPAUSE;
            vh.ERROR = new_vh.ERROR;
            vh.OBS_DIST = new_vh.OBSDIST;
            vh.HAS_CST = new_vh.HASCST;
            vh.CST_ID = new_vh.CSTID;
            //vh.UPD_TIME = new_vh.UPDTIME;
            vh.VEHICLE_ACC_DIST = new_vh.VEHICLEACCDIST;
            vh.MANT_ACC_DIST = new_vh.MANTACCDIST;
            //vh.MANT_DATE = new_vh.MANTDATE;
            vh.GRIP_COUNT = new_vh.GRIPCOUNT;
            vh.GRIP_MANT_COUNT = new_vh.GRIPMANTCOUNT;
            //   vh.GRIP_MANT_DATE = new_vh.GRIPMANTDATE;
            //vh.NODE_ADR = new_vh.NODE_ADR;
            vh.IS_PARKING = new_vh.ISPARKING;
            //   vh.PARK_TIME = new_vh.PARKTIME;
            vh.PARK_ADR_ID = new_vh.PARKADRID;
            vh.IS_CYCLING = new_vh.ISCYCLING;
            //  vh.CYCLERUN_TIME = new_vh.CYCLERUNTIME;
            vh.CYCLERUN_ID = new_vh.CYCLERUNID;

            vh.PredictPath = new_vh.PredictPath.ToArray();
            vh.CyclingPath = new_vh.CyclingPath.ToArray();
            vh.startAdr = new_vh.StartAdr;
            vh.FromAdr = new_vh.FromAdr;
            vh.ToAdr = new_vh.ToAdr;
            vh.CMD_Priority = new_vh.CMDPRIOTITY;
            vh.CMD_CST_ID = new_vh.CMDCSTID;
            vh.Speed = new_vh.Speed;

            vh.ObsVehicleID = new_vh.ObsVehicleID;
            vh.Alarms = new_vh.Alarms.ToList();
            vh.CmdType = (sc.E_CMD_TYPE)cmd_type;
            vh.vh_CMD_Status = (sc.E_CMD_STATUS)cmd_status;
            vh.VhRecentTranEvent = new_vh.VhRecentTranEvent;
            vh.WillPassSectionID = new_vh.WillPassSectionID.ToList();
            vh.procProgress_Percen = new_vh.ProcProgressPercen;
            vh.State = new_vh.State;
            vh.UPD_TIME = (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(new_vh.UPDTIME.Seconds);
        }




        public static void set(this sc.Data.VO.MaintainSpace mts, sc.ProtocolFormat.OHTMessage.MTL_MTS_INFO new_mtl_mts)
        {
            mts.Plc_Link_Stat = new_mtl_mts.NetworkLink ? sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
            mts.Is_Eq_Alive = new_mtl_mts.Alive;
            mts.MTxMode = new_mtl_mts.Mode ? sc.ProtocolFormat.OHTMessage.MTxMode.Auto : sc.ProtocolFormat.OHTMessage.MTxMode.Manual;
            mts.Interlock = new_mtl_mts.Interlock;
            mts.CurrentCarID = new_mtl_mts.CarID;
            mts.SynchronizeTime = Convert.ToDateTime(new_mtl_mts.SynchronizeTime);
        }

        public static void set(this sc.Data.VO.MaintainLift mtl, sc.ProtocolFormat.OHTMessage.MTL_MTS_INFO new_mtl_mts)
        {
            mtl.Plc_Link_Stat = new_mtl_mts.NetworkLink ? sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
            mtl.Is_Eq_Alive = new_mtl_mts.Alive;
            mtl.MTxMode = new_mtl_mts.Mode ? sc.ProtocolFormat.OHTMessage.MTxMode.Auto : sc.ProtocolFormat.OHTMessage.MTxMode.Manual;
            mtl.Interlock = new_mtl_mts.Interlock;
            mtl.CurrentCarID = new_mtl_mts.CarID;
            mtl.MTLLocation = new_mtl_mts.MTLLocation.Trim()== MTLLocation.Upper.ToString()? MTLLocation.Upper:
                new_mtl_mts.MTLLocation.Trim() == MTLLocation.Bottorn.ToString()? MTLLocation.Bottorn:
                MTLLocation.None;
            mtl.SynchronizeTime = Convert.ToDateTime(new_mtl_mts.SynchronizeTime);
        }


        public static void set(this sc.ALINE line, sc.ProtocolFormat.OHTMessage.LINE_INFO new_line)
        {
            line.Secs_Link_Stat = new_line.Host == sc.ProtocolFormat.OHTMessage.LinkStatus.LinkOk ?
                                     sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
            line.DetectionSystemExist = new_line.IMS == sc.ProtocolFormat.OHTMessage.LinkStatus.LinkOk ?
                                      sc.App.SCAppConstants.ExistStatus.Exist : sc.App.SCAppConstants.ExistStatus.NoExist;
            line.Secs_Link_Stat = new_line.Host == sc.ProtocolFormat.OHTMessage.LinkStatus.LinkOk ?
                         sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
            line.Host_Control_State = new_line.HostMode == sc.ProtocolFormat.OHTMessage.HostMode.OnlineRemote ?
                         sc.App.SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote :
                         (new_line.HostMode == sc.ProtocolFormat.OHTMessage.HostMode.OnlineLocal ? sc.App.SCAppConstants.LineHostControlState.HostControlState.On_Line_Local : sc.App.SCAppConstants.LineHostControlState.HostControlState.EQ_Off_line);
            if (new_line.TSCState == sc.ProtocolFormat.OHTMessage.TSCState.Auto)
            {
                line.SCStats = sc.ALINE.TSCState.AUTO;
            }
            else if (new_line.TSCState == sc.ProtocolFormat.OHTMessage.TSCState.Paused)
            {
                line.SCStats = sc.ALINE.TSCState.PAUSED;
            }
            else if (new_line.TSCState == sc.ProtocolFormat.OHTMessage.TSCState.Pausing)
            {
                line.SCStats = sc.ALINE.TSCState.PAUSING;
            }
            else if (new_line.TSCState == sc.ProtocolFormat.OHTMessage.TSCState.Tscint)
            {
                line.SCStats = sc.ALINE.TSCState.TSC_INIT;
            }
            else if (new_line.TSCState == sc.ProtocolFormat.OHTMessage.TSCState.Tscnone)
            {
                line.SCStats = sc.ALINE.TSCState.NONE;
            }

            line.CurrntVehicleModeAutoRemoteCount = (ushort)new_line.CurrntVehicleModeAutoRemoteCount;
            line.CurrntVehicleModeAutoLoaclCount = (ushort)new_line.CurrntVehicleModeAutoLoaclCount;
            line.CurrntVehicleStatusIdelCount = (ushort)new_line.CurrntVehicleStatusIdelCount;
            line.CurrntVehicleStatusErrorCount = (ushort)new_line.CurrntVehicleStatusErrorCount;
            line.CurrntCSTStatueTransferCount = (ushort)new_line.CurrntCSTStatueTransferCount;
            line.CurrntCSTStatueWaitingCount = (ushort)new_line.CurrntCSTStatueWaitingCount;
            line.CurrntHostCommandTransferStatueAssignedCount = (ushort)new_line.CurrntHostCommandTransferStatueAssignedCount;
            line.CurrntHostCommandTransferStatueWaitingCounr = (ushort)new_line.CurrntHostCommandTransferStatueWaitingCounr;
            line.NotifyLineStatusChange();

        }

        public static void set(this sc.ALINE line, sc.ProtocolFormat.OHTMessage.ONLINE_CHECK_INFO new_online_check_info)
        {
            line.EnhancedTransfersChecked = new_online_check_info.EnhancedTransfersChecked;
            line.UnitAlarmStateListChecked = new_online_check_info.UnitAlarmStateListChecked;
            line.TSCStateChecked = new_online_check_info.TSCStateChecked;
            line.EnhancedVehiclesChecked = new_online_check_info.EnhancedVehiclesChecked;
            line.CurrentStateChecked = new_online_check_info.CurrentStateChecked;
            line.CurrentPortStateChecked = new_online_check_info.CurrentPortStateChecked;
            line.LaneCutListChecked = new_online_check_info.LaneCutListChecked;
            line.EnhancedCarriersChecked = new_online_check_info.EnhancedCarriersChecked;

        }


        public static void set(this sc.ALINE line, sc.ProtocolFormat.OHTMessage.PING_CHECK_INFO new_ping_check_info)
        {
            line.MCSConnectionSuccess = new_ping_check_info.MCSConnectionSuccess;
            line.RouterConnectionSuccess = new_ping_check_info.RouterConnectionSuccess;
            line.OHT1ConnectionSuccess = new_ping_check_info.OHT1ConnectionSuccess;
            line.OHT2ConnectionSuccess = new_ping_check_info.OHT2ConnectionSuccess;
            line.OHT3ConnectionSuccess = new_ping_check_info.OHT3ConnectionSuccess;
            line.OHT4ConnectionSuccess = new_ping_check_info.OHT4ConnectionSuccess;
            line.OHT5ConnectionSuccess = new_ping_check_info.OHT5ConnectionSuccess;
            line.OHT6ConnectionSuccess = new_ping_check_info.OHT6ConnectionSuccess;
            line.OHT7ConnectionSuccess = new_ping_check_info.OHT7ConnectionSuccess;
            line.OHT8ConnectionSuccess = new_ping_check_info.OHT8ConnectionSuccess;
            line.OHT9ConnectionSuccess = new_ping_check_info.OHT9ConnectionSuccess;
            line.OHT10ConnectionSuccess = new_ping_check_info.OHT10ConnectionSuccess;
            line.OHT11ConnectionSuccess = new_ping_check_info.OHT11ConnectionSuccess;
            line.OHT12ConnectionSuccess = new_ping_check_info.OHT12ConnectionSuccess;
            line.OHT13ConnectionSuccess = new_ping_check_info.OHT13ConnectionSuccess;
            line.OHT14ConnectionSuccess = new_ping_check_info.OHT14ConnectionSuccess;
            line.MTLConnectionSuccess = new_ping_check_info.MTLConnectionSuccess;
            line.MTSConnectionSuccess = new_ping_check_info.MTSConnectionSuccess;
            line.MTS2ConnectionSuccess = new_ping_check_info.MTS2ConnectionSuccess;
            line.HID1ConnectionSuccess = new_ping_check_info.HID1ConnectionSuccess;
            line.HID2ConnectionSuccess = new_ping_check_info.HID2ConnectionSuccess;
            line.HID3ConnectionSuccess = new_ping_check_info.HID3ConnectionSuccess;
            line.HID4ConnectionSuccess = new_ping_check_info.HID4ConnectionSuccess;
            line.Adam1ConnectionSuccess = new_ping_check_info.Adam1ConnectionSuccess;
            line.Adam2ConnectionSuccess = new_ping_check_info.Adam2ConnectionSuccess;
            line.Adam3ConnectionSuccess = new_ping_check_info.Adam3ConnectionSuccess;
            line.Adam4ConnectionSuccess = new_ping_check_info.Adam4ConnectionSuccess;
            line.AP1ConnectionSuccess = new_ping_check_info.AP1ConnectionSuccess;
            line.AP2ConnectionSuccess = new_ping_check_info.AP2ConnectionSuccess;
            line.AP3ConnectionSuccess = new_ping_check_info.AP3ConnectionSuccess;
            line.AP4ConnectionSuccess = new_ping_check_info.AP4ConnectionSuccess;
            line.AP5ConnectionSuccess = new_ping_check_info.AP5ConnectionSuccess;
            line.AP6ConnectionSuccess = new_ping_check_info.AP6ConnectionSuccess;
            line.AP7ConnectionSuccess = new_ping_check_info.AP7ConnectionSuccess;
            line.AP8ConnectionSuccess = new_ping_check_info.AP8ConnectionSuccess;
            line.AP9ConnectionSuccess = new_ping_check_info.AP9ConnectionSuccess;
            line.AP10ConnectionSuccess = new_ping_check_info.AP10ConnectionSuccess;
        }

        public static void set(this sc.ALINE line, sc.ProtocolFormat.OHTMessage.TRANSFER_INFO new_transfer_info)
        {
            line.MCSCommandAutoAssign = new_transfer_info.MCSCommandAutoAssign;
        }

        public static void set(this List<sc.Data.VO.MPCTipMessage> tipMsgs, sc.ProtocolFormat.OHTMessage.TIP_MESSAGE_COLLECTION gpbTipMsgs)
        {
            tipMsgs.Clear();
            foreach (var info in gpbTipMsgs.TIPMESSAGEINFOS)
            {
                sc.Data.VO.MPCTipMessage msg = new sc.Data.VO.MPCTipMessage()
                {
                    Msg = info.Message,
                    MsgLevel = info.MsgLevel,
                    Time = info.Time,
                    XID = info.XID
                };
                tipMsgs.Add(msg);
            }
        }
        public static void set(this sc.APORTSTATION oldPortStation, sc.APORTSTATION newPortStation)
        {
            oldPortStation.PORT_SERVICE_STATUS = newPortStation.PORT_SERVICE_STATUS;
            oldPortStation.PRIORITY = newPortStation.PRIORITY;
            oldPortStation.PORT_STATUS = newPortStation.PORT_STATUS;
        }

        public static void set(this sc.ASEGMENT oldSegment, sc.ASEGMENT newSegment)
        {
            oldSegment.DISABLE_FLAG_SYSTEM = newSegment.DISABLE_FLAG_SYSTEM;
            oldSegment.DISABLE_FLAG_HID = newSegment.DISABLE_FLAG_HID;
            oldSegment.DISABLE_FLAG_SAFETY = newSegment.DISABLE_FLAG_SAFETY;
            oldSegment.DISABLE_FLAG_USER = newSegment.DISABLE_FLAG_USER;
            oldSegment.DISABLE_TIME = newSegment.DISABLE_TIME;
            oldSegment.PRE_DISABLE_TIME = newSegment.PRE_DISABLE_TIME;
            oldSegment.PRE_DISABLE_FLAG = newSegment.PRE_DISABLE_FLAG;
            oldSegment.DIR = newSegment.DIR;
            oldSegment.NOTE = newSegment.NOTE;
            oldSegment.RESERVE_FIELD = newSegment.RESERVE_FIELD;
            oldSegment.SEG_TYPE = newSegment.SEG_TYPE;
            oldSegment.SPECIAL_MARK = newSegment.SPECIAL_MARK;
            oldSegment.SEG_NUM = newSegment.SEG_NUM;
            oldSegment.STATUS = newSegment.STATUS;
        }

        public static void set(this sc.UASUSR oldUser, sc.UASUSR newUser)
        {
            oldUser.PASSWD = newUser.PASSWD;
            oldUser.POWER_USER_FLG = newUser.POWER_USER_FLG;
            oldUser.USER_GRP = newUser.USER_GRP;
            oldUser.USER_NAME = newUser.USER_NAME;
            oldUser.ADMIN_FLG = newUser.ADMIN_FLG;
            oldUser.DISABLE_FLG = newUser.DISABLE_FLG;
        }

    }
}

