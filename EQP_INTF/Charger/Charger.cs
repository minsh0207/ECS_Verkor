using CommonCtrls;
using OPCUAClient;
using RestAPIServer;
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

namespace EQP_INTF.Charger
{
    public partial class Charger : UserControl
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

        // MES의 응답을 기다리는 Timer 및 Queue에 사용할 구조체
        private static System.Timers.Timer MesResponseTimer = null;
        // 응답받을 Tag (browseName : startNode 이후의 full path) 를 등록해놓을까?
        // 이건 Async로 올수도 있나? 설비 Unit별로 sync로  진행하면 되나? 
        private static MesResponseTimerClass MesResponseWaitItem = null;

        public bool WaitMesResponse(string ResponsePath, List<object> parameters = null)
        {
            if(MesResponseWaitItem != null)
            {
                // 20230330 msh : 이전에 null시켜주는 부분을 삭제하여 해당 log는 주석 처리 함.
                _LOG_($"[{this.Name}] MesResponseWaitItem is not null :[{MesResponseWaitItem.ResponsePath}:{MesResponseWaitItem.RequestTime}] ", ECSLogger.LOG_LEVEL.WARN);
                MesResponseWaitItem = null;
            }

            MesResponseWaitItem = new MesResponseTimerClass();
            MesResponseWaitItem.ResponsePath = ResponsePath;
            DateTime dateTime = DateTime.Now;
            MesResponseWaitItem.RequestTime = dateTime;
            if(parameters != null)
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

        public Charger()
        {
            InitializeComponent();
        }

        private void MES_Response_TimeOut(object sender, System.Timers.ElapsedEventArgs e)
        {
            _LOG_($"[{this.Name}] TimeOut for MES Response on [{MesResponseWaitItem.ResponsePath}] Elapsed Time : {e.SignalTime- MesResponseWaitItem.RequestTime}", ECSLogger.LOG_LEVEL.WARN);

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

        





        //public void InitControl(ApplicationInstance applicationInstance, OPCUAClient.OPCUAClient FMSServer)
        public void InitControl(ApplicationInstance applicationInstance)
        {
            //DataClass - global
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

        public void StartECS_CHG()
        {
            EQPClient.Start();
            FMSClient.Start();
            RestApiServer.Start();

        }
        public void StopECS_CHG()
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

        #region  Display 초기화 
        private void InitDisplayWithCurrentValue(OPCUAClient.OPCUAClient OPCClient)
        {

            List<string> ReadPath = new List<string>();


            //Common_Alive_Radio
            ReadPath.Add("Common.Alive");
            //Power_Radio
            ReadPath.Add("EquipmentStatus.Power");
            //Mode_Radio
            ReadPath.Add("EquipmentStatus.Mode");
            //Status_TextBox
            ReadPath.Add("EquipmentStatus.Status");
            //EQP_ErrorNo_TextBox
            ReadPath.Add("EquipmentStatus.Trouble.ErrorNo");
            //EQP_ErrorLevel_Radio
            ReadPath.Add("EquipmentStatus.Trouble.ErrorLevel");
            //FMS_Status_Radio
            ReadPath.Add("FmsStatus.Trouble.ErrorNo");
            //FMS_ErrorNo_TextBox
            ReadPath.Add("FmsStatus.Trouble.Status");

            //CommandResponse_Radio
            ReadPath.Add("EquipmentControl.Command");
            //Command_TextBox
            ReadPath.Add("EquipmentControl.CommandResponse");

            //Level1TrayExist_Radio
            ReadPath.Add("Location1.TrayInformation.Level1.TrayExist");
            //Level1TrayId_TextBox
            ReadPath.Add("Location1.TrayInformation.Level1.TrayId");
            //Level1CellCount_TextBox
            ReadPath.Add("Location1.TrackInCellInformation.Level1.CellCount");
            //Level2TrayExist_Radio
            ReadPath.Add("Location1.TrayInformation.Level2.TrayExist");
            //Level2TrayId_TextBox
            ReadPath.Add("Location1.TrayInformation.Level2.TrayId");
            //Level2CellCount_TextBox
            ReadPath.Add("Location1.TrackInCellInformation.Level2.CellCount");

            //TrayLoad_Radio
            ReadPath.Add("Location1.TrayInformation.TrayLoad");
            //TrayLoadResponse_Radio
            ReadPath.Add("Location1.TrayInformation.TrayLoadResponse");
            //RequestRecipe_Radio
            ReadPath.Add("Location1.TrayProcess.RequestRecipe");
            //RequestRecipeResponse_Radio
            ReadPath.Add("Location1.TrayProcess.RequestRecipeResponse");
            //RecipeId_TextBox
            ReadPath.Add("Location1.Recipe.RecipeId");
            //OperationMode_TextBox
            ReadPath.Add("Location1.Recipe.OperationMode");

            //ProcessStart_Radio
            ReadPath.Add("Location1.TrayProcess.ProcessStart");
            //ProcessStartResponse_Radio
            ReadPath.Add("Location1.TrayProcess.ProcessStartResponse");
            //ProcessEnd_Radio
            ReadPath.Add("Location1.TrayProcess.ProcessEnd");
            //ProcessEndResponse_Radio
            ReadPath.Add("Location1.TrayProcess.ProcessEndResponse");

            //TrayLoadRequest_Radio
            ReadPath.Add("Location1.TrayProcess.TrayLoadRequest");
            //TrayLoadRequestResponse_Radio
            ReadPath.Add("Location1.TrayProcess.TrayLoadRequestResponse");
            //TrayUnloadRequest_Radio
            ReadPath.Add("Location1.TrayProcess.TrayUnloadRequest");
            //TrayUnloadRequestResponse_Radio
            ReadPath.Add("Location1.TrayProcess.TrayUnloadRequestResponse");

            List<BrowseNode> browseNodeList;
            List<ReadValueId> readValueIdList;
            List<DataValue> nodesReadValue = OPCClient.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

            if (nodesReadValue == null || nodesReadValue.Count < 1)
                return;

            //--------------------------------
            //foreach(DataValue node in nodesReadValue)
            for (int i = 0; i < nodesReadValue.Count; i++)
            {
                //Common_Alive_Radio
                if (browseNodeList[i].browsePath.EndsWith("Common.Alive"))
                    SetGroupRadio(Common_Alive_Radio, (Boolean)nodesReadValue[i].Value);
                //Power_Radio
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Power"))
                    SetGroupRadio(Power_Radio, (Boolean)nodesReadValue[i].Value);
                //Mode_Radio
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Mode"))
                    SetGroupRadio(Mode_Radio, (UInt16)nodesReadValue[i].Value);
                //Status_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Status"))
                    SetTextBox(Status_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //EQP_ErrorNo_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorNo"))
                    SetTextBox(EQP_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //EQP_ErrorLevel_Radio
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorLevel"))
                    SetGroupRadio(EQP_ErrorLevel_Radio, (UInt16)nodesReadValue[i].Value);
                //FMS_Status_Radio
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    SetGroupRadio(FMS_Status_Radio, (Boolean)nodesReadValue[i].Value);
                //FMS_ErrorNo_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    SetTextBox(FMS_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                //CommandResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    SetGroupRadio(CommandResponse_Radio, (Boolean)nodesReadValue[i].Value);
                //Command_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.Command"))
                    SetTextBox(Command_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                //Level1TrayExist_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.Level1.TrayExist"))
                    SetGroupRadio(Level1TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                //Level1TrayId_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.Level1.TrayId"))
                    SetTextBox(Level1TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //Level1CellCount_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackInCellInformation.Level1.CellCount"))
                    SetTextBox(Level1CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //Level2TrayExist_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.Level2.TrayExist"))
                    SetGroupRadio(Level2TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                //Level2TrayId_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.Level2.TrayId"))
                    SetTextBox(Level2TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //Level2CellCount_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackInCellInformation.Level2.CellCount"))
                    SetTextBox(Level2CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                //TrayLoad_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    SetGroupRadio(TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayLoadResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                //RequestRecipe_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    SetGroupRadio(RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                //RequestRecipeResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    SetGroupRadio(RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                //RecipeId_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    SetTextBox(RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //OperationMode_TextBox
                else if (browseNodeList[i].browsePath.EndsWith("Location1.Recipe.OperationMode"))
                    SetTextBox(OperationMode_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                //ProcessStart_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    SetGroupRadio(ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                //ProcessStartResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    SetGroupRadio(ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                //ProcessEnd_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    SetGroupRadio(ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                //ProcessEndResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);

                //TrayLoadRequest_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.TrayLoadRequest"))
                    SetGroupRadio(TrayLoadRequest_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayLoadRequestResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.TrayLoadRequestResponse"))
                    SetGroupRadio(TrayLoadRequestResponse_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayUnloadRequest_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.TrayUnloadRequest"))
                    SetGroupRadio(TrayUnloadRequest_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayUnloadRequestResponse_Radio
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.TrayUnloadRequestResponse"))
                    SetGroupRadio(TrayUnloadRequestResponse_Radio, (Boolean)nodesReadValue[i].Value);
            }
        }
        #endregion

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
                if (Client.m_browseNodeList.NodeList.Count == 0)
                {
                    Client.UpdatelbStatusText("Browsing... wait a minute");
                    _LOG_($"[{Client.Name}] Start to Browse All Nodes");
                    // 20230403 msh : OPCUA Browsing 속도 개선
                    Client.BrowseFromStartNode(NodeId.Parse(Client.StartingNodeId), null, 1);
                    //_LOG_($"[{Client.Name}] Complete to Browse All Nodes : {Client.m_browseNodeList.NodeList.Count} nodes");
                    _LOG_($"[{this.Name}] Complete to Browse All Object Nodes : {Client.m_browseNodeList.ObjectNodeList.Count} nodes");
                    Client.UpdatelbStatusText("Connected");
                }

                // 20230403 msh : OPCUA Browsing 속도 개선
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

        private void CreateFMSMonitoredItem()
        {
            try
            {
                List<MonitoredItem> dataMonitoredItems = new List<MonitoredItem>();
                // 20230403 msh : OPCUA Browsing 속도 개선
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
            // 20230403 msh : OPCUA Browsing 속도 개선
            //EQPClient.m_monitoredItemList.Clear();
            //SetEQPMonitoredItemList();

            foreach(string item in EQPClient.m_monitoredItemList)
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
            if(EQPClient.m_ConnectData.FirstEventFlag == true)
            {
                EQPClient.m_ConnectData.FirstEventFlag = false;
                _LOG_($"EQPClient, Ignore the first DataChanged Event : {subscription.ToString()}");
                return;
            }
            m_application.ThreadPool.Queue(e, EQPClient_ProcessDataChanged);
        }

        private void FMSClient_ProcessDataChanged(object data, StatusCode error)
        {
            try
            {
                DataChangedEventArgs ChangeEventArgs = data as DataChangedEventArgs;
                for (int i = 0; i < ChangeEventArgs.DataChanges.Count; i++)
                {
                    MonitoredItem item = ChangeEventArgs.DataChanges[i].MonitoredItem;
                    //BrowseNode node = FMSClient.m_browseNodeList.FindTargetNodeByNodeId(item.NodeId);
                    // 20230403 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"FMSClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("TrayProcess.TrayLoadResponse"))
                    {
                        FMSSequenceTrayLoadResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.RequestRecipeResponse"))
                    {
                        FMSSequenceRequestRecipeResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequenceProcessStartResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequenceProcessEndResponse(item);
                    }

                }
            }
            catch(Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        

        private void EQPClient_ProcessDataChanged(object data, StatusCode error)
        {
            try
            {
                DataChangedEventArgs ChangeEventArgs = data as DataChangedEventArgs;
                for (int i = 0; i < ChangeEventArgs.DataChanges.Count; i++)
                {
                    MonitoredItem item = ChangeEventArgs.DataChanges[i].MonitoredItem;
                    //BrowseNode node = EQPClient.m_browseNodeList.FindTargetNodeByNodeId(item.NodeId);
                    // 20230403 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"EQPClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    //여기서 Event별로 분기함
                    if (browsePath.EndsWith("Common.Alive"))
                    {
                        UpdateAive(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Power"))
                    {
                        UpdatePower(item);
                        // EQP 상태 정보 저장 로직
                        // eqp_mode에 powerOFF 만 저장함.. on되면 control / maintaenance mode 중 하나가 됨
                        SetEQPPower(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Mode"))
                    {
                        UpdateMode(item);
                        // EQP Mode 정보 저장 로직
                        SetEQPMode(item);

                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Status"))
                    {
                        UpdateStatus(item);
                        // EQP Sttus 정보 저장 로직
                        SetEqpStatus(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Trouble.ErrorNo"))
                    {
                        UpdateTextBox(EQP_ErrorNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Trouble.ErrorLevel"))
                    {
                        UpdateRadioButton(EQP_ErrorLevel_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    {
                        UpdateTextBox(FMS_ErrorNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    {
                        UpdateRadioButton(FMS_Status_Radio, item);
                    }
                    else if (browsePath.EndsWith("EquipmentControl.Command"))
                    {
                        UpdateCommand(item);
                    }
                    else if (browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    {
                        UpdateCommandResponse(item);
                    }

                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(TrayLoad_Radio, item);
                        // Tray와 Cell정보를 조회해서 EQP, FMS 모두 써준다.
                        if(EqpAvailable()) SequenceTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.Level1.TrayExist"))
                    {
                        UpdateTrayExist(item, "Level1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.Level1.TrayId"))
                    {
                        UpdateTrayId(item, "Level1");
                    }
                    else if (browsePath.EndsWith("Location1.TrackInCellInformation.Level1.CellCount"))
                    {
                        UpdateCellCount(item, "Level1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.Level2.TrayExist"))
                    {
                        UpdateTrayExist(item, "Level2");
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.Level2.TrayId"))
                    {
                        UpdateTrayId(item, "Level2");
                    }
                    else if (browsePath.EndsWith("Location1.TrackInCellInformation.Level2.CellCount"))
                    {
                        UpdateCellCount(item, "Level2");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    {
                        UpdateRadioButton(ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessStart(item);

                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceRequestRecipe(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(RequestRecipeResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.TrayLoadRequest"))
                    {
                        UpdateRadioButton(TrayLoadRequest_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrayLoadRequest(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.TrayLoadRequestResponse"))
                    {
                        UpdateRadioButton(TrayLoadRequestResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.TrayUnloadRequest"))
                    {
                        UpdateRadioButton(TrayUnloadRequest_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrayUnloadRequest(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.TrayUnloadRequestResponse"))
                    {
                        UpdateRadioButton(TrayUnloadRequestResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    {
                        UpdateRecipeId(item);
                    }
                    else if (browsePath.EndsWith("Location1.Recipe.OperationMode"))
                    {
                        UpdateOperationMode(item);
                    }
                }
            }catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
            if(EQPClient.ReadValueByPath("EquipmentStatus.Power", out Power) == false)
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


        //EQP Monitored Item List
        #region Create Monitored Items
        private void SetEQPMonitoredItemList()
        {
            //Common
            EQPClient.m_monitoredItemList.Add("Common.Alive");
            //EquipmentStatus
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Power");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Mode");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Status");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorNo");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorLevel");

            //FmsStatus
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.Status");
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.ErrorNo");
            //EquipmentControl
            EQPClient.m_monitoredItemList.Add("EquipmentControl.Command");
            EQPClient.m_monitoredItemList.Add("EquipmentControl.CommandResponse");
            //TrayInformation
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level1.TrayExist");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level1.TrayId");
            //EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level1.CellCount");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level2.TrayExist");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level2.TrayId");
            //EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.Level2.CellCount");

            EQPClient.m_monitoredItemList.Add("Location1.TrackInCellInformation.Level1.CellCount");
            EQPClient.m_monitoredItemList.Add("Location1.TrackInCellInformation.Level2.CellCount");

            //TrayProcess
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayLoadRequest");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayLoadRequestResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayUnloadRequest");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayUnloadRequestResponse");
            //Recipe
            EQPClient.m_monitoredItemList.Add("Location1.Recipe.RecipeId");
            EQPClient.m_monitoredItemList.Add("Location1.Recipe.OperationMode");

        }

        private void SetFMSMonitoredItemList()
        {
            //TrayLoadResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayLoadResponse");
            //RequestRecipeResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");
            //ProcessStartResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            //ProcessEndResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
        }

        // 20230403 msh : OPCUA Browsing 속도 개선
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
            List<string> browsePathList = new List<string>(); ;

            //TrayInformation
            browsePathList.Add("Location1.TrayInformation.TrayCount");

            List<string> levelList = new List<string>()
            {
                "Level1",
                "Level2"
            };

            foreach (var item in levelList)
            {
                browsePathList.Add($"Location1.TrayInformation.{item}.ProductModel");
                browsePathList.Add($"Location1.TrayInformation.{item}.LotId");
                browsePathList.Add($"Location1.TrayInformation.{item}.ProcessId");
                browsePathList.Add($"Location1.TrayInformation.{item}.RouteId");
                browsePathList.Add($"Location1.TrayInformation.{item}.TrayType");
            }

            //TrackInCellInformation
            List<string> cellData = new List<string>()
            {
                "CellExist",
                "CellId",
                "LotId"
            };

            for (int cellNo = 0; cellNo < 60; cellNo++)
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
            browsePathList.Add("Location1.TrayInformation.TrayCount");

            List<string> levelList = new List<string>()
            {
                "Level1",
                "Level2"
            };

            foreach (var item in levelList)
            {
                browsePathList.Add($"Location1.TrayInformation.{item}.CellCount");
                browsePathList.Add($"Location1.TrayInformation.{item}.LotId");
                browsePathList.Add($"Location1.TrayInformation.{item}.Model");
                browsePathList.Add($"Location1.TrayInformation.{item}.ProcessId");
                browsePathList.Add($"Location1.TrayInformation.{item}.RouteId");
                browsePathList.Add($"Location1.TrayInformation.{item}.TrayExist");
                browsePathList.Add($"Location1.TrayInformation.{item}.TrayId");
                browsePathList.Add($"Location1.TrayInformation.{item}.TrayType");
            }

            List<string> cellDataList = new List<string>()
            {
                "CellExist",
                "CellId",
                "LotId"
            };

            for (int cellNo = 1; cellNo <= 60; cellNo++)
            {
                foreach (var item in cellDataList)
                {
                    string browsename = $"Location1.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", cellNo)}";

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

            //Recipe            
            recipeDataList.Add("Location1.Recipe.NextProcessExist");

            if (mappingDirection == MappingDirection.EqpToFms)
            {   
                recipeDataList.Add("Location1.Recipe.NextStepExist");
                recipeDataList.Add("Location1.Recipe.StepNo");
            }
            else
            {
                recipeDataList.Add("Location1.Recipe.RecipeId");        // EQP는 SetEQPMonitoredItemList에서 추가 함.
                recipeDataList.Add("Location1.Recipe.OperationMode");   // EQP는 SetEQPMonitoredItemList에서 추가 함.
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
                List<string> levelList = new List<string>()
                {
                    "Level1",
                    "Level2"
                };

                foreach (var item in levelList)
                {
                    processDataList.Add($"Location1.TrackOutCellInformation.{item}.CellCount");
                    processDataList.Add($"Location1.TrackOutCellInformation.{item}.StartTemp");
                    processDataList.Add($"Location1.TrackOutCellInformation.{item}.EndTemp");
                    processDataList.Add($"Location1.TrackOutCellInformation.{item}.AvgTemp");
                }

                TagName = "Location1.TrackOutCellInformation.Cell._";
            }
            else
            {
                TagName = "Location1.ProcessData.Cell";
            }

            string browseName;
            string cellNo;

            for (int i = 0; i < 60; i++)
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


        #endregion

        #region LogForEQP
        public void _LOG_(string strDescription, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"{EQPID}, {UNITID}, {level.ToString()},  {caller}, {strDescription}";
            string log = $"{EQPID}, {UNITID}, {caller}, {strDescription}";
            ECSLogger.Logger.WriteLog(log, level, this.UNITID);
        }
        #endregion

    }
}
