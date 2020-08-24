using com.mirle.ibg3k0.bc.winform.UI;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverhaedxControl_WindownForm
{
    public partial class MainForm : Form
    {
        OHT_Form oHT_Form = null;
        public WindownApplication app { get; private set; } = null;
        public MainForm()
        {
            InitializeComponent();
            Adapter.Initialize();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            app.GetNatsManager()?.close();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() => app = WindownApplication.getInstance());

            oHT_Form = new OHT_Form(this);
            oHT_Form.MdiParent = this;
            oHT_Form.Show();
            oHT_Form.Focus();
            oHT_Form.AutoScroll = true;
            oHT_Form.WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }
        VehicleDataSettingForm VehicleDataSettingForm;
        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VehicleDataSettingForm == null || VehicleDataSettingForm.IsDisposed)
            {
                VehicleDataSettingForm = new VehicleDataSettingForm();
                VehicleDataSettingForm.Show();
            }
            else
            {
                VehicleDataSettingForm.Show();
            }
        }

        private void cSTInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSTInterfaceOverview_Form.getInstance().Show();
        }

        private void systemLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogQueryForm.getInstance().Show();
        }
    }
}
