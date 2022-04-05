using com.mirle.ibg3k0.sc.App;
using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.PartialObject;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.Data
{
    public class DataObjCacheManager
    {
        public Dictionary<string, HashEntry[]> VhAndEachAdrDistance { get; } = null;
        public Dictionary<string, List<string>> BlockZoneDetails { get; } = null;
        public Dictionary<string, AVEHICLE> VehiclesInfo { get; } = new Dictionary<string, AVEHICLE>();
        // public List<ibg3k0.sc.ASECTION> Sections { get; } = null;
        public List<string> Address { get; } = null;

        #region MapInfo
        public string MapId { get; private set; } = null;
        public List<ibg3k0.sc.ASECTION> SECTIONs = null;
        public List<ibg3k0.sc.ASEGMENT> SEGMENTs = null;
        public List<ibg3k0.sc.ABLOCKZONEDETAIL> BLOCKZONEDETAILs = null;
        List<AVEHICLE> Vehicles = null;
        #endregion MapInfo



        CSApplication app = null;
        // public string[] Vehicles { get; } = null;

        public DataObjCacheManager(CSApplication _app)
        {
            app = _app;
            //Sections = app.MapBLL.loadAllSection();
            MapId = app.MapBLL.GetMapInfoFromHttp(SCAppConstants.MapInfoDataType.MapID);
            SECTIONs = app.MapBLL.GetMapInfoFromHttp<ibg3k0.sc.ASECTION>(SCAppConstants.MapInfoDataType.Section);
            SEGMENTs = app.MapBLL.GetMapInfoFromHttp<ibg3k0.sc.ASEGMENT>(SCAppConstants.MapInfoDataType.Segment);
            BLOCKZONEDETAILs = app.MapBLL.GetMapInfoFromHttp<ibg3k0.sc.ABLOCKZONEDETAIL>(SCAppConstants.MapInfoDataType.BlockZoneDetail);
            Vehicles = app.MapBLL.GetMapInfoFromHttp<AVEHICLE>(SCAppConstants.MapInfoDataType.Vehicle);
            HashSet<string> hsTemp = new HashSet<string>();
            foreach (var sec in SECTIONs)
            {
                hsTemp.Add(sec.FROM_ADR_ID);
                hsTemp.Add(sec.TO_ADR_ID);
            }
            Address = hsTemp.ToList();

            InitialVehicleInfo();
            //Task.Run(() => RegularUpdateVehicleInfo());
            BlockZoneDetails = app.BlockZoneDetailDao.loadBlockZoneByEntrySecDetail(BLOCKZONEDETAILs);
        }



        public void InitialVehicleInfo()
        {
            //for (int i = 0; i < Vehicles.Length; i++)
            //{
            //    Vehicles[i] = app.RedisCacheManager.ListGetByIndexAsync(CSAppConstants.REDIS_LIST_KEY_VEHICLES, i);
            //    AVEHICLE vh = JsonConvert.DeserializeObject<AVEHICLE>(Vehicles[i]);
            //    //vh.FromTheLastSectionUpdateTimer.Start();
            //    VehiclesInfo.Add(vh.VEHICLE_ID, vh);
            //}

            foreach (AVEHICLE vh in Vehicles)
            {
                VehiclesInfo.Add(vh.VEHICLE_ID, vh);
            }
        }



        //public void RegularUpdateVehicleInfo()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            //TODO 需討論是否把VH的車輛資料移放到Hash中
        //            for (int i = 0; i < Vehicles.Length; i++)
        //            {
        //                Vehicles[i] = app.RedisCacheManager.ListGetByIndexAsync(CSAppConstants.REDIS_LIST_KEY_VEHICLES, i);
        //                AVEHICLE vh = JsonConvert.DeserializeObject<AVEHICLE>(Vehicles[i]);
        //                VehiclesInfo[vh.VEHICLE_ID].setObject(vh);
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        SpinWait.SpinUntil(() => false, 500);
        //    }
        //}

        public AVEHICLE GetVehicleInfo(string vh_id)
        {
            AVEHICLE vh = null;
            vh = VehiclesInfo[vh_id];
            //int vh_num = 0;
            //if (int.TryParse(vh_id.Substring(vh_id.Length - 2), out vh_num))
            //{
            //    string svhInfo = app.RedisCacheManager.ListGetByIndexAsync(CSAppConstants.REDIS_LIST_KEY_VEHICLES, vh_num);
            //    vh = JsonConvert.DeserializeObject<AVEHICLE>(svhInfo);
            //}
            return vh;
        }

        public List<AVEHICLE> GetOnSectionVehicle(string sec_id)
        {
            List<AVEHICLE> vhs = null;
            vhs = VehiclesInfo.
                Where(keyValue => keyValue.Value.CUR_SEC_ID.Trim() == sec_id.Trim()).
                Select(keyValue => keyValue.Value).
                ToList();
            return vhs;
        }

        public double GetSectionDistance(string sec_id)
        {
            var section = SECTIONs.Where(sec => sec.SEC_ID.Trim() == sec_id.Trim()).SingleOrDefault();
            return section.SEC_DIS;
        }


    }
}
