using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ObjectRelay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class HistoryTransferForm : Form
    {
        BCMainForm mainform;
        SCApplication scApp = null;
        BindingSource cmsMCS_bindingSource = new BindingSource();
        List<CMDObjToShow> cmdShowList = null;
        int selection_index = -1;
        string[] portsource;
        string[] portdestination;

        public HistoryTransferForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            dgv_TransferCommandList.AutoGenerateColumns = false;
            mainform = _mainForm;
            scApp = mainform.BCApp.SCApplication;
            dgv_TransferCommandList.DataSource = cmsMCS_bindingSource;

            m_StartDTCbx.Value = DateTime.Today;
            m_EndDTCbx.Value = DateTime.Now;
            //loadPortID();
            portsource = scApp.MapBLL.loadAllPort().Select(s => s.PORT_ID).ToArray();
            portdestination = scApp.MapBLL.loadAllPort().Select(s => s.PORT_ID).ToArray();
            BCUtility.setComboboxDataSource(m_SourceIDCbx, portsource);
            BCUtility.setComboboxDataSource(m_DestinationIDCbx, portdestination);

        }

        private void updateCmds()
        {
            DateTime start_time = m_StartDTCbx.Value;
            DateTime end_time = m_EndDTCbx.Value;
            TimeSpan timeSpan = end_time - start_time;
            if (end_time < start_time)
            {
                MessageBox.Show("The end date cannot be earlier than the start date!");
                m_StartDTCbx.Value = DateTime.Today;
                m_EndDTCbx.Value = DateTime.Now;
                return;
            }
            if (timeSpan.TotalDays > 7)
            {
                MessageBox.Show("The difference between the two dates exceeds one week!");
                m_StartDTCbx.Value = DateTime.Today;
                m_EndDTCbx.Value = DateTime.Now;
                return;
            }

            var cmds = mainform.BCApp.SCApplication.CMDBLL.GetCmds(start_time, end_time);
            if (cmds != null && cmds.Count > 0)
            {
                string cmd_id = m_AlarmCodeTbl.Text;
                string source_id = m_SourceIDCbx.Text;
                string destination_id = m_DestinationIDCbx.Text;
                if (!SCUtility.isEmpty(cmd_id))
                {
                    cmds = cmds.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, cmd_id)).ToList();
                }
                if (!SCUtility.isEmpty(source_id))
                {
                    cmds = cmds.Where(cmd => SCUtility.isMatche(cmd.HOSTSOURCE, source_id)).ToList();
                }
                if (!SCUtility.isEmpty(destination_id))
                {
                    cmds = cmds.Where(cmd => SCUtility.isMatche(cmd.HOSTDESTINATION, destination_id)).ToList();
                }
                cmdShowList = cmds.Select(cmd => new CMDObjToShow(cmd)).ToList();
                cmsMCS_bindingSource.DataSource = cmdShowList;
                dgv_TransferCommandList.Refresh();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
        }

        private void btnlSearch_Click_1(object sender, EventArgs e)
        {
            selection_index = -1;
            updateCmds();
        }

        private void HistoryTransferForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainform.removeForm(this.Name);
        }

        private void m_SourceIDCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void m_DestinationIDCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
