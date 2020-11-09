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
    public class HIDToOHxC_ChargeInfoPH2 : PLC_FunBase
    {
        public DateTime Timestamp;

        [PLCElement(ValueName = "HID_TO_OHXC_ALIVE_PH2")]
        public UInt16 Alive;
        [PLCElement(ValueName = "HID_TO_OHXC_HID_ID_PH2")]
        public UInt16 HID_ID;
        [PLCElement(ValueName = "HID_TO_OHXC_V_UNIT_PH2")]
        public UInt16 V_Unit;
        [PLCElement(ValueName = "HID_TO_OHXC_V_DOT_PH2")]
        public UInt16 V_Dot;
        [PLCElement(ValueName = "HID_TO_OHXC_A_UNIT_PH2")]
        public UInt16 A_Unit;
        [PLCElement(ValueName = "HID_TO_OHXC_A_DOT_PH2")]
        public UInt16 A_Dot;
        [PLCElement(ValueName = "HID_TO_OHXC_W_UNIT_PH2")]
        public UInt16 W_Unit;
        [PLCElement(ValueName = "HID_TO_OHXC_W_DOT_PH2")]
        public UInt16 W_Dot;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_UNIT_PH2")]
        public UInt16 Hour_Unit;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_DOT_PH2")]
        public UInt16 Hour_Dot;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_SIGMA_Hi_WORD_PH2")]
        public UInt16 Hour_Sigma_High_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_SIGMA_Lo_WORD_PH2")]
        public UInt16 Hour_Sigma_Low_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_POSITIVE_Hi_WORD_PH2")]
        public UInt16 Hour_Positive_High_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_POSITIVE_Lo_WORD_PH2")]
        public UInt16 Hour_Positive_Low_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_NEGATIVE_Hi_WORD_PH2")]
        public UInt16 Hour_Negative_High_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_HOUR_NEGATIVE_Lo_WORD_PH2")]
        public UInt16 Hour_Negative_Low_Word;
        [PLCElement(ValueName = "HID_TO_OHXC_VR_PH2")]
        public UInt16 VR_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_VS_PH2")]
        public UInt16 VS_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_VT_PH2")]
        public UInt16 VT_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_SIGMA_V_PH2")]
        public UInt16 Sigma_V_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_AR_PH2")]
        public UInt16 AR_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_AS_PH2")]
        public UInt16 AS_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_AT_PH2")]
        public UInt16 AT_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_SIGMA_A_PH2")]
        public UInt16 Sigma_A_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_WR_PH2")]
        public UInt16 WR_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_WS_PH2")]
        public UInt16 WS_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_WT_PH2")]
        public UInt16 WT_Source;
        [PLCElement(ValueName = "HID_TO_OHXC_SIGMA_W_PH2")]
        public UInt16 Sigma_W_Source;

        public double Hour_Sigma_Converted { get { return convertValueTwoWord(Hour_Unit, Hour_Dot, Hour_Sigma_High_Word, Hour_Sigma_Low_Word); } set { } }

        public double Hour_Positive_Converted { get { return convertValueTwoWord(Hour_Unit, Hour_Dot, Hour_Positive_High_Word, Hour_Positive_Low_Word); } set { } }
        public double Hour_Negative_Converted { get { return convertValueTwoWord(Hour_Unit, Hour_Dot, Hour_Negative_High_Word, Hour_Negative_Low_Word); } set { } }
        public double VR_Converted { get { return convertValueOneWord(V_Unit, V_Dot, VR_Source); } set { } }
        public double VS_Converted { get { return convertValueOneWord(V_Unit, V_Dot, VS_Source); } set { } }
        public double VT_Converted { get { return convertValueOneWord(V_Unit, V_Dot, VT_Source); } set { } }
        public double Sigma_V_Converted { get { return convertValueOneWord(V_Unit, V_Dot, Sigma_V_Source); } set { } }
        public double AR_Converted { get { return convertValueOneWord(A_Unit, A_Dot, AR_Source); } set { } }
        public double AS_Converted { get { return convertValueOneWord(A_Unit, A_Dot, AS_Source); } set { } }
        public double AT_Converted { get { return convertValueOneWord(A_Unit, A_Dot, AT_Source); } set { } }
        public double Sigma_A_Converted { get { return convertValueOneWord(A_Unit, A_Dot, Sigma_A_Source); } set { } }
        public double WR_Converted { get { return convertValueOneWord(W_Unit, W_Dot, WR_Source); } set { } }
        public double WS_Converted { get { return convertValueOneWord(W_Unit, W_Dot, WS_Source); } set { } }
        public double WT_Converted { get { return convertValueOneWord(W_Unit, W_Dot, WT_Source); } set { } }
        public double Sigma_W_Converted { get { return convertValueOneWord(W_Unit, W_Dot, Sigma_W_Source); } set { } }

        private double convertValueOneWord(UInt64 unit, UInt64 dot, UInt64 source_value)
        {
            double convertValue;
            double temp;
            double unit_d = Convert.ToDouble(unit);
            double dot_d = Convert.ToDouble(dot);
            double multiplier = Math.Pow(10, (unit_d - dot_d));
            temp = source_value * multiplier;
            convertValue = temp;
            return convertValue;
        }
        private double convertValueTwoWord(UInt64 unit, UInt64 dot, UInt64 source_value_high_word, UInt64 source_value_low_word)
        {
            double convertValue;
            double temp;
            double source_value = (source_value_high_word * 65536) + source_value_low_word;
            double unit_d = Convert.ToDouble(unit);
            double dot_d = Convert.ToDouble(dot);
            double source_value_d = Convert.ToDouble(source_value);
            double multiplier = Math.Pow(10, (unit_d - dot_d));
            //UInt64 multiplier = Convert.ToUInt64(Math.Pow(10, (unit_d - dot_d)));
            temp = source_value_d * multiplier;
            convertValue = temp;
            return convertValue;
        }

        public override string ToString()
        {
            string sJson = Newtonsoft.Json.JsonConvert.SerializeObject(this, JsHelper.jsBooleanArrayConverter, JsHelper.jsTimeConverter);
            sJson = sJson.Replace(nameof(Timestamp), "@timestamp");
            return sJson;
        }
    }




}
