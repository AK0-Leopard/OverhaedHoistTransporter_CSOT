using com.mirle.ibg3k0.ohxc.winform.BackgroundWork;
using com.mirle.ibg3k0.ohxc.winform.BLL;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.ohxc.winform.Schedule;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.DAO;
using com.mirle.ibg3k0.sc.Data.DAO.EntityFramework;
using com.mirle.ibg3k0.sc.Data.VO;
using Newtonsoft.Json;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.App
{

    public enum OHxCFormMode
    {
        CurrentPlayer,
        HistoricalPlayer
    }
    public class WindownApplication
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static OHxCFormMode OHxCFormMode = OHxCFormMode.CurrentPlayer;
        private SCApplication scApp = null;
        public SCApplication SCApplication { get { return scApp; } }
        public static string ID { get; private set; } = "";
        public string ViewerID { get; private set; } = "";

        public BackgroundWorkDriver BackgroundWork_ProcessVhPositionUpdate { get; private set; }


        #region UAS
        private string loginUserID = null;
        public string LoginUserID { get { return loginUserID; } }
        private Dictionary<string, System.Windows.Forms.ToolStripStatusLabel> statusUserIDLabelDic =
            new Dictionary<string, System.Windows.Forms.ToolStripStatusLabel>();
        private Dictionary<string, Action<object>> refresh_UIDisplay_FunDic =
            new Dictionary<string, Action<object>>(); //A0.01
        #endregion UAS

        public RAILDao RailDao { get; private set; } = null;
        public ADDRESSDao AddressDao { get; private set; } = null;
        public PortIconDao PortIconDao { get; private set; } = null;
        public POINTDao PointDao { get; private set; } = null;
        public GROUPRAILSDao GroupRailDao { get; private set; } = null;
        public SectionDao SectionDao { get; private set; } = null;
        public SegmentDao SegmentDao { get; private set; } = null;
        public PortDao PortDao { get; private set; } = null;
        public VehicleDao VehicleDao { get; private set; } = null;
        public UserDao UserDao { get; private set; } = null;
        public UserGroupDao UserGroupDao { get; private set; } = null;
        public UserFuncDao UserFuncDao { get; private set; } = null;
        public FunctionCodeDao FunctionCodeDao { get; private set; } = null;

        public CMD_OHTCDao CMD_OHTCDao { get; private set; } = null;
        public CMD_MCSDao CMD_MCSDao { get; private set; } = null;
        public VCMD_MCSDao VCMD_MCSDao { get; private set; } = null;
        public PortStationDao PortStationDao { get; private set; } = null;
        public AlarmDao AlarmDao { get; private set; } = null;

        private NatsManager natsManager = null;
        private RedisCacheManager redisCacheManager = null;
        private WebClientManager webClientManager = null;
        private ElasticSearchManager elasticSearchManager = null;

        private HistoricalReplyService HistoricalReplyService = null;
        private SysExcuteQualityQueryService SysExcuteQualityQueryService = null;


        public VehicleBLL VehicleBLL { get; private set; }
        public MapBLL MapBLL { get; private set; }
        public LineBLL LineBLL { get; private set; }

        public CmdBLL CmdBLL { get; private set; }
        public AlarmBLL AlarmBLL { get; private set; }
        public PortStationBLL PortStationBLL { get; private set; }
        public SegmentBLL SegmentBLL { get; private set; }

        public SysExcuteQualityBLL SysExcuteQualityBLL { get; private set; }
        public UserBLL UserBLL { get; private set; }
        public OperationHistoryBLL OperationHistoryBLL { get; private set; }



        public ObjCacheManager ObjCacheManager { get; private set; }


        //取得主頁面 版本號碼
        public static String getMainFormVersion(String appendStr)
        {
            return FileVersionInfo.GetVersionInfo(
                Assembly.GetExecutingAssembly().Location).FileVersion.ToString() + appendStr;
        }

        //取得主頁面 建置時間
        public static DateTime GetBuildDateTime()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.ToLocalTime();
            return dt;
        }

        //public void addOperationHis(string user_id, string formName, string action)
        //{
        //    DBConnection conn = null;
        //    try
        //    {
        //        conn = scApp.getDBConnection();
        //        conn.BeginTransaction();
        //        string timeStamp = BCFUtility.formatDateTime(DateTime.Now, SCAppConstants.TimestampFormat_19);
        //        OperationHis his = new OperationHis()
        //        {
        //            T_Stamp = timeStamp,
        //            User_ID = user_id,
        //            Form_Name = formName,
        //            Action = action
        //        };
        //        SCUtility.PrintOperationLog(his);
        //        deleteOperationHis(conn);        //A0.06 
        //        operationHisDao.insertOperationHis(conn, his);
        //        conn.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        if (conn != null) { try { conn.Rollback(); } catch { } }
        //    }
        //    finally
        //    {
        //        if (conn != null) { try { conn.Close(); } catch { } }
        //    }
        //}


        public EventHandler<List<sc.ALARM>> CurrentAlarmChange;

        public EventAction.DBTableWatcher dBTableWatcher;

        private static Object _lock = new Object();
        private static WindownApplication application;
        public static WindownApplication getInstance()
        {
            if (application == null)
            {
                lock (_lock)
                {
                    if (application == null)
                    {
                        application = new WindownApplication();
                    }
                }
            }
            return application;
        }
        #region Redis 
        public const string REDIS_LIST_KEY_VEHICLES = "Redis_List_Vehicles";
        public const string REDIS_KEY_CURRENT_ALARM = "Current Alarm";
        #endregion Redis

        public const string NATS_SUBJECT_VH_INFO_0 = "NATS_SUBJECT_KEY_VH_INFO_{0}_TEST";
        public const string NATS_SUBJECT_LINE_INFO = "NATS_SUBJECT_KEY_LINE_INFO";
        public const string NATS_SUBJECT_CURRENT_ALARM = "NATS_SUBJECT_KEY_CURRENT_ALARMS";
        public WindownApplication()
        {
            ViewerID = getString("Viewer_ID", "");
            webClientManager = WebClientManager.getInstance();

            ObjCacheManager = new ObjCacheManager(this);

            elasticSearchManager = new ElasticSearchManager();

            RailDao = new RAILDao();
            AddressDao = new ADDRESSDao();
            PortIconDao = new PortIconDao();
            PointDao = new POINTDao();
            GroupRailDao = new GROUPRAILSDao();
            SegmentDao = new SegmentDao();
            SectionDao = new SectionDao();
            PortDao = new PortDao();
            VehicleDao = new VehicleDao();
            UserDao = new UserDao();
            UserGroupDao = new UserGroupDao();
            UserFuncDao = new UserFuncDao();
            FunctionCodeDao = new FunctionCodeDao();
            CMD_OHTCDao = new CMD_OHTCDao();
            CMD_MCSDao = new CMD_MCSDao();
            VCMD_MCSDao = new VCMD_MCSDao();
            PortStationDao = new PortStationDao();
            AlarmDao = new AlarmDao();
            MapBLL = new MapBLL(this);
            UserBLL = new UserBLL(this);
            OperationHistoryBLL = new OperationHistoryBLL(this);
            CmdBLL = new CmdBLL(this);
            AlarmBLL = new AlarmBLL(this);
            PortStationBLL = new PortStationBLL(this);
            SegmentBLL = new SegmentBLL(this);
            ObjCacheManager.start();
            ID = ObjCacheManager.MapId;

            LineBLL = new LineBLL(this);
            VehicleBLL = new VehicleBLL(this);
            SysExcuteQualityBLL = new SysExcuteQualityBLL(this);

            dBTableWatcher = new EventAction.DBTableWatcher(this);
            dBTableWatcher.initStart();
            // setEFConnectionString(ObjCacheManager.EFConnectionString);



            SysExcuteQualityQueryService = new SysExcuteQualityQueryService(this);
            //initBackgroundWork();
            //  SysExcuteQualityQueryService = new SysExcuteQualityQueryService();
            switch (WindownApplication.OHxCFormMode)
            {
                case OHxCFormMode.CurrentPlayer:
                    natsManager = new NatsManager(ID, "nats-cluster", ViewerID);
                    redisCacheManager = new RedisCacheManager(ID);
                    SubscriberNatsEvent();
                    SubscriberDBTableWatcherEvent();
                    //SysExcuteQualityQueryService.start();
                    break;
                case OHxCFormMode.HistoricalPlayer:
                    HistoricalReplyService = new HistoricalReplyService(this);
                    //HistoricalReplyService.loadVhHistoricalInfo();
                    break;
            }
            //VehicleBLL.ReguestViewerUpdate();
        }

        private void initBackgroundWork()
        {
            BackgroundWork_ProcessVhPositionUpdate = new BackgroundWorkDriver(new BackgroundWork_ProcessVhPositionUpdate());
        }


        public static string getMessageString(string key, params object[] args)
        {
            return SCApplication.getMessageString(key, args);
        }
        private void SubscriberNatsEvent()
        {
            try
            {
                foreach (var vh in ObjCacheManager.GetVEHICLEs())
                {
                    string subject_id = string.Format(NATS_SUBJECT_VH_INFO_0, vh.VEHICLE_ID);
                    VehicleBLL.SubscriberVehicleInfo(subject_id, VehicleBLL.ProcVehicleInfo);
                    System.Threading.Thread.Sleep(50);
                }
                LineBLL.SubscriberLineInfo(NATS_SUBJECT_LINE_INFO, LineBLL.ProcLineInfo);
                GetNatsManager().Subscriber(NATS_SUBJECT_CURRENT_ALARM, ProcCurrentAlarm);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void SubscriberDBTableWatcherEvent()
        {
            dBTableWatcher.portStationChange += DBTableWatcher_portStationChange;
            dBTableWatcher.userChange += DBTableWatcher_UserChange;
            dBTableWatcher.userGroupChange += DBTableWatcher_UserGroupChange;
            dBTableWatcher.userGroupFuncChange += DBTableWatcher_UserGroupFuncChange;
            dBTableWatcher.mcsCMDChange += DBTableWatcher_MCS_CMDChange;
            dBTableWatcher.segmentChange += DBTableWatcher_segmentChange; ;
            //dBTableWatcher.vehicleChange += DBTableWatcher_VehicleChange;
        }

        private void DBTableWatcher_segmentChange(object sender, EventArgs e)
        {
            ObjCacheManager.updateSegments();
        }

        private void DBTableWatcher_portStationChange(object sender, EventArgs e)
        {
            ObjCacheManager.updatePortStation();
        }
        private void DBTableWatcher_UserChange(object sender, EventArgs e)
        {
            ObjCacheManager.updateUser();
        }
        private void DBTableWatcher_UserGroupChange(object sender, EventArgs e)
        {
            ObjCacheManager.updateUserGroup();
        }

        private void DBTableWatcher_UserGroupFuncChange(object sender, EventArgs e)
        {
            ObjCacheManager.updateUserGroupFunc();
        }

        private void DBTableWatcher_MCS_CMDChange(object sender, EventArgs e)
        {
            ObjCacheManager.updateMCS_CMD();
        }
        //private void DBTableWatcher_VehicleChange(object sender, EventArgs e)
        //{
        //    ObjCacheManager.updateVehicle();
        //}

        private void ProcCurrentAlarm(object sender, StanMsgHandlerArgs handler)
        {
            //List<sc.ALARM> alarms = getCurrentAlarmFromRedis();
            List<sc.ALARM> alarms = AlarmBLL.loadSetAlarm();
            ObjCacheManager.updateAlarm(alarms);
            CurrentAlarmChange?.Invoke(this, alarms);
        }

        public List<sc.ALARM> getCurrentAlarmFromRedis()
        {
            List<sc.ALARM> alarms = new List<sc.ALARM>();
            var redis_values_alarms = GetRedisCacheManager().HashValuesAsync(REDIS_KEY_CURRENT_ALARM).Result;
            foreach (string redis_value_alarm in redis_values_alarms)
            {
                sc.ALARM alarm_obj = (sc.ALARM)JsonConvert.DeserializeObject(redis_value_alarm, typeof(sc.ALARM));
                alarms.Add(alarm_obj);
            }
            return alarms;
        }

        private void setEFConnectionString(string connectionstring)
        {
            string connectionName = "OHTC_DevEntities";
            // Get the configuration file.
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            // Add the connection string.
            ConnectionStringsSection csSection =
                config.ConnectionStrings;
            csSection.ConnectionStrings.Add(
            new ConnectionStringSettings(connectionName, connectionstring));
        }

        public NatsManager GetNatsManager()
        {
            return natsManager;
        }
        public RedisCacheManager GetRedisCacheManager()
        {
            return redisCacheManager;
        }
        public WebClientManager GetWebClientManager()
        {
            return webClientManager;
        }
        public ElasticSearchManager GetElasticSearchManager()
        {
            return elasticSearchManager;
        }


        public HistoricalReplyService GetHistoricalReplyService()
        {
            return HistoricalReplyService;
        }
        public SysExcuteQualityQueryService GetSysExcuteQualityQueryService()
        {
            return SysExcuteQualityQueryService;
        }


        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        private string getString(string key, string defaultValue)
        {
            string rtn = defaultValue;
            try
            {
                rtn = ConfigurationManager.AppSettings.Get(key);
            }
            catch (Exception e)
            {
                logger.Warn("Get Config error[key:{0}][Exception:{1}]", key, e);
            }
            return rtn;
        }

        #region UAS
        public void login(User user)
        {
            login(user.User_ID);
        }

        public void login(string user_id)
        {
            loginUserID = user_id;
            ObjCacheManager.updateLogInUser();
            //refreshLoginUserInfo();
            refresh_UIDisplayFun();//A0.01

        }

        //private void refreshLoginUserInfo()
        //{
        //    foreach (System.Windows.Forms.ToolStripStatusLabel label in statusUserIDLabelDic.Values)
        //    {
        //        try
        //        {
        //            if (label == null || label.IsDisposed)
        //            {
        //                continue;
        //            }
        //            label.Text = loginUserID;
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex, "Exception");
        //        }
        //    }
        //}

        /// <summary>
        /// A0.01
        /// </summary>
        private void refresh_UIDisplayFun()
        {


            foreach (Action<object> action in refresh_UIDisplay_FunDic.Values)
            {
                try
                {
                    if (action == null)
                    {
                        continue;
                    }
                    action(new object());
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
            }
        }
        public void logoff()
        {
            login("");
        }

        /// <summary>
        /// A0.01
        /// </summary>
        /// <param name="refreshFun"></param>
        public void addRefreshUIDisplayFun(Action<object> refreshFun)
        {
            //statusUserIDLabelDic
            if (refresh_UIDisplay_FunDic.ContainsKey(refreshFun.Method.Name))
            {
                refresh_UIDisplay_FunDic[refreshFun.Method.Name] = refreshFun;
            }
            else
            {
                refresh_UIDisplay_FunDic.Add(refreshFun.Method.Name, refreshFun);
            }
        }
        public void addRefreshUIDisplayFun(string formName, Action<object> refreshFun)
        {
            //statusUserIDLabelDic
            if (refresh_UIDisplay_FunDic.ContainsKey(formName))
            {
                refresh_UIDisplay_FunDic[formName] = refreshFun;
            }
            else
            {
                refresh_UIDisplay_FunDic.Add(formName, refreshFun);
            }
        }

        //public void addRe
        #endregion
    }
}
