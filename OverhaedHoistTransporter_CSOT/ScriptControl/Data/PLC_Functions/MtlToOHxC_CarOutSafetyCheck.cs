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
    class MtlToOHxC_CarOutSafetyCheck : PLC_FunBase
    {
        [PLCElement(ValueName = "MTL_TO_OHXC_U2D_SAFETY_CHECK")]
        public bool SafetyCheck;
    }

    class MtlToOHxC_CarOutSafetyCheck_PH2 : PLC_FunBase
    {
        [PLCElement(ValueName = "MTL_TO_OHXC_U2D_SAFETY_CHECK_PH2")]
        public bool SafetyCheck;
    }
}
