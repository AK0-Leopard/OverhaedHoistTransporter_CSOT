using Config.Net;
using ICSharpCode.SharpZipLib.Zip;
using Mirle.BigDataCollection.DataCollection.Controller;
using Mirle.BigDataCollection.Define;
using Mirle.BigDataCollection.Info;
using Mirle.MPLC.SharedMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;
using TibcoPublisher;


namespace Mirle.BigDataCollection.DataCollection
{
    public class DataCollectionManager
    {
        private readonly DataCollectionINI _dataCollectionINI;
        private readonly Device _device;
        public readonly LoggerService _loggerService;
        public readonly STKDataCollectionController _stkDataCollectionController;
        public readonly OHTDataCollectionController _oHTDataCollectionController;
        private readonly Sender _sender;

        //dataCollectionTimer -> 資料收集 Timer
        private readonly Timer dataCollectionTimer = new Timer();

        //連線UPS Timer, Ups 服務在要取資料前N秒會嘗試連線, 取完資料後會關閉連線
        private readonly Timer reconnectUps = new Timer();

        //連線FFU Timer, Modbus 服務在要取資料前N秒會嘗試連線, 取完資料後會關閉連線
        private readonly Timer reconnectMdtuFFU = new Timer();

        //處理資料壓縮和自動刪除90天後的檔案
        private readonly Timer collatingFolderTimer = new Timer();

        public DataCollectionManager(LoggerService loggerService, SMReadWriter smReader)
        {
            _loggerService = loggerService;
            _loggerService.WriteLog("Trace", "DataCollectionManager Beginning.");

            _dataCollectionINI = new ConfigurationBuilder<DataCollectionINI>().UseIniFile(@"Config\DataCollection.ini").Build();

            _sender = new Sender(_dataCollectionINI.Tibco.Subject, _dataCollectionINI.Tibco.Service,
                                _dataCollectionINI.Tibco.Network, _dataCollectionINI.Tibco.Port);
            _loggerService.WriteException("Trace", $"Service:{_dataCollectionINI.Tibco.Service}, Network:;{_dataCollectionINI.Tibco.Network}, Port:{_dataCollectionINI.Tibco.Port}");

            //Chris Add 0713
            _device = new Device(_dataCollectionINI.STKC.DeviceID, _dataCollectionINI.STKC.ControlMode);

            try
            {
                MessageParameter mp = GetCurrentMessage();
                //_stkDataCollectionController = new STKDataCollectionController(smReader, loggerService, _dataCollectionINI);
                //_stkDataCollectionController.Inital(mp, new Device(_dataCollectionINI.STKC.DeviceID,
                //                                                   _dataCollectionINI.STKC.ControlMode));
                _oHTDataCollectionController = new OHTDataCollectionController(smReader, loggerService, _dataCollectionINI);
                _oHTDataCollectionController.Inital(mp, new Device(_dataCollectionINI.STKC.DeviceID,
                                                                  _dataCollectionINI.STKC.ControlMode));
            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }

            _loggerService.WriteLog("Trace", "DataCollectionManager StartUp Succ");

            #region Set Timer

            dataCollectionTimer.Elapsed += DataCollection_Elapsed;
            dataCollectionTimer.Interval = 1000;
            dataCollectionTimer.Start();

            reconnectUps.Elapsed += ReconnectUps_Elapsed;
            reconnectUps.Interval = 1000;
            reconnectUps.Start();

            collatingFolderTimer.Elapsed += collatingFolderTimer_Elapsed;
            collatingFolderTimer.Interval = 86400000;
            collatingFolderTimer.Start();

            if (_dataCollectionINI.STKC.FFUType == (int)FFUType.MDTU)
            {
                reconnectMdtuFFU.Elapsed += ReconnectMdtuFFU_Elapsed;
                reconnectMdtuFFU.Interval = 1000;
                reconnectMdtuFFU.Start();
            }

            #endregion Set Timer
        }

        private void collatingFolderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                collatingFolderTimer.Stop();

                _loggerService.WriteLog("Trace", "In collatingFolderTimer_Elapsed Function");

                #region CompareFolder

