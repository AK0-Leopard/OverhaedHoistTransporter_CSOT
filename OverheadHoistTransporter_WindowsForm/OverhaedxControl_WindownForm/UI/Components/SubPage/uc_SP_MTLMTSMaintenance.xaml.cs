//*********************************************************************************
//      uc_SP_MTLMTSMaintenance.cs
//*********************************************************************************
// File Name: uc_SP_MTLMTSMaintenance.cs
// Description: MTLMTS Maintenance
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_MTLMTSMaintenance.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_MTLMTSMaintenance : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public event EventHandler CloseFormEvent;
        public event EventHandler<ModeStatusChangeEventArgs> modeStatusChange;
        public event EventHandler<VehicleCommandEventArgs> vehicleCommandSend;
        public event EventHandler<VehicleCarOutRequestEventArgs> vehicleCarOutRequestSend;
        List<AVEHICLE> vehicleList;
        List<AEQPT> MTL1MTS1List;
        List<AEQPT> MTS2List;
        App.WindownApplication app = null;
        public event EventHandler<MTSMTLInterlockChangeEventArgs> mTSMTLCarOutInterlockChange;
        public event EventHandler<MTSMTLInterlockChangeEventArgs> mTSMTLCarInInterlockChange;
        public event EventHandler<VehicleCMDCancelAbortEventArgs> vehicleCMDCancelAbort;
        public event EventHandler<VehicleCarOutCancelEventArgs> vehicleCarOutCancel;
        #endregion 公用參數設定

        public uc_SP_MTLMTSMaintenance()
        {
            try
            {
                InitializeComponent();
                //app = App.WindownApplication.getInstance();
                //initTitle();
                //initset(app.ObjCacheManager.GetVEHICLEs());
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
                app = App.WindownApplication.getInstance();
                initset(app.ObjCacheManager.GetVEHICLEs());
                //app = App.WindownApplication.getInstance();
                //MTL = app.ObjCacheManager.GetVEHICLEs(device_id) as MaintainLift;
                registerEvent();
                MTL1MTS1List = app.ObjCacheManager.GetMTL1MTS1();
                MTS2List = app.ObjCacheManager.GetMTS2();


                var all_mtl1_mts1_detail_view_obj = this.MTL1MTS1List.Select(eq => new MTLMTSDatailViewObj(eq));
                grid_MTLMTS1.ItemsSource = all_mtl1_mts1_detail_view_obj;
                var all_mts2_detail_view_obj = this.MTS2List.Select(eq => new MTLMTSDatailViewObj(eq));
                grid_MTS2.ItemsSource = all_mts2_detail_view_obj;
                refreshUI();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void registerEvent()
        {
            try
            {
                modeStatusChange += ModeStatusChange;
                app.ObjCacheManager.VehicleUpdateComplete += ObjCacheManager_VehicleUpdateComplete;
                vehicleCommandSend += VehicleCommandSend;
                vehicleCarOutRequestSend += VehicleCarOutRequestSend;
                mTSMTLCarOutInterlockChange += MTSMTLCarOutInterlockChange;
                mTSMTLCarInInterlockChange += MTSMTLCarInInterlockChange;
                vehicleCMDCancelAbort += VehicleCommandCancelAbortSend;
                vehicleCarOutCancel += VehicleCarOutCancelSend;
                app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_MTLMTS_INFO, app.LineBLL.MTLMTSInfo);
                app.ObjCacheManager.MTLMTSInfoUpdate += ObjCacheManager_MTLMTSInfoUpdate;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void unRegisterEvent()
        {
            try
            {
                modeStatusChange -= ModeStatusChange;
                app.ObjCacheManager.VehicleUpdateComplete -= ObjCacheManager_VehicleUpdateComplete;
                vehicleCommandSend -= VehicleCommandSend;
                vehicleCarOutRequestSend -= VehicleCarOutRequestSend;
                app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_MTLMTS_INFO);
                app.ObjCacheManager.MTLMTSInfoUpdate -= ObjCacheManager_MTLMTSInfoUpdate;
                mTSMTLCarOutInterlockChange -= MTSMTLCarOutInterlockChange;
                mTSMTLCarInInterlockChange -= MTSMTLCarInInterlockChange;
                vehicleCMDCancelAbort -= VehicleCommandCancelAbortSend;
                vehicleCarOutCancel -= VehicleCarOutCancelSend;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_MTLMTSInfoUpdate(object sender, EventArgs e)
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    refreshUI();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initset(List<AVEHICLE> vhs)
        {
            try
            {
                vehicleList = vhs;

                foreach (AVEHICLE vh in vhs)
                {
                    cb_VehicleID.Items.Add(vh.VEHICLE_ID);
                }
                cb_MvToStation.Items.Add("MTS");
                cb_MvToStation.Items.Add("MTL");
                cb_MvToStation.Items.Add("MTS2");


                var all_cmd_detail_view_obj = this.vehicleList.Select(vh => new CommandDatailViewObj(vh));
                grid_CMDDetail.ItemsSource = all_cmd_detail_view_obj;

                //grid_CMDDetail.ItemsSource = vhs;GetMTLMTS

                //VEHICLE_ACC_DIST
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //public void registerEvent()
        //{
        //    modeStatusChange += ModeStatusChange;
        //    app.ObjCacheManager.VehicleUpdateComplete += ObjCacheManager_VehicleUpdateComplete;
        //    vehicleCommandSend += VehicleCommandSend;
        //}
        //public void unRegisterEvent()
        //{
        //    modeStatusChange -= ModeStatusChange;
        //    app.ObjCacheManager.VehicleUpdateComplete -= ObjCacheManager_VehicleUpdateComplete;
        //    vehicleCommandSend -= VehicleCommandSend;
        //}

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
                //if (sender.Equals(btn_AutoR))
                //{
                //    string select_vh_id = (string)cb_VehicleID.SelectedItem;
                //    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoRemote));
                //}
                //else if (sender.Equals(btn_AutoMTS))
                //{
                //    string select_vh_id = (string)cb_VehicleID.SelectedItem;
                //    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoMts));
                //}
                //else if (sender.Equals(btn_AutoMTL))
                //{
                //    string select_vh_id = (string)cb_VehicleID.SelectedItem;
                //    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoMtl));
                //}
                //else 
                if (sender.Equals(btn_CarOutReq))
                {
                    string select_vh_id = (string)cb_VehicleID.SelectedItem;
                    AVEHICLE vh = app.ObjCacheManager.GetVEHICLE(select_vh_id);
                    if (cb_MvToStation.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("Failure", "Target station is empty.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    string station_id = ((string)cb_MvToStation.SelectedItem).Trim();
                    //E_CMD_TYPE cmd_type = E_CMD_TYPE.SystemOut;
                    //if (station.StartsWith("MTS"))
                    //{
                    //    if (vh.MODE_STATUS != VHModeStatus.AutoMts)
                    //    {
                    //        TipMessage_Type_Light.Show("Failure", "Vehicle mode is not AutoMTS ,please change status to AutoMTS first.", BCAppConstants.WARN_MSG);
                    //        return;
                    //    }
                    //    cmd_type = E_CMD_TYPE.SystemOut;//使用Move的話，沒辦法下命令去MTS
                    //}
                    //if (station.StartsWith("MTL"))
                    //{
                    //    if (vh.MODE_STATUS != VHModeStatus.AutoMtl)
                    //    {
                    //        TipMessage_Type_Light.Show("Failure", "Vehicle mode is not AutoMTL ,please change status to AutoMTL first.", BCAppConstants.WARN_MSG);
                    //        return;
                    //    }
                    //    cmd_type = E_CMD_TYPE.MoveToMTL;
                    //}
                    //string destination = string.Empty;
                    //switch (station)
                    //{
                    //    case "MTS":
                    //        destination = BCAppConstants.MTLMTS_Address.MTS_address;
                    //        break;
                    //    case "MTL":
                    //        destination = BCAppConstants.MTLMTS_Address.MTL_address;
                    //        break;
                    //    case "MTS2":
                    //        destination = BCAppConstants.MTLMTS_Address.MTS2_address;
                    //        break;
                    //    default:
                    //        break;
                    //}
                    vehicleCarOutRequestSend?.Invoke(this, new VehicleCarOutRequestEventArgs(select_vh_id, station_id));
                }
                else if (sender.Equals(btn_CarOutCancel))
                {
                    if (cb_MvToStation.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("Failure", "Target station is empty.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    string station_id = ((string)cb_MvToStation.SelectedItem).Trim();
                    vehicleCarOutCancel?.Invoke(this, new VehicleCarOutCancelEventArgs(station_id));

                }
                else if (sender.Equals(btn_Close))
                {

                }
                else
                {

                    //donothing
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }



        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CloseFormEvent?.Invoke(this, e);
                //MTLMTSStatus.unRegisterEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void VehicleCommandSend(object sender, VehicleCommandEventArgs e)
        {
            try
            {
                if (!app.VehicleBLL.SendCmdToControl(e.vh_id, e.cmd_type, e.carrier_id, e.source, e.destination, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Send command failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send command succeeded", "", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void VehicleCarOutRequestSend(object sender, VehicleCarOutRequestEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMTSMTLCarOurRequest(e.vh_id, e.station_id, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Send command failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send command succeeded", "", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void VehicleCommandCancelAbortSend(object sender, VehicleCMDCancelAbortEventArgs e)
        {
            try
            {
                if (!app.VehicleBLL.SendVehicleCMDCancelAbortToControl(e.vh_id, out string result))
                {
                    TipMessage_Type_Light.Show("Send Cancel/Abort failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send Cancel/Abort succeed", "", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void VehicleCarOutCancelSend(object sender, VehicleCarOutCancelEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMTSMTLCarOurCancel(e.station_id, out string result))
                {
                    TipMessage_Type_Light.Show("Send Carout cancel failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send Carout cancel succeed", "", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void ModeStatusChange(object sender, ModeStatusChangeEventArgs e)
        {
            try
            {
                if (!app.VehicleBLL.SendModeStatusChange(e.vh_id, e.modeStatus, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Change status failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Change status succeed", "", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void cb_VehicleID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                refreshUI();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_VehicleUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    refreshUI();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void refreshUI()
        {
            try
            {
                grid_MTLMTS1.Items.Refresh();
                grid_MTS2.Items.Refresh();

                MaintainLift MTL = app.ObjCacheManager.GetMTLMTSByID("MTL") as MaintainLift;
                //displayForInterlock(MTL.EQPT_ID, MTL.Interlock);
                displayForInterlockNew(MTL.EQPT_ID, MTL.CarOutSafetyCheck, MTL.CarInMoving);
                displayForVehicleLift(MTL.EQPT_ID, MTL.MTLLocation.ToString(), !string.IsNullOrWhiteSpace(MTL.CurrentCarID));

                MaintainSpace MTS = app.ObjCacheManager.GetMTLMTSByID("MTS") as MaintainSpace;
                //displayForInterlock(MTS.EQPT_ID, MTS.Interlock);
                displayForInterlockNew(MTS.EQPT_ID, MTS.CarOutSafetyCheck, MTS.CarInMoving);
                displayForVehicleLift(MTS.EQPT_ID, null, !string.IsNullOrWhiteSpace(MTS.CurrentCarID));
                MaintainSpace MTS2 = app.ObjCacheManager.GetMTLMTSByID("MTS2") as MaintainSpace;
                //displayForInterlock(MTS2.EQPT_ID, MTS2.Interlock);
                displayForInterlockNew(MTS2.EQPT_ID, MTS2.CarOutSafetyCheck, MTS2.CarInMoving);
                displayForVehicleLift(MTS2.EQPT_ID, null, !string.IsNullOrWhiteSpace(MTS2.CurrentCarID));
                grid_CMDDetail.Items.Refresh();
                //if (cb_VehicleID.SelectedItem != null)
                //{
                //    string select_vh_id = (string)cb_VehicleID.SelectedItem;
                //    if (!string.IsNullOrEmpty(select_vh_id))
                //    {
                //        AVEHICLE select_vh = app.ObjCacheManager.GetVEHICLE(select_vh_id);
                //        if (!select_vh.isTcpIpConnect)
                //        {
                //            btn_CarOutReq.IsEnabled = false;
                //        }
                //        else
                //        {
                //            btn_CarOutReq.IsEnabled = true;
                //        }
                //        //if (select_vh.MODE_STATUS == VHModeStatus.AutoRemote)
                //        //{
                //        //    btn_AutoR.IsEnabled = false;
                //        //    btn_AutoMTS.IsEnabled = true;
                //        //    btn_AutoMTL.IsEnabled = true;
                //        //    btn_Cmd.IsEnabled = false;
                //        //    //cb_MvToStation.Items.Clear();
                //        //}
                //        //else if (select_vh.MODE_STATUS == VHModeStatus.AutoMts)
                //        //{
                //        //    btn_AutoR.IsEnabled = true;
                //        //    btn_AutoMTS.IsEnabled = false;
                //        //    btn_AutoMTL.IsEnabled = true;
                //        //    btn_Cmd.IsEnabled = true;
                //        //    //cb_MvToStation.Items.Clear();
                //        //    ////cb_MvToStation.Items.Add("MTS1");
                //        //    ////cb_MvToStation.Items.Add("MTS2");
                //        //}
                //        //else if (select_vh.MODE_STATUS == VHModeStatus.AutoMtl)
                //        //{
                //        //    btn_AutoR.IsEnabled = true;
                //        //    btn_AutoMTS.IsEnabled = true;
                //        //    btn_AutoMTL.IsEnabled = false;
                //        //    btn_Cmd.IsEnabled = true;
                //        //    //cb_MvToStation.Items.Clear();
                //        //    //cb_MvToStation.Items.Add("MTL1");
                //        //}
                //        //else
                //        //{
                //        //    btn_AutoR.IsEnabled = true;
                //        //    btn_AutoMTS.IsEnabled = true;
                //        //    btn_AutoMTL.IsEnabled = true;
                //        //    btn_Cmd.IsEnabled = false;
                //        //    //cb_MvToStation.Items.Clear();
                //        //}
                //    }
                //    else
                //    {
                //        //btn_AutoR.IsEnabled = false;
                //        //btn_AutoMTS.IsEnabled = false;
                //        //btn_AutoMTL.IsEnabled = false;
                //        //btn_Cmd.IsEnabled = false;
                //        btn_CarOutReq.IsEnabled = false;
                //        //cb_MvToStation.Items.Clear();
                //    }
                //}
                //else
                //{
                //    //btn_AutoR.IsEnabled = false;
                //    //btn_AutoMTS.IsEnabled = false;
                //    //btn_AutoMTL.IsEnabled = false;
                //    //btn_Cmd.IsEnabled = false;
                //    btn_CarOutReq.IsEnabled = false;
                //    //cb_MvToStation.Items.Clear();
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void interlock_change_refreshUI()
        {
            try
            {
                MaintainLift MTL = app.ObjCacheManager.GetMTLMTSByID("MTL") as MaintainLift;
                //displayForInterlock(MTL.EQPT_ID, MTL.Interlock);
                displayForInterlockNew(MTL.EQPT_ID, MTL.CarOutSafetyCheck, MTL.CarInMoving);
                MaintainSpace MTS = app.ObjCacheManager.GetMTLMTSByID("MTS") as MaintainSpace;
                //displayForInterlock(MTS.EQPT_ID, MTS.Interlock);
                displayForInterlockNew(MTS.EQPT_ID, MTS.CarOutSafetyCheck, MTS.CarInMoving);
                MaintainSpace MTS2 = app.ObjCacheManager.GetMTLMTSByID("MTS2") as MaintainSpace;
                //displayForInterlock(MTS2.EQPT_ID, MTS2.Interlock);
                displayForInterlockNew(MTS2.EQPT_ID, MTS2.CarOutSafetyCheck, MTS2.CarInMoving);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void MTSMTLCarOutInterlockChange(object sender, MTSMTLInterlockChangeEventArgs e)
        {
            try
            {
                bool is_success = false;
                string result = "";
                await Task.Run(() =>
                 {
                     is_success = app.LineBLL.SendMTSMTLCarOutInterlock(e.station_id, e.is_set, out result);
                 });
                if (is_success)
                {
                    TipMessage_Type_Light.Show("", "Interlock Change Request Succeed", BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void MTSMTLCarInInterlockChange(object sender, MTSMTLInterlockChangeEventArgs e)
        {
            try
            {
                bool is_success = false;
                string result = "";
                await Task.Run(() =>
                {
                    is_success = app.LineBLL.SendMTSMTLCarInInterlock(e.station_id, e.is_set, out result);
                });
                if (is_success)
                {
                    TipMessage_Type_Light.Show("", "Interlock Change Request Succeed", BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void displayForInterlock(string station_id, bool isSet)
        {
            try
            {
                var mtlLockOn = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_green.png", UriKind.Relative));
                var mtlLockOff = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_red.png", UriKind.Relative));
                var mtsLockOn = new BitmapImage(new Uri(@"\Resources\MTS_green.png", UriKind.Relative));
                var mtsLockOff = new BitmapImage(new Uri(@"\Resources\MTS_red.png", UriKind.Relative));
                if (station_id == "MTL")
                {
                    if (isSet)
                    {
                        InLockSignal_MTL1A.Source = mtlLockOn;
                        InLockSignal_MTL1B.Source = mtlLockOn;
                    }
                    else
                    {
                        InLockSignal_MTL1A.Source = mtlLockOff;
                        InLockSignal_MTL1B.Source = mtlLockOff;
                    }
                }
                else if (station_id == "MTS")
                {
                    if (isSet)
                    {
                        InLockSignal_MTS1A.Source = mtsLockOn;
                        InLockSignal_MTS1B.Source = mtsLockOn;

                    }
                    else
                    {
                        InLockSignal_MTS1A.Source = mtsLockOff;
                        InLockSignal_MTS1B.Source = mtsLockOff;
                    }
                }
                else if (station_id == "MTS2")
                {
                    if (isSet)
                    {
                        InLockSignal_MTS2A.Source = mtsLockOn;
                        InLockSignal_MTS2B.Source = mtsLockOn;
                    }
                    else
                    {
                        InLockSignal_MTS2A.Source = mtsLockOff;
                        InLockSignal_MTS2B.Source = mtsLockOff;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void displayForInterlockNew(string station_id, bool carOutSafetyCheck, bool carInSafetyCheck)
        {
            try
            {
                var mtlLockOn = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_green.png", UriKind.Relative));
                var mtlLockOff = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_red.png", UriKind.Relative));
                var mtsLockOn = new BitmapImage(new Uri(@"\Resources\MTS_green.png", UriKind.Relative));
                var mtsLockOff = new BitmapImage(new Uri(@"\Resources\MTS_red.png", UriKind.Relative));
                if (station_id == "MTL")
                {
                    InLockSignal_MTL1A.Source = carOutSafetyCheck ? mtlLockOn : mtlLockOff;
                    InLockSignal_MTL1B.Source = carInSafetyCheck ? mtlLockOn : mtlLockOff;
                    //if (carOutInterlock)
                    //{
                    //    InLockSignal_MTL1A.Source = mtlLockOn;
                    //    InLockSignal_MTL1B.Source = mtlLockOn;
                    //}
                    //else
                    //{
                    //    InLockSignal_MTL1A.Source = mtlLockOff;
                    //    InLockSignal_MTL1B.Source = mtlLockOff;
                    //}
                }
                else if (station_id == "MTS")
                {
                    InLockSignal_MTS1A.Source = carOutSafetyCheck ? mtsLockOn : mtsLockOff;
                    InLockSignal_MTS1B.Source = carInSafetyCheck ? mtsLockOn : mtsLockOff;

                    //if (carOutInterlock)
                    //{
                    //    InLockSignal_MTS1A.Source = mtsLockOn;
                    //    InLockSignal_MTS1B.Source = mtsLockOn;

                    //}
                    //else
                    //{
                    //    InLockSignal_MTS1A.Source = mtsLockOff;
                    //    InLockSignal_MTS1B.Source = mtsLockOff;
                    //}
                }
                else if (station_id == "MTS2")
                {
                    InLockSignal_MTS2A.Source = carOutSafetyCheck ? mtsLockOn : mtsLockOff;
                    InLockSignal_MTS2B.Source = carInSafetyCheck ? mtsLockOn : mtsLockOff;

                    //if (carOutInterlock)
                    //{
                    //    InLockSignal_MTS2A.Source = mtsLockOn;
                    //    InLockSignal_MTS2B.Source = mtsLockOn;
                    //}
                    //else
                    //{
                    //    InLockSignal_MTS2A.Source = mtsLockOff;
                    //    InLockSignal_MTS2B.Source = mtsLockOff;
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void displayForVehicleLift(string station_id, string location, bool isSet)
        {
            try
            {
                var withCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
                var withwoCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));
                if (station_id == "MTL")
                {
                    if (isSet)
                    {
                        if (location == "Upper")
                        {
                            VhSignal_MTLUp.Source = withCar;
                            VhSignal_MTLDown.Source = withwoCar;

                            //Lifter Label(向上時)
                            LiftSigOff_Up.Visibility = Visibility.Collapsed;
                            LiftSigOn_Up.Visibility = Visibility.Visible;
                            LiftSigOff_Down.Visibility = Visibility.Visible;
                            LiftSigOn_Down.Visibility = Visibility.Collapsed;
                        }
                        else if (location == "Bottorn")
                        {
                            VhSignal_MTLUp.Source = withwoCar;
                            VhSignal_MTLDown.Source = withCar;

                            //Lifter Label(向下時)
                            LiftSigOff_Up.Visibility = Visibility.Visible;
                            LiftSigOn_Up.Visibility = Visibility.Collapsed;
                            LiftSigOff_Down.Visibility = Visibility.Collapsed;
                            LiftSigOn_Down.Visibility = Visibility.Visible;
                        }
                        else if (location == "None")
                        {
                            VhSignal_MTLUp.Source = withwoCar;
                            VhSignal_MTLDown.Source = withwoCar;
                            LiftArrowSig.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        VhSignal_MTLUp.Source = withwoCar;
                        VhSignal_MTLDown.Source = withwoCar;
                    }
                }
                else if (station_id == "MTS")
                {
                    if (isSet)
                    {
                        //MTS區域內有車輛
                        VhSignal_MTS1.Source = withCar;
                    }
                    else
                    {
                        //MTS區域內沒有車輛
                        VhSignal_MTS1.Source = withwoCar;
                    }
                }
                else if (station_id == "MTS2")
                {
                    if (isSet)
                    {
                        //MTS2區域內有車輛
                        VhSignal_MTS2.Source = withCar;
                    }
                    else
                    {
                        //MTS2區域內沒有車輛
                        VhSignal_MTS2.Source = withwoCar;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        BitmapImage mtlLockOn = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_green.png", UriKind.Relative));
        BitmapImage mtlLockOff = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_red.png", UriKind.Relative));
        BitmapImage mtsLockOn = new BitmapImage(new Uri(@"\Resources\MTS_green.png", UriKind.Relative));
        BitmapImage mtsLockOff = new BitmapImage(new Uri(@"\Resources\MTS_red.png", UriKind.Relative));
        //var mtlLLifterDown = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Down_Flicker.gif", UriKind.Relative));
        //var mtlLLifterUp = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Up_Flicker.gif", UriKind.Relative));

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    interlock_change_refreshUI();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class MTSMTLInterlockChangeEventArgs : EventArgs
        {
            public MTSMTLInterlockChangeEventArgs(string station_id, string is_set)
            {
                this.station_id = station_id;
                this.is_set = is_set;
            }
            public string station_id { get; private set; }
            public string is_set { get; private set; }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton toggleButton = sender as ToggleButton;
                MTLMTSDatailViewObj obj = toggleButton.DataContext as MTLMTSDatailViewObj;
                mTSMTLCarOutInterlockChange?.Invoke(this, new MTSMTLInterlockChangeEventArgs(obj.stationID, toggleButton.IsChecked.ToString()));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void ToggleButtonCarIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton toggleButton = sender as ToggleButton;
                MTLMTSDatailViewObj obj = toggleButton.DataContext as MTLMTSDatailViewObj;
                mTSMTLCarInInterlockChange?.Invoke(this, new MTSMTLInterlockChangeEventArgs(obj.stationID, toggleButton.IsChecked.ToString()));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
    public class VehicleCarOutRequestEventArgs : EventArgs
    {
        public VehicleCarOutRequestEventArgs(string vh_id, string station_id)
        {
            this.vh_id = vh_id;
            this.station_id = station_id;

        }
        public string vh_id { get; private set; }
        public string station_id { get; private set; }
    }
    public class VehicleCarOutCancelEventArgs : EventArgs
    {
        public VehicleCarOutCancelEventArgs(string station_id)
        {
            this.station_id = station_id;

        }
        public string station_id { get; private set; }
    }

}
