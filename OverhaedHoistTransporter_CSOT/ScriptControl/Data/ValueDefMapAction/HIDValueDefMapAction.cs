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
    public class HIDValueDefMapAction : IValueDefMapAction
    {
        public const string DEVICE_NAME_HID = "HID";
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        AEQPT eqpt = null;
        protected SCApplication scApp = null;
        protected BCFApplication bcfApp = null;

        public HIDValueDefMapAction()
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
            var function = scApp.getFunBaseObj<HIDToOHxC_ChargeInfo>(eqpt.EQPT_ID) as HIDToOHxC_ChargeInfo;
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
                scApp.putFunBaseObj<HIDToOHxC_ChargeInfo>(function);
            }
        }

        public virtual void HID_ChargeInfo(object sender, ValueChangedEventArgs args)
        {
            var function = scApp.getFunBaseObj<HIDToOHxC_ChargeInfo>(eqpt.EQPT_ID) as HIDToOHxC_ChargeInfo;
            try
            {
                //1.建立各個Function物件
                function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                //2.read log
                function.Timestamp = DateTime.Now;
                //LogManager.GetLogger("com.mirle.ibg3k0.sc.Common.LogHelper").Info(function.ToString());
                NLog.LogManager.GetCurrentClassLogger().Info(function.ToString());
                eqpt.HID_Info = function;

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
                scApp.putFunBaseObj<HIDToOHxC_ChargeInfo>(function);
            }
        }
        const string HID_IGBT_A_ALARM_CODE = "1";
        const string HID_IGBT_B_ALARM_CODE = "2";
        const string HID_TEMPERATURE_ALARM_CODE = "3";
        const string HID_POWER_ALARM_CODE = "4";
        const string HID_EMI_ALARM_CODE = "5";
        const string HID_SMOKE_ALARM_CODE = "6";
        const string HID_SAFE_CIRCUIT_ALARM_CODE = "7";
        const string HID_FAN_1_ALARM_CODE = "8";
        const string HID_FAN_2_ALARM_CODE = "9";
        const string HID_FAN_3_ALARM_CODE = "10";
        const string HID_TIMEOUT_ALARM_CODE = "11";


        public virtual void IGBT_A_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_IGBT_A_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_IGBT_A_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.IGBT_A_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_IGBT_A_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_IGBT_A_Alarm>(recevie_function);
            }
        }

        public virtual void IGBT_B_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_IGBT_B_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_IGBT_B_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.IGBT_B_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_IGBT_B_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_IGBT_B_Alarm>(recevie_function);
            }
        }

        public virtual void TempAlarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_TempAlarm>(eqpt.EQPT_ID) as HIDToOHxC_TempAlarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.TempAlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_TEMPERATURE_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_TempAlarm>(recevie_function);
            }
        }

        public virtual void PowerAlarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_PowerAlarm>(eqpt.EQPT_ID) as HIDToOHxC_PowerAlarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.PowerAlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_POWER_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_PowerAlarm>(recevie_function);
            }
        }

        public virtual void EMIAlarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_EMI_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_EMI_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.EMI_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_EMI_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_EMI_Alarm>(recevie_function);
            }
        }

        public virtual void SmokeAlarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Smoke_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_Smoke_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.SmokeAlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_SMOKE_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Smoke_Alarm>(recevie_function);
            }
        }

        public virtual void SafeCircuitAlarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_SafeCircuitAlarm>(eqpt.EQPT_ID) as HIDToOHxC_SafeCircuitAlarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.SafeCircuitHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_SAFE_CIRCUIT_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_SafeCircuitAlarm>(recevie_function);
            }
        }

        public virtual void Fan_1_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Fan_1_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_Fan_1_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Fan_1_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_FAN_1_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Fan_1_Alarm>(recevie_function);
            }
        }
       
        public virtual void Fan_2_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Fan_2_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_Fan_2_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Fan_2_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_FAN_2_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Fan_2_Alarm>(recevie_function);
            }
        }

        public virtual void Fan_3_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_Fan_3_Alarm>(eqpt.EQPT_ID) as HIDToOHxC_Fan_3_Alarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.Fan_3_AlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_FAN_3_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_Fan_3_Alarm>(recevie_function);
            }
        }

        public virtual void Timeout_Alarm(object sender, ValueChangedEventArgs args)
        {
            var recevie_function = scApp.getFunBaseObj<HIDToOHxC_TimeoutAlarm>(eqpt.EQPT_ID) as HIDToOHxC_TimeoutAlarm;
            try
            {
                //1.建立各個Function物件
                recevie_function.Read(bcfApp, eqpt.EqptObjectCate, eqpt.EQPT_ID);
                recevie_function.EQ_ID = eqpt.EQPT_ID;
                //2.read log
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: DEVICE_NAME_HID,
                         Data: recevie_function.ToString(),
                         VehicleID: eqpt.EQPT_ID);
                NLog.LogManager.GetLogger("HIDAlarm").Info(recevie_function.ToString());
                ProtocolFormat.OHTMessage.ErrorStatus status = recevie_function.TimeoutAlarmHappend ?
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrSet :
                                                                    ProtocolFormat.OHTMessage.ErrorStatus.ErrReset;
                scApp.LineService.ProcessAlarmReport(eqpt.NODE_ID, eqpt.EQPT_ID, eqpt.Real_ID, "", HID_TIMEOUT_ALARM_CODE, status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
                scApp.putFunBaseObj<HIDToOHxC_TimeoutAlarm>(recevie_function);
            }
        }
       
        public virtual void HID_Control(bool control)
        {
            var function = scApp.getFunBaseObj<OHxCToHID_Control>(eqpt.EQPT_ID) as OHxCToHID_Control;
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
                scApp.putFunBaseObj<OHxCToHID_Control>(function);
            }
        }


        object dateTimeSyneObj = new object();
        uint dateTimeIndex = 0;
        public virtual void DateTimeSyncCommand(DateTime dateTime)
        {

            OHxCToHID_DateTimeSync send_function =
               scApp.getFunBaseObj<OHxCToHID_DateTimeSync>(eqpt.EQPT_ID) as OHxCToHID_DateTimeSync;
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
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(HIDValueDefMapAction), Device: SCAppConstants.DeviceName.DEVICE_NAME_HID,
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
                scApp.putFunBaseObj<OHxCToHID_DateTimeSync>(send_function);
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
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_TRIGGER", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => HID_ChargeInfo(_sender, _e);
                }

                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_IGBT_A_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => IGBT_A_Alarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_IGBT_B_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => IGBT_B_Alarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_TEMP_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => TempAlarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_POWER_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => PowerAlarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_EMI_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => EMIAlarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_SMOKE_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => SmokeAlarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_SAFE_CIRCUIT_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => SafeCircuitAlarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_FAN_1_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Fan_1_Alarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_FAN_2_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Fan_2_Alarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_FAN_3_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Fan_3_Alarm(_sender, _e);
                }
                if (bcfApp.tryGetReadValueEventstring(eqpt.EqptObjectCate, eqpt.EQPT_ID, "HID_TO_OHXC_TIMEOUT_ALARM", out vr))
                {
                    vr.afterValueChange += (_sender, _e) => Timeout_Alarm(_sender, _e);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
            }

        }


    }
}
