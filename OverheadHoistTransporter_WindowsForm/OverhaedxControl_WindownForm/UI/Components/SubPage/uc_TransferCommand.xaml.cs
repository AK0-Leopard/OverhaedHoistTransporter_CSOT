//*********************************************************************************
//      uc_TransferCommand.cs
//*********************************************************************************
// File Name: uc_TransferCommand.cs
// Description: Transfer Command
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
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_TransferCommand.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TransferCommand : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string identity = string.Empty;
        public event EventHandler<MCSCommandStatusChangeEventArgs> mSCCommandStatus;
        public event EventHandler<MCSCommandAssignVeicleEventArgs> mSCCommandAssignVehicle;
        public event EventHandler<MCSCommandShiftEventArgs> mCSCommandShift;
        public event EventHandler CloseFormEvent;
        App.WindownApplication app = null;
        string mcs_cmd_id;
        #endregion 公用參數設定

        public uc_TransferCommand()
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

        public void initUI(TarnferCMDViewObj mcs_cmd, string identifier)
        {
            try
            {
                app = App.WindownApplication.getInstance();
                identity = identifier;
                mcs_cmd_id = mcs_cmd.CMD_ID;
                if (identifier == BCAppConstants.SubPageIdentifier.TRANSFER_CHANGE_STATUS)
                {
                    txt_McsCmdID.Text = mcs_cmd.CMD_ID;
                    ComboBox.Items.Clear();
                    foreach (E_TRAN_STATUS item in Enum.GetValues(typeof(E_TRAN_STATUS)))
                    {
                        ComboBox.Items.Add(item);
                    }
                    ComboBox.SelectedItem = mcs_cmd.TRANSFERSTATE;
                    registerEvent_MCSCommandStatusChange();
                }
                else if (identifier == BCAppConstants.SubPageIdentifier.TRANSFER_ASSIGN_VEHICLE)
                {
                    txt_McsCmdID.Text = mcs_cmd.CMD_ID;
                    ComboBox.Items.Clear();
                    List<AVEHICLE> vhs = app.ObjCacheManager.GetVEHICLEs().ToList();
                    app.VehicleBLL.filterVh(ref vhs, E_VH_TYPE.None);
                    foreach (AVEHICLE vh in vhs)
                    {
                        ComboBox.Items.Add(vh.VEHICLE_ID);
                    }
                    registerEvent_MCSCommandVehicleAssign();

                }
                else if (identifier == BCAppConstants.SubPageIdentifier.TRANSFER_SHIFT_COMMAND)
                {
                    txt_McsCmdID.Text = mcs_cmd.CMD_ID;
                    ComboBox.Items.Clear();
                    List<AVEHICLE> vhs = app.ObjCacheManager.GetVEHICLEs().ToList();
                    app.VehicleBLL.filterVh(ref vhs, E_VH_TYPE.None);
                    foreach (AVEHICLE vh in vhs.ToList())
                    {
                        if (mcs_cmd.VH_ID.Trim() == vh.VEHICLE_ID)
                        {
                            vhs.Remove(vh);
                        }
                    }
                    foreach (AVEHICLE vh in vhs)
                    {
                        ComboBox.Items.Add(vh.VEHICLE_ID);
                    }
                    registerEvent_MCSCommandShift();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void registerEvent_MCSCommandStatusChange()
        {
            try
            {
                mSCCommandStatus += MCSCommandStatusChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void registerEvent_MCSCommandVehicleAssign()
        {
            try
            {
                mSCCommandAssignVehicle += MCSCommandVehicleAssign;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void registerEvent_MCSCommandShift()
        {
            try
            {
                mCSCommandShift += MCSCommandShift;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void unRegisterEvent_MCSCommandStatusChange()
        {
            try
            {
                mSCCommandStatus -= MCSCommandStatusChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void unRegisterEvent_MCSCommandVehicleAssign()
        {
            try
            {
                mSCCommandAssignVehicle -= MCSCommandVehicleAssign;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void unRegisterEvent_MCSCommandShift()
        {
            try
            {
                mCSCommandShift -= MCSCommandShift;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox.Focus(); //將游標放置指定位置
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_McsCmdID, false); //設置IME和輸入是否可以是中文
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetTitleName(string title, string contentTitle)
        {
            try
            {
                Title.Text = title;
                ContentTitle.Text = contentTitle;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (identity == BCAppConstants.SubPageIdentifier.TRANSFER_CHANGE_STATUS)
                {
                    string status = string.Empty;
                    Adapter.BeginInvoke(new SendOrPostCallback(async (o1) =>
                    {
                        E_TRAN_STATUS ee = ((E_TRAN_STATUS)ComboBox.SelectedItem);
                        status = ee.ToString();
                        await Task.Run(() => mSCCommandStatus?.Invoke(this, new MCSCommandStatusChangeEventArgs(mcs_cmd_id, status)));
                    }), null);
                }
                else if (identity == BCAppConstants.SubPageIdentifier.TRANSFER_ASSIGN_VEHICLE)
                {
                    string vhid = string.Empty;
                    Adapter.BeginInvoke(new SendOrPostCallback(async (o1) =>
                    {
                        if (ComboBox.SelectedItem == null)
                        {
                            TipMessage_Type_Light.Show("", "No vehicle has been selected.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        vhid = ComboBox.SelectedItem.ToString();
                        await Task.Run(() => mSCCommandAssignVehicle?.Invoke(this, new MCSCommandAssignVeicleEventArgs(mcs_cmd_id, vhid)));

                    }), null);
                }
                else if (identity == BCAppConstants.SubPageIdentifier.TRANSFER_SHIFT_COMMAND)
                {
                    string vhid = string.Empty;
                    Adapter.BeginInvoke(new SendOrPostCallback(async (o1) =>
                    {
                        if (ComboBox.SelectedItem == null)
                        {
                            TipMessage_Type_Light.Show("", "No vehicle has been selected.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        vhid = ComboBox.SelectedItem.ToString();
                        await Task.Run(() => mCSCommandShift?.Invoke(this, new MCSCommandShiftEventArgs(mcs_cmd_id, vhid)));
                    }), null);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MCSCommandStatusChange(object sender, MCSCommandStatusChangeEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandChangeStatus(e.mcs_cmd, e.status, out string result))
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    Adapter.Invoke(new SendOrPostCallback((o1) =>
                    {
                        CloseFormEvent?.Invoke(this, e);
                    }), null);
                    TipMessage_Type_Light_woBtn.Show("", "Status Change Succeed", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MCSCommandVehicleAssign(object sender, MCSCommandAssignVeicleEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandAssignVehicle(e.mcs_cmd, e.vh_id, out string result))
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    Adapter.Invoke(new SendOrPostCallback((o1) =>
                    {
                        CloseFormEvent?.Invoke(this, e);
                    }), null);

                    TipMessage_Type_Light_woBtn.Show("", "Assign Vehicle Succeed", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MCSCommandShift(object sender, MCSCommandShiftEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandShift(e.mcs_cmd, e.vh_id, out string result))
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    Adapter.Invoke(new SendOrPostCallback((o1) =>
                    {
                        CloseFormEvent?.Invoke(this, e);
                    }), null);
                    TipMessage_Type_Light_woBtn.Show("", "Shift Command Succeed", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class MCSCommandStatusChangeEventArgs : EventArgs
        {
            public MCSCommandStatusChangeEventArgs(string mcs_cmd, string status)
            {
                this.mcs_cmd = mcs_cmd;
                this.status = status;
            }
            public string mcs_cmd { get; private set; }
            public string status { get; private set; }
        }

        public class MCSCommandAssignVeicleEventArgs : EventArgs
        {
            public MCSCommandAssignVeicleEventArgs(string mcs_cmd, string vh_id)
            {
                this.mcs_cmd = mcs_cmd;
                this.vh_id = vh_id;
            }
            public string mcs_cmd { get; private set; }
            public string vh_id { get; private set; }
        }

        public class MCSCommandShiftEventArgs : EventArgs
        {
            public MCSCommandShiftEventArgs(string mcs_cmd, string vh_id)
            {
                this.mcs_cmd = mcs_cmd;
                this.vh_id = vh_id;
            }
            public string mcs_cmd { get; private set; }
            public string vh_id { get; private set; }
        }

    }
}
