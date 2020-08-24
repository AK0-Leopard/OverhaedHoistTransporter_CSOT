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

namespace WPF.Components
{
    /// <summary>
    /// uc_DeviceStatusSignal.xaml 的互動邏輯
    /// </summary>
    public partial class uc_DeviceStatusSignal : UserControl
    {
        public uc_DeviceStatusSignal()
        {
            InitializeComponent();
        }

        //更新連線狀態(連線/斷線)
        public void SetConnStatus(string title, bool ConnectionStatus)
        {
            TitleName.Text = title;
            if (ConnectionStatus == true)
            {
                LinkSignal.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\icon_Link_ON.png", UriKind.Relative));

                //LinkOk.Visibility = Visibility.Visible;
                //LinkFail.Visibility = Visibility.Collapsed;
            }
            else
            {
                LinkSignal.Source = new BitmapImage(new Uri(@"\Resources\SystemIcon\icon_Link_OFF.png", UriKind.Relative));

                //LinkOk.Visibility = Visibility.Collapsed;
                //LinkFail.Visibility = Visibility.Visible;
            }
        }
    }
}
