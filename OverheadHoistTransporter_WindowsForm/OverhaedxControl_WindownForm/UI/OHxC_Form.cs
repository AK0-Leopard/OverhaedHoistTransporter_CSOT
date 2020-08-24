//*********************************************************************************
//      OHxC_Form.cs
//*********************************************************************************
// File Name: OHxC_Form.cs
// Description: OHxC Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/06           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.ohxc.winform.UI;
using com.mirle.ibg3k0.ohxc.winform.UI.Operate;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.ibg3k0.Utility.ul.Data;
using com.mirle.ibg3k0.Utility.ul.Data.VO;
using NLog;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class OHxC_Form : Form
    {
        #region 公用參數設定
        //uctlHistoricalReplyPlayer uctlHistoricalReplyPlayer;
        Dictionary<String, Form> openForms = new Dictionary<string, Form>();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        int Maxlimit_MessageContent = 700;
        int Minlimit_MessageContent = 35;
        int eachMove_MessageContent = 60;

        int Maxlimit_ToolBarContent = 180;
        int Minlimit_ToolBarContent = 81;
        //int eachMove_ToolBarContent = 10;
        OHxCMainForm mainForm;
        public WindownApplication app { get; private set; } = null;
        static Color CLR_MAP_ADDRESS_DUFAULT = Color.Gainsboro;
        static Color CLR_MAP_ADDRESS_START = Color.Violet;
        static Color CLR_MAP_ADDRESS_FROM = Color.Lime;
        static Color CLR_MAP_ADDRESS_TO = Color.Red;

        private readonly static int MAX_LOG_MSG_COUNT = 50;
        List<SYSTEMPROCESS_INFO> systemProcLst = new List<SYSTEMPROCESS_INFO>();
        LogUtility logUtility = null;
        //BindingSource TransCMD_BindingSource = new BindingSource();
        #endregion 公用參數設定

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="_mainForm"></param>
        public OHxC_Form(OHxCMainForm _form)
        {
            try
            {
                InitializeComponent();
                //initFuncKey();
                setFuncKeyClick();
                mainForm = _form;
                app = mainForm.app;
                uctl_Map.start(app);

                //vehicleObjToShowBindingSource.DataSource = app.ObjCacheManager.GetVehicleObjToShows();
                //uc_grid_VHStatus1.grid_VH_Status.ItemsSource = vehicleObjToShowBindingSource;
                uc_grid_VHStatus1.grid_VH_Status.ItemsSource = app.ObjCacheManager.GetVehicleObjToShows();
                uc_grid_VHStatus1.grid_VH_Status.PreviewMouseUp += grid_VH_Status_cell_click;

                uctl_Map.BackColor = Color.FromArgb(9, 0, 45);

                mainForm = _form;
                switch (WindownApplication.OHxCFormMode)
                {
                    case OHxCFormMode.HistoricalPlayer:
                        //this.splitContainer2.Panel1.Controls.Remove(tableLayoutPanel4);

                        //tableLayoutPanel1.Controls.Remove(tableLayoutPanel4);
                        //uctlHistoricalReplyPlayer = new uctlHistoricalReplyPlayer();
                        //uctlHistoricalReplyPlayer.Dock = DockStyle.Fill;

                        //this.splitContainer2.Panel1.Controls.Add(uctlHistoricalReplyPlayer);
                        //tableLayoutPanel1.Controls.Add(uctlHistoricalReplyPlayer, 0, 0);
                        //uctlHistoricalReplyPlayer.Start();
                        //uctlHistoricalReplyPlayer.FocusVehicle += uctl_Map.FocusVehicleProcess;
                        uctlHistoricalReplyPlayer1.Start();
                        uctlHistoricalReplyPlayer1.FocusVehicle += uctl_Map.FocusVehicleProcess;
                        uctlHistoricalReplyPlayer1.Visible = true;
                        //tableLayoutPanel1.
                        //tabControl1.TabPages.RemoveAt(1);
                        break;
                }

                foreach (var vh in uctl_Map.m_objItemNewVhcl)
                {
                    vh.VehicleBeChosen += setMonitorVehicle;
                }
                uctl_Map.MapDoubleClick += setMonitorVehicle;
                //TransCMD_BindingSource.DataSource = app.ObjCacheManager.GetMCS_CMD();
                //uc_grid_TransCMD1.grid_MCS_Command.ItemsSource = TransCMD_BindingSource;
                uc_grid_TransCMD1.grid_MCS_Command.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
                uc_grid_CurAlarm1.grid_Cur_Alarm.ItemsSource = app.ObjCacheManager.GetAlarms();
                uc_grid_SystemLog1.grid_Sys_Log.ItemsSource = systemProcLst;

                app.ObjCacheManager.VehicleUpdateComplete += ObjCacheManager_VehicleUpdateComplete;
                app.ObjCacheManager.MCSCommandUpdateComplete += ObjCacheManager_MCSCMDUpdateComplete;
                app.CurrentAlarmChange += ObjCacheManagerCurAlarmUpdateComplete;

                mainForm.MonitorRoadContorlStatusChanged += MainForm_MonitorRoadContorlStatusChanged;

                splitContainer2.SplitterDistance = 195;
                uctl_Map.Width = 1719;
                uctl_Map.Height = 695;
                logUtility = LogUtility.getInstance();

                utilityLog_tcp.start<LogTitle_TCP>(BCAppConstants.LogType.TCP_ForEQ.ToString());
                utilityLog_secs.start<LogTitle_SECS>(BCAppConstants.LogType.SECS_ForHost.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MainForm_MonitorRoadContorlStatusChanged(object sender, bool e)
        {
            if (e)
            {
                EnreyMonitorRoadControlStatus();
            }
            else
            {
                ResetAllSegment();
            }
        }



        string predictPathHandler = "predictPathHandler";
        AVEHICLE InObservationVh = null;
        public void setMonitorVehicle(object obj, string vh_id)
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

                        IndicateVhInfoFromDgv(vh_id);
                    }
                    //else
                    //{
                    //    if (dgv_vehilce_status.SelectedRows.Count > 0)
                    //        dgv_vehilce_status.SelectedRows[0].Selected = false;
                    //}
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
                //var veicleObjShow = app.ObjCacheManager.GetVehicleObjToShows().Where(o => o.VEHICLE_ID == vh_id).FirstOrDefault();
                //if (veicleObjShow != null)
                //{
                //    int selectIndex = app.ObjCacheManager.GetVehicleObjToShows().IndexOf(veicleObjShow);
                //    if (selectIndex >= 0)
                //    {
                //        if ((uc_grid_VHStatus1.grid_VH_Status.SelectedItems.Count > 0 && dgv_vehilce_status.SelectedRows[0].Index != selectIndex) ||
                //            dgv_vehilce_status.SelectedRows.Count == 0)
                //        {

                //            dgv_vehilce_status.Rows[selectIndex].Selected = true;
                //            dgv_vehilce_status.FirstDisplayedScrollingRowIndex = selectIndex;
                //        }
                //        //                    if ((dgv_vehilce_status.SelectedRows.Count > 0 && dgv_vehilce_status.SelectedRows[0].Index != selectIndex) ||
                //        //dgv_vehilce_status.SelectedRows.Count == 0)
                //        //                    {
                //        //                        dgv_vehilce_status.Rows[selectIndex].Selected = true;
                //        //                        dgv_vehilce_status.FirstDisplayedScrollingRowIndex = selectIndex;
                //        //                    }
                //    }
                //}
                foreach (var item in uc_grid_VHStatus1.grid_VH_Status.Items)
                {
                    VehicleObjToShow obj = (VehicleObjToShow)item;
                    if (obj.VEHICLE_ID.Trim() == vh_id.Trim())
                    {
                        uc_grid_VHStatus1.grid_VH_Status.SelectedItem = item;
                        break;
                    }
                }
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
                refresh_VHStatus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void refresh_VHStatus()
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    uc_grid_VHStatus1.grid_VH_Status.ItemsSource = app.ObjCacheManager.GetVehicleObjToShows();
                }, null);
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
                refresh_TransCMDGrp();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_TransCMDGrp()
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    Object current = uc_grid_TransCMD1.grid_MCS_Command.SelectedItem;
                    List<ListSortDirection?> cols_sortdir = GetColumnInfo(uc_grid_TransCMD1.grid_MCS_Command);
                    List<SortDescription> sortDescriptionList = GetSortInfo(uc_grid_TransCMD1.grid_MCS_Command);
                    uc_grid_TransCMD1.grid_MCS_Command.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
                    if (current != null)
                    {
                        VACMD_MCS pre = (VACMD_MCS)current;
                        foreach (var item in uc_grid_TransCMD1.grid_MCS_Command.Items)
                        {
                            VACMD_MCS now = (VACMD_MCS)item;

                            if (now.CMD_ID.Trim() == pre.CMD_ID.Trim())
                            {
                                uc_grid_TransCMD1.grid_MCS_Command.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    SetColumnInfo(uc_grid_TransCMD1.grid_MCS_Command, cols_sortdir);
                    SetSortInfo(uc_grid_TransCMD1.grid_MCS_Command, sortDescriptionList);
                }, null);
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

        private void ObjCacheManagerCurAlarmUpdateComplete(object obj, List<ALARM> alarms)
        {
            try
            {
                refresh_CurAlarmGrp();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_CurAlarmGrp()
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    uc_grid_CurAlarm1.grid_Cur_Alarm.ItemsSource = app.ObjCacheManager.GetAlarms();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //清除ToolBar Item名稱, 為伸縮鍵時使用之
        private void clearFuncKeyName()
        {
            //uc_FunctionKey1.SetFuncKey("", 1);
            //uc_FunctionKey2.SetFuncKey("", 2);
            //uc_FunctionKey3.SetFuncKey("", 3);
            uc_PlFunctionKey1.uc_FunctionKey4.SetFuncKey("", 4);
            uc_PlFunctionKey1.uc_FunctionKey5.SetFuncKey("", 5);
            //uc_PlFunctionKey1.uc_FunctionKey6.SetFuncKey("", 6);
        }

        private void initFuncKey()
        {
            try
            {
                //uc_PlFunctionKey1.uc_FunctionKey1.SetFuncKey("Automatic Reset", 1);
                //uc_PlFunctionKey1.uc_FunctionKey2.SetFuncKey("Restore Electricity", 2);
                //uc_PlFunctionKey1.uc_FunctionKey3.SetFuncKey("Segment Reservation", 3);
                uc_PlFunctionKey1.uc_FunctionKey4.SetFuncKey("Manual Command", 4);
                uc_PlFunctionKey1.uc_FunctionKey5.SetFuncKey("MCS Command", 5);
                //uc_PlFunctionKey1.uc_FunctionKey6.SetFuncKey("MTL/MTS Maintenace", 6);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void setFuncKeyClick()
        {
            try
            {
                //uc_PlFunctionKey1.uc_FunctionKey1.btn_FuncKey.Click += Btn_FuncKey_Click;
                //uc_PlFunctionKey1.uc_FunctionKey2.btn_FuncKey.Click += Btn_FuncKey_Click;
                //uc_PlFunctionKey1.uc_FunctionKey3.btn_FuncKey.Click += Btn_FuncKey_Click;
                uc_PlFunctionKey1.uc_FunctionKey4.btn_FuncKey.Click += Btn_FuncKey_Click;
                uc_PlFunctionKey1.uc_FunctionKey5.btn_FuncKey.Click += Btn_FuncKey_Click;
                //uc_PlFunctionKey1.uc_FunctionKey6.btn_FuncKey.Click += Btn_FuncKey_Click;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Btn_FuncKey_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (sender.Equals(uc_PlFunctionKey1.uc_FunctionKey4.btn_FuncKey)) //Manual Command
                {
                    mainForm.openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.VehicleManagement);
                }
                else if (sender.Equals(uc_PlFunctionKey1.uc_FunctionKey5.btn_FuncKey)) //MCS Command
                {
                    mainForm.openForm(typeof(frm_Query).Name, false, false, QueryType.McsCommandLog);
                }
                //else if (sender.Equals(uc_PlFunctionKey1.uc_FunctionKey6.btn_FuncKey)) //MTL/MTS Maintenace
                //{
                //    mainForm.openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.MTLMTSMaint);
                //}
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //vehicleObjToShowBindingSource.ResetBindings(false);
                Object current = uc_grid_VHStatus1.grid_VH_Status.SelectedItem;
                List<ListSortDirection?> cols_sortdir = GetColumnInfo(uc_grid_VHStatus1.grid_VH_Status);
                List<SortDescription> sortDescriptionList = GetSortInfo(uc_grid_VHStatus1.grid_VH_Status);
                uc_grid_VHStatus1.grid_VH_Status.ItemsSource = app.ObjCacheManager.GetVehicleObjToShows();
                uc_grid_VHStatus1.grid_VH_Status.Items.Refresh();
                if (current != null)
                {
                    VehicleObjToShow pre = (VehicleObjToShow)current;
                    foreach (var item in uc_grid_VHStatus1.grid_VH_Status.Items)
                    {
                        VehicleObjToShow now = (VehicleObjToShow)item;

                        if (now.VEHICLE_ID.Trim() == pre.VEHICLE_ID.Trim())
                        {
                            uc_grid_VHStatus1.grid_VH_Status.SelectedItem = item;
                            break;
                        }
                    }
                }
                SetColumnInfo(uc_grid_VHStatus1.grid_VH_Status, cols_sortdir);
                SetSortInfo(uc_grid_VHStatus1.grid_VH_Status, sortDescriptionList);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void OHxC_Form_Load(object sender, EventArgs e)
        {
            try
            {
                initFuncKey();
                timer1.Start();
                //setSystemEvent();
                //setTcpMessage();
                //setSecsMessage();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void EnreyMonitorRoadControlStatus(List<string> selectedSegmentID = null)
        {
            RefreshCurrentRoadStatus(selectedSegmentID);
            app.ObjCacheManager.SegmentsUpdateComplete += ObjCacheManager_SegmentsUpdateComplete; ;
        }
        public void LeaveMonitorRoadControlStatus(List<string> selectedSegmentID = null)
        {
            ResetAllSegment();
            app.ObjCacheManager.SegmentsUpdateComplete -= ObjCacheManager_SegmentsUpdateComplete; ;
        }


        private void ObjCacheManager_SegmentsUpdateComplete(object sender, EventArgs e)
        {
            RefreshCurrentRoadStatus(null);
        }

        private void RefreshCurrentRoadStatus(List<string> selectedSegmentID)
        {
            List<ASEGMENT> segment_List = app.ObjCacheManager.GetSegments();
            foreach (ASEGMENT seg in segment_List)
            {
                int index = segment_List.IndexOf(seg);
                if (selectedSegmentID != null && selectedSegmentID.Contains(seg.SEG_NUM))
                {
                    SetSpecifySegmentSelected(seg.SEG_NUM, Color.LightGreen);
                }
                else if (seg.PRE_DISABLE_FLAG)
                {
                    SetSpecifySegmentSelected(seg.SEG_NUM, Color.Pink);
                }
                else if (seg.STATUS == E_SEG_STATUS.Closed)
                {
                    SetSpecifySegmentSelected(seg.SEG_NUM, Color.Red);
                }
                else
                {
                    ResetSpecifySegmentSelected(seg.SEG_NUM);
                }
            }
        }

        public void SetSpecifySegmentSelected(string seg_num, Color set_color)
        {
            uctl_Map.changeSpecifyRailColorBySegNum(seg_num, set_color);
        }
        public void ResetSpecifySegmentSelected(string seg_num)
        {
            uctl_Map.resetRailColor(seg_num);
        }
        public void ResetAllSegment()
        {
            uctl_Map.resetAllRailColor();
        }


        object systemEventLock = new object();
        private void setSystemEvent()
        {
            try
            {
                string subject = BCAppConstants.NATSTopics.NATS_SUBJECT_SYSTEM_LOG;

                //指定要執行的動作
                EventHandler<StanMsgHandlerArgs> msgHandler = (senders, args) =>
                {
                    lock (systemEventLock)
                    {
                        byte[] arrayByte = args.Message.Data;
                        if (arrayByte == null)
                            return;

                        //反序列化
                        Google.Protobuf.MessageParser<SYSTEMPROCESS_INFO> parser = new Google.Protobuf.MessageParser<SYSTEMPROCESS_INFO>(() => new SYSTEMPROCESS_INFO());
                        SYSTEMPROCESS_INFO systemEventInfo = parser.ParseFrom(arrayByte);

                        if (systemProcLst.Count > MAX_LOG_MSG_COUNT)
                        {
                            systemProcLst.RemoveAt(systemProcLst.Count - 1);
                        }

                        systemProcLst.Insert(0, systemEventInfo);

                        //更新畫面
                        Adapter.Invoke(new SendOrPostCallback((o1) =>
                            {
                                //dgv_systemLog.DataSource = systemProcLst;
                                uc_grid_SystemLog1.grid_Sys_Log.ItemsSource = systemProcLst;
                            }), null);

                    }
                };

                //訂閱
                app.GetNatsManager().Subscriber(subject, msgHandler, false, true, 0, null);       //當subject有變化，則進行msgHandler的動作
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        object TcpMsgLock = new object();
        private void setTcpMessage()
        {
            try
            {
                string subject = BCAppConstants.NATSTopics.NATS_SUBJECT_TCPIP_LOG;

                //指定要執行的動作
                EventHandler<StanMsgHandlerArgs> msgHandler = (senders, args) =>
                {
                    lock (TcpMsgLock)
                    {
                        byte[] arrayByte = args.Message.Data;
                        if (arrayByte == null)
                            return;

                        //反序列化
                        Google.Protobuf.MessageParser<EQLOG_INFO> parser = new Google.Protobuf.MessageParser<EQLOG_INFO>(() => new EQLOG_INFO());
                        EQLOG_INFO eqmessage = parser.ParseFrom(arrayByte);

                        LogTitle_TCP logTitleTemp = new LogTitle_TCP()
                        {
                            VH_ID = eqmessage.VHID,
                            SendRecive = eqmessage.SENDRECEIVE,
                            FunName = eqmessage.FUNNAME,
                            SeqNo = eqmessage.SEQNO.ToString(),
                            CmdID = eqmessage.OHTCCMDID,
                            McsCmdID = eqmessage.MCSCMDID,
                            ActType = eqmessage.ACTTYPE,
                            EventType = eqmessage.EVENTTYPE,
                            Message = eqmessage.MESSAGE,
                            LogType = BCAppConstants.LogType.TCP_ForEQ.ToString()
                        };

                        logTitleTemp.Time = eqmessage.TIME;
                        Task.Run(() => logUtility.addLogInfo(logTitleTemp));
                    }
                };

                //訂閱
                app.GetNatsManager().Subscriber(subject, msgHandler, false, true, 0, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }

        }

        object SecsMsgLock = new object();
        private void setSecsMessage()
        {
            string subject = BCAppConstants.NATSTopics.NATS_SUBJECT_SECS_LOG;

            //指定要執行的動作
            EventHandler<StanMsgHandlerArgs> msgHandler = (senders, args) =>
            {
                lock (SecsMsgLock)
                {
                    byte[] arrayByte = args.Message.Data;
                    if (arrayByte == null)
                        return;

                    //反序列化
                    Google.Protobuf.MessageParser<HOSTLOG_INFO> parser = new Google.Protobuf.MessageParser<HOSTLOG_INFO>(() => new HOSTLOG_INFO());
                    HOSTLOG_INFO hostmessage = parser.ParseFrom(arrayByte);

                    LogTitle_SECS logTitleTemp = new LogTitle_SECS()
                    {
                        EQ_ID = hostmessage.EQID,
                        Sx = hostmessage.SX,
                        Fy = hostmessage.FY,
                        FunName = hostmessage.FUNNAME,
                        SendRecive = hostmessage.SENDRECEIVE,
                        DeviceID = hostmessage.DEVICE,
                        Message = hostmessage.MESSAGE,
                        LogType = BCAppConstants.LogType.SECS_ForHost.ToString()
                    };

                    logTitleTemp.Time = hostmessage.TIME;
                    Task.Run(() => logUtility.addLogInfo(logTitleTemp));
                }
            };

            //訂閱
            app.GetNatsManager().Subscriber(subject, msgHandler, false, true, 0, null);
        }

        //放大、恢復、縮小、拖行Message Content介面
        private void btn_ZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (splitContainer1.SplitterDistance >= Maxlimit_MessageContent)
                {
                    splitContainer1.SplitterDistance = Maxlimit_MessageContent;
                    return;
                }
                splitContainer1.SplitterDistance = splitContainer1.SplitterDistance + eachMove_MessageContent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void btn_NormalSize_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.SplitterDistance = Maxlimit_MessageContent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void btn_ZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (splitContainer1.SplitterDistance <= Minlimit_MessageContent)
                {
                    return;
                }
                splitContainer1.SplitterDistance = splitContainer1.SplitterDistance - eachMove_MessageContent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                if (splitContainer1.SplitterDistance >= Maxlimit_MessageContent)
                {
                    splitContainer1.SplitterDistance = Maxlimit_MessageContent;
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //伸縮ToolBar
        bool flag = false;
        public void button1_Click(object sender, EventArgs e)
        {
            //Adapter.Invoke(new SendOrPostCallback((o1) =>
            //{
            //if (flag)
            //    {
            //        splitContainer2.SplitterDistance = Maxlimit_ToolBarContent;
            //        initFuncKey();
            //    }
            //    else
            //    {
            //        splitContainer2.SplitterDistance = Minlimit_ToolBarContent;
            //        clearFuncKeyName();
            //    }
            //    flag = !flag;


            //}), null);


        }
        private void grid_VH_Status_cell_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                VehicleObjToShow seldata = (VehicleObjToShow)uc_grid_VHStatus1.grid_VH_Status.SelectedItem;
                setMonitorVehicle(null, seldata.VEHICLE_ID);
                //UASUSR userAccData = (UASUSR)grid_UserAcc.SelectedItem;
                //if (userAccData == null) return;

                //UA_UserID.txt_Content.Text = userAccData.USER_ID == null ? string.Empty : userAccData.USER_ID;
                //UA_Password.pwd_Password.Password = userAccData.PASSWD == null ? string.Empty : userAccData.PASSWD;
                //UA_ConfrimPassword.pwd_Password.Password = userAccData.PASSWD == null ? string.Empty : userAccData.PASSWD;
                //UA_UserName.txt_Content.Text = userAccData.USER_NAME == null ? string.Empty : userAccData.USER_NAME;
                //UA_Group.combo_Content.Text = userAccData.USER_GRP == null ? string.Empty : userAccData.USER_GRP;
                //UA_BadgeNumber.txt_Content.Text = userAccData.BADGE_NUMBER == null ? string.Empty : userAccData.BADGE_NUMBER;

                //if (userAccData.isDisable())
                //{
                //    UA_AccountActivation.radbtn_Yes.IsChecked = false;
                //    UA_AccountActivation.radbtn_No.IsChecked = true;
                //}
                //else
                //{
                //    UA_AccountActivation.radbtn_Yes.IsChecked = true;
                //    UA_AccountActivation.radbtn_No.IsChecked = false;
                //}

                //UA_Department.txt_Content.Text = userAccData.DEPARTMENT == null ? string.Empty : userAccData.DEPARTMENT;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void showAndHideTabPanel(bool isShow)
        {
            try
            {
                uctl_Map.Focus();

                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    if (isShow)
                    {
                        splitContainer2.SplitterDistance = 195;
                    }
                    else
                    {
                        splitContainer2.SplitterDistance = 80;
                    }
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Task.Run(() => app.MapBLL.GetReserveInfoFromHttp());
        }

        //public void setTabButtonActivate(bool isLogin)
        //{
        //    try
        //    {
        //        if (!isLogin)
        //        {
        //            if (UASUtility.doLogin(this,app))
        //            {
        //                MessageBox.Show("TR");
        //            }
        //            //String FUNCode = BCAppConstants.Operation_Function.FUNC_VEHICLE_MANAGEMENT;
        //            //String FUNCode = BCAppConstants.Maintenance_Function.FUNC_MTS_MTL_MAINTENANCE;

        //            uc_PlFunctionKey1.uc_FunctionKey5.SetFuncKey("MCS Command", 5);
        //            uc_PlFunctionKey1.uc_FunctionKey4.Visibility = System.Windows.Visibility.Collapsed;
        //            uc_PlFunctionKey1.uc_FunctionKey6.Visibility = System.Windows.Visibility.Collapsed;
        //        }
        //        else
        //        {
        //            uc_PlFunctionKey1.uc_FunctionKey4.SetFuncKey("Manual Command", 4);
        //            uc_PlFunctionKey1.uc_FunctionKey5.SetFuncKey("MCS Command", 5);
        //            uc_PlFunctionKey1.uc_FunctionKey6.SetFuncKey("MTL/MTS Maintenace", 6);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}
    }
}
