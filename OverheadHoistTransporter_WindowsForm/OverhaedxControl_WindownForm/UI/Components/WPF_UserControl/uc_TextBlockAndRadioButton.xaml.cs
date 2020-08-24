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
    /// uc_TextBlockAndRadioButton.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TextBlockAndRadioButton : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_TextBlockAndRadioButton()
        {
            InitializeComponent();
        }

        public void SetTitleName(string titleName)
        {
            try
            {
                txb_Title.Text = titleName;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
