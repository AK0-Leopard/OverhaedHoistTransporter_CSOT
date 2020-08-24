using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.sc.Data.DAO;
using com.mirle.ibg3k0.sc.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class AlarmBLL
    {
        AlarmDao alarm_DAO = null;
        public Cache cache { get; private set; } = null;
        public AlarmBLL(WindownApplication _app)
        {
            alarm_DAO = _app.AlarmDao;
            cache = new Cache(_app.ObjCacheManager);
        }

        public List<ALARM> loadSetAlarm()
        {
            List<ALARM> alarms = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                alarms = alarm_DAO.loadSetAlarm(con);
            }
            return alarms;
        }
        public List<ALARM> loadAlarmByConditions(DateTime startDatetime, DateTime endDatetime,
            bool includeSet = false, bool includeClear = false, string eqptID = null, string alarmCode = null)
        {
            List<ALARM> alarms = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                alarms = alarm_DAO.loadAlarmByConditions(con, startDatetime, endDatetime, includeSet, includeClear, eqptID, alarmCode);
            }
            return alarms;
        }


        public class Cache
        {
            ObjCacheManager objCache;
            public Cache(ObjCacheManager _objCache)
            {
                objCache = _objCache;
            }
            public AlarmMap getSuggestion(string eqID, string alarmCode)
            {
                var alarm_map = objCache.GetAlarmMaps().
                                         Where(map => SCUtility.isMatche(map.EQPT_REAL_ID, eqID) &&
                                                      SCUtility.isMatche(map.ALARM_ID, alarmCode)).
                                         FirstOrDefault();
                return alarm_map;
            }
        }


    }
}
