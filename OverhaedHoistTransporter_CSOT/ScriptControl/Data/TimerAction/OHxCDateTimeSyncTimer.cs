//*********************************************************************************
//      AGVCAliveTimer.cs
//*********************************************************************************
// File Name: AGVCAliveTimer.cs
// Description: 
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------


//**********************************************************************************
using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Controller;
using com.mirle.ibg3k0.bcf.Data.TimerAction;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.VO;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace com.mirle.ibg3k0.sc.Data.TimerAction
{
    class OHxCDateTimeSyncTimer : ITimerAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected SCApplication scApp = null;
        protected MPLCSMControl smControl;
        private DateTime lastUpdateTime = DateTime.MinValue;
        int[] syncPoint = new int[16];
        public OHxCDateTimeSyncTimer(string name, long intervalMilliSec)
            : base(name, intervalMilliSec)
        {

        }

        public override void initStart()
        {
            scApp = SCApplication.getInstance();
            //smControl = scApp.getBCFApplication().getMPLCSMControl("Charger") as MPLCSMControl;
        }
        public override void doProcess(object obj)
        {
            if (lastUpdateTime.AddHours(1) < DateTime.Now)
            {
                lastUpdateTime = DateTime.Now;
                Task.Run(() => DateTimeSync(0, "MTL"));

                Task.Run(() => DateTimeSync(1, "MTS"));
                Task.Run(() => DateTimeSync(2, "MTS2"));
                Task.Run(() => DateTimeSync(3, "HID"));
                Task.Run(() => DateTimeSync(4, "HID2"));
                Task.Run(() => DateTimeSync(5, "HID3"));
                Task.Run(() => DateTimeSync(6, "HID4"));
                Task.Run(() => DateTimeSync(7, "MTL2"));
                Task.Run(() => DateTimeSync(8, "MTL3"));
                Task.Run(() => DateTimeSync(9, "MTS3"));

                Task.Run(() => DateTimeSync(10, "HID5"));
                Task.Run(() => DateTimeSync(11, "HID6"));
                Task.Run(() => DateTimeSync(12, "HID7"));
                Task.Run(() => DateTimeSync(13, "HID8"));
                Task.Run(() => DateTimeSync(14, "HID9"));
                Task.Run(() => DateTimeSync(15, "HID10"));
            }
            else
            {
                return;
            }


        }



        private void DateTimeSync(int syncIndex, string eqID)
        {
            if (System.Threading.Interlocked.Exchange(ref syncPoint[syncIndex], 1) == 0)
            {
                AEQPT eqpt = scApp.getEQObjCacheManager().getEquipmentByEQPTID(eqID);
                if (eqpt != null)
                {
                    if (eqpt.EQPT_ID == "HID" || eqpt.EQPT_ID == "HID2" || eqpt.EQPT_ID == "HID3" || eqpt.EQPT_ID == "HID4")
                    {
                        HIDValueDefMapAction mapAction = (eqpt.getMapActionByIdentityKey("HIDValueDefMapAction") as HIDValueDefMapAction);
                        if (mapAction != null)
                        {
                            mapAction.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                    else if (eqpt.EQPT_ID == "HID5" || eqpt.EQPT_ID == "HID6" || eqpt.EQPT_ID == "HID7" || eqpt.EQPT_ID == "HID8" || eqpt.EQPT_ID == "HID9" || eqpt.EQPT_ID == "HID10")
                    {
                        HIDValueDefMapActionPH2 mapActionPh2 = (eqpt.getMapActionByIdentityKey("HIDValueDefMapActionPH2") as HIDValueDefMapActionPH2);
                        if (mapActionPh2 != null)
                        {
                            mapActionPh2.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                    else if (eqpt.EQPT_ID == "MTS" || eqpt.EQPT_ID == "MTS2")
                    {
                        MTSValueDefMapActionNew mapAction = (eqpt.getMapActionByIdentityKey("MTSValueDefMapActionNew") as MTSValueDefMapActionNew);
                        if (mapAction != null)
                        {
                            mapAction.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                    else if (eqpt.EQPT_ID == "MTL")
                    {
                        MTLValueDefMapActionNew mapAction = (eqpt.getMapActionByIdentityKey("MTLValueDefMapActionNew") as MTLValueDefMapActionNew);
                        if (mapAction != null)
                        {
                            mapAction.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                    else if (eqpt.EQPT_ID == "MTL2" || eqpt.EQPT_ID == "MTL3")
                    {
                        MTLValueDefMapActionNewPH2 mapAction = (eqpt.getMapActionByIdentityKey("MTLValueDefMapActionNewPH2") as MTLValueDefMapActionNewPH2);
                        if (mapAction != null)
                        {
                            mapAction.DateTimeSyncCommand(DateTime.Now);
                        }
                    }
                    else if (eqpt.EQPT_ID == "MTS3")
                    {
                        MTSValueDefMapActionNewPH2 mapAction = (eqpt.getMapActionByIdentityKey("MTSValueDefMapActionNewPH2") as MTSValueDefMapActionNewPH2);
                        if (mapAction != null)
                        {
                            mapAction.DateTimeSyncCommand(DateTime.Now);
                        }
                    }

                }

            }
        }
    }
}