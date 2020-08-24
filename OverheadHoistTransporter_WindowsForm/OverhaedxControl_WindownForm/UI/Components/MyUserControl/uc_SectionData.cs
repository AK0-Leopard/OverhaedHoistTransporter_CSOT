//*********************************************************************************
//      uc_SectionData.cs
//*********************************************************************************
// File Name: uc_SectionData.cs
// Description: the User Control of Section Data
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author              Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2018/08/14       Boan                N/A                  N/A                  Initialize.
//**********************************************************************************
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl
{
    public partial class uc_SectionData : UserControl
    {
        IVehicleDataSetting dataSetting;
        BindingSource bindingSource = new BindingSource();

        List<VSection_100ObjToShow> vsecs = null;
        public uc_SectionData()
        {
            InitializeComponent();


            vsecs = new List<VSection_100ObjToShow>();
        }



        private void uc_SectionData_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            //var secs = DataSyncBLL.loadReleaseVSections();
            //foreach (VSECTION_100 sec in secs)
            //{
            //    vsecs.Add(new VSection_100ObjToShow(sec));
            //}
            //dataGridView1.DataSource = vsecs;
        }

        public void start(IVehicleDataSetting _dataSetting)
        {
            dataSetting = _dataSetting;

            var secs = dataSetting.loadReleaseVSections();
            var segments = dataSetting.loadAllSegments();
            List<string> segments_num = new List<string>() { "0" };
            segments_num.AddRange(segments.Select(seg => seg.SEG_NUM.Trim()).ToList());
            setDataGridView(secs);
            cmbo_SectionID_Value.DataSource = vsecs;
            cmbo_SectionID_Value.DisplayMember = "SEC_ID";

            cmbo_SegmentNo_Value.DataSource = segments_num;
            cmbo_SegmentNumber1_Value.DataSource = segments_num.ToList();
            cmbo_SegmentNumber2_Value.DataSource = segments_num.ToList();



            cmbo_DriveDir_Value.DataSource = Enum.GetValues(typeof(E_DIRC_DRIV)).Cast<E_DIRC_DRIV>();
            cmbo_GuideDir_Value.DataSource = Enum.GetValues(typeof(E_DIRC_GUID)).Cast<E_DIRC_GUID>();
            cmbo_DirectionOfGuide1_Value.DataSource = Enum.GetValues(typeof(E_DIRC_GUID)).Cast<E_DIRC_GUID>();
            cmbo_DirectionOfGuide2_Value.DataSource = Enum.GetValues(typeof(E_DIRC_GUID)).Cast<E_DIRC_GUID>();


            cmbo_AreaSensor_Value.DataSource = Enum.GetValues(typeof(E_AreaSensorDir)).Cast<E_AreaSensorDir>();
            cmbo_AreaSensor1_Value.DataSource = Enum.GetValues(typeof(E_AreaSensorDir)).Cast<E_AreaSensorDir>();
            cmbo_AreaSensor2_Value.DataSource = Enum.GetValues(typeof(E_AreaSensorDir)).Cast<E_AreaSensorDir>();

            uc_bt_Save1.MyClick += uc_bt_Save1_Click;
        }


        private void setDataGridView(List<VSECTION_100> secs)
        {
            foreach (VSECTION_100 sec in secs)
            {
                vsecs.Add(new VSection_100ObjToShow(sec));
            }
            bindingSource.DataSource = vsecs;

            dataGridView1.DataSource = bindingSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int select_index = dataGridView1.SelectedRows[0].Index;
            var v_sec = vsecs[select_index];

            E_DIRC_DRIV drive_dir = v_sec.DIRC_DRIV;
            E_DIRC_GUID guide_dir = v_sec.DIRC_GUID;
            string seg_num = v_sec.SEG_NUM;

            E_DIRC_GUID guide_dir_1 = v_sec.CDOG_1;
            E_DIRC_GUID guide_dir_2 = v_sec.CDOG_2;
            string segment_num_1 = v_sec.CHG_SEG_NUM_1;
            string segment_num_2 = v_sec.CHG_SEG_NUM_2;
            E_AreaSensorDir areaSensorDir = v_sec.AREA_SECSOR.Value;
            E_AreaSensorDir areaSensorDir_1 = v_sec.CHG_AREA_SECSOR_1;
            E_AreaSensorDir areaSensorDir_2 = v_sec.CHG_AREA_SECSOR_2;

            double section_distance = v_sec.SEC_DIS / 10000;
            double section_speed = v_sec.SEC_SPD.HasValue ? (v_sec.SEC_SPD.Value / 100) : 0;

            bool PresenceBlockingRequest = v_sec.PRE_BLO_REQ == 1;//0
            bool PresenceDiverge = v_sec.BRANCH_FLAG;//1
            bool PresenceHIDControl = v_sec.HID_CONTROL;//2
            bool EnableChangeGuideArea = v_sec.ENB_CHG_G_AREA == 1;//4
            bool PresenceAddressReport = v_sec.PRE_ADD_REPR == 1;//8
            bool LaserSensor_ForwardStraight = v_sec.RANGE_SENSOR_F == 1;//12
            bool CollisionAvoidance_ForwardStraight = v_sec.OBS_SENSOR_F == 1;//13
            bool CollisionAvoidance_RightStraight = v_sec.OBS_SENSOR_R == 1;//14
            bool CollisionAvoidance_LeftStraight = v_sec.OBS_SENSOR_L == 1;//15
            UInt16 contrl_value = convertvSec2ControlTable(v_sec);

            cmbo_AreaSensor_Value.SelectedItem = areaSensorDir;
            cmbo_SectionID_Value.SelectedIndex = select_index;
            cmbo_DriveDir_Value.SelectedItem = drive_dir;
            cmbo_GuideDir_Value.SelectedItem = guide_dir;
            cmbo_SegmentNo_Value.SelectedItem = seg_num;
            cmbo_DirectionOfGuide1_Value.SelectedItem = guide_dir_1;
            cmbo_SegmentNumber1_Value.SelectedItem = segment_num_1.Trim();
            cmbo_AreaSensor1_Value.SelectedItem = areaSensorDir_1;
            cmbo_DirectionOfGuide2_Value.SelectedItem = guide_dir_2;
            cmbo_SegmentNumber2_Value.SelectedItem = segment_num_2.Trim();
            cmbo_AreaSensor2_Value.SelectedItem = areaSensorDir_2;

            num_section_dis.Value = (decimal)section_distance;
            num_section_speed.Value = (decimal)section_speed;

            //ck_CA_Senser_Forward.Checked = CollisionAvoidance_ForwardStraight;
            //skinCheckBox2.Checked = CollisionAvoidance_RightStraight;
            //skinCheckBox3.Checked = CollisionAvoidance_LeftStraight;
            //skinCheckBox4.Checked = LaserSensor_ForwardStraight;
            //skinCheckBox5.Checked = PresenceBlockingRequest;
            //skinCheckBox6.Checked = PresenceAddressReport;
            //skinCheckBox7.Checked = EnableChangeGuideArea;
            //skinCheckBox8.Checked = PresenceHIDControl;
            //skinCheckBox9.Checked = PresenceDiverge;

            num_controlTable_Value.Value = contrl_value;
        }
        private UInt16 convertvSec2ControlTable(VSection_100ObjToShow vSec)
        {
            return convertvSec2ControlTable(int2Bool(vSec.PRE_BLO_REQ), vSec.BRANCH_FLAG, vSec.HID_CONTROL, vSec.CAN_GUIDE_CHG, vSec.IS_ADR_RPT,
                                        int2Bool(vSec.RANGE_SENSOR_F), int2Bool(vSec.OBS_SENSOR_F), int2Bool(vSec.OBS_SENSOR_R), int2Bool(vSec.OBS_SENSOR_L)); ;
        }
        private UInt16 convertvSec2ControlTable
            (bool PRE_ADD_REPR, bool BRANCH_FLAG, bool HID_CONTROL, bool CAN_GUIDE_CHG, bool IS_ADR_RPT,
            bool RANGE_SENSOR_F, bool OBS_SENSOR_F, bool OBS_SENSOR_R, bool OBS_SENSOR_L)
        {
            System.Collections.BitArray bitArray = new System.Collections.BitArray(16);
            bitArray[0] = PRE_ADD_REPR;
            bitArray[1] = BRANCH_FLAG;
            bitArray[2] = HID_CONTROL;
            bitArray[3] = false;
            bitArray[4] = CAN_GUIDE_CHG;
            bitArray[6] = false;
            bitArray[7] = false;
            bitArray[8] = IS_ADR_RPT;
            bitArray[9] = false;
            bitArray[10] = false;
            bitArray[11] = false;
            bitArray[12] = RANGE_SENSOR_F;
            bitArray[13] = OBS_SENSOR_F;
            bitArray[14] = OBS_SENSOR_R;
            bitArray[15] = OBS_SENSOR_L;
            return getUInt16FromBitArray(bitArray);
        }
        private bool int2Bool(int i)
        {
            return i == 1;
        }
        private UInt16 getUInt16FromBitArray(System.Collections.BitArray bitArray)
        {

            if (bitArray.Length > 16)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return (UInt16)array[0];
        }


        private async void uc_bt_Save1_Click(object sender, EventArgs e)
        {
            string sec_id = cmbo_SectionID_Value.Text as string;
            E_DIRC_DRIV drive_dir = (E_DIRC_DRIV)cmbo_DriveDir_Value.SelectedItem;
            E_DIRC_GUID guide_dir = (E_DIRC_GUID)cmbo_GuideDir_Value.SelectedItem;
            E_AreaSensorDir area_sensor = (E_AreaSensorDir)cmbo_AreaSensor_Value.SelectedItem;
            //string seg_num = cmbo_SegmentNo_Value.Text;

            E_DIRC_GUID guide_dir_1 = (E_DIRC_GUID)cmbo_DirectionOfGuide1_Value.SelectedItem;
            E_DIRC_GUID guide_dir_2 = (E_DIRC_GUID)cmbo_DirectionOfGuide2_Value.SelectedItem;
            string segment_num_1 = cmbo_SegmentNumber1_Value.Text;
            string segment_num_2 = cmbo_SegmentNumber2_Value.Text;
            E_AreaSensorDir areaSensorDir_1 = (E_AreaSensorDir)cmbo_AreaSensor1_Value.SelectedItem;
            E_AreaSensorDir areaSensorDir_2 = (E_AreaSensorDir)cmbo_AreaSensor2_Value.SelectedItem;

            double section_distance = (double)(num_section_dis.Value * 10000);
            double section_speed = (double)(num_section_speed.Value * 100);

            bool CollisionAvoidance_ForwardStraight = ck_CA_Senser_Forward.Checked;
            bool CollisionAvoidance_RightStraight = skinCheckBox2.Checked;
            bool CollisionAvoidance_LeftStraight = skinCheckBox3.Checked;
            bool LaserSensor_ForwardStraight = skinCheckBox4.Checked;
            bool PresenceBlockingRequest = skinCheckBox5.Checked;
            bool PresenceAddressReport = skinCheckBox6.Checked;
            bool EnableChangeGuideArea = skinCheckBox7.Checked;
            bool PresenceHIDControl = skinCheckBox8.Checked;
            bool PresenceDiverge = skinCheckBox9.Checked;

            ASECTION sec = new ASECTION()
            {
                SEC_ID = sec_id,
                DIRC_DRIV = drive_dir,
                DIRC_GUID = guide_dir,
                AREA_SECSOR = area_sensor,
                CDOG_1 = guide_dir_1,
                CDOG_2 = guide_dir_2,
                CHG_SEG_NUM_1 = segment_num_1,
                CHG_SEG_NUM_2 = segment_num_2,
                SEC_DIS = section_distance,
                SEC_SPD = section_speed,
                PRE_BLO_REQ = PresenceBlockingRequest ? 1 : 0,
                //BRANCH_FLAG = PresenceDiverge
            };
            ASECTION_CONTROL_100 sec_100 = new ASECTION_CONTROL_100()
            {
                SEC_ID = sec_id,
                CHG_AREA_SECSOR_1 = areaSensorDir_1,
                CHG_AREA_SECSOR_2 = areaSensorDir_2,
                OBS_SENSOR_F = CollisionAvoidance_ForwardStraight ? 1 : 0,
                OBS_SENSOR_R = CollisionAvoidance_RightStraight ? 1 : 0,
                OBS_SENSOR_L = CollisionAvoidance_LeftStraight ? 1 : 0,
                RANGE_SENSOR_F = LaserSensor_ForwardStraight ? 1 : 0,
                IS_ADR_RPT = PresenceAddressReport,
                CAN_GUIDE_CHG = EnableChangeGuideArea,
                HID_CONTROL = PresenceHIDControl,
                BRANCH_FLAG = PresenceDiverge
            };
            bool isSuccess = false;
            await Task.Run(() =>
             {
                 isSuccess = dataSetting.updateSectionData(sec, sec_100);
             });
            if (isSuccess)
            {
                VSection_100ObjToShow vsec = vsecs.Where(secShow => secShow.SEC_ID == sec_id).SingleOrDefault();
                vsec.DIRC_DRIV = sec.DIRC_DRIV;
                vsec.DIRC_GUID = sec.DIRC_GUID;
                vsec.AREA_SECSOR = sec.AREA_SECSOR;
                vsec.CDOG_1 = sec.CDOG_1;
                vsec.CDOG_2 = sec.CDOG_2;
                vsec.CHG_SEG_NUM_1 = sec.CHG_SEG_NUM_1;
                vsec.CHG_SEG_NUM_2 = sec.CHG_SEG_NUM_2;
                vsec.SEC_DIS = sec.SEC_DIS;
                vsec.SEC_SPD = sec.SEC_SPD;
                vsec.PRE_BLO_REQ = sec.PRE_BLO_REQ;
                //vsec.PRE_DIV = sec.PRE_DIV;

                vsec.CHG_AREA_SECSOR_1 = sec_100.CHG_AREA_SECSOR_1;
                vsec.CHG_AREA_SECSOR_2 = sec_100.CHG_AREA_SECSOR_2;
                vsec.OBS_SENSOR_F = sec_100.OBS_SENSOR_F;
                vsec.OBS_SENSOR_R = sec_100.OBS_SENSOR_R;
                vsec.OBS_SENSOR_L = sec_100.OBS_SENSOR_L;
                vsec.RANGE_SENSOR_F = sec_100.RANGE_SENSOR_F;
                vsec.IS_ADR_RPT = sec_100.IS_ADR_RPT;
                vsec.CAN_GUIDE_CHG = sec_100.CAN_GUIDE_CHG;
                vsec.HID_CONTROL = sec_100.HID_CONTROL;
                vsec.BRANCH_FLAG = sec_100.BRANCH_FLAG;
            }

        }

        private void uc_bt_Save1_Load(object sender, EventArgs e)
        {

        }

        private void num_controlTable_Value_ValueChanged(object sender, EventArgs e)
        {
            UInt16 intValue = (UInt16)num_controlTable_Value.Value;
            var bools = new System.Collections.BitArray(new int[] { intValue }).Cast<bool>().ToArray();

            bool PresenceBlockingRequest = bools[0];//0
            bool PresenceDiverge = bools[1];//1
            bool PresenceHIDControl = bools[2];//2
            bool EnableChangeGuideArea = bools[4];//4
            bool PresenceAddressReport = bools[8];//8
            bool LaserSensor_ForwardStraight = bools[12];//12
            bool CollisionAvoidance_ForwardStraight = bools[13];//13
            bool CollisionAvoidance_RightStraight = bools[14];//14
            bool CollisionAvoidance_LeftStraight = bools[15];//15

            skinCheckBox5.Checked = PresenceBlockingRequest;
            skinCheckBox9.Checked = PresenceDiverge;
            skinCheckBox8.Checked = PresenceHIDControl;
            skinCheckBox7.Checked = EnableChangeGuideArea;
            skinCheckBox6.Checked = PresenceAddressReport;
            skinCheckBox4.Checked = LaserSensor_ForwardStraight;
            ck_CA_Senser_Forward.Checked = CollisionAvoidance_ForwardStraight;
            skinCheckBox2.Checked = CollisionAvoidance_RightStraight;
            skinCheckBox3.Checked = CollisionAvoidance_LeftStraight;


        }

        private void ck_ControlTableItems_Click(object sender, EventArgs e)
        {
            bool PresenceBlockingRequest = skinCheckBox5.Checked;
            bool PresenceDiverge = skinCheckBox9.Checked;
            bool PresenceHIDControl = skinCheckBox8.Checked;
            bool EnableChangeGuideArea = skinCheckBox7.Checked;
            bool PresenceAddressReport = skinCheckBox6.Checked;
            bool LaserSensor_ForwardStraight = skinCheckBox4.Checked;
            bool CollisionAvoidance_ForwardStraight = ck_CA_Senser_Forward.Checked;
            bool CollisionAvoidance_RightStraight = skinCheckBox2.Checked;
            bool CollisionAvoidance_LeftStraight = skinCheckBox3.Checked;


            num_controlTable_Value.Value = (decimal)convertvSec2ControlTable
                (PresenceBlockingRequest, PresenceDiverge, PresenceHIDControl, EnableChangeGuideArea, PresenceAddressReport,
                LaserSensor_ForwardStraight, CollisionAvoidance_ForwardStraight, CollisionAvoidance_RightStraight, CollisionAvoidance_LeftStraight);
        }
    }
}
