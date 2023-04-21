﻿using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ObjectRelay;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using NLog;
using com.mirle.ibg3k0.bc.winform.App;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class TransferCommandQureyListForm : Form
    {
        BCMainForm mainform;
        BindingSource cmsMCS_bindingSource = new BindingSource();
        List<CMD_MCSObjToShow> cmdMCSList = null;
        int selection_index = -1;
        public TransferCommandQureyListForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            dgv_TransferCommand.AutoGenerateColumns = false;
            mainform = _mainForm;

            dgv_TransferCommand.DataSource = cmsMCS_bindingSource;
        }

        private async void updateTransferCommandAsync()
        {
            try
            {
                List<ACMD_MCS> ACMD_MCSs = null;
                await Task.Run(() =>
                {
                    ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished();
                    System.Threading.Thread.Sleep(5000);
                });
                cmdMCSList = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdMCSList;
                dgv_TransferCommand.Refresh();
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                Data: $"Update Transfer Command Failed, Exception:{ex.Message}");
            }

        }

        private async void btn_refresh_Click(object sender, EventArgs e)
        {
            selection_index = -1;
            try
            {
                btn_refresh.Enabled = false;
                //updateTransferCommandAsync();
                List<ACMD_MCS> ACMD_MCSs = null;
                await Task.Run(() =>
                {
                    ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished();
                });
                cmdMCSList = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdMCSList;
                dgv_TransferCommand.Refresh();
                lbl_currentCommandCountValue.Text = ACMD_MCSs.Count().ToString();

            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                Data: $"btn_refresh_Click Failed, Exception:{ex.Message}");
            }
            finally
            {
                btn_refresh.Enabled = true;
            }
        }


        private void dgv_TransferCommand_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_TransferCommand.SelectedRows.Count > 0)
                selection_index = dgv_TransferCommand.SelectedRows[0].Index;
        }

        private async void btn_force_finish_Click(object sender, EventArgs e)
        {
            try
            {
                if (selection_index == -1) return;
                btn_force_finish.Enabled = false;
                var mcs_cmd = cmdMCSList[selection_index];

                //AVEHICLE excute_cmd_of_vh = mainform.BCApp.SCApplication.VehicleBLL.cache.getVehicleByMCSCmdID(mcs_cmd.CMD_ID);
                AVEHICLE excute_cmd_of_vh = null;

                await Task.Run(() =>
                {
                    try
                    {
                        ACMD_OHTC cmd_ohtc = mainform.BCApp.SCApplication.CMDBLL.getExcuteCMD_OHTCAndNoInterruptedByMCSID(mcs_cmd.CMD_ID);
                        excute_cmd_of_vh = mainform.BCApp.SCApplication.VehicleBLL.cache.getVhByID(cmd_ohtc.VH_ID);

                        if (cmd_ohtc != null)
                        {
                            mainform.BCApp.SCApplication.VehicleBLL.doTransferCommandFinish(cmd_ohtc.VH_ID, cmd_ohtc.CMD_ID, CompleteStatus.CmpStatusForceFinishByOp);
                            mainform.BCApp.SCApplication.VIDBLL.initialVIDCommandInfo(cmd_ohtc.VH_ID);
                        }
                        mainform.BCApp.SCApplication.CMDBLL.updateCMD_MCS_TranStatus2Complete(mcs_cmd.CMD_ID, E_TRAN_STATUS.Aborted);
                        mainform.BCApp.SCApplication.ReportBLL.newReportTransferCommandNormalFinish(mcs_cmd.cmd_mcs, excute_cmd_of_vh, sc.Data.SECS.CSOT.SECSConst.CMD_Result_Unsuccessful, null);
                        mainform.BCApp.SCApplication.SysExcuteQualityBLL.doCommandFinish(mcs_cmd.CMD_ID, CompleteStatus.CmpStatusForceFinishByOp, E_CMD_STATUS.AbnormalEndByOHTC);
                    }
                    catch { }
                }
                );
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                  Data: $"Fource mcs command finish success, vh id:{SCUtility.Trim(excute_cmd_of_vh?.VEHICLE_ID, true)}, cst id:{SCUtility.Trim(excute_cmd_of_vh?.CST_ID, true)}, " +
                        $"mcs command id:{SCUtility.Trim(mcs_cmd.CMD_ID, true)},source:{SCUtility.Trim(mcs_cmd.HOSTSOURCE, true)},dest:{SCUtility.Trim(mcs_cmd.HOSTDESTINATION, true)}");
                //updateTransferCommandAsync();

                List<ACMD_MCS> ACMD_MCSs = null;
                await Task.Run(() =>
                {
                    ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished();
                });
                cmdMCSList = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdMCSList;
                dgv_TransferCommand.Refresh();
            }
            catch { }
            finally
            {
                btn_force_finish.Enabled = true;
            }
        }

        private void TransferCommandQureyListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainform.removeForm(this.Name);
        }

        private async void btn_returnToQueue_Click(object sender, EventArgs e)
        {
            try
            {
                if (selection_index == -1) return;
                btn_returnToQueue.Enabled = false;
                var mcs_cmd = cmdMCSList[selection_index];
                string mcs_cmd_id = mcs_cmd.CMD_ID;
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                  Data: $" Start fource change mcs command to queue, mcs cmd id:{SCUtility.Trim(mcs_cmd_id, true)}");
                await Task.Run(() =>
                {
                    try
                    {
                        var new_mcs_cmd = mainform.BCApp.SCApplication.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                        if (new_mcs_cmd == null)
                        {
                            return;
                        }
                        if (new_mcs_cmd.TRANSFERSTATE != E_TRAN_STATUS.PreInitial)
                        {

                            return;
                        }
                        mainform.BCApp.SCApplication.CMDBLL.updateCMD_MCS_TranStatus2Queue(mcs_cmd_id);
                        Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                          Data: $" Success fource change mcs command to queue, mcs cmd id:{SCUtility.Trim(mcs_cmd_id, true)}");
                    }
                    catch { }
                }
                );


                List<ACMD_MCS> ACMD_MCSs = null;
                await Task.Run(() =>
                {
                    ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished();
                });

                cmdMCSList = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdMCSList;
                dgv_TransferCommand.Refresh();
            }
            catch { }
            finally
            {
                btn_force_finish.Enabled = true;
            }
        }

        private async void btn_priorityUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selection_index == -1) return;
                btn_priorityUpdate.Enabled = false;
                var mcs_cmd = cmdMCSList[selection_index];
                string mcs_cmd_id = mcs_cmd.CMD_ID;

                string msg = $"Do you want to update the priority of command :{mcs_cmd_id} to top?";
                DialogResult confirmResult = MessageBox.Show(this, msg,
                    BCApplication.getMessageString("CONFIRM"), MessageBoxButtons.YesNo);
                mainform.BCApp.SCApplication.BCSystemBLL.
                    addOperationHis(mainform.BCApp.LoginUserID, this.Name, $"{msg},confirm result:{confirmResult}");
                if (confirmResult != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                  Data: $" Start update to top priority, mcs cmd id:{SCUtility.Trim(mcs_cmd_id, true)}");
                (bool is_success, string reason) update_result = default((bool is_success, string reason));
                await Task.Run(() =>
                {
                    try
                    {
                        update_result = mainform.BCApp.SCApplication.CMDBLL.tryUpdateCMD_MCS_PrioritySUMBoostToTopPriority(mcs_cmd_id);
                        Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(TransferCommandQureyListForm), Device: "OHTC",
                          Data: $"update to top priority result:{update_result.is_success}({update_result.reason})");
                    }
                    catch { }
                }
                );

                bcf.App.BCFApplication.onInfoMsg($"MCS command:{mcs_cmd_id},update to top priority result:{update_result.is_success}({update_result.reason})");


                List<ACMD_MCS> ACMD_MCSs = null;
                await Task.Run(() =>
                {
                    ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished();
                });

                cmdMCSList = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdMCSList;
                dgv_TransferCommand.Refresh();
            }
            catch { }
            finally
            {
                btn_priorityUpdate.Enabled = true;
            }
        }

        private void btn_restoreToOriginalPriority_Click(object sender, EventArgs e)
        {

        }
    }
}
