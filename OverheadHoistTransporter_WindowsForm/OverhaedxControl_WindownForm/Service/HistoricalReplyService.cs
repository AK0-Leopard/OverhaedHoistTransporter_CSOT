using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using Nest;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Service
{
    public class HistoricalReplyService
    {

        private bool isPlay = false;
        private bool isPause = false;


        private int[] PlayIndexs;
        private long totalSearchCount = 0;
        private string currentPlayVh = "";

        private int currentPlayIndex = 0;
        private int palyRate = 1;
        protected Timer HistoricalReplyPlayerTimer;
        DateTime StartPlayTime = DateTime.MinValue;
        public Dictionary<DateTime, List<Vo.VehicleHistoricalInfo>> Vh_Historical_Info_GroupByTime = new Dictionary<DateTime, List<Vo.VehicleHistoricalInfo>>();
        public List<Vo.VehicleHistoricalInfo> vh_historical_infos = null;
        Logger logger = LogManager.GetCurrentClassLogger();
        string pattern = Regex.Escape("[") + "(.*?)]";
        WindownApplication app = null;
        public EventHandler<int> PlayScheduleChanged;
        public EventHandler<Dictionary<DateTime, List<Vo.VehicleHistoricalInfo>>> LoadComplete;
        public HistoricalReplyService(WindownApplication _app)
        {
            // HistoricalReplyPlayerTimer = new Timer(HistoricalReplyPlayer);
            app = _app;
        }


        public void loadVhHistoricalInfo(DateTime start_time, DateTime end_time)
        {
            var node = new Uri($"http://{Common.ElasticSearchManager.ELASTIC_URL}:9200");
            // var node = new Uri("http://192.168.9.211:9200");
            var settings = new ConnectionSettings(node).DefaultIndex("default");
            settings.DisableDirectStreaming();
            var client = new ElasticClient(settings);
            //SearchRequest sr = new SearchRequest("ohtc-test-objecthistoricalinfo*");
            SearchRequest sr = new SearchRequest("mfoht100-ohtc1-objecthistoricalinfo*");
            DateRangeQuery dq = new DateRangeQuery
            {
                Field = "@timestamp",
                GreaterThan = start_time,
                LessThan = end_time,
            };
            //TermsQuery tsq = new TermsQuery
            //{
            //};
            int startIndex = 0;
            int eachSearchSize = 9999;
            List<ObjectHistricalInfo> object_infos = new List<ObjectHistricalInfo>();
            do
            {
                sr.From = startIndex;
                sr.Size = eachSearchSize;
                dq = new DateRangeQuery
                {
                    Field = "@timestamp",
                    GreaterThan = start_time,
                    LessThan = end_time,
                };
                sr.Query = dq;
                sr.Source = new SourceFilter()
                {
                    Includes = new string[] { "@timestamp", "OBJECT_ID", "RAWDATA" },
                };

                var result = client.Search<ObjectHistricalInfo>(sr);
                if (result.Documents.Count == 0) break;
                var result_info = result.Documents.OrderBy(info => info.timestamp);
                object_infos.AddRange(result_info.ToList());
                // if (object_infos.Count >= result.Total) break;
                //startIndex += eachSearchSize;
                if (result.Documents.Count < eachSearchSize) break;

                start_time = result_info.Last().timestamp.AddMilliseconds(1);
            }
            while (true);
            if (object_infos.Count == 0) return;
            BlockingCollection<Vo.VehicleHistoricalInfo> listTemp = new BlockingCollection<Vo.VehicleHistoricalInfo>();
            Parallel.For(0, object_infos.Count(), rowCount =>
            {
                Vo.VehicleHistoricalInfo Historyinfo = new Vo.VehicleHistoricalInfo();
                DateTime Time = object_infos[rowCount].timestamp;
                string sVh_ID = object_infos[rowCount].OBJECT_ID;
                string sRaw_data = object_infos[rowCount].RAWDATA;

                byte[] vh_info_bytes = Common.WinFromUtility.unCompressString(sRaw_data);
                sc.ProtocolFormat.OHTMessage.VEHICLE_INFO vh_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(vh_info_bytes);
                Historyinfo.Time = Time.ToLocalTime();
                Historyinfo.ID = sVh_ID;
                Historyinfo.VhInfo = vh_info;
                listTemp.Add(Historyinfo);
                // 每處理幾筆就停止5ms
                if (rowCount != 0 && rowCount % 20000 == 0)
                {
                    System.Threading.SpinWait.SpinUntil(() => false, 5);
                }
            });
            vh_historical_infos = listTemp.OrderBy(info => info.Time).ToList();
            StartPlayTime = vh_historical_infos.First().Time;
            var groupResult = from vh_historical_info in vh_historical_infos
                              group vh_historical_info by vh_historical_info.Time;
            Vh_Historical_Info_GroupByTime = groupResult.OrderBy(infos => infos.Key).ToDictionary
                (infos => infos.Key, infos => infos.ToList());
            startIndex = 0;


            LoadComplete?.Invoke(this, Vh_Historical_Info_GroupByTime);
            //foreach (var info in vh_historical_infos)
            //{
            //    Console.WriteLine(info.VhInfo.ToString());
            //}
        }


        private class ObjectHistricalInfo
        {
            [Date(Name = "@timestamp")]
            public DateTime timestamp { get; set; }
            [Date(Name = "OBJECT_ID")]
            public string OBJECT_ID { get; set; }
            [Date(Name = "RAWDATA")]
            public string RAWDATA { get; set; }
        }

        public void loadVhHistoricalInfo()
        {



            string[] file_rows_data = File.ReadAllLines(@"C:\LogFiles\OHxC\VehicleHistoricalInfo.log", System.Text.Encoding.Default);


            BlockingCollection<Vo.VehicleHistoricalInfo> listTemp = new BlockingCollection<Vo.VehicleHistoricalInfo>();
            Parallel.For(0, file_rows_data.Count(), rowCount =>
            {
                Vo.VehicleHistoricalInfo Historyinfo = new Vo.VehicleHistoricalInfo();
                MatchCollection matches = Regex.Matches(file_rows_data[rowCount], pattern);
                string sTime = matches.Count > 0 ? matches[0].Value.Replace("[", "").Replace("]", "") : string.Empty;
                string sVh_ID = matches.Count > 1 ? matches[1].Value.Replace("[", "").Replace("]", "") : string.Empty;
                string sRaw_data = matches.Count > 2 ? matches[2].Value.Replace("[", "").Replace("]", "") : string.Empty;
                DateTime parseDateTime = default(DateTime);
                if (!DateTime.TryParseExact
                (sTime, sc.App.SCAppConstants.DateTimeFormat_23, CultureInfo.InvariantCulture, DateTimeStyles.None, out parseDateTime))
                {
                    logger.Warn($"{nameof(sTime)} parse fail. value{sTime}");
                }
                byte[] vh_info_bytes = Common.WinFromUtility.unCompressString(sRaw_data);
                sc.ProtocolFormat.OHTMessage.VEHICLE_INFO vh_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(vh_info_bytes);
                Historyinfo.Time = parseDateTime;
                Historyinfo.ID = sVh_ID;
                Historyinfo.VhInfo = vh_info;
                listTemp.Add(Historyinfo);
                // 每處理幾筆就停止5ms
                if (rowCount != 0 && rowCount % 20000 == 0)
                {
                    System.Threading.SpinWait.SpinUntil(() => false, 5);
                }
            });
            vh_historical_infos = listTemp.OrderBy(info => info.Time).ToList();
            StartPlayTime = vh_historical_infos.First().Time;
            var groupResult = from vh_historical_info in vh_historical_infos
                              group vh_historical_info by vh_historical_info.Time;
            Vh_Historical_Info_GroupByTime = groupResult.OrderBy(infos => infos.Key).ToDictionary
                (infos => infos.Key, infos => infos.ToList());
            currentPlayIndex = 0;

            //foreach (var info in vh_historical_infos)
            //{
            //    Console.WriteLine(info.VhInfo.ToString());
            //}
        }


        public void Play()
        {
            HistoricalReplyPlayer(null);
        }
        public void Pause()
        {
            isPause = true;
        }
        public void Continue()
        {
            isPause = false;
        }
        public void Stop()
        {
            isPlay = false;
        }
        public void setPlayInfo(string vhID, int[] indexs)
        {
            currentPlayVh = vhID;
            PlayIndexs = indexs;
        }
        public void setStartIndex(int index)
        {
            currentPlayIndex = index;
            // var currentKeyValue = Vh_Historical_Info_GroupByTime.ElementAt(currentIndex);

            var currentKeyValue = Vh_Historical_Info_GroupByTime.ElementAt(PlayIndexs[currentPlayIndex]);
            Task.Run(() => PushVehicleHistoricalInso(currentKeyValue.Value));
        }
        public void setPalyRate(int rate)
        {
            palyRate = rate;
        }

        private void HistoricalReplyPlayer(object obj)
        {
            if (isPlay) return;
            isPlay = true;
            //for (int i = currentPlayIndex; i < Vh_Historical_Info_GroupByTime.Count; i++)
            for (int i = currentPlayIndex; i < PlayIndexs.Length; i++)
            {
                //var currentKeyValue = Vh_Historical_Info_GroupByTime.ElementAt(i);
                var currentKeyValue = Vh_Historical_Info_GroupByTime.ElementAt(PlayIndexs[currentPlayIndex]);
                Task.Run(() => PushVehicleHistoricalInso(currentKeyValue.Value));
                PlayScheduleChanged?.Invoke(this, i);
                if (i + 1 < Vh_Historical_Info_GroupByTime.Count)
                {
                    var nextKeyValue = Vh_Historical_Info_GroupByTime.ElementAt(i + 1);
                    SpinWait.SpinUntil(() => !isPlay, (int)((nextKeyValue.Key - currentKeyValue.Key).TotalMilliseconds / palyRate));
                }
                if (!isPlay)
                {
                    currentPlayIndex = i;
                    return;
                }
                if (isPause) SpinWait.SpinUntil(() => !isPause, System.Threading.Timeout.Infinite);
            }
            isPlay = false;
        }

        private void PushVehicleHistoricalInso(List<Vo.VehicleHistoricalInfo> vehicleHistoricals)
        {
            foreach (var historicalInfo in vehicleHistoricals)
            {
                if (BCFUtility.isMatche(currentPlayVh, "ALL") ||
                    BCFUtility.isMatche(currentPlayVh, historicalInfo.ID))
                    app.VehicleBLL.ProcVehicleInfo(historicalInfo.VhInfo);
            }
            //Parallel.For(0, vehicleHistoricals.Count(), rowCount =>
            //{
            // app.VehicleBLL.ProcVehicleInfo(vehicleHistoricals[rowCount].VhInfo);

            //});
        }
    }
}