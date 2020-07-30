using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.mirle.iibg3k0.ttc.Common;
using com.mirle.iibg3k0.ttc.Common.TCPIP;
using Google.Protobuf.Collections;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;

namespace Mirle.Agvc.Simulator
{
    public partial class Form1 : Form
    {

        public ConfigHandler configHandler;
        public MiddleAgent middleAgent;
        public MiddlerConfigs middlerConfigs;
        public int RichTextBoxMaxLines;
        public XmlHandler AXmlHandler;
        public Dictionary<EnumAddress, LoadunloadCmd> CmdsFromA = new Dictionary<EnumAddress, LoadunloadCmd>();
        public Dictionary<EnumAddress, LoadunloadCmd> CmdsFromB = new Dictionary<EnumAddress, LoadunloadCmd>();
        public Dictionary<EnumAddress, LoadunloadCmd> CmdsFromG = new Dictionary<EnumAddress, LoadunloadCmd>();
        public EnumAddress CurAddr { get; set; } = EnumAddress.A;
        public EnumAddress TempAddr { get; set; } = EnumAddress.A;
        public Random random = new Random();
        public bool MoveEndFlag { get; set; } = false;
        public bool IsInSitu { get; set; } = false;
        public Stopwatch sw = new Stopwatch();
        public string DebugLogMsg { get; set; } = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfigs();
            middleAgent = new MiddleAgent(middlerConfigs);
            middleAgent.OnCmdSend += MiddleAgent_OnCmdSendOrReceive;
            middleAgent.OnCmdReceive += MiddleAgent_OnCmdSendOrReceive;
            middleAgent.OnConnected += MiddleAgent_OnConnected;
            middleAgent.OnDisConnected += MiddleAgent_OnDisConnected;
            middleAgent.AForm = this;

            ConfigToUI();

            LoadXmls();

            txtReserveSectionId.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "系統時間: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
            //RenewCurAddrToGbLocationRbtns(); // jasonwu 20200115--
            txtDebugLogMsg.Text = DebugLogMsg;
            //Task.Run(() =>
            //{
            //    ServerClientAgent.start();
            //});
        }

        private void LoadXmls()
        {
            AXmlHandler = new XmlHandler();
            LoadunloadCmd aCmd = new LoadunloadCmd();
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("AtoA.xml");
            CmdsFromA.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("AtoB.xml");
            CmdsFromA.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("AtoG.xml");
            CmdsFromA.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("BtoB.xml");
            CmdsFromB.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("BtoA.xml");
            CmdsFromB.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("BtoG.xml");
            CmdsFromB.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("GtoG.xml");
            CmdsFromG.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("GtoA.xml");
            CmdsFromG.Add(aCmd.EndAddress, aCmd);
            aCmd = AXmlHandler.ReadXml<LoadunloadCmd>("GtoB.xml");
            CmdsFromG.Add(aCmd.EndAddress, aCmd);
        }

        public delegate void ToolStripStatusLabelTextChangeCallback(ToolStripStatusLabel toolStripStatusLabel, string msg);
        public void ToolStripStatusLabelTextChange(ToolStripStatusLabel toolStripStatusLabel, string msg)
        {
            if (this.InvokeRequired)
            {
                ToolStripStatusLabelTextChangeCallback mydel = new ToolStripStatusLabelTextChangeCallback(ToolStripStatusLabelTextChange);
                this.Invoke(mydel, new object[] { toolStripStatusLabel, msg });
            }
            else
            {
                toolStripStatusLabel.Text = msg;
            }
        }
        private void MiddleAgent_OnDisConnected(object sender, string e)
        {
            ToolStripStatusLabelTextChange(toolStripStatusLabel1, e);
        }
        private void MiddleAgent_OnConnected(object sender, string e)
        {
            ToolStripStatusLabelTextChange(toolStripStatusLabel1, e);
        }

