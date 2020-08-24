using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.bcf.Common;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    public partial class uctlHistoricalReplyPlayer : UserControl
    {
        public EventHandler<string> FocusVehicle;
        WindownApplication app;
        HistoricalReplyService HistoricalReply;
        List<ListBoxItem> AllListBoxItems;
        public uctlHistoricalReplyPlayer()
        {
            InitializeComponent();
        }

        const string KEY_WORD_ALL = "ALL";
        public void Start()
        {
            app = WindownApplication.getInstance();
            HistoricalReply = app.GetHistoricalReplyService();
            // HistoricalReply.loadVhHistoricalInfo();

            HistoricalReply.PlayScheduleChanged += PlayScheduleChangeHandle;
            HistoricalReply.LoadComplete += setListBox;

            var vh_ids = app.ObjCacheManager.GetVEHICLEs().Select(vh => vh.VEHICLE_ID);
            cmb_vh_id.Items.Add(KEY_WORD_ALL);
            cmb_vh_id.Items.AddRange(vh_ids.ToArray());
        }

        private void setListBox(object obj, Dictionary<DateTime, List<Vo.VehicleHistoricalInfo>> changed_index)
        {
            AllListBoxItems = new List<ListBoxItem>();

            int index = 0;
            foreach (var data in HistoricalReply.Vh_Historical_Info_GroupByTime.ToList())
            {
                AllListBoxItems.Add(new ListBoxItem(index++, data));
            }
            Adapter.Invoke((o) =>
            {

                cmb_vh_id.SelectedIndex = 0;
            }, null);

            //SetListBox(AllListBoxItems);
        }

        private void SetListBox(List<ListBoxItem> listBoxItems)
        {
            Adapter.Invoke((o) =>
            {
                listBox1.DisplayMember = "sTime";
                listBox1.ValueMember = "DateTime";
                listBox1.DataSource = listBoxItems;

            }, null);
        }

        class ListBoxItem
        {
            public ListBoxItem(int index, KeyValuePair<DateTime, List<Vo.VehicleHistoricalInfo>> _data)
            {
                Data = _data;
                VhIDs = _data.Value.Select(info => info.ID).ToArray();
                DateTime = _data.Key;
                Index = index;
            }
            public KeyValuePair<DateTime, List<Vo.VehicleHistoricalInfo>> Data;
            public string sTime { get { return Data.Key.ToString(sc.App.SCAppConstants.DateTimeFormat_23); } }
            public DateTime DateTime;
            public string[] VhIDs;
            public int Index;
        }

        private void btn_loaddata_Click(object sender, EventArgs e)
        {
            Task.Run(() => HistoricalReply.Play());
        }

        private void PlayScheduleChangeHandle(object obj, int changed_index)
        {
            Adapter.Invoke((o) =>
            {
                listBox1.SelectedIndex = changed_index;
            }, null);

        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            HistoricalReply.Pause();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;
            HistoricalReply.setStartIndex(listbox.SelectedIndex);
        }

        private void btn_ctu_Click(object sender, EventArgs e)
        {
            HistoricalReply.Continue();
        }

        private void btn_stop1_Click(object sender, EventArgs e)
        {
            HistoricalReply.Stop();
        }

        private void btn_playRate_1_Click(object sender, EventArgs e)
        {
            HistoricalReply.setPalyRate(1);
        }

        private void btn_playRate_2_Click(object sender, EventArgs e)
        {
            HistoricalReply.setPalyRate(2);
        }

        private void btn_playRate_4_Click(object sender, EventArgs e)
        {
            HistoricalReply.setPalyRate(4);
        }

        private void btn_playRate_8_Click(object sender, EventArgs e)
        {
            HistoricalReply.setPalyRate(8);
        }

        private async void btn_StartSearch_Click(object sender, EventArgs e)
        {
            DateTime dateTimeFrom = m_StartDTCbx.Value;
            DateTime dateTimeTo = m_EndDTCbx.Value;
            dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
            dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
            tableLayoutPanel1.Enabled = false;
            await Task.Run(() => HistoricalReply.loadVhHistoricalInfo(dateTimeFrom, dateTimeTo));
            tableLayoutPanel1.Enabled = true;
        }

        private void cmb_vh_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo == null) return;
            List<ListBoxItem> filterlistBoxItems;
            string select_vh_id = combo.Text;
            if (BCFUtility.isMatche(KEY_WORD_ALL, select_vh_id))
            {
                filterlistBoxItems = AllListBoxItems;
            }
            else
            {
                filterlistBoxItems = new List<ListBoxItem>();
                foreach (var item in AllListBoxItems)
                {
                    if (item.VhIDs.Contains(select_vh_id))
                    {
                        filterlistBoxItems.Add(item);
                    }
                }
            }
            HistoricalReply.setPlayInfo(select_vh_id, filterlistBoxItems.Select(item => item.Index).ToArray());
            SetListBox(filterlistBoxItems);
            FocusVehicle?.Invoke(this, select_vh_id);
        }
    }
}
