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
    /// uc_TextBlockAndPasswordbox.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TextBlockAndPasswordbox : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_TextBlockAndPasswordbox()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(pwd_Password, false); //設置IME和輸入是否可以是中文
        }

        public void SetTitleName(string titleName)
        {
            try
            {
                txb_Title.Text = titleName;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
