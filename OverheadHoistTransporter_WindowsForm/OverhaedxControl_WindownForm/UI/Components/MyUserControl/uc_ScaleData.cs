//*********************************************************************************
//      uc_ScaleData.cs
//*********************************************************************************
// File Name: uc_ScaleData.cs
// Description: the User Control of Scale Data
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
    public partial class uc_ScaleData : UserControl
    {
        const int SCALE_10 = 10;
        const int SCALE_100 = 100;
        const int SCALE_1000 = 1000;
        const int SCALE_10000 = 10000;

        IVehicleDataSetting dataSetting;
        sc.SCALE_BASE_DATA scale_base_data = null;
        public uc_ScaleData()
        {
            InitializeComponent();
        }
        public void start(IVehicleDataSetting _dataSetting)
        {
            dataSetting = _dataSetting;
            scale_base_data = dataSetting.getReleaseSCALE_BASE_DATA();

            cmbo_dirc_of_read.DataSource = Enum.GetValues(typeof(E_DIRC_DRIV)).Cast<E_DIRC_DRIV>();

            uc_bt_Save1.MyClick += Uc_bt_Save1_MyClick;


            set2UI(scale_base_data);

        }

        private async void Uc_bt_Save1_MyClick(object sender, EventArgs e)
        {
            int resolution = (int)(numic_Resolution_Value.Value * SCALE_100);
            int inposition_area = (int)(numic_InpositionArea_Value.Value * SCALE_10000);
            int Inposition_Stability_Time = (int)(numic_InpositionStabilityTime_Value.Value * SCALE_10);
            int Total_Pulse_Of_One_Scale = (int)(numic_TotalPulseOfOneScale_Value.Value);
            int scale_offset = (int)(numic_ScaleOffset_Value.Value * SCALE_10000);
            int Scale_Reset_Distance = (int)(numic_ScaleResetDistance_Value.Value * SCALE_10000);
            E_DIRC_DRIV Direction_of_Read = (E_DIRC_DRIV)cmbo_dirc_of_read.SelectedItem;

            sc.SCALE_BASE_DATA new_scale_base_data = new SCALE_BASE_DATA();
            new_scale_base_data.RESOLUTION = resolution;
            new_scale_base_data.INPOSITION_AREA = inposition_area;
            new_scale_base_data.INPOSITION_STABLE_TIME = Inposition_Stability_Time;
            new_scale_base_data.TOTAL_SCALE_PULSE = Total_Pulse_Of_One_Scale;
            new_scale_base_data.SCALE_OFFSET = scale_offset;
            new_scale_base_data.SCALE_RESE_DIST = Scale_Reset_Distance;
            new_scale_base_data.READ_DIR = Direction_of_Read;
            new_scale_base_data.SUB_VER = scale_base_data.SUB_VER;
            new_scale_base_data.ADD_TIME = scale_base_data.ADD_TIME;
            new_scale_base_data.ADD_USER = scale_base_data.ADD_USER;
            new_scale_base_data.UPD_TIME = scale_base_data.UPD_TIME;
            new_scale_base_data.UPD_USER = scale_base_data.UPD_USER;
            bool isSuccess = false;

            await Task.Run(() => isSuccess = dataSetting.updateScaleBaseData(new_scale_base_data));
            if (isSuccess)
            {
                scale_base_data.RESOLUTION = resolution;
                scale_base_data.INPOSITION_AREA = inposition_area;
                scale_base_data.INPOSITION_STABLE_TIME = Inposition_Stability_Time;
                scale_base_data.TOTAL_SCALE_PULSE = Total_Pulse_Of_One_Scale;
                scale_base_data.SCALE_OFFSET = scale_offset;
                scale_base_data.SCALE_RESE_DIST = Scale_Reset_Distance;
                scale_base_data.READ_DIR = Direction_of_Read;
            }
            else
            {
                set2UI(scale_base_data);
            }
        }
        public void set2UI(sc.SCALE_BASE_DATA scale_base_data)
        {

            int resolution = (int)scale_base_data.RESOLUTION;
            int inposition_area = (int)scale_base_data.INPOSITION_AREA;
            int Inposition_Stability_Time = (int)scale_base_data.INPOSITION_STABLE_TIME;
            int Total_Pulse_Of_One_Scale = (int)scale_base_data.TOTAL_SCALE_PULSE;
            int scale_offset = (int)scale_base_data.SCALE_OFFSET;
            int Scale_Reset_Distance = (int)scale_base_data.SCALE_RESE_DIST;
            E_DIRC_DRIV Direction_of_Read = scale_base_data.READ_DIR;

            numic_Resolution_Value.Value = (decimal)resolution / SCALE_100;
            numic_InpositionArea_Value.Value = (decimal)inposition_area / SCALE_10000;
            numic_InpositionStabilityTime_Value.Value = (decimal)Inposition_Stability_Time / SCALE_10;
            numic_TotalPulseOfOneScale_Value.Value = (decimal)Total_Pulse_Of_One_Scale;
            numic_ScaleOffset_Value.Value = (decimal)scale_offset / SCALE_10000;
            numic_ScaleResetDistance_Value.Value = (decimal)Scale_Reset_Distance / SCALE_10000;
            cmbo_dirc_of_read.SelectedItem = Direction_of_Read;
        }

    }
}
