using com.mirle.ibg3k0.ohxc.winform;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class ReserveSectionInfoForm : Form
    {
        OHxCMainForm mainForm = null;
        public ReserveSectionInfoForm()
        {
            InitializeComponent();
        }
        public ReserveSectionInfoForm(OHxCMainForm _mainForm)
        {
            try
            {
                InitializeComponent();
                mainForm = _mainForm;
                uctlReserveSectionView1.Start(mainForm.app);

            }
            catch (Exception ex)
            {
            }
        }

        private void ReserveBLL_ReserveStatusChange(object sender, EventArgs e)
        {
            uctlReserveSectionView1.RefreshReserveSectionInfo();
        }

        private void ReserveSectionInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            uctlReserveSectionView1.Stop();
            mainForm.removeForm(typeof(ReserveSectionInfoForm).Name);
        }

        private async void btn_set_vh_Click(object sender, EventArgs e)
        {

        }

        private void RefreshReserveInfo()
        {
            uctlReserveSectionView1.RefreshReserveSectionInfo();
        }

        private async void btn_reserve_section_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
        }

        private async void btn_set_vh_by_axis_Click(object sender, EventArgs e)
        {

        }

        private async void btn_resetReservedSectionByVh_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_vh_ids.Text;

        }
    }
}
