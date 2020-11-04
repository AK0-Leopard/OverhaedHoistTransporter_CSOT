//*********************************************************************************
//      MTLValueDefMapAction.cs
//*********************************************************************************
// File Name: MTLValueDefMapAction.cs
// Description: 
//
//(c) Copyright 2018, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
//**********************************************************************************
using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.bcf.Common.MPLC;
using com.mirle.ibg3k0.bcf.Controller;
using com.mirle.ibg3k0.bcf.Data.ValueDefMapAction;
using com.mirle.ibg3k0.bcf.Data.VO;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.PLC_Functions;
using KingAOP;
using NLog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace com.mirle.ibg3k0.sc.Data.ValueDefMapAction
{
    public class HIDValueDefMapActionPH2 : IValueDefMapAction
    {
        public const string DEVICE_NAME_HID = "HID";
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        AEQPT eqpt = null;
        protected SCApplication scApp = null;
        protected BCFApplication bcfApp = null;

        public HIDValueDefMapActionPH2()
            : base()
        {
            scApp = SCApplication.getInstance();
            bcfApp = scApp.getBCFApplication();
        }
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new AspectWeaver(parameter, this);
        }

        public virtual string getIdentityKey()
        {
            return this.GetType().Name;
        }
        public virtual void setContext(BaseEQObject baseEQ)
        {
            this.eqpt = baseEQ as AEQPT;

        }
        public virtual void unRegisterEvent()
        {
            //not implement
        }
        public virtual void doShareMemoryInit(BCFAppConstants.RUN_LEVEL runLevel)
        {
            try
            {
                switch (runLevel)
                {
                    case BCFAppConstants.RUN_LEVEL.ZERO:
                        initHID_ChargeInfo();
                        break;
                    case BCFAppConstants.RUN_LEVEL.ONE:
                        break;
                    case BCFAppConstants.RUN_LEVEL.TWO:
                        break;
                    case BCFAppConstants.RUN_LEVEL.NINE:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
            }
        }

        public virtual void initHID_ChargeInfo()
        {
            var function = scApp.getFunBaseObj<HIDToOHxC_ChargeInfoPH2>(eqpt.EQPT_ID) as HIDToOHxC_ChargeInfoPH2;
            try
            {
                //1.建立各個Function物件
                function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                //2.read log
                function.Timestamp = DateTime.Now;
                //LogManager.GetLogger("com.mirle.ibg3k0.sc.Common.LogHelper").Info(function.ToString());
                NLog.LogManager.GetCurrentClassLogger().Info(function.ToString());
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(EQStatusReport), Device: DEVICE_NAME_MTL,
                //    XID: eqpt.EQPT_ID, Data: function.ToString());
                //3.logical (include db save)

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_ChargeInfoPH2>(function);
            }
        }

        public virtual void HID_ChargeInfo(object sender, ValueChangedEventArgs args)
        {
            var function = scApp.getFunBaseObj<HIDToOHxC_ChargeInfoPH2>(eqpt.EQPT_ID) as HIDToOHxC_ChargeInfoPH2;
            try
            {
                //1.建立各個Function物件
                function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                //2.read log
                function.Timestamp = DateTime.Now;
                //LogManager.GetLogger("com.mirle.ibg3k0.sc.Common.LogHelper").Info(function.ToString());
                NLog.LogManager.GetCurrentClassLogger().Info(function.ToString());
                HIDToOHxC_ChargeInfo hid_info = new HIDToOHxC_ChargeInfo();
                hid_info.HID_ID = function.HID_ID;
                hid_info.WR_Converted = function.WR_Converted;
                hid_info.WS_Converted = function.WS_Converted;
                hid_info.WT_Converted = function.WT_Converted;
                hid_info.AR_Converted = function.AR_Converted;
                hid_info.AS_Converted = function.AS_Converted;
                hid_info.AT_Converted = function.AT_Converted;
                hid_info.VR_Converted = function.VR_Converted;
                hid_info.VS_Converted = function.VS_Converted;
                hid_info.VT_Converted = function.VT_Converted;
                hid_info.Hour_Negative_Converted = function.Hour_Negative_Converted;
                hid_info.Hour_Positive_Converted = function.Hour_Positive_Converted;
                hid_info.Hour_Sigma_Converted = function.Hour_Sigma_Converted;
                hid_info.Sigma_W_Converted = function.Sigma_W_Converted;
                hid_info.Sigma_A_Converted = function.Sigma_A_Converted;
                hid_info.Sigma_V_Converted = function.Sigma_V_Converted;

                eqpt.HID_Info = hid_info;

                eqpt.Eq_Alive_Last_Change_time = DateTime.Now;

                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(EQStatusReport), Device: DEVICE_NAME_MTL,
                //    XID: eqpt.EQPT_ID, Data: function.ToString());
                //3.logical (include db save)

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_ChargeInfoPH2>(function);
            }
        }
        //const string HID_IGBT_A_ALARM_CODE = "1";
        //const string HID_IGBT_B_ALARM_CODE = "2";
        //const string HID_TEMPERATURE_ALARM_CODE = "3";
        //const string HID_POWER_ALARM_CODE = "4";
        //const string HID_EMI_ALARM_CODE = "5";
        //const string HID_SMOKE_ALARM_CODE = "6";
        //const string HID_SAFE_CIRCUIT_ALARM_CODE = "7";
        //const string HID_FAN_1_ALARM_CODE = "8";
        //const string HID_FAN_2_ALARM_CODE = "9";
        //const string HID_FAN_3_ALARM_CODE = "10";
        //const string HID_TIMEOUT_ALARM_CODE = "11";


        const string HID_ALARM_1_CODE = "1";
        const string HID_ALARM_2_CODE = "2";
        const string HID_ALARM_3_CODE = "3";
        const string HID_ALARM_4_CODE = "4";
        const string HID_ALARM_5_CODE = "5";
        const string HID_ALARM_6_CODE = "6";
        const string HID_ALARM_7_CODE = "7";
        const string HID_ALARM_8_CODE = "8";
        const string HID_ALARM_9_CODE = "9";
        const string HID_ALARM_10_CODE = "10";


        public virtual void Alarm_1(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_1>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_1;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_1_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_1_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_1>(recevie_function);
            }
        }


        public virtual void Alarm_2(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_2>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_2;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_2_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_2_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_2>(recevie_function);
            }
        }


        public virtual void Alarm_3(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_3>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_3;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_3_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_3_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_3>(recevie_function);
            }
        }

        public virtual void Alarm_4(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_4>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_4;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_4_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_4_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_4>(recevie_function);
            }
        }

        public virtual void Alarm_5(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_5>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_5;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_5_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_5_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_5>(recevie_function);
            }
        }
        public virtual void Alarm_6(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_6>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_6;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_6_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_6_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_6>(recevie_function);
            }
        }
        public virtual void Alarm_7(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_7>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_7;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_7_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_7_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_7>(recevie_function);
            }
        }

        public virtual void Alarm_8(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_8>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_8;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_8_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_8_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_8>(recevie_function);
            }
        }
        public virtual void Alarm_9(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_9>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_9;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_9_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_9_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_9>(recevie_function);
            }
        }

        public virtual void Alarm_10(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Alarm_10>(eqpt.EQPT_ID) as HIDToOHxC_Alarm_10;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Alarm_10_Happend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_ALARM_10_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Alarm_10>(recevie_function);
            }
        }


        public virtual void HID_Control(bool control)
        {
            var function = scApp.getFunBaseObj<OHxCToHID_ControlPH2>(eqpt.EQPT_ID) as OHxCToHID_ControlPH2;
            try
            {
                //1.建立各個Function物件

                function.Control = control;
                function.Write(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                function.Timestamp = DateTime.Now;


                //2.write log
                //LogManager.GetLogger("com.mirle.ibg3k0.sc.Common.LogHelper").Info(function.ToString());
                NLog.LogManager.GetCurrentClassLogger().Info(function.ToString());

                //3.logical (include db save)
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<OHxCToHID_ControlPH2>(function);
            }
        }


        object dateTimeSyneObj = new object();
        uint dateTimeIndex = 0;
        public virtual void DateTimeSyncCommand(DateTime dateTime)
        {

            OHxCToMtl_DateTimeSyncPH2 send_function =
               scApp.getFunBaseObj<OHxCToMtl_DateTimeSyncPH2>(eqpt.EQPT_ID) as OHxCToMtl_DateTimeSyncPH2;
            try
            {
                lock (dateTimeSyneObj)
                {
                    //1.準備要發送的資料
                    send_function.Year = Convert.ToUInt16(dateTime.Year.ToString(), 10);
                    send_function.Month = Convert.ToUInt16(dateTime.Month.ToString(), 10);
                    send_function.Day = Convert.ToUInt16(dateTime.Day.ToString(), 10);
                    send_function.Hour = Convert.ToUInt16(dateTime.Hour.ToString(), 10);
                    send_function.Min = Convert.ToUInt16(dateTime.Minute.ToString(), 10);
                    send_function.Sec = Convert.ToUInt16(dateTime.Second.ToString(), 10);
                    if (dateTimeIndex >= 9999)
                        dateTimeIndex = 0;
                    send_function.Index = ++dateTimeIndex;
                    //2.紀錄發送資料的Log
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: SCAppConstants.DeviceName.DEVICE_NAME_HID,
                             Data: send_function.ToString(),
                             XID: eqpt.EQPT_ID);
                    //3.發送訊息
                    send_function.Write(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<OHxCToMtl_DateTimeSyncPH2>(send_function);
            }
        }



        object SilentSyneObj = new object();
        uint silentIndex = 0;
        public virtual void SilentCommand()
        {

            OHxCToHID_SilentIndexPH2 send_function =
               scApp.getFunBaseObj<OHxCToHID_SilentIndexPH2>(eqpt.EQPT_ID) as OHxCToHID_SilentIndexPH2;
            try
            {
                lock (SilentSyneObj)
                {
                    if (silentIndex >= 9999)
                        silentIndex = 0;
                    send_function.Index = ++silentIndex;
                    //2.紀錄發送資料的Log
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapActionPH2), Device: SCAppConstants.DeviceName.DEVICE_NAME_HID,
                             Data: send_function.ToString(),
                             XID: eqpt.EQPT_ID);
                    //3.發送訊息
                    send_function.Write(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<OHxCToHID_SilentIndexPH2>(send_function);
            }
        }


        string event_id = string.Empty;
        /// <summary>
        /// Does the initialize.
        /// </summary>
        public virtual void doInit()
        {
            try
            {
                ValueRead vr = null;
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_TRIGGER_PH2", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => HID_ChargeInfo(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_1", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_1(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_2", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_2(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_3", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_3(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_4", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_4(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_5", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_5(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_6", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_6(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_7", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_7(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_8", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_8(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_9", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_9(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_ALARM_10", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Alarm_10(_sender, _e);
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
            }

        }


    }
}
