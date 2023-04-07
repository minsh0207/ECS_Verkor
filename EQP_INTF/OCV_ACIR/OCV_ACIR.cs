using CommonCtrls;
using OPCUAClient;
using RestAPIServer;
using RestClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.OCV_ACIR
{
    public partial class OCV_ACIR : UserControl
    {
        #region EQP Basic Information
        string _EQPType;
        [DisplayName("EQP Type"), Description("Equipment Type"), Category("EQP Basic Information")]
        public string EQPType
        {
            get { return _EQPType; }
            set { _EQPType = value; }
        }
        string _EQPID;
        [DisplayName("EQP ID"), Description("Equipment ID"), Category("EQP Basic Information")]
        public string EQPID
        {
            get { return _EQPID; }
            set { _EQPID = value; }
        }
        string _UNITID;
        [DisplayName("UNIT ID"), Description("Unit ID"), Category("EQP Basic Information")]
        public string UNITID
        {
            get { return _UNITID; }
            set { _UNITID = value; }
        }
        #endregion

        #region EQP OPCUA Setting
        string _GroupName;
        [DisplayName("GroupName"), Description("Group Name"), Category("EQP OPCUA Client Setting")]
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        string _Endpoint;
        [DisplayName("Endpoint"), Description("Endpoint for EQP OPCUA Server"), Category("EQP OPCUA Client Setting")]
        public string Endpoint
        {
            get { return _Endpoint; }
            set { _Endpoint = value; }
        }
        string _ID;
        [DisplayName("ID"), Description("ID for OPCUA Server"), Category("EQP OPCUA Client Setting")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _Password;
        [DisplayName("Password"), Description("Password for OPCUA Server"), Category("EQP OPCUA Client Setting")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        string _StartingNodeId;
        [DisplayName("Starting NodeId"), Description("NodeId for starting browse. ex)ns=2;i=22879"), Category("EQP OPCUA Client Setting")]
        public string StartingNodeId
        {
            get { return _StartingNodeId; }
            set { _StartingNodeId = value; }
        }
        // 이후 모든 browsePath 는 Starting Node 이후의 경로만 표시 가능함.
        //int _NamespaceIndex;
        //[DisplayName("NamespaceIndex"), Description("Namespace Index"), Category("EQP OPCUA Client Setting")]
        //public int NamespaceIndex
        //{
        //    get { return _NamespaceIndex; }
        //    set { _NamespaceIndex = value; }
        //}
        #endregion

        #region FMS OPCUA Setting
        string _FmsGroupName;
        [DisplayName("GroupName"), Description("Group Name"), Category("FMS OPCUA Client Setting")]
        public string FmsGroupName
        {
            get { return _FmsGroupName; }
            set { _FmsGroupName = value; }
        }
        string _FmsEndpoint;
        [DisplayName("Endpoint"), Description("Endpoint for EQP OPCUA Server"), Category("FMS OPCUA Client Setting")]
        public string FmsEndpoint
        {
            get { return _FmsEndpoint; }
            set { _FmsEndpoint = value; }
        }
        string _FmsID;
        [DisplayName("ID"), Description("ID for OPCUA Server"), Category("FMS OPCUA Client Setting")]
        public string FmsID
        {
            get { return _FmsID; }
            set { _FmsID = value; }
        }
        string _FmsPassword;
        [DisplayName("Password"), Description("Password for OPCUA Server"), Category("FMS OPCUA Client Setting")]
        public string FmsPassword
        {
            get { return _FmsPassword; }
            set { _FmsPassword = value; }
        }

        string _FmsStartingNodeId;
        [DisplayName("Starting NodeId"), Description("NodeId for starting browse. ex)ns=2;i=22879"), Category("FMS OPCUA Client Setting")]
        public string FmsStartingNodeId
        {
            get { return _FmsStartingNodeId; }
            set { _FmsStartingNodeId = value; }
        }
        // 이후 모든 browsePath 는 Starting Node 이후의 경로만 표시 가능함.
        //int _FmsNamespaceIndex;
        //[DisplayName("NamespaceIndex"), Description("Namespace Index"), Category("FMS OPCUA Client Setting")]
        //public int FmsNamespaceIndex
        //{
        //    get { return _FmsNamespaceIndex; }
        //    set { _FmsNamespaceIndex = value; }
        //}

        string _FmsMesAliveNodeId;
        [DisplayName("MES Alive NodeId"), Description("NodeId for MES Alive ex)ns=2;i=22879"), Category("FMS OPCUA Client Setting")]
        public string FmsMesAliveNodeId
        {
            get { return _FmsMesAliveNodeId; }
            set { _FmsMesAliveNodeId = value; }
        }

        int _FmsMesResponseTimeOut = 5;
        [DisplayName("MES Response TimeOut"), Description("MES Response TimeOut in second"), Category("FMS OPCUA Client Setting")]
        public int FmsMesResponseTimeOut
        {
            get { return _FmsMesResponseTimeOut; }
            set { _FmsMesResponseTimeOut = value; }
        }

        #endregion

        #region REST Server Setting
        int _Port;
        [DisplayName("Port"), Description("Server Port"), Category("REST API Server Setting")]
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        #endregion



        private ApplicationInstance m_application = null;

        // MES의 응답을 기다리는 Timer 및 Queue에 사용할 구조체
        private static System.Timers.Timer MesResponseTimer = null;
        // 응답받을 Tag (browseName : startNode 이후의 full path) 를 등록해놓을까?
        // 이건 Async로 올수도 있나? 설비 Unit별로 sync로  진행하면 되나? 
        private static MesResponseTimerClass MesResponseWaitItem = null;

        public bool WaitMesResponse(string ResponsePath, List<object> parameters = null)
        {
            if (MesResponseWaitItem != null)
            {
                _LOG_($"[{this.Name}] MesResponseWaitItem is not null :[{MesResponseWaitItem.ResponsePath}:{MesResponseWaitItem.RequestTime}] ", ECSLogger.LOG_LEVEL.WARN);
                MesResponseWaitItem = null;
            }

            MesResponseWaitItem = new MesResponseTimerClass();
            MesResponseWaitItem.ResponsePath = ResponsePath;
            DateTime dateTime = DateTime.Now;
            MesResponseWaitItem.RequestTime = dateTime;
            if (parameters != null)
                MesResponseWaitItem.parameters = parameters;

            if (FmsMesResponseTimeOut <= 0)
            {
                _LOG_($"[{this.Name}] MesResponseTimeOut is below 0", ECSLogger.LOG_LEVEL.WARN);
                return false;
            }

            RunMesResponseTimer();

            _LOG_($"[{this.Name}] Start to wait Response from MES [{ResponsePath}] : Request Time({dateTime}) Wait {FmsMesResponseTimeOut} sec.");

            return true;
        }

        public void RunMesResponseTimer()
        {
            MesResponseTimer = null;
            MesResponseTimer = new System.Timers.Timer();
            MesResponseTimer.Interval = FmsMesResponseTimeOut * 1000; // milli-sec 에서 sec로 변환
            MesResponseTimer.Elapsed += MES_Response_TimeOut;
            MesResponseTimer.Start();
        }

        private void MES_Response_TimeOut(object sender, ElapsedEventArgs e)
        {
            _LOG_($"[{this.Name}] TimeOut for MES Response on [{MesResponseWaitItem.ResponsePath}] Elapsed Time : {e.SignalTime - MesResponseWaitItem.RequestTime}", ECSLogger.LOG_LEVEL.WARN);

            MesResponseTimer.Stop();
            MesResponseTimer = null;

            if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.TrayLoadResponse")
                FMSSequenceTrayLoadResponseTimeOut();
            else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.RequestRecipeResponse")
                FMSSequenceRequestRecipeResponseTimeOut();
            else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.ProcessStartResponse")
                FMSSequenceProcessStartResponseTimeOut();
            else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.ProcessEndResponse")
                FMSSequenceProcessEndResponseTimeOut();
            else
            {
                _LOG_($"[{this.Name}] Fail to find case of Mes Response Timeout [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }


        public OCV_ACIR()
        {
            InitializeComponent();
        }

        public void InitControl(ApplicationInstance applicationInstance)
        {
            DataClass.EQP_TYPE = this.EQPType;
            DataClass.EQP_ID = this.EQPID;
            DataClass.UNIT_ID = this.UNITID;
            DataClass.EQPClient = this.EQPClient;
            DataClass.CommandPath = "EquipmentControl";

            m_application = applicationInstance;

            EQPClient.GroupName = this.GroupName;
            EQPClient.Endpoint = this.Endpoint;
            EQPClient.ID = this.ID;
            EQPClient.Password = this.Password;
            EQPClient.StartingNodeId = this.StartingNodeId;
            //EQPClient.NamespaceIndex = this.NamespaceIndex;
            // EQP 기본정보
            EQPClient.EQPType = this.EQPType;
            EQPClient.EQPID = this.EQPID;
            EQPClient.UNITID = this.UNITID;

            EQPClient.InitControl(m_application);
            EQPClient.m_ConnectData.Session.ConnectionStatusUpdate += Session_ConnectionStatusUpdate_EQPClient;

            _LOG_("=============================================================================================================");
            _LOG_($"Init EQPClient Completed : {GroupName}:{Endpoint} / StartingNodeId : {StartingNodeId}");


            FMSClient.GroupName = this.FmsGroupName;
            FMSClient.Endpoint = this.FmsEndpoint;
            FMSClient.ID = this.FmsID;
            FMSClient.Password = this.FmsPassword;
            FMSClient.StartingNodeId = this.FmsStartingNodeId;
            //FMSClient.NamespaceIndex = this.FmsNamespaceIndex;

            FMSClient.MesAliveNodeId = this.FmsMesAliveNodeId;

            // EQP 기본정본보 FMS에서도 어떤 설비에 대한 것인지 알아야 하기 때문에 EQPClient와  동일하게
            FMSClient.EQPType = this.EQPType;
            FMSClient.EQPID = this.EQPID;
            FMSClient.UNITID = this.UNITID;


            FMSClient.InitControl(m_application);
            FMSClient.m_ConnectData.Session.ConnectionStatusUpdate += Session_ConnectionStatusUpdate_FMSClient;

            _LOG_($"Init FMSClient Completed : {FmsGroupName}:{FmsEndpoint} / StartingNodeId : {FmsStartingNodeId}");

            RestApiServer.Port = this.Port;

        }

        public void StartECS_OCV()
        {
            EQPClient.Start();
            FMSClient.Start();
            RestApiServer.Start();

        }
        public void StopECS_OCV()
        {
            EQPClient.Stop();
            FMSClient.Stop();
            RestApiServer.Stop();

        }

        private void Session_ConnectionStatusUpdate_FMSClient(Session sender, ServerConnectionStatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ServerConnectionStatusUpdateEventHandler(Session_ConnectionStatusUpdate_FMSClient), sender, e);
                return;
            }

            switch (e.Status)
            {
                // 만약에 Disconnect면 subscryption delete
                case ServerConnectionStatus.Disconnected:
                    _LOG_("FMSClient, ServerConnectionStatus:Disconnected");
                    break;
                // Connect면 이면 subscryption careate
                case ServerConnectionStatus.Connected:
                    _LOG_("FMSClient, ServerConnectionStatus:Connected");

                    CreateDataMonitoredItem(FMSClient);
                    break;
                case ServerConnectionStatus.ConnectionErrorClientReconnect:
                    _LOG_("FMSClient, ServerConnectionStatus:ConnectionErrorClientReconnect");

                    break;
            }
        }

        private void Session_ConnectionStatusUpdate_EQPClient(Session sender, ServerConnectionStatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ServerConnectionStatusUpdateEventHandler(Session_ConnectionStatusUpdate_EQPClient), sender, e);
                return;
            }

            switch (e.Status)
            {
                // 만약에 Disconnect면 subscryption delete
                case ServerConnectionStatus.Disconnected:
                    _LOG_("EQPClient, ServerConnectionStatus:Disconnected");
                    break;
                // Connect면 이면 subscryption careate
                case ServerConnectionStatus.Connected:
                    _LOG_("EQPClient, ServerConnectionStatus:Connected");
                    //_LOG_("EQPClient, Register Monitored Items");
                    CreateDataMonitoredItem(EQPClient);
                    //초기화면 구축
                    _LOG_("EQPClient, Init Display");
                    InitDisplayWithCurrentValue(EQPClient);
                    _LOG_("EQPClient, Init Display Complete");
                    break;
                case ServerConnectionStatus.ConnectionErrorClientReconnect:
                    _LOG_("EQPClient, ServerConnectionStatus:ConnectionErrorClientReconnect");
                    //초기화면 구축
                    //_LOG_("EQPClient, Init Display");
                    //InitDisplayWithCurrentValue(EQPClient);
                    break;

            }
        }

        //private void CreateDataMonitoredItem(OPCUAClient.OPCUAClient Client)
        //{
        //    try
        //    {
        //        // 각 connection에 subscription 해야 함.
        //        if (Client.Equals(EQPClient))
        //        {
        //            // EQP Server
        //            if (EQPClient.m_ConnectData.Subscription == null || EQPClient.m_ConnectData.Subscription.ConnectionStatus == SubscriptionConnectionStatus.Deleted)
        //            {
        //                _LOG_("EQPClient, Register Monitored Items");
        //                EQPClient.CreateSubscription();
        //                EQPClient.m_ConnectData.Subscription.DataChanged += EQP_Subscription_DataChanged;

        //                // EQP MonitoredItem 등록
        //                CreateEQPMonitoredItem();
        //            }
        //            else
        //            {
        //                _LOG_("[EQPClient] Reconnected.. ");
        //                //EQPClient.m_ConnectData.FirstEventFlag = true;
        //            }
        //        }
        //        else if (Client.Equals(FMSClient))
        //        {
        //            // FMS Server
        //            if (FMSClient.m_ConnectData.Subscription == null || FMSClient.m_ConnectData.Subscription.ConnectionStatus == SubscriptionConnectionStatus.Deleted)
        //            {
        //                _LOG_("FMSClient, Register Monitored Items");
        //                FMSClient.CreateSubscription();
        //                FMSClient.m_ConnectData.Subscription.DataChanged += FMS_Subscription_DataChanged;
        //                //_LOG_($"*****************{FMSClient.m_ConnectData.Subscription.ConnectionStatus.ToString()}");
        //                // FMS MonitoredItem 등록
        //                CreateFMSMonitoredItem();
        //            }
        //            else
        //            {
        //                _LOG_("[FMSClient] Reconnected.. ");
        //                FMSClient.m_ConnectData.FirstEventFlag = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
        //    }
        //}

        private void CreateDataMonitoredItem(OPCUAClient.OPCUAClient Client)
        {
            try
            {
                // 20230327 msh : 통신 재연결시 BrowseFromStartNode 시작하도록 변경
                Client.InitBrowseNodeList();

                //20230315 KJY - 여기서 Browse Node를 check하고 count가 0이면 Browse하는것으로 해야 겠다.
                // 20230327 msh : NodeList -> ObjectNodeList 수정
                if (Client.m_browseNodeList.ObjectNodeList.Count == 0)
                {
                    Client.UpdatelbStatusText("Browsing... wait a minute");
                    _LOG_($"[{Client.Name}] Start to Browse All Nodes");
                    Client.BrowseFromStartNode(NodeId.Parse(Client.StartingNodeId), null, 1);
                    //_LOG_($"[{Client.Name}] Complete to Browse All Nodes : {Client.m_browseNodeList.NodeList.Count} nodes");
                    // 20230324 msh : OPCUA Browsing 속도 개선.
                    _LOG_($"[{this.Name}] Complete to Browse All Object Nodes : {Client.m_browseNodeList.ObjectNodeList.Count} nodes");
                    Client.UpdatelbStatusText("Connected");
                }

                // 20230324 msh : OPCUA Browsing 속도 개선
                if (Client.Equals(EQPClient)) // for EQP OPCUA Server
                {
                    EQPClient.m_monitoredItemList.Clear();
                    SetEQPMonitoredItemList();

                    AddMonitoredBrowsePath(EQPClient.m_monitoredItemList, Client);

                    AddEQPBrowsePath(Client);

                    AddRecipeBrowsePath(Client, MappingDirection.EqpToFms);

                    AddProcessDataBrowsePath(Client, MappingDirection.EqpToFms);
                }
                else // for FMS OPCUA Server
                {
                    FMSClient.m_monitoredItemList.Clear();
                    SetFMSMonitoredItemList();
                    
                    AddMonitoredBrowsePath(FMSClient.m_monitoredItemList, Client);

                    AddFMSBrowsePath(Client);

                    AddRecipeBrowsePath(Client, MappingDirection.FmsToEqp);

                    AddProcessDataBrowsePath(Client, MappingDirection.FmsToEqp);
                }

                //20230315 소스정리
                if (Client.m_ConnectData.Subscription == null || Client.m_ConnectData.Subscription.ConnectionStatus == SubscriptionConnectionStatus.Deleted)
                {
                    _LOG_($"[{Client.Name}], Start to register Monitored Items");
                    Client.CreateSubscription();
                    if (Client.Equals(EQPClient)) // for EQP OPCUA Server
                    {
                        Client.m_ConnectData.Subscription.DataChanged += EQP_Subscription_DataChanged;
                        CreateEQPMonitoredItem();
                    }
                    else // for FMS OPCUA Server
                    {
                        Client.m_ConnectData.Subscription.DataChanged += FMS_Subscription_DataChanged;
                        CreateFMSMonitoredItem();
                    }
                    // EQP MonitoredItem 등록

                    _LOG_($"[{Client.Name}], Complete to register Monitored Items");
                }
                else
                {
                    _LOG_($"[{Client.Name}] Reconnected.. ");
                }

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMS_Subscription_DataChanged(Subscription subscription, DataChangedEventArgs e)
        {
            if (FMSClient.m_ConnectData.FirstEventFlag == true)
            {
                FMSClient.m_ConnectData.FirstEventFlag = false;
                _LOG_($"FMSClient, Ignore the first DataChanged Event : {subscription.ToString()}");
                return;
            }
            m_application.ThreadPool.Queue(e, FMSClient_ProcessDataChanged);
        }



        private void EQP_Subscription_DataChanged(Subscription subscription, DataChangedEventArgs e)
        {
            if (EQPClient.m_ConnectData.FirstEventFlag == true)
            {
                EQPClient.m_ConnectData.FirstEventFlag = false;
                _LOG_($"EQPClient, Ignore the first DataChanged Event : {subscription.ToString()}");
                return;
            }
            m_application.ThreadPool.Queue(e, EQPClient_ProcessDataChanged);
        }

        private void CreateFMSMonitoredItem()
        {
            try
            {
                List<MonitoredItem> dataMonitoredItems = new List<MonitoredItem>();
                // 20230323 msh : OPCUA Browsing 속도 개선, CreateDataMonitoredItem()로 이동
                //FMSClient.m_monitoredItemList.Clear();
                //SetFMSMonitoredItemList();

                foreach (string item in FMSClient.m_monitoredItemList)
                {
                    FMSClient.SetDataMonitoringItemByBrowsePath(dataMonitoredItems, item);
                }
                FMSClient.dataMonitoredItems = dataMonitoredItems;
                //_LOG_($"*****************{FMSClient.m_ConnectData.Subscription.ConnectionStatus.ToString()}");
                List<StatusCode> results = FMSClient.m_ConnectData.Subscription.CreateMonitoredItems(dataMonitoredItems);

                StringBuilder logBuffer = new StringBuilder();
                logBuffer.AppendLine($"FMSClient, Create Monitored Items Count : {results.Count}");

                for (int i = 0; i < results.Count; i++)
                {
                    // log 용
                    //_LOG_($"FMSClient, Create Monitored Items {FMSClient.m_monitoredItemList[i]}:{results[i]}");
                    string log = $"FMSClient, Create Monitored Items {FMSClient.m_monitoredItemList[i]}:{results[i]}";

                    logBuffer.Append(log);
                    if (i < results.Count - 1) logBuffer.AppendLine();
                }                

                _LOG_(logBuffer.ToString(), ECSLogger.LOG_LEVEL.INFO);
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }


        private void CreateEQPMonitoredItem()
        {
            List<MonitoredItem> dataMonitoredItems = new List<MonitoredItem>();

            // 20230323 msh : OPCUA Browsing 속도 개선, CreateDataMonitoredItem()로 이동
            //EQPClient.m_monitoredItemList.Clear();
            //SetEQPMonitoredItemList();

            foreach (string item in EQPClient.m_monitoredItemList)
            {
                EQPClient.SetDataMonitoringItemByBrowsePath(dataMonitoredItems, item);
            }
            EQPClient.dataMonitoredItems = dataMonitoredItems;
            List<StatusCode> results = EQPClient.m_ConnectData.Subscription.CreateMonitoredItems(dataMonitoredItems);

            StringBuilder logBuffer = new StringBuilder();
            logBuffer.AppendLine($"EQPClient, Create Monitored Items Count : {results.Count}");

            for (int i = 0; i < results.Count; i++)
            {
                // log 용
                //_LOG_($"EQPClient, Create Monitored Items {EQPClient.m_monitoredItemList[i]}:{results[i]}");
                string log = string.Format($"EQPClient, Create Monitored Items {EQPClient.m_monitoredItemList[i]}:{results[i]}");

                logBuffer.Append(log);
                if (i < results.Count - 1) logBuffer.AppendLine();
            }

            _LOG_(logBuffer.ToString(), ECSLogger.LOG_LEVEL.INFO);
        }

        // 20230327 msh : DataClear 추가
        private void DrawDataGridViewWithCellBasicInfo(_CellBasicInformation CellInformation, DataGridView targetView, bool DataClear = false)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => DrawDataGridViewWithCellBasicInfo(CellInformation, targetView, DataClear)));
                else
                {
                    targetView.Columns.Clear();
                    //targetView.Rows.Clear();
                    targetView.Refresh();

                    if (DataClear) return;          // 20230328 msh : GridView 초기화

                    //Header 
                    DataTable CellInitData = new DataTable();
                    CellInitData.Columns.Add("Cell No", typeof(int));
                    CellInitData.Columns.Add("Cell ID", typeof(string));
                    CellInitData.Columns.Add("Lot ID", typeof(string));

                    for (int i = 0; i < CellInformation._CellList.Count; i++)
                    {
                        DataRow row = CellInitData.NewRow();
                        row["Cell No"] = i + 1;
                        row["Cell ID"] = CellInformation._CellList[i].CellId != null ? CellInformation._CellList[i].CellId : String.Empty;
                        row["Lot ID"] = CellInformation._CellList[i].LotId != null ? CellInformation._CellList[i].LotId : String.Empty;

                        CellInitData.Rows.Add(row);
                    }
                    targetView.DataSource = CellInitData;
                    targetView.Columns[0].Width = 60;
                    targetView.Columns[1].Width = 150;
                    targetView.Columns[2].Width = 90;

                    targetView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[0].ReadOnly = true;
                    targetView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[1].ReadOnly = true;
                    targetView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[2].ReadOnly = true;

                    targetView.Columns[0].ReadOnly = true;

                }

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }

        private void DrawDataGridViewWithCellList(List<_dat_cell> CellList, DataGridView targetView)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => DrawDataGridViewWithCellList(CellList, targetView)));
                else
                {
                    targetView.Columns.Clear();
                    //targetView.Rows.Clear();
                    targetView.Refresh();

                    //Header 
                    DataTable CellInitData = new DataTable();
                    CellInitData.Columns.Add("Cell No", typeof(int));
                    CellInitData.Columns.Add("Cell ID", typeof(string));
                    CellInitData.Columns.Add("Lot ID", typeof(string));

                    for (int i = 0; i < CellList.Count; i++)
                    {
                        DataRow row = CellInitData.NewRow();
                        row["Cell No"] = i + 1;
                        row["Cell ID"] =CellList[i].CELL_ID != null ? CellList[i].CELL_ID : String.Empty;
                        row["Lot ID"] = CellList[i].LOT_ID != null ? CellList[i].LOT_ID : String.Empty;

                        CellInitData.Rows.Add(row);
                    }
                    targetView.DataSource = CellInitData;
                    targetView.Columns[0].Width = 60;
                    targetView.Columns[1].Width = 150;
                    targetView.Columns[2].Width = 90;

                    targetView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[0].ReadOnly = true;
                    targetView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[1].ReadOnly = true;
                    targetView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[2].ReadOnly = true;

                    targetView.Columns[0].ReadOnly = true;

                }

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }

        private void SetTextBox(TextBox targetTextBox, string value)
        {
            if (InvokeRequired)
                Invoke(new Action(() => SetTextBox(targetTextBox, value)));
            else
                targetTextBox.Text = value;
        }

        private void SetGroupRadio(GroupRadio groupeRadioButton, Boolean value)
        {
            if (value == true)
                groupeRadioButton.IndexChecked = 1;
            else
                groupeRadioButton.IndexChecked = 0;
        }
        private void SetGroupRadio(GroupRadio groupeRadioButton, int value)
        {
            groupeRadioButton.IndexChecked = value;
        }

        private void UpdateRadioButton(GroupRadio targetGroupRadio, MonitoredItem item, bool isBoolean = true)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if (isBoolean == false)
                    targetGroupRadio.IndexChecked = (UInt16)eventItem.LastValue.Value;
                else
                {
                    if ((Boolean)eventItem.LastValue.Value == true)
                        targetGroupRadio.IndexChecked = 1;
                    else
                        targetGroupRadio.IndexChecked = 0;

                }
            }
        }

        private void UpdateTextBox(TextBox targetTextBox, MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                //targetTextBox.Text = (string)eventItem.LastValue.Value.ToString();
                SetTextBox(targetTextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);
            }
        }

        private bool EqpAvailable()
        {
            // EquipmentStatus.Power : true
            // EquipmentStatus.Mode : 0
            // 이거 2개만 보면 되나?

            Boolean Power;
            if (EQPClient.ReadValueByPath("EquipmentStatus.Power", out Power) == false)
            {
                _LOG_("[EQPClient] Fail to ReadValueByPath [EquipmentStatus.Power]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            UInt16 Mode;
            if (EQPClient.ReadValueByPath("EquipmentStatus.Mode", out Mode) == false)
            {
                _LOG_("[EQPClient] Fail to ReadValueByPath [EquipmentStatus.Mode]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            _LOG_($"[EQPClient] Check Current Eqp Status [Power:{Power}, Mode:{Mode}]");

            if (Power == true && Mode == 0)
                return true;
            else
                return false;
        }

        private bool MesAvailable()
        {
            // EquipmentStatus.MesAlive 확인
            // MES가 살아 있는지 여부에 대한 새로운 로직에 따라 추가, 수정
            Boolean MesAlive;

            //FMSClient.ReadValueByPath("EquipmentStatus.MesAlive", out MesAlive);

            if (FMSClient.ReadValueByNodeId(FMSClient.MesAliveNodeId, out MesAlive) == false)
            {
                _LOG_($"[FMSClient] Fail to read MesAlive Node [{FMSClient.MesAliveNodeId}]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            return MesAlive;
        }

        // 20230323 msh : OPCUA Browsing 속도 개선.
        #region AddMonitoredBrowsePath
        /// <summary>
        /// m_monitoredItemList에 정의된 Tag의 NodeID를 가져온다.
        /// </summary>
        private void AddMonitoredBrowsePath(List<string> browseList, OPCUAClient.OPCUAClient Client)
        {
            if (Client.Equals(EQPClient)) // for EQP OPCUA Server
                Client.SetBrowseNodeList(browseList, false, "TrackOutCellInformation");
            else
                Client.SetBrowseNodeList(browseList);
        }
        #endregion

        #region AddEQPBrowsePath
        /// <summary>
        /// subscribe, Recipe, ProcessData 이외의 NodeID를 가져온다.
        /// </summary>
        private void AddEQPBrowsePath(OPCUAClient.OPCUAClient Client)
        {
            List<string> browsePathList = new List<string>();

            List<string> cellData = new List<string>()
            {
                "CellExist",
                "CellId",
                "LotId"
            };

            for (int cellNo = 0; cellNo < 30; cellNo++)
            {
                foreach (var item in cellData)
                {
                    string browsename = $"Location1.TrackInCellInformation.Cell._{cellNo}.{item}";
                    browsePathList.Add(browsename);
                }
            }

            Client.SetBrowseNodeList(browsePathList);
        }
        #endregion

        #region AddFMSBrowsePath
        /// <summary>
        /// subscribe, Recipe, ProcessData 이외의 NodeID를 가져온다.
        /// </summary>
        private void AddFMSBrowsePath(OPCUAClient.OPCUAClient Client)
        {
            List<string> browsePathList = new List<string>(); ;

            //EquipmentStatus
            browsePathList.Add("EquipmentStatus.Status");
            browsePathList.Add("EquipmentStatus.Mode");
            browsePathList.Add("EquipmentStatus.Trouble.ErrorLevel");
            browsePathList.Add("EquipmentStatus.Trouble.ErrorNo");

            //FmsStatus
            browsePathList.Add("FmsStatus.Trouble.Status");
            browsePathList.Add("FmsStatus.Trouble.ErrorNo");

            //TrayProcess
            browsePathList.Add("Location1.TrayProcess.TrayLoad");
            browsePathList.Add("Location1.TrayProcess.RequestRecipe");
            browsePathList.Add("Location1.TrayProcess.ProcessStart");
            browsePathList.Add("Location1.TrayProcess.ProcessEnd");
            browsePathList.Add("Location1.TrayProcess.NextDestination.EquipmentId");
            browsePathList.Add("Location1.TrayProcess.NextDestination.ProcessId");
            browsePathList.Add("Location1.TrayProcess.NextDestination.UnitId");

            //TrayInformation
            browsePathList.Add("Location1.TrayInformation.CellCount");
            browsePathList.Add("Location1.TrayInformation.LotId");
            browsePathList.Add("Location1.TrayInformation.Model");
            browsePathList.Add("Location1.TrayInformation.ProcessId");
            browsePathList.Add("Location1.TrayInformation.RouteId");
            browsePathList.Add("Location1.TrayInformation.TrayExist");
            browsePathList.Add("Location1.TrayInformation.TrayId");
            browsePathList.Add("Location1.TrayInformation.TrayType");

            List<string> cellData = new List<string>()
            {
                "CellExist",
                "CellId",
                "LotId"
            };

            for (int cellNo = 0; cellNo < 30; cellNo++)
            {
                string browsename = $"Location1.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", cellNo + 1)}";

                foreach (var item in cellData)
                {
                    browsePathList.Add($"{browsename}.{item}");
                }
            }

            Client.SetBrowseNodeList(browsePathList);
        }
        #endregion

        #region AddRecipeBrowsePath
        /// <summary>
        /// Recipe Tag에 정의된 NodeID를 가져온다.
        /// </summary>
        private void AddRecipeBrowsePath(OPCUAClient.OPCUAClient Client, MappingDirection mappingDirection)
        {
            List<string> recipeDataList = new List<string>(); ;

            string MappingFile = $@"Mapping\FMS-{EQPType}_SV.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_SV.csv 파일이 있어야 한다.

            // 여기서 Recipe CSV 파일을 load하자. 
            MappingRecipeItem mappingRecipe = new MappingRecipeItem();
            Dictionary<string, MappingRecipeItem> RecipeDic = mappingRecipe.LoadRecipeMappingTable(MappingFile, mappingDirection);

            string browseName;

            recipeDataList.Add("Location1.Recipe.NextProcessExist");

            if (mappingDirection == MappingDirection.FmsToEqp)
            {
                recipeDataList.Add("Location1.Recipe.RecipeId");
                recipeDataList.Add("Location1.Recipe.OperationMode");
            }

            foreach (var item in RecipeDic)
            {
                browseName = string.Format($"Location1.Recipe.{item.Key}");
                recipeDataList.Add(browseName);
            }

            Client.SetBrowseNodeList(recipeDataList);
        }
        #endregion

        #region AddProcessDataBrowsePath
        /// <summary>
        /// ProcessData Tag에 정의된 NodeID를 가져온다.
        /// </summary>
        private void AddProcessDataBrowsePath(OPCUAClient.OPCUAClient Client, MappingDirection mappingDirection)
        {
            Client.m_ProcessDataNodeList = new ProcessDataNodeList
            {
                NodeList = new List<ReadValueId>(),
                NodeIndex = new Dictionary<string, int>()
            };

            List<string> processDataList = new List<string>();
            string TagName;

            string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
            MappingProcessDataItem mappingPD = new MappingProcessDataItem();
            Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, mappingDirection);

            if (mappingDirection == MappingDirection.EqpToFms)
            {
                processDataList.Add("Location1.TrackOutCellInformation.CellCount");
                processDataList.Add("Location1.TrackOutCellInformation.StartFrequency");
                processDataList.Add("Location1.TrackOutCellInformation.EndFrequency");
                processDataList.Add("Location1.TrackOutCellInformation.NumberOfPoints");
                processDataList.Add("Location1.TrackOutCellInformation.StartTemp");
                processDataList.Add("Location1.TrackOutCellInformation.EndTemp");
                processDataList.Add("Location1.TrackOutCellInformation.AvgTemp");

                TagName = "Location1.TrackOutCellInformation.Cell._";
            }
            else
            {
                TagName = "Location1.ProcessData.Cell";
            }

            string browseName;
            string cellNo;

            for (int i = 0; i < 30; i++)
            {
                cellNo = mappingDirection == MappingDirection.EqpToFms ? i.ToString() : string.Format("{0:D2}", i + 1);

                browseName = string.Format($"{TagName}{cellNo}.CellExist");
                processDataList.Add(browseName);
                browseName = string.Format($"{TagName}{cellNo}.CellId");
                processDataList.Add(browseName);
                browseName = string.Format($"{TagName}{cellNo}.LotId");
                processDataList.Add(browseName);
                browseName = string.Format($"{TagName}{cellNo}.NGCode");
                processDataList.Add(browseName);
                browseName = string.Format($"{TagName}{cellNo}.NGType");
                processDataList.Add(browseName);

                foreach (var item in processDataDic)
                {
                    if (item.Value.CommonFlag != "C" || mappingDirection == MappingDirection.FmsToEqp)
                    {
                        browseName = string.Format($"{TagName}{cellNo}.{item.Key}");
                        processDataList.Add(browseName);
                    }
                }
            }

            Client.SetBrowseNodeList(processDataList, true);
        }
        #endregion


        #region LogForEQP
        public void _LOG_(string strDescription, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            string log;
            string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"{EQPID}, {UNITID}, {level.ToString()},  {caller}, {strDescription}";
            if (UNITID == null)
                log = $"{EQPID}, {caller}, {strDescription}";
            else
                log = $"{EQPID}, {UNITID}, {caller}, {strDescription}";

            ECSLogger.Logger.WriteLog(log, level, this.EQPID);
        }
        #endregion
    }
}
