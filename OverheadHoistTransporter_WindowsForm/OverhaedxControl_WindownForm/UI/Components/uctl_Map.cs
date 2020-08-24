using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.ohxc.winform.Properties;
using com.mirle.ibg3k0.sc;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace com.mirle.ibg3k0.bc.winform.UI.Components
{


    public partial class uctl_Map : UserControl
    {
        public event EventHandler<bool> MonitorRoadContorlStatusChanged;


        #region Section Through Color 
        public static Color SEC_THROUGH_COLOR_LV1 = Color.FromArgb(51, 255, 0);
        public static Color SEC_THROUGH_COLOR_LV2 = Color.FromArgb(255, 255, 0);
        public static Color SEC_THROUGH_COLOR_LV3 = Color.FromArgb(255, 204, 0);
        public static Color SEC_THROUGH_COLOR_LV4 = Color.FromArgb(254, 153, 0);
        public static Color SEC_THROUGH_COLOR_LV5 = Color.FromArgb(255, 0, 0);
        public static Color SEC_THROUGH_COLOR_LV6 = Color.FromArgb(204, 0, 1);
        public static Color SEC_THROUGH_COLOR_LV7 = Color.FromArgb(153, 0, 0);
        public static Color SEC_THROUGH_COLOR_LV8 = Color.FromArgb(153, 0, 152);
        public static Color SEC_THROUGH_COLOR_LV9 = Color.FromArgb(203, 0, 204);
        public static Color SEC_THROUGH_COLOR_LV10 = Color.FromArgb(255, 0, 254);
        #endregion Section Through Color 

        #region Section Through Times 
        public const int SEC_THROUGH_TIMES_LV1 = 13;
        public const int SEC_THROUGH_TIMES_LV2 = 26;
        public const int SEC_THROUGH_TIMES_LV3 = 39;
        public const int SEC_THROUGH_TIMES_LV4 = 52;
        public const int SEC_THROUGH_TIMES_LV5 = 65;
        public const int SEC_THROUGH_TIMES_LV6 = 78;
        public const int SEC_THROUGH_TIMES_LV7 = 91;
        public const int SEC_THROUGH_TIMES_LV8 = 104;
        public const int SEC_THROUGH_TIMES_LV9 = 117;
        #endregion Section Through Times 

        int space_Height_m = 0;
        int space_Width_m = 0;
        int zoon_Factor = 0;
        int defaultMaxScale = 0;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private uctlRail[] m_objItemRail = null;
        int zInitialIndex = 0;
        Color RailOriginalColor = Color.Empty;

        public uctlAddress[] m_objItemAddr { get; private set; } = null;
        public uctlNewVehicle[] m_objItemNewVhcl { get; private set; } = null;
        private uctlPortNew[] m_objItemPortNew = null;
        private PictureBox[] m_objItemPic = null;
        List<Label> sectionLables = null;
        List<Label> addressLables = null;
        private Dictionary<string, GroupRails> m_DicSectionGroupRails;
        private Dictionary<string, List<GroupRails>> m_DicSegmentGroupRails;
        private ALINE line;
        private List<TarnferCMDViewObj> all_cmd_view_obj;
        private int currentCMDViewObjDisplayIndex;


        public Dictionary<string, GroupRails> p_DicSectionGroupRails { get { return m_DicSectionGroupRails; } }


        public void FocusVehicleProcess(object obj, string vhID)
        {
            if (BCFUtility.isMatche(vhID, "ALL"))
            {
                foreach (var vh in m_objItemNewVhcl)
                {
                    vh.Visible = true;
                }
            }
            else
            {
                foreach (var vh in m_objItemNewVhcl)
                {
                    if (BCFUtility.isMatche(vhID, vh.ID))
                    {
                        vh.Visible = true;
                    }
                    else
                    {
                        vh.Visible = false;
                    }
                }
            }
        }

        public uctl_Map()
        {
            InitializeComponent();
            dgv_transCMD.ColumnHeadersDefaultCellStyle.Font = new Font("MicrosoftSansSerif", 10.5F);
            dgv_transCMD.DefaultCellStyle.Font = new Font("MicrosoftSansSerif", 10.5F);
            dgv_transCMD.Columns[7].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";

            //uc_grid_TransCMD1.grid_MCS_Command.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
            //app.ObjCacheManager.MCSCommandUpdateComplete += ObjCacheManager_MCSCMDUpdateComplete;
        }
        // public OHT_Form ohtc_Form { get; private set; } = null;
        ohxc.winform.App.WindownApplication app;
        public void start(ohxc.winform.App.WindownApplication _app)
        {
            m_DicSectionGroupRails = new Dictionary<string, GroupRails>();
            m_DicSegmentGroupRails = new Dictionary<string, List<GroupRails>>();

            app = _app;
            line = app.ObjCacheManager.GetLine();
            initialMapSpace();
            InitialDisplayMode();
            InitalThroughLable();

            readRailDatas();
            readAddressPortDatas();
            //readVehicleDatas();
            readNewVehicleDatas();
            readPortIconDatas();

            BidingRailPointAndGroupForRail();
            BidingGroupRailsAndAddress();
            BidingGroupRailsLable();
            BidingAddressLable();
            adjustTheLayerOrder();
            if (m_objItemRail != null && m_objItemRail.Count() > 0)
            {
                RailOriginalColor = m_objItemRail[0].p_RailColor;
                zInitialIndex = pnl_Map.Controls.GetChildIndex(m_objItemRail.Last());
            }
            List<string> selectSeg = new List<string>();
            selectSeg.Add("");
            selectSeg.AddRange(m_DicSegmentGroupRails.Keys.ToList());
            tmrRefresh.Start();
            WinFromUtility.setScale(defaultMaxScale, zoon_Factor);
            ratioChanges();

            if (m_objItemNewVhcl != null)
            {
                for (int ii = 0; ii < m_objItemNewVhcl.Length; ii++)
                {
                    m_objItemNewVhcl[ii].turnOnMonitorVh();
                }
            }

            List<AVEHICLE> vhs = app.ObjCacheManager.GetVEHICLEs();

            //Task.Run(() => RegularUpdateSysQualityStatus());

        }

        //private void ObjCacheManager_MCSCMDUpdateComplete(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        refresh_TransCMDGrp();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        //public void refresh_TransCMDGrp()
        //{
        //    try
        //    {
        //        Adapter.Invoke((obj) =>
        //        {
        //            uc_grid_TransCMD1.grid_MCS_Command.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
        //        }, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        private void InitalThroughLable()
        {
        }
        private void InitialDisplayMode()
        {
            line.isDisplayMode = false;
            line.isCMDIndiSetChanged = false;
            line.isDisplayLastCMD = true;
            line.DisplayLoopSetting = ALINE.CMDIndiSettings.All;
            line.CMDIndiPriortySetting = 0;
            line.CMDLoopIntervalSetting = 4;
            dgv_transCMD.AutoGenerateColumns = false;
            all_cmd_view_obj = new List<TarnferCMDViewObj>();
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(line.CMDLoopIntervalSetting * 1000);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        void _timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer _timer = sender as DispatcherTimer;
            _timer.Interval = TimeSpan.FromMilliseconds(line.CMDLoopIntervalSetting * 1000);
            if (line.isDisplayMode)
            {
                if (line.isCMDIndiSetChanged)
                {
                    all_cmd_view_obj.Clear();
                    line.isCMDIndiSetChanged = false;
                }
                List<VehicleObjToShow> vehicles;
                List<VACMD_MCS> cmdList;

                if (line.isDisplayLastCMD)
                {
                    currentCMDViewObjDisplayIndex = 0;
                    #region 整理Command List
                    switch (line.DisplayLoopSetting)
                    {
                        case ALINE.CMDIndiSettings.All:
                            vehicles = app.ObjCacheManager.GetVehicleObjToShows();
                            cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => (cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled)).ToList();
                            foreach (VehicleObjToShow vh in vehicles)
                            {
                                if (string.IsNullOrEmpty(vh.MCS_CMD) && !string.IsNullOrEmpty(vh.OHTC_CMD))
                                {
                                    ACMD_OHTC aCMD_OHTC = app.CmdBLL.GetCmd_OhtcByID(vh.OHTC_CMD);
                                    if (aCMD_OHTC != null)
                                    {
                                        VACMD_MCS vACMD_MCS = new VACMD_MCS();
                                        vACMD_MCS.CMD_ID = string.Empty;
                                        vACMD_MCS.OHTC_CMD = aCMD_OHTC.CMD_ID;
                                        vACMD_MCS.VH_ID = aCMD_OHTC.VH_ID;
                                        vACMD_MCS.CARRIER_ID = aCMD_OHTC.CARRIER_ID;
                                        vACMD_MCS.HOSTSOURCE = aCMD_OHTC.SOURCE;
                                        vACMD_MCS.HOSTDESTINATION = aCMD_OHTC.DESTINATION;
                                        if (aCMD_OHTC.CMD_START_TIME != null)
                                        {
                                            vACMD_MCS.CMD_INSER_TIME = (DateTime)aCMD_OHTC.CMD_START_TIME;
                                        }
                                        vACMD_MCS.CMD_START_TIME = aCMD_OHTC.CMD_START_TIME;
                                        cmdList.Add(vACMD_MCS);
                                    }
                                }
                            }

                            all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();

                            break;
                        case ALINE.CMDIndiSettings.MCS:
                            cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => (cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled)).ToList();
                            all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();
                            break;
                        case ALINE.CMDIndiSettings.OHxC:
                            cmdList = new List<VACMD_MCS>();
                            vehicles = app.ObjCacheManager.GetVehicleObjToShows();
                            foreach (VehicleObjToShow vh in vehicles)
                            {
                                if (string.IsNullOrEmpty(vh.MCS_CMD) && !string.IsNullOrEmpty(vh.OHTC_CMD))
                                {
                                    ACMD_OHTC aCMD_OHTC = app.CmdBLL.GetCmd_OhtcByID(vh.OHTC_CMD);
                                    if (aCMD_OHTC != null)
                                    {
                                        VACMD_MCS vACMD_MCS = new VACMD_MCS();
                                        vACMD_MCS.CMD_ID = string.Empty;
                                        vACMD_MCS.OHTC_CMD = aCMD_OHTC.CMD_ID;
                                        vACMD_MCS.VH_ID = aCMD_OHTC.VH_ID;
                                        vACMD_MCS.CARRIER_ID = aCMD_OHTC.CARRIER_ID;
                                        vACMD_MCS.HOSTSOURCE = aCMD_OHTC.SOURCE;
                                        vACMD_MCS.HOSTDESTINATION = aCMD_OHTC.DESTINATION;
                                        if (aCMD_OHTC.CMD_START_TIME != null)
                                        {
                                            vACMD_MCS.CMD_INSER_TIME = (DateTime)aCMD_OHTC.CMD_START_TIME;
                                        }
                                        vACMD_MCS.CMD_START_TIME = aCMD_OHTC.CMD_START_TIME;
                                        cmdList.Add(vACMD_MCS);
                                    }
                                }
                            }
                            all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();
                            break;
                        case ALINE.CMDIndiSettings.Priority:
                            cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled && cmd.PRIORITY_SUM >= line.CMDIndiPriortySetting).ToList();
                            all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.PRIORITY_SUM).ToList();
                            break;
                    }
                    #endregion
                    if (all_cmd_view_obj.Count != 0)
                    {
                        List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                        temp.Add(all_cmd_view_obj[currentCMDViewObjDisplayIndex]);
                        dgv_transCMD.DataSource = temp;
                        MapDoubleClick?.Invoke(this, all_cmd_view_obj[currentCMDViewObjDisplayIndex].VH_ID);
                    }
                    else
                    {
                        List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                        dgv_transCMD.DataSource = temp;
                    }
                }
                else
                {
                    if (currentCMDViewObjDisplayIndex < all_cmd_view_obj.Count)
                    {
                        List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                        temp.Add(all_cmd_view_obj[currentCMDViewObjDisplayIndex]);
                        dgv_transCMD.DataSource = temp;
                        MapDoubleClick?.Invoke(this, all_cmd_view_obj[currentCMDViewObjDisplayIndex].VH_ID);
                    }
                    else
                    {
                        currentCMDViewObjDisplayIndex = 0;
                        #region 整理Command List
                        switch (line.DisplayLoopSetting)
                        {
                            case ALINE.CMDIndiSettings.All:
                                vehicles = app.ObjCacheManager.GetVehicleObjToShows();
                                cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => (cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled)).ToList();
                                foreach (VehicleObjToShow vh in vehicles)
                                {
                                    if (string.IsNullOrEmpty(vh.MCS_CMD) && !string.IsNullOrEmpty(vh.OHTC_CMD))
                                    {
                                        ACMD_OHTC aCMD_OHTC = app.CmdBLL.GetCmd_OhtcByID(vh.OHTC_CMD);
                                        if (aCMD_OHTC != null)
                                        {
                                            VACMD_MCS vACMD_MCS = new VACMD_MCS();
                                            vACMD_MCS.CMD_ID = string.Empty;
                                            vACMD_MCS.OHTC_CMD = aCMD_OHTC.CMD_ID;
                                            vACMD_MCS.VH_ID = aCMD_OHTC.VH_ID;
                                            vACMD_MCS.CARRIER_ID = aCMD_OHTC.CARRIER_ID;
                                            vACMD_MCS.HOSTSOURCE = aCMD_OHTC.SOURCE;
                                            vACMD_MCS.HOSTDESTINATION = aCMD_OHTC.DESTINATION;
                                            if (aCMD_OHTC.CMD_START_TIME != null)
                                            {
                                                vACMD_MCS.CMD_INSER_TIME = (DateTime)aCMD_OHTC.CMD_START_TIME;
                                            }
                                            vACMD_MCS.CMD_START_TIME = aCMD_OHTC.CMD_START_TIME;
                                            cmdList.Add(vACMD_MCS);
                                        }
                                    }
                                }

                                all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();

                                break;
                            case ALINE.CMDIndiSettings.MCS:
                                cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => (cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled)).ToList();
                                all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();
                                break;
                            case ALINE.CMDIndiSettings.OHxC:
                                cmdList = new List<VACMD_MCS>();
                                vehicles = app.ObjCacheManager.GetVehicleObjToShows();
                                foreach (VehicleObjToShow vh in vehicles)
                                {
                                    if (string.IsNullOrEmpty(vh.MCS_CMD) && !string.IsNullOrEmpty(vh.OHTC_CMD))
                                    {
                                        ACMD_OHTC aCMD_OHTC = app.CmdBLL.GetCmd_OhtcByID(vh.OHTC_CMD);
                                        if (aCMD_OHTC != null)
                                        {
                                            VACMD_MCS vACMD_MCS = new VACMD_MCS();
                                            vACMD_MCS.CMD_ID = string.Empty;
                                            vACMD_MCS.OHTC_CMD = aCMD_OHTC.CMD_ID;
                                            vACMD_MCS.VH_ID = aCMD_OHTC.VH_ID;
                                            vACMD_MCS.CARRIER_ID = aCMD_OHTC.CARRIER_ID;
                                            vACMD_MCS.HOSTSOURCE = aCMD_OHTC.SOURCE;
                                            vACMD_MCS.HOSTDESTINATION = aCMD_OHTC.DESTINATION;
                                            if (aCMD_OHTC.CMD_START_TIME != null)
                                            {
                                                vACMD_MCS.CMD_INSER_TIME = (DateTime)aCMD_OHTC.CMD_START_TIME;
                                            }
                                            vACMD_MCS.CMD_START_TIME = aCMD_OHTC.CMD_START_TIME;
                                            cmdList.Add(vACMD_MCS);
                                        }
                                    }
                                }
                                all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.CMD_START_TIME).ToList();
                                break;
                            case ALINE.CMDIndiSettings.Priority:
                                cmdList = app.ObjCacheManager.GetMCS_CMD().Where(cmd => cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled && cmd.PRIORITY_SUM >= line.CMDIndiPriortySetting).ToList();
                                all_cmd_view_obj = cmdList.Select(cmd => new TarnferCMDViewObj(cmd)).OrderByDescending(cmd => cmd.PRIORITY_SUM).ToList();
                                break;
                        }
                        #endregion

                        if (all_cmd_view_obj.Count != 0)
                        {
                            AVEHICLE vh = app.ObjCacheManager.GetVEHICLE(all_cmd_view_obj[currentCMDViewObjDisplayIndex].VH_ID);
                            while (vh != null && vh.OHTC_CMD.Trim() != all_cmd_view_obj[currentCMDViewObjDisplayIndex].OHTC_CMD.Trim())
                            {
                                currentCMDViewObjDisplayIndex++;
                                if (all_cmd_view_obj.Count <= currentCMDViewObjDisplayIndex)
                                {
                                    return;
                                }
                                else
                                {
                                    vh = app.ObjCacheManager.GetVEHICLE(all_cmd_view_obj[currentCMDViewObjDisplayIndex].VH_ID);
                                    continue;
                                }
                            }
                            if (vh != null)
                            {
                                List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                                temp.Add(all_cmd_view_obj[currentCMDViewObjDisplayIndex]);
                                dgv_transCMD.DataSource = temp;
                                MapDoubleClick?.Invoke(this, all_cmd_view_obj[currentCMDViewObjDisplayIndex].VH_ID);
                            }
                            else
                            {
                                List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                                dgv_transCMD.DataSource = temp;
                            }
                        }
                        else
                        {
                            List<TarnferCMDViewObj> temp = new List<TarnferCMDViewObj>();
                            dgv_transCMD.DataSource = temp;
                        }
                    }
                    currentCMDViewObjDisplayIndex++;
                }
            }
        }
        private void initialMapSpace()
        {
            if (BCFUtility.isMatche(ohxc.winform.App.WindownApplication.ID, "Hsinchu"))
            {
                space_Height_m = 10000; //新竹地圖大小
                space_Width_m = 14000;
                zoon_Factor = 100;
                defaultMaxScale = 5;
            }
            else if (BCFUtility.isMatche(ohxc.winform.App.WindownApplication.ID, "OHS100"))
            {
                space_Height_m = 150000; //OHS_100
                space_Width_m = 225000;
                zoon_Factor = 600;
                defaultMaxScale = 14;
                trackBar_scale.SmallChange = 2;
            }
            else if (BCFUtility.isMatche(ohxc.winform.App.WindownApplication.ID, "CSOT_T4"))
            {
                space_Height_m = 40000; //OHS_100
                space_Width_m = 60000;
                zoon_Factor = 160;
                defaultMaxScale = 13;
                trackBar_scale.SmallChange = 2;
            }
            else
            {
                space_Height_m = 14000;
                space_Width_m = 40000;
                zoon_Factor = 100;
                defaultMaxScale = 10;
            }
            trackBar_scale.Maximum = defaultMaxScale;
            trackBar_scale.Value = defaultMaxScale;
            //BCUtility.setScale(trackBar_scale.Value);
            WinFromUtility.setScale(10, zoon_Factor);
            double space_Height_PixelsHeight = WinFromUtility.RealLengthToPixelsWidthByScale(space_Height_m);
            double space_Height_PixelsWidth = WinFromUtility.RealLengthToPixelsWidthByScale(space_Width_m);
            pnl_Map.Height = (int)space_Height_PixelsHeight;
            pnl_Map.Width = (int)space_Height_PixelsWidth;
            this.pnl_Map.Resize += new System.EventHandler(this.pnl_Map_Resize);
            pnl_Map.Tag = pnl_Map.Height + "|" + pnl_Map.Width;

            switch (ohxc.winform.App.WindownApplication.OHxCFormMode)
            {
                case ohxc.winform.App.OHxCFormMode.CurrentPlayer:
                    //pic_cmdQueueTime.Visible = true;
                    //pic_cmdCount.Visible = true;
                    break;
                case ohxc.winform.App.OHxCFormMode.HistoricalPlayer:
                    //pic_cmdQueueTime.Visible = false;
                    //pic_cmdCount.Visible = false;
                    break;
            }

        }

        private bool readRailDatas()
        {
            bool bRet = false;
            int iRailDatasCount = 0;
            try
            {
                IEnumerable<ARAIL> enumerRail = null;
                enumerRail = app.MapBLL.loadAllRail();
                iRailDatasCount = enumerRail.Count();
                if (iRailDatasCount > 0)
                {
                    m_objItemRail = new uctlRail[iRailDatasCount];

                    int index = 0;
                    foreach (ARAIL rail in enumerRail)
                    {
                        m_objItemRail[index] = new uctlRail();
                        m_objItemRail[index].p_Num = index + 1;
                        m_objItemRail[index].p_ID = rail.RAIL_ID;
                        m_objItemRail[index].p_RailType = rail.RAILTYPE;
                        m_objItemRail[index].p_StrMapRailInfo = rail;
                        m_objItemRail[index].p_RailWidth = (int)rail.WIDTH;
                        m_objItemRail[index].p_RailColor = WinFromUtility.ConvStr2Color(rail.COLOR);
                        m_objItemRail[index].p_LocX = rail.LOCATIONX;
                        m_objItemRail[index].p_LocY = rail.LOCATIONY + 50;
                        m_objItemRail[index].p_RailLength = (int)rail.LENGTH;
                        if (m_objItemRail[index].p_RailType == E_RAIL_TYPE.Straight_Vertical)
                        {
                            m_objItemRail[index].Tag = m_objItemRail[index].Top + "|" + m_objItemRail[index].Left + "|"
                                + m_objItemRail[index].Height + "|" + m_objItemRail[index].p_RailWidth;
                        }
                        else
                        {
                            m_objItemRail[index].Tag = m_objItemRail[index].Top + "|" + m_objItemRail[index].Left + "|"
                                + m_objItemRail[index].Width + "|" + m_objItemRail[index].p_RailWidth;
                        }

                        if (m_objItemRail[index].p_RailType == E_RAIL_TYPE.Arrow_Up ||
                            m_objItemRail[index].p_RailType == E_RAIL_TYPE.Arrow_Down ||
                            m_objItemRail[index].p_RailType == E_RAIL_TYPE.Arrow_Left ||
                            m_objItemRail[index].p_RailType == E_RAIL_TYPE.Arrow_Right)
                        {
                            m_objItemRail[index].Visible = false;
                        }
                        index++;
                    }
                    this.pnl_Map.Controls.AddRange(m_objItemRail);
                }
                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
                logger.Error(ex, "Exception");
            }
            return (bRet);
        }
        private bool readAddressPortDatas()
        {
            bool bRet = false;
            int iAddrCount = 0;
            try
            {
                IEnumerable<AADDRESS> enumerAdr = app.MapBLL.loadAllAddress();

                iAddrCount = enumerAdr.Count();

                int index = 0;
                m_objItemAddr = new uctlAddress[iAddrCount];
                foreach (AADDRESS adr in enumerAdr)
                {
                    m_objItemAddr[index] = new uctlAddress(this);
                    m_objItemAddr[index].p_AddrPt = index;
                    m_objItemAddr[index].p_Address = adr.ADR_ID.Trim();
                    APOINT point = app.MapBLL.getPointByID(adr.ADR_ID);
                    if (point != null)
                    {
                        m_objItemAddr[index].str_Map_Add_Info = point;
                        m_objItemAddr[index].p_LayoutPoint = 0;
                        m_objItemAddr[index].p_Text = string.Empty;
                        m_objItemAddr[index].p_PointType = point.POINTTYPE;
                        m_objItemAddr[index].p_LocX = point.LOCATIONX;
                        m_objItemAddr[index].p_LocY = point.LOCATIONY + 50;
                        m_objItemAddr[index].p_SizeW = (int)point.WIDTH;
                        m_objItemAddr[index].p_SizeH = (int)point.HEIGHT;
                        m_objItemAddr[index].p_Color = WinFromUtility.ConvStr2Color(point.COLOR);
                        m_objItemAddr[index].p_ZoomLV = adr.ZOOM_LV;
                        m_objItemAddr[index].Visible = adr.ZOOM_LV >= trackBar_scale.Value;
                        m_objItemAddr[index].Tag = m_objItemAddr[index].Top + "|" + m_objItemAddr[index].Left + "|"
                            + m_objItemAddr[index].Height + "|" + m_objItemAddr[index].Width;
                    }
                    else
                    {

                    }
                    index++;
                }

                if (iAddrCount > 0) this.pnl_Map.Controls.AddRange(m_objItemAddr);

                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
                logger.Error(ex, "Exception");
            }
            return (bRet);
        }

        private bool readPortIconDatas()
        {
            bool bRet = false;
            int iPortIconCount = 0;
            try
            {
                IEnumerable<APORTICON> enumerportIcon = app.MapBLL.loadAllPortIcon();

                iPortIconCount = enumerportIcon.Count();

                int index = 0;
                m_objItemPortNew = new uctlPortNew[iPortIconCount];
                foreach (APORTICON adr in enumerportIcon)
                {
                    m_objItemPortNew[index] = new uctlPortNew();
                    m_objItemPortNew[index].p_Color = WinFromUtility.ConvStr2Color(adr.COLOR);
                    m_objItemPortNew[index].p_PortName = adr.PORT_ID;
                    m_objItemPortNew[index].p_Address = adr.ADR_ID;
                    m_objItemPortNew[index].p_LocX = adr.LOCATIONX;
                    m_objItemPortNew[index].p_LocY = adr.LOCATIONY + 50;
                    m_objItemPortNew[index].p_SizeH = adr.HEIGHT;
                    m_objItemPortNew[index].p_SizeW = adr.WIDTH;
                    m_objItemPortNew[index].Tag = m_objItemPortNew[index].Top + "|" + m_objItemPortNew[index].Left + "|"
                                                 + m_objItemPortNew[index].Height + "|" + m_objItemPortNew[index].Width;

                    index++;
                }

                if (iPortIconCount > 0) this.pnl_Map.Controls.AddRange(m_objItemPortNew);

                bRet = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                bRet = false;
            }
            return (bRet);
        }



        private bool readNewVehicleDatas()
        {
            bool bRet = false;
            int iVhclCount = 0;
            try
            {
                List<AVEHICLE> lstEq = app.ObjCacheManager.GetVEHICLEs();
                iVhclCount = lstEq.Count;

                m_objItemNewVhcl = new uctlNewVehicle[iVhclCount];
                m_objItemPic = new PictureBox[iVhclCount * 2];

                for (int ii = 0; ii < iVhclCount; ii++)
                {
                    PictureBox pic_AlarmStatus = new PictureBox();
                    //pic_AlarmStatus.Size = new Size(64, 48);
                    pic_AlarmStatus.SizeMode = PictureBoxSizeMode.Zoom;
                    pic_AlarmStatus.BackColor = Color.Transparent;
                    pic_AlarmStatus.Visible = false;


                    PictureBox pic_CstIcon = new PictureBox();
                    pic_CstIcon.Size =
                        new Size(Resources.Action__Cassette_.Size.Width / 2, Resources.Action__Cassette_.Size.Height / 2);
                    pic_CstIcon.SizeMode = PictureBoxSizeMode.Zoom;
                    pic_CstIcon.BackColor = Color.Transparent;
                    pic_CstIcon.Visible = false;
                    pic_CstIcon.Image = Resources.Action__Cassette_;

                    m_objItemNewVhcl[ii] = new uctlNewVehicle(lstEq[ii], this, pic_AlarmStatus, pic_CstIcon);
                    //m_objItemNewVhcl[ii].Num = ii + 1;
                    int num = ii + 1;
                    m_objItemNewVhcl[ii].Num = num;
                    m_objItemNewVhcl[ii].PrcSetLocation((uctlNewVehicle.UNKNOW_DEFAULT_X_LOCATION_VALUE * num) + 2, uctlNewVehicle.UNKNOW_DEFAULT_Y_LOCATION_VALUE);

                    //m_objItemNewVhcl[ii].turnOnMonitorVh();
                    //m_objItemNewVhcl[ii].p_SizeW = m_objItemVhcl[ii].Width;
                    //m_objItemNewVhcl[ii].p_SizeH = m_objItemVhcl[ii].Height;
                    //m_objItemNewVhcl[ii].Width = Resources.Vehicle__Unconnected_.Width / 2;
                    //m_objItemNewVhcl[ii].Height = Resources.Vehicle__Unconnected_.Height / 2;
                    //m_objItemNewVhcl[ii].Left = m_objItemNewVhcl[ii].Width / 2;
                    //m_objItemNewVhcl[ii].Top = m_objItemNewVhcl[ii].Height / 2;
                    // m_objItemNewVhcl[ii].p_Presence = false;
                    m_objItemPic[ii * 2] = pic_AlarmStatus;
                    m_objItemPic[(ii * 2) + 1] = pic_CstIcon;


                }
                if (iVhclCount > 0) this.pnl_Map.Controls.AddRange(m_objItemNewVhcl);
                if (m_objItemPic.Length > 0) this.pnl_Map.Controls.AddRange(m_objItemPic);
                bRet = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                bRet = false;
            }

            return (bRet);
        }

        private void adjustTheLayerOrder()
        {
            if (m_objItemRail != null)
            {
                for (int ii = 0; ii < m_objItemRail.Length; ii++)
                {
                    m_objItemRail[ii].BringToFront();
                }
            }

            if (m_objItemAddr != null)
            {
                for (int ii = 0; ii < m_objItemAddr.Length; ii++)
                {
                    m_objItemAddr[ii].BringToFront();
                }
            }


            if (m_objItemNewVhcl != null)
            {
                for (int ii = 0; ii < m_objItemNewVhcl.Length; ii++)
                {
                    m_objItemNewVhcl[ii].BringToFront();
                    m_objItemNewVhcl[ii].PicAlarmStatus.BringToFront();
                    m_objItemNewVhcl[ii].PicCSTLoadStatus.BringToFront();
                }
            }
            if (sectionLables != null)
            {
                for (int ii = 0; ii < sectionLables.Count(); ii++)
                {
                    sectionLables[ii].BringToFront();
                }
            }
            if (addressLables != null)
            {
                for (int ii = 0; ii < addressLables.Count(); ii++)
                {
                    addressLables[ii].BringToFront();
                }
            }
        }
        private bool BidingRailPointAndGroupForRail()
        {
            bool bRet = false;
            List<AGROUPRAILS> listGroup = app.MapBLL.loadAllGroupRail();

            for (int i = 0; i < listGroup.Count - 1; i++)
            {
                if (!BCFUtility.isMatche(listGroup[i].SECTION_ID, listGroup[i + 1].SECTION_ID))
                    continue;
                findMatchRailPoint(listGroup[i].RAIL_ID, listGroup[i + 1].RAIL_ID);
            }

            foreach (AGROUPRAILS group_rail in listGroup)
            {
                uctlRail railTemp = null;
                string section_id = group_rail.SECTION_ID.Trim();
                railTemp = m_objItemRail.Where(r => r.p_ID.Trim() == group_rail.RAIL_ID.Trim()).FirstOrDefault();
                if (railTemp == null)
                    continue;
                if (!m_DicSectionGroupRails.ContainsKey(section_id))
                {

                    ASECTION ASEC_ObJ = app.MapBLL.getSectiontByID(section_id);
                    ASEGMENT ASEG_ObJ = app.MapBLL.getSegmentByNum(ASEC_ObJ.SEG_NUM);
                    m_DicSectionGroupRails.Add(group_rail.SECTION_ID.Trim(), new GroupRails(group_rail.SECTION_ID, ASEC_ObJ.SEC_DIS, group_rail.DIR
                                                                                            , ASEG_ObJ.SEG_NUM, ASEG_ObJ.DIR));
                }
                m_DicSectionGroupRails[section_id].uctlRails.Add(new KeyValuePair<uctlRail, E_RAIL_DIR>(railTemp, group_rail.DIR));
            }

            foreach (GroupRails group in m_DicSectionGroupRails.Values)
            {
                group.RefreshDistance();
                group.DecisionFirstAndLastPoint(true);

            }

            List<ASEGMENT> listSegment = app.MapBLL.loadAllSegments();
            foreach (ASEGMENT seg in listSegment)
            {
                List<GroupRails> segSections = m_DicSectionGroupRails.Values
                    .Where(group => group.Segment_Num.Trim() == seg.SEG_NUM.Trim()).ToList();
                m_DicSegmentGroupRails.Add(seg.SEG_NUM.Trim(), segSections);
            }
            return (bRet);
        }
        private bool BidingGroupRailsAndAddress()
        {
            bool bRet = false;
            List<string> secIDs = app.MapBLL.loadAllSectionID();
            uctlAddress addressTemp = null;
            foreach (string sec_id in secIDs)
            {
                ASECTION section = app.MapBLL.getSectiontByID(sec_id);
                if (section == null)
                    continue;
                addressTemp = m_objItemAddr.Where(a => a.p_Address.Trim() == section.FROM_ADR_ID.Trim()).FirstOrDefault();
                if (addressTemp != null)
                    m_DicSectionGroupRails[section.SEC_ID.Trim()].p_Points[0].BindingAddress(addressTemp, false);
                addressTemp = m_objItemAddr.Where(a => a.p_Address.Trim() == section.TO_ADR_ID.Trim()).FirstOrDefault();
                if (addressTemp != null)
                    m_DicSectionGroupRails[section.SEC_ID.Trim()].p_Points[1].BindingAddress(addressTemp, true);
            }
            return (bRet);
        }
        private bool BidingGroupRailsLable()
        {
            bool bRet = false;
            sectionLables = new List<Label>();

            foreach (GroupRails group in m_DicSectionGroupRails.Values)
            {
                sectionLables.Add(group.getSectionLable());
            }
            if (sectionLables.Count > 0)
                this.pnl_Map.Controls.AddRange(sectionLables.ToArray());

            return (bRet);
        }
        private bool BidingAddressLable()
        {
            bool bRet = false;
            addressLables = new List<Label>();

            if (m_objItemAddr != null)
            {
                for (int ii = 0; ii < m_objItemAddr.Length; ii++)
                {
                    if (m_objItemAddr[ii].p_LocX <= 10)
                        continue;
                    addressLables.Add(m_objItemAddr[ii].getLable());
                }
            }
            if (addressLables.Count > 0)
                this.pnl_Map.Controls.AddRange(addressLables.ToArray());

            return (bRet);
        }

        private void findMatchRailPoint(string rail_id, string next_rail_id)
        {
            uctlRail railTemp = null;
            uctlRail NextrailTemp = null;
            railTemp = m_objItemRail.Where(r => r.p_ID.Trim() == rail_id.Trim()).FirstOrDefault();
            NextrailTemp = m_objItemRail.Where(r => r.p_ID.Trim() == next_rail_id.Trim()).FirstOrDefault();
            if (railTemp == null || NextrailTemp == null)
                return;
            foreach (PointObject point in railTemp.p_Points)
            {
                foreach (PointObject next_point in NextrailTemp.p_Points)
                {
                    bool isMatch = pointIsMatch(point.RealPointf, next_point.RealPointf);
                    if (isMatch)
                    {
                        point.BindingPoint(next_point);
                        next_point.BindingPoint(point);
                    }
                }
            }
        }
        private bool pointIsMatch(PointF point, PointF next_point)
        {
            double basePoint_X = point.X;
            double basePoint_Y = point.Y;
            int rangeValue = 5;
            if (Math.Round(next_point.X) > basePoint_X - rangeValue
                && Math.Round(next_point.X) < basePoint_X + rangeValue)
            {
                if (Math.Round(next_point.Y) > basePoint_Y - rangeValue
                    && Math.Round(next_point.Y) < basePoint_Y + rangeValue)
                {
                    return true;
                }
            }
            //if (Math.Round(point.X) == Math.Round(next_point.X)
            //    && Math.Round(point.Y) == Math.Round(next_point.Y))
            //{
            //    return true;
            //}
            return false;
        }

        private void trackBar_scale_Scroll(object sender, EventArgs e)
        {
            WinFromUtility.setScale(trackBar_scale.Value, zoon_Factor);
            ratioChanges();
        }
        private void ratioChanges()
        {
            double space_Height_PixelsHeight = WinFromUtility.RealLengthToPixelsWidthByScale(space_Height_m);
            double space_Height_PixelsWidth = WinFromUtility.RealLengthToPixelsWidthByScale(space_Width_m);
            pnl_Map.Size = new Size((int)space_Height_PixelsWidth, (int)space_Height_PixelsHeight);
        }
        private void pnl_Map_Resize(object sender, EventArgs e)
        {
            if (pnl_Map.Tag == null || m_objItemRail == null) return;
            double scaleWidth = (pnl_Map.Width / double.Parse(pnl_Map.Tag.ToString().Split('|')[1]));
            double scaleHeigh = (pnl_Map.Height / double.Parse(pnl_Map.Tag.ToString().Split('|')[0]));
            foreach (uctlRail rail in m_objItemRail)
            {
                int rail_width = (int)(double.Parse(rail.Tag.ToString().Split('|')[3]) * scaleWidth);
                rail.setWidthByZoonInZoonOut(rail_width);
                int rail_length = (int)(double.Parse(rail.Tag.ToString().Split('|')[2]) * scaleHeigh);
                rail.setLengthByZoonInZoonOut(rail_length);
                rail.p_LocX = (int)(double.Parse(rail.Tag.ToString().Split('|')[1]) * scaleWidth);
                rail.p_LocY = (int)(double.Parse(rail.Tag.ToString().Split('|')[0]) * scaleHeigh);

            }

            //foreach (uctlEquipment eqpt in m_objItemEquip)
            //{
            //    eqpt.Left = (int)(double.Parse(eqpt.Tag.ToString().Split('|')[1]) * scaleWidth);
            //    eqpt.Top = (int)(double.Parse(eqpt.Tag.ToString().Split('|')[0]) * scaleHeigh);
            //}

            foreach (uctlAddress add in m_objItemAddr)
            {
                add.Visible = add.p_ZoomLV >= trackBar_scale.Value;

                add.p_SizeW = (int)(double.Parse(add.Tag.ToString().Split('|')[3]) * scaleWidth);
                add.p_SizeH = (int)(double.Parse(add.Tag.ToString().Split('|')[2]) * scaleHeigh);
                add.Left = (int)(double.Parse(add.Tag.ToString().Split('|')[1]) * scaleWidth);
                add.Top = (int)(double.Parse(add.Tag.ToString().Split('|')[0]) * scaleHeigh);
            }



            foreach (uctlPortNew port in m_objItemPortNew)
            {
                port.Width = (int)(double.Parse(port.Tag.ToString().Split('|')[3]) * scaleWidth);
                port.Height = (int)(double.Parse(port.Tag.ToString().Split('|')[2]) * scaleHeigh);
                port.Left = (int)(double.Parse(port.Tag.ToString().Split('|')[1]) * scaleWidth);
                port.Top = (int)(double.Parse(port.Tag.ToString().Split('|')[0]) * scaleHeigh);
            }

            foreach (GroupRails group in m_DicSectionGroupRails.Values)
            {
                group.DecisionFirstAndLastPoint(false);
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

        #region Rail Flashing Control
        string current_SelectSegment = "";
        List<string[]> current_FlashingSectionGroup = new List<string[]>();
        Boolean change_flag = false;
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            change_flag = !change_flag;
            if (!BCFUtility.isEmpty(current_SelectSegment))
            {
                List<GroupRails> lstGroupRails = m_DicSegmentGroupRails[current_SelectSegment];
                foreach (GroupRails groupRails in lstGroupRails)
                {
                    groupRails.GroupColorChange(change_flag ? RailOriginalColor : Color.Yellow);
                }
            }

            if (current_FlashingSectionGroup != null
                && current_FlashingSectionGroup.Count() > 0)
            {
                foreach (string[] section_ids in current_FlashingSectionGroup)
                {
                    foreach (string section_id in section_ids)
                    {
                        m_DicSectionGroupRails[section_id]
                            .GroupColorChange(change_flag ? RailOriginalColor : Color.Yellow);
                    }
                }
            }
        }

        private void cmb_selectSeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetRailColor(current_SelectSegment);


            RailBringToFrontBySegment(current_SelectSegment);
        }


        public void startFlashingSpecifyRail(string[] sectionGroup)
        {
            RailBringToFrontBySectionGroup(sectionGroup);
            current_FlashingSectionGroup.Add(sectionGroup);
        }
        public void stopFlashingSpecifyRail(string[] section_ids)
        {
            current_FlashingSectionGroup.Remove(section_ids);
        }

        public void resetRailColor(string SelectSegment)
        {
            if (!BCFUtility.isEmpty(SelectSegment))
            {
                foreach (GroupRails groupRails in m_DicSegmentGroupRails[SelectSegment.Trim()])
                {
                    groupRails.GroupColorChange(RailOriginalColor);
                }
            }
        }
        public void resetRailColor(string[] lstSec)
        {
            if (!BCFUtility.isEmpty(lstSec))
            {
                foreach (string groupRails in lstSec)
                {
                    if (m_DicSectionGroupRails.ContainsKey(groupRails))
                        m_DicSectionGroupRails[groupRails].GroupColorChange(RailOriginalColor);
                }
            }
        }
        public void resetAllRailColor()
        {
            foreach (var keyValue in m_DicSectionGroupRails)
            {
                keyValue.Value.GroupColorChange(RailOriginalColor);
            }
        }
        private void RailBringToFrontBySegment(string SelectSegment)
        {
            if (string.IsNullOrWhiteSpace(SelectSegment))
                return;
            int count = 0;
            foreach (GroupRails groupRails in m_DicSegmentGroupRails[SelectSegment])
            {
                foreach (var rail in groupRails.uctlRails)
                {
                    pnl_Map.Controls.SetChildIndex(rail.Key, zInitialIndex + count++);
                }
            }
        }

        private void RailBringToFrontBySectionGroup(string[] SelectSections)
        {
            int count = 0;
            foreach (string section in SelectSections)
            {
                foreach (var rail in m_DicSectionGroupRails[section].uctlRails)
                {
                    Adapter.Invoke((obj) =>
                    {
                        pnl_Map.Controls.SetChildIndex(rail.Key, zInitialIndex + count++);
                    }, null);

                }
            }
        }
        #endregion Rail Flashing Control

        #region Rail Color Change

        public void changeSpecifyRailColorBySegNum(string seg_num, Color rail_color)
        {
            if (string.IsNullOrWhiteSpace(seg_num))
                return;
            List<ASECTION> listSec = app.MapBLL.loadSectionsBySegmentID(seg_num);
            foreach (ASECTION sec in listSec)
            {
                changeSpecifyRailColor(sec.SEC_ID.Trim(), rail_color);
            }
        }
        public void changeSpecifyRailColor(string section, Color rail_color)
        {
            if (string.IsNullOrWhiteSpace(section))
                return;
            if (m_DicSectionGroupRails.ContainsKey(section))
            {
                GroupRails groupRails = m_DicSectionGroupRails[section];
                groupRails.GroupColorChange(rail_color);
            }
        }

        public void changeSpecifyRailColor(string[] sectionGroup)
        {
            if (sectionGroup == null || sectionGroup.Count() == 0)
                return;
            RailBringToFrontBySectionGroup(sectionGroup);
            foreach (string sec in sectionGroup)
            {
                if (m_DicSectionGroupRails.ContainsKey(sec))
                {
                    GroupRails groupRails = m_DicSectionGroupRails[sec];
                    groupRails.GroupColorChange(Color.Yellow);
                }
            }
        }

        public void changeSpecifyRailColor(List<KeyValuePair<string, bool>> SectionsPassStatus)
        {
            string[] sections = null;
            if (SectionsPassStatus == null || SectionsPassStatus.Count() == 0)
                return;
            else
            {
                sections = SectionsPassStatus.Select(o => o.Key).ToArray();
            }
            RailBringToFrontBySectionGroup(sections);
            foreach (KeyValuePair<string, bool> secAndStatus in SectionsPassStatus)
            {
                if (m_DicSectionGroupRails.ContainsKey(secAndStatus.Key))
                {
                    GroupRails groupRails = m_DicSectionGroupRails[secAndStatus.Key];
                    if (!secAndStatus.Value)
                        groupRails.GroupColorChange(Color.Yellow);
                    else
                        groupRails.GroupColorChange(Color.Gray);
                }
            }
        }
        #endregion Rail Color Change
        #region Address Color change
        public void changeSpecifyAddressColor(string adr_id, Color change_color)
        {
            if (string.IsNullOrWhiteSpace(adr_id))
                return;
            uctlAddress uctAdr = m_objItemAddr.
                Where(adr_obj => adr_obj.p_Address == adr_id.Trim()).
                SingleOrDefault();
            if (uctAdr != null)
            {
                uctAdr.p_Color = change_color;
            }
        }
        #endregion Address Color change

        #region Vehicle Position Control



        public GroupRails getGroupRailBySecID(string sec_id)
        {
            if (!m_DicSectionGroupRails.ContainsKey(sec_id.Trim()))
            {
                return null;
            }
            return m_DicSectionGroupRails[sec_id.Trim()];
        }
        public uctlAddress getuctAddressByAdrID(string adr_id)
        {
            uctlAddress uctl_adr = m_objItemAddr.Where(obj => obj.p_Address == adr_id).SingleOrDefault();
            return uctl_adr;
        }

        public GroupRails findMatchGroupRail(string seg_num, string adr_id)
        {
            GroupRails groupRails = null;
            foreach (KeyValuePair<string, GroupRails> keyValue in m_DicSectionGroupRails)
            {
                groupRails = keyValue.Value;
                if (groupRails.Segment_Num.Trim() == seg_num)
                {
                    if (groupRails.p_Points[0].BindAddressID.Trim() == adr_id.Trim())
                    {
                        return groupRails;
                    }
                }
            }
            return groupRails;
        }
        #endregion Vehicle Position Control


        #region Segment Disable Control
        public void RegistRailSelectedEvent(EventHandler eventHandler)
        {
            foreach (uctlRail rail in m_objItemRail)
            {
                rail.RailSelected += eventHandler;
            }
        }
        public void RemoveRailSelectedEvent(EventHandler eventHandler)
        {
            foreach (uctlRail rail in m_objItemRail)
            {
                rail.RailSelected -= eventHandler;
            }
        }

        #endregion Segment Disable Control

        private void cb_railDirection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void uctl_Map_Load(object sender, EventArgs e)
        {
            pnl_Map_Resize(null, null);
        }

        private void cb_SecAdrMark_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void pnl_Map_DoubleClick(object sender, EventArgs e)
        {
            //ohtc_Form.setMonitorVehicle(string.Empty);
        }

        public void trunOnMonitorAllVhStatus()
        {
            foreach (uctlNewVehicle vhOjb in m_objItemNewVhcl)
            {
                vhOjb.turnOnMonitorVh();
            }
        }
        public void trunOffMonitorAllVhStatus()
        {
            foreach (uctlNewVehicle vhOjb in m_objItemNewVhcl)
            {
                vhOjb.turnOffMonitorVh();
            }
        }

        public void DisplayAddressLable(bool isDisplay)
        {
            foreach (Label lbl in addressLables)
            {
                lbl.Visible = isDisplay;
            }
        }

        public void DisplaySectionLables(bool isDisplay)
        {
            foreach (Label lbl in sectionLables)
            {
                lbl.Visible = isDisplay;
            }
        }

        public event EventHandler<string> MapDoubleClick;
        private void pnl_Map_DoubleClick_1(object sender, EventArgs e)
        {
            MapDoubleClick?.Invoke(this, "");
        }

        private void chk_DynamicCmd_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk_DynamicCmd.Checked == true)
            {
                dgv_transCMD.Visible = false;
                line.isDisplayMode = false;
            }
            else
            {
                dgv_transCMD.Visible = true;
                line.isCMDIndiSetChanged = true;
                line.isDisplayMode = true;
            }
        }

        private void chk_MonitorRoadContorlStaus_CheckedChanged(object sender, EventArgs e)
        {
            MonitorRoadContorlStatusChanged?.Invoke(this, chk_MonitorRoadContorlStaus.Checked);
        }
    }
}
