//*********************************************************************************
//      uc_GuideData.cs
//*********************************************************************************
// File Name: uc_GuideData.cs
// Description: the User Control of Guide Data
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

namespace com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl
{
    public partial class uc_GuideData : UserControl
    {
        const int SCALE_10 = 10;
        const int SCALE_100 = 100;
        const int SCALE_1000 = 1000;
        const int SCALE_10000 = 10000;
        IVehicleDataSetting dataSetting;
        List<sc.AVEHICLE_CONTROL_100> vehicle_contorls = null;
        public uc_GuideData()
        {
            InitializeComponent();
        }
        public void start(IVehicleDataSetting _dataSetting)
        {
            dataSetting = _dataSetting;
            vehicle_contorls = dataSetting.loadAllReleaseVehicleControlData();
            cmbo_VehicleID_Value.DataSource = vehicle_contorls;
            cmbo_VehicleID_Value.DisplayMember = "VEHICLE_ID";

            uc_bt_Save1.MyClick += Uc_bt_Save1_MyClick;
        }

        private async void Uc_bt_Save1_MyClick(object sender, EventArgs e)
        {
            var vh_data = cmbo_VehicleID_Value.SelectedItem as sc.AVEHICLE_CONTROL_100;
            var new_vh_data = new sc.AVEHICLE_CONTROL_100();

            int start_stop_speed = (int)(numic_StartStopSpeed_Value.Value * SCALE_100);
            int max_speed = (int)(numic_MaxSpeed_Value.Value * SCALE_100);
            int accel_deccel_time = (int)(numic_AccDeccTime_Value.Value * SCALE_1000);
            int s_curve_rate = (int)(numic_SCurveRate_Value.Value);
            int run_speed = (int)(numic_RunningSpeed_Value.Value * SCALE_100);
            int manual_high_speed = (int)(numic_ManualHighSpeed_Value.Value * SCALE_100);
            int manual_low_speed = (int)(numic_ManualLowSpeed_Value.Value * SCALE_100);
            int lf_lock_position = (int)(numic_FrontLeftLoclPosition_Value.Value * SCALE_10000);
            int lb_lock_position = (int)(numic_RearLeftLoclPosition_Value.Value * SCALE_10000);
            int rf_lock_position = (int)(numic_FrontRightLoclPosition_Value.Value * SCALE_10000);
            int rb_lock_position = (int)(numic_RearRightLoclPosition_Value.Value * SCALE_10000);
            int chang_stable_time = (int)(numic_ChangeStabilityTime_Value.Value * SCALE_10);

            new_vh_data.VEHICLE_ID = vh_data.VEHICLE_ID;
            new_vh_data.GUIDE_START_STOP_SPEED = start_stop_speed ;
            new_vh_data.GUIDE_MAX_SPD = max_speed ;
            new_vh_data.GUIDE_ACCEL_DECCEL_TIME = accel_deccel_time ;
            new_vh_data.GUIDE_S_CURVE_RATE = s_curve_rate ;
            new_vh_data.GUIDE_RUN_SPD = run_speed ;
            new_vh_data.GUIDE_MANUAL_HIGH_SPD = manual_high_speed ;
            new_vh_data.GUIDE_MANUAL_LOW_SPD = manual_low_speed ;
            new_vh_data.GUIDE_LF_LOCK_POSITION = lf_lock_position ;
            new_vh_data.GUIDE_LB_LOCK_POSITION = lb_lock_position ;
            new_vh_data.GUIDE_RF_LOCK_POSITION = rf_lock_position ;
            new_vh_data.GUIDE_RB_LOCK_POSITION = rb_lock_position ;
            new_vh_data.GUIDE_CHG_STABLE_TIME = chang_stable_time ;
            new_vh_data.SUB_VER = "" ;

            bool isSuccess = false;
            await Task.Run(() => isSuccess = dataSetting.updateGuideData(new_vh_data));
            if (isSuccess)
            {
                vh_data.GUIDE_START_STOP_SPEED = start_stop_speed;
                vh_data.GUIDE_MAX_SPD = max_speed ;
                vh_data.GUIDE_ACCEL_DECCEL_TIME = accel_deccel_time;
                vh_data.GUIDE_S_CURVE_RATE = s_curve_rate ;
                vh_data.GUIDE_RUN_SPD = run_speed;
                vh_data.GUIDE_MANUAL_HIGH_SPD = manual_high_speed ;
                vh_data.GUIDE_MANUAL_LOW_SPD = manual_low_speed ;
                vh_data.GUIDE_LF_LOCK_POSITION = lf_lock_position;
                vh_data.GUIDE_LB_LOCK_POSITION = lb_lock_position ;
                vh_data.GUIDE_RF_LOCK_POSITION = rf_lock_position ;
                vh_data.GUIDE_RB_LOCK_POSITION = rb_lock_position ;
                vh_data.GUIDE_CHG_STABLE_TIME = chang_stable_time;
            }
            else
            {
                set2UI(vh_data);
            }
        }

        private void cmbo_VehicleID_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vh_data = cmbo_VehicleID_Value.SelectedItem as sc.AVEHICLE_CONTROL_100;

            set2UI(vh_data);
        }

        private void set2UI(sc.AVEHICLE_CONTROL_100 vh_data)
        {
            double start_stop_speed = (double) vh_data.GUIDE_START_STOP_SPEED / SCALE_100;
            double max_speed = (double)vh_data.GUIDE_MAX_SPD / SCALE_100;
            double accel_deccel_time = (double)vh_data.GUIDE_ACCEL_DECCEL_TIME/ SCALE_1000;
            double s_curve_rate = vh_data.GUIDE_S_CURVE_RATE;
            double run_speed = (double)vh_data.GUIDE_RUN_SPD / SCALE_100;
            double manual_high_speed = (double)vh_data.GUIDE_MANUAL_HIGH_SPD / SCALE_100;
            double manual_low_speed = (double)vh_data.GUIDE_MANUAL_LOW_SPD / SCALE_100;
            double lf_lock_position = (double)vh_data.GUIDE_LF_LOCK_POSITION / SCALE_10000;
            double lb_lock_position = (double)vh_data.GUIDE_LB_LOCK_POSITION / SCALE_10000;
            double rf_lock_position = (double)vh_data.GUIDE_RF_LOCK_POSITION / SCALE_10000;
            double rb_lock_position = (double)vh_data.GUIDE_RB_LOCK_POSITION / SCALE_10000;
            double chang_stable_time = (double)vh_data.GUIDE_CHG_STABLE_TIME / SCALE_10;

            numic_StartStopSpeed_Value.Value =(decimal) start_stop_speed ;
            numic_MaxSpeed_Value.Value = (decimal)max_speed ;
            numic_AccDeccTime_Value.Value = (decimal)accel_deccel_time ;
            numic_SCurveRate_Value.Value = (decimal)s_curve_rate;
            numic_RunningSpeed_Value.Value = (decimal)run_speed;
            numic_ManualHighSpeed_Value.Value = (decimal)manual_high_speed ;
            numic_ManualLowSpeed_Value.Value = (decimal)manual_low_speed ;
            numic_FrontLeftLoclPosition_Value.Value = (decimal)lf_lock_position;
            numic_RearLeftLoclPosition_Value.Value = (decimal)lb_lock_position ;
            numic_FrontRightLoclPosition_Value.Value = (decimal)rf_lock_position;
            numic_RearRightLoclPosition_Value.Value = (decimal)rb_lock_position ;
            numic_ChangeStabilityTime_Value.Value = (decimal)chang_stable_time;
        }
    }
}
