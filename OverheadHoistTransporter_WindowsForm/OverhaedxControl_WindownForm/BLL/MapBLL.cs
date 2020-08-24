using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.DAO;
using com.mirle.ibg3k0.sc.Data.DAO.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class MapBLL
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        WindownApplication app = null;
        ObjCacheManager objCacheManager = null;
        ADDRESSDao adrDAO = null;
        SectionDao sectionDAO = null;
        SegmentDao segmentDAO = null;
        PortDao portDAO = null;
        PortIconDao portIconDAO = null;
        GROUPRAILSDao groupRailDAO = null;
        RAILDao railDAO = null;
        POINTDao pointDAO = null;

        public MapBLL(WindownApplication _app)
        {
            app = _app;
            objCacheManager = app.ObjCacheManager;
            adrDAO = app.AddressDao;
            sectionDAO = app.SectionDao;
            segmentDAO = app.SegmentDao;
            portDAO = app.PortDao;
            groupRailDAO = app.GroupRailDao;
            railDAO = app.RailDao;
            pointDAO = app.PointDao;
            portIconDAO = app.PortIconDao;
        }

        #region Rail
        public List<ARAIL> loadAllRail()
        {

            return objCacheManager.GetRails();
        }



        #endregion Rail
        #region Point       
        public APOINT getPointByID(string point_id)
        {
            APOINT point = objCacheManager.GetPoints().
                           Where(value => value.POINT_ID?.Trim() == point_id?.Trim()).
                           SingleOrDefault();
            return point;
        }

        #endregion Point
        #region GROUPRAILS
        public List<AGROUPRAILS> loadAllGroupRail()
        {
            return objCacheManager.GetGroupRails();
        }
        public List<string> loadAllSectionID()
        {
            List<string> sec_ids = objCacheManager.GetGroupRails().
                Select(value => value.SECTION_ID).Distinct().ToList();
            return sec_ids;
        }
        #endregion GROUPRAILS


        #region Address
        public List<AADDRESS> loadAllAddress()
        {
            return objCacheManager.GetAddresses();
        }
        #endregion Address

        #region Section
        public ASECTION getSectiontByID(string section_id)
        {
            ASECTION section = objCacheManager.GetSections().
                               Where(value => value.SEC_ID?.Trim() == section_id?.Trim()).
                               SingleOrDefault();
            return section;
        }

        public List<ASECTION> loadSectionsBySegmentID(string seg_num)
        {
            List<ASECTION> secs = objCacheManager.GetSections().
                               Where(value => value.SEG_NUM?.Trim() == seg_num?.Trim()).
                               ToList();
            return secs;
        }
        #endregion Section

        #region Segment
        public ASEGMENT getSegmentByNum(string segment_num)
        {
            ASEGMENT segment = objCacheManager.GetSegments().
                               Where(value => value.SEG_NUM?.Trim() == segment_num?.Trim()).
                               SingleOrDefault();
            return segment;
        }
        public List<ASEGMENT> loadAllSegments()
        {
            return objCacheManager.GetSegments();
        }
        #endregion

        #region Port
        public List<APORTSTATION> loadAllPort()
        {
            return objCacheManager.GetPortStations();
        }
        #endregion Port

        #region PortIcon
        public List<APORTICON> loadAllPortIcon()
        {
            return objCacheManager.GetPortIcons();
        }
        #endregion
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
            result = app.GetWebClientManager().GetInfoFromServer(WebClientManager.OHxC_CONTROL_URI, action_targets, sb.ToString());
            return result;
        }
        public List<T> GetMapInfosFromHttp<T>(SCAppConstants.MapInfoDataType dataType)
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
            result = app.GetWebClientManager().GetInfoFromServer(WebClientManager.OHxC_CONTROL_URI, action_targets, sb.ToString());
            objs = JsonConvert.DeserializeObject<List<T>>(result);
            return objs;
        }
        public T GetMapInfoFromHttp<T>(SCAppConstants.MapInfoDataType dataType)
        {
            T obj;
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "MapInfo"
            };
            StringBuilder sb = new StringBuilder();
            //sb.Append($"{nameof(SCAppConstants.MapInfoDataType)}={dataType}");
            sb.Append(dataType);
            result = app.GetWebClientManager().GetInfoFromServer(WebClientManager.OHxC_CONTROL_URI, action_targets, sb.ToString());
            obj = JsonConvert.DeserializeObject<T>(result);
            return obj;
        }
        public async Task<System.Windows.Media.Imaging.BitmapFrame> GetReserveInfoFromHttpAsync()
        {
            byte[] result = new byte[0];
            string[] action_targets = new string[]
            {
                "ReserveInfo",
                "ReserveMap"
            };
            result = await app.GetWebClientManager().GetByteArrayFromServerAsync(action_targets);
            var stream = new MemoryStream(result);
            return System.Windows.Media.Imaging.BitmapFrame.Create(stream);
        }
    }
}
