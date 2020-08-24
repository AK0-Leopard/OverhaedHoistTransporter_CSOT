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
    /// uc_StatusSignal.xaml 的互動邏輯
    /// </summary>
    public partial class uc_StatusSignal : UserControl
    {
        public uc_StatusSignal()
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
                    TitleName.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 204, 0));
                }
                else
                {
                    TitleName.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Exception");
            }
        }
    }
}
