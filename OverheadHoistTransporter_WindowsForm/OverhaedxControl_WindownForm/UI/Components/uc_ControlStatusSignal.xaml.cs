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

namespace WPF.Components
{
    /// <summary>
    /// uc_ControlStatusSignal.xaml 的互動邏輯
    /// </summary>
    public partial class uc_ControlStatusSignal : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_ControlStatusSignal()
        {
            InitializeComponent();
        }

        //更新連線狀態(連線/斷線)
        public void SetConnStatus(string title, bool ConnectionStatus)
        {
            try
            {
                TitleName.Text = title;
                if (ConnectionStatus == true)
                {
                    LinkSignal.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\Control status_LinkOk.png", UriKind.Relative));

                    //LinkSignal.Visibility = Visibility.Visible;
                    //LinkFail.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LinkSignal.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\Control status_LinkFail.png", UriKind.Relative));

                    //LinkOk.Visibility = Visibility.Collapsed;
                    //LinkFail.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}