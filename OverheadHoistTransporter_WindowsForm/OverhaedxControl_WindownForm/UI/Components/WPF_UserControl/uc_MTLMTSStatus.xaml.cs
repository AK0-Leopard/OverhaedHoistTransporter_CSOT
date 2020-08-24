using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using com.mirle.ibg3k0.sc.Data.VO;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl
{
    /// <summary>
    /// uc_MTLMTSStatus.xaml 的互動邏輯
    /// </summary>
    public partial class uc_MTLMTSStatus : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        App.WindownApplication app = null;
        List<AEQPT> MTL1MTS1List;
        List<AEQPT> MTS2List;
        public event EventHandler<MTSMTLInterlockChangeEventArgs> mTSMTLInterlockChange;

        public uc_MTLMTSStatus()
        {
            InitializeComponent();
            //init();
        }
        public void init()
        {
            app = App.WindownApplication.getInstance();
            //MTL = app.ObjCacheManager.GetVEHICLEs(device_id) as MaintainLift;
            MTL1MTS1List = app.ObjCacheManager.GetMTL1MTS1();
            MTS2List = app.ObjCacheManager.GetMTS2();

            var all_mtl1_mts1_detail_view_obj = this.MTL1MTS1List.Select(eq => new MTLMTSDatailViewObj(eq));
            grid_MTLMTS1.ItemsSource = all_mtl1_mts1_detail_view_obj;
            var all_mts2_detail_view_obj = this.MTS2List.Select(eq => new MTLMTSDatailViewObj(eq));
            grid_MTS2.ItemsSource = all_mts2_detail_view_obj;
            refreshUI();
            registerEvent();
        }


        public void registerEvent()
        {
            app.LineBLL.SubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_MTLMTS_INFO, app.LineBLL.MTLMTSInfo);
            app.ObjCacheManager.MTLMTSInfoUpdate += ObjCacheManager_MTLMTSInfoUpdate;
            mTSMTLInterlockChange += MTSMTLInterlockChange;

        }
        public void unRegisterEvent()
        {
            app.LineBLL.UnsubscriberLineInfo(BCAppConstants.NATSTopics.NATS_SUBJECT_MTLMTS_INFO);
            app.ObjCacheManager.MTLMTSInfoUpdate -= ObjCacheManager_MTLMTSInfoUpdate;
            mTSMTLInterlockChange -= MTSMTLInterlockChange;
        }

        private void ObjCacheManager_MTLMTSInfoUpdate(object sender, EventArgs e)
        {
            Adapter.Invoke((obj) =>
            {
                refreshUI();
            }, null);
        }

        private void refreshUI()
        {
            grid_MTLMTS1.Items.Refresh();
            grid_MTS2.Items.Refresh();

            MaintainLift MTL = app.ObjCacheManager.GetMTLMTSByID("MTL") as MaintainLift;
            displayForInterlock(MTL.EQPT_ID, MTL.Interlock);
            displayForVehicleLift(MTL.EQPT_ID, MTL.MTLLocation.ToString(), !string.IsNullOrWhiteSpace(MTL.CurrentCarID));

            MaintainSpace MTS = app.ObjCacheManager.GetMTLMTSByID("MTS") as MaintainSpace;
            displayForInterlock(MTS.EQPT_ID, MTS.Interlock);
            displayForVehicleLift(MTS.EQPT_ID, null, !string.IsNullOrWhiteSpace(MTS.CurrentCarID));
            MaintainSpace MTS2 = app.ObjCacheManager.GetMTLMTSByID("MTS2") as MaintainSpace;
            displayForInterlock(MTS2.EQPT_ID, MTS2.Interlock);
            displayForVehicleLift(MTS2.EQPT_ID, null, !string.IsNullOrWhiteSpace(MTS2.CurrentCarID));
        }


        private void MTSMTLInterlockChange(object sender, MTSMTLInterlockChangeEventArgs e)
        {
            try
            {

                Task.Run(() =>
                {
                    if (!app.LineBLL.SendMTSMTLCarOutInterlock(e.station_id, e.is_set, out string result))
                    {
                        //MessageBox.Show(result);
                        Adapter.Invoke((obj) =>
                        {
                            refreshUI();
                        }, null);
                        TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("", "Interlock Change Request Succeed", BCAppConstants.INFO_MSG);
                    }
                    //app.LineBLL.SendHostModeChange(e.host_mode);
                });

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void displayForInterlock(string station_id ,bool isSet)
        {
            var mtlLockOn = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_green.png", UriKind.Relative));
            var mtlLockOff = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_red.png", UriKind.Relative));
            var mtsLockOn = new BitmapImage(new Uri(@"\Resources\MTS_green.png", UriKind.Relative));
            var mtsLockOff = new BitmapImage(new Uri(@"\Resources\MTS_red.png", UriKind.Relative));
            if (station_id == "MTL")
            {
                if (isSet)
                {
                    InLockSignal_MTL1A.Source = mtlLockOn;
                    InLockSignal_MTL1B.Source = mtlLockOn;
                }
                else
                {
                    InLockSignal_MTL1A.Source = mtlLockOff;
                    InLockSignal_MTL1B.Source = mtlLockOff;
                }
            }
            else if (station_id == "MTS")
            {
                if (isSet)
                {
                    InLockSignal_MTS1A.Source = mtsLockOn;
                    InLockSignal_MTS1B.Source = mtsLockOn;

                }
                else
                {
                    InLockSignal_MTS1A.Source = mtsLockOff;
                    InLockSignal_MTS1B.Source = mtsLockOff;
                }
            }
            else if (station_id == "MTS2")
            {
                if (isSet)
                {
                    InLockSignal_MTS2A.Source = mtsLockOn;
                    InLockSignal_MTS2B.Source = mtsLockOn;
                }
                else
                {
                    InLockSignal_MTS2A.Source = mtsLockOff;
                    InLockSignal_MTS2B.Source = mtsLockOff;
                }
            }
        }
        private void displayForVehicleLift(string station_id,string location, bool isSet)
        {
            var withCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
            var withwoCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));
            if (station_id == "MTL")
            {
                if (isSet)
                {
                    if (location == "Upper")
                    {
                        VhSignal_MTLUp.Source = withCar;
                        VhSignal_MTLDown.Source = withwoCar;

                        //Lifter Label(向上時)
                        LiftSigOff_Up.Visibility = Visibility.Collapsed;
                        LiftSigOn_Up.Visibility = Visibility.Visible;
                        LiftSigOff_Down.Visibility = Visibility.Visible;
                        LiftSigOn_Down.Visibility = Visibility.Collapsed;
                    }
                    else if (location == "Bottorn")
                    {
                        VhSignal_MTLUp.Source = withwoCar;
                        VhSignal_MTLDown.Source = withCar;

                        //Lifter Label(向下時)
                        LiftSigOff_Up.Visibility = Visibility.Visible;
                        LiftSigOn_Up.Visibility = Visibility.Collapsed;
                        LiftSigOff_Down.Visibility = Visibility.Collapsed;
                        LiftSigOn_Down.Visibility = Visibility.Visible;
                    }
                    else if (location == "None")
                    {
                        VhSignal_MTLUp.Source = withwoCar;
                        VhSignal_MTLDown.Source = withwoCar;
                        LiftArrowSig.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    VhSignal_MTLUp.Source = withwoCar;
                    VhSignal_MTLDown.Source = withwoCar;
                }
            }
            else if (station_id == "MTS")
            {
                if (isSet)
                {
                    //MTS區域內有車輛
                    VhSignal_MTS1.Source = withCar;
                }
                else
                {
                    //MTS區域內沒有車輛
                    VhSignal_MTS1.Source = withwoCar;
                }
            }
            else if (station_id == "MTS2")
            {
                if (isSet)
                {
                    //MTS2區域內有車輛
                    VhSignal_MTS2.Source = withCar;
                }
                else
                {
                    //MTS2區域內沒有車輛
                    VhSignal_MTS2.Source = withwoCar;
                }
            }
        }


        private void SignalDisplay()
        {
            //var withCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
            //var withwoCar = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));

            //if (EQPT_ID == "MTL")
            //{
            //    if (CurrentCarID != null)
            //    {
            //        if (MTLLocation == "Upper")
            //        {
            //            VhSignal_MTLUp.Source = withCar;
            //            VhSignal_MTLDown.Source = withwoCar;

            //            //Lifter Label(向上時)
            //            LiftSigOff_Up.Visibility = Visibility.Collapsed;
            //            LiftSigOn_Up.Visibility = Visibility.Visible;
            //            LiftSigOff_Down.Visibility = Visibility.Visible;
            //            LiftSigOn_Down.Visibility = Visibility.Collapsed;

                          ////Lifter箭頭向下, 呈灰色燈號
                          //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Down_Off.png", UriKind.Relative));
            //        }
            //        else if (MTLLocation == "Bottorn")
            //        {
            //            VhSignal_MTLUp.Source = withwoCar;
            //            VhSignal_MTLDown.Source = withCar;

            //            //Lifter Label(向下時)
            //            LiftSigOff_Up.Visibility = Visibility.Visible;
            //            LiftSigOn_Up.Visibility = Visibility.Collapsed;
            //            LiftSigOff_Down.Visibility = Visibility.Collapsed;
            //            LiftSigOn_Down.Visibility = Visibility.Visible;
                          ////Lifter箭頭向上, 呈灰色燈號
                          //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Up_Off.png", UriKind.Relative));
            //        }
            //        else if (MTLLocation == "None")
            //        {
            //            VhSignal_MTLUp.Source = withwoCar;
            //            VhSignal_MTLDown.Source = withwoCar;
            //            LiftArrowSig.Visibility = Visibility.Collapsed;
            //        }
            //    }
            //    else
            //    {
            //        VhSignal_MTLUp.Source = withwoCar;
            //        VhSignal_MTLDown.Source = withwoCar;
            //    }
            //}
            //else if (EQPT_ID == "MTS")
            //{
            //    if (CurrentCarID != null)
            //    {
            //        //MTS1區域內有車輛
            //        VhSignal_MTS1.Source = withCar;
            //    }
            //    else
            //    {
            //        //MTS1區域內沒有車輛
            //        VhSignal_MTS1.Source = withwoCar;
            //    }
            //}
            //else if (EQPT_ID == "MTS2")
            //{
            //    if (CurrentCarID != null)
            //    {
            //        //MTS1區域內有車輛
            //        VhSignal_MTS2.Source = withCar;
            //    }
            //    else
            //    {
            //        //MTS1區域內沒有車輛
            //        VhSignal_MTS2.Source = withwoCar;
            //    }
            //}


            ////Lifter箭頭向上, 呈橘色燈號
            //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Up_On.png", UriKind.Relative));
            ////Lifter箭頭向下, 呈橘色燈號
            //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Down_On.png", UriKind.Relative));
            ////Lifter箭頭向上, 呈灰色燈號
            //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Up_Off.png", UriKind.Relative));
            ////Lifter箭頭向下, 呈灰色燈號
            //LiftArrowSig.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Down_Off.png", UriKind.Relative)); 


            ////MTS1區域內有車輛
            //VhSignal_MTS1.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));
            ////MTS1區域內沒有車輛
            //VhSignal_MTS1.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));

            ////MTS2區域內有車輛
            //VhSignal_MTS2.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
            ////MTS2區域內沒有車輛
            //VhSignal_MTS2.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));

            ////Lifter Label(向上時)
            //LiftSigOff_Up.Visibility = Visibility.Collapsed;
            //LiftSigOn_Up.Visibility = Visibility.Visible;
            //LiftSigOff_Down.Visibility = Visibility.Visible;
            //LiftSigOn_Down.Visibility = Visibility.Collapsed;

            ////MTL的車子在上面時
            //VhSignal_MTLUp.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
            //VhSignal_MTLDown.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));

            ////Lifter Label(向下時)
            //LiftSigOff_Up.Visibility = Visibility.Visible;
            //LiftSigOn_Up.Visibility = Visibility.Collapsed;
            //LiftSigOff_Down.Visibility = Visibility.Collapsed;
            //LiftSigOn_Down.Visibility = Visibility.Visible;

            ////MTL的車子在下面時
            //VhSignal_MTLUp.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle _None.png", UriKind.Relative));
            //VhSignal_MTLDown.Source = new BitmapImage(new Uri(@"\Resources\VehiclePisplay\MTL Vehicle.png", UriKind.Relative));
        }

        BitmapImage mtlLockOn = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_green.png", UriKind.Relative));
        BitmapImage mtlLockOff = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_red.png", UriKind.Relative));
        BitmapImage mtsLockOn = new BitmapImage(new Uri(@"\Resources\MTS_green.png", UriKind.Relative));
        BitmapImage mtsLockOff = new BitmapImage(new Uri(@"\Resources\MTS_red.png", UriKind.Relative));
        //var mtlLLifterDown = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Down_Flicker.gif", UriKind.Relative));
        //var mtlLLifterUp = new BitmapImage(new Uri(@"\Resources\SystemIcon\MTL_Up_Flicker.gif", UriKind.Relative));

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            MTLMTSDatailViewObj obj = toggleButton.DataContext as MTLMTSDatailViewObj;
            mTSMTLInterlockChange?.Invoke(this, new MTSMTLInterlockChangeEventArgs(obj.stationID, toggleButton.IsChecked.ToString()));
        }

        public class MTSMTLInterlockChangeEventArgs : EventArgs
        {
            public MTSMTLInterlockChangeEventArgs(string station_id, string is_set)
            {
                this.station_id = station_id;
                this.is_set = is_set;
            }
            public string station_id { get; private set; }
            public string is_set { get; private set; }
        }
    }
}
