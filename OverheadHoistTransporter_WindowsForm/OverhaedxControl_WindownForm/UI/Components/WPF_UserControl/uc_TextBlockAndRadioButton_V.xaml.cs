using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// uc_TextBlockAndRadioButton_V.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TextBlockAndRadioButton_V : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_TextBlockAndRadioButton_V()
        {
            InitializeComponent();
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_Value, false); //設置IME和輸入是否可以是中文

        }

        public void SetTitleNameInCIS(string txb_title, string title1, string title2, string title3, string title4, string txb_content)
        {
            try
            {
                txb_Title.Text = txb_title;
                radbtn_Content1.Content = title1;
                radbtn_Content2.Content = title2;
                radbtn_Content3.Content = title3;
                radbtn_Content4.Content = title4;
                txb_Content.Text = txb_content;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void SetTitleNameInCPMS(string txb_title, string title1, string title4, string txb_content)
        {
            try
            {
                txb_Title.Text = txb_title;
                radbtn_Content1.Content = title1;
                radbtn_Content2.Visibility = Visibility.Collapsed;
                radbtn_Content3.Visibility = Visibility.Collapsed;
                radbtn_Content4.Content = title4;
                txb_Content.Text = txb_content;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void txt_Value_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new System.Text.RegularExpressions.Regex("[^0-9]+").IsMatch(e.Text);

        }
    }
}
