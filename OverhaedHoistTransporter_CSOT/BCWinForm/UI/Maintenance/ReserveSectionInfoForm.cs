﻿using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.sc;
using Mirle.AK0.Hlt.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class ReserveSectionInfoForm : Form
    {
        BCMainForm form = null;
        BCApplication bcApp = null;
        public ReserveSectionInfoForm(BCMainForm _form)
        {
            InitializeComponent();
            form = _form;
            //uctlReserveSectionView1.Start(form.BCApp);
            bcApp = _form.BCApp;
            uctlReserveSectionView1.Start(bcApp);

            List<string> lstVh = new List<string>();
            lstVh.Add(string.Empty);
            lstVh.AddRange(bcApp.SCApplication.VehicleBLL.loadAllVehicle().
                                                          Select(vh => sc.Common.SCUtility.Trim(vh.VEHICLE_ID, true)).
                                                          ToList());
            string[] allVh = lstVh.ToArray();
            BCUtility.setComboboxDataSource(cmb_vh_ids, allVh);


            List<AADDRESS> allAddress_obj = bcApp.SCApplication.MapBLL.loadAllAddress();
            string[] allAdr_ID = allAddress_obj.Select(adr => adr.ADR_ID).ToArray();
            BCUtility.setComboboxDataSource(cmb_adr_id, allAdr_ID);

            List<ASECTION> sections = bcApp.SCApplication.SectionBLL.cache.GetSections();
            string[] allSec_ID = sections.Select(sec => sec.SEC_ID).ToArray();
            BCUtility.setComboboxDataSource(cmb_reserve_section, allSec_ID.ToArray());

            cmb_fork_dir.DataSource = Enum.GetValues(typeof(HltDirection)).Cast<HltDirection>();
            cmb_sensor_dir.DataSource = Enum.GetValues(typeof(HltDirection)).Cast<HltDirection>();
            cmb_vh_fork_dir.DataSource = Enum.GetValues(typeof(HltDirection)).Cast<HltDirection>();
            cmb_vh_sensor_dir.DataSource = Enum.GetValues(typeof(HltDirection)).Cast<HltDirection>();
            //bcApp.SCApplication.ReserveBLL.ReserveStatusChange += ReserveBLL_ReserveStatusChange;
        }

        private void ReserveBLL_ReserveStatusChange(object sender, EventArgs e)
        {
            uctlReserveSectionView1.RefreshReserveSectionInfo();
        }

        private void ReserveSectionInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bcApp.SCApplication.ReserveBLL.ReserveStatusChange -= ReserveBLL_ReserveStatusChange;
            form.removeForm(nameof(ReserveSectionInfoForm));
            uctlReserveSectionView1.Stop();
        }

        private async void btn_set_vh_Click(object sender, EventArgs e)
        {
            string adr_id = cmb_adr_id.Text;
            string vh_id = cmb_vh_ids.Text;
            var map_address_axis = bcApp.SCApplication.ReserveBLL.GetHltMapAddress(adr_id);

            HltDirection vh_fork_dir;
            Enum.TryParse<HltDirection>(cmb_vh_fork_dir.SelectedValue.ToString(), out vh_fork_dir);
            HltDirection vh_sensor_dir;
            Enum.TryParse<HltDirection>(cmb_vh_sensor_dir.SelectedValue.ToString(), out vh_sensor_dir);

            HltResult result = null;
            await Task.Run(() => result = bcApp.SCApplication.ReserveBLL.TryAddVehicleOrUpdate(vh_id, "", map_address_axis.x, map_address_axis.y, 0, 0, vh_sensor_dir, vh_fork_dir));
            MessageBox.Show(result.ToString());
            RefreshReserveInfo();
        }

        private void RefreshReserveInfo()
        {
            uctlReserveSectionView1.RefreshReserveSectionInfo();
        }

        private async void btn_reserve_section_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_vh_ids.Text;
            string sec_id = cmb_reserve_section.Text.Trim();
            HltDirection fork_dir;
            Enum.TryParse<HltDirection>(cmb_fork_dir.SelectedValue.ToString(), out fork_dir);
            HltDirection sensor_dir;
            Enum.TryParse<HltDirection>(cmb_sensor_dir.SelectedValue.ToString(), out sensor_dir);

            HltResult result = null;
            await Task.Run(() => result = bcApp.SCApplication.ReserveBLL.TryAddReservedSection(vh_id, sec_id, sensor_dir, fork_dir));

            //await Task.Run(() =>
            //{
            //    result = bcApp.SCApplication.ReserveBLL.TryAddReservedSection(vh_id, "0025", sensor_dir, fork_dir);
            //    result = bcApp.SCApplication.ReserveBLL.TryAddReservedSection(vh_id, "0024", sensor_dir, fork_dir);
            //});
            MessageBox.Show(result.ToString());
            RefreshReserveInfo();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show(this, "Do you want to reset current reserve section?",
            BCApplication.getMessageString("CONFIRM"), MessageBoxButtons.YesNo);
            if (confirmResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            await Task.Run(() => bcApp.SCApplication.ReserveBLL.RemoveAllReservedSections());
            bcf.App.BCFApplication.onInfoMsg("Fource release current vehicle reserve status success");
        }

        private async void btn_set_vh_by_axis_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_vh_ids.Text;
            string x_axis = txt_x.Text;
            string y_axis = txt_y.Text;
            float angle = (float)num_vh_angle.Value;
            double.TryParse(x_axis, out double x);
            double.TryParse(y_axis, out double y);
            HltDirection vh_fork_dir;
            Enum.TryParse<HltDirection>(cmb_vh_fork_dir.SelectedValue.ToString(), out vh_fork_dir);
            HltDirection vh_sensor_dir;
            Enum.TryParse<HltDirection>(cmb_vh_sensor_dir.SelectedValue.ToString(), out vh_sensor_dir);

            HltResult result = null;
            await Task.Run(() => result = bcApp.SCApplication.ReserveBLL.TryAddVehicleOrUpdate(vh_id, "", x, y, angle, 0, vh_sensor_dir, vh_fork_dir));
            MessageBox.Show(result.ToString());
        }

        private async void btn_resetReservedSectionByVh_Click(object sender, EventArgs e)
        {
            string vh_id = cmb_vh_ids.Text;
            DialogResult confirmResult = MessageBox.Show(this, $"Do you want to reset vh:{vh_id} current reserved section?",
            BCApplication.getMessageString("CONFIRM"), MessageBoxButtons.YesNo);
            if (confirmResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            await Task.Run(() => bcApp.SCApplication.ReserveBLL.RemoveAllReservedSectionsByVehicleID(vh_id));
            bcf.App.BCFApplication.onInfoMsg($"Fource release current vehicle:{vh_id} reserved status success");
        }
    }
}
