using com.mirle.ibg3k0.ohxc.winform.BLL;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Vo.ObjectRelayVo
{
    public class AlarmObjToShow
    {
        ALARM alarm;
        AlarmBLL alarmBLL;
        public AlarmObjToShow(ALARM _alarm, AlarmBLL _alarmBLL)
        {
            alarm = _alarm;
            alarmBLL = _alarmBLL;
            AlarmMap alarmMap = alarmBLL.cache.getSuggestion("VH_LINE", ALAM_CODE);
            if (alarmMap != null)
            {
                SUGGESTION = alarmMap.SUGGESTION;
                POSSIBLE_CAUSES = alarmMap.POSSIBLE_CAUSES;
            }
        }
        public string EQPT_ID { get { return alarm.EQPT_ID; } }
        public int UNIT_NUM { get { return alarm.UNIT_NUM; } }
        public DateTime RPT_DATE_TIME { get { return alarm.RPT_DATE_TIME; } }
        public DateTime? CLEAR_DATE_TIME { get { return alarm.CLEAR_DATE_TIME; } }
        public string ALAM_CODE { get { return alarm.ALAM_CODE; } }
        public E_ALARM_LVL ALAM_LVL { get { return alarm.ALAM_LVL; } }
        public string ALAM_STAT
        {
            get
            {
                switch (alarm.ALAM_STAT)
                {
                    case ErrorStatus.ErrSet:
                        return "Set";
                    case ErrorStatus.ErrReset:
                        return "Reset";
                    default:
                        return "";
                }
            }
        }
        public string ALAM_DESC { get { return alarm.ALAM_DESC; } }
        public string ADDRESS_ID { get { return alarm.ADDRESS_ID; } }
        public string SUGGESTION { get; } = "";
        public string POSSIBLE_CAUSES { get; } = "";
    }
}
