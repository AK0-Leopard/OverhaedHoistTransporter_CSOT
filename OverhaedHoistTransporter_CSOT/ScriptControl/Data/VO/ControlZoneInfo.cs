using System;
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
        public int VhCount { get; private set; } = 0;
        public IEnumerable<AVEHICLE> Vhs { get; private set; } = new List<AVEHICLE>();
        public int VhLimitCount { get; private set; }
        public ControlZoneInfo(string _ID, List<Point> _scope, List<string> _relateBlockIDs, int vhLimitCount)
        {
            ID = _ID;
            Scope = _scope;
            RelatedBlockIDs = _relateBlockIDs;
            VhLimitCount = vhLimitCount;
        }
        Stopwatch Stopwatch = new Stopwatch();
        public void RefreshControlZoneSectionVhCount(List<AVEHICLE> vhs)
        {
            var get_result = GetInControlZoneSectionVh(vhs);
            VhCount = get_result.vhCount;
            Vhs = get_result.vhs;

            if (!Stopwatch.IsRunning || Stopwatch.ElapsedMilliseconds > 5_000)
            {
                LogControlZoneSectionVhInfo();
                Stopwatch.Restart();
            }
        }
        private void LogControlZoneSectionVhInfo()
        {
            string vh_num = string.Join(",", Vhs.Select(vh => vh.Num));
            NLog.LogManager.GetCurrentClassLogger().Debug($"ID:{ID},Count:{VhCount},vhs:{vh_num}");
        }

        private (int vhCount, IEnumerable<AVEHICLE> vhs) GetInControlZoneSectionVh(List<AVEHICLE> vhs)
        {
            var in_control_zone_vhs = vhs.Where(vh => IsInControlZone(vh.Point));
            return (in_control_zone_vhs.Count(), in_control_zone_vhs);
        }
        public bool IsInControlZone(Point p)
        {
            if (Scope.Count != 4)
            {
                return false;
            }
            return IsControlZone(p, Scope[0], Scope[1], Scope[2], Scope[3]);
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
    }
}
