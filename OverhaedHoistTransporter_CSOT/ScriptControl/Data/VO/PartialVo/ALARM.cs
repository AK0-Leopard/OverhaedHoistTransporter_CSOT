using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace com.mirle.ibg3k0.sc
{
    public partial class ALARM
    {
        public static ConcurrentDictionary<string, ALARM> AlarmInfoList { get; private set; } = new ConcurrentDictionary<string, ALARM>();

        public static void refreshAlarmInfoList(List<ALARM> currentAlarms)
        {
            try
            {
                List<string> new_current_alarm = currentAlarms.Select(alarm => alarm.CompositeKey).ToList();
                List<string> old_current_alarm = ALARM.AlarmInfoList.Keys.ToList();

                List<string> new_add_mcs_cmds = new_current_alarm.Except(old_current_alarm).ToList();
                //1.新增多出來的命令
                foreach (string new_alarm_key in new_add_mcs_cmds)
                {
                    ALARM new_alarm_obj = new ALARM();
                    var current_cmd = currentAlarms.Where(alarm => SCUtility.isMatche(alarm.CompositeKey, new_alarm_key)).FirstOrDefault();
                    if (current_cmd == null) continue;
                    new_alarm_obj.put(current_cmd);
                    ALARM.AlarmInfoList.TryAdd(new_alarm_key, new_alarm_obj);
                }
                //2.刪除已經結束的命令
                List<string> will_del_mcs_cmds = old_current_alarm.Except(new_current_alarm).ToList();
                foreach (string old_key in will_del_mcs_cmds)
                {
                    ALARM.AlarmInfoList.TryRemove(old_key, out var _);
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception");
            }
        }

        public void put(ALARM current_cmd)
        {
            this.EQPT_ID = current_cmd.EQPT_ID;
            this.UNIT_NUM = current_cmd.UNIT_NUM;
            this.RPT_DATE_TIME = current_cmd.RPT_DATE_TIME;
            this.CLEAR_DATE_TIME = current_cmd.CLEAR_DATE_TIME;
            this.ALAM_CODE = current_cmd.ALAM_CODE;
            this.ALAM_LVL = current_cmd.ALAM_LVL;
            this.ALAM_STAT = current_cmd.ALAM_STAT;
            this.ALAM_DESC = current_cmd.ALAM_DESC;
            this.ADDRESS_ID = current_cmd.ADDRESS_ID;
        }

        public string CompositeKey
        {
            get
            {
                return $"{SCUtility.Trim(EQPT_ID)}#{UNIT_NUM}#{RPT_DATE_TIME.ToString(SCAppConstants.DateTimeFormat_27)}";
            }
        }
    }

}
