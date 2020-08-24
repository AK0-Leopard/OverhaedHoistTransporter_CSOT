//*********************************************************************************
//      CSTInterfaceOverview_Form.cs
//*********************************************************************************
// File Name: CSTInterfaceOverview_Form.cs
// Description: CST Interface Overview Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.ohxc.winform.Properties;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NLog;

namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    public partial class CSTInterfaceOverview_Form : Form
    {

        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        List<Signal> signalList = new List<Signal>();
        List<string> vehicleList = new List<string>();
        List<Flow> flowList = new List<Flow>();
        Flow Load_Flow = null;
        Flow Unload_Flow = null;
        private static CSTInterfaceOverview_Form instance;
        #endregion 公用參數設定

        static object locl_obj = new object();
        public static CSTInterfaceOverview_Form getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                lock (locl_obj)
                {
                    if (instance == null || instance.IsDisposed)
                    {
                        instance = new CSTInterfaceOverview_Form();
                    }
                }
            }
            return instance;
        }

        private CSTInterfaceOverview_Form()
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

        public void gen_Stendard_CSTInterface_Image()
        {
            try
            {
                int[][] iArray_ULD = new int[13][];
                iArray_ULD[0] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArray_ULD[1] = new int[10] { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
                iArray_ULD[2] = new int[10] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 };
                iArray_ULD[3] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArray_ULD[4] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArray_ULD[5] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArray_ULD[6] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArray_ULD[7] = new int[10] { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 };
                iArray_ULD[8] = new int[10] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 };
                iArray_ULD[9] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 };
                iArray_ULD[10] = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                iArray_ULD[11] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArray_ULD[12] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };

                int[][] iArriy_LD = new int[13][];
                iArriy_LD[0] = new int[10] { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
                iArriy_LD[1] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArriy_LD[2] = new int[10] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 };
                iArriy_LD[3] = new int[10] { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArriy_LD[4] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArriy_LD[5] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArriy_LD[6] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                iArriy_LD[7] = new int[10] { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 };
                iArriy_LD[8] = new int[10] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 };
                iArriy_LD[9] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 };
                iArriy_LD[10] = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                iArriy_LD[11] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
                iArriy_LD[12] = new int[10] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };

                List<Signal> signals_LD = parserSignalList(iArriy_LD);
                List<Flow> flows_LD = gen_flows_with_time_period(signals_LD, 10);
                Load_Flow = flows_LD[0];

                List<Signal> signals_ULD = parserSignalList(iArray_ULD);
                List<Flow> flows_ULD = gen_flows_with_time_period(signals_ULD, 10);
                Unload_Flow = flows_ULD[0];
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }



        private async void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                await RefreshCSTInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private Task<ISearchResponse<recodevehiclecstinterface>> get_glassinterface_data_from_elastic()
        {
            DateTime search_start_time = dtp_start.Value;
            var node = new Uri("http://192.168.9.211:9200");//台中
            //var node = new Uri("http://192.168.39.111:9200");//新竹

            var settings = new ConnectionSettings(node).DefaultIndex("default");

            var client = new ElasticClient(settings);
            int size = 10000;
            SearchRequest sr = new SearchRequest("recodevehiclecstinterface*");
            search_start_time = search_start_time.Date;
            DateTime start_datetime = search_start_time;
            DateTime end_datetime = start_datetime.AddDays(1);
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
            var result = client.SearchAsync<recodevehiclecstinterface>(sr);
            return result;
        }

        private List<Signal> parserSignalList(List<recodevehiclecstinterface> resultList)
        {
            List<Signal> signals = new List<Signal>();
            try
            {
                foreach (recodevehiclecstinterface record in resultList)
                {
                    Signal signal = new Signal();
                    if (!vehicleList.Contains(record.VH_ID))
                    {
                        vehicleList.Add(record.VH_ID);
                    }
                    signal.VH_ID = record.VH_ID;
                    signal.PORT_ID = record.PORT_ID;
                    signal.record_time = record.timestamp.ToLocalTime();
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
                    signals.Add(signal);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return signals;
        }

        private List<Signal> parserSignalList(int[][] signals)
        {
            List<Signal> signalList = new List<Signal>();
            DateTime dateTime = default(DateTime);
            try
            {
                for (int i = 0; i < signals[0].Length; i++)
                {
                    Signal signal = new Signal();
                    //signal.VH_ID = vh_id;
                    dateTime = dateTime.AddMilliseconds(100);
                    signal.record_time = dateTime;
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return signalList;
        }

        private async Task RefreshCSTInfo()
        {
            try
            {
                ISearchResponse<recodevehiclecstinterface> resultList = await get_glassinterface_data_from_elastic();
                signalList.Clear();
                List<Signal> singnals = parserSignalList(resultList.Documents.ToList());

                flowList = gen_flows_with_time_period(singnals, 10);  //抓到開始特徵後，往後抓一分鐘的訊號作為流程

                int load_count = 0;
                int unload_count = 0;
                int load_unload_ok_count = 0;
                int load_unload_ng_count = 0;

                //await Task.Run(() =>
                //{
                //    foreach (var flow in flowList)
                //    {
                //        flow.startJudge();
                //    }
                //    load_count = flowList.Where(flow => flow.portType == Flow.PortType.Load).Count();
                //    unload_count = flowList.Where(flow => flow.portType == Flow.PortType.Unload).Count();
                //    load_unload_ok_count = flowList.Where(flow => flow.judgeResult == Flow.JudgeResult.OK).Count();
                //    load_unload_ng_count = flowList.Where(flow => flow.judgeResult == Flow.JudgeResult.NG).Count();
                //});


                dgv_cst_transferList.DataSource = flowList;
                lbl_load_count_value.Text = load_count.ToString();
                lbl_unload_count_value.Text = unload_count.ToString();
                lbl_loadunload_count_value.Text = (load_count + unload_count).ToString();
                lbl_ok_count_value.Text = load_unload_ok_count.ToString();
                lbl_ng_count_value.Text = load_unload_ng_count.ToString();

                //if (flowList.Count > 0)
                //{
                //    cmb_VH.DataSource = flowList.Select(flow => flow.VH_ID).Distinct().OrderBy(flow => flow).ToList();
                //}
                //else
                //{
                //    cmb_VH.DataSource = null;
                //}

                //foreach (Flow f in flowList)
                //{
                //    cmb_TransList.Items.Add(f.start_time);
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private List<Flow> gen_flows_with_time_period(List<Signal> target_signals, int time_period_sec)
        {
            // List<Signal> target_signals = signalList;
            List<Flow> flows = new List<Flow>();
            try
            {
                //foreach (Signal s in signalList)
                //{

                //    if (s.record_time >= date && s.record_time < date.AddDays(1))
                //    {
                //        target_signals.Add(s);
                //    }
                //}

                target_signals = target_signals.OrderBy(s => s.record_time).ToList();

                //var query = from flow in target_signals
                //            group flow by flow.VH_ID into grp
                //            select grp;
                //var query_groups = query.ToDictionary(grp => grp.Key.Trim(), grp => grp.OrderBy(value => value.record_time).ToList());
                //foreach (var query_group in query_groups)
                //{
                //    var target_signals_temp = query_group.Value;
                for (int i = 0; i < target_signals.Count - 1; i++)
                {
                    if (isStartSignal(target_signals[i], target_signals[i + 1], out string vh_id, out string port_id, out Flow.PortType portType))
                    {
                        DateTime ExpireTime = target_signals[i + 1].record_time.AddSeconds(time_period_sec);
                        Flow flow = new Flow();
                        flow.start_time = target_signals[i + 1].record_time;
                        flow.signals.Add(target_signals[i]);
                        flow.signals.Add(target_signals[i + 1]);
                        flow.VH_ID = vh_id;
                        flow.Port_ID = port_id;
                        flow.portType = portType;
                        int j = 1;
                        while (target_signals.Count > (i + 1 + j) && target_signals[i + 1 + j].record_time < ExpireTime)
                        {
                            flow.signals.Add(target_signals[i + 1 + j]);
                            j++;
                        }
                        flow.parse2DrawTimeChartInfo();
                        flows.Add(flow);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return flows;
        }

        private bool isStartSignal(Signal first_signal, Signal second_signal, out string vh_id, out string port_id, out Flow.PortType portType)
        {
            bool isStartSingnal = false;

            vh_id = first_signal.VH_ID;
            port_id = first_signal.PORT_ID;
            portType = default(Flow.PortType);
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

                isStartSingnal = true;
                portType = Flow.PortType.Load;
            }
            else if ((first_signal.L_REQ == 0 &&
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
              second_signal.VA == 1 &&
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
                isStartSingnal = true;
                portType = Flow.PortType.Unload;
            }

            return isStartSingnal;
        }

        private void drawCSTInterfaceTimeChart(string title, Chart draw_chart, Flow flow)
        {
            try
            {
                List<int> xLabels = new List<int>();
                List<int> xPoints = new List<int>();

                // Flow flow = flowList[dgv_cst_transferList.SelectedRows[0].Index];
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

                //InterfaceJudgeResult interfaceJudgeResult = await CheckFlowSignal(signals);
                //if (interfaceJudgeResult.result)
                //{
                //    //JudgeResultTextBox.Text = "OK";
                //    //ProbTextBox.Text = String.Format("{0:0.00%}", interfaceJudgeResult.percentage);
                //}
                //else
                //{
                //    //JudgeResultTextBox.Text = "NG";
                //    //ProbTextBox.Text = String.Format("{0:0.00%}", interfaceJudgeResult.percentage);
                //}
                for (int i = 0; i < 13; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        signals[i][j] += (12 - i) * 2;
                    }
                }




                Array.Reverse(titles);
                draw_chart.Titles.Clear();
                draw_chart.ChartAreas.Clear();
                draw_chart.Series.Clear();

                draw_chart.ChartAreas.Add("ChartArea");
                draw_chart.ChartAreas["ChartArea"].AxisY.Maximum = 25;

                draw_chart.ChartAreas["ChartArea"].AxisX.Minimum = 0;
                draw_chart.ChartAreas["ChartArea"].AxisX.Maximum = xPoints[count - 1];
                draw_chart.ChartAreas["ChartArea"].AxisX.Interval = xPoints[count - 1];
                draw_chart.ChartAreas["ChartArea"].AxisX.MajorGrid.Enabled = true;
                draw_chart.ChartAreas["ChartArea"].AxisX.MajorTickMark.Enabled = false;
                draw_chart.ChartAreas["ChartArea"].AxisY.Interval = 2;
                draw_chart.ChartAreas["ChartArea"].AxisY.MajorGrid.Enabled = false;
                draw_chart.ChartAreas["ChartArea"].AxisY.MajorTickMark.Enabled = false;

                for (int i = 0; i < 13; i++)
                {
                    draw_chart.Series.Add("Series" + (i * 2).ToString());
                    draw_chart.Series.Add("Series" + (i * 2 + 1).ToString());
                    draw_chart.Series[(i * 2)].ChartArea = "ChartArea";
                    draw_chart.Series[(i * 2 + 1)].ChartArea = "ChartArea";
                    draw_chart.Series[(i * 2)].ChartType = SeriesChartType.Range;
                    draw_chart.Series[(i * 2 + 1)].ChartType = SeriesChartType.StepLine;
                    draw_chart.Series[(i * 2)].Color = Color.LightGray;
                    draw_chart.Series[(i * 2 + 1)].Color = Color.Red;
                    draw_chart.Series[(i * 2 + 1)].BorderWidth = 4;
                    draw_chart.Series[(i * 2 + 1)].IsVisibleInLegend = false;


                    for (int j = 0; j < signals[0].Count; j++)
                    {
                        if (j != 0)
                        {
                            draw_chart.Series[(i * 2)].Points.AddXY(xPoints[j], (signals.Count - 1 - i) * 2, signals[i][j - 1]);
                        }
                        draw_chart.Series[(i * 2)].Points.AddXY(xPoints[j], (signals.Count - 1 - i) * 2, signals[i][j]);
                    }
                    draw_chart.Series["Series" + (i * 2 + 1).ToString()].Points.DataBindXY(xPoints, signals[i]);


                    draw_chart.ChartAreas["ChartArea"].AxisY.CustomLabels.Add(i * 2, i * 2 + 1, titles[i]);
                }

                draw_chart.Titles.Add(title);
                draw_chart.Titles[0].Font = this.Font;
                for (int i = 0; i < count; i++)
                {
                    draw_chart.ChartAreas["ChartArea"].AxisX.CustomLabels.Add(xPoints[i] - 500, xPoints[i] + 500, xLabels[i].ToString(), 0, LabelMarkStyle.None, GridTickTypes.Gridline);
                }


                //draw_chart.SaveImage("Chart_" + title + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png", ChartImageFormat.Png);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private async Task<InterfaceJudgeResult> CheckFlowSignal(List<List<int>> signals)
        {
            InterfaceJudgeResult interfaceJudgeResult = new InterfaceJudgeResult();
            if (signals.Count != 13)
            {
                interfaceJudgeResult.result = false;
                interfaceJudgeResult.percentage = "1";
                return interfaceJudgeResult;
            }

            if (signals[0].Count != 10)
            {
                interfaceJudgeResult.result = false;
                interfaceJudgeResult.percentage = "1";
                return interfaceJudgeResult;
            }
            List<List<int>> transpose_signals = MatrixTranspose(signals);
            APIResult result = await JsonPostAsync(transpose_signals);
            if (result.Message == "OK")
            {
                interfaceJudgeResult.result = true;
                interfaceJudgeResult.percentage = result.Percentage;
                return interfaceJudgeResult;
            }
            else
            {
                interfaceJudgeResult.result = false;
                interfaceJudgeResult.percentage = result.Percentage;
                return interfaceJudgeResult;
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
                        string url = "http://192.168.102.150:5000/CNN";
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


        private List<List<int>> MatrixTranspose(List<List<int>> matrix)
        {
            List<List<int>> result = new List<List<int>>();

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

            return result;
        }

        private static List<List<int>> signals2signalsArray(List<int> xLabels, List<int> xPoints, Flow flow, int count)
        {
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
            return signals;
        }
        public class InterfaceJudgeResult
        {
            public bool result { get; set; }
            public string percentage { get; set; }
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
            public enum PortType
            {
                Load,
                Unload
            }
            public enum JudgeResult
            {
                OK,
                NG
            }
            public string VH_ID { get; set; }
            public string Port_ID { get; set; }
            public PortType portType { get; set; }
            public string sPortType { get { return portType == PortType.Load ? "   ←" : "   →"; } }
            public DateTime start_time { get; set; }
            public string sStart_time { get { return start_time.ToString("HH:mm:ss"); } }
            public List<Signal> signals = new List<Signal>();
            public JudgeResult judgeResult { get; set; }
            public string percentage { get; set; }
            public string sJudgeResult
            {
                get
                {
                    double percentTemp = 0;

                    double.TryParse(percentage, out percentTemp);
                    percentTemp = Math.Round(percentTemp * 100, 2);
                    return $"{judgeResult}-({(percentTemp).ToString()}%)";
                }
            }
            public DrawTimeChartInfo draw_time_chart_info { get; private set; }

            public void parse2DrawTimeChartInfo()
            {
                draw_time_chart_info = signals2signalsArray(signals);
            }

            public void startJudge()
            {
                var result = CheckFlowSignal(draw_time_chart_info.signalsArray).Result;
                percentage = result.percentage;
                if (result.result)
                {
                    judgeResult = JudgeResult.OK;
                }
                else
                {
                    judgeResult = JudgeResult.NG;
                }
            }


            private DrawTimeChartInfo signals2signalsArray(List<Signal> signals)
            {
                int count = signals.Count;
                List<int> xLabels = new List<int>();
                List<int> xPoints = new List<int>();
                List<List<int>> signalsArray = new List<List<int>>();
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

                int totalExecuteTime = (int)((signals[count - 1].record_time - signals[0].record_time).TotalMilliseconds);


                for (int i = 0; i < count; i++)
                {
                    L_REQ.Add(signals[i].L_REQ);
                    U_REQ.Add(signals[i].U_REQ);
                    READY.Add(signals[i].READY);
                    VA.Add(signals[i].VA);
                    VS_0.Add(signals[i].VS_0);
                    VS_1.Add(signals[i].VS_1);
                    VALID.Add(signals[i].VALID);
                    TR_REQ.Add(signals[i].TR_REQ);
                    BUSY.Add(signals[i].BUSY);
                    COMPT.Add(signals[i].COMPT);
                    AM_AVBL.Add(signals[i].AM_AVBL);
                    HO_AVBL.Add(signals[i].HO_AVBL);
                    ES.Add(signals[i].ES);
                    xLabels.Add((int)((signals[i].record_time - signals[0].record_time).TotalMilliseconds));
                    if (i == 0)
                    {
                        xPoints.Add(0);
                        continue;
                    }
                    int transTime = (int)((signals[i].record_time - signals[i - 1].record_time).TotalMilliseconds);
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
                signalsArray.Add(L_REQ);
                signalsArray.Add(U_REQ);
                signalsArray.Add(READY);
                signalsArray.Add(VA);
                signalsArray.Add(VS_0);
                signalsArray.Add(VS_1);
                signalsArray.Add(VALID);
                signalsArray.Add(TR_REQ);
                signalsArray.Add(BUSY);
                signalsArray.Add(COMPT);
                signalsArray.Add(AM_AVBL);
                signalsArray.Add(HO_AVBL);
                signalsArray.Add(ES);

                var draw_info = new DrawTimeChartInfo()
                {
                    signalsArray = signalsArray,
                    xLabels = xLabels,
                    xPoints = xPoints
                };
                return draw_info;
            }

            private async Task<InterfaceJudgeResult> CheckFlowSignal(List<List<int>> signals)
            {
                InterfaceJudgeResult interfaceJudgeResult = new InterfaceJudgeResult();
                if (signals.Count != 13)
                {
                    interfaceJudgeResult.result = false;
                    interfaceJudgeResult.percentage = "1";
                    return interfaceJudgeResult;
                }

                if (signals[0].Count != 10)
                {
                    interfaceJudgeResult.result = false;
                    interfaceJudgeResult.percentage = "1";
                    return interfaceJudgeResult;
                }
                List<List<int>> transpose_signals = MatrixTranspose(signals);

                APIResult result = await JsonPostAsync(transpose_signals);
                if (result.Message == "OK")
                {
                    interfaceJudgeResult.result = true;
                    interfaceJudgeResult.percentage = result.Percentage;
                    return interfaceJudgeResult;
                }
                else
                {
                    interfaceJudgeResult.result = false;
                    interfaceJudgeResult.percentage = result.Percentage;
                    return interfaceJudgeResult;
                }
            }

            private List<List<int>> MatrixTranspose(List<List<int>> matrix)
            {
                List<List<int>> result = new List<List<int>>();
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
                return result;
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
                            //string url = "http://192.168.102.150:5000/CNN";
                            string url = "http://ohxc.query.mirle.com.tw:5000/CNN";
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


            public class DrawTimeChartInfo
            {
                public List<List<int>> signalsArray;
                public List<int> xLabels;
                public List<int> xPoints;
            }
            public class InterfaceJudgeResult
            {
                public bool result { get; set; }
                public string percentage { get; set; }
            }
        }
        public class APIResult
        {
            public bool Success;
            public string Message;
            public string Percentage;
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

        private void CSTInterfaceOverview_Form_Load(object sender, EventArgs e)
        {
            initialCmd();
            gen_Stendard_CSTInterface_Image();
        }

        private void initialCmd()
        {
            List<string> lstVh = new List<string>() { "" };
            List<sc.AVEHICLE> lstEq = WindownApplication.getInstance().ObjCacheManager.GetVEHICLEs();
            lstVh.AddRange(lstEq.Select(vh => vh.VEHICLE_ID).ToList());
            string[] allVh = lstVh.ToArray();
            WinFromUtility.setComboboxDataSource(cmb_VH, allVh);

            dgv_cst_transferList.AutoGenerateColumns = false;


        }


        private void dgv_cst_transferList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_cst_transferList.SelectedRows.Count == 0 || dgv_cst_transferList.SelectedRows[0].Index < 0) return;
            Flow flow = flowList[dgv_cst_transferList.SelectedRows[0].Index];
            switch (flow.portType)
            {
                case Flow.PortType.Load:
                    drawCSTInterfaceTimeChart("Standard handshake(Load)", chart2, Load_Flow);
                    break;
                case Flow.PortType.Unload:
                    drawCSTInterfaceTimeChart("Standard handshake(Unload)", chart2, Unload_Flow);
                    break;
            }
            string title = $"{flow.VH_ID}-{flow.sStart_time}-{flow.Port_ID}({flow.portType.ToString()})-{flow.sJudgeResult}";
            drawCSTInterfaceTimeChart(title, chart1, flow);
        }


        private void dgv_cst_transferList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgv_cst_transferList.Rows.Count <= e.RowIndex) return;
            if (e.RowIndex < 0) return;
            if (flowList.Count == 0) return;
            DataGridViewRow row = dgv_cst_transferList.Rows[e.RowIndex];
            if (flowList[e.RowIndex].judgeResult == Flow.JudgeResult.NG)
                row.DefaultCellStyle.BackColor = Color.Red;
        }
    }


}
