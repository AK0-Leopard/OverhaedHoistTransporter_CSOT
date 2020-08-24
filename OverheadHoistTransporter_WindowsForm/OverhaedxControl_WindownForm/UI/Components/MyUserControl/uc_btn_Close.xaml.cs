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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.Tool
{
    /// <summary>
    /// uc_btn_Close.xaml 的互動邏輯
    /// </summary>
    public partial class uc_btn_Close : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public event EventHandler Button_Click;

        public uc_btn_Close()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if (Button_Click != null)
            {
                Button_Click(sender, e);
            }
        }
    }
}