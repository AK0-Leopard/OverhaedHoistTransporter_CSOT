using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class BufferPortMaintenanceForm : Form
    {
        const int PORT_STATUS_G = 1;
        const int PORT_STATUS_Y = 2;
        const int PORT_STATUS_R = 3;

        BCMainForm MainForm;
        SCApplication scApp;

        public BufferPortMaintenanceForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            MainForm = _mainForm;
            scApp = _mainForm.BCApp.SCApplication;

            ceratMainTreeNode();
        }

        private void ceratMainTreeNode()
        {
            treeView.Nodes.Clear();
            var eq_buffers = scApp.EquipmentBLL.cache.loadBufferDevices();
            foreach (var buf_device in eq_buffers)
            {
                TreeNode device = new TreeNode()
                {
                    Name = buf_device.EQPT_ID,
                    Text = buf_device.EQPT_ID
                };
                var port_stations = buf_device.loadPortStations(scApp.PortStationBLL);
                foreach (var port_station in port_stations)
                {
                    string status = port_station.IsService(scApp.EquipmentBLL) ? "Enable" : "Disable";
                    TreeNode node = new TreeNode()
                    {
                        Name = port_station.PORT_ID,
                        Text = $"{port_station.PORT_ID}({port_station.ADR_ID}):{status}",
                        Tag = port_station,
                        ImageIndex = getPortStatus2ImageIndex(port_station)

                    };
                    device.Nodes.Add(node);
                }
                treeView.Nodes.Add(device);
            }
        }
        public void refresh()
        {
            var eq_buffers = scApp.EquipmentBLL.cache.loadBufferDevices();
            foreach (var buf_device in eq_buffers)
            {
                string eq_id = buf_device.EQPT_ID;
                var eq_node = treeView.Nodes[eq_id];
                foreach (var node_obj in eq_node.Nodes)
                {
                    TreeNode node = node_obj as TreeNode;
                    var port_station = node.Tag as sc.APORTSTATION;
                    string status = port_station.IsService(scApp.EquipmentBLL) ? "Enable" : "Disable";
                    node.Text = $"{port_station.PORT_ID}({port_station.ADR_ID}):{status}";
                    node.ImageIndex = getPortStatus2ImageIndex(port_station);
                }
            }
        }
        private int getPortStatus2ImageIndex(sc.APORTSTATION port)
        {
            switch (port.PORT_STATUS)
            {
                case E_PORT_STATUS.InService:
                    switch (port.PORT_SERVICE_STATUS)
                    {
                        case sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.InService:
                            return PORT_STATUS_G;
                        case sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.OutOfService:
                            return PORT_STATUS_R;
                    }
                    break;
                case E_PORT_STATUS.OutOfService:
                    switch (port.PORT_SERVICE_STATUS)
                    {
                        case sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.InService:
                            return PORT_STATUS_Y;
                        case sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.OutOfService:
                            return PORT_STATUS_R;
                    }
                    break;
            }
            return PORT_STATUS_R;
        }

        private void CarrierMaintenanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.removeForm(this.Name);
        }

        private async void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                contextMenuStrip1.Enabled = false;
                TreeNode selectTree = treeView.SelectedNode;
                if (selectTree == null) return;
                if (selectTree.Level != 1) return;
                string port_id = selectTree.Name;
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(BufferPortMaintenanceForm), Device: "OHTC",
                  Data: $"Try to enable buffer port:{port_id}...");
                bool is_success = await EnableDisablePortStation(selectTree, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.InService);
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(BufferPortMaintenanceForm), Device: "OHTC",
                  Data: $"Try to enable buffer port:{port_id},Is success:{is_success}");
                refresh();
                if (is_success)
                    MessageBox.Show($"Buffer port:{port_id} enable is success.", "Buffer port", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Buffer port:{port_id} enable is fail.", "Buffer port", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception");
            }
            finally
            {
                contextMenuStrip1.Enabled = true;
            }

        }

        private async void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                contextMenuStrip1.Enabled = false;
                TreeNode selectTree = treeView.SelectedNode;
                if (selectTree == null) return;
                if (selectTree.Level != 1) return;
                string port_id = selectTree.Name;
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(BufferPortMaintenanceForm), Device: "OHTC",
                  Data: $"Try to disabel buffer port:{port_id}...");
                bool is_success = await EnableDisablePortStation(selectTree, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus.OutOfService);
                Common.LogHelper.Log(logger: NLog.LogManager.GetCurrentClassLogger(), LogLevel: LogLevel.Info, Class: nameof(BufferPortMaintenanceForm), Device: "OHTC",
                  Data: $"Try to disabel buffer port:{port_id},Is success:{is_success}");
                refresh();
                if (is_success)
                    MessageBox.Show($"Buffer port:{port_id} disable is success.", "Buffer port", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Buffer port:{port_id} disable is fail.", "Buffer port", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception");
            }
            finally
            {
                contextMenuStrip1.Enabled = true;
            }
        }
        private Task<bool> EnableDisablePortStation(TreeNode selectTree, sc.ProtocolFormat.OHTMessage.PortStationServiceStatus status)
        {
            if (selectTree == null) return (Task<bool>)Task.CompletedTask;
            if (selectTree.Level != 1) return (Task<bool>)Task.CompletedTask;
            string port_id = selectTree.Name;

            return Task.Run(() => scApp.PortStationService.doUpdatePortStationServiceStatus(port_id, status));
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
