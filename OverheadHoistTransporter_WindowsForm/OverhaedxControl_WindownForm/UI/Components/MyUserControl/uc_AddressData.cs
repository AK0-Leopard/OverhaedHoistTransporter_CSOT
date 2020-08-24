//*********************************************************************************
//      uc_AddressData.cs
//*********************************************************************************
// File Name: uc_AddressData.cs
// Description: the User Control of Address Data
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
    public partial class uc_AddressData : UserControl
    {
        const int LOCATION_SCALE = 10000;
        public uc_AddressData()
        {
            InitializeComponent();
        }
        IVehicleDataSetting dataSetting;
        List<AADDRESS_DATA> address_datas = null;
        public void start(IVehicleDataSetting _dataSetting)
        {
            dataSetting = _dataSetting;
            address_datas = dataSetting.loadAllReleaseAddress_Data();

            List<string> vh_ids = address_datas.
                                  Select(address_data => address_data.VEHOCLE_ID).
                                  Distinct().
                                  OrderBy(vh_id => vh_id).ToList();
            List<string> addresses = address_datas.
                      Select(address_data => address_data.ADR_ID).
                      Distinct().
                      OrderBy(adr_id => adr_id).ToList();

            cmbo_VehicleID_Value.DataSource = vh_ids;
            cmbo_AddressID_Value.DataSource = addresses;

            uc_bt_Save1.MyClick += Uc_bt_Save1_MyClick;

        }

        private async void Uc_bt_Save1_MyClick(object sender, EventArgs e)
        {
            string vh_id = cmbo_VehicleID_Value.SelectedItem as string;
            string adr_id = cmbo_AddressID_Value.SelectedItem as string;
            int resolution = (int)numic_Resolution_Value.Value;
            int location = (int)numic_Position_Value.Value * LOCATION_SCALE;
            bool isSuccess = false;
            await Task.Run(() => isSuccess = dataSetting.updateAddressData(vh_id, adr_id, resolution, location));
            AADDRESS_DATA address_data = address_datas.
                Where(data => data.VEHOCLE_ID.Trim() == vh_id && data.ADR_ID.Trim() == adr_id.Trim()).
                SingleOrDefault();
            if (isSuccess)
            {
                address_data.RESOLUTION = resolution;
                address_data.LOACTION = location;
            }
            else
            {
                numic_Resolution_Value.Value = address_data.RESOLUTION;
                double d_location = address_data.LOACTION / LOCATION_SCALE;
                numic_Position_Value.Value = (decimal)d_location;
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbo_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string vh_id = cmbo_VehicleID_Value.SelectedItem as string;
            string vh_id = com.mirle.ibg3k0.sc.BLL.DataSyncBLL.COMMON_ADDRESS_DATA_INDEX;
            string adr_id = cmbo_AddressID_Value.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(vh_id) || string.IsNullOrWhiteSpace(adr_id)) return;
            AADDRESS_DATA address_data = address_datas.
                Where(data => data.VEHOCLE_ID.Trim() == vh_id.Trim() && data.ADR_ID.Trim() == adr_id.Trim()).
                SingleOrDefault();
            numic_Resolution_Value.Value = address_data.RESOLUTION;
            double d_location = address_data.LOACTION / LOCATION_SCALE;
            numic_Position_Value.Value = (decimal)d_location;
        }
    }
}
