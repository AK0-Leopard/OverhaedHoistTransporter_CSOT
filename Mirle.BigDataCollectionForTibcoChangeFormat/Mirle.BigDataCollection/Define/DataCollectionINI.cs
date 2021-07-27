using Config.Net;

namespace Mirle.BigDataCollection.Define
{
    public interface DataCollectionINI
    {
        [Option(Alias = "STKC")]
        ISTKC STKC { get; }

        [Option(Alias = "Report Frequency")]
        IReportFrequency ReportFrequency { get; }

        [Option(Alias = "Data Base")]
        IDataBase DataBase { get; }

        [Option(Alias = "SSSController")]
        INetworkUseRate SSSController { get; }

        [Option(Alias = "UPS")]
        IUPS UPS { get; }

        [Option(Alias = "Motor Spec Value")]
        IDefineMotor DefineMotor { get; }

        [Option(Alias = "FFU Spec Value")]
        IDefineFFU DefineFFU { get; }

        [Option(Alias = "UPS Spec Value")]
        IDefineUPS DefineUPS { get; }

        [Option(Alias = "SSS Controller Spec Value")]
        IDefineSSSController DefineSSSController { get; }

        [Option(Alias = "Pressure Spec Value")]
        IDefinePressure DefinePressure { get; }

        [Option(Alias = "Energy Spec Value")]
        IDefineEnergy DefineEnergy { get; }

        [Option(Alias = "Tibco")]
        ITibco Tibco { get; }
        [Option(Alias = "OHT Spec Value")]
        IOHTData OHTData { get; }
    }
    public interface IOHTData
    {
        [Option(DefaultValue = "")]
        string OHT_FILE_PATH { get; }
    }


    public interface ITibco
    {
        [Option(DefaultValue = "WHCSOT.G6D.BC.PROD.FDC.MOUDLE")]
        string Subject { get; }

        [Option(DefaultValue = "9270")]
        string Service { get; }

        [Option(DefaultValue = "172.18.3.75")]
        string Network { get; }

        [Option(DefaultValue = "7500")]
        string Port { get; }
    }

    public interface INetworkUseRate
    {
        [Option(DefaultValue = "C,")]
        string DiskName { get; }

        [Option(DefaultValue = "Y")]
        string AutomaticSearch { get; }

        [Option(DefaultValue = "")]
        string NetworkCardName { get; }
    }

    public interface IDataBase
    {
        [Option(DefaultValue = 0)]
        int DBMS { get; }

        [Option(DefaultValue = ".")]
        string DbServer { get; }

        [Option(DefaultValue = ".")]
        string FODBServer { get; }

        [Option(DefaultValue = "")]
        string DbName { get; }

        [Option(DefaultValue = "")]
        string DbUser { get; }

        [Option(DefaultValue = "")]
        string DbPassword { get; }

        [Option(DefaultValue = 1433)]
        int DbPort { get; }

        [Option(DefaultValue = "LCSCODE.DB")]
        string SqlitePath { get; }
    }

    public interface ISTKC
    {
        [Option(DefaultValue = "")]
        string DeviceID { get; }

        [Option(DefaultValue = 1)]
        int DeviceType { get; }

        [Option(DefaultValue = 0)]
        int ControlMode { get; }

        [Option(DefaultValue = 12500)]
        int FFUStartAddr { get; }

        [Option(DefaultValue = 13500)]
        int FFUMaxEndAddr { get; }

        [Option(DefaultValue = "127.0.0.1")]
        string MDTUFFUIpAddr { get; }

        [Option(DefaultValue = 1502)]
        int MDTUFFUPort { get; }

        [Option(DefaultValue = 2000)]
        int MDTUFFURefreshInterval { get; }

        [Option(DefaultValue = 12)]
        int MDTUFFUPreRefreshTime { get; }

        [Option(DefaultValue = "DataCollection")]
        string DataCollectionSavePath { get; }

        [Option(DefaultValue = 90)]
        int ZipFileKeepDay { get; }

        [Option(DefaultValue = 1)]
        int FFUType { get; }
    }

    public interface IReportFrequency
    {
        [Option(DefaultValue = 1)]
        int MotorFrequency { get; }

        [Option(DefaultValue = 600)]
        int FFUFrequency { get; }

        [Option(DefaultValue = 600)]
        int PressureFrequency { get; }

        [Option(DefaultValue = 600)]
        int UPSFrequency { get; }

        [Option(DefaultValue = 60)]
        int ControllerFrequency { get; }

        [Option(DefaultValue = 60)]
        int EnergyFrequency { get; }

        [Option(DefaultValue = 600)]
        int CreateXMLFrequency { get; }
    }

    public interface IUPS
    {
        [Option(DefaultValue = "apc")]
        string Account { get; }

        [Option(DefaultValue = "apc")]
        string Password { get; }

        [Option(DefaultValue = "127.0.0.1")]
        string Ip { get; }

        [Option(DefaultValue = 23)]
        int Port { get; }

        [Option(DefaultValue = 30)]
        int ReconnectTime { get; }

        [Option(DefaultValue = 200)]
        int DelayTime { get; }

        [Option(DefaultValue = "Battery State Of Charge")]
        string StateOfChargeName { get; }

        [Option(DefaultValue = "Battery Temperature")]
        string TemperatureName { get; }
    }

    public interface IDefineMotor
    {
        [Option(DefaultValue = 0)]
        int Command_Position_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Command_Position_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Current_Position_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Current_Position_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Torque_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Torque_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Deviation_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Deviation_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Speed_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Speed_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Load_Factor_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Load_Factor_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Regenerative_Load_Factor_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Regenerative_Load_Factor_SPEC_MIN { get; }
    }

    public interface IDefineFFU
    {
        [Option(DefaultValue = 0)]
        int CommandSpeed_Motor_MAX { get; }

        [Option(DefaultValue = 0)]
        int CommandSpeed_Motor_MIN { get; }

        [Option(DefaultValue = 0)]
        int Rotote_Speed_Motor_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Rotote_Speed_Motor_SPEC_MIN { get; }
    }

    public interface IDefineUPS
    {
        [Option(DefaultValue = 0)]
        int Battery_Tempreture_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Battery_Tempreture_SPEC_MIN { get; }
    }

    public interface IDefineSSSController
    {
        [Option(DefaultValue = 0)]
        int HDD_UseRate_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int HDD_UseRate_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int CPU_UseRate_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int CPU_UseRate_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Memory_UseRate_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Memory_UseRate_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int Flow_UseRate_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Flow_UseRate_SPEC_MIN { get; }
    }

    public interface IDefinePressure
    {
        [Option(DefaultValue = 0)]
        int Pressure_Difference_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Pressure_Difference_SPEC_MIN { get; }
    }

    public interface IDefineEnergy
    {
        [Option(DefaultValue = 0)]
        int Electric_energy_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int Electric_energy_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int VoltageRS_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int VoltageRS_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int VoltageST_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int VoltageST_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int CurrentR_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int CurrentR_SPEC_MIN { get; }

        [Option(DefaultValue = 0)]
        int CurrentT_SPEC_MAX { get; }

        [Option(DefaultValue = 0)]
        int CurrentT_SPEC_MIN { get; }
    }
}