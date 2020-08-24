//*********************************************************************************
//      uc_SP_VehicleManagement.cs
//*********************************************************************************
// File Name: uc_SP_VehicleManagement.cs
// Description: Vehicle Management
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.sc;
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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_VehicleManagement.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_VehicleManagement : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        App.WindownApplication app = null;
        public event EventHandler CloseFormEvent;
        public event EventHandler<PauseStatusChangeEventArgs> pauseStatusChange;
        public event EventHandler<ModeStatusChangeEventArgs> modeStatusChange;
        public event EventHandler<VehicleCommandEventArgs> vehicleCommandSend;
        public event EventHandler<VehicleAlarmResetSendEventArgs> vehicleAlarmResetSend;
        public event EventHandler<VehicleResetEventArgs> vehicleResetSend;
        public event EventHandler<VehicleCMDCancelAbortEventArgs> vehicleCMDCancelAbort;
        SysExcuteQualityQueryService sysExcuteQualityQueryService;
        //Form form = null;
        string startDate = "";
        string endDate = "";
        //Color mouse_enter_DGVcells = Color.FromArgb(0, 91, 168);
        #endregion 公用參數設定

        public uc_SP_VehicleManagement()
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
                app = App.WindownApplication.getInstance();
                initTitle();
                init();
                startupUI("", "");
                cb_HrsInterval.Items.Add("");
                for (int i = 1; i <= 24; i++)
                {
                    cb_HrsInterval.Items.Add("Last " + i + " Hours");
                }
                //form = _form;
                //uctl_ElasticQuery_sys_process_log.QueryCommandDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt;
                uctl_ElasticQuery_sys_process_log.QueryCommandDetailEnevt += Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public async void startupUI(string cmdid, string vhid)
        {
            try
            {
                //this.Width = 1711;
                //this.Height = 754;
                m_StartDTCbx.Value = DateTime.Now.AddMonths(-6);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                CommunLog_MCSCmdID.Text = cmdid;
                CommunLog_VhID.Text = vhid;
                if (!(this.CommunLog_MCSCmdID.Text == string.Empty) || !(this.CommunLog_VhID.Text == string.Empty))
                {
                    await search();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initTitle()
        {
            try
            {
                ALINE aLINE = app.ObjCacheManager.GetLine();

                VehicleStatus.SetTXBTitleName("Vehicle ID", "Mode", "Current Address", "Current Section", "Section Distance", "Alarm Count");
                CommandStatus.SetTXBTitleName("Action Status", "MCS Command ID", "Vehicle Command ID", "Command Type", "Carrier ID", "Source", "Destination");
                VehicleStatus.SetBTNTitleName("Auto Remote", "Auto Local", "Auto MTL", "Auto MTS", "Manual", "Alarm Reset");
                CommandStatus.SetBTNTitleName("Cancel/Abort Cmd.");
                VehicleID.SetTitleName("Vehicle ID");
                PauseType.SetTXBTitleName("Error Status", "Normal Pause", "Block Pause", "Obstacle Pause", "HID Pause", "Safety Pause", "Earthquake Pause", "Pause Type");
                PauseType.SetBTNTitleName("Pause", "Continue");
                VehicleCommand.SetTXBTitleName("Vehicle ID", "Command Type", "Carrier ID", "Source", "Destination");
                VehicleCommand.SetBTNTitleName("Command");
                uc_VhLk_Status_1.SetConnStatus("OHT01", aLINE.OHT1ConnectionSuccess);
                uc_VhLk_Status_2.SetConnStatus("OHT02", aLINE.OHT2ConnectionSuccess);
                uc_VhLk_Status_3.SetConnStatus("OHT03", aLINE.OHT3ConnectionSuccess);
                uc_VhLk_Status_4.SetConnStatus("OHT04", aLINE.OHT4ConnectionSuccess);
                uc_VhLk_Status_5.SetConnStatus("OHT05", aLINE.OHT5ConnectionSuccess);
                uc_VhLk_Status_6.SetConnStatus("OHT06", aLINE.OHT6ConnectionSuccess);
                uc_VhLk_Status_7.SetConnStatus("OHT07", aLINE.OHT7ConnectionSuccess);
                uc_VhLk_Status_8.SetConnStatus("OHT08", aLINE.OHT8ConnectionSuccess);
                uc_VhLk_Status_9.SetConnStatus("OHT09", aLINE.OHT9ConnectionSuccess);
                uc_VhLk_Status_10.SetConnStatus("OHT10", aLINE.OHT10ConnectionSuccess);
                uc_VhLk_Status_11.SetConnStatus("OHT11", aLINE.OHT11ConnectionSuccess);
                uc_VhLk_Status_12.SetConnStatus("OHT12", aLINE.OHT12ConnectionSuccess);
                uc_VhLk_Status_13.SetConnStatus("OHT13", aLINE.OHT13ConnectionSuccess);
                uc_VhLk_Status_14.SetConnStatus("OHT14", aLINE.OHT14ConnectionSuccess);
                //uc_TitleContent.SetTitleNameInComuLog("MCS Command ID", "Vehicle ID");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void init()
        {
            registerEvent();

            ALINE aLINE = app.ObjCacheManager.GetLine();
            List<AVEHICLE> vhs = app.ObjCacheManager.GetVEHICLEs().ToList();
            foreach (AVEHICLE vh in app.ObjCacheManager.GetVEHICLEs())
            {
                VehicleID.combo_Content.Items.Add(vh.VEHICLE_ID);
            }
            VehicleID.combo_Content.SelectedIndex = 0;
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Normal);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Block);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Obstacle);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Hid);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Safty);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.Earthquake);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.ManualBlock);
            PauseType.combo_Content.Items.Add(sc.App.SCAppConstants.OHxCPauseType.ManualHID);
            PauseType.combo_Content.SelectedIndex = 0;

            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Move);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Move_Park);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Move_MTPort);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Load);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Unload);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.LoadUnload);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Teaching);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Continue);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Round);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Home);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.Override);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.MTLHome);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.MoveToMTL);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.SystemOut);
            VehicleCommand.combo_Content1.Items.Add(E_CMD_TYPE.SystemIn);

            foreach (AADDRESS adr in app.ObjCacheManager.GetAddresses())
            {
                string s_address = adr.ADR_ID;
                try
                {
                    APORTSTATION port = app.ObjCacheManager.GetPortStations().Where(p => p.ADR_ID.Trim() == s_address).FirstOrDefault();
                    if (port != null)
                    {
                        s_address = s_address + $" ({port.PORT_ID.Trim()})";
                    }
                    VehicleCommand.combo_Content2.Items.Add(s_address);
                    VehicleCommand.combo_Content3.Items.Add(s_address);
                }
                catch (Exception ex)
                {

                }

            }
            VehicleCommand.combo_Content1.SelectedIndex = 0;
            VehicleCommand.combo_Content2.SelectedIndex = 0;
            VehicleCommand.combo_Content3.SelectedIndex = 0;

            List<ALARM> vh_alarms = app.ObjCacheManager.GetAlarms().Where(a => a.EQPT_ID.Trim() == VehicleID.combo_Content.Text.Trim()).ToList();
            txb_AlarmCnt.Text = vh_alarms.Count.ToString();
            alarmlist.ItemsSource = vh_alarms;
        }

        private void registerEvent()
        {
            try
            {
                PauseType.btn_Title1.Click += btn_Click;
                PauseType.btn_Title2.Click += btn_Click;
                VehicleStatus.btn_Title1.Click += btn_Click;
                VehicleStatus.btn_Title2.Click += btn_Click;
                VehicleStatus.btn_Title3.Click += btn_Click;
                VehicleStatus.btn_Title4.Click += btn_Click;
                VehicleStatus.btn_Title5.Click += btn_Click;
                VehicleStatus.btn_Title6.Click += btn_Click;

                VehicleCommand.btn_Title1.Click += btn_Click;
                CommandStatus.btn_Title1.Click += btn_Click;
                btn_VhReset.Click += btn_Click;

                VehicleCommand.combo_Content1.SelectionChanged += commandTypeSelecttionChanged;
                PauseType.combo_Content.SelectionChanged += pauseSelecttionChanged;
                VehicleID.combo_Content.SelectionChanged += vehicleSelecttionChanged;


                pauseStatusChange += PauseStatusChange;
                modeStatusChange += ModeStatusChange;
                vehicleCommandSend += VehicleCommandSend;
                vehicleAlarmResetSend += VehicleAlarmResetSend;
                vehicleResetSend += VehicleResetSend;
                vehicleCMDCancelAbort += VehicleCommandCancelAbortSend;
                //app.ObjCacheManager.VehicleUpdateComplete += ObjCacheManager_VehicleUpdateComplete;
                app.CurrentAlarmChange += update_alarm_info;
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
                PauseType.btn_Title1.Click -= btn_Click;
                PauseType.btn_Title2.Click -= btn_Click;
                VehicleStatus.btn_Title1.Click -= btn_Click;
                VehicleStatus.btn_Title2.Click -= btn_Click;
                VehicleStatus.btn_Title3.Click -= btn_Click;
                VehicleStatus.btn_Title4.Click -= btn_Click;
                VehicleStatus.btn_Title5.Click -= btn_Click;
                VehicleStatus.btn_Title6.Click -= btn_Click;

                VehicleCommand.btn_Title1.Click -= btn_Click;
                CommandStatus.btn_Title1.Click -= btn_Click;
                btn_VhReset.Click -= btn_Click;

                VehicleCommand.combo_Content1.SelectionChanged -= commandTypeSelecttionChanged;
                PauseType.combo_Content.SelectionChanged -= pauseSelecttionChanged;
                VehicleID.combo_Content.SelectionChanged -= vehicleSelecttionChanged;


                pauseStatusChange -= PauseStatusChange;
                modeStatusChange -= ModeStatusChange;
                vehicleCommandSend -= VehicleCommandSend;
                vehicleAlarmResetSend -= VehicleAlarmResetSend;
                vehicleResetSend -= VehicleResetSend;
                vehicleCMDCancelAbort -= VehicleCommandCancelAbortSend;
                app.ObjCacheManager.VehicleUpdateComplete -= ObjCacheManager_VehicleUpdateComplete;
                app.CurrentAlarmChange -= update_alarm_info;
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
                string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;

                if (!string.IsNullOrEmpty(select_vh_id))
                {
                    AVEHICLE select_vh = app.ObjCacheManager.GetVEHICLE(select_vh_id);
                    //ACMD_MCS mcs_cmd = app.CmdBLL.GetCmd_MCSByID(select_vh.MCS_CMD);
                    ACMD_OHTC ohxc_cmd = app.CmdBLL.GetCmd_OhtcByID(select_vh.OHTC_CMD);
                    VehicleStatus.SetTXBVehicleInfo(select_vh.VEHICLE_ID, select_vh.MODE_STATUS.ToString(), select_vh.CUR_ADR_ID
                        , select_vh.CUR_SEC_ID, select_vh.ACC_SEC_DIST.ToString(), app.ObjCacheManager.getAlarmCountByVehicleID(select_vh_id).ToString());
                    PauseType.SetTXBPauseInfo(select_vh.ERROR == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.PauseStatus == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.BLOCK_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.OBS_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.HID_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.SAFETY_DOOR_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.EARTHQUAKE_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF);
                    CommandStatus.SetVehicleCmdInfo(select_vh.ACT_STATUS.ToString(), select_vh.MCS_CMD, select_vh.OHTC_CMD,
                        ohxc_cmd != null ? ohxc_cmd.CMD_TPYE.ToString() : string.Empty, ohxc_cmd != null ? ohxc_cmd.CARRIER_ID : string.Empty, ohxc_cmd != null ? ohxc_cmd.SOURCE : string.Empty, ohxc_cmd != null ? ohxc_cmd.DESTINATION : string.Empty);
                    VehicleCommand.SetVehicleCommandInfo(select_vh_id);
                    sc.App.SCAppConstants.OHxCPauseType pause_type = (sc.App.SCAppConstants.OHxCPauseType)PauseType.combo_Content.SelectedItem;
                    switch (pause_type)
                    {
                        case sc.App.SCAppConstants.OHxCPauseType.Normal:
                            if (PauseType.txb_Value2.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.Block:
                            if (PauseType.txb_Value3.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.Obstacle:
                            if (PauseType.txb_Value4.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.Hid:
                            if (PauseType.txb_Value5.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.Safty:
                            if (PauseType.txb_Value6.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.Earthquake:
                            if (PauseType.txb_Value7.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.ManualBlock:
                            if (PauseType.txb_Value3.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                        case sc.App.SCAppConstants.OHxCPauseType.ManualHID:
                            if (PauseType.txb_Value5.Text == BCAppConstants.SIGNAL_OFF)
                            {
                                PauseType.btn_Title1.IsEnabled = true;
                                PauseType.btn_Title2.IsEnabled = false;
                            }
                            else
                            {
                                PauseType.btn_Title1.IsEnabled = false;
                                PauseType.btn_Title2.IsEnabled = true;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void vehicleSelecttionChanged(object sender, EventArgs e)
        {
            try
            {
                string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                List<ALARM> vh_alarms = app.ObjCacheManager.GetAlarms().Where(a => a.EQPT_ID.Trim() == select_vh_id.Trim()).ToList();
                txb_AlarmCnt.Text = vh_alarms.Count.ToString();
                alarmlist.ItemsSource = vh_alarms;
                if (!string.IsNullOrEmpty(select_vh_id))
                {
                    AVEHICLE select_vh = app.ObjCacheManager.GetVEHICLE(select_vh_id);
                    //ACMD_MCS mcs_cmd = app.CmdBLL.GetCmd_MCSByID(select_vh.MCS_CMD);
                    ACMD_OHTC ohxc_cmd = app.CmdBLL.GetCmd_OhtcByID(select_vh.OHTC_CMD);
                    VehicleStatus.SetTXBVehicleInfo(select_vh.VEHICLE_ID, select_vh.MODE_STATUS.ToString(), select_vh.CUR_ADR_ID
                        , select_vh.CUR_SEC_ID, select_vh.ACC_SEC_DIST.ToString(), app.ObjCacheManager.getAlarmCountByVehicleID(select_vh_id).ToString());
                    PauseType.SetTXBPauseInfo(select_vh.ERROR == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.PauseStatus == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.BLOCK_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.OBS_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.HID_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF, select_vh.SAFETY_DOOR_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF,
                        select_vh.EARTHQUAKE_PAUSE == VhStopSingle.StopSingleOn ? BCAppConstants.SIGNAL_ON : BCAppConstants.SIGNAL_OFF);
                    CommandStatus.SetVehicleCmdInfo(select_vh.ACT_STATUS.ToString(), select_vh.MCS_CMD, select_vh.OHTC_CMD,
                        ohxc_cmd != null ? ohxc_cmd.CMD_TPYE.ToString() : string.Empty, ohxc_cmd != null ? ohxc_cmd.CARRIER_ID : string.Empty, ohxc_cmd != null ? ohxc_cmd.SOURCE : string.Empty, ohxc_cmd != null ? ohxc_cmd.DESTINATION : string.Empty);
                    VehicleCommand.SetVehicleCommandInfo(select_vh_id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void commandTypeSelecttionChanged(object sender, EventArgs e)
        {
            try
            {
                E_CMD_TYPE select_cmd_type = (E_CMD_TYPE)VehicleCommand.combo_Content1.SelectedItem;
                switch (select_cmd_type)
                {
                    case E_CMD_TYPE.Move:
                        VehicleCommand.combo_Content2.IsEnabled = false;
                        VehicleCommand.combo_Content3.IsEnabled = true;
                        break;
                    case E_CMD_TYPE.Load:
                        VehicleCommand.combo_Content2.IsEnabled = true;
                        VehicleCommand.combo_Content3.IsEnabled = false;
                        break;
                    case E_CMD_TYPE.Unload:
                        VehicleCommand.combo_Content2.IsEnabled = false;
                        VehicleCommand.combo_Content3.IsEnabled = true;
                        break;
                    case E_CMD_TYPE.LoadUnload:
                        VehicleCommand.combo_Content2.IsEnabled = true;
                        VehicleCommand.combo_Content3.IsEnabled = true;
                        break;
                    default:
                        VehicleCommand.combo_Content2.IsEnabled = true;
                        VehicleCommand.combo_Content3.IsEnabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void pauseSelecttionChanged(object sender, EventArgs e)
        {
            try
            {
                sc.App.SCAppConstants.OHxCPauseType pause_type = (sc.App.SCAppConstants.OHxCPauseType)PauseType.combo_Content.SelectedItem;
                switch (pause_type)
                {
                    case sc.App.SCAppConstants.OHxCPauseType.Normal:
                        if (PauseType.txb_Value2.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                    case sc.App.SCAppConstants.OHxCPauseType.Block:
                        if (PauseType.txb_Value3.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                    case sc.App.SCAppConstants.OHxCPauseType.Obstacle:
                        if (PauseType.txb_Value4.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                    case sc.App.SCAppConstants.OHxCPauseType.Hid:
                        if (PauseType.txb_Value5.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                    case sc.App.SCAppConstants.OHxCPauseType.Safty:
                        if (PauseType.txb_Value6.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                    case sc.App.SCAppConstants.OHxCPauseType.Earthquake:
                        if (PauseType.txb_Value7.Text == BCAppConstants.SIGNAL_OFF)
                        {
                            PauseType.btn_Title1.IsEnabled = true;
                            PauseType.btn_Title2.IsEnabled = false;
                        }
                        else
                        {
                            PauseType.btn_Title1.IsEnabled = false;
                            PauseType.btn_Title2.IsEnabled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void PauseStatusChange(object sender, PauseStatusChangeEventArgs e)
        {
            try
            {
                if (!app.VehicleBLL.SendPauseStatusChange(e.vh_id, e.pauseType, e.event_type, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Pause failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Pause succeeded", "", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
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

        private void VehicleResetSend(object sender, VehicleResetEventArgs e)
        {
            try
            {
                string vh_id = e.vh_id;
                app.OperationHistoryBLL.addOperationHis(app.LoginUserID, nameof(uc_SP_VehicleManagement), $"Excute vh:{vh_id} reset posiion...");
                if (!app.VehicleBLL.SendVehicleResetToControl(vh_id, out string result))
                {
                    TipMessage_Type_Light.Show("Send reset failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send reset succeeded", "", BCAppConstants.INFO_MSG);
                }
                app.OperationHistoryBLL.addOperationHis(app.LoginUserID, nameof(uc_SP_VehicleManagement), $"Excute vh:{vh_id} reset posiion, action result:{result}");
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
                string vh_id = e.vh_id;
                app.OperationHistoryBLL.addOperationHis(app.LoginUserID, nameof(uc_SP_VehicleManagement), $"Excute vh:{vh_id} cancel/abort command...");
                if (!app.VehicleBLL.SendVehicleCMDCancelAbortToControl(vh_id, out string result))
                {
                    TipMessage_Type_Light.Show("Send Cancel/Abort failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send Cancel/Abort succeed", "", BCAppConstants.INFO_MSG);
                }
                app.OperationHistoryBLL.addOperationHis(app.LoginUserID, nameof(uc_SP_VehicleManagement), $"Excute vh:{vh_id} cancel/abort command,action resule:{result}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void VehicleAlarmResetSend(object sender, VehicleAlarmResetSendEventArgs e)
        {
            try
            {
                if (!app.VehicleBLL.SendVehicleAlarmResetRequest(e.vh_id, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("Send vehicle alarm reset failed", result, BCAppConstants.INFO_MSG);
                }
                else
                {
                    TipMessage_Type_Light.Show("Send vehicle alarm reset succeed", "", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
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

        private void update_alarm_info(object obj, List<ALARM> alarms)
        {
            try
            {
                Adapter.Invoke((o) =>
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    List<ALARM> vh_alarms = alarms.Where(a => a.EQPT_ID.Trim() == select_vh_id.Trim()).ToList();
                    txb_AlarmCnt.Text = vh_alarms.Count.ToString();
                    alarmlist.ItemsSource = vh_alarms;
                    refreshUI();
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
                if (sender.Equals(btn_reset))
                {
                    //foreach (string vh_id in app.ObjCacheManager.hasAlarmVehicleList())
                    //{
                    //    vehicleAlarmResetSend?.Invoke(this, new VehicleAlarmResetSendEventArgs(vh_id));
                    //}
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    vehicleAlarmResetSend?.Invoke(this, new VehicleAlarmResetSendEventArgs(select_vh_id));
                }
                else if (sender.Equals(VehicleStatus.btn_Title1))//AutoRemote
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoRemote));
                }
                else if (sender.Equals(VehicleStatus.btn_Title2))//AutoLocal
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoLocal));
                }
                else if (sender.Equals(VehicleStatus.btn_Title3))//AutoMTL
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoMtl));
                }
                else if (sender.Equals(VehicleStatus.btn_Title4))//AutoMTS
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.AutoMts));
                }
                else if (sender.Equals(VehicleStatus.btn_Title5))//Manual
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    modeStatusChange?.Invoke(this, new ModeStatusChangeEventArgs(select_vh_id, VHModeStatus.Manual));
                }
                else if (sender.Equals(VehicleStatus.btn_Title6))//alarm reset
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    vehicleAlarmResetSend?.Invoke(this, new VehicleAlarmResetSendEventArgs(select_vh_id));
                }

                else if (sender.Equals(PauseType.btn_Title1))
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    sc.App.SCAppConstants.OHxCPauseType pause_type = (sc.App.SCAppConstants.OHxCPauseType)PauseType.combo_Content.SelectedItem;
                    pauseStatusChange?.Invoke(this, new PauseStatusChangeEventArgs(select_vh_id, pause_type, sc.ProtocolFormat.OHTMessage.PauseEvent.Pause));
                }
                else if (sender.Equals(PauseType.btn_Title2))
                {
                    string select_vh_id = (string)VehicleID.combo_Content.SelectedItem;
                    sc.App.SCAppConstants.OHxCPauseType pause_type = (sc.App.SCAppConstants.OHxCPauseType)PauseType.combo_Content.SelectedItem;
                    pauseStatusChange?.Invoke(this, new PauseStatusChangeEventArgs(select_vh_id, pause_type, sc.ProtocolFormat.OHTMessage.PauseEvent.Continue));
                }
                else if (sender.Equals(VehicleCommand.btn_Title1))
                {
                    string select_vh_id = (string)VehicleCommand.txt_Content1.Text;
                    E_CMD_TYPE cmd_type = (E_CMD_TYPE)VehicleCommand.combo_Content1.SelectedItem;
                    string carrier = (string)VehicleCommand.txt_Content2.Text;
                    string source = ((string)VehicleCommand.combo_Content2.SelectedItem).Split(' ')[0];
                    string destination = ((string)VehicleCommand.combo_Content3.SelectedItem).Split(' ')[0];
                    //string = (string)VehicleCommand.txt_Content1.Text;

                    vehicleCommandSend?.Invoke(this, new VehicleCommandEventArgs(select_vh_id, cmd_type, carrier, source, destination));
                }
                else if (sender.Equals(btn_VhReset))
                {
                    string select_vh_id = (string)VehicleCommand.txt_Content1.Text;
                    vehicleResetSend?.Invoke(this, new VehicleResetEventArgs(select_vh_id));
                }
                else if (sender.Equals(CommandStatus.btn_Title1))
                {
                    string select_vh_id = (string)VehicleCommand.txt_Content1.Text;
                    vehicleCMDCancelAbort?.Invoke(this, new VehicleCMDCancelAbortEventArgs(select_vh_id));
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
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1(string vh_id, DateTime query_start_time)
        {
            try
            {
                DateTime dateTimeFrom = query_start_time.AddSeconds(-30);
                DateTime dateTimeTo = query_start_time.AddHours(1);
                m_StartDTCbx.Value = dateTimeFrom;
                m_EndDTCbx.Value = dateTimeTo;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;

                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                CommunLog_VhID.Text = vh_id;
                uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async Task search()
        {
            try
            {
                DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
                DateTime dateTimeTo = System.Convert.ToDateTime(m_EndDTCbx.Value);
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                string cmd_id = CommunLog_MCSCmdID.Text;
                string vh_id = CommunLog_VhID.Text;
                List<RecordReportInfo> record_report_info = null;
                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, cmd_id));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);

            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
            }

            //string keyword = string.Empty;
            //if (SCUtility.isMatche(McsCmdLog_CstID.Text, ""))
            //{
            //    keyword = string.Empty;
            //}
            //else
            //{
            //    keyword = McsCmdLog_CstID.Text.Trim();
            //}

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender.Equals(HypL30mins))
                {
                    //HypL30mins.link = true;
                    m_StartDTCbx.Value = DateTime.Now.AddMinutes(-30);
                    await ClearData();
                    await search();
                }
                else if (sender.Equals(HypL1hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
                else if (sender.Equals(HypL4hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
                else if (sender.Equals(HypL12hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
                else if (sender.Equals(HypL24hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-1);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
                else if (sender.Equals(HypL2days))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-2);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
                else if (sender.Equals(HypL3days))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-3);
                    m_EndDTCbx.Value = DateTime.Now;
                    sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                    await search();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async Task ClearData()
        {
            try
            {
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                CommunLog_VhID.Clear();
                CommunLog_MCSCmdID.Clear();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void cb_HrsInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (cb_HrsInterval.SelectedIndex)
                {
                    case 0:
                        m_StartDTCbx.Value = DateTime.Now.AddMonths(-6);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 1:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 2:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-2);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 3:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-3);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 4:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 5:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-5);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 6:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-6);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 7:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-7);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 8:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-8);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 9:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-9);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 10:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-10);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 11:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-11);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 12:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 13:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-13);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 14:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-14);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 15:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-15);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 16:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-16);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 17:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-17);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 18:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-18);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 19:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-19);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 20:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-20);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 21:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-21);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 22:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-22);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 23:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-23);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 24:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-24);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_VhReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


    }
    public class PauseStatusChangeEventArgs : EventArgs
    {
        public PauseStatusChangeEventArgs(string vh_id, sc.App.SCAppConstants.OHxCPauseType pauseType, PauseEvent event_type)
        {
            this.vh_id = vh_id;
            this.pauseType = pauseType;
            this.event_type = event_type;
        }
        public string vh_id { get; private set; }
        public sc.App.SCAppConstants.OHxCPauseType pauseType { get; private set; }
        public sc.ProtocolFormat.OHTMessage.PauseEvent event_type { get; private set; }
    }
    public class ModeStatusChangeEventArgs : EventArgs
    {
        public ModeStatusChangeEventArgs(string vh_id, VHModeStatus modeStatus)
        {
            this.vh_id = vh_id;
            this.modeStatus = modeStatus;
        }
        public string vh_id { get; private set; }
        public VHModeStatus modeStatus { get; private set; }
    }

    public class VehicleCommandEventArgs : EventArgs
    {
        public VehicleCommandEventArgs(string vh_id, E_CMD_TYPE cmd_type, string carrier_id, string source, string destination)
        {
            this.vh_id = vh_id;
            this.cmd_type = cmd_type;
            this.carrier_id = carrier_id;
            this.source = source;
            this.destination = destination;

        }
        public string vh_id { get; private set; }
        public E_CMD_TYPE cmd_type { get; private set; }
        public string carrier_id { get; private set; }
        public string source { get; private set; }
        public string destination { get; private set; }
    }

    public class VehicleResetEventArgs : EventArgs
    {
        public VehicleResetEventArgs(string vh_id)
        {
            this.vh_id = vh_id;
        }
        public string vh_id { get; private set; }
    }

    public class VehicleCMDCancelAbortEventArgs : EventArgs
    {
        public VehicleCMDCancelAbortEventArgs(string vh_id)
        {
            this.vh_id = vh_id;
        }
        public string vh_id { get; private set; }
    }

    public class VehicleAlarmResetSendEventArgs : EventArgs
    {
        public VehicleAlarmResetSendEventArgs(string vh_id)
        {
            this.vh_id = vh_id;
        }
        public string vh_id { get; private set; }
    }
}
