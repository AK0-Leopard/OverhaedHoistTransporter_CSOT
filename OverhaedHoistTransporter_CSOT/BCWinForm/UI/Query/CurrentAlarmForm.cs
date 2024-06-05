using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class CurrentAlarmForm : Form
    {
        BCMainForm mainForm;
        OHT_Form oht_form;
        BCApplication bcApp;
        private Logger logger = LogManager.GetCurrentClassLogger();

        List<ALARM> aLARMs = new List<ALARM>();
        BindingSource alarmBindingSource = new BindingSource();

        public CurrentAlarmForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            //this.TopMost = true; //不要讓它隨時在最上面

            mainForm = _mainForm;
            bcApp = mainForm.BCApp;
            dgv_Alarm.AutoGenerateColumns = false;
        }

        private void refreshAlarmList(List<ALARM> currentAlarms)
        {
            try
            {
                List<string> new_current_alarm = currentAlarms.Select(alarm => alarm.CompositeKey).ToList();
                List<string> old_current_alarm = aLARMs.Select(alarm => alarm.CompositeKey).ToList();

                List<string> new_add_alarm = new_current_alarm.Except(old_current_alarm).ToList();
                //1.新增多出來的命令
                foreach (string new_alarm_key in new_add_alarm)
                {
                    ALARM new_alarm_obj = new ALARM();
                    var current_cmd = currentAlarms.Where(alarm => SCUtility.isMatche(alarm.CompositeKey, new_alarm_key)).FirstOrDefault();
                    if (current_cmd == null) continue;
                    new_alarm_obj.put(current_cmd);
                    alarmBindingSource.Add(new_alarm_obj);
                    ALARM.AlarmInfoList.TryAdd(new_alarm_key, new_alarm_obj);
                }
                //2.刪除已經結束的命令
                List<string> will_del_mcs_cmds = old_current_alarm.Except(new_current_alarm).ToList();
                foreach (string old_key in will_del_mcs_cmds)
                {
                    var alarm_obj = aLARMs.Where(alarm => SCUtility.isMatche(alarm.CompositeKey, old_key)).FirstOrDefault();
                    alarmBindingSource.Remove(alarm_obj);
                }

            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception");
            }
        }



        private void RoadControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void RoadControlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;

            mainForm.removeForm(this.Name);
        }

        private void CurrentAlarmForm_Load(object sender, EventArgs e)
        {
            alarmBindingSource.DataSource = aLARMs;
            dgv_Alarm.DataSource = alarmBindingSource;
            RefreshAlarm();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshAlarm();
        }
        private void RefreshAlarm()
        {
            try
            {
                var alarms = ALARM.AlarmInfoList.Values.ToList();
                if (alarms == null)
                {
                    refreshAlarmList(new List<ALARM>());
                }
                else
                {
                    refreshAlarmList(alarms);
                }
                dgv_Alarm.Refresh();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}
