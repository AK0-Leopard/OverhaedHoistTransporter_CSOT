//*********************************************************************************
//      VehicleDataSettingForm.cs
//*********************************************************************************
// File Name: VehicleDataSettingForm.cs
// Description: Vehicle Data Setting Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data.DAO;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class VehicleDataSettingForm : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public VehicleDataSettingForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void SectionDataEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_Close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void VehicleDataSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                IVehicleDataSetting vehicleDataSetting = new VehicleDataSetting();

                uc_TravelBaseData1.start(vehicleDataSetting);
                uc_GuideData1.start(vehicleDataSetting);
                uc_AddressData1.start(vehicleDataSetting);
                uc_SectionData1.start(vehicleDataSetting);
                uc_ScaleData1.start(vehicleDataSetting);
                uc_ControlData1.start(vehicleDataSetting);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class VehicleDataSetting : sc.BLL.DataSyncBLL, IVehicleDataSetting
        {
            public ViewSectionDao VSection100Dao { get; private set; } = null;
            public AddressDataDao AddressDataDao { get; private set; } = null;
            public ScaleBaseDataDao ScaleBaseDataDao { get; private set; } = null;
            public ControlDataDao ControlDataDao { get; private set; } = null;
            public VehicleControlDao VehicleControlDao { get; private set; } = null;
            public SegmentDao SegmentDao { get; private set; } = null;

            public SectionDao SectionDao { get; private set; } = null;
            public ohxc.winform.Dao.MirleOHS_100.SectionControlDao sectionControlDao { get; private set; } = null;

            public VehicleDataSetting()
            {
                VSection100Dao = new ViewSectionDao();
                SegmentDao = new SegmentDao();
                AddressDataDao = new AddressDataDao();
                ScaleBaseDataDao = new ScaleBaseDataDao();
                ControlDataDao = new ControlDataDao();
                VehicleControlDao = new VehicleControlDao();

                SectionDao = new SectionDao();
                sectionControlDao = new ohxc.winform.Dao.MirleOHS_100.SectionControlDao();

                start(VSection100Dao, SegmentDao, AddressDataDao, ScaleBaseDataDao, ControlDataDao, VehicleControlDao);
            }

            #region Address Data

            public new List<AADDRESS_DATA> loadAllReleaseAddress_Data()
            {
                return base.loadAllReleaseAddress_Data();
            }
            public bool updateAddressData(string vh_id, string adr_id, int resolution, int location)
            {
                AADDRESS_DATA adr_data = null;
                try
                {
                    using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                    {
                        adr_data = AddressDataDao.getAddressData(con, vh_id, adr_id);
                        adr_data.RESOLUTION = resolution;
                        adr_data.LOACTION = location;
                        AddressDataDao.update(con, adr_data);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }

            }
            #endregion Address Data
            #region Section Data
            public new List<VSECTION_100> loadReleaseVSections()
            {
                return base.loadReleaseVSections();
            }
            public bool updateSectionData(ASECTION sec, ASECTION_CONTROL_100 sec_100)
            {
                try
                {
                    using (System.Transactions.TransactionScope tx = sc.Common.SCUtility.getTransactionScope())
                    {
                        using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                        {
                            updateSection(sec);
                            updateSection(sec_100);
                            tx.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }
                return true;
            }
            private void updateSection(ASECTION sec)
            {
                ASECTION secTemp = new ASECTION();
                using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                {
                    secTemp.SEC_ID = sec.SEC_ID;
                    secTemp.SUB_VER = "";
                    con.ASECTION.Attach(secTemp);
                    secTemp.DIRC_DRIV = sec.DIRC_DRIV;
                    secTemp.DIRC_GUID = sec.DIRC_GUID;
                    secTemp.CDOG_1 = sec.CDOG_1;
                    secTemp.CDOG_2 = sec.CDOG_2;
                    secTemp.CHG_SEG_NUM_1 = sec.CHG_SEG_NUM_1;
                    secTemp.CHG_SEG_NUM_2 = sec.CHG_SEG_NUM_2;
                    secTemp.SEC_DIS = sec.SEC_DIS;
                    secTemp.SEC_SPD = sec.SEC_SPD;
                    secTemp.PRE_BLO_REQ = sec.PRE_BLO_REQ;
                    secTemp.AREA_SECSOR = sec.AREA_SECSOR;
                    //secTemp.PRE_DIV = sec.PRE_DIV;

                    con.Entry(secTemp).Property(p => p.DIRC_DRIV).IsModified = true;
                    con.Entry(secTemp).Property(p => p.DIRC_GUID).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CDOG_1).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CDOG_2).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CHG_SEG_NUM_1).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CHG_SEG_NUM_2).IsModified = true;
                    con.Entry(secTemp).Property(p => p.SEC_DIS).IsModified = true;
                    con.Entry(secTemp).Property(p => p.SEC_SPD).IsModified = true;
                    con.Entry(secTemp).Property(p => p.PRE_BLO_REQ).IsModified = true;
                    con.Entry(secTemp).Property(p => p.AREA_SECSOR).IsModified = true;
                    //con.Entry(secTemp).Property(p => p.PRE_DIV).IsModified = true;

                    SectionDao.update(con, secTemp);
                    con.Entry(secTemp).State = System.Data.Entity.EntityState.Detached;
                }

            }
            private void updateSection(ASECTION_CONTROL_100 sec_100)
            {
                ASECTION_CONTROL_100 secTemp = new ASECTION_CONTROL_100();
                using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                {
                    secTemp.SEC_ID = sec_100.SEC_ID;
                    secTemp.SUB_VER = "";
                    con.ASECTION_CONTROL_100.Attach(secTemp);
                    secTemp.CHG_AREA_SECSOR_1 = sec_100.CHG_AREA_SECSOR_1;
                    secTemp.CHG_AREA_SECSOR_2 = sec_100.CHG_AREA_SECSOR_2;
                    secTemp.OBS_SENSOR_F = sec_100.OBS_SENSOR_F;
                    secTemp.OBS_SENSOR_R = sec_100.OBS_SENSOR_R;
                    secTemp.OBS_SENSOR_L = sec_100.OBS_SENSOR_L;
                    secTemp.RANGE_SENSOR_F = sec_100.RANGE_SENSOR_F;
                    secTemp.IS_ADR_RPT = sec_100.IS_ADR_RPT;
                    secTemp.CAN_GUIDE_CHG = sec_100.CAN_GUIDE_CHG;
                    secTemp.HID_CONTROL = sec_100.HID_CONTROL;
                    secTemp.BRANCH_FLAG = sec_100.BRANCH_FLAG;
                    con.Entry(secTemp).Property(p => p.CHG_AREA_SECSOR_1).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CHG_AREA_SECSOR_2).IsModified = true;
                    con.Entry(secTemp).Property(p => p.OBS_SENSOR_F).IsModified = true;
                    con.Entry(secTemp).Property(p => p.OBS_SENSOR_R).IsModified = true;
                    con.Entry(secTemp).Property(p => p.OBS_SENSOR_L).IsModified = true;
                    con.Entry(secTemp).Property(p => p.RANGE_SENSOR_F).IsModified = true;
                    con.Entry(secTemp).Property(p => p.IS_ADR_RPT).IsModified = true;
                    con.Entry(secTemp).Property(p => p.CAN_GUIDE_CHG).IsModified = true;
                    con.Entry(secTemp).Property(p => p.HID_CONTROL).IsModified = true;
                    con.Entry(secTemp).Property(p => p.BRANCH_FLAG).IsModified = true;

                    sectionControlDao.Update(con, secTemp);
                    con.Entry(secTemp).State = System.Data.Entity.EntityState.Detached;
                }
            }
            #endregion Section Data
            #region Segment Data
            public new List<ASEGMENT> loadAllSegments()
            {
                return base.loadAllSegments();
            }
            #endregion Segment Data
            #region Control Data
            public new CONTROL_DATA getReleaseCONTROL_DATA()
            {
                return base.getReleaseCONTROL_DATA();
            }
            public bool updateControlData(CONTROL_DATA new_control_data)
            {
                try
                {
                    using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                    {
                        con.CONTROL_DATA.Attach(new_control_data);
                        con.Entry(new_control_data).Property(p => p.T1).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T2).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T3).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T4).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T5).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T6).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T7).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.T8).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.BLOCK_REQ_TIME_OUT).IsModified = true;
                        ControlDataDao.update(con, new_control_data);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }
            }

            #endregion Control Data
            #region Guide Data
            public List<AVEHICLE_CONTROL_100> loadAllReleaseVehicleControlData()
            {
                return base.loadAllReleaseVehicleControlData_100();
            }
            public bool updateGuideData(AVEHICLE_CONTROL_100 new_control_data)
            {
                try
                {
                    using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                    {
                        con.AVEHICLE_CONTROL_100.Attach(new_control_data);
                        con.Entry(new_control_data).Property(p => p.GUIDE_START_STOP_SPEED).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_MAX_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_ACCEL_DECCEL_TIME).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_S_CURVE_RATE).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_RUN_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_MANUAL_HIGH_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_MANUAL_LOW_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_LF_LOCK_POSITION).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_LB_LOCK_POSITION).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_RF_LOCK_POSITION).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_RB_LOCK_POSITION).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.GUIDE_CHG_STABLE_TIME).IsModified = true;
                        VehicleControlDao.update(con, new_control_data);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }

            }
            #endregion Guide Data
            #region TravelBase Data
            public bool updateTravelBaseData(AVEHICLE_CONTROL_100 new_control_data)
            {
                try
                {
                    using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                    {
                        con.AVEHICLE_CONTROL_100.Attach(new_control_data);
                        con.Entry(new_control_data).Property(p => p.TRAVEL_RESOLUTION).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_START_STOP_SPEED).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_MAX_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_ACCEL_DECCEL_TIME).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_HOME_DIR).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_HOME_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_KEEP_DIS_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_OBS_DETECT_LONG).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_OBS_DETECT_SHORT).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_MANUAL_HIGH_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_MANUAL_LOW_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_TEACHING_SPD).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_TRAVEL_DIR).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_F_DIR_LIMIT).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_R_DIR_LIMIT).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_S_CURVE_RATE).IsModified = true;
                        con.Entry(new_control_data).Property(p => p.TRAVEL_ENCODER_POLARITY).IsModified = true;
                        VehicleControlDao.update(con, new_control_data);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }
            }
            #endregion TravelBase Data
            #region Scale Data
            public new SCALE_BASE_DATA getReleaseSCALE_BASE_DATA()
            {
                return base.getReleaseSCALE_BASE_DATA();
            }
            public bool updateScaleBaseData(SCALE_BASE_DATA new_scale_base_data)
            {
                try
                {
                    using (sc.Data.DBConnection_EF con = sc.Data.DBConnection_EF.GetUContext())
                    {
                        con.SCALE_BASE_DATA.Attach(new_scale_base_data);
                        con.Entry(new_scale_base_data).Property(p => p.RESOLUTION).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.INPOSITION_AREA).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.INPOSITION_STABLE_TIME).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.TOTAL_SCALE_PULSE).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.SCALE_OFFSET).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.SCALE_RESE_DIST).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.READ_DIR).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.SUB_VER).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.ADD_TIME).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.ADD_USER).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.UPD_TIME).IsModified = true;
                        con.Entry(new_scale_base_data).Property(p => p.UPD_USER).IsModified = true;
                        ScaleBaseDataDao.update(con, new_scale_base_data);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
                    return false;
                }

            }
            #endregion Scale Data
        }

        private void uc_TravelBaseData1_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }

    public interface IVehicleDataSetting
    {
        #region Address Data
        List<AADDRESS_DATA> loadAllReleaseAddress_Data();
        bool updateAddressData(string vh_id, string adr_id, int resolution, int location);
        #endregion Address Data

        #region Section Data
        List<VSECTION_100> loadReleaseVSections();
        bool updateSectionData(ASECTION sec, ASECTION_CONTROL_100 sec_100);
        #endregion Section Data

        #region Segment Data
        List<ASEGMENT> loadAllSegments();
        #endregion Segment Data

        #region Control Data
        CONTROL_DATA getReleaseCONTROL_DATA();
        bool updateControlData(CONTROL_DATA new_control_data);
        #endregion Control Data

        #region Guide Data
        List<AVEHICLE_CONTROL_100> loadAllReleaseVehicleControlData();
        bool updateGuideData(AVEHICLE_CONTROL_100 new_control_data);
        #endregion Guide Data

        #region TravelBase Data
        bool updateTravelBaseData(AVEHICLE_CONTROL_100 new_control_data);
        #endregion TravelBase Data

        #region Scale Data
        SCALE_BASE_DATA getReleaseSCALE_BASE_DATA();
        bool updateScaleBaseData(SCALE_BASE_DATA new_control_data);
        #endregion Scale Data
    }
}
