using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.bc.winform.App;
using NLog;
using com.mirle.ibg3k0.ohxc.winform.Vo;
using com.mirle.ibg3k0.sc;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_DeviceStatusSignal : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************
        sc.Data.VO.Interface.IConnectionStatusChange connectionStatus;

        public uc_DeviceStatusSignal()
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

        public void start(string title, bool status, sc.Data.VO.Interface.IConnectionStatusChange _connectionStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                connectionStatus = _connectionStatus;
                connectionStatus.ConnectionStatusChange += ConnectionStatus_ConnectionStatusChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void end()
        {
            connectionStatus.ConnectionStatusChange -= ConnectionStatus_ConnectionStatusChange;
        }



        private void ConnectionStatus_ConnectionStatusChange(object sender, bool e)
        {
            isConnection = e;
        }

        private bool isconnection = false;
        public bool isConnection
        {
            get { return isconnection; }
            set
            {
                if (isconnection != value)
                {
                    isconnection = value;
                    pic_Signal_Value.Image = isconnection ?
                        Properties.Resources.Connection_Control_On :
                        Properties.Resources.Connection_Control_Off;
                }
            }
        }
        public Image pImage
        {
            set { pic_Signal_Value.Image = value; }
            get { return pic_Signal_Value.Image; }
        }
    }
}
