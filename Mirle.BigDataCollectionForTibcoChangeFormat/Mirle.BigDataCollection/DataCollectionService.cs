using Config.Net;
using Mirle.BigDataCollection.DataCollection;
using Mirle.BigDataCollection.Define;
using Mirle.LCS.FFUC;
using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.DataBlocks.DeviceRange;
using Mirle.MPLC.DataType;
using Mirle.MPLC.SharedMemory;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Mirle.BigDataCollection
{
    public partial class DataCollectionService : Form
    {
        private SMReadWriter smReader;
        private string _deviceID;
        private int _maxMplcAddr;
        private DataCollectionManager manager;
        private string fileVersionInfo;

        public DataCollectionService()
        {
            InitializeComponent();

            DataCollectionINI dataCollectionINI = new ConfigurationBuilder<DataCollectionINI>().UseIniFile(@"Config\DataCollection.ini").Build();
            _deviceID = dataCollectionINI.STKC.DeviceID;
            _maxMplcAddr = dataCollectionINI.STKC.FFUMaxEndAddr;

            fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            this.Text += $"({fileVersionInfo})";

            SMReadWriterInital();
        }

        private void SMReadWriterInital()
        {
            smReader = new SMReadWriter();
            smReader.AddDataBlock(new SMDataBlockInt32(new DDeviceRange("D5000", $"D{_maxMplcAddr}"), $@"Global\{_deviceID}-WordData"));
        }

        public void MeaageSaveToXml(MESSAGE Message, string pathe)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer ser = new XmlSerializer(typeof(MESSAGE));

            using (StreamWriter writer = new StreamWriter(@pathe, false, Encoding.UTF8))
            {
                ser.Serialize(writer, Message, ns);
            }
        }

        private void DataCollectionService_Load(object sender, EventArgs e)
        {
            try
            {
                manager = new DataCollectionManager(new LoggerService(_deviceID), smReader);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                Dispose();
                Close();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void tspClose_Click(object sender, EventArgs e)
        {
            InputPassword inputPassword = new InputPassword(this, false);
            inputPassword.Show();
        }

        public void AppClose()
        {
            try
            {
                manager._loggerService.WriteLog("Trace", "DataCollectionManager tspClose_Click.");
                Dispose();
                Close();
                Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            { Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}"); }
        }

        private void DataCollectionService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
                e.Handled = true;
        }

        private void DataCollectionService_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void DataCollectionService_Shown(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //smReader.WriteWord(txtAddr.Text, Convert.ToInt32(txtValue.Text));
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var result = new Word(smReader, txtGetValue.Text);
            label2.Text = result.GetValue().ToString();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            string m = "";
            try
            {
                using (var client = new TcpClient(txtModBusIP.Text, int.Parse(txtPort.Text)))
                {
                    m += $"TcpClient Succ {Environment.NewLine}";

                    Task.Delay(2000).Wait();

                    var _ffuc = new FFUController(txtModBusIP.Text, int.Parse(txtPort.Text), false);
                    _ffuc.EnableCache = false;
                    _ffuc.RefreshInterval = 2000;
                    _ffuc.Start();
                    txtModbus.Text = $"{m}";

                    Task.Delay(10000).Wait();

                    m += $"_ffuc {_ffuc.IsConnected} {Environment.NewLine}";
                    txtModbus.Text = $"{m}";

                    if (_ffuc.IsConnected)
                    {
                        var groupAndFFUNo = txtGroupAndFFUNo.Text.Split(',');
                        var ffuGroup = _ffuc.GetFFUGroupByNumber(int.Parse(groupAndFFUNo[0]));
                        var ffuNo = ffuGroup.GetFFUByNumber(int.Parse(groupAndFFUNo[1]));

                        m += $"_ffu {JsonConvert.SerializeObject(ffuNo, Formatting.Indented)}";
                    }

                    _ffuc.Pause();
                    Task.Delay(3000).Wait();
                    _ffuc.Dispose();
                    _ffuc = null;
                }
            }
            catch (Exception ex)
            {
                m += $"{ex}\n";
                manager._loggerService.WriteException(MethodBase.GetCurrentMethod().ToString(), ex.ToString());
            }
            txtModbus.Text = $"{m}";
        }

        private void tmiShowDebug_Click(object sender, EventArgs e)
        {
            InputPassword inputPassword = new InputPassword(this, true);
            inputPassword.Show();
        }

        private void DataCollectionService_Deactivate(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
            }
        }

        private void rdCSV_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbCSV.Text))
            {
                DataCollection.Data.OhtRawData.OpenCSV(tbCSV.Text);
            }
        }
    }
}