//*********************************************************************************
//      uc_TravelBaseData.cs
//*********************************************************************************
// File Name: uc_TravelBaseData.cs
// Description: the User Control of Travel Base Data
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
    public partial class uc_TravelBaseData : UserControl
    {

        const int SCALE_10 = 10;
        const int SCALE_100 = 100;
        const int SCALE_1000 = 1000;
        const int SCALE_10000 = 10000;
        List<sc.AVEHICLE_CONTROL_100> vehicle_contorls = null;

        IVehicleDataSetting dataSetting;
        public uc_TravelBaseData()
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

            cmbo_DirectOfHome_Value.DataSource = Enum.GetValues(typeof(E_DIRC_DRIV)).Cast<E_DIRC_DRIV>();
            cmbo_TravelDirection_Value.DataSource = Enum.GetValues(typeof(E_DIRC_DRIV)).Cast<E_DIRC_DRIV>();
            cmbo_PoleOfEncoder_Value.DataSource = Enum.GetValues(typeof(E_ENCODER_POLE)).Cast<E_ENCODER_POLE>();

        }

        private async void Uc_bt_Save1_MyClick(object sender, EventArgs e)
        {
            var vh_data = cmbo_VehicleID_Value.SelectedItem as sc.AVEHICLE_CONTROL_100;
            var new_vh_data = new sc.AVEHICLE_CONTROL_100();

            int resolution = (int)(numic_Resolution_Value.Value * SCALE_1000);
            int start_stop_speed = (int)(numic_StartStopSpeed_Value.Value * SCALE_100);
            int max_speed = (int)(numic_MaxSpeed_Value.Value * SCALE_100);
            int accel_deccel_time = (int)(numic_AccDeccTime_Value.Value * SCALE_1000);
            E_DIRC_DRIV dire_of_home = (E_DIRC_DRIV)cmbo_DirectOfHome_Value.SelectedItem;
            int home_speed = (int)(numic_HomeSpeed_Value.Value * SCALE_100);
            int keep_distance_speed = (int)(numic_KeepDistanceSpeed_Value.Value * SCALE_100);
            int keep_distance_far_speed = (int)(numic_KeepDistanceFar_Value.Value);
            int keep_distance_near_speed = (int)(numic_KeepDistanceNear_Value.Value);
            int maunual_high_speed = (int)(numic_ManualHighSpeed_Value.Value * SCALE_100);
            int maunual_low_speed = (int)(numic_ManualLowSpeed_Value.Value * SCALE_100);
            int teaching_speed = (int)(numic_TeachingSpeed_Value.Value * SCALE_100);
            E_DIRC_DRIV travel_dirc = (E_DIRC_DRIV)cmbo_TravelDirection_Value.SelectedItem;
            int front_limit = (int)(numic_FrontLimit_Value.Value);
            int rear_limit = (int)(numic_FrontLimit_Value.Value);
            int s_curve_rate = (int)(numic_SCurveRate_Value.Value);
            E_ENCODER_POLE pole_encoder = (E_ENCODER_POLE)cmbo_PoleOfEncoder_Value.SelectedItem;
            int position_compensation = (int)(numic_position_compensation.Value * SCALE_10000);

            new_vh_data.VEHICLE_ID = vh_data.VEHICLE_ID;
            new_vh_data.TRAVEL_RESOLUTION = resolution;
            new_vh_data.TRAVEL_START_STOP_SPEED = start_stop_speed;
            new_vh_data.TRAVEL_MAX_SPD = max_speed;
            new_vh_data.TRAVEL_ACCEL_DECCEL_TIME = accel_deccel_time;
            new_vh_data.TRAVEL_HOME_DIR = dire_of_home;
            new_vh_data.TRAVEL_HOME_SPD = home_speed;
            new_vh_data.TRAVEL_KEEP_DIS_SPD = keep_distance_speed;
            new_vh_data.TRAVEL_OBS_DETECT_LONG = keep_distance_far_speed;
            new_vh_data.TRAVEL_OBS_DETECT_SHORT = keep_distance_near_speed;
            new_vh_data.TRAVEL_MANUAL_HIGH_SPD = maunual_high_speed;
            new_vh_data.TRAVEL_MANUAL_LOW_SPD = maunual_low_speed;
            new_vh_data.TRAVEL_TEACHING_SPD = teaching_speed;
            new_vh_data.TRAVEL_TRAVEL_DIR = travel_dirc;
            new_vh_data.TRAVEL_TRAVEL_DIR = travel_dirc;
            new_vh_data.TRAVEL_F_DIR_LIMIT = position_compensation; //先用F_DIR_LIMIT 作為position_compensation的欄位
            //new_vh_data.TRAVEL_R_DIR_LIMIT = rear_limit * SCALE_100;
            new_vh_data.TRAVEL_S_CURVE_RATE = s_curve_rate;
            new_vh_data.TRAVEL_ENCODER_POLARITY = pole_encoder;
            new_vh_data.SUB_VER = "";

            bool isSuccess = false;
            await Task.Run(() => isSuccess = dataSetting.updateTravelBaseData(new_vh_data));
            if (isSuccess)
            {
                vh_data.TRAVEL_RESOLUTION = resolution;
                vh_data.TRAVEL_START_STOP_SPEED = start_stop_speed;
                vh_data.TRAVEL_MAX_SPD = max_speed;
                vh_data.TRAVEL_ACCEL_DECCEL_TIME = accel_deccel_time;
                vh_data.TRAVEL_HOME_DIR = dire_of_home;
                vh_data.TRAVEL_HOME_SPD = home_speed;
                vh_data.TRAVEL_KEEP_DIS_SPD = keep_distance_speed;
                vh_data.TRAVEL_OBS_DETECT_LONG = keep_distance_far_speed;
                vh_data.TRAVEL_OBS_DETECT_SHORT = keep_distance_near_speed;
                vh_data.TRAVEL_MANUAL_HIGH_SPD = maunual_high_speed;
                vh_data.TRAVEL_MANUAL_LOW_SPD = maunual_low_speed;
                vh_data.TRAVEL_TEACHING_SPD = teaching_speed;
                vh_data.TRAVEL_TRAVEL_DIR = travel_dirc;
                //vh_data.TRAVEL_F_DIR_LIMIT = front_limit;
                vh_data.TRAVEL_R_DIR_LIMIT = rear_limit;
                vh_data.TRAVEL_S_CURVE_RATE = s_curve_rate;
                vh_data.TRAVEL_ENCODER_POLARITY = pole_encoder;
                vh_data.TRAVEL_F_DIR_LIMIT = position_compensation;
            }
            else
            {
                set2UI(vh_data);
            }
        }

        private void set2UI(sc.AVEHICLE_CONTROL_100 vh_data)
        {

            double resolution = (double)vh_data.TRAVEL_RESOLUTION / SCALE_1000;
            double start_stop_speed = (double)vh_data.TRAVEL_START_STOP_SPEED / SCALE_100;
            double max_speed = (double)vh_data.TRAVEL_MAX_SPD / SCALE_100;
            double accel_deccel_time = (double)vh_data.TRAVEL_ACCEL_DECCEL_TIME / SCALE_1000;
            E_DIRC_DRIV dire_of_home = vh_data.TRAVEL_HOME_DIR;
            double home_speed = (double)vh_data.TRAVEL_HOME_SPD / SCALE_100;
            double keep_distance_speed = (double)vh_data.TRAVEL_KEEP_DIS_SPD / SCALE_100;
            double keep_distance_far_speed = (double)vh_data.TRAVEL_OBS_DETECT_LONG;
            double keep_distance_near_speed = (double)vh_data.TRAVEL_OBS_DETECT_SHORT;
            double maunual_high_speed = (double)vh_data.TRAVEL_MANUAL_HIGH_SPD / SCALE_100;
            double maunual_low_speed = (double)vh_data.TRAVEL_MANUAL_LOW_SPD / SCALE_100;
            double teaching_speed = (double)vh_data.TRAVEL_TEACHING_SPD / SCALE_100;
            E_DIRC_DRIV travel_dirc = vh_data.TRAVEL_TRAVEL_DIR;
            double front_limit = (double)vh_data.TRAVEL_F_DIR_LIMIT;
            double rear_limit = (double)vh_data.TRAVEL_R_DIR_LIMIT;
            double s_curve_rate = (double)vh_data.TRAVEL_S_CURVE_RATE;
            E_ENCODER_POLE pole_encoder = vh_data.TRAVEL_ENCODER_POLARITY;
            double position_compensation = (double)vh_data.TRAVEL_F_DIR_LIMIT / SCALE_10000;

            numic_Resolution_Value.Value = (decimal)resolution;
            numic_StartStopSpeed_Value.Value = (decimal)start_stop_speed;
            numic_MaxSpeed_Value.Value = (decimal)max_speed;
            numic_AccDeccTime_Value.Value = (decimal)accel_deccel_time;
            cmbo_DirectOfHome_Value.SelectedItem = dire_of_home;
            numic_HomeSpeed_Value.Value = (decimal)home_speed;
            numic_KeepDistanceSpeed_Value.Value = (decimal)keep_distance_speed;
            numic_KeepDistanceFar_Value.Value = (decimal)keep_distance_far_speed;
            numic_KeepDistanceNear_Value.Value = (decimal)keep_distance_near_speed;
            numic_ManualHighSpeed_Value.Value = (decimal)maunual_high_speed;
            numic_ManualLowSpeed_Value.Value = (decimal)maunual_low_speed;
            numic_TeachingSpeed_Value.Value = (decimal)teaching_speed;
            cmbo_TravelDirection_Value.SelectedItem = travel_dirc;
            numic_FrontLimit_Value.Value = (decimal)front_limit;
            numic_RearLimit_Value.Value = (decimal)rear_limit;
            numic_SCurveRate_Value.Value = (decimal)s_curve_rate;
            cmbo_PoleOfEncoder_Value.SelectedItem = pole_encoder;
            numic_position_compensation.Value = (decimal)position_compensation;
            //int resolution = (int)numic_Resolution_Value.Value;
            //int start_stop_speed = (int)numic_StartStopSpeed_Value.Value;
            //int max_speed = (int)numic_MaxSpeed_Value.Value;
            //int accel_deccel_time = (int)numic_AccDeccTime_Value.Value;
            //E_DIRC_DRIV dire_of_home = (E_DIRC_DRIV)cmbo_DirectOfHome_Value.SelectedItem;
            //int home_speed = (int)numic_HomeSpeed_Value.Value;
            //int keep_distance_speed = (int)numic_KeepDistanceSpeed_Value.Value;
            //int keep_distance_far_speed = (int)numic_KeepDistanceFar_Value.Value;
            //int keep_distance_near_speed = (int)numic_KeepDistanceNear_Value.Value;
            //int maunual_high_speed = (int)numic_ManualHighSpeed_Value.Value;
            //int maunual_low_speed = (int)numic_ManualLowSpeed_Value.Value;
            //int teaching_speed = (int)numic_TeachingSpeed_Value.Value;
            //E_DIRC_DRIV travel_dirc = (E_DIRC_DRIV)cmbo_TravelDirection_Value.SelectedItem;
            //int front_limit = (int)numic_FrontLimit_Value.Value;
            //int rear_limit = (int)numic_FrontLimit_Value.Value;
            //int s_curve_rate = (int)numic_SCurveRate_Value.Value;
            //E_ENCODER_POLE pole_encoder = (E_ENCODER_POLE)cmbo_PoleOfEncoder_Value.SelectedItem;

            //numic_StartStopSpeed_Value.Value = start_stop_speed / SCALE_100;
            //numic_MaxSpeed_Value.Value = max_speed / SCALE_100;
            //numic_AccDeccTime_Value.Value = accel_deccel_time / SCALE_1000;
            //numic_SCurveRate_Value.Value = s_curve_rate;
            //numic_RunningSpeed_Value.Value = run_speed / SCALE_100;
            //numic_ManualHighSpeed_Value.Value = manual_high_speed / SCALE_10000;
            //numic_ManualLowSpeed_Value.Value = manual_low_speed / SCALE_10000;
            //numic_FrontLeftLoclPosition_Value.Value = lf_lock_position / SCALE_10000;
            //numic_RearLeftLoclPosition_Value.Value = lb_lock_position / SCALE_10000;
            //numic_FrontRightLoclPosition_Value.Value = rf_lock_position / SCALE_10000;
            //numic_RearRightLoclPosition_Value.Value = rb_lock_position / SCALE_10000;
            //numic_ChangeStabilityTime_Value.Value = chang_stable_time / SCALE_10;
        }

        private void cmbo_VehicleID_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vh_data = cmbo_VehicleID_Value.SelectedItem as sc.AVEHICLE_CONTROL_100;

            set2UI(vh_data);
        }
    }
}
