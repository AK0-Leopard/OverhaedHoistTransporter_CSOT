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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl
{
    /// <summary>
    /// uc_StatusInfo_Default.xaml 的互動邏輯
    /// </summary>
    public partial class uc_StatusInfo_Default : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static App.WindownApplication app = null;
        //*******************公用參數設定*******************

        public uc_StatusInfo_Default()
        {
            InitializeComponent();
        }

        //組件載入
        private void MainSignalBackGround_Load(object sender, EventArgs e)
        {
            try
            {
                app = App.WindownApplication.getInstance();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //設定標題文字、標題值、標題字體顏色
        public void setTitleName(string title1, string title2, string title3)
        {
            try
            {
                labTitle1.Text = title1;
                labTitle2.Text = title2;
                Title.Text = title3;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string TransferCount
        { set { labVal1.Text = value; } }
        public string WaitingCount
        { set { labVal2.Text = value; } }
        public string AssignedCount
        { set { labVal1.Text = value; } }
        public string WatingCount
        { set { labVal2.Text = value; } }
    }
}
