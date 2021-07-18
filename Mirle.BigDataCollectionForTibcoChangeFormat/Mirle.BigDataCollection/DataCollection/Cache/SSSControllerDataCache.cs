using Mirle.BigDataCollection.Define;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mirle.BigDataCollection.DataCollection.Collection
{
    public class SSSControllerDataCache
    {
        public ManagementObjectSearcher hd = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE NAME = 'C:' OR NAME = 'D:' ");
        private readonly LoggerService _loggerService;

        public string HDD_UseRate { get; set; }
        public string CPU_UseRate { get; set; }
        public string Memory_UseRate { get; set; }
        public string Network_UseRate { get; set; }

        private PerformanceCounter cpu;
        private List<NetworkInfo> lstNetworlInfo = new List<NetworkInfo>();

        public SSSControllerDataCache(DataCollectionINI dataCollectionINI, LoggerService loggerService)
        {
            //取錯資料?
            _loggerService = loggerService;
            cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            string hdSql = "SELECT * FROM Win32_LogicalDisk";

            var hdArray = dataCollectionINI.SSSController.DiskName.Split(',');

            for (int i = 0; i < hdArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(hdArray[i]))
                {
                    if (!hdSql.Contains("WHERE"))
                    {
                        hdSql += $" WHERE NAME = '{hdArray[i]}:' ";
                    }
                    else
                    {
                        hdSql += $" OR NAME = '{hdArray[i]}:' ";
                    }
                }
            }

            if (dataCollectionINI.SSSController.AutomaticSearch == "Y")
            {
                PerformanceCounterCategory pcg = new PerformanceCounterCategory("Network Interface");
                // instance = pcg.GetInstanceNames()[0];

                foreach (var InstamceName in pcg.GetInstanceNames())
                {
                    var bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", InstamceName);
                    var dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", InstamceName);
                    var dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", InstamceName);

                    if (bandwidthCounter.RawValue != 0 && dataSentCounter.RawValue != 0 && dataReceivedCounter.RawValue != 0)
                    {
                        lstNetworlInfo.Add(new NetworkInfo(bandwidthCounter, dataSentCounter, dataReceivedCounter));
                        loggerService.WriteLog("Trace", $"lstNetworlInfo.Add:{InstamceName}.");
                    }
                }
            }
            else
            {
                var instance = dataCollectionINI.SSSController.NetworkCardName;
                instance = instance.Replace("(", "[");
                instance = instance.Replace(")", "]");

                var bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", instance);
                var dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
                var dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
                lstNetworlInfo.Add(new NetworkInfo(bandwidthCounter, dataSentCounter, dataReceivedCounter));
            }
        }

        public void GetData()
        {
            try
            {
                HDD_UseRate = GetHDUsage().ToString();
                CPU_UseRate = Convert.ToInt32(cpu.NextValue()).ToString();
                Memory_UseRate = Convert.ToInt32(GetMemoryUseRate()).ToString();
                Network_UseRate = lstNetworlInfo.Average(n => ((8 * (Convert.ToInt32(n.dataSentCounter.NextValue()) + Convert.ToInt32(n.dataReceivedCounter.NextValue()))) / (Convert.ToInt32(n.bandwidthCounter.NextValue()) / 100))).ToString();

                //((8 * (Convert.ToInt32(dataSent) + Convert.ToInt32(dataReceived))) / (Convert.ToInt32(bandwidth) / 100)).ToString();
            }
            catch (Exception ex)
            {
                HDD_UseRate = "0";
                CPU_UseRate = "0";
                Memory_UseRate = "0";
                Network_UseRate = "0";

                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
            }
        }

        public void GetEmptyData()
        {
            HDD_UseRate = "0";
            CPU_UseRate = "0";
            Memory_UseRate = "0";
            Network_UseRate = "0";
        }

        public  SSSControllerDataCache()
        {
            HDD_UseRate = "0";
            CPU_UseRate = "0";
            Memory_UseRate = "0";
            Network_UseRate = "0";
        }

        public int GetHDUsage()
        {
            int hdUsage = 0;

            foreach (ManagementObject Disk in hd.Get())
            {
                //總空間
                decimal totalSize = Convert.ToDecimal(Disk["Size"].ToString());
                //可用空間
                decimal usageSize = Convert.ToDecimal(Disk["FreeSpace"].ToString());
                //使用率(0~100)
                hdUsage += Convert.ToInt16(Math.Round(100 - (usageSize / totalSize * 100)));
            }
            hdUsage = hdUsage / hd.Get().Count;
            return hdUsage;
        }

        private static decimal GetMemoryUseRate()
        {
            Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
            decimal percentFree = ((decimal)phav / (decimal)tot) * 100;
            decimal percentOccupied = 100 - percentFree;
            return percentOccupied;
        }

        public static class PerformanceInfo
        {
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

            [StructLayout(LayoutKind.Sequential)]
            public struct PerformanceInformation
            {
                public int Size;
                public IntPtr CommitTotal;
                public IntPtr CommitLimit;
                public IntPtr CommitPeak;
                public IntPtr PhysicalTotal;
                public IntPtr PhysicalAvailable;
                public IntPtr SystemCache;
                public IntPtr KernelTotal;
                public IntPtr KernelPaged;
                public IntPtr KernelNonPaged;
                public IntPtr PageSize;
                public int HandlesCount;
                public int ProcessCount;
                public int ThreadCount;
            }

            public static long GetPhysicalAvailableMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }
            }

            public static long GetTotalMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    public class NetworkInfo
    {
        public PerformanceCounter bandwidthCounter;
        public PerformanceCounter dataSentCounter;
        public PerformanceCounter dataReceivedCounter;

        public NetworkInfo(PerformanceCounter _bandwidthCounter, PerformanceCounter _dataSentCounter, PerformanceCounter _dataReceivedCounter)
        {
            bandwidthCounter = _bandwidthCounter;
            dataSentCounter = _dataSentCounter;
            dataReceivedCounter = _dataReceivedCounter;
        }
    }
}