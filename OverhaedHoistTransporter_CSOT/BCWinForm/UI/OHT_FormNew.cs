﻿using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc.ObjectRelay;
using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.Utility.ul.Data.VO;
using com.mirle.ibg3k0.sc.Common;
using System.Threading;
using NLog;
using com.mirle.ibg3k0.bc.winform.Common;
using System.Drawing;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class OHT_FormNew : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BCMainForm mainform = null;
        SCApplication scApp = null;
        //BindingList<VehicleObjToShow> ObjectToShow_list = new BindingList<VehicleObjToShow>();
        BindingSource bindingSource = new BindingSource();
        string[] allAdr_ID = null;
        string[] allPortID = null;
        List<CMD_MCSObjToShow> cmd_mcs_obj_to_show = null;
        BindingSource cmsMCS_bindingSource = new BindingSource();

        List<ALARM> aLARMs = new List<ALARM>();

        public OHT_FormNew(BCMainForm _form)
        {
            InitializeComponent();
            
            mainform = _form;
            scApp = mainform.BCApp.SCApplication;
            uctlMapWPF1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(29, 36, 60));
            uctlMapWPF1.Start(mainform.BCApp);

            //ui_Vehicle1.start("OHT001");
            initialComBox();
            initialDataGreadView();
            // utilityLog_SECS.start<LogTitle_SECS>();
            //dgv_vhStatus.AutoGenerateColumns = false;
            bindingSource.DataSource = scApp.getEQObjCacheManager().CommonInfo.ObjectToShow_list;
            dgv_vhStatus.DataSource = bindingSource;
            scApp.getEQObjCacheManager().CommonInfo.ObjectToShow_list.Clear();

            
            dgv_TransferCommand.AutoGenerateColumns = false;



            double distance_scale = 1000;
            if (SCUtility.isMatche(scApp.BC_ID, SCAppConstants.WorkVersion.VERSION_NAME_TAICHUNG6F))
            {
                distance_scale = 1000 * 10000;
            }

            //using (DBConnection_EF context = new DBConnection_EF())
            //{
            //lstSection = context.AVEHICLE.ToList();
            //context.AVEHICLE.Load();
            foreach (var vh in scApp.getEQObjCacheManager().getAllVehicle())
            {
                VehicleObjToShow vhShowObj = new VehicleObjToShow(vh, distance_scale);

                scApp.getEQObjCacheManager().CommonInfo.ObjectToShow_list.Add(vhShowObj);
            }
            //}
            timer_TimedUpdates.Enabled = true;
            adjustmentDataGridViewWeight();

            initialEvent();
            SetHostControlState(scApp.getEQObjCacheManager().getLine());
            scApp.RoadControlService.SegmentListChanged += RoadControlService_SegmentListChanged;
            RefreshMapColor();
        }

        private void RoadControlService_SegmentListChanged(object sender, ASEGMENT e)
        {
            Adapter.Invoke((obj) =>
            {
                RefreshMapColor();
            }, null);
        }

        private void initialEvent()
        {
            ALINE line = scApp.getEQObjCacheManager().getLine();

            line.addEventHandler(this.Name
           , BCFUtility.getPropertyName(() => line.ServiceMode)
           , (s1, e1) =>
           {
               Adapter.Invoke((obj) =>
               {
                   switch (line.ServiceMode)
                   {
                       case SCAppConstants.AppServiceMode.None:
                           lbl_isMaster.BackColor = Color.Gray;
                           break;
                       case SCAppConstants.AppServiceMode.Active:
                           lbl_isMaster.BackColor = Color.Green;
                           break;
                       case SCAppConstants.AppServiceMode.Standby:
                           lbl_isMaster.BackColor = Color.Yellow;
                           break;
                   }
               }, null);
           });
            line.addEventHandler(this.Name
           , BCFUtility.getPropertyName(() => line.Secs_Link_Stat)
                , (s1, e1) =>
                {
                    lbl_hostconn.BackColor =
                    line.Secs_Link_Stat == SCAppConstants.LinkStatus.LinkOK ? Color.Green : Color.Gray;
                }
                );
            line.addEventHandler(this.Name
           , BCFUtility.getPropertyName(() => line.Redis_Link_Stat)
                , (s1, e1) =>
                {
                    lbl_RediStat.BackColor =
                    line.Redis_Link_Stat == SCAppConstants.LinkStatus.LinkOK ? Color.Green : Color.Gray;
                }
                );
            line.addEventHandler(this.Name
            , BCFUtility.getPropertyName(() => line.IsEarthquakeHappend)
                , (s1, e1) =>
                {
                    lbl_earthqualeHappend.BackColor =
                    line.IsEarthquakeHappend ? Color.Red : Color.Gray;
                    //if (line.IsEarthquakeHappend)
                    //{
                    //    aLARMs[0].ALAM_CODE = "AE001";
                    //    aLARMs[0].ALAM_DESC = "An earthquake has occurred !!!";
                    //    aLARMs[0].ALAM_LVL = "Alarm";
                    //    aLARMs[0].EQPT_ID = "MCP";
                    //    aLARMs[0].RPT_DATE_TIME = DateTime.Now.ToString(SCAppConstants.DateTimeFormat_19);
                    //}
                    //else
                    //{
                    //    aLARMs[0].ALAM_CODE = "";
                    //    aLARMs[0].ALAM_DESC = "";
                    //    aLARMs[0].ALAM_LVL = "";
                    //    aLARMs[0].EQPT_ID = "";
                    //    aLARMs[0].RPT_DATE_TIME = "";
                    //}
                    //Adapter.Invoke((obj) =>
                    //{
                    //    if (line.IsEarthquakeHappend)
                    //        tbcList.SelectedIndex = 4;
                    //    dgv_Alarm.Refresh();
                    //}, null);
                }
                );
            line.addEventHandler(this.Name
                , BCFUtility.getPropertyName(() => line.DetectionSystemExist)
                    , (s1, e1) =>
                    {
                        lbl_detectionSystemExist.BackColor =
                        line.DetectionSystemExist == SCAppConstants.ExistStatus.Exist ? Color.Green : Color.Gray;
                    }
                    );
            line.addEventHandler(this.Name
                , BCFUtility.getPropertyName(() => line.Host_Control_State)
                    , (s1, e1) =>
                    {
                        SetHostControlState(line);
                    }
                    );
            scApp.getNatsManager().Subscriber(SCAppConstants.NATS_SUBJECT_CURRENT_ALARM, SetCurrentAlarm);
        }

        private void SetCurrentAlarm(object sender, EventArgs e)
        {
            List<ALARM> alarms = scApp.AlarmBLL.getCurrentAlarmsFromRedis();
            Adapter.Invoke((obj) =>
            {
                dgv_Alarm.DataSource = alarms;
            }, null);
        }

        private void SetHostControlState(ALINE line)
        {
            Color hostMode_Color = Color.Empty;
            switch (line.Host_Control_State)
            {
                case SCAppConstants.LineHostControlState.HostControlState.EQ_Off_line:
                    hostMode_Color = Color.Gray;
                    break;
                case SCAppConstants.LineHostControlState.HostControlState.On_Line_Local:
                    hostMode_Color = Color.Yellow;
                    break;
                case SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote:
                    hostMode_Color = Color.Green;
                    break;
                default:
                    hostMode_Color = Color.Gray;
                    break;
            }
            lbl_HoseMode.BackColor = hostMode_Color;
        }

        private void adjustmentDataGridViewWeight()
        {
            dgv_vhStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dgv_vhStatus.Columns)
            {
                switch (col.Name)
                {
                    case "MCS_CMD":
                        col.FillWeight = 1200;
                        break;
                    case "OHTC_CMD":
                        col.FillWeight = 1200;
                        break;
                    case "ACT_STATUS":
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = 1500;
                        break;
                    case "PACK_TIME":
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = 2400;
                        break;
                    case "CYCLERUN_TIME":
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = 2400;
                        break;
                    case "OBS_DIST2Show":
                    case "VEHICLE_ACC_DIST2Show":
                    case "ACC_SEC_DIST2Show":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
            }
        }

        void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (INotifyPropertyChanged item in e.NewItems)
                item.PropertyChanged += item_PropertyChanged;
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void initialComBox()
        {
            //string[] adr_port1_ID = allAddress_obj.
            //                        Where(adr => adr.PORT1_ID != null && adr.PORT1_ID != string.Empty).
            //                        Select(adr => adr.PORT1_ID).ToArray();
            //string[] adr_port2_ID = allAddress_obj.
            //            Where(adr => adr.PORT2_ID != null && adr.PORT2_ID != string.Empty).
            //            Select(adr => adr.PORT2_ID).ToArray();
            //List<string> portIDTemp = new List<string>();
            //portIDTemp.AddRange(adr_port1_ID);    
            //portIDTemp.AddRange(adr_port2_ID);
            //portIDTemp.OrderBy(id => id);
            allPortID = scApp.MapBLL.loadAllPort().Select(s => s.PORT_ID).ToArray();

            List<AADDRESS> allAddress_obj = scApp.MapBLL.loadAllAddress();
            allAdr_ID = allAddress_obj.Select(adr => adr.ADR_ID).ToArray();

            //過濾掉在MaintainDevice之中的Addresses
            allAdr_ID = allAdr_ID.Where(adr_id => !scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, adr_id)).
                                  ToArray();
            BCUtility.setComboboxDataSource(cmb_toAddress, allAdr_ID.ToArray());
            BCUtility.setComboboxDataSource(cmb_fromAddress, allAdr_ID.ToArray());
            //cmb_fromAddress.DataSource = allAdr_ID.ToArray();
            //cmb_fromAddress.AutoCompleteCustomSource.AddRange(allAdr_ID);
            //cmb_fromAddress.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cmb_fromAddress.AutoCompleteSource = AutoCompleteSource.ListItems;


            string[] allCycleRunZone = scApp.CycleBLL.loadCycleRunMasterByCurrentCycleTypeID
                (scApp.getEQObjCacheManager().getLine().Currnet_Cycle_Type).Select(master => master.ENTRY_ADR_ID).ToArray();
            cmb_cycRunZone.DataSource = allCycleRunZone;


            string[] allSec = scApp.MapBLL.loadAllSectionID().ToArray();
            cmb_fromSection.DataSource = allSec;
            cmb_fromSection.AutoCompleteCustomSource.AddRange(allSec);
            cmb_fromSection.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_fromSection.AutoCompleteSource = AutoCompleteSource.ListItems;


            List<string> lstVh = new List<string>();
            lstVh.Add(string.Empty);
            lstVh.AddRange(scApp.VehicleBLL.loadAllVehicle().Select(vh => vh.VEHICLE_ID).ToList());
            string[] allVh = lstVh.ToArray();
            cmb_Vehicle.DataSource = allVh;
            cmb_Vehicle.AutoCompleteCustomSource.AddRange(allVh);
            cmb_Vehicle.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_Vehicle.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbm_Action.DataSource = Enum.GetValues(typeof(E_CMD_TYPE)).Cast<E_CMD_TYPE>()
                                                  .Where(e => e == E_CMD_TYPE.Move ||
                                                              e == E_CMD_TYPE.Load ||
                                                              e == E_CMD_TYPE.Unload ||
                                                              e == E_CMD_TYPE.LoadUnload).ToList();


            List<string> park_zone_type = scApp.ParkBLL.loadAllParkZoneType();
            cb_parkZoneType.DataSource = park_zone_type;

        }


        private void initialDataGreadView()
        {
            aLARMs.Add(new ALARM());
            dgv_Alarm.AutoGenerateColumns = false;
            dgv_Alarm.DataSource = aLARMs;
        }


        private void btn_start_Click(object sender, EventArgs e)
        {
            string from_adr = cmb_fromAddress.Text;
            string to_adr = cmb_toAddress.Text;
            string vehicle_id = cmb_Vehicle.Text;

            AVEHICLE select_vh = scApp.VehicleBLL.cache.getVhByID(vehicle_id);
            if (select_vh == null)
            {
                MessageBox.Show($"No find vehile.", "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfSection(scApp.SegmentBLL, select_vh.CUR_SEC_ID))
            {
                MessageBox.Show($"Can't manual control in maintain device of vehicle:{vehicle_id} current section:{select_vh.CUR_SEC_ID}",
                    "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, select_vh.CUR_ADR_ID))
            {
                MessageBox.Show($"Can't manual control in maintain device of vehicle:{vehicle_id} current address:{select_vh.CUR_ADR_ID}",
                    "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, from_adr))
            {
                MessageBox.Show($"Can't set maintain device range of address:{from_adr}",
                    "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, to_adr))
            {
                MessageBox.Show($"Can't set maintain device range of address:{to_adr}",
                    "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            E_CMD_TYPE cmd_type;
            Enum.TryParse<E_CMD_TYPE>(cbm_Action.SelectedValue.ToString(), out cmd_type);

            if (cmd_type == E_CMD_TYPE.LoadUnload && from_adr == to_adr)
            {
                MessageBox.Show($"Can't execute a loadunload command with load address and unload address the same.", "Start fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cmd_type)
            {
                case E_CMD_TYPE.Move:
                case E_CMD_TYPE.MoveToMTL:
                case E_CMD_TYPE.SystemOut:
                case E_CMD_TYPE.SystemIn:
                case E_CMD_TYPE.MTLHome:
                    excuteMoveCommand(cmd_type);
                    break;
                case E_CMD_TYPE.Round:
                    excuteCycleRunCommand();
                    break;
                case E_CMD_TYPE.LoadUnload:
                    excuteLoadUnloadCommand();
                    break;
                case E_CMD_TYPE.Teaching:
                    excuteTeachingCommand();
                    break;
                case E_CMD_TYPE.Home:
                    excuteHomeCommand();
                    break;
                //case E_CMD_TYPE.MTLHome:
                //    excuteMTLHomeCommand();
                //    break;
                case E_CMD_TYPE.Load:
                    excuteLoadCommand();
                    break;
                case E_CMD_TYPE.Unload:
                    excuteUnloadCommand();
                    break;
            }
            cbm_Action.SelectedIndex = 0;
        }

        private async void excuteLoadUnloadCommand()
        {
            string fromSection = cmb_fromSection.Text;
            ASECTION asection = scApp.MapBLL.getSectiontByID(fromSection);

            string hostsource = cmb_fromAddress.Text;
            string hostdest = cmb_toAddress.Text;
            string from_adr = string.Empty;
            string to_adr = string.Empty;
            E_VH_TYPE vh_type = E_VH_TYPE.None;
            scApp.MapBLL.getAddressID(hostsource, out from_adr, out vh_type);
            scApp.MapBLL.getAddressID(hostdest, out to_adr);
            string vehicleId = string.Empty;
            //if (BCFUtility.isEmpty(cmb_Vehicle.Text))
            //{
            //    //AVEHICLE firstVh = scApp.VehicleBLL.findBestSuitableVhByFromAdr(fromadr);
            //    AVEHICLE firstVh = scApp.VehicleBLL.findBestSuitableVhStepByStepFromAdr(from_adr, vh_type);
            //    if (firstVh != null)
            //        vehicleId = firstVh.VEHICLE_ID.Trim();
            //}
            //else
            //{
            //    vehicleId = cmb_Vehicle.Text;
            //}

            vehicleId = cmb_Vehicle.Text;

            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find idle vehile.");
                return;
            }
            //string cst_id = $"{from_adr}To{to_adr}";
            string cst_id = txt_cstID.Text;
            if (BCFUtility.isEmpty(cst_id))
            {
                MessageBox.Show("cst id can't empty.");
                return;
            }
            //scApp.CMDBLL.creatCommand_OHTC(vehicleId, string.Empty, string.Empty,
            //                                E_CMD_TYPE.LoadUnload,
            //                                cmb_fromAddress.Text,
            //                                cmb_toAddress.Text, 0, 0);
            //Task.Run(() => { scApp.CMDBLL.generateCmd_OHTC_Details(); });
            //string from_adr = cmb_fromAddress.Text;
            //string to_adr = cmb_toAddress.Text;
            sc.BLL.CMDBLL.OHTCCommandCheckResult check_result_info = null;
            await Task.Run(() =>
             {
                 scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, cst_id,
                                                 E_CMD_TYPE.LoadUnload,
                                                 from_adr,
                                                 to_adr, 0, 0);
                 check_result_info = sc.BLL.CMDBLL.getCallContext<sc.BLL.CMDBLL.OHTCCommandCheckResult>
                    (sc.BLL.CMDBLL.CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT);
             });
            if (check_result_info != null && !check_result_info.IsSuccess)
            {
                MessageBox.Show(check_result_info.ToString(), "Command create fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void excuteCycleRunCommand()
        {
            string toAdr = string.Empty;
            string vehicleId = string.Empty;

            vehicleId = cmb_Vehicle.Text;
            toAdr = cmb_cycRunZone.Text;
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }

            //scApp.CMDBLL.creatCommand_OHTC(vehicleId, string.Empty, string.Empty,
            //                                E_CMD_TYPE.Round,
            //                                string.Empty,
            //                                toAdr, 0, 0);
            //Task.Run(() => { scApp.CMDBLL.generateCmd_OHTC_Details(); });
            Task.Run(() =>
            scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, string.Empty,
                                            E_CMD_TYPE.Round,
                                            string.Empty,
                                            toAdr, 0, 0));
        }

        private async void excuteMoveCommand(E_CMD_TYPE cmd_type)
        {
            string toAdr = string.Empty;
            string vehicleId = string.Empty;

            vehicleId = cmb_Vehicle.Text;
            toAdr = cmb_toAddress.Text;
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }

            //scApp.CMDBLL.creatCommand_OHTC(vehicleId, string.Empty, string.Empty,
            //                                E_CMD_TYPE.Move,
            //                                string.Empty,
            //                                toAdr, 0, 0);
            //Task.Run(() => { scApp.CMDBLL.generateCmd_OHTC_Details(); });
            sc.BLL.CMDBLL.OHTCCommandCheckResult check_result_info = null;

            await Task.Run(() =>
            {
                scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, string.Empty,
                                                cmd_type,
                                                string.Empty,
                                                toAdr, 0, 0);
                check_result_info = sc.BLL.CMDBLL.getCallContext<sc.BLL.CMDBLL.OHTCCommandCheckResult>
                                   (sc.BLL.CMDBLL.CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT);
            });
            if (check_result_info != null && !check_result_info.IsSuccess)
            {
                MessageBox.Show(check_result_info.ToString(), "Command create fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async void excuteLoadCommand()
        {
            string fromAdr = string.Empty;
            string vehicleId = string.Empty;
            string cmd_id = string.Empty;
            string cst_id = txt_cstID.Text;

            vehicleId = cmb_Vehicle.Text;
            fromAdr = cmb_fromAddress.Text;
            cmd_id = scApp.SequenceBLL.getCommandID(SCAppConstants.GenOHxCCommandType.Manual);
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }
            if (BCFUtility.isEmpty(cst_id))
            {
                MessageBox.Show("cst id can't empty.");
                return;
            }
            AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(vehicleId);
            sc.BLL.CMDBLL.OHTCCommandCheckResult check_result_info = null;
            await Task.Run(() =>
             {
                 //if (SCUtility.isMatche(vh.CUR_ADR_ID, fromAdr))
                 //{
                 //    scApp.VehicleService.TransferRequset(vehicleId, cmd_id, ActiveType.Load, "CST02", new string[0], new string[0], fromAdr, "");
                 //}
                 //else
                 {
                     //scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, "CST06",
                     scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, cst_id,
                                                      E_CMD_TYPE.Load,
                                                      fromAdr,
                                                      string.Empty, 0, 0);
                     check_result_info = sc.BLL.CMDBLL.getCallContext<sc.BLL.CMDBLL.OHTCCommandCheckResult>
                    (sc.BLL.CMDBLL.CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT);

                 }
             });
            if (check_result_info != null && !check_result_info.IsSuccess)
            {
                MessageBox.Show(check_result_info.ToString(), "Command create fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async void excuteUnloadCommand()
        {
            string toAdr = string.Empty;
            string vehicleId = string.Empty;
            string cmd_id = string.Empty;
            string cst_id = string.Empty;
            vehicleId = cmb_Vehicle.Text;
            toAdr = cmb_toAddress.Text;
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }
            AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(vehicleId);
            cmd_id = scApp.SequenceBLL.getCommandID(SCAppConstants.GenOHxCCommandType.Manual);
            //cst_id = SCUtility.Trim(vh.CST_ID, true);
            cst_id = SCUtility.Trim(txt_cstID.Text, true);

            sc.BLL.CMDBLL.OHTCCommandCheckResult check_result_info = null;

            await Task.Run(() =>
             {

                 //if (SCUtility.isMatche(vh.CUR_ADR_ID, toAdr))
                 //{
                 //    scApp.VehicleService.TransferRequset(vehicleId, cmd_id, ActiveType.Unload, cst_id, new string[0], new string[0], "", toAdr);
                 //}
                 //else
                 {

                     scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, cst_id,
                                                     E_CMD_TYPE.Unload,
                                                     string.Empty,
                                                     toAdr, 0, 0);
                     check_result_info = sc.BLL.CMDBLL.getCallContext<sc.BLL.CMDBLL.OHTCCommandCheckResult>
                    (sc.BLL.CMDBLL.CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT);
                 }
                 if (check_result_info != null && !check_result_info.IsSuccess)
                 {
                     MessageBox.Show(check_result_info.ToString(), "Command create fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 }

             });
        }


        private void excuteMoveCommandAllVh()
        {
            //string toAdr = string.Empty;
            List<AVEHICLE> lstVh = scApp.VehicleBLL.cache.loadVhs();
            foreach (AVEHICLE vh in lstVh)
            {
                if (SCUtility.isEmpty(vh.CUR_ADR_ID) || SCUtility.isEmpty(vh.CUR_SEC_ID))
                {
                    continue;
                }

                ASECTION sec = scApp.MapBLL.getSectiontByID(vh.CUR_SEC_ID);

                string vehicleId = vh.VEHICLE_ID.Trim();
                string toAdr = sec.TO_ADR_ID;
                string[] nextSections = scApp.MapBLL.loadNextSectionIDBySectionID(vh.CUR_SEC_ID);
                ASECTION nextSection = null;
                if (nextSections != null && nextSections.Count() > 0)
                    nextSection = scApp.MapBLL.getSectiontByID(nextSections[0]);

                if (BCFUtility.isEmpty(vehicleId))
                {
                    MessageBox.Show("No find vehile.");
                    return;
                }
                Task.Run(() =>
                scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, string.Empty,
                                                E_CMD_TYPE.Move,
                                                string.Empty,
                                                nextSection.TO_ADR_ID, 0, 0));
                SpinWait.SpinUntil(() => false, 1000);
            }
        }

        private void excuteTeachingCommand()
        {
            string vh_id = cmb_Vehicle.Text.Trim();
            string from_adr = cmb_fromAddress.Text;
            string to_adr = cmb_toAddress.Text;

            Task.Run(() => { scApp.VehicleService.TeachingRequest(vh_id, from_adr, to_adr); });

        }
        private void excuteHomeCommand()
        {
            string toAdr = string.Empty;
            string vehicleId = string.Empty;

            vehicleId = cmb_Vehicle.Text;
            toAdr = cmb_toAddress.Text;
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }

            AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(vehicleId);

            Task.Run(() =>
            scApp.VehicleService.TransferRequset(vehicleId, scApp.SequenceBLL.getCommandID(SCAppConstants.GenOHxCCommandType.Manual), ActiveType.Home, "", new string[0], new string[0], "", ""));
            //vh.sned_Str31(scApp.SequenceBLL.getCommandID_Manual(), ActiveType.Home, "", new string[0], new string[0], "", ""));
        }

        private void excuteMTLHomeCommand()
        {
            string toAdr = string.Empty;
            string vehicleId = string.Empty;

            vehicleId = cmb_Vehicle.Text;
            toAdr = cmb_toAddress.Text;
            if (BCFUtility.isEmpty(vehicleId))
            {
                MessageBox.Show("No find vehile.");
                return;
            }

            AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(vehicleId);

            Task.Run(() =>
            scApp.VehicleService.TransferRequset(vehicleId, scApp.SequenceBLL.getCommandID(SCAppConstants.GenOHxCCommandType.Manual), ActiveType.Mtlhome, "", new string[0], new string[0], "", ""));
            //vh.sned_Str31(scApp.SequenceBLL.getCommandID_Manual(), ActiveType.Home, "", new string[0], new string[0], "", ""));
        }
        //private string getAddressID(string adr_port_id)
        //{
        //    E_VH_TYPE vh_type = E_VH_TYPE.None;
        //    return getAddressID(adr_port_id, out vh_type);
        //}
        //private string getAddressID(string adr_port_id, out E_VH_TYPE vh_type)
        //{
        //    string Adr_ID = string.Empty;
        //    if (Raid_PortNameType_AdrID.Checked)
        //    {
        //        Adr_ID = adr_port_id;
        //        vh_type = E_VH_TYPE.None;
        //    }
        //    else if (Raid_PortNameType_PortID.Checked)
        //    {
        //        APORT port = scApp.MapBLL.getPortByPortID(adr_port_id);

        //        if (port != null)
        //        {
        //            Adr_ID = port.ADR_ID;
        //            vh_type = port.ULD_VH_TYPE;
        //        }
        //        else
        //        {
        //            vh_type = E_VH_TYPE.None;
        //        }
        //    }
        //    else
        //    {
        //        Adr_ID = adr_port_id;
        //        vh_type = E_VH_TYPE.None;
        //    }
        //    return Adr_ID;
        //}


        private void timer_TimedUpdates_Tick(object sender, EventArgs e)
        {
            //using (DBConnection_EF context = new DBConnection_EF())
            //{
            //    dgv_vhStatus.DataSource = context.AVEHICLE.ToList();
            //}
            //if (currentSelectIndex != -1)
            //    dgv_vhStatus.Rows[currentSelectIndex].Selected = true;
            dgv_vhStatus.Refresh();
            updateTransferCommand();
        }

        private async void updateTransferCommand()
        {
            List<ACMD_MCS> ACMD_MCSs = null;
            await Task.Run(() => ACMD_MCSs = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinished());
            cmd_mcs_obj_to_show = ACMD_MCSs.Select(cmd => new CMD_MCSObjToShow(mainform.BCApp.SCApplication.VehicleBLL, cmd)).ToList();
            //cmd_mcs_obj_to_show = mainform.BCApp.SCApplication.CMDBLL.loadACMD_MCSIsUnfinishedObjToShow();
            cmsMCS_bindingSource.DataSource = cmd_mcs_obj_to_show;
            dgv_TransferCommand.Refresh();
        }


        int currentSelectIndex = -1;
        //Equipment InObservationVh = null;
        AVEHICLE InObservationVh = null;
        string predictPathHandler = "predictPathHandler";
        private void dgv_vhStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentSelectIndex = e.RowIndex;
                string vh_id = dgv_vhStatus.Rows[currentSelectIndex].Cells[0].Value as string;

                setMonitorVehicle(vh_id);
            }
        }

        public void setMonitorVehicle(string vh_id)
        {
            lock (predictPathHandler)
            {
                if (InObservationVh != null)
                    InObservationVh.removeEventHandler(predictPathHandler);

                resetSpecifyRail();
                resetSpecifyAdr();
                if (!BCFUtility.isEmpty(vh_id))
                {
                    InObservationVh = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);

                    changePredictPathByInObservation();

                    InObservationVh.addEventHandler(predictPathHandler
                                        , BCFUtility.getPropertyName(() => InObservationVh.VhExcuteCMDStatusChangeEvent)
                                        , (s1, e1) => { changePredictPathByInObservation(); });
                    InObservationVh.addEventHandler(predictPathHandler
                                        , BCFUtility.getPropertyName(() => InObservationVh.VhStatusChangeEvent)
                                        , (s1, e1) => { changePredictPathByInObservation(); });

                    cmb_Vehicle.Text = vh_id;
                    VehicleObjToShow veicleObjShow = scApp.getEQObjCacheManager().CommonInfo.ObjectToShow_list.Where(o => o.VEHICLE_ID == vh_id).FirstOrDefault();
                    if (veicleObjShow != null)
                    {
                        int selectIndex = scApp.getEQObjCacheManager().CommonInfo.ObjectToShow_list.IndexOf(veicleObjShow);
                        if (selectIndex >= 0)
                        {
                            if ((dgv_vhStatus.SelectedRows.Count > 0 && dgv_vhStatus.SelectedRows[0].Index != selectIndex) ||
                                dgv_vhStatus.SelectedRows.Count == 0)
                            {
                                dgv_vhStatus.Rows[selectIndex].Selected = true;
                                dgv_vhStatus.FirstDisplayedScrollingRowIndex = selectIndex;
                            }
                        }
                    }
                }
                else
                {
                    if (dgv_vhStatus.SelectedRows.Count > 0)
                        dgv_vhStatus.SelectedRows[0].Selected = false;
                    cmb_Vehicle.Text = string.Empty;

                }
            }
        }

        bool setCombBoxFlag = false;
        public void setAdrCombobox(string adr_id)
        {
            if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, adr_id))
            {
                MessageBox.Show("Can't set maintain device range of address", "Set fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (setCombBoxFlag)
            {
                cmb_fromAddress.Text = adr_id;
                setCombBoxFlag = false;
            }
            else
            {
                cmb_toAddress.Text = adr_id;
                setCombBoxFlag = true;
            }
        }


        public void entryMonitorMode()
        {

        }
        public void LeaveMonitorMode()
        {

        }

        public async void entryMonitorMode_SectionThroughTimesAsync()
        {

        }
        private string int2colorTranfer(int passTimes)
        {
            double Denominator = 5000;
            double percentage = (passTimes / Denominator) * 100;
            double radPercen = percentage;
            double greenPercen = 100 - percentage;
            double radColorScale = 255 * (radPercen / 100);
            double greenColorScale = 255 * (greenPercen / 100);
            string sRadColorScale = Convert.ToString((int)radColorScale, 16);
            string sGreenColorScale = Convert.ToString((int)greenColorScale, 16).ToUpper();
            string colorCoding = string.Format("#{0}{1}00", sRadColorScale, sGreenColorScale);
            return colorCoding;
        }

        public void LeaveMonitorMode_SectionThroughTimes()
        {
        }

        public void entrySegmentSetMode(EventHandler eventHandler)
        {
        }
        public void LeaveSegmentSetMode(EventHandler eventHandler)
        {
        }
        public void SetSpecifySegmentSelected(string seg_num, Color set_color)
        {
        }
        public void ResetSpecifySegmentSelected(string seg_num)
        {
        }

        public void ResetAllSegment()
        {
        }

        public void ResetAllSelectedSegment()
        {
        }


        private void changePredictPathByInObservation()
        {
            //resetSpecifyRail();
            //resetSpecifyAdr();

            if (InObservationVh.ACT_STATUS == VHActionStatus.CycleRun)
            {
                resetSpecifyRail();
                resetSpecifyAdr();
                setSpecifyRail(InObservationVh.CyclingPath);
            }
            else
            {
                if (InObservationVh.vh_CMD_Status < E_CMD_STATUS.NormalEnd)
                {
                    setSpecifyRail(InObservationVh.PredictPath);
                    setSpecifyAdr();
                }
                else
                {
                    resetSpecifyRail();
                    resetSpecifyAdr();
                }
            }
        }

        string reqSelectionStartAdr = string.Empty;
        string reqSelectionFromAdr = string.Empty;
        string reqSelectionToAdr = string.Empty;
        private void setSpecifyAdr()
        {

        }
        private void resetSpecifyAdr()
        {

        }
        string[] preSelectionSec = null;
        private void setSpecifyRail(string[] spacifyPath)
        {

        }
        private void resetSpecifyRail()
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void dgv_vhStatus_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            logger.Error(e.Exception, "Exception");
            //todo error catch
        }

        private void btn_continuous_Click(object sender, EventArgs e)
        {
            //Equipment noticeCar = scApp.getEQObjCacheManager().getEquipmentByEQPTID(cmb_Vehicle.Text.Trim());
            string vh_id = cmb_Vehicle.Text.Trim();
            Task.Run(() =>
            {
                AVEHICLE noticeCar = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);

                if (noticeCar.IsPause)
                {
                    Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(OHT_Form), Device: "OHTC",
                      Data: $"Send manual continuous to vh;{vh_id}");
                    scApp.VehicleService.PauseRequest(vh_id, PauseEvent.Continue, SCAppConstants.OHxCPauseType.Normal);
                    //noticeCar.sned_Str39(PauseEvent.Continue, PauseType.OhxC);
                }
                else
                {
                    //scApp.VehicleBLL.noticeVhPass(vh_id);
                    Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(OHT_Form), Device: "OHTC",
                      Data: $"Send manual continuous(block release) to vh;{vh_id}");
                    scApp.VehicleService.noticeVhPass(vh_id);
                }
            });
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            //Equipment noticeCar = scApp.getEQObjCacheManager().getEquipmentByEQPTID(cmb_Vehicle.Text.Trim());
            //AVEHICLE noticeCar = scApp.getEQObjCacheManager().getVehicletByVHID(cmb_Vehicle.Text.Trim());
            string notice_vh_id = cmb_Vehicle.Text.Trim();

            Task.Run(() =>
            {
                //if (noticeCar.sned_Str39(PauseEvent.Pause, PauseType.OhxC))
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(OHT_Form), Device: "OHTC",
                  Data: $"Send manual pause to vh;{notice_vh_id}");

                if (scApp.VehicleService.PauseRequest(notice_vh_id, PauseEvent.Pause, SCAppConstants.OHxCPauseType.Normal))
                {

                }
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mainform.isAutoOpenTip = cb_autoTip.Checked;
        }

        private void cbm_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_fromAddress.Enabled = false;
            cmb_toAddress.Visible = false;
            cmb_cycRunZone.Visible = false;
            btn_start.Enabled = false;
            dgv_TaskCommand.Enabled = false;
            btn_AutoMove.Enabled = false;
            lbl_destinationName.Text = "To Address";
            E_CMD_TYPE cmd_type;
            Enum.TryParse<E_CMD_TYPE>(cbm_Action.SelectedValue.ToString(), out cmd_type);
            switch (cmd_type)
            {
                case E_CMD_TYPE.Move:
                case E_CMD_TYPE.MoveToMTL:
                case E_CMD_TYPE.SystemIn:
                case E_CMD_TYPE.SystemOut:
                    cmb_toAddress.Visible = true;
                    btn_start.Enabled = true;
                    dgv_TaskCommand.Enabled = true;
                    btn_AutoMove.Enabled = true;
                    break;
                case E_CMD_TYPE.Round:
                    cmb_cycRunZone.Visible = true;
                    btn_start.Enabled = true;
                    lbl_destinationName.Text = "Round Entry Adr.";
                    break;
                case E_CMD_TYPE.LoadUnload:
                    cmb_fromAddress.Enabled = true;
                    cmb_toAddress.Visible = true;
                    btn_start.Enabled = true;
                    dgv_TaskCommand.Enabled = true;
                    break;
                case E_CMD_TYPE.Teaching:
                    cmb_fromAddress.Enabled = true;
                    cmb_toAddress.Visible = true;
                    btn_start.Enabled = true;
                    break;
                case E_CMD_TYPE.Home:
                case E_CMD_TYPE.MTLHome:
                    btn_start.Enabled = true;
                    break;
                case E_CMD_TYPE.Load:
                    cmb_fromAddress.Enabled = true;
                    btn_start.Enabled = true;
                    break;
                case E_CMD_TYPE.Unload:
                    cmb_toAddress.Visible = true;
                    btn_start.Enabled = true;
                    break;


            }
        }

        private void Raid_PortNameType_CheckedChanged(object sender, EventArgs e)
        {
            string source_name = string.Empty;
            string destination_name = string.Empty;
            if (Raid_PortNameType_AdrID.Checked)
            {
                source_name = "From Address";
                destination_name = "To Address";
                BCUtility.setComboboxDataSource(cmb_toAddress, allAdr_ID);
                BCUtility.setComboboxDataSource(cmb_fromAddress, allAdr_ID.ToArray());
            }
            else if (Raid_PortNameType_PortID.Checked)
            {
                source_name = "From Port";
                destination_name = "To Port";
                BCUtility.setComboboxDataSource(cmb_toAddress, allPortID);
                BCUtility.setComboboxDataSource(cmb_fromAddress, allPortID.ToArray());
            }
            lbl_sourceName.Text = source_name;
            lbl_destinationName.Text = destination_name;
        }

        private void ck_montor_vh_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btn_AutoMove_Click(object sender, EventArgs e)
        {
            Task.Run(() => excuteMoveCommandAllVh());
            //            string vehicleId = cmb_Vehicle.Text;

            //            Task.Run(() =>
            //scApp.VehicleService.TransferRequset(vehicleId, scApp.SequenceBLL.getCommandID(SCAppConstants.GenOHxCCommandType.Auto), ActiveType.Move, "", new string[] { "0402", "0412" }, new string[0], "", ""));

        }

        private void cb_sectionThroughTimes_Click(object sender, EventArgs e)
        {
            if (cb_sectionThroughTimes.Checked)
            {
                entryMonitorMode_SectionThroughTimesAsync();
            }
            else
            {
                LeaveMonitorMode_SectionThroughTimes();
            }

        }


        private void OHT_Form_Load(object sender, EventArgs e)
        {
            ck_montor_vh.Checked = true;
            dgv_TransferCommand.DataSource = cmsMCS_bindingSource;
        }

        private void btn_st1_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_Vehicle.Text.Trim();
            string mt_adr_id = "12299";
            Task.Run(() =>
            {
                //AVEHICLE noticeCar = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);
                //noticeCar.sned_S1();
                //scApp.VehicleService.HostBasicVersionReport(vh_id);
                scApp.VehicleService.doReservationVhToMaintainsBufferAddress(vh_id, mt_adr_id);
            });
        }

        private void cb_sectionThroughTimes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_parkZoneTypeChange_Click(object sender, EventArgs e)
        {
            string selected_park_zone_type = cb_parkZoneType.SelectedItem as string;
            if (selected_park_zone_type == null) return;
            scApp.ParkBLL.doParkZoneTypeChange(selected_park_zone_type);
            MessageBox.Show("OK");
        }

        private void btn_recover_to_autoremote_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_Vehicle.Text.Trim();
            Task.Run(() =>
            {
                scApp.VehicleService.doRecoverModeStatusToAutoRemote(vh_id);
            });
        }

        private void uctl_Map_Load(object sender, EventArgs e)
        {

        }

        private void cmb_Vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vh_id = cmb_Vehicle.Text;
            if (SCUtility.isEmpty(vh_id)) return;
            AVEHICLE vh = scApp.VehicleBLL.cache.getVhByID(vh_id);
            if (vh == null) return;
            txt_cstID.Text = vh.HAS_CST == 1 ? SCUtility.Trim(vh.CST_ID, true) : "Manual_CST";
        }

        private void RefreshMapColor()
        {
            List<ASEGMENT> segment_List = null;
            segment_List = scApp.SegmentBLL.cache.GetSegments();
            foreach (ASEGMENT seg in segment_List)
            {
                int index = segment_List.IndexOf(seg);

                if (seg.PRE_DISABLE_FLAG)
                {
                    this.SetSpecifySegmentSelected(seg.SEG_NUM, Color.Pink);
                }
                else if (seg.STATUS == E_SEG_STATUS.Closed)
                {
                    this.SetSpecifySegmentSelected(seg.SEG_NUM, Color.Red);
                }
                else
                {
                    this.ResetSpecifySegmentSelected(seg.SEG_NUM);
                }
            }
        }
    }
}