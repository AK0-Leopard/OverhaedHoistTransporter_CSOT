using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl
{
    /// <summary>
    /// uc_VhSettingS1.xaml 的互動邏輯
    /// </summary>
    public partial class uc_VhSettingS1 : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_VhSettingS1()
        {
            InitializeComponent();
        }

        public void SetTXBTitleName(string titleName1, string titleName2, string titleName3, string titleName4, string titleName5, string titleName6)
        {
            try
            {
                txb_Title1.Text = titleName1;
                txb_Title2.Text = titleName2;
                txb_Title3.Text = titleName3;
                txb_Title4.Text = titleName4;
                txb_Title5.Text = titleName5;
                txb_Title6.Text = titleName6;
                txb_Title7.Visibility = Visibility.Collapsed;
                txb_Value7.Visibility = Visibility.Collapsed;
                txb.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public void SetTXBTitleName(string titleName1, string titleName2, string titleName3, string titleName4, string titleName5, string titleName6, string titleName7)
        {
            try
            {
                txb_Title1.Text = titleName1;
                txb_Title2.Text = titleName2;
                txb_Title3.Text = titleName3;
                txb_Title4.Text = titleName4;
                txb_Title5.Text = titleName5;
                txb_Title6.Text = titleName6;
                txb_Title7.Text = titleName7;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void SetVehicleCmdInfo(string action_status, string MCS_cmd_id, string OHxC_cmd_id, string cmd_type, string carrier_id, string source, string destination)
        {
            try
            {
                txb_Value1.Text = action_status;
                txb_Value2.Text = MCS_cmd_id;
                txb_Value3.Text = OHxC_cmd_id;
                txb_Value4.Text = cmd_type;
                txb_Value5.Text = carrier_id;
                txb_Value6.Text = source;
                txb_Value7.Text = destination;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void SetTXBVehicleInfo(string vh_id, string mode, string curr_adr, string curr_sec, string sec_dis, string alarm_sts)
        {
            try
            {
                txb_Value1.Text = vh_id;
                txb_Value2.Text = mode;
                txb_Value3.Text = curr_adr;
                txb_Value4.Text = curr_sec;
                txb_Value5.Text = sec_dis;
                txb_Value6.Text = alarm_sts;

                if (mode == VHModeStatus.AutoRemote.ToString())
                {
                    btn_Title1.IsEnabled = false;
                    btn_Title2.IsEnabled = true;
                    btn_Title3.IsEnabled = true;
                    btn_Title4.IsEnabled = true;
                    btn_Title5.IsEnabled = true;
                }
                else if (mode == VHModeStatus.AutoLocal.ToString())
                {
                    btn_Title1.IsEnabled = true;
                    btn_Title2.IsEnabled = false;
                    btn_Title3.IsEnabled = true;
                    btn_Title4.IsEnabled = true;
                    btn_Title5.IsEnabled = true;
                }
                else if (mode == VHModeStatus.AutoMtl.ToString())
                {
                    btn_Title1.IsEnabled = true;
                    btn_Title2.IsEnabled = true;
                    btn_Title3.IsEnabled = false;
                    btn_Title4.IsEnabled = true;
                    btn_Title5.IsEnabled = true;
                }
                else if (mode == VHModeStatus.AutoMts.ToString())
                {
                    btn_Title1.IsEnabled = true;
                    btn_Title2.IsEnabled = true;
                    btn_Title3.IsEnabled = true;
                    btn_Title4.IsEnabled = false;
                    btn_Title5.IsEnabled = true;
                }
                else if (mode == VHModeStatus.Manual.ToString())
                {
                    btn_Title1.IsEnabled = true;
                    btn_Title2.IsEnabled = true;
                    btn_Title3.IsEnabled = true;
                    btn_Title4.IsEnabled = true;
                    btn_Title5.IsEnabled = false;
                }
                else
                {
                    btn_Title1.IsEnabled = true;
                    btn_Title2.IsEnabled = true;
                    btn_Title3.IsEnabled = true;
                    btn_Title4.IsEnabled = true;
                    btn_Title5.IsEnabled = true;
                }

                //if(alarm_sts == "0")
                //{
                //    btn_Title5.IsEnabled = false;
                //}
                //else
                //{
                //    btn_Title5.IsEnabled = true;
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetBTNTitleName(string btnName1, string btnName2, string btnName3, string btnName4, string btnName5, string btnName6)
        {
            try
            {
                btn_Title1.Content = btnName1;
                btn_Title2.Content = btnName2;
                btn_Title3.Content = btnName3;
                btn_Title4.Content = btnName4;
                btn_Title5.Content = btnName5;
                btn_Title6.Content = btnName6;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetBTNTitleName(string btnName1)
        {
            try
            {
                btn_Title1.Content = btnName1;
                btn_Title2.Visibility = Visibility.Collapsed;
                btn_Title3.Visibility = Visibility.Collapsed;
                btn_Title4.Visibility = Visibility.Collapsed;
                btn_Title5.Visibility = Visibility.Collapsed;
                btn_Title6.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
