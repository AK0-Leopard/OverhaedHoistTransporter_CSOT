using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mirle.ibg3k0.ohxc.winform.Common
{
    public class ObjCacheManager
    {
        WindownApplication app = null;
        List<AVEHICLE> Vehicles = null;
        List<AEQPT> Eqpts = null;
        List<VehicleObjToShow> VehicleObjToShows = null;
        ALINE Line = null;

        public List<sc.Data.VO.MPCTipMessage> MPCTipMessages = null;
        public event EventHandler<List<sc.Data.VO.MPCTipMessage>> mPCTipMessagesChange;
        public event EventHandler PortStationUpdateComplete;
        public event EventHandler UserUpdateComplete;
        public event EventHandler UserGroupUpdateComplete;
        public event EventHandler ConnectionInfoUpdate;
        public event EventHandler OnlineCheckInfoUpdate;
        public event EventHandler PingCheckInfoUpdate;
        public event EventHandler MTLMTSInfoUpdate;

        public event EventHandler MCSCommandUpdateComplete;
        public event EventHandler VehicleUpdateComplete;
        public event EventHandler TransferInfoUpdate;
        public event EventHandler LogInUserChanged;

        public event EventHandler SegmentsUpdateComplete;

        #region MapInfo
        public string MapId { get; private set; } = null;
        public string EFConnectionString { get; private set; } = null;
        List<ARAIL> RAILs = null;
        List<APOINT> POINTs = null;
        List<AGROUPRAILS> GROUPRAILs = null;
        List<AADDRESS> ADDRESSes = null;
        List<ASECTION> SECTIONs = null;
        List<ASEGMENT> SEGMENTs = null;
        List<APORTSTATION> PORTSTATIONs = null;
        List<APORTICON> PORTICONs = null;
        List<UASUSR> USERs = null;
        List<ALARM> ALARMs = null;
        List<UASUSRGRP> USERGROUPs = null;
        List<UASUFNC> USERFUNCs = null;
        List<UASFNC> FUNCs = null;
        List<VACMD_MCS> MCS_CMDs = null;
        #endregion MapInfo

        #region Alarm Map
        List<AlarmMap> AlarmMaps = null;
        #endregion Alarm Map


        public ObjCacheManager(WindownApplication _app)
        {
            app = _app;
            //Vehicles = app.VehicleBLL.loadAllVehicle();
            //Line = new ALINE();


        }
        public void start()
        {
            initialMapInfo();
        }

        private void initialMapInfo()
        {
            MapId = app.MapBLL.GetMapInfoFromHttp(sc.App.SCAppConstants.MapInfoDataType.MapID);
            EFConnectionString = app.MapBLL.GetMapInfoFromHttp(sc.App.SCAppConstants.MapInfoDataType.EFConnectionString);
            RAILs = app.MapBLL.GetMapInfosFromHttp<ARAIL>(sc.App.SCAppConstants.MapInfoDataType.Rail);
            POINTs = app.MapBLL.GetMapInfosFromHttp<APOINT>(sc.App.SCAppConstants.MapInfoDataType.Point);
            GROUPRAILs = app.MapBLL.GetMapInfosFromHttp<AGROUPRAILS>(sc.App.SCAppConstants.MapInfoDataType.GroupRails);
            ADDRESSes = app.MapBLL.GetMapInfosFromHttp<AADDRESS>(sc.App.SCAppConstants.MapInfoDataType.Address);
            SECTIONs = app.MapBLL.GetMapInfosFromHttp<ASECTION>(sc.App.SCAppConstants.MapInfoDataType.Section);
            SEGMENTs = app.MapBLL.GetMapInfosFromHttp<ASEGMENT>(sc.App.SCAppConstants.MapInfoDataType.Segment);
            PORTSTATIONs = app.MapBLL.GetMapInfosFromHttp<APORTSTATION>(sc.App.SCAppConstants.MapInfoDataType.Port);
            PORTICONs = app.MapBLL.GetMapInfosFromHttp<APORTICON>(sc.App.SCAppConstants.MapInfoDataType.PortIcon);
            Vehicles = app.MapBLL.GetMapInfosFromHttp<AVEHICLE>(sc.App.SCAppConstants.MapInfoDataType.Vehicle);
            Eqpts = app.MapBLL.GetMapInfosFromHttp<AEQPT>(sc.App.SCAppConstants.MapInfoDataType.Eqpt);
            List<MaintainLift> mtlList = app.MapBLL.GetMapInfosFromHttp<MaintainLift>(sc.App.SCAppConstants.MapInfoDataType.MTL);
            List<MaintainSpace> mtsList = app.MapBLL.GetMapInfosFromHttp<MaintainSpace>(sc.App.SCAppConstants.MapInfoDataType.MTS);

            for (int i = 0; i < Eqpts.Count; i++)
            {
                var mtl = mtlList.Where(m => sc.Common.SCUtility.isMatche(m.EQPT_ID, Eqpts[i].EQPT_ID)).FirstOrDefault();
                if (mtl != null)
                {
                    Eqpts[i] = mtl;
                }
            }

            for (int i = 0; i < Eqpts.Count; i++)
            {
                var mts = mtsList.Where(m => sc.Common.SCUtility.isMatche(m.EQPT_ID, Eqpts[i].EQPT_ID)).FirstOrDefault();
                if (mts != null)
                {
                    Eqpts[i] = mts;
                }
            }

            //Eqpts = new List<AEQPT>();
            //Eqpts.AddRange(mtlList);
            //Eqpts.AddRange(mtsList);
            Line = app.MapBLL.GetMapInfoFromHttp<ALINE>(sc.App.SCAppConstants.MapInfoDataType.Line);



            ALARMs = app.AlarmBLL.loadSetAlarm();
            AlarmMaps = app.MapBLL.GetMapInfosFromHttp<AlarmMap>(sc.App.SCAppConstants.MapInfoDataType.AlarmMap);

            MPCTipMessages = new List<sc.Data.VO.MPCTipMessage>();

            PORTSTATIONs = app.PortStationBLL.OperateDB.loadPortStation();
            USERs = app.UserBLL.OperateDBUser.loadUser();
            USERGROUPs = app.UserBLL.OperateDBUserGroup.loadUserGroup();
            USERFUNCs = app.UserBLL.OperateDBUserGroupFunc.loadAllUserGroupFunc();
            FUNCs = app.UserBLL.OperateDBFunctionCode.loadAllFunctionCode();
            //app.CmdBLL.loadACMD_MCSIsUnfinishedObjToShow();
            //MCS_CMDs = app.CmdBLL.loadACMD_MCSIsUnfinishedObjToShow();
            //MCS_CMDs = new List<sc.ObjectRelay.CMD_MCSObjToShow>();
            //MCS_CMDs = app.CmdBLL.loadVACMD_MCS();
            MCS_CMDs = app.CmdBLL.loadVACMD_MCS();
            initialVehicleObjToShow();
        }

        private void initialVehicleObjToShow()
        {
            double max_distance = SECTIONs.Max(sec => sec.SEC_DIS);
            double distance_scale = 0;
            if (max_distance > 1000 * 10000)
            {
                distance_scale = 1000 * 10000;
            }
            else
            {
                distance_scale = 1000;
            }
            //if ()
            VehicleObjToShows = new List<VehicleObjToShow>();
            foreach (var vh in Vehicles)
            {
                if (WindownApplication.OHxCFormMode == OHxCFormMode.HistoricalPlayer)
                {
                    vh.CUR_ADR_ID = "";
                    vh.CUR_SEC_ID = "";
                }
                VehicleObjToShows.Add(new VehicleObjToShow(this, vh, distance_scale));
            }
        }

        public AVEHICLE GetVEHICLE(string vh_id)
        {
            return Vehicles.Where(vh => vh.VEHICLE_ID.Trim() == vh_id.Trim()).SingleOrDefault();
        }
        public List<AVEHICLE> GetVEHICLEs()
        {
            return Vehicles;
        }

        public ALINE GetLine()
        {
            return Line;
        }

        public List<ARAIL> GetRails()
        {
            return RAILs;
        }
        public List<APOINT> GetPoints()
        {
            return POINTs;
        }
        public List<AGROUPRAILS> GetGroupRails()
        {
            return GROUPRAILs;
        }
        public List<AADDRESS> GetAddresses()
        {
            return ADDRESSes;
        }
        public List<ASECTION> GetSections()
        {
            return SECTIONs;
        }
        public List<ASEGMENT> GetSegments()
        {
            return SEGMENTs;
        }

        public List<APORTSTATION> GetPortStations()
        {
            return PORTSTATIONs;
        }
        public APORTSTATION GetPortStation(string addressID)
        {
            return PORTSTATIONs.Where(station => station.ADR_ID.Trim() == addressID).SingleOrDefault();
            //return PORTSTATIONs.Where(station => station.ADR_ID.Trim() == addressID).FirstOrDefault();
        }
        public List<APORTICON> GetPortIcons()
        {
            return PORTICONs;
        }
        public List<VehicleObjToShow> GetVehicleObjToShows()
        {
            return VehicleObjToShows;
        }


        public List<UASUSR> GetUsers()
        {
            return USERs;
        }

        public List<ALARM> GetAlarms()
        {
            return ALARMs;
        }

        public List<VACMD_MCS> GetMCS_CMD()
        {
            return MCS_CMDs;
        }
        public List<AlarmMap> GetAlarmMaps()
        {
            return AlarmMaps;
        }



        public List<AEQPT> GetMTL1MTS1()
        {
            return Eqpts.Where(c => c.EQPT_ID.Trim() == "MTS" || c.EQPT_ID.Trim() == "MTL").ToList();
        }
        public AEQPT GetMTLMTSByID(string station_id)
        {
            return Eqpts.Where(c => c.EQPT_ID.Trim() == station_id.Trim()).FirstOrDefault();
        }
        public List<AEQPT> GetMTS2()
        {
            return Eqpts.Where(c => c.EQPT_ID.Trim() == "MTS2").ToList();
        }
        public List<AEQPT> GetMaintainDevice()
        {
            return Eqpts.Where(c => c is MaintainLift || c is MaintainSpace).ToList();
        }
        public OHCV GetOHCV(string segmentID)
        {
            return Eqpts.Where(c => c is OHCV && (c as OHCV).SegmentLocation == segmentID).
                         FirstOrDefault() as OHCV;
        }

        public int getHourlyCMDCount()
        {
            return MCS_CMDs.Where(c => c.CMD_FINISH_TIME != null && ((DateTime)c.CMD_FINISH_TIME).AddHours(1) > DateTime.Now).ToList().Count;
        }

        public int getTodayCMDCount()
        {
            return MCS_CMDs.Where(c => c.CMD_FINISH_TIME != null && ((DateTime)c.CMD_FINISH_TIME).Year == DateTime.Now.Year
            && ((DateTime)c.CMD_FINISH_TIME).Month == DateTime.Now.Month && ((DateTime)c.CMD_FINISH_TIME).Day == DateTime.Now.Day).ToList().Count;
        }

        public List<UASUSRGRP> GetUserGroups()
        {
            return USERGROUPs;
        }
        public List<UASUFNC> GetUserFuncs(string user_grp_id)
        {
            return USERFUNCs.Where(f => f.USER_GRP.Trim() == user_grp_id.Trim()).ToList();
        }

        public List<UASFNC> GetFunctionCodes()
        {
            return FUNCs;
        }

        public void PutVehicle(sc.ProtocolFormat.OHTMessage.VEHICLE_INFO new_vh)
        {
            if (new_vh == null) return;
            AVEHICLE vh = Vehicles.Where(v => v.VEHICLE_ID == new_vh.VEHICLEID.Trim()).SingleOrDefault();
            vh.set(new_vh);
            vh.NotifyVhPositionChange();
            vh.NotifyVhStatusChange();
            VehicleUpdateComplete?.Invoke(this, EventArgs.Empty);

        }

        //public void PutVehicle(AVEHICLE new_vh)
        //{
        //    if (new_vh == null) return;
        //    AVEHICLE vh = Vehicles.Where(v => v.VEHICLE_ID == new_vh.VEHICLE_ID.Trim()).SingleOrDefault();
        //    vh.set(new_vh);
        //    vh.NotifyVhPositionChange();
        //    vh.NotifyVhStatusChange();
        //}

        public void putLine(sc.ProtocolFormat.OHTMessage.LINE_INFO newLineInfo)
        {
            Line.set(newLineInfo);
            Line.NotifyLineStatusChange();
        }


        public void putOnlineCheckInfo(sc.ProtocolFormat.OHTMessage.ONLINE_CHECK_INFO newOnlineCheckInfo)
        {
            Line.set(newOnlineCheckInfo);
            OnlineCheckInfoUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void putPingCheckInfo(sc.ProtocolFormat.OHTMessage.PING_CHECK_INFO newPingCheckInfo)
        {
            Line.set(newPingCheckInfo);
            PingCheckInfoUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void putMTL_MTSCheckInfo(sc.ProtocolFormat.OHTMessage.MTL_MTS_INFO newMTLMTSInfo)
        {
            if (newMTLMTSInfo == null) return;
            AEQPT eqpt = Eqpts.Where(e => e.EQPT_ID == newMTLMTSInfo.StationID.Trim()).SingleOrDefault();
            if (eqpt is MaintainSpace)
            {
                MaintainSpace MTS = eqpt as MaintainSpace;
                MTS.Plc_Link_Stat = newMTLMTSInfo.NetworkLink ? sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
                MTS.Is_Eq_Alive = newMTLMTSInfo.Alive;
                MTS.MTxMode = newMTLMTSInfo.Mode ? sc.ProtocolFormat.OHTMessage.MTxMode.Auto : sc.ProtocolFormat.OHTMessage.MTxMode.Manual;
                MTS.Interlock = newMTLMTSInfo.Interlock;
                MTS.CurrentCarID = newMTLMTSInfo.CarID;
                MTS.CurrentPreCarOurDistance = Convert.ToUInt32(newMTLMTSInfo.Distance);
                MTS.SynchronizeTime = Convert.ToDateTime(newMTLMTSInfo.SynchronizeTime);
                MTS.CarOutInterlock = newMTLMTSInfo.CarOutInterlock;
                MTS.CarInMoving = newMTLMTSInfo.CarInMoving;
            }
            else if (eqpt is MaintainLift)
            {
                MaintainLift MTL = eqpt as MaintainLift;
                MTL.Plc_Link_Stat = newMTLMTSInfo.NetworkLink ? sc.App.SCAppConstants.LinkStatus.LinkOK : sc.App.SCAppConstants.LinkStatus.LinkFail;
                MTL.Is_Eq_Alive = newMTLMTSInfo.Alive;
                MTL.MTxMode = newMTLMTSInfo.Mode ? sc.ProtocolFormat.OHTMessage.MTxMode.Auto : sc.ProtocolFormat.OHTMessage.MTxMode.Manual;
                MTL.Interlock = newMTLMTSInfo.Interlock;
                MTL.CurrentCarID = newMTLMTSInfo.CarID;
                MTL.MTLLocation = newMTLMTSInfo.MTLLocation == MTLLocation.Bottorn.ToString() ? MTLLocation.Bottorn : newMTLMTSInfo.MTLLocation == MTLLocation.Upper.ToString() ? MTLLocation.Upper : MTLLocation.None;
                MTL.CurrentPreCarOurDistance = Convert.ToUInt32(newMTLMTSInfo.Distance);
                MTL.SynchronizeTime = Convert.ToDateTime(newMTLMTSInfo.SynchronizeTime);
                MTL.CarOutInterlock = newMTLMTSInfo.CarOutInterlock;
                MTL.CarInMoving = newMTLMTSInfo.CarInMoving;
            }

            MTLMTSInfoUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void putTransferInfo(sc.ProtocolFormat.OHTMessage.TRANSFER_INFO newTransferInfo)
        {
            Line.set(newTransferInfo);
            TransferInfoUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void putTipMessageInfos(sc.ProtocolFormat.OHTMessage.TIP_MESSAGE_COLLECTION gpbMsgInfo)
        {
            MPCTipMessages.set(gpbMsgInfo);
            mPCTipMessagesChange?.Invoke(this, MPCTipMessages);
        }

        public void putConnectionInfo(sc.ProtocolFormat.OHTMessage.LINE_INFO newLineInfo)
        {
            Line.set(newLineInfo);
            ConnectionInfoUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void updatePortStation()
        {
            var port_stations = app.PortStationBLL.OperateDB.loadPortStation();
            foreach (var new_port_station in port_stations)
            {
                APORTSTATION port_station = PORTSTATIONs.
                    Where(port => port.PORT_ID.Trim() == new_port_station.PORT_ID.Trim()).
                    FirstOrDefault();
                port_station.set(new_port_station);
            }

            PortStationUpdateComplete?.Invoke(this, EventArgs.Empty);
        }

        public void updateSegments()
        {
            var segments_do = app.SegmentBLL.OperateDB.loadSegments();
            foreach (var segment_do in segments_do)
            {
                ASEGMENT seg_vo = SEGMENTs.
                    Where(s => s.SEG_NUM.Trim() == segment_do.SEG_NUM.Trim()).
                    FirstOrDefault();
                seg_vo.set(segment_do);
            }

            SegmentsUpdateComplete?.Invoke(this, EventArgs.Empty);
        }

        public void updateUser()
        {
            var users = app.UserBLL.OperateDBUser.loadUser();
            //foreach (var new_user in users)
            //{
            //    UASUSR user = USERs.
            //        Where(u => u.USER_ID.Trim() == new_user.USER_ID.Trim()).
            //        FirstOrDefault();
            //    user.set(new_user);
            //}
            USERs = users;
            UserUpdateComplete?.Invoke(this, EventArgs.Empty);
        }

        public void updateUserGroup()
        {
            var userGroups = app.UserBLL.OperateDBUserGroup.loadUserGroup();
            USERGROUPs = userGroups;
            UserGroupUpdateComplete?.Invoke(this, EventArgs.Empty);
        }
        public void updateUserGroupFunc()
        {
            var userGroupFuncs = app.UserBLL.OperateDBUserGroupFunc.loadAllUserGroupFunc();
            USERFUNCs = userGroupFuncs;
        }

        public void updateLogInUser()
        {
            LogInUserChanged?.Invoke(this, EventArgs.Empty);
        }

        public void updateMCS_CMD()
        {
            //var mcs_cmd = app.CmdBLL.loadVACMD_MCS();
            var mcs_cmd = app.CmdBLL.loadVACMD_MCS();
            MCS_CMDs = mcs_cmd;
            MCSCommandUpdateComplete?.Invoke(this, EventArgs.Empty);
        }

        //public void updateVehicle()
        //{
        //    Vehicles = app.MapBLL.GetMapInfosFromHttp<AVEHICLE>(sc.App.SCAppConstants.MapInfoDataType.Vehicle);
        //    VehicleUpdateComplete?.Invoke(this, EventArgs.Empty);
        //}

        public void updateAlarm(List<ALARM> alarms)
        {
            ALARMs = alarms;
        }

        public int getAlarmCountByVehicleID(string vh_id)
        {
            var alarm = from a in ALARMs
                        where a.EQPT_ID.Trim() == vh_id.Trim()
                        select a;
            List<ALARM> alarmList = alarm.ToList();
            return alarmList.Count;
        }

        public HashSet<string> hasAlarmVehicleList()
        {
            var alarm = from a in ALARMs
                        select a.EQPT_ID;
            List<string> alarmList = alarm.ToList();
            HashSet<string> hs = new HashSet<string>(alarmList);
            return hs;
        }

    }
}
