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
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.NGSorter
{
    public partial class NGSorter : UserControl
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
        //20230222 - MES 응답을 기다리는 TimeOut
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

        private static System.Timers.Timer MesResponseTimer = null;
        // 응답받을 Tag (browseName : startNode 이후의 full path) 를 등록해놓을까?
        // 이건 Async로 올수도 있나? 설비 Unit별로 sync로  진행하면 되나? 
        private static MesResponseTimerClass MesResponseWaitItem = null;

        //20230320 KJY - 작업중인 Cell 정보를 계속 남겨 두어야 할 필요가 있음.
        // Pick으로 새 Tray가 들어올때 Clear, Place에 새 Tray가 들어올때도 Clear


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
        private void MES_Response_TimeOut(object sender, System.Timers.ElapsedEventArgs e)
        {
            _LOG_($"[{this.Name}] TimeOut for MES Response on [{MesResponseWaitItem.ResponsePath}] Elapsed Time : {e.SignalTime - MesResponseWaitItem.RequestTime}", ECSLogger.LOG_LEVEL.WARN);

            MesResponseTimer.Stop();
            MesResponseTimer = null;

            if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.TrayLoadResponse")
                FMSSequencePickTrayLoadResponseTimeOut();
            //else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.RequestCellGradeResponse")
            //    FMSSequencePickRequestCellGradeResponseTimeOut();

            //20230331 sgh ProcessStartResponse -> ProcessStart로 변경
            else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.ProcessStart")
                FMSSequencePickProcessStartResponseTimeOut();
            //20230331 sgh ProcessEndResponse -> ProcessEnd로 변경
            else if (MesResponseWaitItem.ResponsePath == "Location1.TrayProcess.ProcessEnd")
                FMSSequencePickProcessEndResponseTimeOut();
            //else if (MesResponseWaitItem.ResponsePath == "Location2.TrayProcess.ProcessStartResponse")
            //    FMSSequencePlaceProcessStartResponseTimeOut();
            else if (MesResponseWaitItem.ResponsePath == "Location2.TrayProcess.TrayLoadResponse")
                FMSSequencePlaceTrayLoadResponseTimeOut();
            else if (MesResponseWaitItem.ResponsePath == "Location2.TrayProcess.ProcessEndResponse")
                FMSSequencePlaceProcessEndResponseTimeOut();
            else
            {
                _LOG_($"[{this.Name}] Fail to find case of Mes Response Timeout [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }

        

        

        //private void FMSSequencePlaceProcessStartResponseTimeOut()
        //{
        //    throw new NotImplementedException();
        //}

        

        

        //private void FMSSequencePickRequestCellGradeResponseTimeOut()
        //{
        //    throw new NotImplementedException();
        //}

        

        public NGSorter()
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

        public void StartECS_NGS()
        {
            EQPClient.Start();
            FMSClient.Start();
            RestApiServer.Start();

        }
        public void StopECS_NGS()
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
                    break;
                case ServerConnectionStatus.ConnectionErrorClientReconnect:
                    _LOG_("EQPClient, ServerConnectionStatus:ConnectionErrorClientReconnect");
                    //초기화면 구축
                    //_LOG_("EQPClient, Init Display");
                    //InitDisplayWithCurrentValue(EQPClient);
                    break;

            }
        }

        private void CreateDataMonitoredItem(OPCUAClient.OPCUAClient Client)
        {
            try
            {
                // 20230327 msh : 통신 재연결시 BrowseFromStartNode 시작하도록 변경
                Client.InitBrowseNodeList();

                //20230315 KJY - 여기서 Browse Node를 check하고 count가 0이면 Browse하는것으로 해야 겠다.
                if (Client.m_browseNodeList.NodeList.Count ==0)
                {
                    _LOG_($"[{Client.Name}] Start to Browse All Nodes");
                    Client.BrowseFromStartNode(NodeId.Parse(Client.StartingNodeId), null, 1);
                    //_LOG_($"[{Client.Name}] Complete to Browse All Nodes : {Client.m_browseNodeList.NodeList.Count} nodes");
                    // 20230406 msh : OPCUA Browsing 속도 개선.
                    _LOG_($"[{this.Name}] Complete to Browse All Object Nodes : {Client.m_browseNodeList.ObjectNodeList.Count} nodes");
                }

                // 20230406 msh : OPCUA Browsing 속도 개선
                if (Client.Equals(EQPClient)) // for EQP OPCUA Server
                {
                    EQPClient.m_monitoredItemList.Clear();
                    SetEQPMonitoredItemList();

                    AddMonitoredBrowsePath(EQPClient.m_monitoredItemList, Client);

                    AddEQPBrowsePath(Client);
                }
                else // for FMS OPCUA Server
                {
                    FMSClient.m_monitoredItemList.Clear();
                    SetFMSMonitoredItemList();

                    AddMonitoredBrowsePath(FMSClient.m_monitoredItemList, Client);

                    AddFMSBrowsePath(Client);
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
                // 20230406 msh : OPCUA Browsing 속도 개선, CreateDataMonitoredItem()로 이동
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
                    string log = string.Format($"FMSClient, Create Monitored Items {FMSClient.m_monitoredItemList[i]}:{results[i]}");

                    logBuffer.Append(log);
                    if (i < results.Count - 1) logBuffer.AppendLine();
                }

                _LOG_(logBuffer.ToString(), ECSLogger.LOG_LEVEL.INFO);
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] : {ex.Message}");
            }
        }


        private void CreateEQPMonitoredItem()
        {
            List<MonitoredItem> dataMonitoredItems = new List<MonitoredItem>();
            // 20230406 msh : OPCUA Browsing 속도 개선, CreateDataMonitoredItem()로 이동
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

        private void DrawDataGridViewWithCellBasicInfo(_CellBasicInformation CellInformation, DataGridView targetView)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => DrawDataGridViewWithCellBasicInfo(CellInformation, targetView)));
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

        private void DrawDataGridViewWithCellGradeInfo(_SorterCellInformation CellInformation, DataGridView targetView)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => DrawDataGridViewWithCellGradeInfo(CellInformation, targetView)));
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
                    CellInitData.Columns.Add("NGType", typeof(string));
                    CellInitData.Columns.Add("NGCode", typeof(string));
                    //CellInitData.Columns.Add("Grade", typeof(string));
                    CellInitData.Columns.Add("DefectType", typeof(string));


                    for (int i = 0; i < CellInformation._CellList.Count; i++)
                    {
                        DataRow row = CellInitData.NewRow();
                        row["Cell No"] = i + 1;
                        row["Cell ID"] = CellInformation._CellList[i].CellId != null ? CellInformation._CellList[i].CellId : String.Empty;
                        row["Lot ID"] = CellInformation._CellList[i].LotId != null ? CellInformation._CellList[i].LotId : String.Empty;                        
                        row["NGType"] = CellInformation._CellList[i].NGType != null ? CellInformation._CellList[i].NGType : String.Empty;
                        row["NGCode"] = CellInformation._CellList[i].NGCode != null ? CellInformation._CellList[i].NGCode : String.Empty;
                        //row["Grade"] = CellInformation._CellList[i].CellGrade != null ? CellInformation._CellList[i].CellGrade : String.Empty;
                        row["DefectType"] = CellInformation._CellList[i].DefectType != null ? CellInformation._CellList[i].DefectType : String.Empty;

                        CellInitData.Rows.Add(row);
                    }
                    targetView.DataSource = CellInitData;
                    targetView.Columns[0].Width = 60;
                    targetView.Columns[1].Width = 150;
                    targetView.Columns[2].Width = 90;
                    targetView.Columns[3].Width = 60;
                    targetView.Columns[4].Width = 60;
                    targetView.Columns[5].Width = 60;

                    targetView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[0].ReadOnly = true;
                    targetView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[1].ReadOnly = true;
                    targetView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[2].ReadOnly = true;
                    targetView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[3].ReadOnly = true;
                    targetView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[4].ReadOnly = true;
                    targetView.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[5].ReadOnly = true;

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
            // EquipmentStatus.Mode : 4  **** Apro설비들과 코드가 다름에 주의
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

            if (Power == true && Mode == 4)
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

        // 20230406 msh : OPCUA Browsing 속도 개선.
        #region AddMonitoredBrowsePath
        /// <summary>
        /// m_monitoredItemList에 정의된 Tag의 NodeID를 가져온다.
        /// </summary>
        private void AddMonitoredBrowsePath(List<string> browseList, OPCUAClient.OPCUAClient Client)
        {
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

            //TrayInformation
            List<string> locationList = new List<string>()
            {
                "PickLocation",
                "PlaceLocation"
            };

            foreach (var location in locationList)
            {
                browsePathList.Add($"{location}.TrayInformation.TrayType");
            }

            //PickLocation.CellInformation
            List<string> cellData = new List<string>()
            {
                "CellExist",
                "CellId",
                "DefectType",
                "LotId",
                "NGCode",
                "NGType"
            };

            foreach (var location in locationList)
            {
                for (int cellNo = 0; cellNo < 30; cellNo++)
                {
                    foreach (var item in cellData)
                    {
                        string browsename = $"{location}.CellInformation.Cell._{cellNo}.{item}";
                        browsePathList.Add(browsename);
                    }
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
            List<string> locationList = new List<string>()
            {
                "Location1",
                "Location2"
            };

            foreach (var location in locationList)
            {
                if (location == "Location1")
                {
                    browsePathList.Add($"{location}.TrayProcess.TrayLoad");
                }

                browsePathList.Add($"{location}.TrayProcess.ProcessStart");
                browsePathList.Add($"{location}.TrayProcess.ProcessEnd");
                browsePathList.Add($"{location}.TrayProcess.NextDestination.EquipmentId");
                browsePathList.Add($"{location}.TrayProcess.NextDestination.ProcessId");
                browsePathList.Add($"{location}.TrayProcess.NextDestination.UnitId");
            }

            //TrayInformation
            List<string> cellData = new List<string>()
            {
                "CellExist",
                "CellId",
                "LotId"
            };

            foreach (var location in locationList)
            {
                for (int cellNo = 0; cellNo < 30; cellNo++)
                {
                    string browsename = $"{location}.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", cellNo + 1)}";

                    foreach (var item in cellData)
                    {
                        browsePathList.Add($"{browsename}.{item}");
                    }
                }
            }

            List<string> cellGrade = new List<string>()
            {
                "DefectType",
                "NGCode",
                "NGType"
            };

            foreach (var location in locationList)
            {
                for (int cellNo = 0; cellNo < 30; cellNo++)
                {
                    string browsename = $"{location}.TrayInformation.CellGrade.Cell{string.Format("{0:D2}", cellNo + 1)}";

                    foreach (var item in cellGrade)
                    {
                        browsePathList.Add($"{browsename}.{item}");
                    }
                }
            }

            Client.SetBrowseNodeList(browsePathList);
        }
        #endregion


        #region LogForEQP
        public void _LOG_(string strDescription, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"{EQPID}, {UNITID}, {level.ToString()},  {caller}, {strDescription}";
            string log = $"{EQPID}, , {caller}, {strDescription}";
            ECSLogger.Logger.WriteLog(log, level, this.EQPID);
        }
        #endregion

    }
}
