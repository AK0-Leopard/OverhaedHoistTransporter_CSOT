using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.bcf.Common;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    public partial class uctl_Dashboard : UserControl
    {
        static Color SYSTME_PROCESS_STATUS_NORMAL = Color.FromArgb(0, 204, 0);
        static Color SYSTME_PROCESS_STATUS_WARN = Color.FromArgb(255, 147, 39);
        static Color SYSTME_PROCESS_STATUS_DANGER = Color.FromArgb(254, 85, 85);

        WindownApplication app = null;
        int totle_vh_count = 0;
        public uctl_Dashboard()
        {
            InitializeComponent();
        }

        public void start(WindownApplication _app)
        {
            app = _app;
            var vhs = app.ObjCacheManager.GetVEHICLEs();
            totle_vh_count = vhs.Count;
            progress_bar_vehicle_status.Maximum = totle_vh_count;


            switch (ohxc.winform.App.WindownApplication.OHxCFormMode)
            {
                case ohxc.winform.App.OHxCFormMode.CurrentPlayer:
                    app.GetSysExcuteQualityQueryService().SysExcuteQualityImageChanged += Uctl_Map_SysExcuteQualityImageChanged;
                    app.GetSysExcuteQualityQueryService().VehicleIdleStatusChanged += Uctl_Map_VehicleIdleStatusChanged;
                    app.GetSysExcuteQualityQueryService().OhxCExcuteEffectivenessChanged += Uctl_Dashboard_OhxCEffectivenessChanged; ;
                    app.GetSysExcuteQualityQueryService().CurrnetMCSCommandCountChanged += Uctl_Dashboard_CurrnetMCSCommandCountChanged;
                    break;
                case ohxc.winform.App.OHxCFormMode.HistoricalPlayer:
                    //trunOffMonitorAllVhStatus();
                    break;
            }
        }


        private void Uctl_Dashboard_CurrnetMCSCommandCountChanged(object sender, int[] e)
        {
            Color color_status = SYSTME_PROCESS_STATUS_DANGER;
            int cmd_queue_count = e[1];
            int mcs_cmd_queueOfprogress_bar_max = 0;
            if (cmd_queue_count <= totle_vh_count)
                mcs_cmd_queueOfprogress_bar_max = totle_vh_count;
            else
                mcs_cmd_queueOfprogress_bar_max = cmd_queue_count;

            //if (cmd_queue_count > (totle_vh_count * 2 / 3))
            //{
            //    color_status = SYSTME_PROCESS_STATUS_DANGER;
            //}
            //else if (cmd_queue_count > (totle_vh_count * 1 / 3))
            //{
            //    color_status = SYSTME_PROCESS_STATUS_WARN;
            //}
            //else
            //{
            //    color_status = SYSTME_PROCESS_STATUS_NORMAL;
            //}

            Adapter.Invoke((obj) =>
            {
                progress_bar_mcs_cmd_queue_status.TrackFore = color_status;
                progress_bar_mcs_cmd_queue_status.Maximum = mcs_cmd_queueOfprogress_bar_max;
                progress_bar_mcs_cmd_queue_status.Value = cmd_queue_count;

                lbl_mcs_cmd_queue_count.ForeColor = color_status;
                lbl_mcs_cmd_queue_count.Text = cmd_queue_count.ToString();
            }, null);
        }

        private void Uctl_Dashboard_OhxCEffectivenessChanged(object sender, int[] e)
        {
            Color color_status = Color.Empty;
            int total_complete_cmd_count = e[0];
            int effectiveness_Percentage = e[1];
            if (effectiveness_Percentage > 80)
            {
                color_status = SYSTME_PROCESS_STATUS_NORMAL;
            }
            else if (effectiveness_Percentage > 60)
            {

                color_status = SYSTME_PROCESS_STATUS_WARN;
            }
            else
            {
                color_status = SYSTME_PROCESS_STATUS_DANGER;
            }
            Adapter.Invoke((obj) =>
            {
                progress_bar_ohxc_effectiveness.TrackFore = color_status;
                progress_bar_ohxc_effectiveness.Value = effectiveness_Percentage;

                lbl_system_effectiveness.Text = $"{effectiveness_Percentage} % - total:{total_complete_cmd_count}";
                lbl_system_effectiveness.ForeColor = color_status;
            }, null);
        }

        private void Uctl_Map_SysExcuteQualityImageChanged(object sender, EventArgs e)
        {
            Adapter.Invoke((obj) =>
            {
                pic_cmdQueueTime.Image = app.GetSysExcuteQualityQueryService().Image_Cmd_Queue_Time;
                pic_cmdCount.Image = app.GetSysExcuteQualityQueryService().Image_Cmd_Count;
            }, null);
        }

        private void Uctl_Map_VehicleIdleStatusChanged(object sender, int e)
        {
            int idle_vh_count = e;
            Color color_status = SYSTME_PROCESS_STATUS_NORMAL;

            //if (idle_vh_count > (totle_vh_count * 2 / 3))
            //{
            //    color_status = SYSTME_PROCESS_STATUS_NORMAL;
            //}
            //else if (idle_vh_count > (totle_vh_count * 1 / 3))
            //{
            //    color_status = SYSTME_PROCESS_STATUS_WARN;
            //}
            //else
            //{
            //    color_status = SYSTME_PROCESS_STATUS_DANGER;
            //}
            Adapter.Invoke((obj) =>
            {
                progress_bar_vehicle_status.TrackFore = color_status;
                progress_bar_vehicle_status.Value = idle_vh_count;

                lbl_Idle_count.ForeColor = color_status;
                lbl_Idle_count.Text = $"{idle_vh_count}/{totle_vh_count}";

            }, null);
        }


    }
}
