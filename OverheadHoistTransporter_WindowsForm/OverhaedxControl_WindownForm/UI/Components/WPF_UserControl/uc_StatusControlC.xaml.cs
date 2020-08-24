using com.mirle.ibg3k0.bcf.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    /// <summary>
    /// uc_StatusControl.xaml 的互動邏輯
    /// </summary>
    public partial class uc_StatusControlC : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_StatusControlC()
        {
            InitializeComponent();
        }

        public void SetTitleName(string TitleValue, string btn_Custom1, string btn_Custom2, string btn_Custom3)
        {
            try
            {
                lab_TitleValue.Text = TitleValue;
                Button1.Content = btn_Custom1;
                Button2.Content = btn_Custom2;
                Button3.Content = btn_Custom3;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新連線狀態(連線/斷線)
        public void SetConnSignal(string SignalValue, bool ConnectionStatus)
        {
            try
            {
                Adapter.BeginInvoke(new SendOrPostCallback((o1) =>
                {
                    lab_SignalValue.Text = SignalValue;
                    if (ConnectionStatus == true)
                    {
                        lab_SignalValue.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 204, 0));
                    }
                    else
                    {
                        lab_SignalValue.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                    }
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        
        
    }
}
