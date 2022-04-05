using RouteKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.iibg3k0.ids.ohxc.RouteKit
{
    public class AMSGuide : Guide
    {
        public override bool ImportMap()
        {
            bool flag = true;
            try
            {
                int i = 0;
                HashSet<string> fromAddHasSet = new HashSet<string>();
                List<ASECTION> section = dao.loadAll(con);
                foreach (ASECTION sec in section)
                {
                    Section s = new Section(sec.SEC_ID.Trim(), sec.FROM_ADR_ID.Trim(), sec.TO_ADR_ID.Trim(), sec.SEC_DIS, sec.SEG_NUM.Trim());
                    SectionList.Add(s);
                    SectionListIndex.Add(sec.SEC_ID.Trim(), i.ToString());

                    var segment = bll.getSegmentBySectionID(sec.SEC_ID.Trim());
                    if (!fromAddHasSet.Add(sec.FROM_ADR_ID))
                    {
                        if (segment.SEG_TYPE.ToString() == "Station")
                            SectionIndex.Add("none", "none");
                    }
                    else
                    {
                        SectionIndex.Add(sec.FROM_ADR_ID.Trim(), i.ToString());
                    }
                    i++;
                }
                foreach (Section sec in SectionList)
                {
                    var nextSection = dao.loadNextSectionIDBySectionID(con, sec.SectionCode).ToArray();
                    //var nextSection = dao.loadNextSectionIDBySectionID(con, "1229912199").ToArray(); //測試用
                    foreach (string strSection in nextSection)
                    {
                        int index = Convert.ToInt16(SectionListIndex[strSection.Trim()]);
                        SectionDoubleLink(sec, SectionList[index]);
                    }
                }

                var allAddress = dao.loadAllAddress(con);
                foreach (string address in allAddress)
                {
                    var segmentArray = dao.loadSegmentNumByAdr(con, address.Trim());
                    string strSeg = string.Empty;
                    foreach (string seg in segmentArray)
                        strSeg += "," + seg.Trim();
                    addressMappingSegment.Add(address.Trim(), strSeg.TrimStart(','));
                }

                List<List<string>> keepAddressArray = new List<List<string>>();
                List<string> all_adr_in_seg = null;
                string strAddress = string.Empty;
                int arrayIndex = 0;
                List<string> segments = dao.loadAllSegmentNum(con);
                //all_adr_in_seg = dao.loadAllAdrBySegmentNum(con, "26");  //測試用
                foreach (string seg in segments)
                {
                    all_adr_in_seg = dao.loadAllAdrBySegmentNum(con, seg.Trim());
                    List<string> addressArray = new List<string>();
                    foreach (string adr in all_adr_in_seg)
                        addressArray.Add(adr.Trim());
                    keepAddressArray.Add(addressArray);
                    keepAddressArrayIndex.Add(seg.Trim(), arrayIndex.ToString());
                    arrayIndex++;
                }

                var segArr = seg_dao.loadAllSegments(con);
                int indexCount = 0;
                foreach (ASEGMENT seg in segArr)
                {
                    string segType = string.Empty;
                    switch (Convert.ToInt16(seg.SEG_TYPE))
                    {
                        case 1:
                            segType = "Common";
                            break;
                        case 3:
                            if (seg.SPECIAL_MARK == 1)
                                segType = "Station";
                            else if (seg.SPECIAL_MARK == 2)
                                segType = "Rotating";
                            break;
                        case 4:
                            segType = "Maintenance";
                            break;
                    }
                    var secArr = dao.loadBySegmentNum(con, seg.SEG_NUM.Trim());
                    double segDistance = 0;
                    foreach (ASECTION sec in secArr)
                        segDistance += Convert.ToDouble(sec.SEC_DIS);
                    int index_AddressArray = Convert.ToInt16(keepAddressArrayIndex[seg.SEG_NUM.Trim()]);
                    Segment s = new Segment(seg.SEG_NUM.Trim(), segType, segDistance, keepAddressArray[index_AddressArray].ToArray());
                    SegmentList.Add(s);
                    SegmentListIndex.Add(seg.SEG_NUM.Trim(), indexCount.ToString());
                    indexCount++;
                }

                foreach (Segment seg in SegmentList)
                {
                    var nextSeg = dao.loadNextSegmentNumBySegmentNum(con, seg.SegmentCode.Trim());
                    foreach (string strSeg in nextSeg)
                    {
                        int segIndex = Convert.ToInt16(SegmentListIndex[strSeg.Trim()]);
                        SegmentDoubleLink(seg, SegmentList[segIndex]);
                    }
                }

                foreach (Segment seg in SegmentList)
                {
                    #region Initiation(Default Segment Status)
                    //switch (seg.SegmentType)
                    //{
                    //    case "Common":
                    //        seg.Status = "Active";
                    //        bll.updateSegStatus(seg.SegmentCode, E_SEG_STATUS.Active);
                    //        break;
                    //    case "Station":
                    //        seg.Status = "Active";  //取消關閉 Station_Type Segment的連動效果，Station的預設狀態為Active
                    //        bll.updateSegStatus(seg.SegmentCode, E_SEG_STATUS.Inactive);
                    //        break;
                    //    case "Rotating":
                    //        seg.Status = "Active";
                    //        bll.updateSegStatus(seg.SegmentCode, E_SEG_STATUS.Active);
                    //        break;
                    //    case "Maintenance":
                    //        seg.Status = "Active";  //2017/5/9，增加往下游找section功能後，Maintenance的預設狀態為Active
                    //        bll.updateSegStatus(seg.SegmentCode, E_SEG_STATUS.Inactive);
                    //        break;
                    //}
                    #endregion
                    var asegment = seg_dao.getByID(con, seg.SegmentCode);
                    switch (Convert.ToInt16(asegment.STATUS))
                    {
                        case 1:
                            seg.Status = "Active";
                            break;
                        case 2:
                            seg.Status = "Inactive";
                            break;
                        case 3:
                            seg.Status = "Closed";
                            break;
                    }
                    var asection = dao.getSectionBySegmentID(con, seg.SegmentCode);
                    foreach (ASECTION sec in asection)
                    {
                        int index = Convert.ToInt16(SectionListIndex[sec.SEC_ID.Trim()]);
                        SectionList[index].Status = seg.Status;
                    }
                }

            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error(ex, "Exception");
            }
            return flag;
        }
    }
}
