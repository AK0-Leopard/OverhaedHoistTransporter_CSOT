//*********************************************************************************
//      uc_ControlData.cs
//*********************************************************************************
// File Name: uc_ControlData.cs
// Description: the User Control of Control Data
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author              Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2018/08/14       Boan                N/A                  N/A                  Initialize.
//**********************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.sc;

namespace com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl
{
    public partial class uc_ControlData : UserControl
    {

        const int TIMEOUT_SCALE = 10;
        public uc_ControlData()
        {
            InitializeComponent();
        }
        IVehicleDataSetting dataSetting;
        CONTROL_DATA Control_Data = null;

        public void start(IVehicleDataSetting _dataSetting)
        {
            dataSetting = _dataSetting;
            Control_Data = dataSetting.getReleaseCONTROL_DATA();

            setValue2UI();

            uc_bt_Save1.MyClick += Uc_bt_Save1_MyClick;
        }

        private async void Uc_bt_Save1_MyClick(object sender, EventArgs e)
        {
            int t1 = (int)(numic_TimeoutOfT1_Value.Value * TIMEOUT_SCALE);
            int t2 = (int)(numic_TimeoutOfT2_Value.Value * TIMEOUT_SCALE);
            int t3 = (int)(numic_TimeoutOfT3_Value.Value * TIMEOUT_SCALE);
            int t4 = (int)(numic_TimeoutOfT4_Value.Value * TIMEOUT_SCALE);
            int t5 = (int)(numic_TimeoutOfT5_Value.Value * TIMEOUT_SCALE);
            int t6 = (int)(numic_TimeoutOfT6_Value.Value * TIMEOUT_SCALE);
            int t7 = (int)(numic_TimeoutOfT7_Value.Value * TIMEOUT_SCALE);
            int t8 = (int)(numic_TimeoutOfT8_Value.Value * TIMEOUT_SCALE);
            int block_time = (int)(numic_TimeoutOfBlockingResponse_Value.Value * TIMEOUT_SCALE);


            CONTROL_DATA new_control_cata = new CONTROL_DATA()
            {
                T1 = t1,
                T2 = t2,
                T3 = t3,
                T4 = t4,
                T5 = t5,
                T6 = t6,
                T7 = t7,
                T8 = t8,
                BLOCK_REQ_TIME_OUT = block_time,
              
                SUB_VER = Control_Data.SUB_VER
            };
            bool isSuccess = false;
            await Task.Run(() => isSuccess = dataSetting.updateControlData(new_control_cata));
            if(isSuccess)
            {
                Control_Data = new_control_cata;
            }
            else
            {
                setValue2UI();
            }
        }

        private void setValue2UI()
        {
            double t1 = (double)Control_Data.T1 / TIMEOUT_SCALE;
            double t2 = (double)Control_Data.T2 / TIMEOUT_SCALE;
            double t3 = (double)Control_Data.T3 / TIMEOUT_SCALE;
            double t4 = (double)Control_Data.T4 / TIMEOUT_SCALE;
            double t5 = (double)Control_Data.T5 / TIMEOUT_SCALE;
            double t6 = (double)Control_Data.T6 / TIMEOUT_SCALE;
            double t7 = (double)Control_Data.T7 / TIMEOUT_SCALE;
            double t8 = (double)Control_Data.T8 / TIMEOUT_SCALE;
            double block_time = (double)Control_Data.BLOCK_REQ_TIME_OUT / TIMEOUT_SCALE;

            numic_TimeoutOfT1_Value.Value = (decimal)t1;
            numic_TimeoutOfT2_Value.Value = (decimal)t2;
            numic_TimeoutOfT3_Value.Value = (decimal)t3;
            numic_TimeoutOfT4_Value.Value = (decimal)t4;
            numic_TimeoutOfT5_Value.Value = (decimal)t5;
            numic_TimeoutOfT6_Value.Value = (decimal)t6;
            numic_TimeoutOfT7_Value.Value = (decimal)t7;
            numic_TimeoutOfT8_Value.Value = (decimal)t8;
            numic_TimeoutOfBlockingResponse_Value.Value = (decimal)block_time;
        }
    }
}