                var fileList = Directory.GetFileSystemEntries($"Log");
                foreach (string fileName in fileList)
                {
                    var fileName1 = fileName.Split('\\')[1];

                    bool notZipFile = !fileName1.Contains(".zip");
                    if (notZipFile)
                    {
                        DateTime fileCreateTime = DateTime.ParseExact(fileName1, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime retentionPeriod = fileCreateTime.AddDays(1);

                        bool fileOverdue = DateTime.Compare(retentionPeriod, DateTime.Now) < 0;
                        if (fileOverdue)
                        {
                            _loggerService.WriteLog("Trace", $"Folder Overdue Compare : {fileName} compare to {fileName}.zip");
                            ZipFile($"{fileName}.zip", $"{fileName}");

                            _loggerService.WriteLog("Trace", $"Folder Overdue Delete : {fileName}");
                            DirectoryInfo DIFO = new DirectoryInfo($"{fileName}");
                            DIFO.Delete(true);
                        }
                    }
                }

                #endregion CompareFolder

                #region DeleteZipLogFile

                fileList = Directory.GetFileSystemEntries($"Log", "*.zip");
                foreach (string fileName in fileList)
                {
                    var fileName1 = fileName.Split('\\')[1];
                    DateTime fileCreateTime = DateTime.ParseExact(fileName1.Split('.')[0], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime retentionPeriod = fileCreateTime.AddDays(_dataCollectionINI.STKC.ZipFileKeepDay);

                    bool fileOverdue = DateTime.Compare(retentionPeriod, DateTime.Now) < 0;
                    if (fileOverdue)
                    {
                        _loggerService.WriteLog("Trace", $"ZipFile Overdue Delete : {fileName}");
                        File.Delete(fileName);
                        _loggerService.WriteLog("Trace", $"ZipFile Overdue Delete Succ");
                    }
                }

                #endregion DeleteZipLogFile
            }
            catch (Exception ex)
            {
                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
            }
            finally
            {
                collatingFolderTimer.Start();
            }
        }

