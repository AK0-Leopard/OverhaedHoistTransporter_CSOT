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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    /// <summary>
    /// uctl_ElasticQuery.xaml 的互動邏輯
    /// </summary>
    public partial class uctl_ElasticQuery : UserControl
    {

        public event EventHandler<string> QueryCommandDetailEnevt;
        public event EventHandler<string> QueryVehicleDetailEnevt;


        public uctl_ElasticQuery()
        {
            InitializeComponent();
        }

        public void setDataItemsSource<T>(IEnumerable<T> items_source)
        {
            dgv_log_query.ItemsSource = items_source;
        }

        private void MenuItem_Cmd_Detail_Click(object sender, RoutedEventArgs e)
        {
            var query = dgv_log_query.SelectedItem as Service.ASYSEXCUTEQUALITY;
            QueryCommandDetailEnevt?.Invoke(this, query.MCS_CMD_ID);
        }

        private void MenuItem_VhId_Detail_Click(object sender, RoutedEventArgs e)
        {
            var query = dgv_log_query.SelectedItem as Service.ASYSEXCUTEQUALITY;
            QueryVehicleDetailEnevt?.Invoke(this, query.VH_ID);
        }
    }
}