        private void ConfigToUI()
        {
            txtIp.Text = middlerConfigs.RemoteIp;
            txtPort.Text = middlerConfigs.RemotePort.ToString();

            foreach (var item in Enum.GetNames(typeof(EnumAddress)))
            {
                string xxx = item;
                cbLeft.Items.Add(item);
                cbRight.Items.Add(item);
            }
        }

        private void AppendDebugLogMsg(string msg)
        {
            try
            {
                DebugLogMsg = string.Concat(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), "\t", msg, "\r\n", DebugLogMsg);

                if (DebugLogMsg.Length > 65535)
                {
                    DebugLogMsg = DebugLogMsg.Substring(65535);
                }
            }
            catch (Exception ex)
            {
                LoggerAgent.Instance.LogMsg("Error", new LogFormat("Error", "5", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID", ex.StackTrace));
            }
        }

        private void MiddleAgent_OnCmdSendOrReceive(object sender, string msg)
        {
            AppendDebugLogMsg(msg);
            LoggerAgent.Instance.LogMsg("Debug", new LogFormat("Debug", "5", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID", msg));

        }

        private void LoadConfigs()
        {
            string iniPath = Path.Combine(Environment.CurrentDirectory, "Configs.ini");
            configHandler = new ConfigHandler(iniPath);

            LoggerAgent.LogConfigPath = configHandler.GetString("MainFlow", "LogConfigPath", "Log.ini");
            int.TryParse(configHandler.GetString("MainFlow", "RichTextBoxMaxLines", "15"), out int tempRichTextBoxMaxLines);
            RichTextBoxMaxLines = tempRichTextBoxMaxLines;

            middlerConfigs = new MiddlerConfigs();

            int.TryParse(configHandler.GetString("Middler", "ClientNum", "1"), out int tempClientNum);
            middlerConfigs.ClientNum = tempClientNum;

            middlerConfigs.ClientName = configHandler.GetString("Middler", "ClientName", "AGV01");

            middlerConfigs.RemoteIp = configHandler.GetString("Middler", "RemoteIp", "127.0.0.1");

            int.TryParse(configHandler.GetString("Middler", "RemotePort", "10001"), out int tempRemotePort);
            middlerConfigs.RemotePort = tempRemotePort;

            middlerConfigs.LocalIp = configHandler.GetString("Middler", "LocalIp", "127.0.0.1");

            int.TryParse(configHandler.GetString("Middler", "LocalPort", "5002"), out int tempPort);
            middlerConfigs.LocalPort = tempPort;

            int.TryParse(configHandler.GetString("Middler", "RecvTimeoutMs", "10000"), out int tempRecvTimeoutMs);
            middlerConfigs.RecvTimeoutMs = tempRecvTimeoutMs;

            int.TryParse(configHandler.GetString("Middler", "SendTimeoutMs", "0"), out int tempSendTimeoutMs);
            middlerConfigs.SendTimeoutMs = tempSendTimeoutMs;

            int.TryParse(configHandler.GetString("Middler", "MaxReadSize", "0"), out int tempMaxReadSize);
            middlerConfigs.MaxReadSize = tempMaxReadSize;

            int.TryParse(configHandler.GetString("Middler", "ReconnectionIntervalMs", "10000"), out int tempReconnectionIntervalMs);
            middlerConfigs.ReconnectionIntervalMs = tempReconnectionIntervalMs;

            int.TryParse(configHandler.GetString("Middler", "MaxReconnectionCount", "10"), out int tempMaxReconnectionCount);
            middlerConfigs.MaxReconnectionCount = tempMaxReconnectionCount;

            int.TryParse(configHandler.GetString("Middler", "RetryCount", "2"), out int tempRetryCount);
            middlerConfigs.RetryCount = tempRetryCount;

            int.TryParse(configHandler.GetString("Middler", "SleepTime", "10"), out int tempSleepTime);
            middlerConfigs.SleepTime = tempSleepTime;

            middlerConfigs.IsServer = bool.Parse(configHandler.GetString("Middler", "IsServer ", "false"));

            middlerConfigs.CycleRunIntervalMs = int.Parse(configHandler.GetString("Middler", "CycleRunIntervalMs ", "4000"));

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int numOfCmdItems = dataGridView1.Rows.Count - 1;
            if (numOfCmdItems < 0)
            {
                return;
            }
            int cmdNum = int.Parse(cbSend.Text.Split('_')[0].Substring(3));
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            for (int i = 0; i < numOfCmdItems; i++)
            {
                var row = dataGridView1.Rows[i];
                var pairName = row.Cells[0].Value.ToString();
                var pairValue = row.Cells[1].Value.ToString();

                if (!string.IsNullOrWhiteSpace(pairName))
                {
                    pairs[pairName] = pairValue;
                }
            }

            // middleAgent.SendCommand(cmdNum, pairs);
            var seqNum = (ushort)numSeq.Value;
            middleAgent.SendCommand(cmdNum, pairs, seqNum);
        }

        private void cbSend_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectCmd = cbSend.Text.Split('_')[0].Substring(3);
            int selectCmdNum = int.Parse(selectCmd);
            SetDataGridViewFromCmdNum(selectCmdNum);
        }

        private void SetDataGridViewFromCmdNum(int selectCmdNum)
        {
            PropertyInfo[] infos;
            var cmdType = (EnumCmdNums)selectCmdNum;

            switch (cmdType)
            {
                case EnumCmdNums.Cmd000_EmptyCommand:
                    ID_2_BASIC_INFO_VERSION_RESPONSE cmd002 = new ID_2_BASIC_INFO_VERSION_RESPONSE();
                    infos = cmd002.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd002);
                    break;
                case EnumCmdNums.Cmd31_TransferRequest:
                    ID_31_TRANS_REQUEST cmd31 = new ID_31_TRANS_REQUEST();
                    cmd31.CmdID = "Cmd001";
                    cmd31.CSTID = "CA0070";
                    cmd31.ActType = ActiveType.Move;
                    //cmd31.DestinationAdr = "adr011";
                    cmd31.ToAdr = "adr011";
                    cmd31.LoadAdr = "adr010";
                    //cmd31.GuideAddressesStartToLoad.AddRange(new List<string>());
                    //cmd31.GuideAddressesToDestination.AddRange(new List<string>());
                    cmd31.GuideSections.AddRange(new List<string>());
                    cmd31.GuideSegments.AddRange(new List<string>());
                    //cmd31.GuideSectionsStartToLoad.AddRange(new List<string>());
                    //cmd31.GuideSectionsToDestination.AddRange(new List<string>());
                    infos = cmd31.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd31);
                    break;
                case EnumCmdNums.Cmd32_TransferCompleteResponse:
                    ID_32_TRANS_COMPLETE_RESPONSE cmd32 = new ID_32_TRANS_COMPLETE_RESPONSE();
                    cmd32.ReplyCode = 0;
                    infos = cmd32.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd32);
                    break;
                case EnumCmdNums.Cmd33_ControlZoneCancelRequest:
                    ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST cmd33 = new ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST();
                    cmd33.CancelSecID = "Sec001";
                    cmd33.ControlType = ControlType.Nothing;
                    infos = cmd33.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd33);
                    break;

