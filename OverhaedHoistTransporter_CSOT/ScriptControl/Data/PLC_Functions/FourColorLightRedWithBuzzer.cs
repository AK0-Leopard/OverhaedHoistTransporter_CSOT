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
    class FourColorLightRedWithBuzzer : PLC_FunBase
    {
        [PLCElement(ValueName = "BUZZER")]
        public Boolean Buzzer;
        [PLCElement(ValueName = "RED_LIGHT")]
        public Boolean RedLight;
    }
}
