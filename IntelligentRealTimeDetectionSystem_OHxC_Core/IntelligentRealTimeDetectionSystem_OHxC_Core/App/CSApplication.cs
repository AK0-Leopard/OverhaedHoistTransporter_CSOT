using com.mirle.iibg3k0.ids.ohxc.Common;
using com.mirle.iibg3k0.ids.ohxc.Data;
using com.mirle.iibg3k0.ids.ohxc.Data.BLL;
using com.mirle.iibg3k0.ids.ohxc.Data.Dao;
using com.mirle.iibg3k0.ids.ohxc.Data.Service;
using com.mirle.iibg3k0.ids.ohxc.TimerAction;
using RouteKit;
using System;
using System.Collections.Generic;

namespace com.mirle.iibg3k0.ids.ohxc.App
{
    public class CSApplication
    {
        public static string OhxC_ID { get; private set; } = null;
        public static string ServerName { get; private set; } = null;

        public event EventHandler<bool> ObserverChanged;
        public bool IsCurrentObserver { get; private set; } = false;

        private static Object _lock = new Object();
        private static CSApplication application;
        public DataObjCacheManager DataObjCacheManger { get; } = null;
        public RedisCacheManager RedisCacheManager { get; } = null;
        public NatsManager NatsManager { get; } = null;
        public WebClientManager WebClientManager { get; } = null;


        public BlockZoneDetailDao BlockZoneDetailDao { get; } = null;



        public MapBLL MapBLL { get; } = null;
        public CheckBLL CheckBLL { get; } = null;
        public VehicleBLL VehicleBLL { get; } = null;
        public EventBLL EventBLL { get; } = null;

        public CheckService CheckService { get; } = null;
        public VehicleServer VehicleServer { get; } = null;

        public List<ITimerAction> TimerActions { get; } = new List<ITimerAction>();


        public Guide Guide { get; } = null;

        CSApplication()
        {
            WebClientManager = WebClientManager.getInstance();

            #region Initial Dao
            BlockZoneDetailDao = new BlockZoneDetailDao();
            #endregion Initial Dao


            #region Initial BLL
            MapBLL = new MapBLL();
            CheckBLL = new CheckBLL();
            #endregion Initial BLL
            MapBLL.Start(this);




            DataObjCacheManger = new DataObjCacheManager(this);
            OhxC_ID = DataObjCacheManger.MapId;
            NatsManager = new NatsManager(OhxC_ID, "test-cluster", ServerName);
            RedisCacheManager = new RedisCacheManager(OhxC_ID);

            Guide = new Guide();
            Guide.ImportMap(DataObjCacheManger.SECTIONs, DataObjCacheManger.SEGMENTs);

            VehicleBLL = new VehicleBLL(this);
            EventBLL = new EventBLL(this);



            TimerActions.Add(new RegularCheckTimerAction("RegularCheckTimerAction", 500));

            CheckBLL.Start(this);


            CheckService = new CheckService();
            VehicleServer = new VehicleServer(this);
            CheckService.Start(this);
            VehicleServer.Start();

            InitialTimerStart();
        }

        private void InitialTimerStart()
        {
            foreach (var timerAction in TimerActions)
            {
                timerAction.start();
            }
        }

        public void AsCurrentObserver()
        {
            if (IsCurrentObserver != true)
            {
                IsCurrentObserver = true;
                ObserverChanged?.Invoke(this, IsCurrentObserver);
            }
        }
        public void RetiredCurrentObserver()
        {
            if (IsCurrentObserver != false)
            {
                IsCurrentObserver = false;
                ObserverChanged?.Invoke(this, IsCurrentObserver);
            }
        }


        public static CSApplication getInstance(string ohxc_id, string server_name)
        {
            OhxC_ID = ohxc_id;
            ServerName = server_name;
            return getInstance();
        }
        public static CSApplication getInstance()
        {
            if (application == null)
            {
                lock (_lock)
                {
                    if (application == null)
                    {
                        application = new CSApplication();
                    }
                }
            }
            return application;
        }


    }
}
