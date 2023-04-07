using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayBinder
{
    public partial class TrayBinder : Form
    {
        public string UNITID;

        public TrayBinder()
        {
            InitializeComponent();
            UNITID = "TrayBinder";

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\TrayBinder";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "TrayBinder";


            InitControl();
        }

        private void InitControl()
        {
            // Model List
            InitModelID();
            // model/route/Next process 모두 첫번째것을 선택한다.
            TrayZone_comboBox.SelectedIndex = 0;

            initCellDataGridView();

        }

        private void initCellDataGridView()
        {
            DataTable CellInitData = new DataTable();
            CellInitData.Columns.Add("Cell No", typeof(int));
            CellInitData.Columns.Add("Cell ID", typeof(string));
            CellInitData.Columns.Add("Lot ID", typeof(string));

            CellInitData.Columns.Add("NGCode", typeof(string));
            CellInitData.Columns.Add("NGType", typeof(string));
            for (int i=1; i<=30; i++)
            {
                DataRow row = CellInitData.NewRow();
                row["Cell No"] = i;
                CellInitData.Rows.Add(row);
            }

            Cell_dataGridView.DataSource = CellInitData;
            Cell_dataGridView.Columns[0].Width = 70;
            Cell_dataGridView.Columns[1].Width = 300;
            Cell_dataGridView.Columns[2].Width = 150;
            Cell_dataGridView.Columns[3].Width = 100;
            Cell_dataGridView.Columns[4].Width = 100;

            Cell_dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            Cell_dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            Cell_dataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            Cell_dataGridView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            Cell_dataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            Cell_dataGridView.Columns[0].ReadOnly = true;

        }

        private void InitModelID()
        {
            _jsonMstModelResponse ModelList = RESTClientBiz.GetRestModelInformation(UNITID);
            // ComboBox로 
            Model_comboBox.DataSource = null;
            Model_comboBox.Items.Clear();
            Model_comboBox.DisplayMember = "Name";
            Model_comboBox.ValueMember = "Value";

            if (ModelList != null && ModelList.DATA.Count > 0)
            {
                DataTable Modeldt = new DataTable();

                Modeldt.Columns.Add("Name");
                Modeldt.Columns.Add("Value");

                foreach(_mst_model model in ModelList.DATA)
                {
                    Modeldt.Rows.Add(model.MODEL_ID, model.MODEL_ID);
                }

                Model_comboBox.DataSource= Modeldt;
                if(Modeldt.Rows.Count > 0)
                    Model_comboBox.SelectedIndex = 0;
            }
        }

        private void InitRoute(string ModelID)
        {
            _jsonMstRouteResponse RouteList = RESTClientBiz.GetRestRouteInformation(ModelID, UNITID);

            Route_comboBox.DataSource = null;
            Route_comboBox.Items.Clear();
            Route_comboBox.DisplayMember = "Name";
            Route_comboBox.ValueMember = "Value";

            if (RouteList != null && RouteList.DATA.Count > 0)
            {
                DataTable Modeldt = new DataTable();

                Modeldt.Columns.Add("Name");
                Modeldt.Columns.Add("Value");

                foreach (_mst_route route in RouteList.DATA)
                {
                    Modeldt.Rows.Add(route.ROUTE_ID, route.ROUTE_ID);
                }

                Route_comboBox.DataSource = Modeldt;
                if (Modeldt.Rows.Count > 0)
                    Route_comboBox.SelectedIndex = 0;
            }
        }

        private void InitNextProcess(string ModelID, string RouteID)
        {
            NextProcess_comboBox.DataSource = null;
            NextProcess_comboBox.Items.Clear();
            NextProcess_comboBox.DisplayMember = "Name";
            NextProcess_comboBox.ValueMember = "Value";


            string processListJson = string.Empty;
            string SelectedModelID = Model_comboBox.SelectedValue as string;
            string SelectedRouteID = Route_comboBox.SelectedValue as string;
            _jsonMstRouteResponse RouteList = RESTClientBiz.GetRestRouteInformation(SelectedModelID, UNITID);

            if(RouteList != null && RouteList.DATA.Count>0)
            {
                _mst_route selectedRoute = RouteList.DATA.Find(x => x.ROUTE_ID == SelectedRouteID);
                if(selectedRoute != null)
                {
                    processListJson = selectedRoute.JSON_DATA;

                    List<JObject> processObject = JsonConvert.DeserializeObject<List<JObject>>(processListJson);

                    DataTable Processdt = new DataTable();
                    Processdt.Columns.Add("Name");
                    Processdt.Columns.Add("Value");

                    foreach (JObject obj in processObject)
                    {
                        string ProcessName = obj["process_name".ToUpper()].ToString();
                        string ProcessID = obj["eqp_type".ToUpper()].ToString()+obj["process_type".ToUpper()].ToString()+obj["process_no".ToUpper()].ToString();
                        Processdt.Rows.Add(ProcessName, ProcessID);

                    }

                    NextProcess_comboBox.DataSource = Processdt;
                }

            }
        }

        private void Model_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Route 콤보 초기화
            string SelectedModelID = Model_comboBox.SelectedValue as string;
            InitRoute(SelectedModelID);

        }

        private void Route_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // NextProcess 콤보를 초기화 해야 함.
            string SelectedModelID = Model_comboBox.SelectedValue as string;
            string SelectedRouteID = Route_comboBox.SelectedValue as string;

            InitNextProcess(SelectedModelID, SelectedRouteID);
        }

        private void Create_button_Click(object sender, EventArgs e)
        {
            // setTrayInformation REST API 호출함.
            // https://<fms_server_name>/mes/setTrayInformation
            // http://<server_name>/ecs/manualTrayCellInput
            _jsonEcsApiSetTrayInformationRequest request = new _jsonEcsApiSetTrayInformationRequest();
            _jsonEcsApiCreateTrayInformationRequest CTErequest = new _jsonEcsApiCreateTrayInformationRequest();
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            request.ACTION_ID = "SET_TRAY_INFORMATION";
            CTErequest.ACTION_ID = "CREATE_TRAY_INFORMATION";
            request.REQUEST_TIME = dateTime;
            CTErequest.REQUEST_TIME = dateTime;

            request.ACTION_USER = "TrayBinder";
            CTErequest.ACTION_USER = "TrayBinder";

            string TrayID = TrayID_textBox.Text.Trim();
            if(TrayID.Length == 0)
            {
                AlertWindow("Input TrayID");
                return;
            }
            request.TRAY_ID = TrayID;
            CTErequest.TRAY_ID = TrayID;


            // EmptyTray생성은 http://<server_name>/ecs/createEmptyTray
            if (CreateEmptyTray_checkbox.Checked)
            {
                _jsonCreateEmptyTrayResponse CE_response = RESTClientBiz.CallEcsApiCreateEmptyTray(TrayID, TrayZone_comboBox.Text);
                if (CE_response == null)
                {
                    AlertWindow("Fail to call REST API, createEmptyTray");
                    return;
                }

                if (CE_response.RESPONSE_CODE == "200")
                {
                    AlertWindow("Create Empty Tray Success!!");
                }
                else
                {
                    AlertWindow($"RESPONSE_CODE={CE_response.RESPONSE_CODE}:{CE_response.RESPONSE_MESSAGE}", "Error", MessageBoxIcon.Error);

                }


                return;
            }

            // Empty Tray 처리
            if(SetTrayEmpty_checkbox.Checked)
            {
                _jsonSetTrayEmptyResponse STEresponse = RESTClientBiz.CallEcsApiSetTrayEmpty(TrayID);
                if (STEresponse == null)
                {
                    AlertWindow("Fail to call REST API, setTrayEmpty");
                    return;
                }

                if (STEresponse.RESPONSE_CODE == "200")
                {
                    AlertWindow("Set Tray Empty Success!!");
                }
                else
                {
                    AlertWindow($"RESPONSE_CODE={STEresponse.RESPONSE_CODE}:{STEresponse.RESPONSE_MESSAGE}", "Error", MessageBoxIcon.Error);
                }


                return;
            }


            string ModelId = Model_comboBox.SelectedValue.ToString();
            if (ModelId.Length == 0)
            {
                AlertWindow("select Model ID");
                return;
            }
            request.MODEL_ID = ModelId;
            CTErequest.MODEL_ID = ModelId;


            string RouteId = Route_comboBox.SelectedValue.ToString();
            if (RouteId.Length == 0)
            {
                AlertWindow("select Route ID");
                return;
            }
            request.ROUTE_ID = RouteId;
            CTErequest.ROUTE_ID = RouteId;

            string ProcessId = NextProcess_comboBox.SelectedValue.ToString();
            if (ProcessId.Length == 0)
            {
                AlertWindow("select Next Process");
                return;
            } else if(ProcessId.Length <7)
            {
                AlertWindow($"something is wroing in Process ID[{ProcessId}]");
                return;
            }

            //LotID
            request.LOT_ID = LotID_textBox.Text;
            CTErequest.LOT_ID = LotID_textBox.Text;

            string EQP_TYPE = ProcessId.Substring(0, 3);
            string PROCESS_TYPE = ProcessId.Substring(3, 3);
            string PROCESS_NO = ProcessId.Substring(6);

            //request.NEXT_ROUTE_ORDER_NO = NextProcess_comboBox.SelectedIndex.ToString();
            request.NEXT_ROUTE_ORDER_NO = NextProcess_comboBox.SelectedIndex+1; // +1맞나?
            request.NEXT_EQP_TYPE = EQP_TYPE;
            request.NEXT_PROCESS_TYPE = PROCESS_TYPE;
            request.NEXT_PROCESS_NO = int.Parse(PROCESS_NO);

            CTErequest.NEXT_ROUTE_ORDER_NO = NextProcess_comboBox.SelectedIndex + 1; // +1맞나?
            CTErequest.NEXT_EQP_TYPE = EQP_TYPE;
            CTErequest.NEXT_PROCESS_TYPE = PROCESS_TYPE;
            CTErequest.NEXT_PROCESS_NO = int.Parse(PROCESS_NO);

            // Tray Zone
            request.TRAY_ZONE = TrayZone_comboBox.Text;
            CTErequest.TRAY_ZONE = TrayZone_comboBox.Text;


            // string.Empty
            request.NEXT_EQP_ID = String.Empty;
            request.NEXT_UNIT_ID = String.Empty;

            CTErequest.NEXT_EQP_ID = String.Empty;
            CTErequest.NEXT_UNIT_ID = String.Empty;


            request.CELL_LIST = new List<Cell_Basic_Info>();
            CTErequest.CELL_LIST = new List<Cell_Basic_Info>();

            // Cell Counting해보자. 
            int CellCount = 0;
            string CellId = string.Empty;
            string LotId = string.Empty;
            for (int rowIndex = 0; rowIndex < 30; rowIndex++)
            {
                int columnIndex = 1;
                CellId = Cell_dataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString().Trim();
                columnIndex = 2;
                LotId =  Cell_dataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString().Trim();

                if(CellId.Length>0 && LotId.Length>0)
                {
                    CellCount++;
                    Cell_Basic_Info  Cell = new Cell_Basic_Info();
                    Cell.CELL_POSITION = rowIndex + 1;
                    Cell.CELL_ID = CellId;
                    Cell.LOT_ID = LotId;
                    Cell.CELL_EXIST = "Y";

                    request.CELL_LIST.Add(Cell);
                    CTErequest.CELL_LIST.Add(Cell);
                }
            }

            request.CELL_COUNT = CellCount;
            CTErequest.CELL_COUNT = CellCount;

            // request 호출해서 
            // https://210.91.148.176:30011/ecs/setTrayInformation  호출하자... 
            // setTrayInformation 은 Cell 정보가 이미 FMS에 있어야 한다.
            // Cell 정보를 새로 만드려면, 2.26	createTrayInformation 을 사용한다.

            if (CreateCell_checkbox.Checked)
            {
                // Cell 정보를 새로 insert 하면서 Tray 만듦
                // setTrayInformation 을 위한 class와 멤버가 같으니깐 같이 쓰면 됨.
                _jsonEcsApiCreateTrayInformationResponse response = RESTClientBiz.CallEcsApiCreateTrayInformation(CTErequest, UNITID);

                if (response == null)
                {
                    AlertWindow("Fail to call REST API, CREATE_TRAY_INFORMATION");
                    return;
                }


                if (response.RESPONSE_CODE == "200")
                {
                    AlertWindow("Create Success!!");
                }
                else
                {
                    AlertWindow($"RESPONSE_CODE={response.RESPONSE_CODE}:{response.RESPONSE_MESSAGE}", "Error", MessageBoxIcon.Error);

                }
            }
            else
            {
                // Cell 정보가 이미 FMS에 있을때
                _jsonEcsApiSetTrayInformationResponse response = RESTClientBiz.CallEcsApiSetTrayInformation(request, UNITID);

                if (response == null)
                {
                    AlertWindow("Fail to call REST API, SET_TRAY_INFORMATION");
                    return;
                }


                if (response.RESPONSE_CODE == "200")
                {
                    AlertWindow("Create Success!!");
                }
                else
                {
                    AlertWindow($"RESPONSE_CODE={response.RESPONSE_CODE}:{response.RESPONSE_MESSAGE}", "Error", MessageBoxIcon.Error);

                }
            }

        }

        private void AlertWindow(string msg, string msgBoxName = null, MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information)
        {
            MessageBox.Show(msg, msgBoxName, MessageBoxButtons.OK,messageBoxIcon);
        }

        private void CreateCellId_button_Click(object sender, EventArgs e)
        {
            string TrayId = TrayID_textBox.Text.Trim();
            if(TrayId.Length < 1)
            {
                AlertWindow("input TrayId before create Cell ID");
                return;
            }

            for(int rowIndex=0; rowIndex < 30; rowIndex++)
            {
                int columnIndex = 1;
                Cell_dataGridView.Rows[rowIndex].Cells[columnIndex].Value = TrayId + $"_Cell_{string.Format("{0:D2}", rowIndex+1)}";
            }
        }

        private void Reset_button_Click(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < 30; rowIndex++)
            {
                for(int columnIndex=1; columnIndex <=2; columnIndex++)
                {
                    Cell_dataGridView.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                }
            }
        }

        private void LotId_button_Click(object sender, EventArgs e)
        {
            string LotId = LotID_textBox.Text.Trim();
            if(LotId.Length <1)
            {
                AlertWindow("input Lot ID");
                return;
            }
            for (int rowIndex = 0; rowIndex < 30; rowIndex++)
            {
                int columnIndex = 2;
                if(Cell_dataGridView.Rows[rowIndex].Cells[columnIndex-1].Value.ToString().Trim().Length > 0)
                    Cell_dataGridView.Rows[rowIndex].Cells[columnIndex].Value = LotId;

            }

        }

        private void normal_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(normal_checkbox.Checked)
            {
                CreateEmptyTray_checkbox.Checked = false;
                SetTrayEmpty_checkbox.Checked = false;
            }
        }

        private void CreateEmptyTray_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(CreateEmptyTray_checkbox.Checked)
            {
                normal_checkbox.Checked = false;
                SetTrayEmpty_checkbox.Checked = false;
            }
        }

        private void SetTrayEmpty_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(SetTrayEmpty_checkbox.Checked)
            {
                normal_checkbox.Checked = false;
                CreateEmptyTray_checkbox.Checked = false;
            }
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            try
            {
                // TrayID 검색해서 정보 담자
                _jsonDatTrayResponse TrayData;
                _jsonDatCellResponse TrayCellData;
                TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayID_textBox.Text, "TrayBinder");
                TrayCellData = RESTClientBiz.GetRestCellInfoByTrayId(TrayID_textBox.Text, "TrayBinder");

                if (TrayData != null && TrayData.DATA != null && TrayData.DATA.Count > 0)
                {
                    //Model
                    if (TrayData.DATA[0].MODEL_ID != null && TrayData.DATA[0].MODEL_ID.Length > 0)
                    {
                        Model_comboBox.Text = TrayData.DATA[0].MODEL_ID.Trim();
                        //Rotue
                        Route_comboBox.Text = TrayData.DATA[0].ROUTE_ID.Trim();
                        //NextProcess
                        string EQPType = TrayData.DATA[0].NEXT_EQP_TYPE.Trim();
                        string ProcessType = TrayData.DATA[0].NEXT_PROCESS_TYPE.Trim();
                        int ProcessNo = TrayData.DATA[0].NEXT_PROCESS_NO;
                        string ProcessId = EQPType + ProcessType + string.Format("{0:D3}", ProcessNo);
                        if (ProcessId.Length == 9)
                            NextProcess_comboBox.SelectedValue = ProcessId;
                        //LotID
                        LotID_textBox.Text = TrayData.DATA[0].LOT_ID.Trim();
                        //TrayZone
                        TrayZone_comboBox.Text = TrayData.DATA[0].TRAY_ZONE.Trim();

                        // Cell Information
                        WriteDataGridView(TrayCellData);





                    } // Model 이 없으면 의미 없다.
                    else
                    {
                        TrayZone_comboBox.Text = TrayData.DATA[0].TRAY_ZONE.Trim();

                    }

                }
            } catch(Exception ex)
            {
                AlertWindow($"Fail to Search Tray: {ex.Message}", "Error", MessageBoxIcon.Error);
            }


        }

        public void WriteDataGridView(_jsonDatCellResponse TrayCellData)
        {
            initCellDataGridView();

            //Header 
            DataTable CellInitData = new DataTable();

            CellInitData.Columns.Add("Cell No", typeof(int));
            CellInitData.Columns.Add("Cell ID", typeof(string));
            CellInitData.Columns.Add("Lot ID", typeof(string));
            CellInitData.Columns.Add("NGCode", typeof(string));
            CellInitData.Columns.Add("NGType", typeof(string));

            int CellDataIndex = 0;

            for (int i = 0; i < 30; i++)
            {

                DataRow row = CellInitData.NewRow();

                if(i == TrayCellData.DATA[CellDataIndex].CELL_NO -1)
                {
                    row["Cell No"] = i + 1;
                    row["Cell ID"] = TrayCellData.DATA[CellDataIndex].CELL_ID != null ? TrayCellData.DATA[CellDataIndex].CELL_ID : string.Empty;
                    row["Lot ID"] = TrayCellData.DATA[CellDataIndex].LOT_ID != null ? TrayCellData.DATA[CellDataIndex].LOT_ID : string.Empty;
                    row["NGCode"] = TrayCellData.DATA[CellDataIndex].GRADE_CODE != null ? TrayCellData.DATA[CellDataIndex].GRADE_CODE : string.Empty;
                    row["NGType"] = TrayCellData.DATA[CellDataIndex].GRADE_NG_TYPE != null ? TrayCellData.DATA[CellDataIndex].GRADE_NG_TYPE : string.Empty;
                    CellDataIndex++;
                } else
                {
                    row["Cell No"] = i + 1;
                    row["Cell ID"] = string.Empty;
                    row["Lot ID"] = string.Empty;
                    row["NGCode"] = string.Empty;
                    row["NGType"] = string.Empty;
                }

                CellInitData.Rows.Add(row);
            }
            Cell_dataGridView.DataSource = CellInitData;

        }

        private void TrayID_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_button_Click(this, null);
            }
        }
    }
}
