using Mirle.BigDataCollection.DataCollection.Cache;
using Mirle.BigDataCollection.DataCollection.Collection;
using Mirle.BigDataCollection.DataCollection.SignalCollection;
using Mirle.BigDataCollection.Define;
using Mirle.BigDataCollection.Info;
using Mirle.LCS.FFUC;
using Mirle.MPLC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Linq;
using Mirle.BigDataCollection.DataCollection.Data;


namespace Mirle.BigDataCollection.DataCollection.Controller
{
    public class OHTDataCollectionController : IController
    {
        private Dictionary<string, string> _ffuMplcAddrMapping = new Dictionary<string, string>();

        public MESSAGE _message;
        public string _sqlite = "LCSCODE.DB";
        public SSSControllerDataCache _computerInfo;
        public Dictionary<TracePositionKey, SUBEQP> _dicTracePosition = new Dictionary<TracePositionKey, SUBEQP>();
        public List<SUBEQP> sendList = new List<SUBEQP>();
        public UPSDataCache _upsInfo;

        public FFUController _ffuc;

        public OHTDataCollectionController(IMPLCProvider smReadWriter, LoggerService loggerService, DataCollectionINI dataCollectionINI) : base(smReadWriter, loggerService, dataCollectionINI)
        {
            try
            {
                _sqlite = dataCollectionINI.DataBase.SqlitePath;
                _loggerService.WriteLog("Trace", $"{_sqlite}");

                //var sql = OpenConnection(_sqlite);

                _computerInfo = new SSSControllerDataCache(_dataCollectionINI, _loggerService);
                //_upsInfo = new UPSDataCache(loggerService);
                //_upsInfo = new UPSDataCache(_dataCollectionINI, _loggerService);

                if (dataCollectionINI.STKC.FFUType == (int)FFUType.MDTU)
                {
                    _loggerService.WriteLog("Trace", "FFUType is MDTU, Ctrate FFUController Object.");

                    _ffuc = new FFUController(_dataCollectionINI.STKC.MDTUFFUIpAddr, _dataCollectionINI.STKC.MDTUFFUPort, false);
                    _ffuc.EnableCache = false;
                    _ffuc.RefreshInterval = _dataCollectionINI.STKC.MDTUFFURefreshInterval;
                }
            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }
        }

        public void ReconnectUps()
        {
            _loggerService.WriteLog("Trace", "Reconnect UPS.");
            _upsInfo = new UPSDataCache(_dataCollectionINI, _loggerService);
        }

        public void Inital(MessageParameter mp, Device device)
        {
            _message = new MESSAGE(mp);

            _dicTracePosition.Clear();
            int craneNumber = (device.ControlMode == ControlModeTypes.Single || device.ControlMode == ControlModeTypes.OhtMode) ? 1 : 36;
            int forkNumber = (device.ControlMode == ControlModeTypes.Single || device.ControlMode == ControlModeTypes.Dual) ? 1 : 2;

            try
            {
                InitialMotorDataFormat(device, craneNumber, forkNumber);
                _loggerService.WriteLog("Trace", "Initial Motor Succ");

                InitialPressureDataFormat(device, craneNumber);
                _loggerService.WriteLog("Trace", "Initial Pressure Succ");

                //InitialFFUDataFormat(device);
                //_loggerService.WriteLog("Trace", "Initial FFU Succ");

                InitialUPSDataFormat(device);
                _loggerService.WriteLog("Trace", "Initial UPS");

                InitailControllerDataFormat(device, craneNumber);
                _loggerService.WriteLog("Trace", "Initial Controller");

                InitialEnergy(device, craneNumber);
                _loggerService.WriteLog("Trace", "Initial Energy");

                _loggerService.WriteLog("Trace", $"Trace Count : {_dicTracePosition.Count}");
            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }
        }

