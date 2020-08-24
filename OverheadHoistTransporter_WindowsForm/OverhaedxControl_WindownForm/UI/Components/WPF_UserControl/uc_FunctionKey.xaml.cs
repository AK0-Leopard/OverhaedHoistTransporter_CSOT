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
    /// uc_FunctionKey.xaml 的互動邏輯
    /// </summary>
    public partial class uc_FunctionKey : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_FunctionKey()
        {
            InitializeComponent();
        }

        //更新連線狀態(連線/斷線)
        public void SetFuncKey(string title, int picFuncKey)
        {
            try
            {
                txb_FuncKeyName.Text = title;
                if (picFuncKey == 1)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_AutomaticReset.png", UriKind.Relative));
                }
                else if (picFuncKey == 2)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_RestoreElectricity.png", UriKind.Relative));
                }
                else if (picFuncKey == 3)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_SegmentReservation.png", UriKind.Relative));
                }
                else if (picFuncKey == 4)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_ManualCommand.png", UriKind.Relative));
                }
                else if (picFuncKey == 5)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_MCSCommand.png", UriKind.Relative));
                }
                else if (picFuncKey == 6)
                {
                    pic_FuncKey.Source = new BitmapImage(new Uri(@"\Resources\FunctionKeyIcon\FuncKey_MTLMTSMaintenance.png", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
