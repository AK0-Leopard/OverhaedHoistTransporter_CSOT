//*********************************************************************************
//      OHT_Form.cs
//*********************************************************************************
// File Name: OHT_Form.cs
// Description: OHT Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.ohxc.winform.UI.Components;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;
using OverhaedxControl_WindownForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class OHT_Form : Form
    {
        #region 公用參數設定
        static Color CLR_MAP_ADDRESS_DUFAULT = Color.Gainsboro;
        static Color CLR_MAP_ADDRESS_START = Color.Violet;
        static Color CLR_MAP_ADDRESS_FROM = Color.Lime;
        static Color CLR_MAP_ADDRESS_TO = Color.Red;

        uctlHistoricalReplyPlayer uctlHistoricalReplyPlayer;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        MainForm mainform = null;
        public WindownApplication app { get; private set; } = null;
        string[] allAdr_ID = null;
        string[] allPortID = null;

        List<ALARM> aLARMs = new List<ALARM>();
        #endregion 公用參數設定

        public OHT_Form(MainForm _form)
        {
            try
            {
                InitializeComponent();
                mainform = _form;
                app = mainform.app;
                uctl_Map.start(app);
                uctl_Dashboard1.start(app);
                initialDataGreadView();
                vehicleObjToShowBindingSource.DataSource = app.ObjCacheManager.GetVehicleObjToShows();
                dgv_vhStatus.DataSource = vehicleObjToShowBindingSource;

                uctl_Map.BackColor = Color.FromArgb(29, 36, 60);
                uctl_Dashboard1.BackColor = Color.FromArgb(29, 36, 60);

                initialUI();
                initialEvent();

                timer_refreshVhInfo.Start();

                switch (WindownApplication.OHxCFormMode)
                {
                    case OHxCFormMode.CurrentPlayer:
                        initialComBox();
                        break;
                    case OHxCFormMode.HistoricalPlayer:
                        tableLayoutPanel1.Controls.Remove(grp_ControlTable);
                        uctlHistoricalReplyPlayer = new uctlHistoricalReplyPlayer();
                        uctlHistoricalReplyPlayer.Dock = DockStyle.Fill;
                        tableLayoutPanel1.Controls.Add(uctlHistoricalReplyPlayer, 1, 1);
                        uctlHistoricalReplyPlayer.Start();
                        //tableLayoutPanel1.
                        tabControl1.TabPages.RemoveAt(1);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initialUI()
        {
            try
            {
                ALINE line = app.ObjCacheManager.GetLine();
                lbl_hostconn.BackColor =
                line.Secs_Link_Stat == SCAppConstants.LinkStatus.LinkOK ? Color.Green : Color.Gray;
                lbl_RediStat.BackColor =
                line.Redis_Link_Stat == SCAppConstants.LinkStatus.LinkOK ? Color.Green : Color.Gray;
                lbl_earthqualeHappend.BackColor =
                line.IsEarthquakeHappend ? Color.Red : Color.Gray;
                lbl_detectionSystemExist.BackColor =
                line.DetectionSystemExist == SCAppConstants.ExistStatus.Exist ? Color.Green : Color.Gray;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initialEvent()
        {
            try
            {
                ALINE line = app.ObjCacheManager.GetLine();

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
                app.CurrentAlarmChange += CurrentAlarmChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void CurrentAlarmChange(object obj, List<sc.ALARM> alarms)
        {
            try
            {
                Adapter.Invoke((o) =>
                {
                    dgv_Alarm.DataSource = alarms;
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void adjustmentDataGridViewWeight()
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        void Local_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initialComBox()
        {
            try
            {
                List<AADDRESS> allAddress_obj = app.MapBLL.loadAllAddress();
                List<string> allAdr = new List<string>() { "" };
                allAdr.AddRange(allAddress_obj.Select(adr => adr.ADR_ID).ToList());
                allAdr_ID = allAdr.ToArray();
                allPortID = app.MapBLL.loadAllPort().Select(s => s.PORT_ID).ToArray();
                WinFromUtility.setComboboxDataSource(cmb_toAddress, allAdr_ID);
                WinFromUtility.setComboboxDataSource(cmb_fromAddress, allAdr_ID.ToArray());


                //string[] allCycleRunZone = app.CycleBLL.loadCycleRunMasterByCurrentCycleTypeID
                //    (scApp.getEQObjCacheManager().getLine().Currnet_Cycle_Type).Select(master => master.ENTRY_ADR_ID).ToArray();
                //cmb_cycRunZone.DataSource = allCycleRunZone;


                string[] allSec = app.MapBLL.loadAllSectionID().ToArray();
                WinFromUtility.setComboboxDataSource(cmb_fromSection, allSec);

                List<string> lstVh = new List<string>() { "" };
                lstVh.AddRange(uctl_Map.m_objItemNewVhcl.Select(vh => vh.ID).ToList());
                string[] allVh = lstVh.ToArray();
                WinFromUtility.setComboboxDataSource(cmb_Vehicle, allVh);


                cbm_Action.DataSource = Enum.GetValues(typeof(E_CMD_TYPE)).Cast<E_CMD_TYPE>()
                                                      .Where(e => e != E_CMD_TYPE.Move_MTPort &&
                                                                  e != E_CMD_TYPE.Move_Park).ToList();


                //List<string> park_zone_type = scApp.ParkBLL.loadAllParkZoneType();
                //cb_parkZoneType.DataSource = park_zone_type;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void initialDataGreadView()
        {
            try
            {
                aLARMs.Add(new ALARM());
                dgv_Alarm.AutoGenerateColumns = false;
                dgv_Alarm.DataSource = aLARMs;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private async void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                btn_start.Enabled = false;
                E_CMD_TYPE cmd_type;
                Enum.TryParse<E_CMD_TYPE>(cbm_Action.SelectedValue.ToString(), out cmd_type);
                bool isSuccess = false;
                string from_adr = cmb_fromAddress.Text;
                string to_adr = cmb_toAddress.Text;
                string vehicleId = cmb_Vehicle.Text;
                switch (cmd_type)
                {
                    case E_CMD_TYPE.Move:
                    case E_CMD_TYPE.Load:
                    case E_CMD_TYPE.Unload:
                    case E_CMD_TYPE.LoadUnload:
                    case E_CMD_TYPE.Home:
                    case E_CMD_TYPE.Teaching:
                        await Task.Run(() => isSuccess = app.VehicleBLL.SendCmdToControl(vehicleId,
                                                          cmd_type,
                                                          from_adr,
                                                          to_adr));
                        break;
                }

                MessageBox.Show(isSuccess ? "OK" : "NG");
                btn_start.Enabled = true;

                //switch (cmd_type)
                //{
                //    case E_CMD_TYPE.Move:
                //        excuteMoveCommand();
                //        break;
                //    //case E_CMD_TYPE.Round:
                //    //    excuteCycleRunCommand();
                //    //    break;
                //    case E_CMD_TYPE.LoadUnload:
                //        excuteLoadUnloadCommand();
                //        break;
                //    case E_CMD_TYPE.Teaching:
                //        excuteTeachingCommand();
                //        break;
                //    case E_CMD_TYPE.Home:
                //        excuteHomeCommand();
                //        break;
                //    case E_CMD_TYPE.Load:
                //        excuteLoadCommand();
                //        break;
                //    case E_CMD_TYPE.Unload:
                //        excuteUnloadCommand();
                //        break;
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void excuteLoadUnloadCommand()
        {
            try
            {
                string from_adr = cmb_fromAddress.Text;
                string to_adr = cmb_toAddress.Text;
                string vehicleId = cmb_Vehicle.Text;

                Task.Run(() => app.VehicleBLL.SendCmdToControl(vehicleId,
                                                  E_CMD_TYPE.LoadUnload,
                                                  from_adr,
                                                  to_adr));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void excuteCycleRunCommand()
        //{
        //    string toAdr = string.Empty;
        //    string vehicleId = string.Empty;

        //    vehicleId = cmb_Vehicle.Text;
        //    toAdr = cmb_cycRunZone.Text;
        //    if (BCFUtility.isEmpty(vehicleId))
        //    {
        //        MessageBox.Show("No find vehile.");
        //        return;
        //    }

        //    Task.Run(() =>
        //    scApp.CMDBLL.doCreatTransferCommand(vehicleId, string.Empty, string.Empty,
        //                                    E_CMD_TYPE.Round,
        //                                    string.Empty,
        //                                    toAdr, 0, 0));
        //}


        int currentSelectIndex = -1;
        //Equipment InObservationVh = null;
        AVEHICLE InObservationVh = null;
        string predictPathHandler = "predictPathHandler";
        private void dgv_vhStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                currentSelectIndex = e.RowIndex;
                string vh_id = dgv_vhStatus.Rows[currentSelectIndex].Cells[0].Value as string;

                setMonitorVehicle(vh_id);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }

        public void setMonitorVehicle(string vh_id)
        {
            try
            {
                lock (predictPathHandler)
                {
                    if (InObservationVh != null)
                        InObservationVh.removeEventHandler(predictPathHandler);

                    resetSpecifyRail();
                    resetSpecifyAdr();
                    if (!BCFUtility.isEmpty(vh_id))
                    {

                        InObservationVh = app.ObjCacheManager.GetVEHICLE(vh_id);

                        changePredictPathByInObservation();

                        InObservationVh.addEventHandler(predictPathHandler
                                            , BCFUtility.getPropertyName(() => InObservationVh.VhExcuteCMDStatusChangeEvent)
                                            , (s1, e1) => { changePredictPathByInObservation(); });
                        InObservationVh.addEventHandler(predictPathHandler
                                            , BCFUtility.getPropertyName(() => InObservationVh.VhStatusChangeEvent)
                                            , (s1, e1) => { changePredictPathByInObservation(); });

                        cmb_Vehicle.Text = vh_id;
                        IndicateVhInfoFromDgv(vh_id);
                    }
                    else
                    {
                        if (dgv_vhStatus.SelectedRows.Count > 0)
                            dgv_vhStatus.SelectedRows[0].Selected = false;
                        cmb_Vehicle.Text = string.Empty;

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void IndicateVhInfoFromDgv(string vh_id)
        {
            try
            {
                var veicleObjShow = app.ObjCacheManager.GetVehicleObjToShows().Where(o => o.VEHICLE_ID == vh_id).FirstOrDefault();
                if (veicleObjShow != null)
                {
                    int selectIndex = app.ObjCacheManager.GetVehicleObjToShows().IndexOf(veicleObjShow);
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        bool setCombBoxFlag = false;
        public void setAdrCombobox(string adr_id)
        {
            try
            {
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public void entryMonitorMode()
        {
            //setMonitorVehicle(string.Empty);
            //Dictionary<string, double> dicSecIDAndAvgPingTime = scApp.NetWorkQualityBLL.loadGroupBySecIDAndAvgPingTime();
            //foreach (KeyValuePair<string, double> keyValue in dicSecIDAndAvgPingTime)
            //{
            //    if (keyValue.Value > 100)
            //    {
            //        uctl_Map.changeSpecifyRailColor(keyValue.Key, Color.Red);
            //    }
            //    else if (keyValue.Value > 60)
            //    {
            //        uctl_Map.changeSpecifyRailColor(keyValue.Key, Color.Orange);
            //    }
            //    else
            //    {
            //        uctl_Map.changeSpecifyRailColor(keyValue.Key, Color.LightGreen);
            //    }
            //}
            //uctl_Map.DisplaySectionLables(true);
            //preSelectionSec = dicSecIDAndAvgPingTime.Keys.ToArray();
        }
        public void LeaveMonitorMode()
        {
            try
            {
                resetSpecifyRail();
                uctl_Map.DisplaySectionLables(false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public async void entryMonitorMode_SectionThroughTimesAsync()
        {
            //setMonitorVehicle(string.Empty);
            //Dictionary<string, int> dicSecIDAndThroughTimes = null;
            //await Task.Run(() => dicSecIDAndThroughTimes = scApp.MapBLL.loadGroupBySecAndThroughTimes());
            ////string color_Rad = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            ////string color_Y = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            //int totalHour = 24;
            //ck_montor_vh.Checked = false;
            //uctl_Map.entryMonitorMode();
            //foreach (KeyValuePair<string, int> keyValue in dicSecIDAndThroughTimes)
            //{
            //    //int2colorTranfer(keyValue.Value);

            //    double perHourPassTimes = keyValue.Value;
            //    Color changeColor = Color.Empty;
            //    if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV9)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV10;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV8)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV9;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV7)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV8;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV6)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV7;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV5)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV6;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV4)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV5;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV3)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV4;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV2)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV3;
            //    }
            //    else if (perHourPassTimes > BCAppConstants.SEC_THROUGH_TIMES_LV1)
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV2;
            //    }
            //    else
            //    {
            //        changeColor = BCAppConstants.SEC_THROUGH_COLOR_LV1;
            //    }
            //    uctl_Map.changeSpecifyRailColor(keyValue.Key, changeColor);

            //}
            //preSelectionSec = dicSecIDAndThroughTimes.Keys.ToArray();
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
            try
            {
                ck_montor_vh.Checked = true;
                resetSpecifyRail();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void entrySegmentSetMode(EventHandler eventHandler)
        {
            try
            {
                setMonitorVehicle(string.Empty);
                uctl_Map.RegistRailSelectedEvent(eventHandler);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void LeaveSegmentSetMode(EventHandler eventHandler)
        {
            try
            {
                uctl_Map.RemoveRailSelectedEvent(eventHandler);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetSpecifySegmentSelected(string seg_num, Color set_color)
        {
            try
            {
                uctl_Map.changeSpecifyRailColorBySegNum(seg_num, set_color);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void ResetSpecifySegmentSelected(string seg_num)
        {
            try
            {
                uctl_Map.resetRailColor(seg_num);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void ResetAllSegment()
        {
            try
            {
                uctl_Map.resetAllRailColor();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void changePredictPathByInObservation()
        {
            try
            {
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        string reqSelectionStartAdr = string.Empty;
        string reqSelectionFromAdr = string.Empty;
        string reqSelectionToAdr = string.Empty;
        private void setSpecifyAdr()
        {
            try
            {
                if (!BCFUtility.isEmpty(InObservationVh.startAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (InObservationVh.startAdr, CLR_MAP_ADDRESS_START);
                    reqSelectionStartAdr = InObservationVh.startAdr;
                }
                if (!BCFUtility.isEmpty(InObservationVh.FromAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (InObservationVh.FromAdr, CLR_MAP_ADDRESS_FROM);
                    reqSelectionFromAdr = InObservationVh.FromAdr;

                }
                if (!BCFUtility.isEmpty(InObservationVh.ToAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (InObservationVh.ToAdr, CLR_MAP_ADDRESS_TO);
                    reqSelectionToAdr = InObservationVh.ToAdr;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void resetSpecifyAdr()
        {
            try
            {
                if (!BCFUtility.isEmpty(reqSelectionStartAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (reqSelectionStartAdr, CLR_MAP_ADDRESS_DUFAULT);
                    reqSelectionStartAdr = string.Empty;
                }
                if (!BCFUtility.isEmpty(reqSelectionFromAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (reqSelectionFromAdr, CLR_MAP_ADDRESS_DUFAULT);
                    reqSelectionFromAdr = string.Empty;

                }
                if (!BCFUtility.isEmpty(reqSelectionToAdr))
                {
                    uctl_Map.changeSpecifyAddressColor
                        (reqSelectionToAdr, CLR_MAP_ADDRESS_DUFAULT);
                    reqSelectionToAdr = string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        string[] preSelectionSec = null;
        private void setSpecifyRail(string[] spacifyPath)
        {
            try
            {
                if (spacifyPath == null)
                {
                    return;
                }

                if (BCFUtility.isMatche(preSelectionSec, spacifyPath))
                {
                    return;
                }

                preSelectionSec = spacifyPath;
                uctl_Map.changeSpecifyRailColor(spacifyPath);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void resetSpecifyRail()
        {
            try
            {
                if (preSelectionSec != null)
                {
                    uctl_Map.resetRailColor(preSelectionSec);
                }

                preSelectionSec = null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
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
            try
            {
                logger.Error(e.Exception, "Exception");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_continuous_Click(object sender, EventArgs e)
        {
            try
            {
                btn_continuous.Enabled = false;
                string vh_id = cmb_Vehicle.Text.Trim();
                bool isSuccess = false;
                await Task.Run(() => isSuccess = app.VehicleBLL.SendPaserToControl(vh_id, PauseEvent.Continue));

                MessageBox.Show(isSuccess ? "OK" : "NG");
                btn_continuous.Enabled = true;

                //Equipment noticeCar = scApp.getEQObjCacheManager().getEquipmentByEQPTID(cmb_Vehicle.Text.Trim());
                //string vh_id = cmb_Vehicle.Text.Trim();
                //Task.Run(() =>
                //{
                //    AVEHICLE noticeCar = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);

                //    if (noticeCar.IsPause)
                //    {
                //        scApp.VehicleService.PauseRequest(vh_id, PauseEvent.Continue, PauseType.OhxC);
                //        //noticeCar.sned_Str39(PauseEvent.Continue, PauseType.OhxC);
                //    }
                //    else
                //    {
                //        scApp.VehicleBLL.noticeVhPass(vh_id);
                //    }
                //});
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_pause_Click(object sender, EventArgs e)
        {
            try
            {
                btn_pause.Enabled = false;
                string vh_id = cmb_Vehicle.Text.Trim();
                bool isSuccess = false;
                await Task.Run(() => isSuccess = app.VehicleBLL.SendPaserToControl(vh_id, PauseEvent.Pause));

                MessageBox.Show(isSuccess ? "OK" : "NG");
                btn_pause.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mainform.isAutoOpenTip = cb_autoTip.Checked;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void cbm_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmb_fromAddress.SelectedIndex = 0;
                cmb_toAddress.SelectedIndex = 0;
                // cmb_cycRunZone.SelectedIndex = 0;

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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Raid_PortNameType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string source_name = string.Empty;
                string destination_name = string.Empty;
                if (Raid_PortNameType_AdrID.Checked)
                {
                    source_name = "From Address";
                    destination_name = "To Address";
                    WinFromUtility.setComboboxDataSource(cmb_toAddress, allAdr_ID);
                    WinFromUtility.setComboboxDataSource(cmb_fromAddress, allAdr_ID.ToArray());
                }
                else if (Raid_PortNameType_PortID.Checked)
                {
                    source_name = "From Port";
                    destination_name = "To Port";
                    WinFromUtility.setComboboxDataSource(cmb_toAddress, allPortID);
                    WinFromUtility.setComboboxDataSource(cmb_fromAddress, allPortID.ToArray());
                }
                lbl_sourceName.Text = source_name;
                lbl_destinationName.Text = destination_name;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ck_montor_vh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ck_montor_vh.Checked)
                {
                    uctl_Map.trunOnMonitorAllVhStatus();
                }
                else
                {
                    uctl_Map.trunOffMonitorAllVhStatus();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_AutoMove_Click(object sender, EventArgs e)
        {
            try
            {
                //Task.Run(() => excuteMoveCommandAllVh());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void cb_sectionThroughTimes_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void OHT_Form_Load(object sender, EventArgs e)
        {
            try
            {
                ck_montor_vh.Checked = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_st1_Click(object sender, EventArgs e)
        {
            try
            {
                //string vh_id = cmb_Vehicle.Text.Trim();
                //string mt_adr_id = "12299";
                //Task.Run(() =>
                //{
                //    //AVEHICLE noticeCar = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);
                //    //noticeCar.sned_S1();
                //    //scApp.VehicleService.HostBasicVersionReport(vh_id);
                //    scApp.VehicleService.doReservationVhToMaintainsPort(vh_id, mt_adr_id);
                //});
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void cb_sectionThroughTimes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_parkZoneTypeChange_Click(object sender, EventArgs e)
        {
            try
            {
                //string selected_park_zone_type = cb_parkZoneType.SelectedItem as string;
                //if (selected_park_zone_type == null) return;
                //scApp.ParkBLL.doParkZoneTypeChange(selected_park_zone_type);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_recover_to_autoremote_Click(object sender, EventArgs e)
        {
            try
            {
                //string vh_id = cmb_Vehicle.Text.Trim();
                //Task.Run(() =>
                //{
                //    scApp.VehicleService.doRecoverModeStatusToAutoRemote(vh_id);
                //});
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uctl_Map_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void timer_refreshVhInfo_Tick(object sender, EventArgs e)
        {
            try
            {
                dgv_vhStatus.Refresh();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Image image = null;
                //await Task.Run(() => image = app.LineBLL.getSysExcuteQualityImaget("CMDQueueTime.png", "2018-11-01T00:00:00Z", "2018-11-02T00:00:00Z"));
                //pic_Cmd_Queue_Time.Image = image;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() => app.GetSysExcuteQualityQueryService().UpdateSysEfficiencyComparisonBaseTable_parallel());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}