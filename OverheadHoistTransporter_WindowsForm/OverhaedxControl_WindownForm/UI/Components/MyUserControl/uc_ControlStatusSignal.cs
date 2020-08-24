using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;


namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_ControlStatusSignal : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_ControlStatusSignal()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        //更新連線狀態(連線/斷線)
        public void SetConnStatus(string title, bool ConnectionStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                pic_Signal_Value.Image = ConnectionStatus ?
                    Properties.Resources.Control_status_LinkOk :
                    Properties.Resources.Control_status_LinkFail;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public Image pImage
        {
            set { pic_Signal_Value.Image = value; }
            get { return pic_Signal_Value.Image; }
        }
    }
}
