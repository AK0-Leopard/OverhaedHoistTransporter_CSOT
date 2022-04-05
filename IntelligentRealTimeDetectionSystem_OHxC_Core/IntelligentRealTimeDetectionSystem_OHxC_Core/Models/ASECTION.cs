using System;
using System.Collections.Generic;

namespace com.mirle.iibg3k0.ids.ohxc.Models
{
    public partial class ASECTION
    {
        public string SEC_ID { get; set; }
        public int? SEC_ORDER_NUM { get; set; }
        public int SEG_ORDER_NUM { get; set; }
        public int DIRC_DRIV { get; set; }
        public int DIRC_GUID { get; set; }
        public int? AREA_SECSOR { get; set; }
        public string SEG_NUM { get; set; }
        public string FROM_ADR_ID { get; set; }
        public string TO_ADR_ID { get; set; }
        public double SEC_DIS { get; set; }
        public double? SEC_SPD { get; set; }
        public int DIS_FROM_ORIGIN { get; set; }
        public int CDOG_1 { get; set; }
        public string CHG_SEG_NUM_1 { get; set; }
        public int CDOG_2 { get; set; }
        public string CHG_SEG_NUM_2 { get; set; }
        public int PRE_BLO_REQ { get; set; }
        public int SEC_TYPE { get; set; }
        public int SEC_DIR { get; set; }
        public int PADDING { get; set; }
        public int ENB_CHG_G_AREA { get; set; }
        public int PRE_DIV { get; set; }
        public int PRE_ADD_REPR { get; set; }
        public int OBS_SENSOR { get; set; }
        public string SUB_VER { get; set; }
        public DateTime? ADD_TIME { get; set; }
        public string ADD_USER { get; set; }
        public DateTime? UPD_TIME { get; set; }
        public string UPD_USER { get; set; }
        public int START_BC1 { get; set; }
        public int END_BC1 { get; set; }
        public int START_BC2 { get; set; }
        public int END_BC2 { get; set; }
        public int START_BC3 { get; set; }
        public int END_BC3 { get; set; }
    }
}
