//*********************************************************************************
//      uc_TabDeviceStatus.cs
//*********************************************************************************
// File Name: uc_TabDeviceStatus.cs
// Description: 用於uc_SystemModeControl的Device Status使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author       Request No.    Tag     Description
// ------------ -------------  -------------  ------  -----------------------------
// 2019/07/11    Xenia Tseng       N/A         N/A     Initial Release
//**********************************************************************************

using System;
using NLog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.bcf.Common;
using System.Threading;
using com.mirle.ibg3k0.sc;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_TabDeviceStatus : UserControl
    {
        //*******************公用參數設定*******************

        //*******************私有參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        App.WindownApplication app = null;
        List<uc_DeviceStatusSignal> uc_DeviceStatusSignals = null;



        public uc_TabDeviceStatus()
        {
            try
            {
                InitializeComponent();
                //registerEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start()
        {
            app = App.WindownApplication.getInstance();
            uc_DeviceStatusSignals = new List<uc_DeviceStatusSignal>();
            refreshUI();
        }
        public void end()
        {
            foreach (var device_status_signal in uc_DeviceStatusSignals)
            {
                device_status_signal.end();
            }
        }

        //初始化UI
        private void refreshUI()
        {
            try
            {
                //uc_MCS_Status.SetConnStatus("MCS", true);
                //uc_Router_Status.SetConnStatus("Router", false);

                /*Vehicle Link Status*/
                int row_index = 1;
                int column_index = 0;
                var vhs = app.ObjCacheManager.GetVEHICLEs();
                foreach (var vh in vhs)
                {
                    setControlToTlp(tlp_vh_link_status, ref row_index, ref column_index, vh.VEHICLE_ID, vh);
                }

                var DeviceConnectionInfos = app.ObjCacheManager.GetLine().DeviceConnectionInfos;
                /*PLC Status*/
                var plc_device = DeviceConnectionInfos.
                                 Where(device_info => device_info.Type == sc.ProtocolFormat.OHTMessage.DeviceConnectionType.Plc);
                row_index = 1;
                column_index = 0;
                foreach (var device_info in plc_device)
                {
                    setControlToTlp(tlp_plc_status, ref row_index, ref column_index, device_info.Name, device_info);
                }

                ///*AP Status*/
                var ap_device = DeviceConnectionInfos.
                 Where(device_info => device_info.Type == sc.ProtocolFormat.OHTMessage.DeviceConnectionType.Ap);
                row_index = 1;
                column_index = 0;
                foreach (var device_info in ap_device)
                {
                    setControlToTlp(tlp_ap_status, ref row_index, ref column_index, device_info.Name, device_info);
                }

                /*MCS Status*/
                var mcs_device = DeviceConnectionInfos.
                 Where(device_info => device_info.Type == sc.ProtocolFormat.OHTMessage.DeviceConnectionType.Mcs);
                row_index = 1;
                column_index = 0;
                foreach (var device_info in mcs_device)
                {
                    setControlToTlp(tlp_mcs_status, ref row_index, ref column_index, device_info.Name, device_info);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void setControlToTlp(TableLayoutPanel tlp, ref int row_index, ref int column_index, string name, sc.Data.VO.Interface.IConnectionStatusChange iconnectionStatus)
        {
            uc_DeviceStatusSignal uc_VhLk_Status = new uc_DeviceStatusSignal();
            uc_VhLk_Status.start(name, false, iconnectionStatus);
            uc_VhLk_Status.Margin = new System.Windows.Forms.Padding(0);
            tlp.Controls.Add(uc_VhLk_Status, column_index, row_index);
            column_index++;
            column_index++;
            if (column_index >= tlp.ColumnCount)
            {
                column_index = 0;
                row_index++;
                row_index++;
            }
            uc_DeviceStatusSignals.Add(uc_VhLk_Status);
        }

        private void timer_update_status_Tick(object sender, EventArgs e)
        {

        }



        //#region MCS連線狀態屬性
        //bool mcsconn_stat;
        //bool MCSconn_STAT
        //{
        //    get { return mcsconn_stat; }
        //    set
        //    {
        //        mcsconn_stat = value;

        //        Adapter.Invoke(new SendOrPostCallback(o =>
        //        {
        //            uc_MCS_Status.SetConnStatus("MCS", mcsconn_stat);
        //        }), null);
        //    }
        //}
        //#endregion MQ連線狀態屬性

        //private void updateMplcStatus(Line line)
        //{
        //    try
        //    {
        //        Adapter.
        //    }
        //    caBeginInvoke(new SendOrPostCallback((o1) =>
        //        {
        //            MCSconn_STAT = (line.Plc_Link_Stat == SCAppConstants.LinkStatus.Link_OK);
        //        }), null);tch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private void registerEvent()
        //{
        //若與後端鏈結非同一個class時, 就要分別update status
        //若是同個class時, 用refreshUI即可
        //try
        //{
        //    MCS mcs = xxxxxxx
        //    string Handler = this.Name;
        //    if (line != null)
        //    {
        //        line.addEventHandler(Handler, BCFUtility.getPropertyName(() => mcs.MCS),
        //            (s1, e1) => updateMCSStatus(mcs.MCS, mcs.Route));
        //    }
        //        line.addEventHandler(Handler, BCFUtility.getPropertyName(() => mcs.Route),
        //            (s1, e1) => updateMCSStatus(mcs.MCS, mcs.Route));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    logger.Error(ex, "Exception");
        //}
        //}

        //private void updateMCSStatus(bool mcs, bool route)
        //{
        //    if (mcs)
        //    {

        //    }
        //    else
        //    {

        //    }

        //    if (route)
        //    {

        //    }
        //    else
        //    {

        //    }

        //}
    }
}
