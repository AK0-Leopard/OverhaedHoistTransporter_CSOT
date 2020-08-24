//*********************************************************************************
//      uc_SP_TransferManagement.cs
//*********************************************************************************
// File Name: uc_SP_TransferManagement.cs
// Description: Transfer Management
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
using com.mirle.ibg3k0.ohxc.winform.UI.Menu_Operation.RequestPopForm;
using com.mirle.ibg3k0.ohxc.winform.UI.Menu_System;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_TransferManagement.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_TransferManagement : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public event EventHandler CloseFormEvent;
        ohxc.winform.App.WindownApplication app = null;
        List<VACMD_MCS> cmdList;
        bool updateTransferInfo = false;
        public event EventHandler<MCSCommandAutoAssignUpdateEventArgs> mSCCommandAutoAssignChange;
        public event EventHandler<MCSCommandCancelAbortEventArgs> mSCCommandCancelAbortRequest;
        public event EventHandler<MCSCommandForceFinishEventArgs> mSCCommandForceFinishRequest;
        #endregion 公用參數設定

        public uc_SP_TransferManagement()
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
                registerEvent();
                //Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
                init(app.ObjCacheManager.GetMCS_CMD());

                DispatcherTimer _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(2000);
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (updateTransferInfo)
            {
                updateTransferInfo = false;
                refresh_MCSCMDGrp();
                refresh_CMDCommandCount();
            }
        }
        private void registerEvent()
        {
            try
            {
                mSCCommandCancelAbortRequest += MSCCommandCancelAbort;
                mSCCommandAutoAssignChange += MSCCommandAutoAssignChange;
                mSCCommandForceFinishRequest += MSCCommandForceFinish;
                app.ObjCacheManager.TransferInfoUpdate += ObjCacheManager_TransferInfo_update;
                app.ObjCacheManager.MCSCommandUpdateComplete += ObjCacheManager_MCSCMDUpdateComplete;

                app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_TRANSFER_INFO, app.LineBLL.TransferInfo);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        //{
        //    unregisterEvent();
        //}

        public void unregisterEvent()
        {
            try
            {
                mSCCommandCancelAbortRequest -= MSCCommandCancelAbort;
                mSCCommandAutoAssignChange -= MSCCommandAutoAssignChange;
                mSCCommandForceFinishRequest -= MSCCommandForceFinish;
                app.ObjCacheManager.TransferInfoUpdate -= ObjCacheManager_TransferInfo_update;
                app.ObjCacheManager.MCSCommandUpdateComplete -= ObjCacheManager_MCSCMDUpdateComplete;

                app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_TRANSFER_INFO);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void init(List<VACMD_MCS> cmds)
        {
            ALINE aLINE = app.ObjCacheManager.GetLine();
            try
            {
                cmdList = cmds;
                var all_cmd_view_obj = this.cmdList.Select(cmd => new TarnferCMDViewObj(cmd));
                grid_MCS_Command.ItemsSource = all_cmd_view_obj;
                MCSQueueCount.Text = app.CmdBLL.getCMD_MCSIsQueueCount().ToString();
                TotalCommandCount.Text = app.CmdBLL.getCMD_MCSTotalCount().ToString();
                TogBtn_McsQUpdate.set_ToggleButton(aLINE.MCSCommandAutoAssign);
                btn_AssignVh.IsEnabled = !aLINE.MCSCommandAutoAssign;
                btn_ShiftCmd.IsEnabled = !aLINE.MCSCommandAutoAssign;
                btn_ChangeStatus.IsEnabled = !aLINE.MCSCommandAutoAssign;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_MCSCMDUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                updateTransferInfo = true;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_CMDCommandCount()
        {
            try
            {
                MCSQueueCount.Text = app.CmdBLL.getCMD_MCSIsQueueCount().ToString();
                TotalCommandCount.Text = app.CmdBLL.getCMD_MCSTotalCount().ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_MCSCMDGrp()
        {
            try
            {
                Object current = grid_MCS_Command.SelectedItem;
                List<ListSortDirection?> cols_sortdir = GetColumnInfo(grid_MCS_Command);
                List<SortDescription> sortDescriptionList = GetSortInfo(grid_MCS_Command);
                cmdList = app.ObjCacheManager.GetMCS_CMD();
                var all_cmd_view_obj = this.cmdList.Select(cmd => new TarnferCMDViewObj(cmd));
                grid_MCS_Command.ItemsSource = all_cmd_view_obj;
                grid_MCS_Command.Items.Refresh();
                if (current != null)
                {
                    TarnferCMDViewObj pre = (TarnferCMDViewObj)current;
                    foreach (var item in grid_MCS_Command.Items)
                    {
                        TarnferCMDViewObj now = (TarnferCMDViewObj)item;

                        if (now.CMD_ID.Trim() == pre.CMD_ID.Trim())
                        {
                            grid_MCS_Command.SelectedItem = item;
                            break;
                        }
                    }
                }
                SetColumnInfo(grid_MCS_Command, cols_sortdir);
                SetSortInfo(grid_MCS_Command, sortDescriptionList);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }


        List<ListSortDirection?> GetColumnInfo(System.Windows.Controls.DataGrid dg)
        {
            List<ListSortDirection?> columnInfos = new List<ListSortDirection?>();
            foreach (var column in dg.Columns)
            {
                columnInfos.Add(column.SortDirection);
            }
            return columnInfos;
        }

        List<SortDescription> GetSortInfo(System.Windows.Controls.DataGrid dg)
        {
            List<SortDescription> sortInfos = new List<SortDescription>();
            foreach (var sortDescription in dg.Items.SortDescriptions)
            {
                sortInfos.Add(sortDescription);
            }
            return sortInfos;
        }

        void SetColumnInfo(System.Windows.Controls.DataGrid dg, List<ListSortDirection?> columnInfos)
        {
            //columnInfos.Sort((c1, c2) => { return c1.DisplayIndex - c2.DisplayIndex; });
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                dg.Columns[i].SortDirection = columnInfos[i];
            }
            //foreach (var columnInfo in columnInfos)
            //{
            //    var column = dg.Columns.FirstOrDefault(col => col.Header == columnInfo.Header);
            //    if (column != null)
            //    {
            //        if (columnInfo.SortDirection != null)
            //        {

            //        }
            //        column.SortDirection = columnInfo.SortDirection;
            //        column.DisplayIndex = columnInfo.DisplayIndex;
            //        column.Visibility = columnInfo.Visibility;
            //    }
            //}
        }

        void SetSortInfo(System.Windows.Controls.DataGrid dg, List<SortDescription> sortInfos)
        {
            dg.Items.SortDescriptions.Clear();
            foreach (var sortInfo in sortInfos)
            {
                dg.Items.SortDescriptions.Add(sortInfo);
            }
        }

        private void ObjCacheManager_TransferInfo_update(object sender, EventArgs e)
        {
            ALINE aLINE = app.ObjCacheManager.GetLine();
            try
            {
                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                {
                    TogBtn_McsQUpdate.set_ToggleButton(aLINE.MCSCommandAutoAssign);
                    btn_AssignVh.IsEnabled = !aLINE.MCSCommandAutoAssign;
                    btn_ShiftCmd.IsEnabled = !aLINE.MCSCommandAutoAssign;
                    btn_ChangeStatus.IsEnabled = !aLINE.MCSCommandAutoAssign;
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MSCCommandAutoAssignChange(object sender, MCSCommandAutoAssignUpdateEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandAutoAssignChange(e.autoAssign, out string result))
                {
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {

                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MSCCommandCancelAbort(object sender, MCSCommandCancelAbortEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandCancelAbort(e.mcs_cmd, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    TipMessage_Type_Light_woBtn.Show("", "Cancel Abort Succeed", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MSCCommandForceFinish(object sender, MCSCommandForceFinishEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandForceFinish(e.mcs_cmd, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    TipMessage_Type_Light_woBtn.Show("", "Force Finish Succeed", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
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
                if (sender.Equals(btn_CancelAbort))
                {
                    var confirmResult = TipMessage_Request_Light.Show("Cancel/Abort the command ?");
                    if (confirmResult != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        TarnferCMDViewObj vmcs_cmd = (TarnferCMDViewObj)grid_MCS_Command.SelectedItem;

                        await Task.Run(() => mSCCommandCancelAbortRequest?.Invoke(this, new MCSCommandCancelAbortEventArgs(vmcs_cmd.CMD_ID.Trim())));
                        //TipMessage_Type_Light.Show("", "Successfully command.", BCAppConstants.INFO_MSG);
                    }
                }
                else if (sender.Equals(btn_Finish))
                {
                    var confirmResult = TipMessage_Request_Light.Show("Force finish the command ?");
                    if (confirmResult != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        TarnferCMDViewObj vmcs_cmd = (TarnferCMDViewObj)grid_MCS_Command.SelectedItem;
                        await Task.Run(() => mSCCommandForceFinishRequest?.Invoke(this, new MCSCommandForceFinishEventArgs(vmcs_cmd.CMD_ID.Trim())));
                        //TipMessage_Type_Light.Show("", "Successfully command.", BCAppConstants.INFO_MSG);
                    }
                }
                else if (sender.Equals(btn_AssignVh))
                {
                    if (grid_MCS_Command.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("", "There is no Transfer Command has been selected.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    if (((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).TRANSFERSTATE != E_TRAN_STATUS.Queue)
                    {
                        TipMessage_Type_Light.Show("", "Assign vehicle only for the command which transfer status is queue.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    TarnferCMDViewObj mcs_cmd = (TarnferCMDViewObj)grid_MCS_Command.SelectedItem;
                    AssignVehiclePopupForm assignVehicle = new AssignVehiclePopupForm();
                    assignVehicle.readCmdID(mcs_cmd);
                    assignVehicle.ShowDialog();
                }
                else if (sender.Equals(btn_ShiftCmd))
                {
                    if (grid_MCS_Command.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("", "There is no Transfer Command has been selected.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    if (((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).TRANSFERSTATE != E_TRAN_STATUS.Initial)
                    {
                        TipMessage_Type_Light.Show("", "Shift command only for the command which transfer status is initial.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).VH_ID))
                    {
                        TipMessage_Type_Light.Show("", "Shift command only for the command which already assign vehicle.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    TarnferCMDViewObj mcs_cmd = (TarnferCMDViewObj)grid_MCS_Command.SelectedItem;
                    ShiftCommandPopupForm shiftCommand = new ShiftCommandPopupForm();
                    shiftCommand.readCmdID(mcs_cmd);
                    shiftCommand.ShowDialog();
                }
                else if (sender.Equals(btn_ChangeStatus))
                {
                    if (grid_MCS_Command.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("", "There is no Transfer Command has been selected.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    TarnferCMDViewObj mcs_cmd = (TarnferCMDViewObj)grid_MCS_Command.SelectedItem;
                    ChangeStatusPopupForm changeStatus = new ChangeStatusPopupForm();
                    changeStatus.readCmdID(mcs_cmd);
                    changeStatus.ShowDialog();
                }
                else if (sender.Equals(btn_ChangePriorty))
                {
                    if (grid_MCS_Command.SelectedItem == null)
                    {
                        TipMessage_Type_Light.Show("", "There is no Transfer Command has been selected.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    if (((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).TRANSFERSTATE != E_TRAN_STATUS.Queue)
                    {
                        TipMessage_Type_Light.Show("", "Priority Change only for the command which transfer status is queue.", BCAppConstants.WARN_MSG);
                        return;
                    }
                    //ChangePriorityPopupForm changePriority = new ChangePriorityPopupForm(((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).CMD_ID.Trim());
                    //changePriority.ShowDialog();

                    string mcs_cmd = ((TarnferCMDViewObj)grid_MCS_Command.SelectedItem).CMD_ID.Trim();
                    ChangePriorityPopupForm changePriority = new ChangePriorityPopupForm();
                    changePriority.readCmdID(mcs_cmd);
                    changePriority.ShowDialog();
                }
                else if (sender.Equals(btn_Export))
                {
                    if (CsvUtility.exportLotDataToCSV(app.ObjCacheManager.GetMCS_CMD()))
                    {
                        TipMessage_Type_Light_woBtn.Show("", "Export data completed.", BCAppConstants.INFO_MSG);
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("", "Export data failed.", BCAppConstants.WARN_MSG);
                    }
                }
                else if (sender.Equals(btn_Close))
                {
                    if (TogBtn_McsQUpdate.Toggled1 == false)
                        await Task.Run(() => mSCCommandAutoAssignChange?.Invoke(this, new MCSCommandAutoAssignUpdateEventArgs(true.ToString())));
                    CloseFormEvent?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            { logger.Error(ex, "Exception"); }
        }

        private async void TogBtn_McsQUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TogBtn_McsQUpdate.Toggled1 == true)
                {
                    btn_AssignVh.IsEnabled = false;
                    btn_ShiftCmd.IsEnabled = false;
                    btn_ChangeStatus.IsEnabled = false;
                    await Task.Run(() => mSCCommandAutoAssignChange?.Invoke(this, new MCSCommandAutoAssignUpdateEventArgs(true.ToString())));
                }
                else
                {
                    btn_AssignVh.IsEnabled = true;
                    btn_ShiftCmd.IsEnabled = true;
                    btn_ChangeStatus.IsEnabled = true;

                    await Task.Run(() => mSCCommandAutoAssignChange?.Invoke(this, new MCSCommandAutoAssignUpdateEventArgs(false.ToString())));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }

        public class MCSCommandAutoAssignUpdateEventArgs : EventArgs
        {
            public MCSCommandAutoAssignUpdateEventArgs(string autoAssign)
            {
                try
                {
                    this.autoAssign = autoAssign;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
            }
            public string autoAssign { get; private set; }
        }

        public class MCSCommandCancelAbortEventArgs : EventArgs
        {
            public MCSCommandCancelAbortEventArgs(string mcs_cmd)
            {
                this.mcs_cmd = mcs_cmd;
            }

            public string mcs_cmd
            {
                get;
                private set;
            }
        }

        public class MCSCommandForceFinishEventArgs : EventArgs
        {
            public MCSCommandForceFinishEventArgs(string mcs_cmd)
            {
                this.mcs_cmd = mcs_cmd;
            }
            public string mcs_cmd
            {
                get;
                private set;
            }
        }

    }
}
