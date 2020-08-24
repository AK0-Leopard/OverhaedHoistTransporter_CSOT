//*********************************************************************************
//      uc_SP_PortMaint.cs
//*********************************************************************************
// File Name: uc_SP_PortMaint.cs
// Description: Port Maintenance
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_PortMaint.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_PortMaint : UserControl
    {
        #region 公用參數設定
        public event EventHandler CloseFormEvent;
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        public event EventHandler<PortPriorityUpdateEventArgs> PortPriorityUpdate;
        public event EventHandler<PortSerivceStatusUpdateEventArgs> PortEnableDisable;

        List<APORTSTATION> portStations = null;
        List<VACMD_MCS> mcsCommands = null;

        #endregion 公用參數設定

        public uc_SP_PortMaint()
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

        public void startupUI()
        {
            try
            {
                this.Width = 1711;
                this.Height = 754;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void AllCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox headercb = (CheckBox)sender;

        //    for (int i = 0; i < eqPortList.Items.Count; i++)
        //    {
        //        DataGridRow neddrow = (DataGridRow)eqPortList.ItemContainerGenerator.ContainerFromIndex(i);
        //        CheckBox cb = (CheckBox)eqPortList.Columns[0].GetCellContent(neddrow);
        //        cb.IsChecked = headercb.IsChecked;
        //    }
        //}

        public void initUI()
        {
            try
            {
                start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start()
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                start(app.ObjCacheManager.GetPortStations(), app.ObjCacheManager.GetMCS_CMD());
                registerEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void registerEvent()
        {
            try
            {
                PortPriorityUpdate += Uc_SP_PortMaint1_PortPriorityUpdate;
                PortEnableDisable += Uc_SP_PortMaint1_PortEnableDisable;
                app.ObjCacheManager.PortStationUpdateComplete += ObjCacheManager_PortStationUpdateComplete; ;
                //app.ObjCacheManager.MCSCommandUpdateComplete += ObjCacheManager_PortStationUpdateComplete; ;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void unRegisterEvent()
        {
            try
            {
                PortPriorityUpdate -= Uc_SP_PortMaint1_PortPriorityUpdate;
                PortEnableDisable -= Uc_SP_PortMaint1_PortEnableDisable;
                app.ObjCacheManager.PortStationUpdateComplete -= ObjCacheManager_PortStationUpdateComplete; ;
                //app.ObjCacheManager.MCSCommandUpdateComplete -= ObjCacheManager_PortStationUpdateComplete; ;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_PortStationUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PortMaint1_PortEnableDisable(object sender, ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint.PortSerivceStatusUpdateEventArgs e)
        {
            try
            {
                app.PortStationBLL.SendPortStatusChange(e.port_id, e.status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_PortMaint1_PortPriorityUpdate(object sender, ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint.PortPriorityUpdateEventArgs e)
        {
            try
            {
                app.PortStationBLL.SendPortPriorityUpdate(e.port_id, e.priority);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start(List<APORTSTATION> _portStations, List<VACMD_MCS> _mcsCommands)
        {
            try
            {
                portStations = _portStations;
                mcsCommands = _mcsCommands;
                allPortList.ItemsSource = this.portStations;

                var all_port_station_view_obj = this.portStations.Select(port_station => new PortStationViewObj(port_station, _mcsCommands));
                allPortList.ItemsSource = all_port_station_view_obj;
                //var eqpt_port_station_view_obj = this.portStations.Where(port_station => port_station.PORT_TYPE == (int)sc.App.SCAppConstants.EqptType.Equipment)
                //                                         .Select(port_station => new PortStationViewObj(port_station));
                //eqPortList.ItemsSource = eqpt_port_station_view_obj;

                cmdPortID.ItemsSource = all_port_station_view_obj;
                //cmdEqPortID.ItemsSource = eqpt_port_station_view_obj;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void Refresh()
        {
            try
            {
                bcf.Common.Adapter.Invoke((obj) =>
                {
                    allPortList.Items.Refresh();
                    //eqPortList.Items.Refresh();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void setAllPortList(List<APORTSTATION> items_source)
        {
            try
            {
                portStations = items_source;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
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
            try
            {
                if (sender.Equals(btn_set))
                {
                    string port_id = cmdPortID.Text;
                    int priority = (int)numPrority.Value;
                    PortPriorityUpdate?.Invoke(this, new PortPriorityUpdateEventArgs(port_id, priority));
                }
                //else if (sender.Equals(btn_enable))
                //{
                //    string port_id = cmdEqPortID.Text;
                //    PortEnableDisable?.Invoke(this, new PortSerivceStatusUpdateEventArgs(port_id, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.InService));
                //}
                //for (int i = eqPortList.Items.Count - 1; i >= 0; i--)
                //{
                //    bool chk = (bool)eqPortList.Rows[i].Cells[3].Value;
                //    if (chk == true)
                //    {
                //        System.Windows.Forms.DataGridViewRow drow = eqPortList.Rows[i];
                //        eqPortList.Rows.Remove(drow);
                //    }
                //}
                //    string port_id = cmdEqPortID.Text;
                //    PortEnableDisable?.Invoke(this, new PortSerivceStatusUpdateEventArgs(port_id, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.InService));
                //}
                //else if (sender.Equals(btn_disable))
                //{
                //    string port_id = cmdEqPortID.Text;
                //    PortEnableDisable?.Invoke(this, new PortSerivceStatusUpdateEventArgs(port_id, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.OutOfService));
                //}
                //else if (sender.Equals(btn_loadrequest))
                //{
                //    MessageBox.Show("btn_loadrequest");
                //}
                //else if (sender.Equals(btn_unloadrequest))
                //{
                //    MessageBox.Show("btn_unloadrequest");
                //}
                //else if (sender.Equals(btn_wait))
                //{
                //    MessageBox.Show("btn_wait");
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void eqPortList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    PortStationViewObj port = (PortStationViewObj)eqPortList.SelectedItem;
        //    if (port != null)
        //    {
        //        cmdEqPortID.Text = port.PORT_ID;
        //    }
        //}

        private void allPortList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                PortStationViewObj port = (PortStationViewObj)allPortList.SelectedItem;
                if (port != null)
                {
                    cmdPortID.Text = port.PORT_ID;
                    numPrority.Value = port.PRIORITY;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public class PortPriorityUpdateEventArgs : EventArgs
        {
            public PortPriorityUpdateEventArgs(string portID, int _priority)
            {
                port_id = portID;
                priority = _priority;
            }

            public string port_id { get; private set; }
            public int priority { get; private set; }
        }
        public class PortSerivceStatusUpdateEventArgs : EventArgs
        {
            public PortSerivceStatusUpdateEventArgs(string portID, com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage.PortStationServiceStatus _status)
            {
                port_id = portID;
                status = _status;
            }

            public string port_id { get; private set; }
            public com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage.PortStationServiceStatus status { get; private set; }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CloseFormEvent?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }

    public class EnumConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.
        ///                 </param><param name="targetType">The type of the binding target property.
        ///                 </param><param name="parameter">The converter parameter to use.
        ///                 </param><param name="culture">The culture to use in the converter.
        ///                 </param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((sc.App.SCAppConstants.EqptType)value).ToString();
        }
        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.
        ///                 </param><param name="targetType">The type to convert to.
        ///                 </param><param name="parameter">The converter parameter to use.
        ///                 </param><param name="culture">The culture to use in the converter.
        ///                 </param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion
    }


}
