using com.mirle.ibg3k0.bcf.Common;
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
    /// uc_grid_CurAlarm.xaml 的互動邏輯
    /// </summary>
    public partial class uc_grid_CurAlarm : UserControl
    {
        ohxc.winform.App.WindownApplication app = null;
        public uc_grid_CurAlarm()
        {
            InitializeComponent();
        }

        public void start()
        {
            grid_Cur_Alarm.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
        }

        private void ObjCacheManager_MCSCMDUpdateComplete(object sender, EventArgs e)
        {
            refresh_MCSCMDGrp();
        }
        public void refresh_MCSCMDGrp()
        {
            Adapter.Invoke((obj) =>
            {
                grid_Cur_Alarm.ItemsSource = app.ObjCacheManager.GetMCS_CMD();
            }, null);
        }

    }
}