                case EnumCmdNums.Cmd36_TransferEventResponse:
                    ID_36_TRANS_EVENT_RESPONSE cmd36 = new ID_36_TRANS_EVENT_RESPONSE();
                    cmd36.IsBlockPass = PassType.Pass;
                    cmd36.ReplyCode = 0;
                    infos = cmd36.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd36);
                    break;
                case EnumCmdNums.Cmd37_TransferCancelRequest:
                    ID_37_TRANS_CANCEL_REQUEST cmd37 = new ID_37_TRANS_CANCEL_REQUEST();
                    cmd37.CmdID = "Cmd001";
                    cmd37.ActType = CMDCancelType.CmdAbout;
                    infos = cmd37.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd37);
                    break;
                case EnumCmdNums.Cmd39_PauseRequest:
                    ID_39_PAUSE_REQUEST cmd39 = new ID_39_PAUSE_REQUEST();
                    cmd39.EventType = PauseEvent.Continue;
                    cmd39.PauseType = PauseType.None;
                    infos = cmd39.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd39);
                    break;
                case EnumCmdNums.Cmd41_ModeChange:
                    ID_41_MODE_CHANGE_REQ cmd41 = new ID_41_MODE_CHANGE_REQ();
                    cmd41.OperatingVHMode = OperatingVHMode.OperatingAuto;
                    infos = cmd41.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd41);
                    break;
                case EnumCmdNums.Cmd43_StatusRequest:
                    ID_43_STATUS_REQUEST cmd43 = new ID_43_STATUS_REQUEST();
                    infos = cmd43.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd43);
                    break;
                case EnumCmdNums.Cmd44_StatusRequest:
                    ID_44_STATUS_CHANGE_RESPONSE cmd44 = new ID_44_STATUS_CHANGE_RESPONSE();
                    cmd44.ReplyCode = 0;
                    infos = cmd44.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd44);
                    break;
                case EnumCmdNums.Cmd45_PowerOnoffRequest:
                    ID_45_POWER_OPE_REQ cmd45 = new ID_45_POWER_OPE_REQ();
                    cmd45.OperatingPowerMode = OperatingPowerMode.OperatingPowerOn;
                    infos = cmd45.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd45);
                    break;

                case EnumCmdNums.Cmd71_RangeTeachRequest:
                    ID_71_RANGE_TEACHING_REQUEST cmd71 = new ID_71_RANGE_TEACHING_REQUEST();
                    cmd71.FromAdr = "Adr001";
                    cmd71.ToAdr = "Adr002";
                    infos = cmd71.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd71);
                    break;
                case EnumCmdNums.Cmd72_RangeTeachCompleteResponse:
                    ID_72_RANGE_TEACHING_COMPLETE_RESPONSE cmd72 = new ID_72_RANGE_TEACHING_COMPLETE_RESPONSE();
                    cmd72.ReplyCode = 0;
                    infos = cmd72.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd72);
                    break;
                case EnumCmdNums.Cmd74_AddressTeachResponse:
                    ID_74_ADDRESS_TEACH_RESPONSE cmd74 = new ID_74_ADDRESS_TEACH_RESPONSE();
                    cmd74.ReplyCode = 0;
                    infos = cmd74.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd74);
                    break;
                case EnumCmdNums.Cmd91_AlarmResetRequest:
                    ID_91_ALARM_RESET_REQUEST cmd91 = new ID_91_ALARM_RESET_REQUEST();
                    infos = cmd91.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd91);
                    break;
                case EnumCmdNums.Cmd94_AlarmResponse:
                    ID_94_ALARM_RESPONSE cmd94 = new ID_94_ALARM_RESPONSE();
                    cmd94.ReplyCode = 0;
                    infos = cmd94.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd94);
                    break;
                case EnumCmdNums.Cmd131_TransferResponse:
                    ID_131_TRANS_RESPONSE cmd131 = new ID_131_TRANS_RESPONSE();
                    cmd131.CmdID = "Cmd001";
                    cmd131.ActType = ActiveType.Move;
                    cmd131.NgReason = "Empty";
                    cmd131.ReplyCode = 0;
                    infos = cmd131.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd131);
                    break;
                case EnumCmdNums.Cmd132_TransferCompleteReport:
                    ID_132_TRANS_COMPLETE_REPORT cmd132 = new ID_132_TRANS_COMPLETE_REPORT();
                    cmd132.CmdID = "Cmd001";
                    infos = cmd132.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd132);
                    break;
                case EnumCmdNums.Cmd133_ControlZoneCancelResponse:
                    ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE cmd133 = new ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE();
                    cmd133.CancelSecID = "sec001";
                    cmd133.ControlType = ControlType.Block;
                    cmd133.ReplyCode = 0;
                    infos = cmd133.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd133);
                    break;
                case EnumCmdNums.Cmd134_TransferEventReport:
                    ID_134_TRANS_EVENT_REP cmd134 = new ID_134_TRANS_EVENT_REP();
                    cmd134.CurrentAdrID = "adr001";
                    cmd134.CurrentSecID = "sec001";
                    //cmd134.DrivingDirection = DriveDirction.DriveDirForward;
                    cmd134.EventType = EventType.AdrPass;
                    cmd134.SecDistance = 12345;
                    infos = cmd134.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd134);
                    break;
                case EnumCmdNums.Cmd136_TransferEventReport:
                    ID_136_TRANS_EVENT_REP cmd136 = new ID_136_TRANS_EVENT_REP();
                    cmd136.CSTID = "CA0070";
                    cmd136.CurrentAdrID = "adr001";
                    cmd136.CurrentSecID = "sec001";
                    cmd136.EventType = EventType.AdrPass;
                    infos = cmd136.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd136);
                    break;
                case EnumCmdNums.Cmd137_TransferCancelResponse:
                    ID_137_TRANS_CANCEL_RESPONSE cmd137 = new ID_137_TRANS_CANCEL_RESPONSE();
                    cmd137.ReplyCode = 0;
                    infos = cmd137.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd137);
                    break;
                case EnumCmdNums.Cmd139_PauseResponse:
                    ID_139_PAUSE_RESPONSE cmd139 = new ID_139_PAUSE_RESPONSE();
                    cmd139.ReplyCode = 0;
                    infos = cmd139.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd139);
                    break;
                case EnumCmdNums.Cmd141_ModeChangeResponse:
                    ID_141_MODE_CHANGE_RESPONSE cmd141 = new ID_141_MODE_CHANGE_RESPONSE();
                    cmd141.ReplyCode = 0;
                    infos = cmd141.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd141);
                    break;
                case EnumCmdNums.Cmd143_StatusResponse:
                    ID_143_STATUS_RESPONSE cmd143 = new ID_143_STATUS_RESPONSE();
                    cmd143.CmdID = "Cmd001";
                    infos = cmd143.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd143);
                    break;
                case EnumCmdNums.Cmd144_StatusReport:
                    ID_144_STATUS_CHANGE_REP cmd144 = new ID_144_STATUS_CHANGE_REP();
                    cmd144.CmdID = "Cmd001";
                    infos = cmd144.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd144);
                    break;
                case EnumCmdNums.Cmd145_PowerOnoffResponse:
                    ID_145_POWER_OPE_RESPONSE cmd145 = new ID_145_POWER_OPE_RESPONSE();
                    cmd145.ReplyCode = 0;
                    infos = cmd145.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd145);
                    break;
                case EnumCmdNums.Cmd171_RangeTeachResponse:
                    ID_171_RANGE_TEACHING_RESPONSE cmd171 = new ID_171_RANGE_TEACHING_RESPONSE();
                    cmd171.ReplyCode = 0;
                    infos = cmd171.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd171);
                    break;
                case EnumCmdNums.Cmd172_RangeTeachCompleteReport:
                    ID_172_RANGE_TEACHING_COMPLETE_REPORT cmd172 = new ID_172_RANGE_TEACHING_COMPLETE_REPORT();
                    cmd172.CompleteCode = 0;
                    infos = cmd172.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd172);
                    break;
                case EnumCmdNums.Cmd174_AddressTeachReport:
                    ID_174_ADDRESS_TEACH_REPORT cmd174 = new ID_174_ADDRESS_TEACH_REPORT();
                    cmd174.Addr = "adr001";
                    infos = cmd174.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd174);
                    break;
                case EnumCmdNums.Cmd191_AlarmResetResponse:
                    ID_191_ALARM_RESET_RESPONSE cmd191 = new ID_191_ALARM_RESET_RESPONSE();
                    cmd191.ReplyCode = 0;
                    infos = cmd191.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd191);
                    break;
                case EnumCmdNums.Cmd194_AlarmReport:
                default:
                    ID_194_ALARM_REPORT cmd194 = new ID_194_ALARM_REPORT();
                    cmd194.ErrCode = "Empty";
                    cmd194.ErrDescription = "Empty";
                    cmd194.ErrStatus = ErrorStatus.ErrSet;
                    infos = cmd194.GetType().GetProperties();
                    SetDataGridViewFromInfos(infos, cmd194);
                    break;
            }
        }

        private void SetDataGridViewFromInfos(PropertyInfo[] infos, object obj)
        {
            dataGridView1.Rows.Clear();

            foreach (var info in infos)
            {
                if (info.CanWrite)
                {
                    var name = info.Name;
                    var value = info.GetValue(obj);
                    string[] row = { name, value.ToString() };
                    dataGridView1.Rows.Add(row);
                }
            }

            foreach (var info in infos)
            {
                if (info.PropertyType == typeof(RepeatedField<string>))
                {
                    var name = info.Name;
                    var value = info.GetValue(obj);
                    string[] row = { name, value.ToString() };
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private void btnAlarmSet_Click(object sender, EventArgs e)
        {
            middleAgent.SetAlarmToOHBC();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                middlerConfigs.RemoteIp = txtIp.Text;
                middlerConfigs.RemotePort = int.Parse(txtPort.Text);

                SaveMiddlerConfigs();

                middleAgent.ReConnect();

            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        private void SaveMiddlerConfigs()
        {
            string iniPath = Path.Combine(Environment.CurrentDirectory, "Configs.ini");
            configHandler = new ConfigHandler(iniPath);

            configHandler.SetString("Middler", "RemoteIp", middlerConfigs.RemoteIp);
            configHandler.SetString("Middler", "RemotePort", middlerConfigs.RemotePort.ToString());
            configHandler.SetString("Middler", "IsServer", middlerConfigs.IsServer.ToString());

        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            middleAgent.DisConnect();
        }

        private void cbAutoApplyReserve_CheckedChanged(object sender, EventArgs e)
        {
            middleAgent.AutoApplyReserve = cbAutoApplyReserve.Checked;
        }

        public Dictionary<string, string> SetupCommandInfoPairs(LoadunloadCmd loadunloadCmd)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            try
            {
                pairs.Add("CmdID", loadunloadCmd.CmdId);
                pairs.Add("CSTID", loadunloadCmd.CstId);
                //pairs.Add("ActType", loadunloadCmd.ActType.ToString());
                pairs.Add("GuideSectionsStartToLoad", SetupCommandInfoPairFromBuffer(loadunloadCmd.GuideSectionsStartToLoad));
                pairs.Add("GuideAddressesStartToLoad", SetupCommandInfoPairFromBuffer(loadunloadCmd.GuideAddressesStartToLoad));
                pairs.Add("LoadAdr", loadunloadCmd.LoadAdr);
                pairs.Add("GuideSectionsToDestination", SetupCommandInfoPairFromBuffer(loadunloadCmd.GuideSectionsToDestination));
                pairs.Add("GuideAddressesToDestination", SetupCommandInfoPairFromBuffer(loadunloadCmd.GuideAddressesToDestination));
                pairs.Add("DestinationAdr", loadunloadCmd.DestinationAdr);
                pairs.Add("SecDistance", loadunloadCmd.SecDistance.ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

            return pairs;
        }

        public string SetupCommandInfoPairFromBuffer(List<string> strbuffer)
        {
            if (strbuffer.Count == 0)
            {
                return "[]";
            }

            string v = "[";
            for (int i = 0; i < strbuffer.Count - 1; i++)
            {
                v += strbuffer[i] + ",";
            }
            v += strbuffer[strbuffer.Count - 1] + "]";
            return v;
        }

        private EnumAddress EnumAddressParse(string v)
        {
            try
            {
                v = v.Trim();

                return (EnumAddress)Enum.Parse(typeof(EnumAddress), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return EnumAddress.A;
            }
        }

        public EnumAddress GetEnumAddressFormRadioButton(RadioButton radioButton)
        {
            return EnumAddressParse(radioButton.Text);
        }

        private void gbLoactionRbtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                EnumAddress enumAddress = GetEnumAddressFormRadioButton(radioButton);
                CurAddr = enumAddress;
                //RichTextBoxAppendHead(richTextBox1, CurAddr.ToString());
            }
        }

        public void RenewCurAddrToGbLocationRbtns()
        {
            switch (CurAddr)
            {
                case EnumAddress.B:
                    rbtnB.Checked = true;
                    break;
                case EnumAddress.G:
                    rbtnG.Checked = true;
                    break;
                case EnumAddress.A:
                default:
                    rbtnA.Checked = true;
                    break;
            }
        }

        private void btnNextRandomLoaction_Click(object sender, EventArgs e)
        {
            EnumAddress randomAddress = GetNextRandomAdr();

            CurAddr = randomAddress;

            RenewCurAddrToGbLocationRbtns();
        }

        public delegate void DelSendNextRandomLoadunloadCmd();
        public void SendNextRandomLoadunloadCmd()
        {
            if (InvokeRequired)
            {
                DelSendNextRandomLoadunloadCmd mydel = new DelSendNextRandomLoadunloadCmd(SendNextRandomLoadunloadCmd);
                Invoke(mydel);
            }
            else
            {
                TempAddr = GetNextRandomAdr();
                LoadunloadCmd cmd = GetLoadunloadCmd(CurAddr, TempAddr);

                var pairs = SetupCommandInfoPairs(cmd);

                middleAgent.SendCommand(31, pairs);

                var showMsg = $"+++++++++ {cmd.CmdText} ++++++++++";

                AppendDebugLogMsg(showMsg);

                //LoopNext();
            }
        }

        private LoadunloadCmd GetLoadunloadCmd(EnumAddress startAdr, EnumAddress endAdr)
        {
            Dictionary<EnumAddress, LoadunloadCmd> CmdsFromX = new Dictionary<EnumAddress, LoadunloadCmd>();
            switch (startAdr)
            {
                case EnumAddress.B:
                    CmdsFromX = CmdsFromB;
                    break;
                case EnumAddress.G:
                    CmdsFromX = CmdsFromG;
                    break;
                case EnumAddress.A:
                default:
                    CmdsFromX = CmdsFromA;
                    break;
            }

            if (!CmdsFromX.ContainsKey(endAdr))
            {
                return new LoadunloadCmd();
            }

            var loadUnloadCmd = CmdsFromX[endAdr];
            var num = (random.Next() % 1000).ToString();
            loadUnloadCmd.CmdId = num;

            return loadUnloadCmd;
        }

        private EnumAddress GetNextRandomAdr()
        {
            EnumAddress randomAddress = CurAddr;

            if (IsInSitu)
            {
                randomAddress = (EnumAddress)(random.Next() % 3);
            }
            else
            {
                while (randomAddress == CurAddr)
                {
                    randomAddress = (EnumAddress)(random.Next() % 3);
                }
            }

            return randomAddress;
        }

        private void btnSendRandomSingleCmd_Click(object sender, EventArgs e)
        {
            SendNextRandomLoadunloadCmd();
        }

        private void btnRandomLoop_Click(object sender, EventArgs e)
        {
            if (timerRandomLoop.Enabled)
            {
                timerRandomLoop.Enabled = false;
                btnRandomLoop.ForeColor = Color.Red;
            }
            else
            {
                MoveEndFlag = true;
                timerRandomLoop.Enabled = true;
                sw.Start();
                btnRandomLoop.ForeColor = Color.Green;
            }
        }

        private void timerRandomLoop_Tick(object sender, EventArgs e)
        {
            if (MoveEndFlag)
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds > middlerConfigs.CycleRunIntervalMs)
                {
                    MoveEndFlag = false;
                    SendNextRandomLoadunloadCmd();
                    sw.Reset();
                }
                else
                {
                    sw.Start();
                }
            }
        }

        public void LoopNext()
        {
            CurAddr = TempAddr;
            MoveEndFlag = true;
        }

        public void UnloadArrival()
        {

        }

        private void btnXtoY_Click(object sender, EventArgs e)
        {
            EnumAddress startAdr, endAdr;
            if (cbMiddle.Text == ">>")
            {
                startAdr = EnumAddressParse(cbLeft.Text);
                endAdr = EnumAddressParse(cbRight.Text);
            }
            else
            {
                startAdr = EnumAddressParse(cbRight.Text);
                endAdr = EnumAddressParse(cbLeft.Text);
            }

            var loadunloadCmd = GetLoadunloadCmd(startAdr, endAdr);

            var pairs = SetupCommandInfoPairs(loadunloadCmd);

            middleAgent.SendCommand(31, pairs);

            var showMsg = $"+++++++++ {loadunloadCmd.CmdText} ++++++++++";
            AppendDebugLogMsg(showMsg);
        }

        private void cbIsInSitu_CheckedChanged(object sender, EventArgs e)
        {
            IsInSitu = cbIsInSitu.Checked;
        }

        private void btnCleanRichTextBox_Click(object sender, EventArgs e)
        {
            DebugLogMsg = "";
        }

        private void btnIsNull_Click(object sender, EventArgs e)
        {
            var msg = $"IsNull={middleAgent.ServerClientAgent == null}";
            AppendDebugLogMsg(msg);
        }

        private void btnSetReserveReplyOnce_Click(object sender, EventArgs e)
        {
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
            middleAgent.Send_Cmd36_ReservePass(txtReserveSectionId.Text, cbReserveSuccess.Checked);
        }

        private void RadioBtn_Client_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtn_Server.Checked == true)
            {
                middlerConfigs.IsServer = true;
            }
            else if (RadioBtn_Client.Checked == true)
            {
                middlerConfigs.IsServer = false;
            }
        }

        private void radioOperaSet_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Normal.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abnormal_BcrReadFail_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abnormal_BcrReadFail.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abnormal_BcrMisMatch_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abnormal_BcrMisMatch.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abnormal_BcrDuplicate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abnormal_BcrDuplicate.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abnormal_DoubleStorage_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abnormal_DoubleStorage.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abnormal_EmptyRetrieval_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abnormal_EmptyRetrieval.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Cancel.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radioButton_Abort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Abort.Checked == true)
            {
                CheckChanged();
            }
        }

        private void radio_InterlockError_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_InterlockError.Checked == true)
            {
                CheckChanged();
            }
        }
        private void CheckChanged()
        {
            if (radioButton_Normal.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.NormalComplete);
            }
            else if (radioButton_Abnormal_BcrReadFail.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.Abnormal_BcrReadFail);
            }
            else if (radioButton_Abnormal_BcrMisMatch.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.Abnormal_BcrMismatch);
            }
            else if (radioButton_Abnormal_BcrDuplicate.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.Abnormal_BcrDuplicate);
            }
            else if (radioButton_Abnormal_DoubleStorage.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.Abnormal_DoubleStorage);
            }
            else if (radioButton_Abnormal_EmptyRetrieval.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.Abnormal_EmptyRetrieval);
            }
            else if (radioButton_Cancel.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.CancelComplete);
            }
            else if (radioButton_Abort.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.AbortComplete);
            }
            else if (radioButton_InterlockError.Checked == true)
            {
                middleAgent.OperatypeSet(EnumOperaType.InterlockError);
            }
        }

        private void set_button_Click(object sender, EventArgs e)
        {
            Global_Param.MismatchID = Mismatch_ID.Text;
        }
    }
}
