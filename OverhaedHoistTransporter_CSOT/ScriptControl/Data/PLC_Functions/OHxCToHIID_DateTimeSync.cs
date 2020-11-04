using com.mirle.ibg3k0.sc.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.PLC_Functions
{
    class OHxCToHID_DateTimeSync : PLC_FunBase
    {
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_YEAR")]
        public uint Year;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_MONTH")]
        public uint Month;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_DAY")]
        public uint Day;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_HOUR")]
        public uint Hour;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_MINUTE")]
        public uint Min;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_SECOND")]
        public uint Sec;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_INDEX", IsIndexProp = true)]
        public uint Index;

    }


    class OHxCToHID_DateTimeSync_PH2 : PLC_FunBase
    {
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_YEAR_PH2")]
        public uint Year;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_MONTH_PH2")]
        public uint Month;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_DAY_PH2")]
        public uint Day;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_HOUR_PH2")]
        public uint Hour;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_MINUTE_PH2")]
        public uint Min;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_SYNC_COMMAND_SECOND_PH2")]
        public uint Sec;
        [PLCElement(ValueName = "OHTC_TO_HID_DATE_TIME_INDEX_PH2", IsIndexProp = true)]
        public uint Index;

    }

}
