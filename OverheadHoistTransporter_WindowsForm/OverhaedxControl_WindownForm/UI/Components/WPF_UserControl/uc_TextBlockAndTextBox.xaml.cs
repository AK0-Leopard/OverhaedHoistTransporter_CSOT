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
    /// uc_TextBlockAndTextBox.xaml 的互動邏輯
    /// </summary>
    public partial class uc_TextBlockAndTextBox : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public uc_TextBlockAndTextBox()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_Content, false); //設置IME和輸入是否可以是中文
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

        // 判断字符是否为字母或数字
        Boolean IsNumOrLetter(String str)
        {
            char[] tmpCharArray = str.ToCharArray();
            if (
            ((tmpCharArray[0] >= 'A') && (tmpCharArray[0] <= 'z'))
            || ((tmpCharArray[0] >= 'a') && (tmpCharArray[0] <= 'z'))
            || ((tmpCharArray[0] >= '0') && (tmpCharArray[0] < '9'))
            )
            { return true; }
            else
            { return false; }
        }

        private void txt_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txt_Content.Text.Length; i++)
            {
                string tmpStr = txt_Content.Text.Substring(i, 1);
                if (IsNumOrLetter(tmpStr) == false)
                {
                    txt_Content.Text = txt_Content.Text.Remove(i, 1);
                    txt_Content.SelectionStart = txt_Content.Text.Length;
                }
            }
        }
    }
}
