using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.sc.Common;
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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    /// <summary>
    /// uc_TitleContent.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TitleContent : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        SysExcuteQualityQueryService sysExcuteQualityQueryService;
        public event EventHandler<LogRequestEventArgs> LogRequest;


        public uc_TitleContent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox3.Focus(); //將游標指定在密碼位置
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(TextBox3, false); //設置IME和輸入是否可以是中文
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(TextBox4, false); //設置IME和輸入是否可以是中文
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(TextBox6, false); //設置IME和輸入是否可以是中文
            //startupUI();
        }

        //private void startupUI()
        //{
        //    m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
        //    m_EndDTCbx.Value = DateTime.Now;
        //    cb_HrsInterval.MouseWheel += Cb_HrsInterval_MouseWheel;
        //    cb_HrsInterval.Items.Add("");
        //    for (int i = 1; i <= 24; i++)
        //    {
        //        cb_HrsInterval.Items.Add("Last " + i + " Hours");
        //    }
        //}

        private void Cb_HrsInterval_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        //private async Task search()
        //{
        //    try
        //    {
        //        DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
        //        DateTime dateTimeTo = System.Convert.ToDateTime(m_EndDTCbx.Value);

        //        string keyword = string.Empty;
        //        if (SCUtility.isMatche(TextBox3.Text, "")) //確認Carrier ID是否為空
        //        {
        //            keyword = string.Empty;
        //        }
        //        else
        //        {
        //            keyword = TextBox3.Text.Trim();
        //        }

        //        dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
        //        dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
        //        string cst_id = TextBox3.Text;
        //        List<ASYSEXCUTEQUALITY> system_qualitys = null;
        //        await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo));

        //        //await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo, keyword));
        //        system_qualitys = system_qualitys.OrderBy(info => info.CMD_INSERT_TIME).ToList();
        //        //uctl_ElasticQuery_CMDExcute_1.setDataItemsSource(system_qualitys);
        //        LogRequest?.Invoke(this, new LogRequestEventArgs(dateTimeFrom, dateTimeTo, cst_id));
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { ButtonClick(sender, e); }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (sender.Equals(Hyperlink1))
            //    {
            //        m_StartDTCbx.Value = DateTime.Now.AddMinutes(-30);
            //        m_EndDTCbx.Value = DateTime.Now;
            //        //ButtonClick(btn_Search,e);
            //    }
            //    else if (sender.Equals(Hyperlink2))
            //    {

            //    }
            //    else if (sender.Equals(Hyperlink3))
            //    {

            //    }
            //    else if (sender.Equals(Hyperlink4))
            //    {

            //    }
            //    else if (sender.Equals(Hyperlink5))
            //    {

            //    }
            //    else if (sender.Equals(Hyperlink6))
            //    {

            //    }
            //    else if (sender.Equals(Hyperlink7))
            //    {

            //    }
            //    else if (sender.Equals(btn_Search))
            //    {
            //        DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
            //        DateTime dateTimeTo = System.Convert.ToDateTime(m_EndDTCbx.Value);

            //        string keyword = string.Empty;
            //        if (SCUtility.isMatche(TextBox3.Text, "")) //確認Carrier ID是否為空
            //        {
            //            keyword = string.Empty;
            //        }
            //        else
            //        {
            //            keyword = TextBox3.Text.Trim();
            //        }

            //        dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            //        dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            //        string cst_id = TextBox3.Text;
            //        LogRequest?.Invoke(this, new LogRequestEventArgs(dateTimeFrom, dateTimeTo, keyword));

            //        //List<ASYSEXCUTEQUALITY> system_qualitys = null;
            //        //await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo));

            //        //await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo, keyword));
            //        //system_qualitys = system_qualitys.OrderBy(info => info.CMD_INSERT_TIME).ToList();
            //        //uctl_ElasticQuery_CMDExcute_1.setDataItemsSource(system_qualitys);
            //    }
            //}
            //finally { }
        }

        public class LogRequestEventArgs : EventArgs
        {
            public LogRequestEventArgs(DateTime startTime, DateTime endTime, string cstId)
            {
                this.startTime = startTime;
                this.endTime = endTime;
                this.cstId = cstId;
            }

            public DateTime startTime { get; private set; }
            public DateTime endTime { get; private set; }
            public string cstId { get; private set; }
        }

        public void SetTitleNameInMcsLog(string titleName)
        {
            try
            {
                DockPanel4.Visibility = Visibility.Collapsed;
                DockPanel6.Visibility = Visibility.Collapsed;
                TitleName3.Text = titleName;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetTitleNameInComuLog(string titleName3, string titleName4)
        {
            try
            {
                DockPanel6.Visibility = Visibility.Collapsed;
                TitleName3.Text = titleName3;
                TitleName4.Text = titleName4;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}
