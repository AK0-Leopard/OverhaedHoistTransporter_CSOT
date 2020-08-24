//*********************************************************************************
//      CSTInterface_Form.cs
//*********************************************************************************
// File Name: CSTInterface_Form.cs
// Description: CST Interface Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using Nest;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    public partial class CSTInterface_Form : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        List<string> vehicleList = new List<string>();
        List<string> PortIDList = new List<string>();
        List<Signal> signalList = new List<Signal>();
        List<Flow> flowList = new List<Flow>();
        private static CSTInterface_Form instance;
        static object locl_obj = new object();
        #endregion 公用參數設定

        public static CSTInterface_Form getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                lock (locl_obj)
                {
                    if (instance == null)
                    {
                        instance = new CSTInterface_Form();
                    }
                }
            }
            return instance;
        }

        private CSTInterface_Form()
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

        private async void cmb_TransList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string title;
                List<int> xLabels = new List<int>();
                List<int> xPoints = new List<int>();
                if (cmb_FT.Text.Contains("Load"))
                {
                    title = "Load";
                }
                else
                {
                    title = "Unload";
                }

                Flow flow = new Flow();
                flow = flowList[cmb_TransList.SelectedIndex];
                int count = flow.signals.Count;
                List<List<int>> signals = new List<List<int>>();
                List<int> L_REQ = new List<int>();
                List<int> U_REQ = new List<int>();
                List<int> READY = new List<int>();
                List<int> VA = new List<int>();
                List<int> VS_0 = new List<int>();
                List<int> VS_1 = new List<int>();
                List<int> VALID = new List<int>();
                List<int> TR_REQ = new List<int>();
                List<int> BUSY = new List<int>();
                List<int> COMPT = new List<int>();
                List<int> AM_AVBL = new List<int>();
                List<int> HO_AVBL = new List<int>();
                List<int> ES = new List<int>();

                int totalExecuteTime = (int)((flow.signals[count - 1].record_time - flow.signals[0].record_time).TotalMilliseconds);


                for (int i = 0; i < count; i++)
                {
                    L_REQ.Add(flow.signals[i].L_REQ);
                    U_REQ.Add(flow.signals[i].U_REQ);
                    READY.Add(flow.signals[i].READY);
                    VA.Add(flow.signals[i].VA);
                    VS_0.Add(flow.signals[i].VS_0);
                    VS_1.Add(flow.signals[i].VS_1);
                    VALID.Add(flow.signals[i].VALID);
                    TR_REQ.Add(flow.signals[i].TR_REQ);
                    BUSY.Add(flow.signals[i].BUSY);
                    COMPT.Add(flow.signals[i].COMPT);
                    AM_AVBL.Add(flow.signals[i].AM_AVBL);
                    HO_AVBL.Add(flow.signals[i].HO_AVBL);
                    ES.Add(flow.signals[i].ES);
                    xLabels.Add((int)((flow.signals[i].record_time - flow.signals[0].record_time).TotalMilliseconds));
                    if (i == 0)
                    {
                        xPoints.Add(0);
                        continue;
                    }
                    int transTime = (int)((flow.signals[i].record_time - flow.signals[i - 1].record_time).TotalMilliseconds);
                    float rate = (float)transTime / totalExecuteTime;
                    if (rate > 0.5)
                    {
                        xPoints.Add(xPoints[i - 1] + 5);
                        continue;
                    }
                    if (rate > 0.25)
                    {
                        xPoints.Add(xPoints[i - 1] + 4);
                        continue;
                    }
                    if (rate > 0.12)
                    {
                        xPoints.Add(xPoints[i - 1] + 3);
                        continue;
                    }
                    if (rate > 0.06)
                    {
                        xPoints.Add(xPoints[i - 1] + 2);
                        continue;
                    }
                    xPoints.Add(xPoints[i - 1] + 1);

                }
                signals.Add(L_REQ);
                signals.Add(U_REQ);
                signals.Add(READY);
                signals.Add(VA);
                signals.Add(VS_0);
                signals.Add(VS_1);
                signals.Add(VALID);
                signals.Add(TR_REQ);
                signals.Add(BUSY);
                signals.Add(COMPT);
                signals.Add(AM_AVBL);
                signals.Add(HO_AVBL);
                signals.Add(ES);

                string[] titles = new string[13] { "OHS->STK : L_REQ", "OHS->STK : U_REQ",
               "OHS->STK : READY", "OHS->STK : VA", "OHS->STK : VS_0",
            "OHS->STK : VS_1","STK->OHS : VALID","STK->OHS : TR_REQ", "STK->OHS : BUSY",
            "STK->OHS : COMPT","STK->OHS : AM_AVBL","OHS->STK : HO_AVBL","OHS->STK : ES"};

                bool result = await CheckFlowSignal(signals);
                if (result)
                {
                    textBox1.Text = "OK";
                }
                else
                {
                    textBox1.Text = "NG";
                }
                for (int i = 0; i < 13; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        signals[i][j] += (12 - i) * 2;
                    }
                }




                Array.Reverse(titles);
                chart1.Titles.Clear();
                chart1.ChartAreas.Clear();
                chart1.Series.Clear();

                chart1.ChartAreas.Add("ChartArea");
                chart1.ChartAreas["ChartArea"].AxisY.Maximum = 25;

                chart1.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                chart1.ChartAreas["ChartArea"].AxisX.Maximum = xPoints[count - 1];
                chart1.ChartAreas["ChartArea"].AxisX.Interval = xPoints[count - 1];
                chart1.ChartAreas["ChartArea"].AxisX.MajorGrid.Enabled = true;
                chart1.ChartAreas["ChartArea"].AxisX.MajorTickMark.Enabled = false;
                chart1.ChartAreas["ChartArea"].AxisY.Interval = 2;
                chart1.ChartAreas["ChartArea"].AxisY.MajorGrid.Enabled = false;
                chart1.ChartAreas["ChartArea"].AxisY.MajorTickMark.Enabled = false;

                for (int i = 0; i < 13; i++)
                {
                    chart1.Series.Add("Series" + (i * 2).ToString());
                    chart1.Series.Add("Series" + (i * 2 + 1).ToString());
                    chart1.Series[(i * 2)].ChartArea = "ChartArea";
                    chart1.Series[(i * 2 + 1)].ChartArea = "ChartArea";
                    chart1.Series[(i * 2)].ChartType = SeriesChartType.Range;
                    chart1.Series[(i * 2 + 1)].ChartType = SeriesChartType.StepLine;
                    chart1.Series[(i * 2)].Color = Color.LightGray;
                    chart1.Series[(i * 2 + 1)].Color = Color.Red;
                    chart1.Series[(i * 2 + 1)].BorderWidth = 4;
                    chart1.Series[(i * 2 + 1)].IsVisibleInLegend = false;


                    for (int j = 0; j < signals[0].Count; j++)
                    {
                        if (j != 0)
                        {
                            chart1.Series[(i * 2)].Points.AddXY(xPoints[j], (signals.Count - 1 - i) * 2, signals[i][j - 1]);
                        }
                        chart1.Series[(i * 2)].Points.AddXY(xPoints[j], (signals.Count - 1 - i) * 2, signals[i][j]);
                    }
                    chart1.Series["Series" + (i * 2 + 1).ToString()].Points.DataBindXY(xPoints, signals[i]);


                    chart1.ChartAreas["ChartArea"].AxisY.CustomLabels.Add(i * 2, i * 2 + 1, titles[i]);
                }

                chart1.Titles.Add(title);
                for (int i = 0; i < count; i++)
                {
                    chart1.ChartAreas["ChartArea"].AxisX.CustomLabels.Add(xPoints[i] - 500, xPoints[i] + 500, xLabels[i].ToString(), 0, LabelMarkStyle.None, GridTickTypes.Gridline);
                }


                //chart1.SaveImage("Chart_" + title + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<bool> CheckFlowSignal(List<List<int>> signals)
        {
            if (signals.Count != 13) return false;
            if (signals[0].Count != 10) return false;
            List<List<int>> transpose_signals = MatrixTranspose(signals);
            APIResult result = await JsonPostAsync(transpose_signals);
            if (result.Message == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<List<int>> MatrixTranspose(List<List<int>> matrix)
        {
            List<List<int>> result = new List<List<int>>();
            try
            {
                for (int i = 0; i < matrix[0].Count; i++)
                {
                    result.Add(new List<int>());
                }
                for (int i = 0; i < matrix[0].Count; i++)
                {
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        result[i].Add(matrix[j][i]);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return result;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= 23; i++)
                {
                    //vehicleList.Add("OHx" + i.ToString("D2"));
                    vehicleList.Add("OHT" + i.ToString("D2"));
                }
                //gen_fake_glassinterface_data();
                foreach (string v in vehicleList)
                {
                    cmb_VH.Items.Add(v);
                }
                cmb_FT.Items.Add("Load");
                cmb_FT.Items.Add("Unload");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void gen_fake_glassinterface_data()
        {
            try
            {
                Random rand = new Random();
                int[] xmembers = new int[11];
                vehicleList.Add("OHT_01");
                vehicleList.Add("OHT_02");
                foreach (string vh_id in vehicleList)
                {
                    DateTime record_time = DateTime.Now.Date;
                    DateTime end_time = record_time.AddHours(1);
                    while (record_time < end_time)
                    {
                        int temp = 0;
                        int total_signal = 13;
                        int total_timing = 10;
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0)
                            {
                                temp = 0;
                            }
                            else
                            {
                                temp = rand.Next(400, 1200);
                            }
                            xmembers[i] = temp;
                        }
                        //xmembers[10] = (xmembers[9] / 10) * 11;
                        int[][] signals_ULD = new int[13][];
                        signals_ULD[0] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals_ULD[1] = new int[10] { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
                        signals_ULD[2] = new int[10] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 };
                        signals_ULD[3] = new int[10] { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals_ULD[4] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals_ULD[5] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals_ULD[6] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals_ULD[7] = new int[10] { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 };
                        signals_ULD[8] = new int[10] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 };
                        signals_ULD[9] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 };
                        signals_ULD[10] = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                        signals_ULD[11] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals_ULD[12] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };

                        int[][] signals = new int[13][];
                        signals[0] = new int[10] { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
                        signals[1] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals[2] = new int[10] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 };
                        signals[3] = new int[10] { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals[4] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals[5] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals[6] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        signals[7] = new int[10] { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 };
                        signals[8] = new int[10] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 };
                        signals[9] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 };
                        signals[10] = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                        signals[11] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                        signals[12] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                        string senario = "Load";
                        if (rand.Next(0, 2) == 0)//隨機取Load或Unload流程
                        {
                            signals = signals_ULD;
                            senario = "Unload";
                        }

                        if (rand.Next(0, 2) == 0)
                        {
                            int defects = rand.Next(1, 21);//隨機取1到20個defect
                            List<int> positionList = new List<int>();
                            for (int i = 0; i < defects; i++)
                            {
                                int tmp = rand.Next(0, 130);
                                while (positionList.Contains(tmp))
                                {
                                    tmp = rand.Next(0, 130);
                                }
                                positionList.Add(tmp);
                            }
                            foreach (int pos in positionList)
                            {
                                int x = pos % 13;
                                int y = pos / 13;
                                if (signals[x][y] == 0)
                                {
                                    signals[x][y] = 1;
                                }
                                else
                                {
                                    signals[x][y] = 0;
                                }
                            }
                        }


                        for (int i = 0; i < signals[0].Length; i++)
                        {
                            Signal signal = new Signal();
                            signal.VH_ID = vh_id;
                            record_time = record_time.AddMilliseconds(xmembers[i]);
                            signal.record_time = record_time;
                            signal.L_REQ = signals[0][i];
                            signal.U_REQ = signals[1][i];
                            signal.READY = signals[2][i];
                            signal.VA = signals[3][i];
                            signal.VS_0 = signals[4][i];
                            signal.VS_1 = signals[5][i];
                            signal.VALID = signals[6][i];
                            signal.TR_REQ = signals[7][i];
                            signal.BUSY = signals[8][i];
                            signal.COMPT = signals[9][i];
                            signal.AM_AVBL = signals[10][i];
                            signal.HO_AVBL = signals[11][i];
                            signal.ES = signals[12][i];
                            signalList.Add(signal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void get_glassinterface_data_from_elastic()
        {
            try
            {
                //var node = new Uri("http://192.168.7.79:9200");//台中
                var node = new Uri("http://192.168.39.111:9200");//新竹

                var settings = new ConnectionSettings(node).DefaultIndex("default");

                var client = new ElasticClient(settings);
                int size = 10000;
                SearchRequest sr = new SearchRequest("recodevehiclecstinterface*");

                DateTime start_datetime = dtp_start.Value.AddHours(-10);
                DateTime end_datetime = start_datetime.AddHours(10);
                DateRangeQuery dq = new DateRangeQuery
                {
                    Field = "@timestamp",
                    GreaterThan = start_datetime,
                    LessThan = end_datetime,
                };

                TermsQuery tsq = new TermsQuery
                {
                    Field = new Field("VH_ID.keyword"),
                    Terms = new List<string> { cmb_VH.Text }
                };

                sr.From = 0;
                sr.Size = size;

                sr.Query = dq && tsq;
                //sr.Query = dq;
                sr.Source = new SourceFilter()
                {
                    Includes = new string[] { "@timestamp", "VALID", "AM_AVBL", "BUSY", "COMPT", "CONT", "CS_0", "CS_1", "HO_AVBL", "L_REQ", "READY", "TR_REQ", "U_REQ", "VA", "VS_0", "VS_1", "ES", "VH_ID", "PORT_ID" },
                    //Excludes = new string[] { "CMD_FINISH_TIME" }
                };
                var result = client.Search<recodevehiclecstinterface>(sr);

                List<recodevehiclecstinterface> resultList = result.Documents.ToList<recodevehiclecstinterface>();
                signalList.Clear();
                foreach (recodevehiclecstinterface record in resultList)
                {
                    Signal signal = new Signal();
                    if (!vehicleList.Contains(record.VH_ID))
                    {
                        vehicleList.Add(record.VH_ID);
                    }
                    signal.VH_ID = record.VH_ID;
                    signal.PORT_ID = record.PORT_ID;
                    signal.record_time = record.timestamp;
                    signal.L_REQ = record.L_REQ;
                    signal.U_REQ = record.U_REQ;
                    signal.READY = record.READY;
                    signal.VA = record.VA;
                    signal.VS_0 = record.VS_0;
                    signal.VS_1 = record.VS_1;
                    signal.VALID = record.VALID;
                    signal.TR_REQ = record.TR_REQ;
                    signal.BUSY = record.BUSY;
                    signal.COMPT = record.COMPT;
                    signal.AM_AVBL = record.AM_AVBL;
                    signal.HO_AVBL = record.HO_AVBL;
                    signal.ES = record.ES;
                    signalList.Add(signal);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas.Clear();
                chart1.Series.Clear();
                chart1.Legends.Clear();
                chart1.Titles.Clear();
                cmb_TransList.Text = string.Empty;
                cmb_TransList.Items.Clear();
                cmbPort.Text = string.Empty; ;
                cmbPort.Items.Clear();
                get_glassinterface_data_from_elastic();
                setPortList();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void setPortList()
        {
            try
            {
                PortIDList.Clear();
                cmbPort.Items.Clear();
                foreach (Signal s in signalList)
                {
                    if (!PortIDList.Contains(s.PORT_ID))
                    {
                        PortIDList.Add(s.PORT_ID);
                    }
                }
                foreach (string v in PortIDList)
                {
                    if (v == null) continue;
                    cmbPort.Items.Add(v);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private List<Flow> gen_flows(string vh_id, string port_id, DateTime date, bool isLoad)
        {
            List<Signal> target_signals = new List<Signal>();
            List<Flow> flows = new List<Flow>();
            foreach (Signal s in signalList)
            {
                if (s.VH_ID == vh_id && s.PORT_ID == port_id)
                {
                    if (s.record_time >= date && s.record_time < date.AddDays(1))
                    {
                        target_signals.Add(s);
                    }
                }
            }
            target_signals = target_signals.OrderBy(s => s.record_time).ToList();
            for (int i = 0; i < target_signals.Count - 1; i++)
            {
                if (isStartSignal(isLoad, target_signals[i], target_signals[i + 1]))
                {
                    for (int j = 5; j < 15; j++)
                    {
                        if ((i + j + 1) >= target_signals.Count)
                        {
                            break;
                        }
                        if (isEndSignal(target_signals[i + j], target_signals[i + j + 1]))
                        {
                            Flow flow = new Flow();
                            flow.VH_ID = vh_id;
                            flow.start_time = target_signals[i + 1].record_time;
                            for (int x = i; x <= (i + j + 1); x++)
                            {
                                flow.signals.Add(target_signals[x]);
                            }
                            flows.Add(flow);
                            break;
                        }
                    }
                }
            }
            return flows;
        }


        private List<Flow> gen_flows_with_time_period(string vh_id, string port_id, DateTime date, bool isLoad, int time_period_sec)
        {
            List<Signal> target_signals = new List<Signal>();
            List<Flow> flows = new List<Flow>();
            foreach (Signal s in signalList)
            {
                if (s.VH_ID == vh_id && s.PORT_ID == port_id)
                {
                    if (s.record_time >= date && s.record_time < date.AddDays(1))
                    {
                        target_signals.Add(s);
                    }
                }
            }
            target_signals = target_signals.OrderBy(s => s.record_time).ToList();
            for (int i = 0; i < target_signals.Count - 1; i++)
            {
                if (isStartSignal(isLoad, target_signals[i], target_signals[i + 1]))
                {
                    DateTime ExpireTime = target_signals[i + 1].record_time.AddSeconds(time_period_sec);
                    Flow flow = new Flow();
                    flow.start_time = target_signals[i + 1].record_time;
                    flow.signals.Add(target_signals[i]);
                    flow.signals.Add(target_signals[i + 1]);
                    flow.VH_ID = vh_id;
                    int j = 1;
                    while (target_signals.Count > (i + 1 + j) && target_signals[i + 1 + j].record_time < ExpireTime)
                    {
                        flow.signals.Add(target_signals[i + 1 + j]);
                        j++;
                    }
                    flows.Add(flow);
                }
            }
            return flows;
        }

        private bool isStartSignal(bool isLoad, Signal first_signal, Signal second_signal)
        {
            if (isLoad)
            {
                if ((first_signal.L_REQ == 0 &&
                  first_signal.U_REQ == 0 &&
                  first_signal.READY == 0 &&
                  first_signal.VA == 0 &&
                  first_signal.VS_0 == 0 &&
                  first_signal.VS_1 == 0 &&
                  first_signal.VALID == 0 &&
                  first_signal.TR_REQ == 0 &&
                  first_signal.BUSY == 0 &&
                  first_signal.COMPT == 0 &&
                  first_signal.AM_AVBL == 1 &&
                  first_signal.HO_AVBL == 0 &&
                  first_signal.ES == 0) &&
                  (second_signal.L_REQ == 1 &&
                  second_signal.U_REQ == 0 &&
                  second_signal.READY == 0 &&
                  second_signal.VA == 0 &&
                  second_signal.VS_0 == 1 &&
                  second_signal.VS_1 == 0 &&
                  second_signal.VALID == 0 &&
                  second_signal.TR_REQ == 0 &&
                  second_signal.BUSY == 0 &&
                  second_signal.COMPT == 0 &&
                  second_signal.AM_AVBL == 1 &&
                  second_signal.HO_AVBL == 1 &&
                  second_signal.ES == 1))
                {
                    return true;
                }
            }
            else
            {
                if ((first_signal.L_REQ == 0 &&
                  first_signal.U_REQ == 0 &&
                  first_signal.READY == 0 &&
                  first_signal.VA == 0 &&
                  first_signal.VS_0 == 0 &&
                  first_signal.VS_1 == 0 &&
                  first_signal.VALID == 0 &&
                  first_signal.TR_REQ == 0 &&
                  first_signal.BUSY == 0 &&
                  first_signal.COMPT == 0 &&
                  first_signal.AM_AVBL == 1 &&
                  first_signal.HO_AVBL == 0 &&
                  first_signal.ES == 0) &&
                  (second_signal.L_REQ == 0 &&
                  second_signal.U_REQ == 1 &&
                  second_signal.READY == 0 &&
                  second_signal.VA == 0 &&
                  second_signal.VS_0 == 1 &&
                  second_signal.VS_1 == 0 &&
                  second_signal.VALID == 0 &&
                  second_signal.TR_REQ == 0 &&
                  second_signal.BUSY == 0 &&
                  second_signal.COMPT == 0 &&
                  second_signal.AM_AVBL == 1 &&
                  second_signal.HO_AVBL == 1 &&
                  second_signal.ES == 1))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isEndSignal(Signal first_signal, Signal second_signal)
        {
            if ((first_signal.L_REQ == 0 &&
              first_signal.U_REQ == 0 &&
              first_signal.READY == 0 &&
              first_signal.VA == 1 &&
              first_signal.VS_0 == 1 &&
              first_signal.VS_1 == 0 &&
              first_signal.VALID == 0 &&
              first_signal.TR_REQ == 0 &&
              first_signal.BUSY == 0 &&
              first_signal.COMPT == 1 &&
              first_signal.AM_AVBL == 1 &&
              first_signal.HO_AVBL == 1 &&
              first_signal.ES == 1) &&
              (second_signal.L_REQ == 0 &&
              second_signal.U_REQ == 0 &&
              second_signal.READY == 0 &&
              second_signal.VA == 0 &&
              second_signal.VS_0 == 0 &&
              second_signal.VS_1 == 0 &&
              second_signal.VALID == 0 &&
              second_signal.TR_REQ == 0 &&
              second_signal.BUSY == 0 &&
              second_signal.COMPT == 0 &&
              second_signal.AM_AVBL == 1 &&
              second_signal.HO_AVBL == 0 &&
              second_signal.ES == 0))
            {
                return true;
            }
            return false;
        }

        private void cmb_FT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshCSTInfo();
                //Image img;
                //if (cmb_FT.Text == "Load")
                //{
                //    using (var bmpTemp = new Bitmap(Path.Combine(Environment.CurrentDirectory, "Chart_Load_20181029110716.png")))
                //    {
                //        img = new Bitmap(bmpTemp);
                //    }
                //    panel1.BackgroundImage = img;
                //}
                //else
                //{
                //    using (var bmpTemp = new Bitmap(Path.Combine(Environment.CurrentDirectory, "Chart_Unload_20181029110843.png")))
                //    {
                //        img = new Bitmap(bmpTemp);
                //    }
                //    panel1.BackgroundImage = img;
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }



        private static async Task<APIResult> JsonPostAsync(List<List<int>> signals)
        {
            APIResult fooAPIResult;
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        string url = "http://127.0.0.1:5000/CNN";
                        HttpResponseMessage response = null;

                        #region  設定相關網址內容

                        // Accept 用於宣告客戶端要求服務端回應的文件型態 (底下兩種方法皆可任選其一來使用)
                        //client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                        //var fooJSON = JsonConvert.SerializeObject(apiData);
                        var fooJSON = JsonConvert.SerializeObject(signals);
                        using (var fooContent = new StringContent(fooJSON, Encoding.UTF8, "application/json"))
                        {
                            response = await client.PostAsync(url, fooContent);
                        }
                        #endregion
                        #endregion

                        #region 處理呼叫完成 Web API 之後的回報結果
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                // 取得呼叫完成 API 後的回報內容
                                String strResult = await response.Content.ReadAsStringAsync();

                                fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                            }
                            else
                            {
                                fooAPIResult = new APIResult
                                {
                                    Success = false,
                                    Message = string.Format("Error Code:{0}, Error Message:{1}", response.StatusCode, response.RequestMessage),
                                    Payload = null,
                                };
                            }
                        }
                        else
                        {
                            fooAPIResult = new APIResult
                            {
                                Success = false,
                                Message = "應用程式呼叫 API 發生異常",
                                Payload = null,
                            };
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fooAPIResult = new APIResult
                        {
                            Success = false,
                            Message = ex.Message,
                            Payload = ex,
                        };
                    }
                }
            }

            return fooAPIResult;
        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshCSTInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void RefreshCSTInfo()
        {
            try
            {
                chart1.ChartAreas.Clear();
                chart1.Series.Clear();
                chart1.Legends.Clear();
                chart1.Titles.Clear();
                cmb_TransList.Text = string.Empty;
                cmb_TransList.Items.Clear();
                //flowList = gen_flows(cmb_VH.Text, cmbPort.Text, dtp_start.Value.Date, cmb_FT.Text == "Load");    //抓到流程結束特徵作為結束
                flowList = gen_flows_with_time_period(cmb_VH.Text, cmbPort.Text, dtp_start.Value.Date, cmb_FT.Text == "Load", 60);  //抓到開始特徵後，往後抓一分鐘的訊號作為流程

                foreach (Flow f in flowList)
                {
                    cmb_TransList.Items.Add(f.start_time);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        class Signal
        {
            public string VH_ID { get; set; }
            public string PORT_ID { get; set; }
            public DateTime record_time { get; set; }
            public int L_REQ { get; set; }
            public int U_REQ { get; set; }
            public int READY { get; set; }
            public int VA { get; set; }
            public int VS_0 { get; set; }
            public int VS_1 { get; set; }
            public int VALID { get; set; }
            public int TR_REQ { get; set; }
            public int BUSY { get; set; }
            public int COMPT { get; set; }
            public int AM_AVBL { get; set; }
            public int HO_AVBL { get; set; }
            public int ES { get; set; }
        }
        class Flow
        {
            public string VH_ID { get; set; }
            public DateTime start_time { get; set; }
            public List<Signal> signals = new List<Signal>();
        }
        public class APIResult
        {
            public bool Success;
            public string Message;
            public object Payload;
            public APIResult()
            {

            }
        }
        public class APIData
        {
            public string name;
            public Dictionary<string, string> items;
            public APIData()
            {

            }
        }
        public class recodevehiclecstinterface
        {
            [Date(Name = "@timestamp")]
            public DateTime timestamp { get; set; }
            public int VALID { get; set; }
            public int AM_AVBL { get; set; }
            public int BUSY { get; set; }
            public int COMPT { get; set; }
            public int CONT { get; set; }
            public int CS_0 { get; set; }
            public int CS_1 { get; set; }
            public int HO_AVBL { get; set; }
            public int L_REQ { get; set; }
            public int READY { get; set; }
            public int TR_REQ { get; set; }
            public int U_REQ { get; set; }
            public int VA { get; set; }
            public int VS_0 { get; set; }
            public int VS_1 { get; set; }
            public int ES { get; set; }
            public string VH_ID { get; set; }
            public string PORT_ID { get; set; }
        }

        private void CSTInterface_Form_Load(object sender, EventArgs e)
        {

        }
    }

}