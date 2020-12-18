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
    class OHxCToHID_SilentIndexPH2 : PLC_FunBase
    {
        [PLCElement(ValueName = "OHXC_TO_HID_SILENT_INDEX_PH2", IsIndexProp = true)]
        public uint Index;
    }


}