        public void ZipFile(string filename, string directory)
        {
            try
            {
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ReconnectMdtuFFU_Elapsed(object sender, ElapsedEventArgs e)
        {
            reconnectMdtuFFU.Stop();
            try
            {
                if (needStartMdtuFFU())
                {
                    _stkDataCollectionController._ffuc.Start();
                    Task.Delay(8000).Wait();
                    _loggerService.WriteLog("Trace", $"MDTU FFU Start. _ffuc Connected Status : {_stkDataCollectionController._ffuc.IsConnected}");
                }

                if (needPauseMdtuFFU() && _stkDataCollectionController._ffuc.IsConnected)
                {
                    _stkDataCollectionController._ffuc.Pause();
                    _loggerService.WriteLog("Trace", $"MDTU FFU Pause. _ffuc Connected Status : {_stkDataCollectionController._ffuc.IsConnected}");
                }
            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }
            reconnectMdtuFFU.Start();
        }

        private bool needPauseMdtuFFU()
        {
            int nowTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return ((nowTimestamp - 1) % _dataCollectionINI.ReportFrequency.FFUFrequency == 0) ? true : false;
        }

        private bool needStartMdtuFFU()
        {
            int nowTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return ((nowTimestamp + _dataCollectionINI.STKC.MDTUFFUPreRefreshTime) % _dataCollectionINI.ReportFrequency.FFUFrequency == 0) ? true : false;
        }

        private void ReconnectUps_Elapsed(object sender, ElapsedEventArgs e)
        {
            reconnectUps.Stop();
            try
            {
                if (needReconnectUps())
                {
                    _loggerService.WriteLog("Trace", "Need Reconnect UPS.");

                    _stkDataCollectionController.ReconnectUps();
                }
            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }
            reconnectUps.Start();
        }

        private bool needReconnectUps()
        {
            int nowTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return ((nowTimestamp + _dataCollectionINI.UPS.ReconnectTime) % _dataCollectionINI.ReportFrequency.UPSFrequency == 0) ? true : false;
        }

        private void DataCollection_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                dataCollectionTimer.Stop();



                //_stkDataCollectionController.DataCollection();
                for (int i = 1; i <= 36; i++)
                {
                    string csvPath = @"C:\Log\CSOT\FDC\";
                    string vhID = i.ToString().PadLeft(2, '0');

                    DirectoryInfo directoryInfo = new DirectoryInfo(csvPath);

                    var files_csv = directoryInfo.GetFiles("*.csv");
                    var files_csv_by_vh = files_csv.Where(dir => dir.Name.Contains($"T{vhID}"));
                    List<string> listFile = files_csv_by_vh.Select(file => file.FullName).ToList();



                    //foreach (var fileName in directoryInfo.GetFiles("*.csv"))
                    //{
                    //    Regex regex = new Regex("T" + vhID);
                    //    Match m = regex.Match(fileName.ToString());
                    //    if (m.Success == true)
                    //    {
                    //        listFile.Add(csvPath + fileName.ToString());
                    //    }
                    //}

                    //csvPath = csvPath + @"\OHT-T" + vhID + @"_BigData_" + DateTime.Now.ToString("yyyy-MM-dd");


                    //if (System.IO.File.Exists(csvPath))
                    //foreach(string lsFile in listFile)
                    //{
                    _oHTDataCollectionController.DataCollectionByOht(_device, listFile, vhID);

                    //_sender.conn();

                    //if (!_sender.isConn)
                    //{
                    //    _loggerService.WriteLog("Trace", $"Tibco is not Conn");
                    //    _loggerService.WriteException("Trace", $"{_sender.exMsg}");
                    //    _sender.exMsg = string.Empty;
                    //    return;
                    //}
                    //var sendList = _stkDataCollectionController.sendList;
                    var sendList = _oHTDataCollectionController.sendList;

                    var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var mp = _oHTDataCollectionController._message;
                    mp.TransactionID = dt;
                    mp.TimeStamp = dt;
                    mp.EQP_LIST.EQP.Add(new EQP
                    {
                        MACHINENAME = _dataCollectionINI.STKC.DeviceID,
                        CHECK_TIME = dt,
                        SUBEQP_LIST = new MSUBEQP(),
                    });

                    foreach (var item in sendList)
                    {
                        mp.EQP_LIST.EQP[0].SUBEQP_LIST.SUBEQP_LIST.Add(item);
                    }

                    string send = Serialize(mp);

                    if (_sender.Send(send, _dataCollectionINI.STKC.DeviceID))
                    {
                        _loggerService.WriteLog("Trace", $"Tibco Send Succ ");
                        MoveToHitoryAndCompress(files_csv_by_vh);
                    }
                    else
                    {
                        _loggerService.WriteLog("Trace", $"Tibco Send Fail : { _sender.exMsg}");
                        _loggerService.WriteException("Tibco Send Message Function", _sender.exMsg);
                    }
                    mp.EQP_LIST.EQP.Clear();

                    _oHTDataCollectionController.sendList.Clear();
                    //}
                }

            }
            catch (Exception ex)
            {
                _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
            }
            finally
            {
                dataCollectionTimer.Start();
            }
        }

        private void MoveToHitoryAndCompress(IEnumerable<FileInfo> fieldInfos)
        {
            foreach (var fieldInfo in fieldInfos)
            {
                string path = fieldInfo.DirectoryName;
                string move_to_path = $"{path}\\History\\";
                if (!System.IO.Directory.Exists(move_to_path))
                {
                    System.IO.Directory.CreateDirectory(move_to_path);
                }
                System.IO.File.Move(fieldInfo.FullName, $"{move_to_path}{fieldInfo.Name}");
            }
        }

        private MessageParameter GetCurrentMessage()
        {
            MessageParameter message = new MessageParameter();
            message.MessageID = "IOTDATAFORFDC";
            message.TransactionID = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            message.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            message.LineName = _dataCollectionINI.STKC.DeviceID;

            return message;
        }

        public static string Serialize(object dataToSerialize)
        {
            if (dataToSerialize == null) return null;

            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(dataToSerialize.GetType());
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
        }

        ~DataCollectionManager()
        {
            _loggerService.WriteLog("Trace", "DataCollectionManager Shut Down");
        }
    }
}