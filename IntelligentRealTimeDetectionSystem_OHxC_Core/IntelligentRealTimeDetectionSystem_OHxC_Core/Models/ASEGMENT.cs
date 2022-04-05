using System;
using System.Collections.Generic;
using static com.mirle.iibg3k0.ids.ohxc.App.CSAppConstants;

namespace com.mirle.iibg3k0.ids.ohxc.Models
{
    public partial class ASEGMENT
    {
        public string SEG_NUM { get; set; }
        public E_SEG_STATUS STATUS { get; set; }
        public int SEG_TYPE { get; set; }
        public int? SPECIAL_MARK { get; set; }
        public string RESERVE_FIELD { get; set; }
        public string NOTE { get; set; }
        public int DIR { get; set; }
        public bool PRE_DISABLE_FLAG { get; set; }
        public DateTime? PRE_DISABLE_TIME { get; set; }
        public DateTime? DISABLE_TIME { get; set; }
    }
}
