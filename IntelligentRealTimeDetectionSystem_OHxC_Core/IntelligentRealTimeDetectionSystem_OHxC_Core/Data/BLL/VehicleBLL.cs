using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.Data;
using com.mirle.iibg3k0.ids.ohxc.PartialObject;
using RouteKit;
using StackExchange.Redis;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace com.mirle.iibg3k0.ids.ohxc.Data.BLL
{
    public class VehicleBLL
    {
        DataObjCacheManager dataObjCacheManager = null;
        RedisCacheManager redisCacheManager = null;

        Guide guide = null;

        public VehicleBLL(CSApplication app)
        {
            dataObjCacheManager = app.DataObjCacheManger;
            redisCacheManager = app.RedisCacheManager;
            guide = app.Guide;
        }
        public void Start()
        {

        }

        public bool IsPause(AVEHICLE vh)
        {
            return vh.IsBlocking || vh.IsError || vh.IsHIDPause || vh.IsPause;
        }

        public void SubscriberVehicleInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            //CSApplication.getInstance().NatsManager.Subscriber(subject, handler, is_last: true);
            CSApplication.getInstance().NatsManager.Subscriber(subject, handler);
            //app.GetNatsManager().Subscriber(subject, handler);
        }

        #region VH And Each Address Distance
        public void InitialAllVHAndAddressDistance()
        {
            foreach (var vh_info in dataObjCacheManager.VehiclesInfo)
            {
                InitialVhAndEachAdrDistanceHashTable(vh_info.Key);
            }
        }
        private void InitialVhAndEachAdrDistanceHashTable(string vh_id)
        {
            HashEntry[] allAdrOfDistance = new HashEntry[dataObjCacheManager.Address.Count];
            foreach (string adr in dataObjCacheManager.Address)
            {
                double distance = 0;
                int index = dataObjCacheManager.Address.IndexOf(adr);
                HashEntry hashEntry = new HashEntry(adr, distance);
                allAdrOfDistance[index] = hashEntry;
            }
            redisCacheManager.HashSet($"{CSAppConstants.REDIS_KEY_VH_TO_ADR_DISTANCE}_{vh_id}", allAdrOfDistance);
        }
        public void updateVhAndEachAdrDistanceHashTable(string vh_id, string crt_adr)
        {
            foreach (string adr in dataObjCacheManager.Address)
            {
                double distance = 0;
                if (crt_adr != null)
                {
                    string[] routeInfo = guide.DownstreamSearchSection(crt_adr, adr, 0);
                    if (string.IsNullOrWhiteSpace(routeInfo[0])) continue;
                    var routeDetailAndDistance = PaserRoute2SectionsAndDistance(routeInfo[0]);
                    if (routeDetailAndDistance.Equals(default(KeyValuePair<string[], double>)))
                        continue;
                    distance = routeDetailAndDistance.Value;
                }
                redisCacheManager.HashSet($"{CSAppConstants.REDIS_KEY_VH_TO_ADR_DISTANCE}_{vh_id}", adr, distance, When.Always, CommandFlags.FireAndForget);
            }
        }
        private KeyValuePair<string[], double> PaserRoute2SectionsAndDistance(string minRoute)
        {
            if (!minRoute.Contains("=")) return default(KeyValuePair<string[], double>);
            KeyValuePair<string[], double> routeDetailAndDistance = new KeyValuePair<string[], double>();
            string route = minRoute.Split('=')[0];
            string[] routeSection = route.Split(',');
            string distance = minRoute.Split('=')[1];
            double idistance = double.MaxValue;
            if (!double.TryParse(distance, out idistance))
            {

            }
            routeDetailAndDistance = new KeyValuePair<string[], double>(routeSection, idistance);
            return routeDetailAndDistance;
        }

        #endregion VH And Each Address Distance

        public List<AVEHICLE> loadWillPassTheRoadSectionOfVhs(string sec_id)
        {

            var vhs = dataObjCacheManager.VehiclesInfo.Values.
                      Where(vh => vh.WillPassSectionID != null && 
                                  vh.WillPassSectionID.Where(pass_sec => pass_sec.Trim() == sec_id.Trim()).Count() > 0);
            if (vhs != null && vhs.Count() > 0)
                return vhs.ToList();
            else
                return null;

        }

    }
}
