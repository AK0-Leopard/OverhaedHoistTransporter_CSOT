using com.mirle.ibg3k0.ohxc.winform.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    public partial class LogQueryForm : Form
    {
        SysExcuteQualityQueryService sysExcuteQualityQueryService;


        static LogQueryForm form = null;
        static object obj_lock = new object();
        public static LogQueryForm getInstance()
        {
            if (form == null || form.IsDisposed)
            {
                lock (obj_lock)
                {
                    if (form == null || form.IsDisposed)
                    {
                        form = new LogQueryForm();
                    }
                    else
                    {
                        form.Focus();
                    }
                }
            }
            return form;
        }

        public LogQueryForm()
        {
            InitializeComponent();
            sysExcuteQualityQueryService = App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
            uctl_ElasticQuery_CMDExcute_1.QueryCommandDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt;
            uctl_ElasticQuery_sys_process_log.QueryCommandDetailEnevt += Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1;

            List<string> lstVh = new List<string>() { "" };
            List<sc.AVEHICLE> lstEq = App.WindownApplication.getInstance().ObjCacheManager.GetVEHICLEs();
            lstVh.AddRange(lstEq.Select(vh => vh.VEHICLE_ID).ToList());
            string[] allVh = lstVh.ToArray();
            Common.WinFromUtility.setComboboxDataSource(cb_vh_id, allVh);

        }

        private async void Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1(string vh_id, DateTime query_start_time)
        {
            DateTime dateTimeFrom = query_start_time.AddSeconds(-30);
            DateTime dateTimeTo = query_start_time.AddHours(1);
            dateTimePicker1.Value = dateTimeFrom;
            dateTimePicker2.Value = dateTimeTo;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            List<RecordReportInfo> record_report_info = null;

            await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
            record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
            cb_vh_id.Text = vh_id;
            uctl_ElasticQuery_System_Process1.setDataItemsSource(record_report_info);

            tab_main.SelectedIndex = 2;
        }

        private async void Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt(object sender, string cmd_id)
        {
            DateTime dateTimeFrom = m_MCS_Cmd_Query_StartDTCbx.Value;
            DateTime dateTimeTo = DateTime.Now;
            dt_sys_process_log_start_time.Value = dateTimeFrom;
            dt_sys_process_log_end_time.Value = dateTimeTo;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            List<RecordReportInfo> record_report_info = null;

            await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, null, cmd_id));
            record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
            txt_sys_process_log_cmd_id.Text = cmd_id;
            uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
            tab_main.SelectedIndex = 1;

        }

        private async void btn_search_mcs_cmd_query_Click(object sender, EventArgs e)
        {
            DateTime dateTimeFrom = m_MCS_Cmd_Query_StartDTCbx.Value;
            DateTime dateTimeTo = m_MCS_Cmd_Query_EndDTCbx.Value;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);

            List<ASYSEXCUTEQUALITY> system_qualitys = null;
            await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo));
            system_qualitys = system_qualitys.OrderBy(info => info.CMD_INSERT_TIME).ToList();

            uctl_ElasticQuery_CMDExcute_1.setDataItemsSource(system_qualitys);
        }

        private async void btn_sys_process_log_search_Click(object sender, EventArgs e)
        {
            DateTime dateTimeFrom = dt_sys_process_log_start_time.Value;
            DateTime dateTimeTo = dt_sys_process_log_end_time.Value;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            string cmd_id = txt_sys_process_log_cmd_id.Text;
            List<RecordReportInfo> record_report_info = null;
            await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, null, cmd_id));
            record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();

            uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
        }

        private async void btn_sys_process_log_vh_search_Click(object sender, EventArgs e)
        {
            DateTime dateTimeFrom = dateTimePicker1.Value;
            DateTime dateTimeTo = dateTimePicker2.Value;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            string vh_id = cb_vh_id.Text;
            List<RecordReportInfo> record_report_info = null;
            await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
            record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();

            uctl_ElasticQuery_System_Process1.setDataItemsSource(record_report_info);
        }

    }
}
