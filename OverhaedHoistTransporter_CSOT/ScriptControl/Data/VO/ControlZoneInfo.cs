using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Service;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.VO
{
    public class ControlZoneInfo
    {
        public string ID { get; private set; } = "";
        public List<Point> Scope { get; private set; } = new List<Point>();
        public List<string> RelatedBlockIDs { get; private set; } = new List<string>();

        public ConcurrentDictionary<AVEHICLE, Stopwatch> ZoneFullFallOhtList { get; private set; } = new ConcurrentDictionary<AVEHICLE, Stopwatch>();
        public ConcurrentDictionary<ControlZoneInfo, Stopwatch> InterlockZoneReserveTime { get; private set; } = new ConcurrentDictionary<ControlZoneInfo, Stopwatch>();
        public int VhCount { get; private set; } = 0;
        public List<AVEHICLE> Vhs { get; private set; } = new List<AVEHICLE>();
        public int VhLimitCount { get; private set; }
        public ControlZoneInfo(string _ID, List<Point> _scope, List<string> _relateBlockIDs, int vhLimitCount)
        {
            ID = _ID;
            Scope = _scope;
            RelatedBlockIDs = _relateBlockIDs;
            VhLimitCount = vhLimitCount;
        }
        Stopwatch Stopwatch = new Stopwatch();
        public void RefreshControlZoneSectionVhCount(SCApplication app, List<AVEHICLE> vhs)
        {
            var get_result = GetInControlZoneSectionVh(app, vhs);
            VhCount = get_result.vhCount;
            Vhs = get_result.vhs;

            if (!Stopwatch.IsRunning || Stopwatch.ElapsedMilliseconds > 5_000)
            {
                LogControlZoneSectionVhInfo();
                Stopwatch.Restart();
            }
        }
        public void RefreshZoneFullFallOhtList(SCApplication app)
        {
            if (!ZoneFullFallOhtList.Any())
            {
                if (InterlockZoneReserveTime.Any())
                {
                    LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                       Data: $"control zone full fail list:{ID} is empty, remove all vh from control zone full fail list.",
                       VehicleID: "");
                    InterlockZoneReserveTime.Clear();
                }
                return;
            }
            //確認OHT是否已經1.異常2.變手動3.進入該Control，是的話就可以將它清除
            foreach (var vh in ZoneFullFallOhtList.Keys.ToList())
            {
                if (vh.ERROR == ProtocolFormat.OHTMessage.VhStopSingle.StopSingleOn)
                {
                    LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                       Data: $"Vh:{vh.Num} has error, remove from control zone full fail list:{ID}",
                       VehicleID: vh.VEHICLE_ID);
                    ZoneFullFallOhtList.TryRemove(vh, out _);
                }
                else if (vh.MODE_STATUS == ProtocolFormat.OHTMessage.VHModeStatus.Manual)
                {
                    LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                       Data: $"Vh:{vh.Num} has change to manual, remove from control zone full fail list:{ID}",
                       VehicleID: vh.VEHICLE_ID);
                    ZoneFullFallOhtList.TryRemove(vh, out _);
                }
                else if (IsInControlZone(app, vh, false))
                {
                    LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                       Data: $"Vh:{vh.Num} has in control zone:{ID}, remove from control zone full fail list:{ID}",
                       VehicleID: vh.VEHICLE_ID);
                    ZoneFullFallOhtList.TryRemove(vh, out _);
                }
            }

            if (!ZoneFullFallOhtList.Any())
            {
                LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                   Data: $"control zone full fail list:{ID} is empty, remove all vh from control zone full fail list.",
                   VehicleID: "");
                InterlockZoneReserveTime.Clear();
            }
        }
        private void LogControlZoneSectionVhInfo()
        {
            string vh_num = string.Join(",", Vhs.Select(vh => vh.Num));
            NLog.LogManager.GetCurrentClassLogger().Debug($"ID:{ID},Count:{VhCount},vhs:{vh_num}");
        }

        private (int vhCount, List<AVEHICLE> vhs) GetInControlZoneSectionVh(SCApplication app, List<AVEHICLE> vhs)
        {
            var in_control_zone_vhs = vhs.Where(vh => IsInControlZone(app, vh, true)).ToList();
            return (in_control_zone_vhs.Count(), in_control_zone_vhs);
        }
        //public bool IsInControlZone(Point p)
        public bool IsInControlZone(App.SCApplication app, AVEHICLE vh, bool IsIncludeReserved)
        {
            Point p = vh.Point;
            if (Scope.Count != 4)
            {
                return false;
            }
            //1.確認位置是否在範圍內
            if (IsControlZone(p, Scope[0], Scope[1], Scope[2], Scope[3]))
            {
                return true;
            }
            if (IsIncludeReserved)
            {
                //2.確認是否有已預約該Zone的入口BlockSection
                var reserved_section = app.ReserveBLL.GetCurrentReserveSectionIDs(vh.VEHICLE_ID);
                foreach (string block_ids in RelatedBlockIDs)
                {
                    var block_obj = app.BlockControlBLL.cache.getBlockZoneMaster(block_ids);
                    if (block_obj == null) continue;
                    if (HasIntersection(block_obj.GetBlockZoneDetailSectionIDs(), reserved_section))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool HasIntersection(List<string> list1, List<string> list2)
        {
            return list1.Intersect(list2).Any();
        }

        bool IsControlZone(Point p, Point p1, Point p2, Point p3, Point p4)
        {
            // 找到矩形的邊界
            int minX = Math.Min(Math.Min(p1.X, p2.X), Math.Min(p3.X, p4.X));
            int maxX = Math.Max(Math.Max(p1.X, p2.X), Math.Max(p3.X, p4.X));
            int minY = Math.Min(Math.Min(p1.Y, p2.Y), Math.Min(p3.Y, p4.Y));
            int maxY = Math.Max(Math.Max(p1.Y, p2.Y), Math.Max(p3.Y, p4.Y));

            // 判斷點是否在邊界內
            bool is_in = (p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY);
            return is_in;
        }

        public void AddVhToZoneFullFallOhtList(AVEHICLE vh)
        {
            if (!ZoneFullFallOhtList.ContainsKey(vh))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                ZoneFullFallOhtList.TryAdd(vh, stopwatch);
            }
        }
        public void AddInterlockZoneReserveTime(ControlZoneInfo controlZone)
        {
            if (!InterlockZoneReserveTime.ContainsKey(controlZone))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                InterlockZoneReserveTime.TryAdd(controlZone, stopwatch);
            }
        }
        public (bool Has, List<ControlZoneInfo> ReserveControlZone) HasActiveInterlockZoneReserve()
        {
            if (!InterlockZoneReserveTime.Any())
                return (false, null);
            var in_reserve_time = InterlockZoneReserveTime.Where(pair => pair.Value.ElapsedMilliseconds < 15_000).ToList();
            return (in_reserve_time.Any(), in_reserve_time.Select(pair => pair.Key).ToList());
        }
        public void IncrementVhCount()
        {
            VhCount++;
        }
    }
}
