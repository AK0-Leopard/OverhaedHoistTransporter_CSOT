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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_TipMessage
{
    /// <summary>
    /// TipMessage_Type_Light.xaml 的互動邏輯
    /// </summary>
    public partial class TipMessage_Type_Light : UserControl
    {
        static TipMessage_Type_Light TipMsg;
        static System.Windows.Forms.DialogResult confirmResult = System.Windows.Forms.DialogResult.No;
        public event EventHandler CloseFormEvent;

        public TipMessage_Type_Light(String _tipTitle, String _tipText, int _tipType)
        {
            InitializeComponent();

            btn_Close_X.Click += Close_Forms_ClickEvent;
            btn_Ok.Click += Close_Forms_ClickEvent;
        }

        private void Close_Forms_ClickEvent(object sender, RoutedEventArgs e)
        {
            CloseFormEvent?.Invoke(this, e);
        }

        //public static System.Windows.Forms.DialogResult Show(String _tipTitle, String _tipText, int _tipType)
        //{
        //    TipMsg = new TipMessage_Type_Light(_tipTitle, _tipText, _tipType);
        //    TipMsg.txb_tipTitle.Text = _tipTitle.Trim();
        //    TipMsg.txb_tipTitle.Text = _tipText.Trim();

        //}

    }
}
