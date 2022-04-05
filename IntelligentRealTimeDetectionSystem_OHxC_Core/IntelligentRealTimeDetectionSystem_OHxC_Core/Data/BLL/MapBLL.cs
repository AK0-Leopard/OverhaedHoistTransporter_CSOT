using com.mirle.ibg3k0.sc.App;
using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.Data.Dao;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.iibg3k0.ids.ohxc.Data.BLL
{
    public class MapBLL
    {
        BlockZoneDetailDao blockZoneDetailDao = null;
        CSApplication app = null;
        public void Start(CSApplication _app)
        {
            app = _app;
            blockZoneDetailDao = app.BlockZoneDetailDao;
        }



        public Dictionary<string, List<string>> loadBlockZoneByEntrySecDetail()
        {
            Dictionary<string, List<string>> dicBlockZoneDetail = null;
            dicBlockZoneDetail = blockZoneDetailDao.loadBlockZoneByEntrySecDetail(app.DataObjCacheManger.BLOCKZONEDETAILs);
            return dicBlockZoneDetail;
        }



        public string GetMapInfoFromHttp(SCAppConstants.MapInfoDataType dataType)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "MapInfo"
            };
            StringBuilder sb = new StringBuilder();
            //sb.Append($"{nameof(SCAppConstants.MapInfoDataType)}={dataType}");
            sb.Append(dataType);
            result = app.WebClientManager.GetInfoFromServer(action_targets, sb.ToString());
            return result;
        }
        public List<T> GetMapInfoFromHttp<T>(SCAppConstants.MapInfoDataType dataType)
        {
            List<T> objs = null;
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "MapInfo"
            };
            StringBuilder sb = new StringBuilder();
            //sb.Append($"{nameof(SCAppConstants.MapInfoDataType)}={dataType}");
            sb.Append(dataType);
            result = app.WebClientManager.GetInfoFromServer(action_targets, sb.ToString());
            objs = JsonConvert.DeserializeObject<List<T>>(result);
            return objs;
        }

    }
}