        private void InitialEnergy(Device device, int craneNumber)
        {
            for (int craneNo = 1; craneNo <= craneNumber; craneNo++)
            {
                SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_M0{craneNo}_Energy" };

                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Electric_energy" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Electric_energy_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineEnergy.Electric_energy_SPEC_MAX.ToString().ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Electric_energy_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineEnergy.Electric_energy_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageRS" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageRS_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineEnergy.VoltageRS_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageRS_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineEnergy.VoltageRS_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageST" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageST_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineEnergy.VoltageST_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"VoltageST_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineEnergy.VoltageST_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentR" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentR_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineEnergy.CurrentR_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentR_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineEnergy.CurrentR_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentT" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentT_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineEnergy.CurrentT_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CurrentT_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineEnergy.CurrentT_SPEC_MIN.ToString() });
                _dicTracePosition.Add(new TracePositionKey(CollectionTypes.ENERGY, _dataCollectionINI.ReportFrequency.EnergyFrequency), item);
            }
        }

        private void InitialUPSDataFormat(Device device)
        {
            SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_HP_UPS" };

            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Battery_Status" });
            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Battery_Tempreture" });
            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Tempreture_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineUPS.Battery_Tempreture_SPEC_MAX.ToString() });
            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Tempreture_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineUPS.Battery_Tempreture_SPEC_MIN.ToString() });

            _dicTracePosition.Add(new TracePositionKey(CollectionTypes.UPS, _dataCollectionINI.ReportFrequency.UPSFrequency), item);
        }

        private void InitailControllerDataFormat(Device device, int craneNumber)
        {
            //SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_Controller" };
            for (int craneNo = 1; craneNo <= craneNumber; craneNo++)
            {
                SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_M0{craneNo}_Controller" };

                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_HDD_UseRate" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_HDD_UseRate_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineSSSController.HDD_UseRate_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"HDD_UseRate_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineSSSController.HDD_UseRate_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CPU_UseRate" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CPU_UseRate_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineSSSController.CPU_UseRate_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CPU_UseRate_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineSSSController.CPU_UseRate_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Memory_UseRate" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Memory_UseRate_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineSSSController.Memory_UseRate_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Memory_UseRate_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineSSSController.Memory_UseRate_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Flow_UseRate" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Flow_UseRate_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineSSSController.Flow_UseRate_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Flow_UseRate_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineSSSController.Flow_UseRate_SPEC_MIN.ToString() });

                _dicTracePosition.Add(new TracePositionKey(CollectionTypes.CONTROLLER, _dataCollectionINI.ReportFrequency.ControllerFrequency), item);
            }
        }

        private void InitialFFUDataFormat(Device device)
        {
            _ffuMplcAddrMapping = new Dictionary<string, string>();

            string sqliteQuery = $"Select * From STKFFU WHERE StockerID='{device.StockerID}' " +
                                 $"AND CAST(MPLC AS INT) >= {_dataCollectionINI.STKC.FFUStartAddr} " +
                                 $"AND CAST(MPLC AS INT) <= {_dataCollectionINI.STKC.FFUMaxEndAddr} Order by FFUID ASC";

            if (_dataCollectionINI.STKC.FFUType == (int)FFUType.MDTU)
            {
                sqliteQuery = $"Select * From STKFFU WHERE StockerID='{device.StockerID}' " +
                              $"AND CAST(MPLC AS INT) >= 422001 Order by FFUID ASC";
            }

            foreach (var row in GetDataTableBySQLite(_sqlite, sqliteQuery).AsEnumerable())
            {
                var ffuID = row["FFUID"].ToString();
                var shelfID = row["ShelfID"].ToString();
                string bank = shelfID.Substring(0, 2);
                string mplcAddr = row["MPLC"].ToString();

                if (!_ffuMplcAddrMapping.ContainsKey(ffuID))
                {
                    _ffuMplcAddrMapping.Add(ffuID, mplcAddr);
                }

                SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_Bank{bank}_Position_FFU-{ffuID}" };

                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CommadSpeed_Motor" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Rotote_Speed_Motor" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Ratio_Speed_Motor" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CommandSpeed_Motor_MAX", PARAM_VALUE = _dataCollectionINI.DefineFFU.CommandSpeed_Motor_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"CommandSpeed_Motor_MIN", PARAM_VALUE = _dataCollectionINI.DefineFFU.CommandSpeed_Motor_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Rotote_Speed_Motor_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineFFU.Rotote_Speed_Motor_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Rotote_Speed_Motor_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineFFU.Rotote_Speed_Motor_SPEC_MIN.ToString() });

                _dicTracePosition.Add(new TracePositionKey(CollectionTypes.FFU, ffuID, _dataCollectionINI.ReportFrequency.FFUFrequency), item);
            }
        }

        private void InitialPressureDataFormat(Device device, int craneNo)
        {
            SUBEQP item = new SUBEQP() { UNITNAME = $"{device.StockerID}_M0{craneNo}_Pressure" };

            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Pressure_Difference" });
            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Pressure_Difference_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MAX.ToString() });
            item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Pressure_Difference_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MIN.ToString() });

            _dicTracePosition.Add(new TracePositionKey(CollectionTypes.PRESSURE, _dataCollectionINI.ReportFrequency.PressureFrequency), item);

            //item = new SUBEQP() { UNITNAME = $"{device.StockerID}_Mid_Pressure" };

            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Mid_Pressure_Difference" });
            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Mid_Pressure_Difference_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MAX.ToString() });
            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"Mid_Pressure_Difference_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MIN.ToString() });

            //_dicTracePosition.Add(new TracePositionKey(CollectionTypes.PRESSURE, 2, _dataCollectionINI.ReportFrequency.PressureFrequency), item);

            //item = new SUBEQP() { UNITNAME = $"{device.StockerID}_OP_Pressure" };

            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"OP_Pressure_Difference" });
            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"OP_Pressure_Difference_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MAX.ToString() });
            //item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"OP_Pressure_Difference_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefinePressure.Pressure_Difference_SPEC_MIN.ToString() });

            //_dicTracePosition.Add(new TracePositionKey(CollectionTypes.PRESSURE, 3, _dataCollectionINI.ReportFrequency.PressureFrequency), item);
        }

        private void InitialMotorDataFormat(Device device, int craneNumber, int forkNumber)
        {
            for (int craneNo = 1; craneNo <= craneNumber; craneNo++)
            {
                int unitNo = 1;
                SUBEQP item;
                item = new SUBEQP() { UNITNAME = $"{device.StockerID}_M0{craneNo}" };

                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Command_Position" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Current_Position" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Torque" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Deviation" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Speed" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Load_Factor" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Regenerative_Load_Factor" });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Command_Position_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Command_Position_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Command_Position_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Command_Position_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Current_Position_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Current_Position_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Current_Position_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Current_Position_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Torque_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Torque_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Torque_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Torque_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Deviation_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Deviation_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Deviation_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Deviation_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Speed_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Speed_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Speed_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Speed_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Load_Factor_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Load_Factor_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Load_Factor_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Load_Factor_SPEC_MIN.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Regenerative_Load_Factor_SPEC_MAX", PARAM_VALUE = _dataCollectionINI.DefineMotor.Regenerative_Load_Factor_SPEC_MAX.ToString() });
                item.PARAM_LIST.PARAM.Add(new PARAM { PARAM_NAME = $"M0{craneNo}_Regenerative_Load_Factor_SPEC_MIN", PARAM_VALUE = _dataCollectionINI.DefineMotor.Regenerative_Load_Factor_SPEC_MIN.ToString() });

                _dicTracePosition.Add(new TracePositionKey(CollectionTypes.MOTOR, unitNo, craneNo, _dataCollectionINI.ReportFrequency.MotorFrequency), item);
            }
        }

        public void DataCollectionByOht(Device device, List<string> lsFile, string vhID)
        {
            try
            {
                string report_key = $"{device.StockerID}_M{vhID}";
                var DIC_BY_VH = _dicTracePosition.Where(dis => dis.Value.UNITNAME.Contains(report_key)).ToList();
                foreach (var path in lsFile)
                {
                    DataTable dt = new DataTable();
                    dt = Data.OhtRawData.OpenCSV(path);

                    foreach (KeyValuePair<TracePositionKey, SUBEQP> item in DIC_BY_VH)
                    {
                        //item.Key.sendRemainingSeconds++;

                        //if (item.Key.sendRemainingSeconds != item.Key.Frequency)
                        //    continue;

                        item.Key.sendRemainingSeconds = 0;

                        switch (item.Key.Collection)
                        {
                            case CollectionTypes.MOTOR:
                                //STKMotorDataCache stkMotorData = new STKMotorDataCache(new STKMotorDataCollection(item.Key.CraneNo, item.Key.UnitNo, _SMReadWriter));


                                STKMotorDataCache stkMotorData = new STKMotorDataCache();

                                foreach (DataRow dtr in dt.Rows)
                                {

                                    stkMotorData.Command_Position = dtr[9].ToString();
                                    stkMotorData.Current_Position = dtr[10].ToString();
                                    stkMotorData.Torque = dtr[11].ToString();
                                    stkMotorData.Speed = dtr[12].ToString();
                                    stkMotorData.Deviation = "0";
                                    stkMotorData.Load_Factor = "0";
                                    stkMotorData.Regenerative_Load_Factor = "0";

                                    sendList.Add(MotorDataMapping(item, stkMotorData));
                                }


                                break;

                            case CollectionTypes.CONTROLLER:

                                SSSControllerDataCache sSSControllerData = new SSSControllerDataCache();
                                foreach (DataRow dtr in dt.Rows)
                                {
                                    sSSControllerData.CPU_UseRate = dtr[5].ToString();
                                    sSSControllerData.Memory_UseRate = dtr[6].ToString();
                                    sSSControllerData.HDD_UseRate = dtr[7].ToString();
                                    sSSControllerData.Network_UseRate = "0";

                                    sendList.Add(ControllerDataMapping(item, sSSControllerData));
                                }

                                break;

                            case CollectionTypes.FFU:
                                STKFFUDataCache stkFFUData = new STKFFUDataCache(new FFUDataCollection(_ffuMplcAddrMapping[item.Key.UnitName], _SMReadWriter));
                                if (_dataCollectionINI.STKC.FFUType == (int)FFUType.MDTU)
                                {
                                    _loggerService.WriteLog("Trace", $"Get MDTU FFU Data, _ffuc Connected Status is : {_ffuc.IsConnected}");
                                    stkFFUData = new STKFFUDataCache(item.Key.UnitName, _ffuc);
                                }
                                sendList.Add(UPSDataMapping(item, stkFFUData));
                                break;

                            case CollectionTypes.UPS:
                                if (_upsInfo.isConnect)
                                {
                                    _upsInfo.GetData(true);
                                }
                                else
                                {
                                    _upsInfo.GetExceptionData();
                                }
                                sendList.Add(UPSDataMapping(item));
                                break;

                            case CollectionTypes.PRESSURE:
                                PressureDataCache stkPressureData = new PressureDataCache(new PressureCollection(item.Key.UnitNo, _SMReadWriter));
                                sendList.Add(PressureDataMapping(item, stkPressureData));
                                break;

                            case CollectionTypes.ENERGY:
                                //EnergyDataCache stkEnergyData = new EnergyDataCache(new EnergyCollection(item.Key.CraneNo, _SMReadWriter));
                                EnergyDataCache energyDataCache = new EnergyDataCache();

                                foreach (DataRow dtr in dt.Rows)
                                {

                                    energyDataCache.CurrentR = dtr[1].ToString();
                                    energyDataCache.CurrentT = dtr[1].ToString();
                                    energyDataCache.VoltageRS = dtr[2].ToString();
                                    energyDataCache.VoltageST = dtr[2].ToString();
                                    energyDataCache.ElectricEnergy = dtr[3].ToString();

                                    sendList.Add(EnergyDataMapping(item, energyDataCache));

                                }
                                break;
                        }
                    }
                }

            }
            catch (Exception ex) { _loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString()); }
        }

        private SUBEQP EnergyDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item, EnergyDataCache stkEnergyData)
        {
            SUBEQP result = item.Value;

            result.PARAM_LIST.PARAM[0].PARAM_VALUE = stkEnergyData.ElectricEnergy;
            result.PARAM_LIST.PARAM[3].PARAM_VALUE = stkEnergyData.VoltageRS;
            result.PARAM_LIST.PARAM[6].PARAM_VALUE = stkEnergyData.VoltageST;
            result.PARAM_LIST.PARAM[9].PARAM_VALUE = stkEnergyData.CurrentR;
            result.PARAM_LIST.PARAM[12].PARAM_VALUE = stkEnergyData.CurrentT;

            return result;
        }

        private SUBEQP PressureDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item, PressureDataCache stkPressureData)
        {
            SUBEQP result = item.Value;

            result.PARAM_LIST.PARAM[0].PARAM_VALUE = stkPressureData.Pressure;

            return result;
        }

        private SUBEQP UPSDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item)
        {
            SUBEQP result = item.Value;

            result.PARAM_LIST.PARAM[0].PARAM_VALUE = _upsInfo.BatteryChargingStatus;
            result.PARAM_LIST.PARAM[1].PARAM_VALUE = _upsInfo.BatteryTemperature;

            return result;
        }

        private SUBEQP UPSDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item, STKFFUDataCache stkFFUData)
        {
            SUBEQP result = item.Value;

            result.PARAM_LIST.PARAM[0].PARAM_VALUE = stkFFUData.CommadSpeed;
            result.PARAM_LIST.PARAM[1].PARAM_VALUE = _computerInfo.HDD_UseRate;
            result.PARAM_LIST.PARAM[2].PARAM_VALUE = _computerInfo.HDD_UseRate;

            return result;
        }

        private SUBEQP ControllerDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item, SSSControllerDataCache sSSControllerData)
        {
            SUBEQP result = item.Value;

            //result.PARAM_LIST.PARAM[0].PARAM_VALUE = _computerInfo.HDD_UseRate;
            //result.PARAM_LIST.PARAM[3].PARAM_VALUE = _computerInfo.CPU_UseRate;
            //result.PARAM_LIST.PARAM[6].PARAM_VALUE = _computerInfo.Memory_UseRate;
            //result.PARAM_LIST.PARAM[9].PARAM_VALUE = _computerInfo.Network_UseRate;
            result.PARAM_LIST.PARAM[0].PARAM_VALUE = sSSControllerData.HDD_UseRate;
            result.PARAM_LIST.PARAM[3].PARAM_VALUE = sSSControllerData.CPU_UseRate;
            result.PARAM_LIST.PARAM[6].PARAM_VALUE = sSSControllerData.Memory_UseRate;
            result.PARAM_LIST.PARAM[9].PARAM_VALUE = sSSControllerData.Network_UseRate;

            return result;
        }

        private SUBEQP MotorDataMapping(KeyValuePair<TracePositionKey, SUBEQP> item, STKMotorDataCache stkMotorData)
        {
            SUBEQP result = item.Value;

            result.PARAM_LIST.PARAM[0].PARAM_VALUE = stkMotorData.Command_Position;
            result.PARAM_LIST.PARAM[1].PARAM_VALUE = stkMotorData.Current_Position;
            result.PARAM_LIST.PARAM[2].PARAM_VALUE = stkMotorData.Torque;
            result.PARAM_LIST.PARAM[3].PARAM_VALUE = stkMotorData.Deviation;
            result.PARAM_LIST.PARAM[4].PARAM_VALUE = stkMotorData.Speed;
            result.PARAM_LIST.PARAM[5].PARAM_VALUE = stkMotorData.Load_Factor;
            result.PARAM_LIST.PARAM[6].PARAM_VALUE = stkMotorData.Regenerative_Load_Factor;

            return result;
        }

        public SQLiteConnection OpenConnection(string database)
        {
            var conntion = new SQLiteConnection()
            {
                ConnectionString = $"Data Source={database};Version=3;New=False;Compress=True;"
            };
            if (conntion.State == ConnectionState.Open) conntion.Close();
            conntion.Open();
            return conntion;
        }

        public DataTable GetDataTableBySQLite(string database, string sqlQuery)
        {
            var connection = OpenConnection(database);
            var dataAdapter = new SQLiteDataAdapter(sqlQuery, connection);
            var myDataTable = new DataTable();
            var myDataSet = new DataSet();
            myDataSet.Clear();
            dataAdapter.Fill(myDataSet);
            myDataTable = myDataSet.Tables[0];
            if (connection.State == ConnectionState.Open) connection.Close();
            return myDataTable;
        }
    }
}
