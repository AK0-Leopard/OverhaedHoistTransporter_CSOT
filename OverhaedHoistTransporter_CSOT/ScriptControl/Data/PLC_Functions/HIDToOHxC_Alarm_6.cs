﻿using com.mirle.ibg3k0.sc.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.PLC_Functions
{
    public class HIDToOHxC_Alarm_6 : PLC_FunBase
    {
        public string EQ_ID;
        [PLCElement(ValueName = "HID_TO_OHXC_ALARM_6")]
        public bool Alarm_6_Happend;
    }


}
