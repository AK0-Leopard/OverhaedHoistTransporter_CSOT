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
    public partial class uctl_ElasticQuery_System_Process : UserControl
    {
        /// <summary>
        /// 以VH ID 查詢
        /// </summary>
        public event Action<string, DateTime> QueryCommandDetailEnevt;

        public uctl_ElasticQuery_System_Process()
        {
            InitializeComponent();
        }

        public void setDataItemsSource<T>(IEnumerable<T> items_source)
        {
            dgv_log_query.ItemsSource = items_source;
        }

        private void MenuItem_Cmd_Detail_Click(object sender, RoutedEventArgs e)
        {
            var query = dgv_log_query.SelectedItem as Service.RecordReportInfo;
            QueryCommandDetailEnevt?.Invoke(query.VH_ID, query.Timestamp);

        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;
            row.DetailsVisibility = row.DetailsVisibility == Visibility.Collapsed ?
             Visibility.Visible : Visibility.Collapsed;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void RowDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
