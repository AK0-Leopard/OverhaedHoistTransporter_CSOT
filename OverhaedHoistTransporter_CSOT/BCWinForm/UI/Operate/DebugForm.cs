﻿using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.VO;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static com.mirle.ibg3k0.sc.App.SCAppConstants;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class DebugForm : Form
    {

        BCMainForm mainForm;
        BCApplication bcApp;
        List<RadioButton> radioButtons = new List<RadioButton>();
        List<BLOCKZONEQUEUE> blocked_queues = null;
        AEQPT MTSMTL = null;
        AECDATAMAP maxAllowActionTimeECData = null;
        public DebugForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
            bcApp = mainForm.BCApp;

            cb_StartGenAntoCmd.Checked = DebugParameter.CanAutoRandomGeneratesCommand;
            cb_FroceBlockPass.Checked = DebugParameter.isForcedPassBlockControl;
            cb_FroceBlockPass.Checked = DebugParameter.isForcedRejectBlockControl;
            ck_is_open_command_shift.Checked = DebugParameter.isOpenCommandShift;

            ch_force_pass_red_light_with_buzzer.Checked = DebugParameter.isForcePassFourColorLightRedWithBuzzerSignal;
            num_ChangePathCommandPath.Value = sc.App.SystemParameter.ChangePathCommandCount;
            List<string> lstVh = new List<string>();
            lstVh.Add(string.Empty);
            lstVh.AddRange(bcApp.SCApplication.getEQObjCacheManager().getAllVehicle().Select(vh => vh.VEHICLE_ID).ToList());
            string[] allVh = lstVh.ToArray();
            BCUtility.setComboboxDataSource(cmb_tcpipctr_Vehicle, allVh);
            BCUtility.setComboboxDataSource(cmb_plcctr_Vehicle, allVh.ToArray());
            BCUtility.setComboboxDataSource(cmb_car_out_vh, allVh.ToArray());

            List<ASEGMENT> segments = bcApp.SCApplication.SegmentBLL.cache.GetSegments();
            string[] segment_ids = segments.Select(seg => seg.SEG_NUM).ToArray();
            BCUtility.setComboboxDataSource(cmb_refresh_vh_order_in_seg_id, segment_ids.ToArray());


            List<AADDRESS> allAddress_obj = bcApp.SCApplication.MapBLL.loadAllAddress();
            string[] allAdr_ID = allAddress_obj.Select(adr => adr.ADR_ID).ToArray();
            BCUtility.setComboboxDataSource(cmb_teach_from_adr, allAdr_ID);
            BCUtility.setComboboxDataSource(cmb_teach_to_adr, allAdr_ID.ToArray());

            List<AEQPT> maintainDevices = bcApp.SCApplication.EquipmentBLL.cache.loadMaintainDevice();
            string[] maintain_devices_id = maintainDevices.Select(eq => eq.EQPT_ID).ToArray();
            BCUtility.setComboboxDataSource(cmb_maintain_device, maintain_devices_id.ToArray());

            List<AEQPT> ohcvDevices = bcApp.SCApplication.EquipmentBLL.cache.loadOHCVDevices();
            string[] ohcv_devices_id = ohcvDevices.Select(eq => eq.EQPT_ID).ToArray();
            BCUtility.setComboboxDataSource(cb_cv_ids, ohcv_devices_id.ToArray());



            cb_OperMode.DataSource = Enum.GetValues(typeof(sc.ProtocolFormat.OHTMessage.OperatingVHMode));
            cb_PwrMode.DataSource = Enum.GetValues(typeof(sc.ProtocolFormat.OHTMessage.OperatingPowerMode));
            cmb_pauseEvent.DataSource = Enum.GetValues(typeof(sc.ProtocolFormat.OHTMessage.PauseEvent));
            cmb_pauseType.DataSource = Enum.GetValues(typeof(OHxCPauseType));
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdCancel);
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdAbort);
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdCancelIdMismatch);
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdCancelIdReadFailed);
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdCancelIdReadDuplicate);
            c.Items.Add(sc.ProtocolFormat.OHTMessage.CMDCancelType.CmdCancelIdReadForceFinish);
            //c.DataSource = Enum.GetValues(typeof(sc.ProtocolFormat.OHTMessage.CMDCancelType));

            radioButtons.Add(radio_bit0);
            radioButtons.Add(radio_bit1);
            radioButtons.Add(radio_bit2);
            radioButtons.Add(radio_bit3);
            radioButtons.Add(radio_bit4);
            radioButtons.Add(radio_bit5);
            radioButtons.Add(radio_bit6);
            radioButtons.Add(radio_bit7);
            radioButtons.Add(radio_bit8);
            radioButtons.Add(radio_bit9);
            radioButtons.Add(radio_bita);
            radioButtons.Add(radio_bitb);
            radioButtons.Add(radio_bitc);
            radioButtons.Add(radio_bitd);
            radioButtons.Add(radio_bite);
            radioButtons.Add(radio_bitf);



            cb_Cache_data_Name.Items.Add("");
            cb_Cache_data_Name.Items.Add("APORTSTATION");
            dgv_cache_object_data.AutoGenerateColumns = false;
            comboBox_HID_control.SelectedIndex = 0;
            maxAllowActionTimeECData = bcApp.SCApplication.LineBLL.getECData(SCAppConstants.ECID_MAX_ALLOW_ACTION_TIME_SECOND);
            if (maxAllowActionTimeECData != null)
            {
                MaxAllowActionTimeSecond_Max_txb.Text = maxAllowActionTimeECData.ECMAX;
                MaxAllowActionTimeSecond_Min_txb.Text = maxAllowActionTimeECData.ECMIN;
                MaxAllowActionTimeSecond_Current_txb.Text = maxAllowActionTimeECData.ECV;
            }
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            DebugParameter.IsDebugMode = true;
        }

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TrunOffAllVhPLCControl();
            DebugParameter.IsDebugMode = false;
            mainForm.removeForm(typeof(DebugForm).Name);
        }



        private void cb_FroceBlockPass_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.isForcedPassBlockControl = cb_FroceBlockPass.Checked;
        }


        AVEHICLE noticeCar = null;
        string vh_id = null;
        private void cmb_Vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            vh_id = cmb_tcpipctr_Vehicle.Text.Trim();

            noticeCar = bcApp.SCApplication.getEQObjCacheManager().getVehicletByVHID(vh_id);
            lbl_id_37_cmdID_value.Text = noticeCar?.OHTC_CMD;
            lbl_install_status.Text = noticeCar?.IS_INSTALLED.ToString();
            lbl_listening_status.Text = noticeCar?.IsTcpIpListening(bcApp.SCApplication.getBCFApplication()).ToString();
        }

        private void uctl_Btn1_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.HostBasicVersionReport);
            //asyExecuteAction(noticeCar.sned_S1);
        }
        private void uctl_SendFun11_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.BasicInfoReport);
            //asyExecuteAction(noticeCar.sned_S11);
        }
        private void uctl_SendFun13_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.TavellingDataReport);
            //asyExecuteAction(noticeCar.sned_S13);
        }
        private void uctl_SendFun15_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.SectionDataReport);
            //asyExecuteAction(noticeCar.sned_S15);
        }
        private void uctl_SendFun17_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.AddressDataReport);
            //asyExecuteAction(noticeCar.sned_S17);
        }

        private void uctl_SendFun19_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.ScaleDataReport);
            //asyExecuteAction(noticeCar.sned_S19);
        }

        private void uctl_SendFun21_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.ControlDataReport);
            //asyExecuteAction(noticeCar.sned_S21);
        }

        private void uctl_SendFun23_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.GuideDataReport);
            //asyExecuteAction(noticeCar.sned_S23);
        }

        private void asyExecuteAction(Func<string, bool> act)
        {
            Task.Run(() =>
            {
                act(vh_id);
            });
        }

        private void uctl_SendAllFun_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.doDataSysc);
            //asyExecuteAction(noticeCar.sned_ALL);
        }

        private void uctl_Send_Fun_71_Click(object sender, EventArgs e)
        {
            string from_adr = cmb_teach_from_adr.Text;
            string to_adr = cmb_teach_to_adr.Text;
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.TeachingRequest(vh_id, from_adr, to_adr);
                //noticeCar.send_Str71(from_adr, to_adr);
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.IndividualUploadRequest);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.IndividualChangeRequest);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sc.ProtocolFormat.OHTMessage.OperatingVHMode OperatingMode;
            Enum.TryParse(cb_OperMode.SelectedValue.ToString(), out OperatingMode);
            Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(DebugForm), Device: "OHTC",
              Data: $"Send mode change command(ID:41) to vh:{vh_id}, OperatingMode:{OperatingMode}");
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.ModeChangeRequest(vh_id, OperatingMode);
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sc.ProtocolFormat.OHTMessage.OperatingPowerMode operatiogPowerMode;
            Enum.TryParse(cb_PwrMode.SelectedValue.ToString(), out operatiogPowerMode);

            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.PowerOperatorRequest(vh_id, operatiogPowerMode);
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            asyExecuteAction(bcApp.SCApplication.VehicleService.AlarmResetRequest);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(DebugForm), Device: "OHTC",
              Data: $"Excute force command finish for vh:{vh_id}");
            try
            {
                button6.Enabled = false;
                await Task.Run(() =>
                {
                    bcApp.SCApplication.CMDBLL.forceUpdataCmdStatus2FnishByVhID(vh_id);
                });
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"force finish command Failed, Exception:{ex.Message}");
            }
            finally
            {
                button6.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                button7.Enabled = false;
                Task.Run(() =>
                {
                    //bcApp.SCApplication.VehicleService.forceResetVHStatus(vh_id);
                    bcApp.SCApplication.VehicleService.VehicleStatusRequest(vh_id, true);
                });
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"refresh vh:{vh_id} status failed, Exception:{ex.Message}");
            }
            finally
            {
                button7.Enabled = true;
            }
        }

        private void cb_StartGenAntoCmd_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            DebugParameter.CanAutoRandomGeneratesCommand = cb.Checked;

            if (!cb.Checked)
            {
                //Task.Run(() =>
                //{
                //    var mcs_cmds = bcApp.SCApplication.CMDBLL.loadMCS_Command_Queue();
                //    foreach (var cmd in mcs_cmds)
                //    {
                //        bcApp.SCApplication.CMDBLL.updateCMD_MCS_TranStatus2Complete(cmd.CMD_ID, E_TRAN_STATUS.Canceled);
                //        bcApp.SCApplication.SysExcuteQualityBLL.doCommandFinish(cmd.CMD_ID, sc.ProtocolFormat.OHTMessage.CompleteStatus.CmpStatusForceFinishByOp, E_CMD_STATUS.CancelEndByOHTC);
                //    }
                //});
            }
        }

        private void btn_forceReleaseALLBlock_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.forceReleaseBlockControl();
            });
        }

        private void btn_ForceReleaseBlock_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.forceReleaseBlockControl(vh_id);
            });
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            sc.ProtocolFormat.OHTMessage.PauseEvent pauseEvent;
            OHxCPauseType pauseType;
            Enum.TryParse(cmb_pauseEvent.SelectedValue.ToString(), out pauseEvent);
            Enum.TryParse(cmb_pauseType.SelectedValue.ToString(), out pauseType);

            Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(DebugForm), Device: "OHTC",
              Data: $"Send pause command(ID:39) to vh:{vh_id}, pause event:{pauseEvent}, pause type:{pauseType}");
            try
            {
                btn_pause.Enabled = false;
                Task.Run(() =>
                {
                    bcApp.SCApplication.VehicleService.PauseRequest(vh_id, pauseEvent, pauseType);
                });
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"pause vh:{vh_id} failed, Exception:{ex.Message}");
            }
            finally
            {
                btn_pause.Enabled = true;
            }
        }




        private void label17_Click(object sender, EventArgs e)
        {

        }



        AVEHICLE plcctrAVEHICLE = null;
        string event_id = "DebugFrom";
        private void cmb_plcctr_Vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (plcctrAVEHICLE != null)
            {
                plcctrAVEHICLE.removeEventHandler(event_id);
                plcctrAVEHICLE = null;
            }
            if (string.IsNullOrWhiteSpace(cmb_plcctr_Vehicle.Text))
            {
                ck_PLC_control_flag.Checked = false;
                ck_PLC_control_flag.Enabled = false;
                return;
            }
            else
            {
                ck_PLC_control_flag.Enabled = true;
            }
            plcctrAVEHICLE = bcApp.SCApplication.VehicleBLL.getVehicleByID(cmb_plcctr_Vehicle.Text);
            plcctrAVEHICLE.addEventHandler(event_id
                                , BCFUtility.getPropertyName(() => plcctrAVEHICLE.Status_Info_PLC)
                                , (s1, e1) => { updateVehicleStatusInfo(); });
            updateVehicleControlItem();
            updateVehicleStatusInfo();
        }

        private void updateVehicleControlItem()
        {
            if (plcctrAVEHICLE == null) return;
            ck_PLC_control_flag.Checked = plcctrAVEHICLE.isPLCInControl;
            if (plcctrAVEHICLE.VehicleControlItemForPLC == null) return;
            for (int i = 0; i < radioButtons.Count(); i++)
            {
                radioButtons[i].Checked = plcctrAVEHICLE.VehicleControlItemForPLC[i];
            }
        }

        private void ck_PLC_control_flag_CheckedChanged(object sender, EventArgs e)
        {
            if (plcctrAVEHICLE == null) return;
            bool isControl = (sender as CheckBox).Checked;
            tlp_PLCControl.Enabled = isControl;
            if (isControl)
            {
                bcApp.SCApplication.VehicleService.PLC_Control_TrunOn(plcctrAVEHICLE.VEHICLE_ID);
            }
            else
            {
                foreach (var radio in radioButtons)
                    radio.Checked = false;
                bcApp.SCApplication.VehicleService.PLC_Control_TrunOff(plcctrAVEHICLE.VEHICLE_ID);

            }
        }

        private void radio_bitX_Click(object sender, EventArgs e)
        {
            (sender as RadioButton).Checked = !(sender as RadioButton).Checked;
        }

        private void radio_bitX_CheckedChanged(object sender, EventArgs e)
        {
            Boolean[] bools = radioButtons.Select(radio => radio.Checked).ToArray();
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.SetVehicleControlItemForPLC(plcctrAVEHICLE.VEHICLE_ID, bools);
            });
        }

        private void updateVehicleStatusInfo()
        {
            if (plcctrAVEHICLE == null | plcctrAVEHICLE.Status_Info_PLC == null) return;
            Adapter.Invoke((obj) =>
            {
                VH_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.vh_id.ToString();
                CUR_SEC_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.cur_sec_id.ToString();
                CUR_SEC_DIST_Value.Text = plcctrAVEHICLE.Status_Info_PLC.CUR_SEC_DIST.ToString();
                CUR_ADR_DIST_Value.Text = plcctrAVEHICLE.Status_Info_PLC.cur_adr_id.ToString();
                TRAN_CMD_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.tran_cmd_id.ToString();
                ACTION_STATUS_Value.Text = plcctrAVEHICLE.Status_Info_PLC.ACTION_STATUS.ToString();
                HAS_CST_Value.Text = plcctrAVEHICLE.Status_Info_PLC.HAS_CST.ToString();
                CST_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.CST_ID?.ToString();
                OBS_PAUSE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.OBS_PAUSE.ToString();
                BLOCK_PAUSE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.BLOCK_PAUSE.ToString();
                NORMAL_PAUSE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.NORMAL_PAUSE.ToString();
                HID_PAUSE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.HID_PAUSE.ToString();
                ERROR_PAUSE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.ERROR_PAUSE.ToString();
                CUR_BLOCK_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.cur_block_id.ToString();
                CUR_HID_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.cur_hid_id.ToString();
                VH_MODE_STATUS_Value.Text = plcctrAVEHICLE.Status_Info_PLC.VH_MODE_STATUS.ToString();
                VH_SPEED_MIN_Value.Text = plcctrAVEHICLE.Status_Info_PLC.VH_SPEED_MIN.ToString();
                VH_ENCODER_VALUE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.VH_ENCODER_VALUE.ToString();
                VH_MAG_VALUE_Value.Text = plcctrAVEHICLE.Status_Info_PLC.VH_MAG_VALUE.ToString();
                SPEED_LIMIT_Value.Text = plcctrAVEHICLE.Status_Info_PLC.SPEED_LIMIT.ToString();
                LEFT_GUIDE_STATUS_Value.Text = plcctrAVEHICLE.Status_Info_PLC.LEFT_GUIDE_STATUS.ToString();
                RIGHT_GUIDE_STATUS_Value.Text = plcctrAVEHICLE.Status_Info_PLC.RIGHT_GUIDE_STATUS.ToString();
                SEC_DIST_DEV_Value.Text = plcctrAVEHICLE.Status_Info_PLC.SEC_DIST_DEV.ToString();
                DEV_SEC_ID_Value.Text = plcctrAVEHICLE.Status_Info_PLC.dev_sec_id.ToString();
                Power_Mode_Value.Text = plcctrAVEHICLE.Status_Info_PLC.POWER_STATUS.ToString();
                ACC_SEC_DIST_Value.Text = plcctrAVEHICLE.Status_Info_PLC.ACC_SEC_DIST.ToString();
                plc_time_Value.Text = plcctrAVEHICLE.Status_Info_PLC.PLC_Datetime.ToString(SCAppConstants.DateTimeFormat_23);

            }, null);

        }

        private void TrunOffAllVhPLCControl()
        {
            var vhs = bcApp.SCApplication.getEQObjCacheManager().getAllVehicle();

            foreach (var vh in vhs)
            {
                vh.PLC_Control_TrunOff();
            }

        }

        private void cb_FroceBlockReject_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.isForcedRejectBlockControl = cb_FroceBlockReject.Checked;
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            try
            {
                button9.Enabled = false;
                sc.ProtocolFormat.OHTMessage.CMDCancelType type;
                Enum.TryParse(c.SelectedItem.ToString(), out type);

                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(DebugForm), Device: "OHTC",
                  Data: $"Send cancel command(ID:37) to vh:{vh_id},command id:{BCFUtility.Trim(noticeCar.OHTC_CMD)} cacnel type:{type}");
                await Task.Run(() =>
                {
                    noticeCar.sned_Str37(noticeCar.OHTC_CMD, type);
                });
            }
            finally
            {
                button9.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Task.Run(() =>
            //{
            //    var mapAction = bcApp.SCApplication.getEQObjCacheManager().getLine().getMapActionByIdentityKey(nameof(sc.Data.ValueDefMapAction.MCSDefaultMapAction)) as sc.Data.ValueDefMapAction.MCSDefaultMapAction;
            //    mapAction.s2f35Test();
            //});
        }

        private void btn_blocked_sec_refresh_Click(object sender, EventArgs e)
        {
            cb_block_section.Text = "";
            blocked_queues = bcApp.SCApplication.MapBLL.loadAllUsingBlockQueue();

            cb_block_section.DataSource = blocked_queues;
            cb_block_section.DisplayMember = "DisplayMember";
        }

        private void btn_release_block_Click(object sender, EventArgs e)
        {
            int index = cb_block_section.SelectedIndex;
            if (blocked_queues == null || blocked_queues.Count == 0) return;
            BLOCKZONEQUEUE queue = blocked_queues[index];
            if (queue != null)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(DebugForm), Device: "OHTC",
                  Data: $"Recheck block control to release:{queue.ToString()}");
            }
            Task.Run(() => bcApp.SCApplication.VehicleService.reCheckBlockControl(queue));
        }

        private void cb_block_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cb_block_section.SelectedIndex;
            lbl_BlockedVh.Text = blocked_queues[index].CAR_ID;
        }

        private void btn_portInServeice_Click(object sender, EventArgs e)
        {
            string port_id = cb_PortID.Text;
            //Task.Run(() =>
            //{
            //    bcApp.SCApplication.VehicleService.doPortServeiceChange(port_id, E_PORT_STATUS.InService);
            //});
        }

        private void btn_portOutOfServeice_Click(object sender, EventArgs e)
        {
            string port_id = cb_PortID.Text;
            //Task.Run(() =>
            //{
            //    bcApp.SCApplication.VehicleService.doPortServeiceChange(port_id, E_PORT_STATUS.OutOfService);
            //});

        }

        private void ck_test_carrierinterface_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.isTestCarrierInterfaceError = ck_test_carrierinterface_error.Checked;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            //Task.Run(() =>
            //{
            //    var mapAction = bcApp.SCApplication.getEQObjCacheManager().getLine().getMapActionByIdentityKey(nameof(sc.Data.ValueDefMapAction.MCSDefaultMapAction)) as sc.Data.ValueDefMapAction.MCSDefaultMapAction;
            //    mapAction.removeCmdTest(txt_remove_cst_id.Text, txt_remove_loc_id.Text);
            //});
            // bcApp.SCApplication.FlexsimCommandDao.setCommandToFlexsimDB("OHT01", "30103","1", "20311","1","CST01","0");
        }



        private async void ck_autoTech_Click(object sender, EventArgs e)
        {
            sc.App.SystemParameter.AutoTeching = ck_autoTech.Checked;
            if (!ck_autoTech.Checked) return;
            string vh_id = cmb_tcpipctr_Vehicle.Text;
            await Task.Run(() =>
             {
                 bcApp.SCApplication.VehicleService.AutoTeaching(vh_id);
                 SpinWait.SpinUntil(() => !sc.App.SystemParameter.AutoTeching);
             });
            ck_autoTech.Checked = sc.App.SystemParameter.AutoTeching;

        }

        private void btn_reset_teach_result_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                List<ASECTION> sections = bcApp.SCApplication.CatchDataFromDB_Section;
                foreach (var sec in sections)
                {
                    if (bcApp.SCApplication.MapBLL.resetSecTechingTime(sec.SEC_ID))
                    {
                        sec.LAST_TECH_TIME = null;
                    }

                }
            });
        }

        private void btn_cmd_override_test_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_tcpipctr_Vehicle.Text;
            bool is_need_pause_first = cb_pauseFirst.Checked;
            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.VhicleChangeThePath(vh_id, is_need_pause_first);
            });
        }

        private void uctl_SendFun2_Click(object sender, EventArgs e)
        {
            //asyExecuteAction(bcApp.SCApplication.VehicleService.BasicInfoVersionReport);
            asyExecuteAction(bcApp.SCApplication.VehicleService.HostBasicVersionReport);


        }

        private void cb_Cache_data_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected_name = (sender as ComboBox).SelectedItem as string;
            if (selected_name == "APORTSTATION")
            {
                var aportation = bcApp.SCApplication.getEQObjCacheManager().getALLPortStation();
                dgv_cache_object_data.DataSource = aportation;
            }
        }

        private void dgv_cache_object_data_EditModeChanged(object sender, EventArgs e)
        {

        }


        #region MTL Test


        private void btn_mtl_dateTimeSync_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var mtl_mapaction = maintainEQ.
                    getMapActionByIdentityKey(nameof(com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)) as
                    com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase;
                mtl_mapaction.DateTimeSyncCommand(DateTime.Now);
            }
            );
        }
        #endregion MTL Test

        private void btn_mtl_message_download_Click(object sender, EventArgs e)
        {
            string msg = txt_mtlMessage.Text;
            Task.Run(() =>
            {
                var mtl_mapaction = maintainEQ.
                    getMapActionByIdentityKey(nameof(com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)) as
                    com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase;
                mtl_mapaction.OHxCMessageDownload(msg);
            }
            );
        }

        private void btn_mtl_vh_realtime_info_Click(object sender, EventArgs e)
        {
            UInt16 car_id = UInt16.Parse(txt_mtl_car_id.Text);
            UInt16 action_mode = UInt16.Parse(txt_mtl_action_mode.Text);
            UInt16 cst_exist = UInt16.Parse(txt_mtl_cst_exist.Text);
            UInt16 current_section_id = UInt16.Parse(txt_mtl_current_sec_id.Text);
            UInt32 current_address_id = UInt32.Parse(txt_mtl_current_adr_id.Text);
            UInt32 buffer_distance = UInt32.Parse(txt_mtl_buffer_distance.Text);
            UInt16 speed = UInt16.Parse(txt_mtl_speed.Text);
            Task.Run(() =>
            {
                var mtl_mapaction = maintainEQ.
                    getMapActionByIdentityKey(nameof(com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)) as
                    com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase;
                mtl_mapaction.CarRealtimeInfo(car_id, action_mode, cst_exist, current_section_id, current_address_id, buffer_distance, speed);
            }
            );
        }

        private void btn_mtl_car_out_notify_Click(object sender, EventArgs e)
        {
            //UInt16 car_id = UInt16.Parse(txt_mtl_car_out_notify_car_id.Text);
            //Task.Run(() =>
            //{
            //    var mtl_mapaction = maintainEQ.
            //        getMapActionByIdentityKey(nameof(com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)) as
            //        com.mirle.ibg3k0.sc.Data.ValueDefMapAction.MTxValueDefMapActionBase;
            //    mtl_mapaction.OHxC_CarOutNotify(car_id);
            //}
            //);
        }

        private void btn_rename_cst_id_Click(object sender, EventArgs e)
        {
            string cst_id = txt_rename_cst_id.Text;

            Task.Run(() =>
            {
                bcApp.SCApplication.VehicleService.CarrierIDRenameRequset(vh_id, noticeCar.CST_ID, cst_id);
            });
        }

        private void btn_mtl_o2m_u2d_CarOutInterface(object sender, EventArgs e)
        {
            bool car_out_car_out_interlock = btn_mtl_o2m_u2d_caroutInterlock.Checked;
            bool car_out_car_out_ready = btn_mtl_o2m_u2d_caroutready.Checked;
            bool car_out_carmoving = btn_mtl_o2m_u2d_carmoving.Checked;
            bool car_out_car_move_cmp = btn_mtl_o2m_u2d_carmovingcmp.Checked;
            AEQPT eQPT = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID("MTS");

            Task.Run(() => (
            eQPT.getMapActionByIdentityKey("MTSValueDefMapActionNew") as sc.Data.ValueDefMapAction.MTSValueDefMapActionNew)
            .OHxC2MTL_CarOutInterface(car_out_car_out_interlock, car_out_car_out_ready, car_out_carmoving, car_out_car_move_cmp));
        }

        private void btn_mtl_o2m_d2u_CarInInterfaece(object sender, EventArgs e)
        {
            bool car_in_carmoving = btn_mtl_o2m_d2u_moving.Checked;
            bool car_in_car_move_cmp = btn_mtl_o2m_d2u_movingcmp.Checked;
            AEQPT eQPT = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID("MTS");

            Task.Run(() =>
            (eQPT.getMapActionByIdentityKey("MTSValueDefMapActionNew") as sc.Data.ValueDefMapAction.MTSValueDefMapActionNew)
            .OHxC2MTL_CarInInterface(car_in_carmoving, car_in_car_move_cmp));
        }

        private void btn_mtl2ohxc_carinterface_refresh_Click(object sender, EventArgs e)
        {
            bool car_out_safety_check = false;
            bool car_out_move_cmp = false;
            bool car_in_safety_check = false;
            bool car_in_interlock = false;
            //AEQPT eQPT = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID("MTS");
            string maption_action_name = "";
            if (maintainEQ is sc.Data.VO.MaintainSpace)
            {
                maption_action_name = "MTSValueDefMapActionNew";
            }
            else
            {
                maption_action_name = "MTLValueDefMapActionNew";
            }

            (maintainEQ.getMapActionByIdentityKey(maption_action_name) as sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)
            .GetMTL2OHxC_CarOutInterface(out car_out_safety_check, out car_out_move_cmp);
            (maintainEQ.getMapActionByIdentityKey(maption_action_name) as sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)
            .GetMTL2OHxC_CarInInterface(out car_in_safety_check, out car_in_interlock);
            btn_mtl_m2o_u2d_move_cmp.Checked = car_out_move_cmp;
            btn_mtl_m2o_u2d_safetycheck.Checked = car_out_safety_check;
            btn_mtl_m2o_d2u_safetycheck.Checked = car_in_safety_check;
            btn_mtl_m2o_d2u_interlock.Checked = car_in_interlock;
        }

        private async void btn_SendHIDControl_Click(object sender, EventArgs e)
        {
            try
            {
                btn_SendHIDControl.Enabled = false;
                SCApplication scApp = SCApplication.getInstance();
                AEQPT eqpt_HID = scApp.getEQObjCacheManager().getEquipmentByEQPTID(comboBox_HID.Text);
                if (eqpt_HID != null)
                {
                    HIDValueDefMapAction mapAction = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapAction") as HIDValueDefMapAction);
                    if (mapAction != null)
                    {
                        bool signal = comboBox_HID_control.SelectedIndex == 0 ? true : false;
                        await Task.Run(() => mapAction.HID_Control(signal));
                    }
                    else
                    {
                        HIDValueDefMapActionPH2 mapActionPH2 = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapActionPH2") as HIDValueDefMapActionPH2);

                        bool signal = comboBox_HID_control.SelectedIndex == 0 ? true : false;
                        await Task.Run(() => mapActionPH2.HID_Control(signal));
                    }

                }
                else
                {
                    MessageBox.Show("Please Select HID.");
                }
            }
            finally
            {
                btn_SendHIDControl.Enabled = true;
            }
        }



        private void button7_Click_1(object sender, EventArgs e)
        {
            SCApplication scApp = SCApplication.getInstance();
            Task.Run(() => scApp.LineService.OnlineWithHostOp());
        }

        private void ck_CycleRunTest_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.IsCycleRun = ck_CycleRunTest.Checked;
        }


        private void uctlButton1_Click(object sender, EventArgs e)
        {
            //var eQPT = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID("MTS") as sc.Data.VO.MaintainSpace;
            string maption_action_name = "";
            if (maintainEQ is sc.Data.VO.MaintainSpace)
            {
                maption_action_name = "MTSValueDefMapActionNew";
            }
            else
            {
                maption_action_name = "MTLValueDefMapActionNew";
            }

            Task.Run(() => (
            maintainEQ.getMapActionByIdentityKey(maption_action_name) as sc.Data.ValueDefMapAction.MTxValueDefMapActionBase)
            .MTL_LFTStatus(null, null));
            Adapter.Invoke((obj) =>
            {
                lbl_mtx_mode.Text = maintainEQ.MTxMode.ToString();
                lbl_hasvh_value.Text = maintainEQ.HasVehicle.ToString();
            }, null);

        }

        private async void btn_carOutTest_Click(object sender, EventArgs e)
        {
            try
            {
                string vh_id = cmb_tcpipctr_Vehicle.Text;
                btn_carOutTest.Enabled = false;
                bool isSuccess = false;
                string result = "";
                await Task.Run(() =>
                {
                    //var r = bcApp.SCApplication.MTLService.carOutRequset(vh_id);
                    //isSuccess = r.isSuccess;
                    //result = r.result;

                });
                if (!isSuccess)
                {
                    MessageBox.Show(result);
                }
            }
            catch { }
            finally
            {
                btn_carOutTest.Enabled = true;
            }
        }

        private void btn_cauout_cancel_Click(object sender, EventArgs e)
        {
            //bcApp.SCApplication.MTLService.carOutRequestCancle();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private async void btn_changeToAutoRemote_Click(object sender, EventArgs e)
        {
            try
            {
                btn_changeToAutoRemote.Enabled = false;
                await Task.Run(() => bcApp.SCApplication.VehicleService.VehicleAutoModeCahnge(vh_id, sc.ProtocolFormat.OHTMessage.VHModeStatus.AutoRemote));
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"change to auto remote failed, Exception:{ex.Message}");
            }
            finally
            {
                btn_changeToAutoRemote.Enabled = true;
            }
        }
        private async void btn_changeToAutoLocal_Click(object sender, EventArgs e)
        {
            try
            {
                btn_changeToAutoMTS.Enabled = false;
                await Task.Run(() => bcApp.SCApplication.VehicleService.VehicleAutoModeCahnge(vh_id, sc.ProtocolFormat.OHTMessage.VHModeStatus.AutoMts));
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"change to auto mts failed, Exception:{ex.Message}");

            }
            finally
            {
                btn_changeToAutoMTS.Enabled = true;
            }
        }
        private async void btn_changeToAutoMTL_Click(object sender, EventArgs e)
        {
            try
            {
                btn_changeToAutoMTL.Enabled = false;
                await Task.Run(() => bcApp.SCApplication.VehicleService.VehicleAutoModeCahnge(vh_id, sc.ProtocolFormat.OHTMessage.VHModeStatus.AutoMtl));
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"change to auto mtl failed, Exception:{ex.Message}");
            }
            finally
            {
                btn_changeToAutoMTL.Enabled = true;
            }
        }
        private async void btn_changeToAutoLocal_Click_1(object sender, EventArgs e)
        {
            try
            {
                btn_changeToAutoLocal.Enabled = false;
                await Task.Run(() => bcApp.SCApplication.VehicleService.VehicleAutoModeCahnge(vh_id, sc.ProtocolFormat.OHTMessage.VHModeStatus.AutoLocal));
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Error, Class: nameof(DebugForm), Device: "OHTC",
                Data: $"change to auto local failed, Exception:{ex.Message}");
            }
            finally
            {
                btn_changeToAutoLocal.Enabled = true;
            }
        }


        private void btn_hid_info_Click(object sender, EventArgs e)
        {
            AEQPT eqpt_HID = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID(comboBox_HID.Text);
            if (eqpt_HID != null)
            {
                var hid_info = eqpt_HID.HID_Info;
                if (hid_info == null) return;
                Adapter.Invoke((obj) =>
                {
                    lbl_hour_sigma_word_value.Text = hid_info.Hour_Sigma_Converted.ToString();
                    lbl_vr_value.Text = hid_info.VR_Converted.ToString();
                    lbl_vs_value.Text = hid_info.VS_Converted.ToString();
                    lbl_vt_value.Text = hid_info.VT_Converted.ToString();

                    lbl_ar_value.Text = hid_info.AR_Converted.ToString();
                    lbl_as_value.Text = hid_info.AS_Converted.ToString();
                    lbl_at_value.Text = hid_info.AT_Converted.ToString();

                    lbl_sigma_w_value.Text = hid_info.Sigma_W_Converted.ToString();
                }, null);
            }
            else
            {
                MessageBox.Show("Please Select HID.");
            }
        }

        private void btn_mtl_info_refresh_Click(object sender, EventArgs e)
        {
            //var eQPT = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID("MTS") as sc.Data.VO.MaintainSpace;
            Adapter.Invoke((obj) =>
            {
                lbl_mtl_current_car_id.Text = maintainEQ.CurrentCarID?.ToString();
                lbl_mtl_has_vh.Text = maintainEQ.HasVehicle.ToString();
                lbl_mtl_stop_single.Text = maintainEQ.StopSingle.ToString();
                lbl_mtl_mode.Text = maintainEQ.MTxMode.ToString();
                lbl_mtl_location.Text = maintainEQ.MTLLocation.ToString();
                lbl_mtl_moving_status.Text = maintainEQ.MTLMovingStatus.ToString();
                lbl_mtl_encoder.Text = maintainEQ.Encoder.ToString();
                lbl_mtl_in_position.Text = maintainEQ.VhInPosition.ToString();

            }, null);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private async void btn_carOutTest_Click_1(object sender, EventArgs e)
        {
            try
            {
                string maintain_eq = cmb_maintain_device.Text;
                string vh_id = cmb_car_out_vh.Text;
                sc.Data.VO.Interface.IMaintainDevice maintainDevice = bcApp.SCApplication.EquipmentBLL.cache.getMaintainDevice(maintain_eq);
                btn_carOutTest.Enabled = false;
                bool isSuccess = false;
                string result = "";
                await Task.Run(() =>
                {
                    //var r = bcApp.SCApplication.MTLService.carOutRequset(maintainDevice, vh_id);
                    var r = default((bool isSuccess, string result));
                    AVEHICLE pre_car_out_vh = bcApp.SCApplication.VehicleBLL.cache.getVhByID(vh_id);
                    if (maintainDevice is sc.Data.VO.MaintainLift)
                    {
                        sc.Data.VO.Interface.IMaintainDevice dockingMTS = bcApp.SCApplication.EquipmentBLL.cache.GetDockingMTLOfMaintainSpace();
                        r = bcApp.SCApplication.MTLService.checkVhAndMTxCarOutStatus(maintainDevice, dockingMTS, pre_car_out_vh);
                        if (r.isSuccess)
                        {
                            r = bcApp.SCApplication.MTLService.processCarOutScenario(maintainDevice as sc.Data.VO.MaintainLift, pre_car_out_vh);
                        }
                    }
                    else if (maintainDevice is sc.Data.VO.MaintainSpace)
                    {
                        r = bcApp.SCApplication.MTLService.checkVhAndMTxCarOutStatus(maintainDevice, null, pre_car_out_vh);
                        if (r.isSuccess)
                        {
                            r = bcApp.SCApplication.MTLService.processCarOutScenario(maintainDevice as sc.Data.VO.MaintainSpace, pre_car_out_vh);
                        }
                    }
                    isSuccess = r.isSuccess;
                    result = r.result;

                });
                if (!isSuccess)
                {
                    MessageBox.Show(result);
                }

            }
            catch { }
            finally
            {
                btn_carOutTest.Enabled = true;
            }
        }

        private async void btn_cauout_cancel_Click_1(object sender, EventArgs e)
        {
            try
            {
                btn_cauout_cancel.Enabled = false;
                string maintain_eq = cmb_maintain_device.Text;
                sc.Data.VO.Interface.IMaintainDevice maintainDevice = bcApp.SCApplication.EquipmentBLL.cache.getMaintainDevice(maintain_eq);
                await Task.Run(() => bcApp.SCApplication.MTLService.carOutRequestCancle(maintainDevice));
            }
            catch { }
            finally
            {
                btn_cauout_cancel.Enabled = true;
            }
        }

        private void btn_refresh_carout_info_Click(object sender, EventArgs e)
        {
            var mtl = bcApp.SCApplication.EquipmentBLL.cache.GetMaintainLift();
            var mts = bcApp.SCApplication.EquipmentBLL.cache.GetDockingMTLOfMaintainSpace();
            Adapter.Invoke((obj) =>
            {
                lbl_mtlCarOutVh.Text = mtl.PreCarOutVhID;
                lbl_mtsCarOutVh.Text = mts.PreCarOutVhID;
            }, null);

        }
        AEQPT maintainEQ = null;
        private void cmb_maintain_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            string device_id = cmb_maintain_device.Text;
            maintainEQ = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID(device_id) as AEQPT;
        }

        private async void btn_changeToRemove_Click(object sender, EventArgs e)
        {
            try
            {

                if (!noticeCar.IS_INSTALLED)
                {
                    MessageBox.Show($"{vh_id} is removed ready!");
                    return;
                }
                btn_changeToRemove.Enabled = false;
                //await Task.Run(() => bcApp.SCApplication.VehicleService.Remove(vh_id));
                (bool isSuccess, string result) check_result = default((bool isSuccess, string result));
                await Task.Run(() => check_result = bcApp.SCApplication.VehicleService.RemoveNew(vh_id));
                //MessageBox.Show($"{vh_id} remove ok");
                if (check_result.isSuccess)
                {
                    MessageBox.Show($"{vh_id} remove ok");
                }
                else
                {
                    MessageBox.Show($"{vh_id} remove fail.{Environment.NewLine}" +
                                    $"result:{check_result.result}");
                }
                lbl_install_status.Text = noticeCar?.IS_INSTALLED.ToString();
            }
            finally
            {
                btn_changeToRemove.Enabled = true;
            }
        }

        private async void btn_changeToInstall_Click(object sender, EventArgs e)
        {
            try
            {
                if (noticeCar.IS_INSTALLED)
                {
                    MessageBox.Show($"{vh_id} is install ready!");
                    return;
                }

                btn_changeToInstall.Enabled = false;
                (bool isSuccess, string result) check_result = default((bool isSuccess, string result));
                //await Task.Run(() => bcApp.SCApplication.VehicleService.Install(vh_id));
                await Task.Run(() => check_result = bcApp.SCApplication.VehicleService.InstallNew(vh_id));
                //MessageBox.Show($"{vh_id} install ok");
                if (check_result.isSuccess)
                {
                    MessageBox.Show($"{vh_id} install ok");
                }
                else
                {
                    MessageBox.Show($"{vh_id} install fail.{Environment.NewLine}" +
                                    $"result:{check_result.result}");
                }
                lbl_install_status.Text = noticeCar?.IS_INSTALLED.ToString();
            }
            finally
            {
                btn_changeToInstall.Enabled = true;
            }
        }

        private void cb_test_duplicate_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.TestDuplicate = cb_test_duplicate.Checked;
        }

        private async void btn_refresh_vh_order_in_seg_Click(object sender, EventArgs e)
        {
            try
            {
                btn_refresh_vh_order_in_seg.Enabled = false;
                string seg_id = cmb_refresh_vh_order_in_seg_id.Text;
                var seg_obj = bcApp.SCApplication.SegmentBLL.cache.GetSegment(seg_id);
                await Task.Run(() => seg_obj.RefreshVhOrder(bcApp.SCApplication.VehicleBLL, bcApp.SCApplication.SectionBLL));
                RefreshVehicleOrderInSegment(seg_id);
            }
            finally
            {
                btn_refresh_vh_order_in_seg.Enabled = true;
            }
        }

        private void cmb_refresh_vh_order_in_seg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seg_id = (sender as ComboBox).Text;
            RefreshVehicleOrderInSegment(seg_id);
        }

        private void RefreshVehicleOrderInSegment(string seg_id)
        {
            var seg_obj = bcApp.SCApplication.SegmentBLL.cache.GetSegment(seg_id);
            txt_vh_order_in_segment.Text = string.Join(",", seg_obj.GetVehicleOrderInSegment());
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void LifterPosition_cb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void set_MTLMTS_btn_Click(object sender, EventArgs e)
        {
            string eq_id = MTLMTS_cb_box.Text;
            MTSMTL = bcApp.SCApplication.getEQObjCacheManager().getEquipmentByEQPTID(eq_id);
        }

        private void plc_link_btn_Click(object sender, EventArgs e)
        {
            if (MTSMTL != null)
            {
                if (MTSMTL.Plc_Link_Stat == LinkStatus.LinkOK)
                {
                    MTSMTL.Plc_Link_Stat = LinkStatus.LinkFail;
                    return;
                }
                if (MTSMTL.Plc_Link_Stat == LinkStatus.LinkFail)
                {
                    MTSMTL.Plc_Link_Stat = LinkStatus.LinkOK;
                    return;
                }
            }
        }

        private void set_Alive_btn_Click(object sender, EventArgs e)
        {
            if (MTSMTL != null)
            {
                int alive = Convert.ToInt32(alive_numeric.Value);
                //MTSMTL.Is_Eq_Alive = !MTSMTL.Is_Eq_Alive;
                MTSMTL.Eq_Alive_Last_Change_time = DateTime.Now;
            }
        }

        private void mode_change_btn_Click(object sender, EventArgs e)
        {
            if (MTSMTL != null)
            {
                if (MTSMTL.MTxMode == sc.ProtocolFormat.OHTMessage.MTxMode.Auto)
                {
                    MTSMTL.MTxMode = sc.ProtocolFormat.OHTMessage.MTxMode.Manual;
                    return;
                }
                if (MTSMTL.MTxMode == sc.ProtocolFormat.OHTMessage.MTxMode.Manual)
                {
                    MTSMTL.MTxMode = sc.ProtocolFormat.OHTMessage.MTxMode.Auto;
                    return;
                }
            }
        }

        private void OHTC_send_interlock_btn_Click(object sender, EventArgs e)
        {
            //if (MTSMTL.EQPT_ID.StartsWith("MTL"))
            //{
            //    (MTSMTL as MaintainLift).CarOutInterlock = !(MTSMTL as MaintainLift).CarOutInterlock;
            //    return;
            //}
            //if (MTSMTL.EQPT_ID.StartsWith("MTL"))
            //{
            //    (MTSMTL as MaintainLift).CarOutInterlock = !(MTSMTL as MaintainLift).CarOutInterlock;
            //}
        }

        private void set_VehicleID_btn_Click(object sender, EventArgs e)
        {
            if (MTSMTL != null)
            {
                MTSMTL.CurrentCarID = vehicleID_textbox.Text;
            }
        }

        private void set_Lifterposition_btn_Click(object sender, EventArgs e)
        {
            if (LifterPosition_cb.Text == "UP")
            {
                MTSMTL.MTLLocation = sc.ProtocolFormat.OHTMessage.MTLLocation.Upper;
            }
            else if (LifterPosition_cb.Text == "DOWN")
            {
                MTSMTL.MTLLocation = sc.ProtocolFormat.OHTMessage.MTLLocation.Bottorn;
            }
            else if (LifterPosition_cb.Text == "NONE")
            {
                MTSMTL.MTLLocation = sc.ProtocolFormat.OHTMessage.MTLLocation.None;
            }
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            SCApplication scApp = SCApplication.getInstance();
            AEQPT eqpt_HID = scApp.getEQObjCacheManager().getEquipmentByEQPTID("HID");
            HIDValueDefMapAction mapAction = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapAction") as HIDValueDefMapAction);
            mapAction.PowerAlarm(null, null);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            SCApplication scApp = SCApplication.getInstance();
            AEQPT eqpt_HID = scApp.getEQObjCacheManager().getEquipmentByEQPTID("HID");
            HIDValueDefMapAction mapAction = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapAction") as HIDValueDefMapAction);
            mapAction.TempAlarm(null, null);

        }

        private void set_Distance_btn_Click(object sender, EventArgs e)
        {
            uint distance = Convert.ToUInt32(numericUpDown_distance.Value);
            if (MTSMTL != null)
            {
                if (MTSMTL.EQPT_ID == "MTS")
                {
                    (MTSMTL as MaintainSpace).CurrentPreCarOurDistance = distance;
                }
                else if (MTSMTL.EQPT_ID == "MTS2")
                {
                    (MTSMTL as MaintainSpace).CurrentPreCarOurDistance = distance;
                }
                else if (MTSMTL.EQPT_ID == "MTL")
                {
                    (MTSMTL as MaintainLift).CurrentPreCarOurDistance = distance;
                }
                else
                {

                }
            }
        }

        private async void btn_open_tcp_port_Click(object sender, EventArgs e)
        {
            bool is_success = false;
            await Task.Run(() =>
             {
                 is_success = bcApp.SCApplication.VehicleService.startVehicleTcpIpServer(vh_id);
             });
            MessageBox.Show(is_success ? "OK" : "NG");
        }

        private async void btn_close_tcp_port_Click(object sender, EventArgs e)
        {
            bool is_success = false;
            await Task.Run(() =>
            {
                is_success = bcApp.SCApplication.VehicleService.stopVehicleTcpIpServer(vh_id);
            });
            MessageBox.Show(is_success ? "OK" : "NG");
        }

        private void lbl_install_status_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void lbl_listening_status_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Control) == Keys.Control)
            {
                btn_open_tcp_port.Visible = true;
                btn_close_tcp_port.Visible = true;
            }
        }

        private void ck_SaftyCheckComplete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_A", "SAFETY_CHECK_COMPLETE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        private void ck_SaftyCheckRequest_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_A", "SAFETY_CHECK_REQUEST");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        private void ck_DoorClosed_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_A", "DOOR_CLOSE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        private void ck_Alive_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_A", "OHCV_TO_OHTC_ALIVE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }




        private void ck_DoorClosed_B_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_B", "DOOR_CLOSE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }


        private void ck_SaftyCheckRequest_B_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_B", "SAFETY_CHECK_REQUEST");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        private void ck_SaftyCheckComplete_B_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_B", "SAFETY_CHECK_COMPLETE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        private void ck_Alive_B_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            bcf.Controller.ValueRead vr = bcApp.SCApplication.getBCFApplication().getReadValueEvent(SCAppConstants.EQPT_OBJECT_CATE_EQPT, "CV31_B", "OHCV_TO_OHTC_ALIVE");
            vr.Value = new int[] { ck.Checked ? 1 : 0 };
        }

        OHCV selectedOHCV = null;
        private void cb_cv_ids_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cv_id = (sender as ComboBox).Text;
            selectedOHCV = bcApp.SCApplication.EquipmentBLL.cache.getOHCV(cv_id);
        }

        private async void button11_Click_1(object sender, EventArgs e)
        {
            string block_id = txt_req_block_id_test.Text;
            bool is_success = false;
            await Task.Run(() =>
            {
                is_success = bcApp.SCApplication.VehicleService.ProcessBlockReqTest
                (bcApp.SCApplication.getBCFApplication(), noticeCar, block_id);
            });
            MessageBox.Show(is_success.ToString());
        }

        private async void btnLighthouse_Click(object sender, EventArgs e)
        {
            try
            {
                tableLayoutPanel6.Enabled = false;
                await Task.Run(() =>
                {
                    var Lighthouse = bcApp.SCApplication.EquipmentBLL.cache.getFourColorLighthouse();
                    if (sender == btn_lighthouse_red_set)
                    {
                        Lighthouse.setFourColorLightRedWithBuzzer(true, true);
                    }
                    else if (sender == btn_lighthouse_green_set)
                    {
                        Lighthouse.setFourColorLightGreen(true);
                    }
                    else if (sender == btn_lighthouse_blue_set)
                    {
                        Lighthouse.setFourColorLightBlue(true);
                    }
                    else if (sender == btn_lighthouse_orange_set)
                    {
                        Lighthouse.setFourColorLightOrange(true);
                    }
                    else if (sender == btn_lighthouse_red_reset)
                    {
                        Lighthouse.setFourColorLightRedWithBuzzer(false, false);
                    }
                    else if (sender == btn_lighthouse_green_reset)
                    {
                        Lighthouse.setFourColorLightGreen(false);
                    }
                    else if (sender == btn_lighthouse_blue_reset)
                    {
                        Lighthouse.setFourColorLightBlue(false);
                    }
                    else if (sender == btn_lighthouse_orange_reset)
                    {
                        Lighthouse.setFourColorLightOrange(false);
                    }
                });
            }
            finally
            {
                tableLayoutPanel6.Enabled = true;
            }
        }

        private void ch_force_pass_red_light_with_buzzer_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.isForcePassFourColorLightRedWithBuzzerSignal = ch_force_pass_red_light_with_buzzer.Checked;
        }

        private void lbl_id_37_cmdID_value_Click(object sender, EventArgs e)
        {

        }

        private async void MaxAllowActionTimeSecond_Set_btn_Click(object sender, EventArgs e)
        {
            try
            {
                MaxAllowActionTimeSecond_Set_btn.Enabled = false;
                List<AECDATAMAP> aECDATAMAPs = new List<AECDATAMAP>();
                if (!int.TryParse(MaxAllowActionTimeSecond_Current_txb.Text, out int test))
                {
                    MessageBox.Show("Please enter number.");
                    return;
                }
                maxAllowActionTimeECData.ECV = MaxAllowActionTimeSecond_Current_txb.Text;
                aECDATAMAPs.Add(maxAllowActionTimeECData);
                string returnMsg = string.Empty;
                await Task.Run(() => bcApp.SCApplication.LineBLL.updateECData(aECDATAMAPs, out returnMsg, false));
                if (!string.IsNullOrWhiteSpace(returnMsg))
                {
                    MessageBox.Show(returnMsg);
                }
            }
            finally
            {
                MaxAllowActionTimeSecond_Set_btn.Enabled = true;
            }
        }

        private void num_test_dis_ValueChanged(object sender, EventArgs e)
        {
            string vh_id = cmb_tcpipctr_Vehicle.Text;
            string sec_id = txt_sec_id.Text;
            uint distance = (uint)num_test_dis.Value;
            var section_obj = mainForm.BCApp.SCApplication.SectionBLL.cache.GetSection(sec_id);
            sc.ProtocolFormat.OHTMessage.ID_134_TRANS_EVENT_REP id_134_trans_event_rep = new sc.ProtocolFormat.OHTMessage.ID_134_TRANS_EVENT_REP()
            {
                CurrentAdrID = section_obj == null ? "" : section_obj.TO_ADR_ID,
                CurrentSecID = sec_id,
                EventType = sc.ProtocolFormat.OHTMessage.EventType.AdrPass,
                SecDistance = distance,
            };

            Task.Run(() =>
            {
                mainForm.BCApp.SCApplication.VehicleBLL.setAndPublishPositionReportInfo2Redis(vh_id, id_134_trans_event_rep);
            });
        }

        private async void uctlButton2_Click(object sender, EventArgs e)
        {
            try
            {

                uctlButton2.Enabled = false;
                (bool isSuccess, string result) check_result = default((bool isSuccess, string result));
                //await Task.Run(() => bcApp.SCApplication.VehicleService.Install(vh_id));
                await Task.Run(() => check_result = bcApp.SCApplication.VehicleService.FakeRemoveInstallNew(vh_id));
                //MessageBox.Show($"{vh_id} install ok");
                if (check_result.isSuccess)
                {
                    MessageBox.Show($"{vh_id} synchonize ok");
                }
                else
                {
                    MessageBox.Show($"{vh_id} synchonize fail.{Environment.NewLine}" +
                                    $"result:{check_result.result}");
                }

            }
            finally
            {
                uctlButton2.Enabled = true;
            }
        }

        private void btn_hid_datetime_sync_Click(object sender, EventArgs e)
        {
            try
            {
                btn_hid_datetime_sync.Enabled = false;
                SCApplication scApp = SCApplication.getInstance();
                AEQPT eqpt_HID = scApp.getEQObjCacheManager().getEquipmentByEQPTID(comboBox_HID.Text);
                if (eqpt_HID != null)
                {
                    HIDValueDefMapAction mapAction = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapAction") as HIDValueDefMapAction);
                    if (mapAction != null)
                    {
                        mapAction.DateTimeSyncCommand(DateTime.Now);
                    }
                    else
                    {
                        HIDValueDefMapActionPH2 mapActionPh2 = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapActionPH2") as HIDValueDefMapActionPH2);
                        if (mapActionPh2 != null)
                        {
                            mapActionPh2.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select HID.");
                }
            }
            finally
            {
                btn_hid_datetime_sync.Enabled = true;
            }
        }

        private async void btn_SlientControl_Click(object sender, EventArgs e)
        {
            SCApplication scApp = SCApplication.getInstance();
            AEQPT eqpt_HID = scApp.getEQObjCacheManager().getEquipmentByEQPTID(comboBox_HID.Text);
            if (eqpt_HID != null)
            {
                HIDValueDefMapActionPH2 mapAction = (eqpt_HID.getMapActionByIdentityKey("HIDValueDefMapActionPH2") as HIDValueDefMapActionPH2);
                if (mapAction != null)
                {
                    await Task.Run(() => mapAction.SilentCommand());
                }
            }
            else
            {
                MessageBox.Show("Please Select HID.");
            }
        }

        private void num_ChangePathCommandPath_ValueChanged(object sender, EventArgs e)
        {
            sc.App.SystemParameter.setChangePathCommandCount((int)num_ChangePathCommandPath.Value);
        }

        private void ck_is_open_command_shift_CheckedChanged(object sender, EventArgs e)
        {
            DebugParameter.isOpenCommandShift = ck_is_open_command_shift.Checked;
        }
    }
}
