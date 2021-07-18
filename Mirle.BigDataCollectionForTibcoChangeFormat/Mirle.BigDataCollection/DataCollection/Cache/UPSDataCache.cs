using Mirle.BigDataCollection.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Mirle.BigDataCollection.DataCollection.Collection
{
    public class UPSDataCache
    {
        public string BatteryChargingStatus { get; set; }
        public string BatteryTemperature { get; set; }
        public bool isConnect { get; set; }

        private NetworkStream telnetStream_A;
        private TcpClient telnet_A;
        private string Telnet_out;
        private readonly LoggerService _loggerService;
        private Dictionary<string, dynamic> _upsDataList = new Dictionary<string, dynamic>();
        private DataCollectionINI _dataCollectionINI;

        public UPSDataCache(LoggerService loggerService)
        {
            _loggerService = loggerService;
            isConnect = false;
        }

        public UPSDataCache(DataCollectionINI dataCollectionINI, LoggerService loggerService)
        {
            _loggerService = loggerService;

            _loggerService.WriteLog("UPSTrace", $"Connect UPS Server.");
            _dataCollectionINI = dataCollectionINI;

            _upsDataList.Add($"{dataCollectionINI.UPS.StateOfChargeName}", 0);
            _loggerService.WriteLog("UPSTrace", $"_upsDataList.Add:{dataCollectionINI.UPS.StateOfChargeName}.");
            _upsDataList.Add($"{dataCollectionINI.UPS.TemperatureName}", 0);
            _loggerService.WriteLog("UPSTrace", $"_upsDataList.Add:{dataCollectionINI.UPS.TemperatureName}.");

            try
            {
                Telnet_Connect(dataCollectionINI.UPS.Ip, dataCollectionINI.UPS.Port);
                _loggerService.WriteLog("UPSTrace", $"Telnet_Connect:{dataCollectionINI.UPS.Ip}:{dataCollectionINI.UPS.Port}.");
                Thread.Sleep(dataCollectionINI.UPS.DelayTime);
                _loggerService.WriteLog("UPSTrace", $"DelayTime {dataCollectionINI.UPS.DelayTime} ms.");
                Telnet_SendData("");

                _loggerService.WriteLog("UPSTrace", $"Login account:{dataCollectionINI.UPS.Account}.");
                Telnet_SendData($"{dataCollectionINI.UPS.Account}");
                Thread.Sleep(dataCollectionINI.UPS.DelayTime);
                _loggerService.WriteLog("UPSTrace", $"DelayTime {dataCollectionINI.UPS.DelayTime} ms.");

                _loggerService.WriteLog("UPSTrace", $"Login password:{dataCollectionINI.UPS.Password}.");
                Telnet_SendData($"{dataCollectionINI.UPS.Password}");
                Thread.Sleep(dataCollectionINI.UPS.DelayTime);
                _loggerService.WriteLog("UPSTrace", $"DelayTime {dataCollectionINI.UPS.DelayTime} ms.");

                GetData(false);

                isConnect = true;
                _loggerService.WriteLog("UPSTrace", "Reconnect Succ.");
            }
            catch (Exception ex)
            {
                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
                _loggerService.WriteLog("UPSTrace", "Reconnect UPS Fail.");
                telnet_A = null;
                isConnect = false;
            }
        }

        public void GetExceptionData()
        {
            BatteryChargingStatus = "0";
            BatteryTemperature = "0";
            _loggerService.WriteLog("UPSTrace", "UPS GetExceptionData.");
        }

        public void GetData(bool closeConnectionAfterGetData)
        {
            try
            {
                _loggerService.WriteLog("UPSTrace", $"Cmd : detstatus -all.");
                Telnet_SendData("detstatus -all");
                Thread.Sleep(_dataCollectionINI.UPS.DelayTime);
                _loggerService.WriteLog("UPSTrace", $"DelayTime {_dataCollectionINI.UPS.DelayTime} ms.");

                string rawData = Telnet_Read();
                var arryRawData = rawData.Split(new string[] { "apc>detstatus -all", "apc>" }, StringSplitOptions.RemoveEmptyEntries);
                var dataLine = arryRawData.Last().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in dataLine)
                {
                    var data = item.Split(':');
                    if (_upsDataList.ContainsKey(data[0]))
                    {
                        _upsDataList[data[0]] = data[1].Split(' ')[1];
                        Console.WriteLine(Convert.ToString(_upsDataList[data[0]]));
                    }
                }

                BatteryChargingStatus = Convert.ToString(_upsDataList[$"{_dataCollectionINI.UPS.StateOfChargeName}"]);
                BatteryTemperature = Convert.ToString(_upsDataList[$"{_dataCollectionINI.UPS.TemperatureName}"]);

                _loggerService.WriteLog("UPSTrace", $"CloseConnectionAfterGetData:{closeConnectionAfterGetData}.");
                //取得資料後關閉連線
                if (closeConnectionAfterGetData)
                {
                    _loggerService.WriteLog("UPSTrace", "Telnet Service Closing.");
                    //bye 是關閉連線的指令
                    Telnet_SendData("bye");
                    _loggerService.WriteLog("UPSTrace", "Telnet Service Close.");
                }
            }
            catch (Exception ex)
            {
                isConnect = false;
                BatteryChargingStatus = "0";
                BatteryTemperature = "0";
                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
            }
        }

        public void GetEmptyData()
        {
            BatteryChargingStatus = "0";
            BatteryTemperature = "0";
        }

        private void Telnet_Connect(string ip, int port)
        {
            telnet_A = new TcpClient();
            telnet_A.SendTimeout = 100;
            telnet_A.ReceiveTimeout = 100;
            telnet_A.Connect(ip, port);

            if (telnet_A.Connected)
            {
                telnetStream_A = telnet_A.GetStream();
            }
        }

        private string Telnet_Read()
        {
            try
            {
                if (telnetStream_A.DataAvailable)
                {
                    byte[] bytes = new byte[telnet_A.ReceiveBufferSize];
                    int numBytesRead = telnetStream_A.Read(bytes, 0, (int)telnet_A.ReceiveBufferSize);
                    Array.Resize(ref bytes, numBytesRead);

                    Telnet_out = Encoding.ASCII.GetString(bytes);
                    return Telnet_out;
                }
                return "";
            }
            catch (Exception ex)
            {
                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
                return "";
            }
        }

        public void Telnet_SendData(string s)
        {
            byte[] bytWrite_telnet_A;

            if (s != null)
            {
                bytWrite_telnet_A = Encoding.ASCII.GetBytes(s + "\r");
                //寫入資料
                telnetStream_A.Write(bytWrite_telnet_A, 0, bytWrite_telnet_A.Length);
            }
        }
    }
}