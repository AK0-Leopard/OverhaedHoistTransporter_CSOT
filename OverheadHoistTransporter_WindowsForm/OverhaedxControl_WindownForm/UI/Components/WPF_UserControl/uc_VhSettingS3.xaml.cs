﻿using NLog;
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
    /// uc_VhSettingS3.xaml 的互動邏輯
    /// </summary>
    public partial class uc_VhSettingS3 : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_VhSettingS3()
        {
            InitializeComponent();
        }

        public void SetTXBTitleName(string titleName1, string titleName2, string titleName3, string titleName4, string titleName5)
        {
            try
            {
                txb_Title1.Text = titleName1;
                txb_Title2.Text = titleName2;
                txb_Title3.Text = titleName3;
                txb_Title4.Text = titleName4;
                txb_Title5.Text = titleName5;
                //txb_Title6.Text = titleName6;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetVehicleCommandInfo(string vh_id)
        {
            try
            {
                if (txt_Content1.IsReadOnly == false)
                {
                    txt_Content1.IsReadOnly = true;
                }
                txt_Content1.Text = vh_id;
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
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
