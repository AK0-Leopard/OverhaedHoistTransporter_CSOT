using com.mirle.iibg3k0.ids.ohxc.App;
using NLog;
using System;

namespace com.mirle.iibg3k0.ids.ohxc.TimerAction
{
    public class RegularCheckTimerAction : ITimerAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CSApplication app = null;
        TimeSpan timeOut_10Sec = new TimeSpan(0, 0, 10);

        public RegularCheckTimerAction(string name, long intervalMilliSec)
            : base(name, intervalMilliSec)
        {

        }

        public override void initStart()
        {
            app = CSApplication.getInstance();
        }

        private long syncPoint = 0;
        public override void doProcess(object obj)
        {
            if (System.Threading.Interlocked.Exchange(ref syncPoint, 1) == 0)
            {

                try
                {
                    app.CheckBLL.CheckCanBeCurrentObserver();
                    if (!app.IsCurrentObserver) return;

                    app.RedisCacheManager.stringSetAsync(CSAppConstants.REDIS_KEY_CHECK_SYSTEM_EXIST_FLAG, "", timeOut_10Sec);


                    //app.CheckService.ChcekMCSCommandStatus();
                    app.CheckService.CheckVehiclePosition();
                    //app.CheckService.ChcekVhStatus();
                    app.CheckService.ChcekBlockControlBlockingTimeout();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    System.Threading.Interlocked.Exchange(ref syncPoint, 0);
                }
            }
        }
    }
}
