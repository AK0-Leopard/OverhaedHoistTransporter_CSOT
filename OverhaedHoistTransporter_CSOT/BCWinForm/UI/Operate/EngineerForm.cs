using com.mirle.ibg3k0.bc.winform;
using com.mirle.ibg3k0.bc.winform.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class EngineerForm : Form
    {

        public delegate void SegmentSelectedEventHandler(string[] sSeg_ID);
        public event SegmentSelectedEventHandler evtSegmentSelected;
        public delegate void SectionSelectedEventHandler(string[] sSec_ID, string startAdr, string fromAdr, string toAdr);
        public event SectionSelectedEventHandler evtSectionSelected;
        BCMainForm mainForm;
        BCApplication bcApp;
        /// <summary>
        /// This Form Show
        /// </summary>
        public void PrcShow()
        {
            this.BringToFront();
            this.Show();
        }
        /// <summary>
        /// This Form Hide
        /// </summary>
        public void PrcHide()
        {
            this.Hide();
        }
        private int m_iMapSizeW = 0;
        public int p_MapSizeW
        {
            get { return (this.m_iMapSizeW); }
            set { this.m_iMapSizeW = value; }
        }

        private int m_iMapSizeH = 0;
        public int p_MapSizeH
        {
            get { return (this.m_iMapSizeH); }
            set { this.m_iMapSizeH = value; }
        }
        public EngineerForm(BCMainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
            bcApp = mainForm.BCApp;
            string[] allAdr = loadAllAdr();
            cmb_fromAdr.DataSource = allAdr;
            cmb_fromAdr.AutoCompleteCustomSource.AddRange(allAdr);
            cmb_fromAdr.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_fromAdr.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_toAdr.DataSource = allAdr.ToArray();
            cmb_toAdr.AutoCompleteCustomSource.AddRange(allAdr);
            cmb_toAdr.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_toAdr.AutoCompleteSource = AutoCompleteSource.ListItems;

            string[] allSec = bcApp.SCApplication.MapBLL.loadAllSection().Select(sec => sec.SEC_ID).ToArray();
            cmb_fromSection.DataSource = allSec;
            cmb_fromSection.AutoCompleteCustomSource.AddRange(allSec);
            cmb_fromSection.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_fromSection.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void btn_FromAdrToAdr_Click(object sender, EventArgs e)
        {
            string fromAdr = cmb_fromAdr.Text;
            string toAdr = cmb_toAdr.Text;
            string[] Reutrn = bcApp.SCApplication.RouteGuide.DownstreamSearchSection(fromAdr, toAdr, 1);
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(Reutrn[0]))
                sb.AppendLine("SegmentClosed");
            else
            {
                var allRoute = Reutrn[1].Split(';');
                foreach (string route in allRoute)
                    sb.AppendLine(route);
                sb.AppendLine("<MinRoute>");
                sb.AppendLine(Reutrn[0]);
            }
            txt_Route.Text = sb.ToString();

            var minRoute = Reutrn[0].Split('=');
            string[] minRouteSeg = minRoute[0].Split(',');
            bcApp.onTestGuideSectionSearch(minRouteSeg);
        }

        private string[] loadAllAdr()
        {
            string[] allAdrID = null;
            allAdrID = bcApp.SCApplication.MapBLL.loadAllAddress().Select(adr => adr.ADR_ID).ToArray();
            return allAdrID;
        }

       

        private void btn_FromSecToAdr_Click(object sender, EventArgs e)
        {
            string fromSec = cmb_fromSection.Text;
            string toAdr = cmb_toAdr.Text;
            string[] ReutrnVh2FromAdr = bcApp.SCApplication.RouteGuide.DownstreamSearchSection_FromSecToAdr
                    (fromSec, toAdr, 0);
            string svh2FromAdr = (ReutrnVh2FromAdr != null && ReutrnVh2FromAdr.Count() > 0) ? ReutrnVh2FromAdr[0] : string.Empty;
            txt_Route.Text = svh2FromAdr;
            var minRoute = ReutrnVh2FromAdr[0].Split('=');
            string[] minRouteSeg = minRoute[0].Split(',');
            bcApp.onTestGuideSectionSearch(minRouteSeg);

        }

        private void EngineerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.removeForm(typeof(EngineerForm).Name);
        }
    }
}
