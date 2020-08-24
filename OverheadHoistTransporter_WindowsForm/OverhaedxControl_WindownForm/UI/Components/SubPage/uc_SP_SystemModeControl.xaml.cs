//*********************************************************************************
//      uc_SP_SystemModeControl.cs
//*********************************************************************************
// File Name: uc_SP_SystemModeControl.cs
// Description: System Mode Control
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
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static com.mirle.ibg3k0.sc.ALINE;


namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_SystemModeControl.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_SystemModeControl : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public event EventHandler<LinkStatusUpdateEventArgs> linkStatusChange;
        public event EventHandler<HostModeUpdateEventArgs> hostModeChange;
        public event EventHandler<TSCStateUpdateEventArgs> tscStateChange;
        public event EventHandler CloseFormEvent;
        ohxc.winform.App.WindownApplication app = null;
        #endregion 公用參數設定

        public uc_SP_SystemModeControl()
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

        public void initUI()
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                init();
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
                linkStatusChange += LinkStatusChange;
                hostModeChange += HostModeChange;
                tscStateChange += TSCStateChange;
                app.ObjCacheManager.ConnectionInfoUpdate += ObjCacheManager_ConnectionInfo_update;
                app.ObjCacheManager.OnlineCheckInfoUpdate += ObjCacheManager_OnlineCheckInfo_update;
                app.ObjCacheManager.PingCheckInfoUpdate += ObjCacheManager_PingCheckInfo_update;

                app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_CONNECTION_INFO, app.LineBLL.ConnectioneInfo);
                app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_ONLINE_CHECK_INFO, app.LineBLL.OnlineCheckInfo);
                app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_PING_CHECK_INFO, app.LineBLL.PingCheckInfo);
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
                app.ObjCacheManager.ConnectionInfoUpdate -= ObjCacheManager_ConnectionInfo_update;
                app.ObjCacheManager.OnlineCheckInfoUpdate -= ObjCacheManager_OnlineCheckInfo_update;
                app.ObjCacheManager.PingCheckInfoUpdate -= ObjCacheManager_PingCheckInfo_update;
                linkStatusChange -= LinkStatusChange;
                hostModeChange -= HostModeChange;
                tscStateChange -= TSCStateChange;

                app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_CONNECTION_INFO);
                app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_ONLINE_CHECK_INFO);
                app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_PING_CHECK_INFO);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void LinkStatusChange(object sender, LinkStatusUpdateEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendLinkStatusChange(e.linkstatus, out string result))
                {
                    //TipMessage_Type_Light.Show("Change Failure", "Communicating status can't be off.", BCAppConstants.INFO_MSG);
                    TipMessage_Type_Light.Show("Change Failure", result, BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void HostModeChange(object sender, HostModeUpdateEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendHostModeChange(e.host_mode, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Change Failure", result, BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void TSCStateChange(object sender, TSCStateUpdateEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendTSCStateChange(e.tscstate, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Change Failure", result, BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_ConnectionInfo_update(object sender, EventArgs e)
        {
            bool TSCstat_Color = false;
            string TSCstat_Display = string.Empty;
            bool SECSstat_Color = false;
            string SECSstat_Display = string.Empty;
            bool Controlstat_Color = false;
            string Controlstat_Display = string.Empty;
            ALINE aLINE = app.ObjCacheManager.GetLine();
            try
            {
                switch (aLINE.Secs_Link_Stat)
                {
                    case SCAppConstants.LinkStatus.LinkOK:
                        SECSstat_Color = true;
                        //SECSstat_Display = BCAppConstants.SECSLinkDisplay.Link;
                        SECSstat_Display = "On";
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            CommunicationStatus.Button1.IsEnabled = false;
                            CommunicationStatus.Button2.IsEnabled = true;
                        }), null);
                        break;
                    case SCAppConstants.LinkStatus.LinkFail:
                        SECSstat_Color = false;
                        //SECSstat_Display = BCAppConstants.SECSLinkDisplay.NotLink;
                        SECSstat_Display = "Off";
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            CommunicationStatus.Button1.IsEnabled = true;
                            CommunicationStatus.Button2.IsEnabled = false;
                        }), null);
                        break;
                }

                switch (aLINE.Host_Control_State)
                {
                    case SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote:
                        Controlstat_Color = true;
                        Controlstat_Display = BCAppConstants.HostModeDisplay.OnlineRemote;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            ControlStatus.Button1.IsEnabled = false;
                            ControlStatus.Button2.IsEnabled = true;
                            ControlStatus.Button3.IsEnabled = true;
                            TSCStatus.Button1.IsEnabled = true;
                            TSCStatus.Button2.IsEnabled = true;
                        }), null);
                        break;
                    case SCAppConstants.LineHostControlState.HostControlState.On_Line_Local:
                        Controlstat_Color = true;
                        Controlstat_Display = BCAppConstants.HostModeDisplay.OnlineLocal;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            ControlStatus.Button1.IsEnabled = true;
                            ControlStatus.Button2.IsEnabled = false;
                            ControlStatus.Button3.IsEnabled = true;
                            TSCStatus.Button1.IsEnabled = true;
                            TSCStatus.Button2.IsEnabled = true;
                        }), null);
                        break;
                    case SCAppConstants.LineHostControlState.HostControlState.EQ_Off_line:
                        Controlstat_Color = false;
                        Controlstat_Display = BCAppConstants.HostModeDisplay.Offline;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            ControlStatus.Button1.IsEnabled = true;
                            ControlStatus.Button2.IsEnabled = true;
                            ControlStatus.Button3.IsEnabled = false;
                            TSCStatus.Button1.IsEnabled = false;
                            TSCStatus.Button2.IsEnabled = false;
                        }), null);
                        break;
                }

                switch (aLINE.SCStats)
                {
                    case TSCState.AUTO:
                        TSCstat_Color = true;
                        TSCstat_Display = BCAppConstants.TSCStateDisplay.Auto;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            TSCStatus.Button1.IsEnabled = false;
                            TSCStatus.Button2.IsEnabled = true;
                        }), null);
                        break;
                    case TSCState.NONE:
                        TSCstat_Color = false;
                        TSCstat_Display = BCAppConstants.TSCStateDisplay.None;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            TSCStatus.Button1.IsEnabled = false;
                            TSCStatus.Button2.IsEnabled = true;
                        }), null);
                        break;
                    case TSCState.PAUSED:
                        TSCstat_Color = false;
                        TSCstat_Display = BCAppConstants.TSCStateDisplay.Pause;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            //TSCStatus.Button1.IsEnabled = true;
                            TSCStatus.Button2.IsEnabled = false;
                        }), null);
                        break;
                    case TSCState.PAUSING:
                        TSCstat_Color = false;
                        TSCstat_Display = BCAppConstants.TSCStateDisplay.Pausing;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            TSCStatus.Button1.IsEnabled = false;
                            TSCStatus.Button2.IsEnabled = false;
                        }), null);
                        break;
                    case TSCState.TSC_INIT:
                        TSCstat_Color = false;
                        TSCstat_Display = BCAppConstants.TSCStateDisplay.Init;
                        Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                        {
                            TSCStatus.Button1.IsEnabled = false;
                            TSCStatus.Button2.IsEnabled = false;
                        }), null);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            CommunicationStatus.SetConnectSignal(SECSstat_Display, SECSstat_Color);
            ControlStatus.SetConnSignal(Controlstat_Display, Controlstat_Color);
            TSCStatus.SetConnectSignal(TSCstat_Display, TSCstat_Color);
        }

        private void ObjCacheManager_OnlineCheckInfo_update(object sender, EventArgs e)
        {
            ALINE aLINE = app.ObjCacheManager.GetLine();
            try
            {
                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                {
                    ControlStatus.uc_ControlStatusSignal1.SetConnStatus("Current port states", aLINE.CurrentPortStateChecked);
                    ControlStatus.uc_ControlStatusSignal2.SetConnStatus("Current state", aLINE.CurrentStateChecked);
                    ControlStatus.uc_ControlStatusSignal3.SetConnStatus("Enhanced vehicles", aLINE.EnhancedVehiclesChecked);
                    ControlStatus.uc_ControlStatusSignal4.SetConnStatus("TSC state", aLINE.TSCStateChecked);
                    ControlStatus.uc_ControlStatusSignal5.SetConnStatus("Unit alarm state list", aLINE.UnitAlarmStateListChecked);
                    ControlStatus.uc_ControlStatusSignal6.SetConnStatus("Enhanced transfers", aLINE.EnhancedTransfersChecked);
                    ControlStatus.uc_ControlStatusSignal7.SetConnStatus("Enhanced carriers", aLINE.EnhancedCarriersChecked);
                    ControlStatus.uc_ControlStatusSignal8.SetConnStatus("Lane cut list", aLINE.LaneCutListChecked);
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_PingCheckInfo_update(object sender, EventArgs e)
        {
            ALINE aLINE = app.ObjCacheManager.GetLine();
            try
            {
                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                {
                    /*MCS Status*/
                    uc_MCS_Status.SetConnStatus("MCS", aLINE.MCSConnectionSuccess);
                    uc_Router_Status.SetConnStatus("Router", aLINE.RouterConnectionSuccess);
                    /*Vehicle Link Status*/
                    uc_VhLk_Status_OHT1.SetConnStatus("OHT1", aLINE.OHT1ConnectionSuccess);
                    uc_VhLk_Status_OHT2.SetConnStatus("OHT2", aLINE.OHT2ConnectionSuccess);
                    uc_VhLk_Status_OHT3.SetConnStatus("OHT3", aLINE.OHT3ConnectionSuccess);
                    uc_VhLk_Status_OHT4.SetConnStatus("OHT4", aLINE.OHT4ConnectionSuccess);
                    uc_VhLk_Status_OHT5.SetConnStatus("OHT5", aLINE.OHT5ConnectionSuccess);
                    uc_VhLk_Status_OHT6.SetConnStatus("OHT6", aLINE.OHT6ConnectionSuccess);
                    uc_VhLk_Status_OHT7.SetConnStatus("OHT7", aLINE.OHT7ConnectionSuccess);
                    uc_VhLk_Status_OHT8.SetConnStatus("OHT8", aLINE.OHT8ConnectionSuccess);
                    uc_VhLk_Status_OHT9.SetConnStatus("OHT9", aLINE.OHT9ConnectionSuccess);
                    uc_VhLk_Status_OHT10.SetConnStatus("OHT10", aLINE.OHT10ConnectionSuccess);
                    uc_VhLk_Status_OHT11.SetConnStatus("OHT11", aLINE.OHT11ConnectionSuccess);
                    uc_VhLk_Status_OHT12.SetConnStatus("OHT12", aLINE.OHT12ConnectionSuccess);
                    uc_VhLk_Status_OHT13.SetConnStatus("OHT13", aLINE.OHT13ConnectionSuccess);
                    uc_VhLk_Status_OHT14.SetConnStatus("OHT14", aLINE.OHT14ConnectionSuccess);
                    /*PLC Status*/
                    uc_PLC_Status_MTL.SetConnStatus("MTL", aLINE.MTLConnectionSuccess);
                    uc_PLC_Status_MTS1.SetConnStatus("MTS", aLINE.MTSConnectionSuccess);
                    uc_PLC_Status_MTS2.SetConnStatus("MTS2", aLINE.MTS2ConnectionSuccess);
                    uc_PLC_Status_HID1.SetConnStatus("HID1", aLINE.HID1ConnectionSuccess);
                    uc_PLC_Status_HID2.SetConnStatus("HID2", aLINE.HID2ConnectionSuccess);
                    uc_PLC_Status_HID3.SetConnStatus("HID3", aLINE.HID3ConnectionSuccess);
                    uc_PLC_Status_HID4.SetConnStatus("HID4", aLINE.HID4ConnectionSuccess);
                    uc_PLC_Status_ADAM6050_1.SetConnStatus("ADAM6050-1", aLINE.Adam1ConnectionSuccess);
                    uc_PLC_Status_ADAM6050_2.SetConnStatus("ADAM6050-2", aLINE.Adam2ConnectionSuccess);
                    uc_PLC_Status_ADAM6050_3.SetConnStatus("ADAM6050-3", aLINE.Adam3ConnectionSuccess);
                    uc_PLC_Status_ADAM6050_4.SetConnStatus("ADAM6050-4", aLINE.Adam4ConnectionSuccess);
                    /*AP Status*/
                    uc_AP_Status_1.SetConnStatus("AP-1", aLINE.AP1ConnectionSuccess);
                    uc_AP_Status_2.SetConnStatus("AP-2", aLINE.AP2ConnectionSuccess);
                    uc_AP_Status_3.SetConnStatus("AP-3", aLINE.AP3ConnectionSuccess);
                    uc_AP_Status_4.SetConnStatus("AP-4", aLINE.AP4ConnectionSuccess);
                    uc_AP_Status_5.SetConnStatus("AP-5", aLINE.AP5ConnectionSuccess);
                    uc_AP_Status_6.SetConnStatus("AP-6", aLINE.AP6ConnectionSuccess);
                    uc_AP_Status_7.SetConnStatus("AP-7", aLINE.AP7ConnectionSuccess);
                    uc_AP_Status_8.SetConnStatus("AP-8", aLINE.AP8ConnectionSuccess);
                    uc_AP_Status_9.SetConnStatus("AP-9", aLINE.AP9ConnectionSuccess);
                    uc_AP_Status_10.SetConnStatus("AP-10", aLINE.AP10ConnectionSuccess);
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //初始化UI
        private void init()
        {
            try
            {
                CommunicationStatus.SetTitleName("Communication Status", "On", "Off");
                ControlStatus.SetTitleName("Control Status", "Online Remote", "Online Local", "Offline");
                TSCStatus.SetTitleName("TSC Status", "Auto", "Pause");

                CommunicationStatus.Button1.Click += Button_Click;
                CommunicationStatus.Button2.Click += Button_Click;
                ControlStatus.Button1.Click += Button_Click;
                ControlStatus.Button2.Click += Button_Click;
                ControlStatus.Button3.Click += Button_Click;
                TSCStatus.Button1.Click += Button_Click;
                TSCStatus.Button2.Click += Button_Click;
                ObjCacheManager_ConnectionInfo_update(null, null);
                ObjCacheManager_OnlineCheckInfo_update(null, null);
                ObjCacheManager_PingCheckInfo_update(null, null);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //var confirmResult = TipMessage_Request_Light.Show("Are you sure to change status ? ");

                if (sender.Equals(CommunicationStatus.Button1))
                {
                    await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkOK.ToString())));

                    //if (confirmResult != System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    //    TipMessage_Type_Light.Show("Successfully Command", "Successfully command to change communication status.", BCAppConstants.INFO_MSG);
                    //    await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkOK.ToString())));
                    //}

                }
                else if (sender.Equals(CommunicationStatus.Button2))
                {
                    await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkFail.ToString())));

                    //if (confirmResult != System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    //    TipMessage_Type_Light.Show("Successfully Command", "Successfully command to change communication status.", BCAppConstants.INFO_MSG);
                    //    await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkFail.ToString())));
                    //}
                }
                else if (sender.Equals(ControlStatus.Button1))
                {
                    await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote.ToString())));
                }
                else if (sender.Equals(ControlStatus.Button2))
                {
                    await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.On_Line_Local.ToString())));
                }
                else if (sender.Equals(ControlStatus.Button3))
                {
                    await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.EQ_Off_line.ToString())));
                }
                else if (sender.Equals(TSCStatus.Button1))
                {
                    await Task.Run(() => tscStateChange?.Invoke(this, new TSCStateUpdateEventArgs(ALINE.TSCState.AUTO.ToString())));
                }
                else if (sender.Equals(TSCStatus.Button2))
                {
                    await Task.Run(() => tscStateChange?.Invoke(this, new TSCStateUpdateEventArgs(ALINE.TSCState.PAUSED.ToString())));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class HostModeUpdateEventArgs : EventArgs
        {
            public HostModeUpdateEventArgs(string host_mode)
            {
                this.host_mode = host_mode;
            }

            public string host_mode { get; private set; }
        }

        public class TSCStateUpdateEventArgs : EventArgs
        {
            public TSCStateUpdateEventArgs(string tscstate)
            {
                this.tscstate = tscstate;
            }

            public string tscstate { get; private set; }
        }

        public class LinkStatusUpdateEventArgs : EventArgs
        {
            public LinkStatusUpdateEventArgs(string linkstatus)
            {
                this.linkstatus = linkstatus;
            }

            public string linkstatus { get; private set; }
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

        //private void uc_btn_on_Button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        on_Button_Click();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private async void on_Button_Click()
        //{
        //    try
        //    {
        //        //uc_btn_on.Enabled = false;
        //        //uc_btn_off.Enabled = true;
        //        await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkOK.ToString())));
        //        //uc_CommunicationStatus.SetConnSignal("On", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private void uc_btn_off_Button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        off_Button_Click();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private async void off_Button_Click()
        //{
        //    try
        //    {
        //        //uc_btn_on.Enabled = true;
        //        //uc_btn_off.Enabled = false;
        //        await Task.Run(() => linkStatusChange?.Invoke(this, new LinkStatusUpdateEventArgs(SCAppConstants.LinkStatus.LinkFail.ToString())));
        //        //uc_CommunicationStatus.SetConnSignal("Off", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private async void uc_btn_offline_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.Host_Offline.ToString())));
        //        //offline_Button_Click();

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private void offline_Button_Click()
        //{
        //    try
        //    {
        //        uc_btn_onlineR.Enabled = true;
        //        uc_btn_onlineL.Enabled = false;
        //        uc_btn_offline.Enabled = false;
        //        uc_ControlStatus.SetConnSignal("Offline", false);
        //        pause_Button_alloff_Click();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private async void uc_btn_onlineR_Button_Click(object sender, EventArgs e)
        //{
        //    await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote.ToString())));
        //    //hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote.ToString()));
        //    //uc_btn_onlineR.Enabled = false;
        //    //uc_btn_onlineL.Enabled = true;
        //    //uc_btn_offline.Enabled = true;
        //    //uc_ControlStatus.SetConnSignal("Online Remote", true);
        //    //uc_btn_auto.Enabled = true;
        //    //if (uc_btn_pause.Enabled==false)
        //    //{
        //    //    pause_Button_Click();
        //    //}
        //    //else
        //    //{
        //    //    auto_Button_Click();
        //    //}
        //}


        //private async void uc_btn_onlineL_Button_Click(object sender, EventArgs e)
        //{
        //    await Task.Run(() => hostModeChange?.Invoke(this, new HostModeUpdateEventArgs(SCAppConstants.LineHostControlState.HostControlState.On_Line_Local.ToString())));
        //    //uc_btn_onlineR.Enabled = true;
        //    //uc_btn_onlineL.Enabled = false;
        //    //uc_btn_offline.Enabled = true;
        //    //uc_ControlStatus.SetConnSignal("Online Local", true);
        //    //uc_btn_auto.Enabled = false;
        //}

        //private async void uc_btn_auto_Button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await Task.Run(() => tscStateChange?.Invoke(this, new TSCStateUpdateEventArgs(ALINE.TSCState.AUTO.ToString())));
        //        //auto_Button_Click();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private async void uc_btn_pause_Button_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await Task.Run(() => tscStateChange?.Invoke(this, new TSCStateUpdateEventArgs(ALINE.TSCState.AUTO.ToString())));
        //        await Task.Run(() => tscStateChange?.Invoke(this, new TSCStateUpdateEventArgs(ALINE.TSCState.PAUSED.ToString())));
        //        //pause_Button_Click();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private void auto_Button_Click()
        //{
        //    try
        //    {
        //        uc_btn_auto.Enabled = false;
        //        uc_btn_pause.Enabled = true;
        //        uc_TSCStatus.SetConnSignal("Auto", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private void pause_Button_Click()
        //{
        //    try
        //    {
        //        uc_btn_auto.Enabled = true;
        //        uc_btn_pause.Enabled = false;
        //        uc_TSCStatus.SetConnSignal("Pause", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //private void pause_Button_alloff_Click()
        //{
        //    try
        //    {
        //        uc_btn_auto.Enabled = false;
        //        uc_btn_pause.Enabled = false;
        //        uc_TSCStatus.SetConnSignal("Pause", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
        //protected override void OnHandleDestroyed(EventArgs e)
        //{
        //    app.ObjCacheManager.ConnectionInfoUpdate -= ObjCacheManager_ConnectionInfo_update;
        //    linkStatusChange -= LinkStatusChange;
        //    hostModeChange -= HostModeChange;
        //    tscStateChange -= TSCStateChange;
        //    app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_CONNECTION_INFO);
        //    base.OnHandleDestroyed(e);
        //}

        //private void Button1_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Button1");
        //    //throw new NotImplementedException();
        //}

        //public void buttonClick(Button btn1, Button btn2, Button btn3)
        //{
        //    try
        //    {
        //        btn1.Click += Button_Click;
        //        btn2.Click += Button_Click;
        //        btn3.Click += Button_Click;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try { ButtonClick(sender, e); }
        //    catch (Exception ex) { }
        //}

        //private void ButtonClick(object sender, RoutedEventArgs e)
        //{
        //    try {
        //        if (sender.Equals(btn1))
        //        {
        //            if (btn1_Click != null)
        //            {
        //                btn1_Click(sender, e);
        //            }
        //        }
        //        else if (sender.Equals(btn2))
        //        {
        //            if (btn2_Click != null)
        //            {      
        //                btn2_Click(sender, e);
        //            }
        //        }
        //        else if (sender.Equals(btn3))
        //        {
        //            if (btn3_Click != null)
        //            {      
        //                btn3_Click(sender, e);
        //            }
        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        //private void HostModeChange(object sender, HostModeUpdateEventArgs e)
        //{
        //    bool TSCstat_Color = false;
        //    bool SECSstat_Color = false;
        //    bool Controlstat_Color = false;
        //    ALINE aLINE = app.ObjCacheManager.GetLine();
        //    try
        //    {
        //        if (!app.LineBLL.SendHostModeChange(e.host_mode, out string result))
        //        {
        //            MessageBox.Show(result);
        //        }
        //        //app.LineBLL.SendHostModeChange(e.host_mode);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private void TSCStateChange(object sender, TSCStateUpdateEventArgs e)
        //{
        //    try
        //    {
        //        if (!app.LineBLL.SendTSCStateChange(e.tscstate, out string result))
        //        {
        //            MessageBox.Show(result);
        //        }
        //        //app.LineBLL.SendHostModeChange(e.host_mode);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //private void ObjCacheManager_ConnectionInfo_update(object sender, EventArgs e)
        //{
        //    bool TSCstat_Color = false;
        //    string TSCstat_Display = string.Empty;
        //    bool SECSstat_Color = false;
        //    string SECSstat_Display = string.Empty;
        //    bool Controlstat_Color = false;
        //    string Controlstat_Display = string.Empty;
        //    ALINE aLINE = app.ObjCacheManager.GetLine();
        //    try
        //    {
        //        switch (aLINE.Secs_Link_Stat)
        //        {
        //            case SCAppConstants.LinkStatus.LinkOK:
        //                SECSstat_Color = true;
        //                SECSstat_Display = BCAppConstants.SECSLinkDisplay.Link;
        //                break;
        //            case SCAppConstants.LinkStatus.LinkFail:
        //                SECSstat_Color = false;
        //                SECSstat_Display = BCAppConstants.SECSLinkDisplay.NotLink;
        //                break;
        //        }

        //        switch (aLINE.Host_Control_State)
        //        {
        //            case SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote:
        //                Controlstat_Color = true;
        //                Controlstat_Display = BCAppConstants.HostModeDisplay.OnlineRemote;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_onlineR.Enabled = false;
        //                    //uc_btn_onlineL.Enabled = true;
        //                    //uc_btn_offline.Enabled = true;
        //                    //uc_btn_auto.Enabled = true;
        //                    //uc_btn_pause.Enabled = true;
        //                }), null);
        //                break;
        //            case SCAppConstants.LineHostControlState.HostControlState.On_Line_Local:
        //                Controlstat_Color = true;
        //                Controlstat_Display = BCAppConstants.HostModeDisplay.OnlineLocal;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_onlineR.Enabled = true;
        //                    //uc_btn_onlineL.Enabled = false;
        //                    //uc_btn_offline.Enabled = true;
        //                    //uc_btn_auto.Enabled = true;
        //                    //uc_btn_pause.Enabled = true;
        //                }), null);
        //                break;
        //            case SCAppConstants.LineHostControlState.HostControlState.Host_Offline:
        //                Controlstat_Color = false;
        //                Controlstat_Display = BCAppConstants.HostModeDisplay.Offline;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_onlineR.Enabled = true;
        //                    //uc_btn_onlineL.Enabled = true;
        //                    //uc_btn_offline.Enabled = false;
        //                    //uc_btn_auto.Enabled = false;
        //                    //uc_btn_pause.Enabled = false;
        //                }), null);
        //                break;
        //        }

        //        switch (aLINE.SCStats)
        //        {
        //            case TSCState.AUTO:
        //                TSCstat_Color = true;
        //                TSCstat_Display = BCAppConstants.TSCStateDisplay.Auto;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_auto.Enabled = false;
        //                    //uc_btn_pause.Enabled = true;
        //                }), null);
        //                break;
        //            case TSCState.NONE:
        //                TSCstat_Color = false;
        //                TSCstat_Display = BCAppConstants.TSCStateDisplay.None;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_auto.Enabled = true;
        //                    //uc_btn_pause.Enabled = true;
        //                }), null);
        //                break;
        //            case TSCState.PAUSED:
        //                TSCstat_Color = false;
        //                TSCstat_Display = BCAppConstants.TSCStateDisplay.Pause;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_auto.Enabled = true;
        //                    //uc_btn_pause.Enabled = false;
        //                }), null);
        //                break;
        //            case TSCState.PAUSING:
        //                TSCstat_Color = false;
        //                TSCstat_Display = BCAppConstants.TSCStateDisplay.Pausing;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_auto.Enabled = false;
        //                    //uc_btn_pause.Enabled = false;
        //                }), null);
        //                break;
        //            case TSCState.TSC_INIT:
        //                TSCstat_Color = false;
        //                TSCstat_Display = BCAppConstants.TSCStateDisplay.Init;
        //                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
        //                {
        //                    //uc_btn_auto.Enabled = false;
        //                    //uc_btn_pause.Enabled = false;
        //                }), null);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }


        ////CommunicationStatus.SetConnSignal(SECSstat_Display, SECSstat_Color);
        ////ControlStatus.SetConnSignal(Controlstat_Display, Controlstat_Color);
        ////TSCStatus.SetConnSignal(TSCstat_Display, TSCstat_Color);
        //CommunicationStatus.SetTitleNameDefault("Communication Status", aLINE.Secs_Link_Stat.ToString(), "On", "Off", SECSstat_Color);
        //    ControlStatus.SetTitleName("Control Status", aLINE.Host_Control_State.ToString(), "Online Remote", "Online Local", "Offline", Controlstat_Color);
        //    TSCStatus.SetTitleNameDefault("TSC Status", aLINE.SCStats.ToString(), "Auto", "Pause", TSCstat_Color);


        //}


        //private void ObjCacheManager_ConnectionInfo_update(object sender, EventArgs e)
        //{
        //    app = ohxc.winform.App.WindownApplication.getInstance();
        //    app.ObjCacheManager.ConnectionInfoUpdate += ObjCacheManager_ConnectionInfo_update;
        //    ALINE aLINE = app.ObjCacheManager.GetLine();
        //    bool TSCstat_Color = false;
        //    bool SECSstat_Color = false;
        //    bool Controlstat_Color = false;
        //    try
        //    {
        //        if (aLINE.SCStats.ToString() == "AUTO")
        //        {
        //            TSCstat_Color = true;
        //        }
        //        else
        //        {
        //            TSCstat_Color = false;
        //        }

        //        if (aLINE.Secs_Link_Stat.ToString() == "LinkOK")
        //        {
        //            SECSstat_Color = true;
        //        }
        //        else
        //        {
        //            SECSstat_Color = false;
        //        }

        //        if (aLINE.Host_Control_State.ToString() == "Host_Offline")
        //        {
        //            Controlstat_Color = false;
        //        }
        //        else
        //        {
        //            Controlstat_Color = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //    CommunicationStatus.SetTitleNameDefault("Communication Status", aLINE.Secs_Link_Stat.ToString(), "On", "Off", SECSstat_Color);
        //    ControlStatus.SetTitleName("Control Status", aLINE.Host_Control_State.ToString(), "Online Remote", "Online Local", "Offline", Controlstat_Color);
        //    TSCStatus.SetTitleNameDefault("TSC Status", aLINE.SCStats.ToString(), "Auto", "Pause", TSCstat_Color);
        //}

        //初始化UI
        //private void initUI()
        //{
        //    try
        //    {
        //        CommunicationStatus.SetTitleNameDefault("Communication Status", "TEST", "On", "Off", true);
        //        ControlStatus.SetTitleName("Control Status", "TEST", "Online Remote", "Online Local", "Offline", true);
        //        TSCStatus.SetTitleNameDefault("TSC Status", "TEST", "Auto", "Pause", false);

        //        /*Control Status*/
        //        //uc_ControlStatus1.SetConnStatus("Current port states", true);
        //        //uc_ControlStatus2.SetConnStatus("Current state", true);
        //        //uc_ControlStatus3.SetConnStatus("Enhanced vehicles", true);
        //        //uc_ControlStatus4.SetConnStatus("TSC state", true);
        //        //uc_ControlStatus5.SetConnStatus("Unit alarm state list", true);
        //        //uc_ControlStatus6.SetConnStatus("Enhanced transfers", true);
        //        //uc_ControlStatus7.SetConnStatus("Enhanced carriers", false);
        //        //uc_ControlStatus8.SetConnStatus("Lane cut list", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //public class HostModeUpdateEventArgs : EventArgs
        //{
        //    public HostModeUpdateEventArgs(string host_mode)
        //    {
        //        this.host_mode = host_mode;
        //    }

        //    public string host_mode { get; private set; }
        //}

        //public class TSCStateUpdateEventArgs : EventArgs
        //{
        //    public TSCStateUpdateEventArgs(string tscstate)
        //    {
        //        this.tscstate = tscstate;
        //    }

        //    public string tscstate { get; private set; }
        //}



    }
}
