//*********************************************************************************
//      GuideNew.cs
//*********************************************************************************
// File Name: GuideNew.cs
// Description: 
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------

//**********************************************************************************
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RouteKit
{
    public class GuideNew : IGuide
    {
        int n;//節點數量
        int[,] graph;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        List<InterCost> interCostsList = null;
        List<mySection> sectionList = null;


        Dictionary<int, int> addressIndexDic;
        Dictionary<int, int> IndexAddressDic;
        Dictionary<int, int> sectionIndexDic;


        List<List<int>> banPatternList = null;//設定被Ban的Section會紀錄在該list裡面
        //List<List<int>> banEndPatternList = null;//由於AGV物理條件上的限制，某些Section不能作為路徑的結尾，這些Section會紀錄在這個List裡面。
        //List<List<int>> alternativePathConfigList = null;
        //string BCID = string.Empty;
        private SCApplication scApp = null;




        public GuideNew()
        {



        }

        private void Build(List<ASECTION> sections, List<ASEGMENT> segments, double moveCostForward, double moveCostReverse)
        {
            sectionList = readDataForSectionList(sections, moveCostForward, moveCostReverse);
            initialIndexDic(sectionList);
            n = addressIndexDic.Count;
            createInterCostList(sectionList);

            foreach (ASEGMENT seg in segments)
            {
                if (seg.STATUS != E_SEG_STATUS.Active)
                {
                    foreach (mySection sec in sectionList)
                    {
                        if (sec.seg_num == seg.SEG_NUM.Trim())
                        {
                            banRouteTwoDirect(sec.section_id);
                        }
                    }
                }
            }
            //PresetBanEndPattern(sectionList);

            initGraph();
        }

        public void start(SCApplication scApp,
            double moveCostForward, double moveCostReverse)
        {
            this.scApp = scApp;

            using (DBConnection_EF con = new DBConnection_EF())
            {
                List<ASECTION> sections = scApp.SectionDao.loadAll(con);
                List<ASEGMENT> segments = scApp.SegmentDao.loadAllSegments(con);
                //banEndPatternList = new List<List<int>>();
                banPatternList = new List<List<int>>();
                Build(sections, segments, moveCostForward, moveCostReverse);

            }

            //dao = scApp.SectionDao;
            //seg_dao = scApp.SegmentDao;
            //bll = scApp.MapBLL;
            //con = new DBConnection_EF();
        }
        private List<mySection> readDataForSectionList(List<ASECTION> sections, double moveCostForward, double moveCostReverse)
        {
            List<mySection> sectionList = new List<mySection>();
            foreach (ASECTION sec in sections)
            {
                string section_id = sec.SEC_ID.Trim();
                int address_1 = int.Parse(sec.FROM_ADR_ID);
                int address_2 = int.Parse(sec.TO_ADR_ID);

                double section_dis = sec.SEC_DIS;
                int moveCost_1 = Convert.ToInt32(sec.SEC_DIS);
                int moveCost_2 = 0;

                double movecostF_weight = moveCostForward;
                double movecostR_weight = moveCostReverse;

                //if (sec.SEC_DIR == E_RAIL_DIR.F)
                //{
                //    moveCost_1 = moveCost_1 + (int)(section_dis * movecostF_weight);
                //    moveCost_2 = moveCost_2 + (int)(section_dis * movecostR_weight);
                //}
                //else
                //{
                //    moveCost_1 = moveCost_1 + (int)(section_dis * movecostR_weight);
                //    moveCost_2 = moveCost_2 + (int)(section_dis * movecostF_weight);
                //}
                //string changeSec_1 = SCUtility.Trim(sec.ADR1_CHG_SEC_ID_1);
                //int interCost_1 = sec.ADR1_CHG_SEC_COST_1;
                //string changeSec_2 = SCUtility.Trim(sec.ADR1_CHG_SEC_ID_2);
                //int interCost_2 = sec.ADR1_CHG_SEC_COST_2;
                //string changeSec_3 = SCUtility.Trim(sec.ADR1_CHG_SEC_ID_3);
                //int interCost_3 = sec.ADR1_CHG_SEC_COST_3;
                //string changeSec_4 = SCUtility.Trim(sec.ADR2_CHG_SEC_ID_1);
                //int interCost_4 = sec.ADR2_CHG_SEC_COST_1;
                //string changeSec_5 = SCUtility.Trim(sec.ADR2_CHG_SEC_ID_2);
                //int interCost_5 = sec.ADR2_CHG_SEC_COST_2;
                //string changeSec_6 = SCUtility.Trim(sec.ADR2_CHG_SEC_ID_3);
                //int interCost_6 = sec.ADR2_CHG_SEC_COST_3;
                bool isBanEnd_From2To = false;
                bool isBanEnd_To2From = true;
                int direct = (int)sec.SEC_DIR;

                mySection section = new mySection(section_id, address_1, address_2, moveCost_1, moveCost_2,
                    string.Empty, 0, string.Empty, 0, string.Empty, 0,
                    string.Empty, 0, string.Empty, 0, string.Empty, 0,
                    isBanEnd_From2To, isBanEnd_To2From, direct, sec.SEG_NUM);
                sectionList.Add(section);

            }
            return sectionList;
        }


        private void initGraph()//M0.06
        {
            graph = new int[n, n];

            foreach (mySection s in sectionList)
            {
                int index_1 = addressIndexDic[s.address_1];
                int index_2 = addressIndexDic[s.address_2];
                graph[index_1, index_2] = s.moveCost_1;
                graph[index_2, index_1] = s.moveCost_2;
            }
        }

        #region Application Interface

        object searchSafe_lockObj = new object();
        public string[] tryGetDownstreamSearchSection
           (string startAdr, string endAdr, int flag, bool isIgnoreStatus = false)
        {
            //List<string> busy_segment = scApp.CMDBLL.cache.loadBusySection();
            //List<string> busy_segment = scApp.CMDBLL.cache.loadUnwalkableSections();
            var unwalkable_sections = scApp.CMDBLL.cache.loadUnwalkableSections();

            string[] route = DownstreamSearchSection
                             (startAdr, endAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhAndBusySections);

            if (route == null || route.Count() == 0 || SCUtility.isEmpty(route[0]))
            {
                route = DownstreamSearchSection
                             (startAdr, endAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhSections);
            }
            return route;
        }
        public string[] DownstreamSearchSection(string startAdr, string endAdr, int flag, bool isIgnoreStatus = false, List<string> banSegmentIDList = null)//A0.01
        {
            lock (searchSafe_lockObj)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                Data: $"Enter DownstreamSearchSection method. From Adr:[{startAdr}] To Adr:[{endAdr}] Flag:[{flag}] isIgnoreStatus:[{isIgnoreStatus}]");
                string[] returnArray = new string[2];
                returnArray[0] = string.Empty;
                returnArray[1] = string.Empty;
                startAdr = startAdr.ToUpper();
                endAdr = endAdr.ToUpper();
                int iStartAdr = 0;
                int iEndAdr = 0;
                if (!int.TryParse(startAdr, out iStartAdr) || !int.TryParse(endAdr, out iEndAdr))
                {
                    return returnArray;
                }

                StringBuilder sb = scApp.stringBuilder.GetObject();
                try
                {
                    //List<RouteInfo> routeInfos = getFromToRoutesAddrToAddr(iStartAdr, iEndAdr, isIgnoreStatus);
                    List<RouteInfo> routeInfos = findPathAdrToAdr(iStartAdr, iEndAdr, isIgnoreStatus, banSegmentIDList);

                    #region buildMinRoute
                    if (routeInfos.Count > 0 && routeInfos[0].sections[0] != null)
                    {
                        for (int i = 0; i < routeInfos[0].sections.Count; i++)
                        {
                            if (i != routeInfos[0].sections.Count - 1)
                            {
                                sb.Append(routeInfos[0].sections[i].section_id);
                                sb.Append(",");
                            }
                            else
                            {
                                sb.Append(routeInfos[0].sections[i].section_id);
                            }
                        }
                        if (sb.Length > 0)
                        {
                            sb.Append("=");
                            sb.Append(routeInfos[0].total_cost);
                        }
                        returnArray[0] = sb.ToString();
                        sb.Clear();
                    }
                    else
                    {
                        returnArray[0] = string.Empty;
                    }
                    #endregion buildMinRoute
                    #region buildAllRoute
                    if (flag == 1)
                    {
                        foreach (RouteInfo ri in routeInfos)
                        {
                            for (int i = 0; i < ri.sections.Count; i++)
                            {
                                if (i != ri.sections.Count - 1)
                                {
                                    sb.Append(ri.sections[i].section_id);
                                    sb.Append(",");
                                }
                                else
                                {
                                    sb.Append(ri.sections[i].section_id);
                                }
                            }
                            if (sb.Length > 0)
                            {
                                sb.Append("=");
                                sb.Append(ri.total_cost);
                            }
                            sb.Append(";");
                            //returnArray[0] = sb.ToString();
                        }
                        returnArray[1] = sb.ToString().TrimEnd(';');

                    }
                    else
                    {
                        returnArray[1] = string.Empty;

                    }
                    #endregion buildAllRoute

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    scApp.stringBuilder.PutObject(sb);
                }
                return returnArray;
            }

        }

        public string[] DownstreamSearchSection_FromSecToSec(string fromSec, string toSec, int flag, bool isIncludeLastSec, bool isIgnoreStatus = false, List<string> banSegmentIDList = null)//A0.01
        {
            lock (searchSafe_lockObj)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                Data: $"Enter DownstreamSearchSection_FromSecToSec method. From Sec:[{fromSec}] To Sec:[{toSec}] Flag:[{flag}] isIncludeLastSec:[{isIncludeLastSec}] isIgnoreStatus:[{isIgnoreStatus}]");
                ASECTION fromSection = scApp.SectionBLL.cache.GetSection(fromSec);
                ASECTION toSection = scApp.SectionBLL.cache.GetSection(toSec);
                string startAdr = fromSection.FROM_ADR_ID.Trim();
                string endAdr = toSection.TO_ADR_ID.Trim();
                string[] returnArray = new string[2];
                returnArray[0] = string.Empty;
                returnArray[1] = string.Empty;
                startAdr = startAdr.ToUpper();
                endAdr = endAdr.ToUpper();
                int iStartAdr = 0;
                int iEndAdr = 0;
                if (!int.TryParse(startAdr, out iStartAdr) || !int.TryParse(endAdr, out iEndAdr))
                {
                    return returnArray;
                }

                StringBuilder sb = scApp.stringBuilder.GetObject();
                try
                {
                    //List<RouteInfo> routeInfos = getFromToRoutesAddrToAddr(iStartAdr, iEndAdr, isIgnoreStatus);
                    List<RouteInfo> routeInfos = findPathAdrToAdr(iStartAdr, iEndAdr, isIgnoreStatus, banSegmentIDList);
                    #region buildMinRoute
                    if (routeInfos.Count > 0 && routeInfos[0].sections[0] != null)
                    {
                        if (isIncludeLastSec)
                        {
                            for (int i = 0; i < routeInfos[0].sections.Count; i++)
                            {
                                if (i != routeInfos[0].sections.Count - 1)
                                {
                                    sb.Append(routeInfos[0].sections[i].section_id);
                                    sb.Append(",");
                                }
                                else
                                {
                                    sb.Append(routeInfos[0].sections[i].section_id);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < routeInfos[0].sections.Count - 1; i++)
                            {
                                if (i != routeInfos[0].sections.Count - 2)
                                {
                                    sb.Append(routeInfos[0].sections[i].section_id);
                                    sb.Append(",");
                                }
                                else
                                {
                                    sb.Append(routeInfos[0].sections[i].section_id);
                                }
                            }
                        }

                        if (sb.Length > 0)
                        {
                            sb.Append("=");
                            sb.Append(routeInfos[0].total_cost);
                        }
                        returnArray[0] = sb.ToString();
                        sb.Clear();
                    }
                    else
                    {
                        returnArray[0] = string.Empty;
                    }
                    #endregion buildMinRoute
                    #region buildAllRoute
                    if (flag == 1)
                    {
                        foreach (RouteInfo ri in routeInfos)
                        {
                            if (isIncludeLastSec)
                            {
                                for (int i = 0; i < ri.sections.Count; i++)
                                {
                                    if (i != ri.sections.Count - 1)
                                    {
                                        sb.Append(ri.sections[i].section_id);
                                        sb.Append(",");
                                    }
                                    else
                                    {
                                        sb.Append(ri.sections[i].section_id);
                                    }
                                }
                                if (sb.Length > 0)
                                {
                                    sb.Append("=");
                                    sb.Append(ri.total_cost);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ri.sections.Count - 1; i++)
                                {
                                    if (i != ri.sections.Count - 2)
                                    {
                                        sb.Append(ri.sections[i].section_id);
                                        sb.Append(",");
                                    }
                                    else
                                    {
                                        sb.Append(ri.sections[i].section_id);
                                    }
                                }
                                if (sb.Length > 0)
                                {
                                    sb.Append("=");
                                    sb.Append(ri.total_cost);
                                }
                            }

                            sb.Append(";");
                            //returnArray[0] = sb.ToString();
                        }
                        returnArray[1] = sb.ToString().TrimEnd(';');

                    }
                    else
                    {
                        returnArray[1] = string.Empty;

                    }
                    #endregion buildAllRoute

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    scApp.stringBuilder.PutObject(sb);
                }
                return returnArray;
            }

        }


        public string[] DownstreamSearchSection_FromSecToAdr(string fromSec, string toAdr, int flag, bool isIgnoreStatus = false, List<string> banSegmentIDList = null)//A0.01
        {
            lock (searchSafe_lockObj)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                Data: $"Enter DownstreamSearchSection_FromSecToAdr method. From Sec:[{fromSec}] To Adr:[{toAdr}] Flag:[{flag}] isIgnoreStatus:[{isIgnoreStatus}]");
                ASECTION fromSection = scApp.SectionBLL.cache.GetSection(fromSec);
                string startAdr = fromSection.TO_ADR_ID.Trim();
                bool needFirstSec = true;
                string[] returnArray = new string[2];
                returnArray[0] = string.Empty;
                returnArray[1] = string.Empty;
                startAdr = startAdr.ToUpper();
                toAdr = toAdr.ToUpper();
                int iStartAdr = 0;
                int iEndAdr = 0;
                if (!int.TryParse(startAdr, out iStartAdr) || !int.TryParse(toAdr, out iEndAdr))
                {
                    return returnArray;
                }
                if (needFirstSec)
                {
                    ASEGMENT seg = scApp.SegmentBLL.cache.GetSegment(fromSection.SEG_NUM);
                    if (seg.STATUS != E_SEG_STATUS.Active)
                    {
                        return returnArray;
                    }
                }


                //List<Section> fromSectionList = new List<Section>();
                //List<Section> toSectionList = new List<Section>();
                StringBuilder sb = scApp.stringBuilder.GetObject();
                try
                {
                    List<RouteInfo> routeInfos = new List<RouteInfo>();
                    if (startAdr == toAdr)
                    {
                        List<mySection> _sections = new List<mySection>();
                        mySection s = getSectionByTwoNode(int.Parse(fromSection.FROM_ADR_ID), int.Parse(fromSection.TO_ADR_ID));
                        _sections.Add(s);
                        RouteInfo _ri = new RouteInfo(_sections, s.moveCost_1);
                        routeInfos.Add(_ri);
                    }
                    else if (needFirstSec)
                    {

                        //routeInfos = getFromToRoutesAddrToAddr(iStartAdr, iEndAdr, isIgnoreStatus);
                        routeInfos = findPathAdrToAdr(iStartAdr, iEndAdr, isIgnoreStatus, banSegmentIDList);
                        foreach (RouteInfo ri in routeInfos)
                        {
                            mySection s = getSectionByTwoNode(int.Parse(fromSection.FROM_ADR_ID), int.Parse(fromSection.TO_ADR_ID));
                            ri.sections.Insert(0, s);
                            ri.addresses.Insert(0, iEndAdr);
                            ri.total_cost += s.moveCost_1;
                        }
                    }

                    #region buildMinRoute
                    if (routeInfos.Count > 0 && routeInfos[0].sections[0] != null)
                    {
                        for (int i = 0; i < routeInfos[0].sections.Count; i++)
                        {
                            if (i != routeInfos[0].sections.Count - 1)
                            {
                                sb.Append(routeInfos[0].sections[i].section_id);
                                sb.Append(",");
                            }
                            else
                            {
                                sb.Append(routeInfos[0].sections[i].section_id);
                            }
                        }
                        if (sb.Length > 0)
                        {
                            sb.Append("=");
                            sb.Append(routeInfos[0].total_cost);
                        }
                        returnArray[0] = sb.ToString();
                        sb.Clear();
                    }
                    else
                    {
                        returnArray[0] = string.Empty;
                    }
                    #endregion buildMinRoute
                    #region buildAllRoute
                    if (flag == 1)
                    {
                        foreach (RouteInfo ri in routeInfos)
                        {
                            for (int i = 0; i < ri.sections.Count; i++)
                            {
                                if (i != ri.sections.Count - 1)
                                {
                                    sb.Append(ri.sections[i].section_id);
                                    sb.Append(",");
                                }
                                else
                                {
                                    sb.Append(ri.sections[i].section_id);
                                }
                            }
                            if (sb.Length > 0)
                            {
                                sb.Append("=");
                                sb.Append(ri.total_cost);
                            }
                            sb.Append(";");
                            //returnArray[0] = sb.ToString();
                        }
                        returnArray[1] = sb.ToString().TrimEnd(';');

                    }
                    else
                    {
                        returnArray[1] = string.Empty;

                    }
                    #endregion buildAllRoute

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    scApp.stringBuilder.PutObject(sb);
                }
                return returnArray;
            }
        }

        public List<RouteInfo> findPathAdrToAdr(int from_addr, int to_addr, bool isIgnoreStatus, List<string> banSegmentIDList = null)
        {
            try
            {

                if (from_addr != to_addr)//如果沒有From To Address相同的情況，就直接呼叫路徑查找。
                {
                    return getFromToRoutesAddrToAddr(from_addr, to_addr, isIgnoreStatus, banSegmentIDList);
                }
                else
                {
                    List<RouteInfo> routeInfos = new List<RouteInfo>();
                    List<ASECTION> sections = scApp.SectionBLL.cache.GetSectionsByFromAddress(from_addr.ToString());
                    if (sections.Count == 0)
                    {
                        return routeInfos;
                    }
                    else
                    {
                        foreach (ASECTION s in sections)
                        {
                            if (!isIgnoreStatus)
                            {
                                ASEGMENT seg = scApp.SegmentBLL.cache.GetSegment(s.SEG_NUM);
                                if (seg == null)
                                {
                                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                                    Data: $"In findPathAdrToAdr. find segment fail, Section:[{s.SEC_ID}].");
                                    continue;
                                }
                                if (seg.STATUS == E_SEG_STATUS.Closed)
                                {
                                    continue;
                                }
                            }

                            string newFrom_adr = s.TO_ADR_ID;
                            List<RouteInfo> routeInfos_temp = getFromToRoutesAddrToAddr(int.Parse(newFrom_adr), to_addr, isIgnoreStatus, banSegmentIDList);
                            if (routeInfos_temp != null && routeInfos_temp.Count != 0)
                            {
                                routeInfos_temp[0].addresses.Insert(0, from_addr);
                                routeInfos_temp[0].sections.Insert(0, getSectionByTwoNode(from_addr, int.Parse(newFrom_adr)));
                                routeInfos_temp[0].total_cost = routeInfos_temp[0].total_cost + Convert.ToInt32(s.SEC_DIS);
                            }
                            else
                            {
                                continue;
                            }

                            if (routeInfos == null || routeInfos.Count == 0)
                            {
                                routeInfos = routeInfos_temp;
                            }
                            else if (routeInfos[0].total_cost > routeInfos_temp[0].total_cost)
                            {
                                routeInfos = routeInfos_temp;
                            }
                            else
                            {
                                continue;
                            }

                        }
                        return routeInfos;
                    }


                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Error, Class: nameof(GuideNew), Device: "OHT",
                Data: $"FindPathAdrToAdr have exception happen.Exception:[{ex.ToString()}] From Adr: [{from_addr}] To Adr: [{to_addr}]");
                return new List<RouteInfo>();

            }

        }

        private ConcurrentDictionary<string, GuideQuickSearch> dicGuideQuickSearch =
         new ConcurrentDictionary<string, GuideQuickSearch>();
        private ConcurrentDictionary<string, GuideQuickSearch> dicGuideQuickSearchForMCS =
         new ConcurrentDictionary<string, GuideQuickSearch>();

        public class GuideQuickSearch
        {
            public bool isWalker;
            public double totalCost;
            public string[] GuideSections;
            public GuideQuickSearch(bool _isWalker, double _totalCost, string[] guideSections)
            {
                isWalker = _isWalker;
                totalCost = _totalCost;
                GuideSections = guideSections;
            }
        }

        public bool checkRoadIsWalkable(string from_adr, string to_adr)
        {
            return checkRoadIsWalkable(from_adr, to_adr, false);
        }

        public bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand)
        {
            //KeyValuePair<string[], double> route_distance;
            return checkRoadIsWalkable(from_adr, to_adr, isMaintainDeviceCommand, isRecalculate: false, out double route_distance, out var guideSections);
        }
        //public bool checkRoadIsWalkable(string from_adr, string to_adr, out KeyValuePair<string[], double> route_distance)
        public bool checkRoadIsWalkable(string from_adr, string to_adr, out double route_distance)
        {
            return checkRoadIsWalkable(from_adr, to_adr, false, isRecalculate: false, out route_distance, out var guideSections);
        }
        public bool checkRoadIsWalkable(string from_adr, string to_adr, bool isRecalculate, out double route_distance, out string[] route_sections)
        {
            return checkRoadIsWalkable(from_adr, to_adr, false, isRecalculate: isRecalculate, out route_distance, out route_sections);
        }
        public bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand, out double route_distance)
        {
            return checkRoadIsWalkable(from_adr, to_adr, isMaintainDeviceCommand, isRecalculate: false, out route_distance, out var route_sections);
        }
        //public bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand, out KeyValuePair<string[], double> route_distance)
        public bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand, bool isRecalculate, out double route_distance, out string[] guideSections)
        {
            //route_distance = double.MaxValue;
            string key = $"{SCUtility.Trim(from_adr, true)},{SCUtility.Trim(to_adr, true)}";
            bool is_exist = dicGuideQuickSearch.TryGetValue(key, out GuideQuickSearch guideQuickSearch);
            if (isMaintainDeviceCommand != true && is_exist && !isRecalculate)
            {
                route_distance = guideQuickSearch.totalCost;
                guideSections = guideQuickSearch.GuideSections;
                //DebugParameter.GuideQuickSearchTimes++;
                return guideQuickSearch.isWalker;
            }
            else
            {

                //string[] route = DownstreamSearchSection
                //                     (from_adr, to_adr, 1);
                string[] route = tryGetDownstreamSearchSection
                                     (from_adr, to_adr, 1);
                if (SCUtility.isEmpty(route[0]))
                {
                    route_distance = double.MaxValue;
                    guideSections = new string[0];
                    return false;
                }
                string[] AllRoute = route[1].Split(';');
                List<KeyValuePair<string[], double>> routeDetailAndDistance = PaserRoute2SectionsAndDistance(AllRoute);
                bool isWalkable = false;

                //判斷找出來的路徑是否有經過MaintainDevice的，有的話要將他過濾掉
                if (!isMaintainDeviceCommand)
                {
                    foreach (var routeDetial in routeDetailAndDistance.ToList())
                    {
                        List<string> maintain_device_ids = scApp.EquipmentBLL.cache.GetAllMaintainDeviceSegments();
                        List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                        string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                        bool is_include_maintain_device_segment = secOfSegments.Where(seg => maintain_device_ids.Contains(seg)).Count() != 0;
                        if (is_include_maintain_device_segment)
                        {
                            routeDetailAndDistance.Remove(routeDetial);
                        }
                    }
                }

                //判斷將要走過的路上是否有故障車 A0.02
                foreach (var routeDetial in routeDetailAndDistance.ToList())
                {
                    List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                    List<AVEHICLE> vhs = scApp.VehicleBLL.loadAllErrorVehicle();
                    foreach (AVEHICLE vh in vhs)
                    {
                        bool IsErrorVhOnPassSection = lstSec.Where(sec => sec.SEC_ID.Trim() == vh.CUR_SEC_ID.Trim()).Count() > 0;
                        if (IsErrorVhOnPassSection)
                        {
                            routeDetailAndDistance.Remove(routeDetial);
                            if (routeDetailAndDistance.Count == 0)
                            {
                                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                                   Data: $"Can't find the way on check road is walkable.Because block by error vehicle [{vh.VEHICLE_ID}] on sec [{vh.CUR_SEC_ID}]");
                            }
                        }
                    }
                }

                //判斷是否有路徑是被Disable或是Pre disable
                if (scApp.getEQObjCacheManager().getLine().SegmentPreDisableExcuting)
                {
                    List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();
                    //string[] AllRoute = route[1].Split(';');
                    //List<KeyValuePair<string[], double>> routeDetailAndDistance = PaserRoute2SectionsAndDistance(AllRoute);
                    foreach (var routeDetial in routeDetailAndDistance.ToList())
                    {
                        List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                        string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                        bool isIncludePassSeg = secOfSegments.Where(seg => nonActiveSeg.Contains(seg)).Count() != 0;
                        if (isIncludePassSeg)
                        {
                            routeDetailAndDistance.Remove(routeDetial);
                        }
                    }
                }
                if (routeDetailAndDistance.Count > 0)
                {
                    //route_distance = routeDetailAndDistance.OrderBy(keyValue => keyValue.Value).FirstOrDefault();
                    var route_detail_and_distance = routeDetailAndDistance.OrderBy(keyValue => keyValue.Value).FirstOrDefault();
                    route_distance = route_detail_and_distance.Value;
                    guideSections = route_detail_and_distance.Key;
                    isWalkable = true;
                    dicGuideQuickSearch.TryAdd(key, new GuideQuickSearch(isWalkable, route_distance, guideSections));
                }
                else
                {
                    route_distance = double.MaxValue;
                    guideSections = new string[0];
                    isWalkable = false;
                }
                //else
                //{
                //    string[] routeSection;
                //    double idistance;
                //    RouteInfo2KeyValue(route[0], out routeSection, out idistance);
                //    route_distance = new KeyValuePair<string[], double>(routeSection, idistance);
                //    isWalkable = true;
                //}

                return isWalkable;
            }
        }



        public bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr)
        {
            return checkRoadIsWalkableForMCSCommand(from_adr, to_adr, false);
        }

        public bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr, bool isMaintainDeviceCommand)
        {
            //KeyValuePair<string[], double> route_distance;
            double route_distance;
            return checkRoadIsWalkableForMCSCommand(from_adr, to_adr, isMaintainDeviceCommand, out route_distance);
        }

        //public bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr, bool isMaintainDeviceCommand, out KeyValuePair<string[], double> route_distance)
        public bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr, bool isMaintainDeviceCommand, out double route_distance)
        {
            //route_distance = default(KeyValuePair<string[], double>);
            string key = $"{SCUtility.Trim(from_adr, true)},{SCUtility.Trim(to_adr, true)}";
            bool is_exist = dicGuideQuickSearchForMCS.TryGetValue(key, out GuideQuickSearch guideQuickSearch);
            if (isMaintainDeviceCommand != true && is_exist)
            {
                route_distance = guideQuickSearch.totalCost;
                return guideQuickSearch.isWalker;
            }
            else
            {

                string[] route = DownstreamSearchSection
                                 (from_adr, to_adr, 1);
                if (SCUtility.isEmpty(route[0]))
                {
                    route_distance = double.MaxValue;
                    return false;
                }
                string[] AllRoute = route[1].Split(';');
                List<KeyValuePair<string[], double>> routeDetailAndDistance = PaserRoute2SectionsAndDistance(AllRoute);
                bool isWalkable = false;

                //判斷找出來的路徑是否有經過MaintainDevice的，有的話要將他過濾掉
                if (!isMaintainDeviceCommand)
                {
                    foreach (var routeDetial in routeDetailAndDistance.ToList())
                    {
                        List<string> maintain_device_ids = scApp.EquipmentBLL.cache.GetAllMaintainDeviceSegments();
                        List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                        string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                        bool is_include_maintain_device_segment = secOfSegments.Where(seg => maintain_device_ids.Contains(seg)).Count() != 0;
                        if (is_include_maintain_device_segment)
                        {
                            routeDetailAndDistance.Remove(routeDetial);
                        }
                    }
                }

                //判斷是否有路徑是被Disable或是Pre disable
                if (scApp.getEQObjCacheManager().getLine().SegmentPreDisableExcuting)
                {
                    List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();
                    //string[] AllRoute = route[1].Split(';');
                    //List<KeyValuePair<string[], double>> routeDetailAndDistance = PaserRoute2SectionsAndDistance(AllRoute);
                    foreach (var routeDetial in routeDetailAndDistance.ToList())
                    {
                        List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                        string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                        bool isIncludePassSeg = secOfSegments.Where(seg => nonActiveSeg.Contains(seg)).Count() != 0;
                        if (isIncludePassSeg)
                        {
                            routeDetailAndDistance.Remove(routeDetial);
                        }
                    }
                }
                if (routeDetailAndDistance.Count > 0)
                {
                    //route_distance = routeDetailAndDistance.OrderBy(keyValue => keyValue.Value).FirstOrDefault();
                    //route_distance = routeDetailAndDistance.OrderBy(keyValue => keyValue.Value).FirstOrDefault().Value;
                    var route_detail_and_distance = routeDetailAndDistance.OrderBy(keyValue => keyValue.Value).FirstOrDefault();
                    route_distance = route_detail_and_distance.Value;
                    var guideSections = route_detail_and_distance.Key;
                    isWalkable = true;
                    dicGuideQuickSearchForMCS.TryAdd(key, new GuideQuickSearch(isWalkable, route_distance, guideSections));
                }
                else
                {
                    route_distance = double.MaxValue;
                    isWalkable = false;
                }
                //else
                //{
                //    string[] routeSection;
                //    double idistance;
                //    RouteInfo2KeyValue(route[0], out routeSection, out idistance);
                //    route_distance = new KeyValuePair<string[], double>(routeSection, idistance);
                //    isWalkable = true;
                //}
                return isWalkable;
            }
        }
        public void clearAllDirGuideQuickSearchInfo()
        {
            dicGuideQuickSearch.Clear();
            dicGuideQuickSearchForMCS.Clear();
        }
        private List<KeyValuePair<string[], double>> PaserRoute2SectionsAndDistance(string[] AllRoute)
        {
            List<KeyValuePair<string[], double>> routeDetailAndDistance = new List<KeyValuePair<string[], double>>();
            foreach (string routeDetial in AllRoute)
            {
                string[] routeSection;
                double idistance;
                RouteInfo2KeyValue(routeDetial, out routeSection, out idistance);
                routeDetailAndDistance.Add(new KeyValuePair<string[], double>(routeSection, idistance));
            }
            return routeDetailAndDistance;
        }

        private void RouteInfo2KeyValue(string routeDetial, out string[] routeSection, out double idistance)
        {
            string route = routeDetial.Split('=')[0];
            routeSection = route.Split(',');
            string distance = routeDetial.Split('=')[1];
            idistance = double.MaxValue;
            if (!double.TryParse(distance, out idistance))
            {
                logger.Warn($"fun:{nameof(PaserRoute2SectionsAndDistance)},parse distance fail.Route:{route},distance:{distance}");
            }
        }

        //        public List<RouteInfo> getFromToRoutesAddrToAddr(int from_addr, int to_addr, bool isIgnoreStatus)//M0.06
        //        {

        //            Stopwatch stopWatch = new Stopwatch();
        //            stopWatch.Start();

        //            LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
        //            Data: $"Start Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}]");
        //            if (from_addr == to_addr)
        //            {
        //                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
        //Data: $"Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}] is the same ，return empty result");
        //                return new List<RouteInfo>();
        //            }

        //            int pathAddCount = 0;
        //            PathInfo pathInfo = new PathInfo();
        //            try
        //            {
        //                #region Dijkstra
        //                int[,] localGraph = new int[n, n];
        //                for (int i = 0; i < n; i++)//複製一個Cost圖出來
        //                {
        //                    for (int j = 0; j < n; j++)
        //                    {
        //                        localGraph[i, j] = graph[i, j];
        //                    }
        //                }
        //                if (!isIgnoreStatus)
        //                {
        //                    foreach (List<int> banPattern in banPatternList)//Ban掉的路徑輸入進計算Cost的圖
        //                    {
        //                        int ban_index_from = addressIndexDic[banPattern[0]];
        //                        int ban_index_to = addressIndexDic[banPattern[1]];
        //                        localGraph[ban_index_from, ban_index_to] = 0;//Cost 0代表無法到達
        //                    }
        //                }

        //                //Cost圖準備完成

        //                bool isFirstTime = true;
        //                do
        //                {
        //                    if (!isFirstTime)
        //                    {
        //                        int ban_index_from = addressIndexDic[pathInfo.path[pathInfo.path.Count - 2]];
        //                        int ban_index_to = addressIndexDic[pathInfo.path[pathInfo.path.Count - 1]];
        //                        localGraph[ban_index_from, ban_index_to] = 0;//Cost 0代表無法到達
        //                        pathInfo = new PathInfo();
        //                    }
        //                    else
        //                    {
        //                        isFirstTime = false;
        //                    }
        //                    int[] dist;
        //                    int[] previous;
        //                    int index_from = addressIndexDic[from_addr];
        //                    int index_to = addressIndexDic[to_addr];
        //                    dijkstra(localGraph, index_from, index_to, out dist, out previous);
        //                    //Dijkstra演算結束
        //                    if (dist[index_to] == int.MaxValue)//表示無法到達
        //                    {
        //                        break;
        //                    }
        //                    pathInfo.total_cost = dist[index_to];

        //                    int index = index_to;
        //                    while (previous[index] != index_from)
        //                    {
        //                        pathInfo.path.Add(IndexAddressDic[index]);
        //                        index = previous[index];
        //                        pathAddCount++;
        //                    }
        //                    pathInfo.path.Add(IndexAddressDic[index]);
        //                    pathInfo.path.Add(IndexAddressDic[previous[index]]);
        //                    pathInfo.path.Reverse();
        //                }
        //                while (false);

        //                List<RouteInfo> routeList = new List<RouteInfo>();
        //                if (pathInfo.path.Count != 0)
        //                {
        //                    List<mySection> route = new List<mySection>();
        //                    for (int i = 0; i < pathInfo.path.Count - 1; i++)
        //                    {
        //                        mySection section = getSectionByTwoNode(pathInfo.path[i], pathInfo.path[i + 1]);
        //                        route.Add(section);
        //                    }
        //                    routeList.Add(new RouteInfo(route, pathInfo.path, pathInfo.total_cost));
        //                }
        //                else
        //                {
        //                    //do nothing
        //                }
        //                stopWatch.Stop();
        //                // Get the elapsed time as a TimeSpan value.
        //                TimeSpan ts = stopWatch.Elapsed;

        //                // Format and display the TimeSpan value.
        //                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //                    ts.Hours, ts.Minutes, ts.Seconds,
        //                    ts.Milliseconds / 10);
        //                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
        //                Data: $"End Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}] Using Time:[{elapsedTime}]");
        //                //                LogHelper.Log(logger: logger, LogLevel: LogLevel.Error, Class: nameof(GuideNew), Device: "OHT",
        //                //Data: $"Find Path using Dijkstra have exception happen. From Adr: [{from_addr}] To Adr: [{to_addr}] Path Add Count: [{pathAddCount}] PathInfo:[{pathInfo.path}]");
        //                return routeList;
        //                #endregion Dijkstra
        //            }
        //            catch (Exception ex)
        //            {
        //                stopWatch.Stop();
        //                // Get the elapsed time as a TimeSpan value.
        //                TimeSpan ts = stopWatch.Elapsed;

        //                // Format and display the TimeSpan value.
        //                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //                    ts.Hours, ts.Minutes, ts.Seconds,
        //                    ts.Milliseconds / 10);
        //                LogHelper.Log(logger: logger, LogLevel: LogLevel.Error, Class: nameof(GuideNew), Device: "OHT",
        //                Data: $"Find Path using Dijkstra have exception happen.Exception:[{ex.ToString()}] From Adr: [{from_addr}] To Adr: [{to_addr}] Path Add Count: [{pathAddCount}] Using Time:[{elapsedTime}]");
        //                return new List<RouteInfo>();
        //            }


        //        }
        public List<RouteInfo> getFromToRoutesAddrToAddr(int from_addr, int to_addr, bool isIgnoreStatus, List<string> banSegmentIDList = null)//M0.06
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
            LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(GuideNew), Device: "OHT",
            Data: $"Start Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}]");
            if (from_addr == to_addr)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
        Data: $"Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}] is the same ，return empty result");
                return new List<RouteInfo>();
            }

            int pathAddCount = 0;
            PathInfo pathInfo = new PathInfo();
            try
            {
                #region Dijkstra
                int[,] localGraph = new int[n, n];
                for (int i = 0; i < n; i++)//複製一個Cost圖出來
                {
                    for (int j = 0; j < n; j++)
                    {
                        localGraph[i, j] = graph[i, j];
                    }
                }
                if (!isIgnoreStatus)
                {
                    if (banSegmentIDList != null)
                    {
                        #region 建立新的banPatternList
                        List<List<int>> _banPatternList = new List<List<int>>(banPatternList);
                        foreach (string segmentID in banSegmentIDList)
                        {
                            if (!int.TryParse(segmentID, out int sec_id)) continue;
                            mySection section = getSectionByID(sec_id);
                            if (section != null)
                            {
                                List<int> banPattern_1 = new List<int>();
                                banPattern_1.Add(section.address_1);
                                banPattern_1.Add(section.address_2);

                                List<int> banPattern_2 = new List<int>();
                                banPattern_2.Add(section.address_2);
                                banPattern_2.Add(section.address_1);
                                bool isExist_1 = false;
                                foreach (List<int> p in _banPatternList)
                                {
                                    if (p[0] == banPattern_1[0] && p[1] == banPattern_1[1])
                                    {
                                        isExist_1 = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (!isExist_1)
                                {
                                    _banPatternList.Add(banPattern_1);
                                }

                                bool isExist_2 = false;
                                foreach (List<int> p in _banPatternList)
                                {
                                    if (p[0] == banPattern_2[0] && p[1] == banPattern_2[1])
                                    {
                                        isExist_2 = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (!isExist_2)
                                {
                                    _banPatternList.Add(banPattern_2);
                                }
                            }
                        }
                        #endregion 建立新的banPatternList
                        foreach (List<int> banPattern in _banPatternList)//Ban掉的路徑輸入進計算Cost的圖
                        {
                            int ban_index_from = addressIndexDic[banPattern[0]];
                            int ban_index_to = addressIndexDic[banPattern[1]];
                            localGraph[ban_index_from, ban_index_to] = 0;//Cost 0代表無法到達
                        }
                    }
                    else
                    {

                        foreach (List<int> banPattern in banPatternList)//Ban掉的路徑輸入進計算Cost的圖
                        {
                            int ban_index_from = addressIndexDic[banPattern[0]];
                            int ban_index_to = addressIndexDic[banPattern[1]];
                            localGraph[ban_index_from, ban_index_to] = 0;//Cost 0代表無法到達
                        }
                    }

                }

                //Cost圖準備完成

                bool isFirstTime = true;
                do
                {
                    if (!isFirstTime)
                    {
                        int ban_index_from = addressIndexDic[pathInfo.path[pathInfo.path.Count - 2]];
                        int ban_index_to = addressIndexDic[pathInfo.path[pathInfo.path.Count - 1]];
                        localGraph[ban_index_from, ban_index_to] = 0;//Cost 0代表無法到達
                        pathInfo = new PathInfo();
                    }
                    else
                    {
                        isFirstTime = false;
                    }
                    int[] dist;
                    int[] previous;
                    int index_from = addressIndexDic[from_addr];
                    int index_to = addressIndexDic[to_addr];
                    dijkstra(localGraph, index_from, index_to, out dist, out previous);
                    //Dijkstra演算結束
                    if (dist[index_to] == int.MaxValue)//表示無法到達
                    {
                        break;
                    }
                    pathInfo.total_cost = dist[index_to];

                    int index = index_to;
                    while (previous[index] != index_from)
                    {
                        pathInfo.path.Add(IndexAddressDic[index]);
                        index = previous[index];
                        pathAddCount++;
                    }
                    pathInfo.path.Add(IndexAddressDic[index]);
                    pathInfo.path.Add(IndexAddressDic[previous[index]]);
                    pathInfo.path.Reverse();
                }
                while (false);

                List<RouteInfo> routeList = new List<RouteInfo>();
                if (pathInfo.path.Count != 0)
                {
                    List<mySection> route = new List<mySection>();
                    for (int i = 0; i < pathInfo.path.Count - 1; i++)
                    {
                        mySection section = getSectionByTwoNode(pathInfo.path[i], pathInfo.path[i + 1]);
                        route.Add(section);
                    }
                    routeList.Add(new RouteInfo(route, pathInfo.path, pathInfo.total_cost));
                }
                else
                {
                    //do nothing
                }
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GuideNew), Device: "OHT",
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(GuideNew), Device: "OHT",
                Data: $"End Dijkstra algorithm find path. From Adr: [{from_addr}] To Adr: [{to_addr}] Using Time:[{elapsedTime}]");
                //                LogHelper.Log(logger: logger, LogLevel: LogLevel.Error, Class: nameof(GuideNew), Device: "OHT",
                //Data: $"Find Path using Dijkstra have exception happen. From Adr: [{from_addr}] To Adr: [{to_addr}] Path Add Count: [{pathAddCount}] PathInfo:[{pathInfo.path}]");
                return routeList;
                #endregion Dijkstra
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Error, Class: nameof(GuideNew), Device: "OHT",
                Data: $"Find Path using Dijkstra have exception happen.Exception:[{ex.ToString()}] From Adr: [{from_addr}] To Adr: [{to_addr}] Path Add Count: [{pathAddCount}] Using Time:[{elapsedTime}]");
                return new List<RouteInfo>();
            }


        }


        public void dijkstra(int[,] graph, int index_from, int index_to, out int[] dist, out int[] previous)
        {
            dist = new int[n];
            previous = new int[n];
            bool[] sptSet = new bool[n];

            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
            dist[index_from] = 0;
            for (int count = 0; count < n - 1; count++)
            {
                int u = minDistance(dist, sptSet);
                if (u == index_to) break;//已經找到目的地，不用再找了。
                sptSet[u] = true;
                for (int v = 0; v < n; v++)
                {
                    int intercostFirstPoint = previous[u];
                    int intercostSecondPoint = u;
                    int intercostThirdPoint = v;
                    int inter_cost = 0;
                    InterCost interCost = interCostsList.Find((InterCost c) => c.firstPoint == intercostFirstPoint && c.secondPoint == intercostSecondPoint && c.thirdPoint == intercostThirdPoint);
                    if (interCost == null)
                    {
                        inter_cost = 0;
                    }
                    else
                    {
                        inter_cost = interCost.cost;
                    }
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] + inter_cost < dist[v])
                    {
                        previous[v] = u;
                        dist[v] = dist[u] + graph[u, v] + inter_cost;
                    }
                }
            }
        }

        int minDistance(int[] dist,
                bool[] sptSet)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < n; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }



        //public (List<RouteInfo> stratFromRouteInfoList, List<RouteInfo> fromToRouteInfoList) getStartFromThenFromToRoutesAddrToAddrToAddr(int start_addr, int from_addr, int to_addr)
        //{
        //    List<RouteInfo> startFromRouteList = getFromToRoutesAddrToAddr(start_addr, from_addr);
        //    List<RouteInfo> fromToRouteList = getFromToRoutesAddrToAddr(from_addr, to_addr);
        //    return (startFromRouteList, fromToRouteList);
        //}
        /////////////////////////////




        public ASEGMENT OpenSegment(string strSegCode)
        {
            ASEGMENT seg_do = null;
            try
            {
                seg_do = scApp.MapBLL.EnableSegment(strSegCode);

                foreach (mySection sec in sectionList)
                {
                    if (sec.seg_num == strSegCode)
                    {
                        unbanRouteTwoDirect(sec.section_id);
                    }
                }
                clearAllDirGuideQuickSearchInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return seg_do;
        }
        public ASEGMENT OpenSegment(string strSegCode, ASEGMENT.DisableType disableType)
        {
            ASEGMENT seg_vo = null;
            ASEGMENT seg_do = null;
            try
            {
                seg_vo = scApp.SegmentBLL.cache.GetSegment(strSegCode);
                lock (seg_vo)
                {
                    seg_do = scApp.MapBLL.EnableSegment(strSegCode, disableType);
                    foreach (mySection sec in sectionList)
                    {
                        if (sec.seg_num == strSegCode)
                        {
                            unbanRouteTwoDirect(sec.section_id);
                        }
                    }
                    clearAllDirGuideQuickSearchInfo();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return seg_do;
        }

        public ASEGMENT CloseSegment(string strSegCode)
        {
            ASEGMENT seg_do = null;
            try
            {
                seg_do = scApp.MapBLL.DisableSegment(strSegCode);

                foreach (mySection sec in sectionList)
                {
                    if (sec.seg_num == strSegCode)
                    {
                        banRouteTwoDirect(sec.section_id);
                    }
                }
                clearAllDirGuideQuickSearchInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return seg_do;
        }

        public ASEGMENT CloseSegment(string strSegCode, ASEGMENT.DisableType disableType)
        {
            ASEGMENT seg_vo = null;
            ASEGMENT seg_do = null;
            try
            {
                seg_vo = scApp.SegmentBLL.cache.GetSegment(strSegCode);
                lock (seg_vo)
                {
                    seg_do = scApp.MapBLL.DisableSegment(strSegCode, disableType);

                    foreach (mySection sec in sectionList)
                    {
                        if (sec.seg_num == strSegCode)
                        {
                            banRouteTwoDirect(sec.section_id);
                        }
                    }
                    clearAllDirGuideQuickSearchInfo();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return seg_do;
        }

        //public void banRouteOneDirect(int from, int to)
        //{
        //    List<int> banPattern = new List<int>();
        //    banPattern.Add(from);
        //    banPattern.Add(to);
        //    bool isExist = false;
        //    foreach (List<int> p in banPatternList)
        //    {
        //        if (p[0] == banPattern[0] && p[1] == banPattern[1])
        //        {
        //            isExist = true;
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    if (!isExist)
        //    {
        //        banPatternList.Add(banPattern);
        //    }
        //}

        //public void unbanRouteOneDirect(int from, int to)
        //{
        //    List<int> banPattern = new List<int>();
        //    banPattern.Add(from);
        //    banPattern.Add(to);
        //    bool isExist = false;
        //    int index = 0;
        //    foreach (List<int> p in banPatternList)
        //    {
        //        if (p[0] == banPattern[0] && p[1] == banPattern[1])
        //        {
        //            isExist = true;
        //            break;
        //        }
        //        else
        //        {
        //            index++;
        //            continue;
        //        }
        //    }
        //    if (isExist)
        //    {
        //        banPatternList.RemoveAt(index);
        //    }
        //}
        public void banRouteTwoDirect(string sectionID)
        {
            if (!int.TryParse(sectionID, out int sec_id)) return;
            mySection section = getSectionByID(sec_id);
            if (section != null)
                banRouteTwoDirect(section.address_1, section.address_2);
        }
        public void banRouteTwoDirect(int address_1, int address_2)
        {
            List<int> banPattern_1 = new List<int>();
            banPattern_1.Add(address_1);
            banPattern_1.Add(address_2);

            List<int> banPattern_2 = new List<int>();
            banPattern_2.Add(address_2);
            banPattern_2.Add(address_1);
            bool isExist_1 = false;
            foreach (List<int> p in banPatternList)
            {
                if (p[0] == banPattern_1[0] && p[1] == banPattern_1[1])
                {
                    isExist_1 = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (!isExist_1)
            {
                banPatternList.Add(banPattern_1);
            }

            bool isExist_2 = false;
            foreach (List<int> p in banPatternList)
            {
                if (p[0] == banPattern_2[0] && p[1] == banPattern_2[1])
                {
                    isExist_2 = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (!isExist_2)
            {
                banPatternList.Add(banPattern_2);
            }
        }
        public void unbanRouteTwoDirect(string sectionID)
        {
            if (!int.TryParse(sectionID, out int sec_id)) return;
            mySection section = getSectionByID(sec_id);
            unbanRouteTwoDirect(section.address_1, section.address_2);
        }
        public void unbanRouteTwoDirect(int address_1, int address_2)
        {
            List<int> banPattern_1 = new List<int>();
            banPattern_1.Add(address_1);
            banPattern_1.Add(address_2);

            List<int> banPattern_2 = new List<int>();
            banPattern_2.Add(address_2);
            banPattern_2.Add(address_1);
            bool isExist_1 = false;
            int index_1 = 0;
            foreach (List<int> p in banPatternList)
            {
                if (p[0] == banPattern_1[0] && p[1] == banPattern_1[1])
                {
                    isExist_1 = true;
                    break;
                }
                else
                {
                    index_1++;
                    continue;
                }
            }
            if (isExist_1)
            {
                banPatternList.RemoveAt(index_1);
            }

            bool isExist_2 = false;
            int index_2 = 0;
            foreach (List<int> p in banPatternList)
            {
                if (p[0] == banPattern_2[0] && p[1] == banPattern_2[1])
                {
                    isExist_2 = true;
                    break;
                }
                else
                {
                    index_2++;
                    continue;
                }
            }
            if (isExist_2)
            {
                banPatternList.RemoveAt(index_2);
            }
        }

        public int[] getAllBanDirectArray()
        {
            int arrayLength = (banPatternList.Count) * 2;
            int[] banDirectArray = new int[arrayLength];
            int index = 0;
            foreach (List<int> pattern in banPatternList)
            {
                banDirectArray[index++] = pattern[0];
                banDirectArray[index++] = pattern[1];
            }
            return banDirectArray;
        }
        public void resetBanRoute()
        {
            banPatternList.Clear();
        }



        #endregion Application Interface






        private mySection getSectionByTwoNode(int node1, int node2)
        {
            foreach (mySection s in sectionList)
            {
                if ((s.address_1 == node1 && s.address_2 == node2)
                 || (s.address_1 == node2 && s.address_2 == node1))
                {
                    return s;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }
        private mySection getSectionByID(int ID)
        {
            return sectionList.Where(sec => sec.isection_id == ID).SingleOrDefault();
        }



        /// <summary>
        /// 預先確認要將那些路徑Ban
        /// 1.禁止直接橫移到Port的路徑
        /// </summary>
        //private void PresetBanEndPattern(List<Section> sections)
        //{
        //    banEndPatternList.Clear();
        //    foreach (var sec in sections)
        //    {
        //        if (sec.isBanEnd_From2To)
        //        {
        //            List<int> banEndPattern = new List<int>();
        //            banEndPattern.Add(sec.address_1);
        //            banEndPattern.Add(sec.address_2);

        //            banEndPatternList.Add(banEndPattern);
        //        }
        //        if (sec.isBanEnd_To2From)
        //        {
        //            List<int> banEndPattern = new List<int>();
        //            banEndPattern.Add(sec.address_2);
        //            banEndPattern.Add(sec.address_1);
        //            banEndPatternList.Add(banEndPattern);
        //        }
        //    }
        //}

        //private void DecisionCostWeight(out double movecostF_weight, out double movecostR_weight)
        //{
        //    switch (BCID)
        //    {
        //        //case App.SCAppConstants.WorkVersion.VERSION_NAME_AUO_CAAGV100_Beta:
        //        //    movecostF_weight = 0.1;
        //        //    movecostR_weight = 10;
        //        //    break;
        //        case App.SCAppConstants.WorkVersion.VERSION_NAME_AUO_CAAGVC00:
        //        case App.SCAppConstants.WorkVersion.VERSION_NAME_TAICHUNG:
        //        case App.SCAppConstants.WorkVersion.VERSION_NAME_AUO_CAAGV200:
        //            movecostF_weight = 1;
        //            movecostR_weight = 1;
        //            break;
        //        default:
        //            movecostF_weight = 0.7;
        //            movecostR_weight = 1;
        //            break;
        //    }
        //}

        //private bool isByPass(int adr1, int adr2)
        //{
        //    int[] bypass_adr = new[] { 48048, 48047, 48050, 48053, 48054, 48057, 48058 };
        //    if (bypass_adr.Contains(adr1)) return true;
        //    if (bypass_adr.Contains(adr2)) return true;
        //    return false;

        //}
        public int[] getCatchSectionCount()
        {
            int[] section_count = new int[] { sectionList.Count, sectionIndexDic.Count };
            return section_count;
        }


        private void initialIndexDic(List<mySection> sectionList)
        {
            SortedSet<int> addressSet = new SortedSet<int>();
            sectionIndexDic = new Dictionary<int, int>();
            for (int i = 0; i < sectionList.Count; i++)
            {
                int section_id = Convert.ToInt32(sectionList[i].section_id);
                sectionIndexDic.Add(section_id, i);
                addressSet.Add(sectionList[i].address_1);
                addressSet.Add(sectionList[i].address_2);
            }
            addressIndexDic = new Dictionary<int, int>();
            IndexAddressDic = new Dictionary<int, int>();
            int index = 0;
            foreach (int address in addressSet)
            {
                addressIndexDic.Add(address, index);
                IndexAddressDic.Add(index, address);
                index++;
            }

        }



        private void createInterCostList(List<mySection> sectionList)
        {
            interCostsList = new List<InterCost>();
            foreach (mySection s in sectionList)
            {
                if (s.changeSec_1 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {

                        if (sectionList[i].section_id == s.changeSec_1.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_2];
                            int index_2 = addressIndexDic[s.address_1];
                            if (sectionList[i].address_1 == s.address_1)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_1.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
                if (s.changeSec_2 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {
                        if (sectionList[i].section_id == s.changeSec_2.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_2];
                            int index_2 = addressIndexDic[s.address_1];
                            if (sectionList[i].address_1 == s.address_1)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_2.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
                if (s.changeSec_3 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {
                        if (sectionList[i].section_id == s.changeSec_3.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_2];
                            int index_2 = addressIndexDic[s.address_1];
                            if (sectionList[i].address_1 == s.address_1)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_3.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
                if (s.changeSec_4 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {
                        if (sectionList[i].section_id == s.changeSec_4.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_1];
                            int index_2 = addressIndexDic[s.address_2];
                            if (sectionList[i].address_1 == s.address_2)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_4.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
                if (s.changeSec_5 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {
                        if (sectionList[i].section_id == s.changeSec_5.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_1];
                            int index_2 = addressIndexDic[s.address_2];
                            if (sectionList[i].address_1 == s.address_2)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_5.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
                if (s.changeSec_6 != null)
                {
                    for (int i = 0; i < sectionList.Count; i++)
                    {
                        if (sectionList[i].section_id == s.changeSec_6.changeSec)
                        {
                            int last_address;
                            int index_1 = addressIndexDic[s.address_1];
                            int index_2 = addressIndexDic[s.address_2];
                            if (sectionList[i].address_1 == s.address_2)
                            {
                                last_address = sectionList[i].address_2;
                            }
                            else
                            {
                                last_address = sectionList[i].address_1;
                            }
                            int index_3 = addressIndexDic[last_address];
                            InterCost interCost = new InterCost(s.changeSec_6.interCost, index_1, index_2, index_3);
                            interCostsList.Add(interCost);
                            break;
                        }
                    }
                }
            }
        }








    }





    public class PathInfo : IEquatable<PathInfo>//單點到單點的路徑資訊
    {
        public int total_cost;
        public List<int> costDetail;
        public List<int> path;
        public int startDelayTime;
        public List<int> costAccumulation;

        public PathInfo()
        {

            costDetail = new List<int>();
            costAccumulation = new List<int>();
            path = new List<int>();
        }

        public PathInfo(int total_cost, List<int> path)
        {
            this.total_cost = total_cost;
            this.path = path;
        }

        public bool isPathLoop()
        {
            if (path.Count < 3)//少於3個節點不可能loop
            {
                return false;
            }
            else
            {
                List<int> temp = this.path.ToList();
                //temp.RemoveAt(0);
                while (temp.Count > 0)
                {
                    int x = temp[0];
                    temp.RemoveAt(0);
                    if (temp.Contains(x))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool isPathBanned(List<List<int>> banPatternList)
        {

            for (int i = 0; i < banPatternList.Count; i++)
            {
                for (int j = 0; j + banPatternList[i].Count - 1 < path.Count; j++)
                {
                    bool findBannedPattern = true;
                    for (int k = 0; k < banPatternList[i].Count(); k++)
                    {
                        if (path[j + k] == banPatternList[i][k])
                        {
                            continue;
                        }
                        else
                        {
                            findBannedPattern = false;
                            break;
                        }
                    }
                    if (findBannedPattern)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        public bool isPathEndBanned(List<List<int>> banPatternList)//某些特定的路徑結尾是不能使用的
        {

            for (int i = 0; i < banPatternList.Count; i++)
            {

                bool findBannedPattern = true;
                for (int k = 0; k < banPatternList[i].Count(); k++)
                {
                    if (path[(path.Count - banPatternList[i].Count()) + k] == banPatternList[i][k])
                    {
                        continue;
                    }
                    else
                    {
                        findBannedPattern = false;
                        break;
                    }
                }
                if (findBannedPattern)
                {
                    return true;
                }
                else
                {
                    continue;
                }

            }
            return false;
        }

        public bool Equals(PathInfo other)
        {
            if (this.total_cost != other.total_cost)
            {
                return false;
            }
            if (!this.path.SequenceEqual(other.path))
            {
                return false;
            }
            return true;
        }
    }





    //class InterCost : IEquatable<InterCost>//因為載具可能會需要轉向，所花費的時間稱之為intercost
    class InterCost //因為載具可能會需要轉向，所花費的時間稱之為intercost
    {
        public int cost;
        public int firstPoint;
        public int secondPoint;
        public int thirdPoint;
        //public List<int> pattern;

        public InterCost()
        {
            //pattern = new List<int>();
        }

        //public InterCost(int cost, List<int> pattern)
        //{
        //    this.cost = cost;
        //    this.pattern = pattern;
        //}

        public InterCost(int cost, int firstPoint, int secondPoint, int thirdPoint)
        {
            this.cost = cost;
            //pattern = new List<int>();
            //pattern.Add(a);
            //pattern.Add(b);
            //pattern.Add(c);
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.thirdPoint = thirdPoint;
        }


        //public bool isMatch(List<int> _pattern)
        //{
        //    if (_pattern.Count < 3)//少於3個點必定有誤
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (!this.pattern.SequenceEqual(_pattern))
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //}

        //public bool Equals(InterCost other)
        //{
        //    if (this.cost != other.cost)
        //    {
        //        return false;
        //    }
        //    if (!this.pattern.SequenceEqual(other.pattern))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }

    public class mySection //section都是雙向的
    {
        public int isection_id;
        public string section_id;
        public string seg_num;
        public int address_1;
        public int address_2;
        public int moveCost_1;
        public int moveCost_2;
        public bool isBanEnd_From2To;
        public bool isBanEnd_To2From;
        public int direct;//1是順向,2是逆向

        public changeSection changeSec_1;
        public changeSection changeSec_2;
        public changeSection changeSec_3;

        public changeSection changeSec_4;
        public changeSection changeSec_5;
        public changeSection changeSec_6;

        public mySection(string section_id, int address_1, int address_2, int moveCost_1, int moveCost_2,
            string changeSec_1, int interCost_1, string changeSec_2, int interCost_2,
            string changeSec_3, int interCost_3, string changeSec_4, int interCost_4,
            string changeSec_5, int interCost_5, string changeSec_6, int interCost_6,
            bool _isBanEnd_From2To, bool _isBanEnd_To2From, int direct, string seg_num)
        {
            this.section_id = section_id;
            int.TryParse(this.section_id, out isection_id);
            this.address_1 = address_1;
            this.address_2 = address_2;
            this.moveCost_1 = moveCost_1;
            this.moveCost_2 = moveCost_2;
            if (!string.IsNullOrEmpty(changeSec_1))
            {
                this.changeSec_1 = new changeSection(changeSec_1, interCost_1);
            }
            if (!string.IsNullOrEmpty(changeSec_2))
            {
                this.changeSec_2 = new changeSection(changeSec_2, interCost_2);
            }
            if (!string.IsNullOrEmpty(changeSec_3))
            {
                this.changeSec_3 = new changeSection(changeSec_3, interCost_3);
            }
            if (!string.IsNullOrEmpty(changeSec_4))
            {
                this.changeSec_4 = new changeSection(changeSec_4, interCost_4);
            }
            if (!string.IsNullOrEmpty(changeSec_5))
            {
                this.changeSec_5 = new changeSection(changeSec_5, interCost_5);
            }
            if (!string.IsNullOrEmpty(changeSec_6))
            {
                this.changeSec_6 = new changeSection(changeSec_6, interCost_6);
            }

            isBanEnd_From2To = _isBanEnd_From2To;
            isBanEnd_To2From = _isBanEnd_To2From;
            this.direct = direct;
            this.seg_num = seg_num;
            //this.changeSec_1 = changeSec_1;
            //this.interCost_1 = interCost_1;
            //this.changeSec_2 = changeSec_2;
            //this.interCost_2 = interCost_2;
            //this.changeSec_3 = changeSec_3;
            //this.interCost_3 = interCost_3;
        }
        public class changeSection
        {
            public string changeSec;
            public int interCost;
            public changeSection(string changeSec, int interCost)
            {
                this.changeSec = changeSec;
                this.interCost = interCost;
            }
        }
    }

    public class RouteInfo
    {
        public RouteInfo(List<mySection> _sections, List<int> _addresses, int _total_cost)
        {
            sections = _sections;
            addresses = _addresses;
            total_cost = _total_cost;
        }
        public RouteInfo(List<mySection> _sections, int _total_cost)
        {
            sections = _sections;
            total_cost = _total_cost;
        }
        public List<mySection> sections;
        public List<int> addresses;

        public int total_cost;
        public List<string> GetSectionIDs()
        {
            return sections.Select(sec => sec?.section_id).ToList();
        }







    }


}
