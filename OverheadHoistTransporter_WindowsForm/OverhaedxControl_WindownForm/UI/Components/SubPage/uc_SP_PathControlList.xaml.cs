//*********************************************************************************
//      uc_SP_PortMaint.cs
//*********************************************************************************
// File Name: uc_SP_PortMaint.cs
// Description: Port Maintenance
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                Author              Request No.         Tag                 Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/12/29          Kevin               N/A                 N/A                 Initial Release
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_PortMaint.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_PathControlList : UserControl
    {
        #region 公用參數設定
        public event EventHandler CloseFormEvent;
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        public event EventHandler<SegmentStatusUpdateEventArgs> SegmentEnableDisable;
        public event EventHandler<SegmentStatusUpdateEventArgs> SegmentCVEnable;
        public event EventHandler<SegmentStatusUpdateEventArgs> SegmentHIDEnable;

        List<ASEGMENT> segments = null;

        #endregion 公用參數設定

        public uc_SP_PathControlList()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void startupUI()
        {
            try
            {
                this.Width = 1711;
                this.Height = 754;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void initUI()
        {
            try
            {
                start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start()
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                start(app.ObjCacheManager.GetSegments());
                registerEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void registerEvent()
        {
            try
            {
                app.ObjCacheManager.SegmentsUpdateComplete += ObjCacheManager_SegmentsUpdateComplete;
                SegmentEnableDisable += Uc_SP_PathControlList_SegmentEnableDisable;
                SegmentCVEnable += Uc_SP_PathControlList_SegmentCVEnable;
                SegmentHIDEnable += Uc_SP_PathControlList_SegmentHIDEnable;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PathControlList_SegmentHIDEnable(object sender, SegmentStatusUpdateEventArgs e)
        {
            try
            {
                app.SegmentBLL.webAPI.SendSegmentStatusUpdate(e.seg_id, ASEGMENT.DisableType.HID, E_SEG_STATUS.Active);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PathControlList_SegmentCVEnable(object sender, SegmentStatusUpdateEventArgs e)
        {
            try
            {
                var send_result = app.SegmentBLL.webAPI.SendSegmentStatusUpdate(e.seg_id, ASEGMENT.DisableType.Safety, E_SEG_STATUS.Active);
                if (!send_result.isSuccess)
                {
                    TipMessage_Type_Light.Show("Failure", send_result.result, BCAppConstants.WARN_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PathControlList_SegmentEnableDisable(object sender, SegmentStatusUpdateEventArgs e)
        {
            try
            {
                app.SegmentBLL.webAPI.SendSegmentStatusUpdate(e.seg_id, ASEGMENT.DisableType.User, e.status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_SegmentsUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public void unregisterEvent()
        {
            try
            {
                SegmentEnableDisable -= Uc_SP_PathControlList_SegmentEnableDisable;
                SegmentCVEnable -= Uc_SP_PathControlList_SegmentCVEnable;
                SegmentHIDEnable -= Uc_SP_PathControlList_SegmentHIDEnable;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void Uc_SP_PortMaint1_PortEnableDisable(object sender, ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint.PortSerivceStatusUpdateEventArgs e)
        {
            try
            {
                app.PortStationBLL.SendPortStatusChange(e.port_id, e.status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PortMaint1_PortPriorityUpdate(object sender, ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint.PortPriorityUpdateEventArgs e)
        {
            try
            {
                app.PortStationBLL.SendPortPriorityUpdate(e.port_id, e.priority);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start(List<ASEGMENT> _segemtn)
        {
            try
            {
                segments = _segemtn;
                var all_segment_view_obj = this.segments.Select(segment => new SegmentViewObj(segment));
                allSegmentList.ItemsSource = all_segment_view_obj;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void Refresh()
        {
            try
            {
                bcf.Common.Adapter.Invoke((obj) =>
                {
                    allSegmentList.Items.Refresh();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SegmentViewObj segement = (SegmentViewObj)allSegmentList.SelectedItem;
                string seg_id = segement.SEG_NUM;
                if (sender.Equals(btn_Enable))
                {
                    SegmentEnableDisable?.Invoke(this, new SegmentStatusUpdateEventArgs(seg_id, E_SEG_STATUS.Active));
                }
                else if (sender.Equals(btn_Disable))
                {
                    SegmentEnableDisable?.Invoke(this, new SegmentStatusUpdateEventArgs(seg_id, E_SEG_STATUS.Closed));
                }
                else if (sender.Equals(btn_Enable_CV))
                {
                    SegmentCVEnable?.Invoke(this, new SegmentStatusUpdateEventArgs(seg_id, E_SEG_STATUS.Active));
                }
                //else if (sender.Equals(btn_Enable_HID))
                //{
                //    SegmentHIDEnable?.Invoke(this, new SegmentStatusUpdateEventArgs(seg_id, E_SEG_STATUS.Active));
                //}

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }




        public class SegmentStatusUpdateEventArgs : EventArgs
        {
            public SegmentStatusUpdateEventArgs(string _segmentID, sc.E_SEG_STATUS _status)
            {
                seg_id = _segmentID;
                status = _status;
            }

            public string seg_id { get; private set; }
            public sc.E_SEG_STATUS status { get; private set; }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CloseFormEvent?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void segments_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
