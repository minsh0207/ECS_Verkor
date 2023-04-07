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
using UnifiedAutomation.UaClient.Controls;
//using static RestAPIServer.DataClass;

namespace OPCUAClient
{
    public partial class OPCUAClient : UserControl
    {
        #region [Variables]
        private ApplicationInstance m_applicationInstance;
        public ConnectData m_ConnectData = null;
        public BrowseNodeList m_browseNodeList = null;
        // 20230323 msh : OPCUA Browsing 속도 개선, Browsing할때 NodeID를 미리 정의해 둔다.
        public ProcessDataNodeList m_ProcessDataNodeList = null;

        public List<string> m_monitoredItemList = new List<string>();
        public List<MonitoredItem> dataMonitoredItems = null;
        public class ConnectData
        {
            public Session Session;
            public string EndpointUrl;
            public ICertificate Certificate;
            public Subscription Subscription;
            public object Parent;
            public bool FirstEventFlag;
        }

        //public class BrowseNodeList
        //{
        //    public List<BrowseNode> NodeList;

        //    public BrowseNode FindTargetNodeByPath(string browsePath)
        //    {
        //        BrowseNode node = this.NodeList.Find(x => x.browsePath.EndsWith(browsePath));
        //        if (node == null)
        //            return null;
        //        else
        //            return node;
        //    }

        //    public BrowseNode FindTargetNodeByNodeId(NodeId nodeId)
        //    {
        //        BrowseNode node = this.NodeList.Find(x => x.nodeId.Equals(nodeId));
        //        if (node == null)
        //            return null;
        //        else
        //            return node;
        //    }
        //}
        public class BrowseNode
        {
            public NodeId nodeId;
            public QualifiedName browseName;
            public LocalizedText displayName;
            public NodeClass nodeClass;
            public string browsePath;
        }

        //20230324 KJY - Dictionary로 만들어보자
        public class BrowseNodeList
        {
            // browsePath가 Key
            public Dictionary<string, BrowseNode> NodeList;
            // nodeId가 key
            public Dictionary<NodeId, BrowseNode> NodeIdDic;
            // Object Type의 NodeId를 저장한다.
            public Dictionary<string, BrowseNode> ObjectNodeList;       // 20230324 msh : OPCUA Browsing 속도 개선.


            //public BrowseNode FindTargetNodeByPath(string browsePath)
            //{
            //    if (NodeList.ContainsKey(browsePath))
            //        return NodeList[browsePath];
            //    else
            //        return null;
            //}

            // 20230324 msh : OPCUA Browsing 속도 개선.
            public BrowseNode FindTargetNodeByPath(string browsePath)
            {
                BrowseNode node = null;

                if (ObjectNodeList.ContainsKey(browsePath))
                {
                    node = ObjectNodeList[browsePath];
                }

                //node = ObjectNodeList.FirstOrDefault(x => x.Key.EndsWith(browsePath)).Value;          // Tag의 EndsWith로 검색 시 사용

                if (node == null)
                {
                    if (NodeList.ContainsKey(browsePath))
                    {
                        return NodeList[browsePath];
                    }

                    //node = VariableNodeList.FirstOrDefault(x => x.Key.EndsWith(browsePath)).Value;    // Tag의 EndsWith로 검색 시 사용

                    if (node == null) return null;
                    else return node;
                }
                else
                    return node;
            }

            public BrowseNode FindTargetNodeByNodeId(NodeId nodeId)
            {
                if (NodeIdDic.ContainsKey(nodeId))
                    return NodeIdDic[nodeId];
                else
                    return null;
            }
        }

        #endregion

        public class ProcessDataNodeList
        {
            public List<ReadValueId> NodeList;
            public Dictionary<string, int> NodeIndex;
        }


        string _GroupName;
        [DisplayName("GroupName"), Description("Group Name"), Category("OPCUA Client Setting")]
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        string _Endpoint;
        [DisplayName("Endpoint"), Description("Endpoint for EQP OPCUA Server"), Category("OPCUA Client Setting")]
        public string Endpoint
        {
            get { return _Endpoint; }
            set { _Endpoint = value; }
        }
        string _ID;
        [DisplayName("ID"), Description("ID for OPCUA Server"), Category("OPCUA Client Setting")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _Password;
        [DisplayName("Password"), Description("Password for OPCUA Server"), Category("OPCUA Client Setting")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        string _StartingNodeId;
        [DisplayName("Starting NodeId"), Description("NodeId for starting browse. ex)ns=2;i=22879"), Category("OPCUA Client Setting")]
        public string StartingNodeId
        {
            get { return _StartingNodeId; }
            set { _StartingNodeId = value; }
        }
        // 이후 모든 browsePath 는 Starting Node 이후의 경로만 표시 가능함.
        //int _NamespaceIndex;
        //[DisplayName("NamespaceIndex"), Description("Namespace Index"), Category("OPCUA Client Setting")]
        //public int NamespaceIndex
        //{
        //    get { return _NamespaceIndex; }
        //    set { _NamespaceIndex = value; }
        //}

        string _MesAliveNodeId;
        [DisplayName("MES Alive NodeId"), Description("NodeId for MES Alive. ex)ns=2;i=22879"), Category("OPCUA Client Setting")]
        public string MesAliveNodeId
        {
            get { return _MesAliveNodeId; }
            set { _MesAliveNodeId = value; }
        }

        //EQP_TYPE, EQP_ID, UNIT_ID
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


        public OPCUAClient()
        {
            InitializeComponent();
        }

        public void InitControl(ApplicationInstance applicationInstance)
        {
            m_applicationInstance = applicationInstance;
            m_ConnectData = new ConnectData();

            //m_browseNodeList = new BrowseNodeList();
            ////m_browseNodeList.NodeList = new List<BrowseNode>();
            ////20230324 KJY - Dic으로 교체
            //m_browseNodeList.NodeList = new Dictionary<string, BrowseNode>();
            //m_browseNodeList.NodeIdDic = new Dictionary<NodeId, BrowseNode>();
            //// 20230323 msh : OPCUA Browsing 속도 개선
            //m_browseNodeList.ObjectNodeList = new Dictionary<string, BrowseNode>();

            InitBrowseNodeList();       // 20230404 msh : 추가


            // 아래 두줄은 Connect할때 하도록 이동.... (OPCServer가 내려가 있을때는 에러가 나니깐... )
            // Session은 만들어야 하니깐.. certificate만 분리
            InitConnectData();
            //m_ConnectData.FirstEventFlag = true;
        }

        private void InitConnectData()
        {
            try
            {
                if (m_ConnectData.Session == null)
                {
                    m_ConnectData.EndpointUrl = Endpoint;
                    m_ConnectData.Session = new Session(m_applicationInstance);
                    m_ConnectData.Session.UseDnsNameAndPortFromDiscoveryUrl = true;
                    m_ConnectData.Session.ConnectionStatusUpdate += Session_ConnectionStatusUpdate;

                    
                }
                else
                {
                    return;
                }
 
            } catch(Exception ex)
            {
                _LOG_($"[Exception] InitConnectData() : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }

        }
        /// <summary>
        /// 20230208 KJY - Certificate 분리. Connect 할때만 사용하도록.
        /// </summary>
        private void InitCertificate()
        {
            try
            {
                //_LOG_($"[{this.Name}] Initialize Certificated started on [{Endpoint}]");


                ////certification for client
                //var configuration = m_applicationInstance.ApplicationSettings;
                string storePath = System.Environment.CurrentDirectory;
                //CreateCertificateSettings settings = new CreateCertificateSettings()
                //{
                //    CommonName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                //    ApplicationUri = m_applicationInstance.ApplicationUri,
                //    SubjectName = configuration.ApplicationCertificate.SubjectName
                //};
                //using (var factory = m_applicationInstance.SecurityProvider.CreateCertificateFactory())
                //{
                //    m_ConnectData.Certificate = factory.CreateCertificate(storePath, settings);
                //}

                //Certification for server

                EndpointDescription endpoint;
                using (Discovery discovery = new Discovery())
                {
                    endpoint = discovery.GetMostSecureEndpoint(Endpoint);
                }

                ICertificate certificate = SecurityUtils.LoadCertificate(endpoint.ServerCertificate);

                if (certificate != null)
                {
                    m_applicationInstance.TrustedStore.Add(certificate);
                    _LOG_($"[{this.Name}] Initialize Certificate Completed : [{storePath}]:{certificate.Thumbprint}");

                    // certificate 있을때만 User 정보를 넣을수 있다.
                    //if (m_ConnectData.Session.UserIdentity == null)
                    //{
                    //    m_ConnectData.Session.UserIdentity = new UserIdentity();

                    //    m_ConnectData.Session.UserIdentity.IdentityType = UserIdentityType.UserName;
                    //    m_ConnectData.Session.UserIdentity.UserName = ID;
                    //    m_ConnectData.Session.UserIdentity.Password = Password;
                    //}
                }
                else
                {
                    _LOG_($"[{this.Name}] endpoint.ServerCertificate is null ", ECSLogger.LOG_LEVEL.WARN);
                }


                //_LOG_($"[{this.Name}] Initialize Certificate Completed : Certicate is None");


                // ID/Pawwword 있을때만
                if (ID != null && ID.Length > 0 && Password != null && Password.Length > 0)
                {
                    if (m_ConnectData.Session.UserIdentity == null)
                    {
                        m_ConnectData.Session.UserIdentity = new UserIdentity();

                        m_ConnectData.Session.UserIdentity.IdentityType = UserIdentityType.UserName;
                        m_ConnectData.Session.UserIdentity.UserName = ID;
                        m_ConnectData.Session.UserIdentity.Password = Password;
                    }
                }
                // SessionName 
                if (UNITID != null && UNITID.Length > 0)
                    m_ConnectData.Session.SessionName = $"ECS_{EQPType}_{UNITID}";
                else
                    m_ConnectData.Session.SessionName = $"ECS_{EQPType}";

                _LOG_($"[{this.Name}] Initialize Certificate Completed");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        // 20230404 msh : 재연결시 해당 변수를 초기화 해줘야 한다.
        #region InitBrowseNodeList
        public void InitBrowseNodeList()
        {
            m_browseNodeList = new BrowseNodeList();
            m_browseNodeList.NodeList = new Dictionary<string, BrowseNode>();
            m_browseNodeList.ObjectNodeList = new Dictionary<string, BrowseNode>();
            m_browseNodeList.NodeIdDic = new Dictionary<NodeId, BrowseNode>();

            m_ProcessDataNodeList = new ProcessDataNodeList();
            m_ProcessDataNodeList.NodeList = new List<ReadValueId>();
            m_ProcessDataNodeList.NodeIndex = new Dictionary<string, int>();
        }
        #endregion

        private void OPCUAClient_Load(object sender, EventArgs e)
        {
            gbClient.Text = GroupName;
            lbEndPoint.Text = Endpoint;
        }
        public void SetEndpoint()
        {
            lbEndPoint.Text = "Endpoint : " + Endpoint;
        }
        public void SetApplicationInstance(ApplicationInstance applicationInstance)
        {
            m_applicationInstance = applicationInstance;
        }
        public void Start()
        {
            btStartStop_Click(this, null);
        }
        public void Stop()
        {
            try
            {
                if (m_ConnectData != null)
                {
                    if (m_ConnectData.Session != null && m_ConnectData.Session.ConnectionStatus == ServerConnectionStatus.Connected)
                    {
                        m_ConnectData.Session.BeginDisconnect(
                                    OnDisconnectCompleted,
                                    m_ConnectData.Session);
                    }
                }
                _LOG_($"[{this.Name}]Stop OPCUAClient");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void btStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                // 20230207 KJY - InitControl()에서 옮겨옴
                // 20230208 KJY - Certificate만 여기서
                //InitConnectData();
                
                m_ConnectData.FirstEventFlag = true;

                if (m_ConnectData.Session == null)
                {
                    m_ConnectData.Session = new Session(m_applicationInstance);
                    m_ConnectData.Session.ConnectionStatusUpdate += Session_ConnectionStatusUpdate;
                }

                // Security Check 하고 connection
                if (m_ConnectData.Session.ConnectionStatus == ServerConnectionStatus.Disconnected)
                {
                    InitCertificate();

                    //m_ConnectData.Session.BeginConnect(
                    //    m_ConnectData.EndpointUrl,
                    //    //m_ConnectData.Security,
                    //    SecuritySelection.None,
                    //    null,
                    //    RetryInitialConnect.No,
                    //    m_ConnectData.Session.DefaultRequestSettings,
                    //    OnConnectCompleted,
                    //    m_ConnectData.Session);

                    // 동기일때
                    m_ConnectData.Session.Connect(m_ConnectData.EndpointUrl, SecuritySelection.None);
                    
                    _LOG_($"[{this.Name}] Connection Success to [{m_ConnectData.EndpointUrl}] with SessionName : [{m_ConnectData.Session.SessionName}]");


                    // Browse - full scan from start node
                    // 20230315 KJY - Connection 이벤트가 오는 시간차 때문에 각 개별 ECS의 CreateDataMonitoredItem(OPCUAClient.OPCUAClient Client) 에서 일괄 처리하는 것으로 수정
                    //m_browseNodeList.NodeList.Clear();

                    //_LOG_($"[{this.Name}] Start to Browse All Nodes");
                    //BrowseFromStartNode(NodeId.Parse(this.StartingNodeId), null);
                    //_LOG_($"[{this.Name}] Complete to Browse All Nodes : {m_browseNodeList.NodeList.Count} nodes");

                }
                else
                {
                    //m_ConnectData.Session.BeginDisconnect(
                    //    OnDisconnectCompleted,
                    //    m_ConnectData.Session);

                    // 동기 일때
                    m_ConnectData.Session.Disconnect();
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        

        private void OnConnectCompleted(IAsyncResult result)
        {
            // need to make sure the results are processed on the correct thread.
            if (InvokeRequired)
            {
                BeginInvoke(new AsyncCallback(OnConnectCompleted), result);
                return;
            }

            // get the session used to send the request which was passed as the userData in the Begin call.
            Session session = (Session)result.AsyncState;

            try
            {
                
                session.EndConnect(result);

                _LOG_($"[{this.Name}] Connection Success to [{m_ConnectData.EndpointUrl}] with SessionName : [{session.SessionName}]");
                // Browse - full scan from start node
                m_browseNodeList.NodeList.Clear();

                _LOG_($"[{this.Name}] Start to Browse All Nodes");
                BrowseFromStartNode(NodeId.Parse(this.StartingNodeId), null);
                //_LOG_($"[{this.Name}] Complete to Browse All Nodes : {m_browseNodeList.NodeList.Count} nodes");
                // 20230324 msh : OPCUA Browsing 속도 개선.
                _LOG_($"[{this.Name}] Complete to Browse All Object Nodes : {m_browseNodeList.ObjectNodeList.Count} nodes");
            }
            catch (Exception e)
            {
                ExceptionDlg.ShowInnerException(this.Text, e);
            }
        }



        /// <summary>
        ///  NodeId를 알아내기 위해 connection이 완료된후에 메모리로 올린다.
        ///  browsePath로 알아내면 된다.
        /// </summary>
        /// <param name="startingNode"></param>
        /// <param name="prefixPath"></param>
        //public void BrowseFromStartNode(NodeId startingNode, string prefixPath)
        //{
        //    BrowseContext context = new BrowseContext();
        //    context.BrowseDirection = BrowseDirection.Forward;
        //    context.ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences;
        //    context.IncludeSubtypes = true;
        //    context.MaxReferencesToReturn = 0;



        //    NodeId nodeId = startingNode;
        //    byte[] continuationPoint = null;

        //    string browsePath = string.Empty;

        //    // browse the references (setting a 10 second timeout).
        //    List<ReferenceDescription> references = m_ConnectData.Session.Browse(
        //        nodeId,
        //        context,
        //        new RequestSettings() { OperationTimeout = 10000 },
        //        out continuationPoint);

        //    foreach (ReferenceDescription reference in references)
        //    {
        //        if (prefixPath != null && prefixPath.Length > 0)
        //            browsePath = prefixPath + $".{reference.DisplayName.ToString()}";
        //        else
        //            browsePath = reference.DisplayName.ToString();

        //        BrowseNode browseNode = new BrowseNode();
        //        browseNode.nodeId = NodeId.Parse(reference.NodeId.ToString());
        //        browseNode.displayName = reference.DisplayName.ToString();
        //        browseNode.browsePath = browsePath;
        //        browseNode.browseName = reference.BrowseName.ToString();
        //        browseNode.nodeClass = reference.NodeClass;

        //        m_browseNodeList.NodeList.Add(browseNode);

        //        BrowseFromStartNode(NodeId.Parse(browseNode.nodeId.ToString()), browseNode.browsePath);
        //    }


        //}

        // 20230324 msh : OPCUA Browsing 속도 개선. 1 = Object만 검색, 0은 All
        public void BrowseFromStartNode(NodeId startingNode, string prefixPath, uint nodeClassMask = 0)
        {
            BrowseContext context = new BrowseContext();
            context.BrowseDirection = BrowseDirection.Forward;
            context.ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences;
            context.IncludeSubtypes = true;
            context.MaxReferencesToReturn = 0;
            context.NodeClassMask = nodeClassMask;      


            NodeId nodeId = startingNode;
            byte[] continuationPoint = null;

            string browsePath = string.Empty;

            // browse the references (setting a 10 second timeout).
            List<ReferenceDescription> references = m_ConnectData.Session.Browse(
                nodeId,
                context,
                new RequestSettings() { OperationTimeout = 10000 },
                out continuationPoint);

            foreach (ReferenceDescription reference in references)
            {
                if (prefixPath != null && prefixPath.Length > 0)
                    browsePath = prefixPath + $".{reference.DisplayName.ToString()}";
                else
                    browsePath = reference.DisplayName.ToString();

                BrowseNode browseNode = new BrowseNode();
                browseNode.nodeId = NodeId.Parse(reference.NodeId.ToString());
                browseNode.displayName = reference.DisplayName.ToString();
                browseNode.browsePath = browsePath;
                //browseNode.browseName = reference.BrowseName.ToString();
                // 20230323 msh : NamespaceIndex를 사용하기 위해 수정
                browseNode.browseName = reference.BrowseName;
                browseNode.nodeClass = reference.NodeClass;

                if (nodeClassMask == 0)
                {
                    m_browseNodeList.NodeList.Add(browseNode.browsePath, browseNode);
                    m_browseNodeList.NodeIdDic.Add(browseNode.nodeId, browseNode);
                }
                else
                {
                    // 20230324 msh : OPCUA Browsing 속도 개선
                    m_browseNodeList.ObjectNodeList.Add(browsePath, browseNode);
                }

                BrowseFromStartNode(NodeId.Parse(browseNode.nodeId.ToString()), browseNode.browsePath, nodeClassMask);
            }


        }



        private void OnDisconnectCompleted(IAsyncResult result)
        {
            // need to make sure the results are processed on the correct thread.
            if (InvokeRequired)
            {
                BeginInvoke(new AsyncCallback(OnDisconnectCompleted), result);
                return;
            }

            // get the session used to send the request which was passed as the userData in the Begin call.
            Session session = (Session)result.AsyncState;

            try
            {
                session.EndDisconnect(result);
            }
            catch (Exception e)
            {
                ExceptionDlg.ShowInnerException(this.Text, e);
            }
        }

        

        private void Session_ConnectionStatusUpdate(Session sender, ServerConnectionStatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ServerConnectionStatusUpdateEventHandler(Session_ConnectionStatusUpdate), sender, e);
                return;
            }
            // check that the current session matches the session that raised the event.
            if (!Object.ReferenceEquals(m_ConnectData.Session, sender))
            {
                return;
            }

            lock (this)
            {
                lbStatus.BackColor = Color.Orange;
                lbStatus.ForeColor = Color.White;

                switch (e.Status)
                {
                    case ServerConnectionStatus.Disconnected:
                        btStartStop.Text = "Connect";
                        lbStatus.Text = "Disconnected";
                        lbStatus.BackColor = Color.Red;
                        lbStatus.ForeColor = Color.White;

                        //Subscribe delete
                        //DeleteSubscription();

                        break;
                    case ServerConnectionStatus.Connected:
                        btStartStop.Text = "Disonnect";
                        lbStatus.Text = "Connected";

                        lbStatus.BackColor = Color.Blue;
                        lbStatus.ForeColor = Color.White;

                        //Subscribe add
                        //CreateSubscription();

                        break;
                    case ServerConnectionStatus.ConnectionWarningWatchdogTimeout:
                        // update status label
                        lbStatus.Text = "ConnectionWarningWatchdogTimeout";
                        break;
                    case ServerConnectionStatus.ConnectionErrorClientReconnect:
                        // update status label
                        lbStatus.Text = "ConnectionErrorClientReconnect";
                        break;
                    case ServerConnectionStatus.ServerShutdownInProgress:
                        // update status label
                        lbStatus.Text = "ServerShutdownInProgress";
                        break;
                    case ServerConnectionStatus.ServerShutdown:
                        // update status label
                        lbStatus.Text = "ServerShutdown";
                        break;
                    case ServerConnectionStatus.SessionAutomaticallyRecreated:
                        // update status label
                        lbStatus.Text = "SessionAutomaticallyRecreated";
                        break;
                    case ServerConnectionStatus.Connecting:
                        // update status label
                        lbStatus.Text = "Connecting";
                        break;
                    case ServerConnectionStatus.LicenseExpired:
                        // update status label
                        lbStatus.Text = "LicenseExpired";
                        break;

                }
            }

        }

        public void UpdatelbStatusText(string Statusmessage)
        {
            if(InvokeRequired)
                Invoke(new Action(() => UpdatelbStatusText(Statusmessage)));
            else
            {
                if (Statusmessage == "Connected")
                {
                    lbStatus.BackColor = Color.Blue;
                    lbStatus.ForeColor = Color.White;
                    lbStatus.Text = "Connected";
                }
                else
                {
                    lbStatus.BackColor = Color.Orange;
                    lbStatus.ForeColor = Color.White;
                    lbStatus.Text = Statusmessage;
                }
            }

        }


        public void DeleteSubscription()
        {
            if (m_ConnectData.Subscription != null)
            {
                m_ConnectData.Subscription.Delete(new RequestSettings() { OperationTimeout = 5000 });
                m_ConnectData.Subscription = null;
            }
        }

        public void CreateSubscription()
        {
            try
            {
                m_ConnectData.FirstEventFlag = true;

                if (m_ConnectData.Subscription == null)
                {
                    m_ConnectData.Subscription = new Subscription(m_ConnectData.Session);
                    m_ConnectData.Subscription.Create(new RequestSettings() { OperationTimeout = 10000 });
                }
                else
                {
                    //
                    //if (dataMonitoredItems != null && dataMonitoredItems.Count > 0)
                    //{
                    //    List<StatusCode> results = m_ConnectData.Subscription.DeleteMonitoredItems(
                    //    dataMonitoredItems,
                    //    new RequestSettings() { OperationTimeout = 10000 });
                    //}
                    if (m_ConnectData.Subscription.ConnectionStatus == SubscriptionConnectionStatus.Deleted)
                    {
                        m_ConnectData.Subscription = new Subscription(m_ConnectData.Session);
                        m_ConnectData.Subscription.Create(new RequestSettings() { OperationTimeout = 10000 });
                    }
                    //m_ConnectData.Subscription.Delete(new RequestSettings() { OperationTimeout = 10000 });
                    //m_ConnectData.Subscription = new Subscription(m_ConnectData.Session);
                    //m_ConnectData.Subscription.Create(new RequestSettings() { OperationTimeout = 10000 });
                }

                //20221228 KJY - 무조건 새로 만든다.
                //m_ConnectData.Subscription = new Subscription(m_ConnectData.Session);
                //m_ConnectData.Subscription.Create(new RequestSettings() { OperationTimeout = 10000 });
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                UnifiedAutomation.UaClient.Controls.BrowseDlg dialog = new UnifiedAutomation.UaClient.Controls.BrowseDlg();
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.Size = new Size(500, 500);
                dialog.Location = this.Location;

                NodeId nodeId = dialog.ShowDialog(m_ConnectData.Session, ObjectIds.ObjectsFolder, ReferenceTypeIds.HierarchicalReferences);
            }
            catch (Exception exception)
            {
                ExceptionDlg.ShowInnerException(this.Text, exception);
            }
        }


        #region Read Node

        public bool ReadValueByPath(string nodePath, out Boolean readValue)
        {
            DataValue readData = ReadNodeByPath(nodePath);
            if (readData == null)
            {
                readValue = false;
                return false;
            }
            else
            {
                readValue = readData.Value != null ? (Boolean)readData.Value : false;
                return true;
            }
                
        }
        public bool ReadValueByPath(string nodePath, out UInt16 readValue)
        {
            DataValue readData = ReadNodeByPath(nodePath);
            if (readData == null)
            {
                readValue = 0;
                return false;
            }
            else
            {
                readValue = readData.Value != null ? (UInt16)readData.Value : (UInt16)0;
                return true;
            }

        }

        public bool ReadValueByPath(string nodePath, out String readValue)
        {
            DataValue readData = ReadNodeByPath(nodePath);
            if (readData == null)
            {
                readValue = String.Empty;
                return false;
            }
            else
            {
                readValue = readData.Value != null ? readData.Value.ToString():string.Empty;

                if (readValue == "")    // 20230404 msh : 값이 없는경우도 false를 리턴하도록함.
                {
                    return false;
                }
                return true;
            }

        }

        public bool ReadValueByNodeId(string nodeId, out Boolean readValue)
        {
            NodeId targetNodeId = NodeId.Parse(nodeId);

            ReadValueId targetNode = new ReadValueId();
            targetNode.NodeId = targetNodeId;

            // node의 attribute가 Value임.
            targetNode.AttributeId = Attributes.Value;

            DataValue readData = ReadNode(targetNode);

            if (readData == null)
            {
                readValue = false;
                return false;
            }
            else
            {
                readValue = readData.Value != null ? (Boolean)readData.Value : false;
                return true;
            }
        }


        public DataValue ReadNodeByPath(string nodePath)
        {
            BrowseNode node = m_browseNodeList.FindTargetNodeByPath(nodePath);
            if (node == null) return null;

            NodeId nodeId = node.nodeId;

            ReadValueId targetNode = new ReadValueId();
            targetNode.NodeId = nodeId;

            // node의 attribute가 Value임.
            targetNode.AttributeId = Attributes.Value;

            return ReadNode(targetNode);
        }

        public DataValue ReadNode(ReadValueId nodeToRead)
        {
            if (nodeToRead == null)
            {
                //log

                return null;
            }
            List<ReadValueId> nodesToRead = new List<ReadValueId>();
            nodesToRead.Add(nodeToRead);

            List<DataValue> results = ReadNodes(nodesToRead);

            if (results != null && results.Count > 0)
            {
                return results[0];
            }
            else
            {
                //log

                return null;
            }

        }

        

        public List<DataValue> ReadNodesByPathList(List<string> path, out List<BrowseNode> browseNodeOut, out List<ReadValueId> readValueIdOut)
        {
            List<ReadValueId> nodesToRead = new List<ReadValueId>();
            browseNodeOut = new List<BrowseNode>();

            foreach (string nodePath in path)
            {
                BrowseNode node = m_browseNodeList.FindTargetNodeByPath(nodePath);
                if (node == null)
                {
                    _LOG_($"node Path [{nodePath}] is not Exist", ECSLogger.LOG_LEVEL.WARN);
                    continue;
                }

                browseNodeOut.Add(node);

                NodeId nodeId = node.nodeId;
                ReadValueId targetNode = new ReadValueId();
                targetNode.NodeId = nodeId;
                targetNode.AttributeId = Attributes.Value;

                nodesToRead.Add(targetNode);
            }
            readValueIdOut = nodesToRead;

            if (nodesToRead.Count > 0)
                return ReadNodes(nodesToRead);
            else
                return null;
        }

        

        public List<DataValue> ReadNodes(List<ReadValueId> nodesToRead)
        {
            try
            {
                if (nodesToRead == null || nodesToRead.Count < 1)
                {
                    //log

                    return null;
                }

                List<DataValue> results = m_ConnectData.Session.Read(
                        nodesToRead,
                        0,
                        TimestampsToReturn.Both,
                        new RequestSettings() { OperationTimeout = 10000 });
                /// [Read Attribute]
                /// 

                return results;
            } catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        #endregion


        #region TranslateBrowsePath
        public BrowsePathResult TranslateBrowsePathToNodeId(BrowsePath pathToTranslate)
        {
            if (pathToTranslate == null)
            {
                //log
                return null;
            }

            List<BrowsePath> pathsToTranslate = new List<BrowsePath>();
            pathsToTranslate.Add(pathToTranslate);

            List<BrowsePathResult> result = TranslateBrowsePathsToNodeIds(pathsToTranslate);
            if (result != null && result.Count > 0)
            {
                return result[0];
            } else
            {
                return null;
            }
        }

        public List<BrowsePathResult> TranslateBrowsePathsToNodeIds(List<BrowsePath> pathsToTranslate)
        {
            if (pathsToTranslate == null || pathsToTranslate.Count < 1)
            {
                //log

                return null;
            }


            List<BrowsePathResult> results = m_ConnectData.Session.TranslateBrowsePath(
                    pathsToTranslate,
                    new RequestSettings() { OperationTimeout = 10000 });

            return results;
        }

        // 20230323 msh : fullBrowseName 추가
        public BrowsePath GetBrowsePath(NodeId startingNodeId, IList<QualifiedName> qnames, string fullBrowseName = "")
        {
            BrowsePath browsePath = new BrowsePath();
            browsePath.StartingNode = startingNodeId;
            browsePath.UserData = fullBrowseName;

            foreach (QualifiedName qname in qnames)
            {
                browsePath.RelativePath.Elements.Add(new RelativePathElement()
                {
                    ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences,
                    IncludeSubtypes = true,
                    IsInverse = false,
                    TargetName = qname
                });
            }

            return browsePath;
        }
        #endregion



        #region Write to Nodes
        private WriteValue SetWriteValueWithObject(string targetPath, object value)
        {
            WriteValue writeItem = new WriteValue();
            BrowseNode node = m_browseNodeList.FindTargetNodeByPath(targetPath);
            if (node == null)
            {
                _LOG_($"Node Path Not Founded : [{targetPath}]", ECSLogger.LOG_LEVEL.WARN);
                return null;
            }

            

            writeItem.NodeId = node.nodeId;
            DataValue dataValue = new DataValue();
            dataValue.Value = value;

            writeItem.Value = dataValue;
            //Attribute가 value
            writeItem.AttributeId = Attributes.Value;
            writeItem.UserData = targetPath;        // 20230320 msh : TagPath 추가

            // 20230330 msh : Log찍는데 너무 많은 시간이 소요 됨. 개선 필요
            //_LOG_($"Prepare WriteItem : [{targetPath}:{node.nodeId}] :: [{value}]");

            return writeItem;
        }
        private WriteValue SetWriteValueWithDataValue(string targetPath, DataValue dataValue)
        {
            WriteValue writeItem = new WriteValue();
            BrowseNode node = m_browseNodeList.FindTargetNodeByPath(targetPath);
            if (node == null)
            {
                _LOG_($"Node Path Not Founded : [{targetPath}]", ECSLogger.LOG_LEVEL.WARN);
                return null;
            }
            writeItem.NodeId = node.nodeId;
            //writeItem.Value = dataValue;
            // 따로 만들어서 SourceTimestamp만 따로 넣어보자.
            DataValue newDataValue = new DataValue();
            newDataValue.Value = dataValue.Value;
            newDataValue.SourceTimestamp = dataValue.SourceTimestamp;
            writeItem.Value = newDataValue;

            writeItem.AttributeId = Attributes.Value;
            writeItem.UserData = targetPath;        // 20230320 msh : TagPath 추가

            // 20230330 msh : Log찍는데 너무 많은 시간이 소요 됨. 개선 필요
            //_LOG_($"Prepare WriteItem : [{targetPath}:{node.nodeId}] :: [{dataValue.Value}:{dataValue.SourceTimestamp}]");

            return writeItem;
        }

        public bool WriteNodeByPath(string targetPath, object value)
        {
            WriteValue writeItem = new WriteValue();
            BrowseNode node = m_browseNodeList.FindTargetNodeByPath(targetPath);
            if (node == null)
            {
                _LOG_($"Node Path Not Founded : [{targetPath}]", ECSLogger.LOG_LEVEL.WARN);
                return false;
            }
            
            writeItem.NodeId = node.nodeId;
            DataValue dataValue = new DataValue();
            dataValue.Value = value;

            writeItem.Value = dataValue;
            //Attribute가 value
            writeItem.AttributeId = Attributes.Value;
            writeItem.UserData = targetPath;        // 20230320 msh : TagPath 추가

            _LOG_($"Prepare WriteItem : [{targetPath}:{node.nodeId}] :: [{value}]");

            return WriteNode(writeItem);
        }


        public bool WriteNodeWithDic(Dictionary<string, object> writeItems)
        {
            List<WriteValue> nodesToWrite = new List<WriteValue>();
            StringBuilder logBuffer = new StringBuilder();
            logBuffer.AppendLine($"WriteNodeWithDic Count : {writeItems.Count}");

            string lastKey = writeItems.Last().Key;

            foreach (KeyValuePair<string, Object> item in writeItems)
            {
                WriteValue writeItem = new WriteValue();
                BrowseNode node = m_browseNodeList.FindTargetNodeByPath(item.Key);
                if(node == null)
                {
                    _LOG_($"Node Path Not Founded : [{item.Key}]", ECSLogger.LOG_LEVEL.WARN);
                    continue;
                }
                writeItem.NodeId = node.nodeId;
                
                //DataValue class는 잘 기억해둬라. timestamp를 옮길때도 여기의 값이 쓰여진다.
                DataValue dataValue = new DataValue();
                dataValue.Value = item.Value;
                
                writeItem.Value = dataValue;

                //Attribute가 value
                writeItem.AttributeId = Attributes.Value;
                writeItem.UserData = item.Key;        // 20230320 msh : TagPath 추가

                nodesToWrite.Add(writeItem);

                // 20230330 msh : Log찍는데 너무 많은 시간이 소요 됨. 개선 필요
                //_LOG_($"Prepare WriteItem : [{item.Key}:{node.nodeId}] :: [{item.Value}]");

                string log = string.Format($"Prepare WriteItem : [{item.Key}:{node.nodeId}] :: [{item.Value}]");

                logBuffer.Append(log);
                if (lastKey != item.Key) logBuffer.AppendLine();
            }

            _LOG_(logBuffer.ToString(), ECSLogger.LOG_LEVEL.INFO);

            return WriteNodes(nodesToWrite);

        }

        public bool WriteNode(WriteValue nodeToWrite)
        {
            List<WriteValue> nodesToWrite = new List<WriteValue>();
            nodesToWrite.Add(nodeToWrite);

            return WriteNodes(nodesToWrite);
        }

        public bool WriteNodes(List<WriteValue> nodesToWrite)
        {
            if (nodesToWrite == null || nodesToWrite.Count < 1)
            {
                // log
                _LOG_("No node to write", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }


            // read the value (setting a 10 second timeout).
            List<StatusCode> results = m_ConnectData.Session.Write(
                nodesToWrite,
                new RequestSettings() { OperationTimeout = 10000 });
            /// [Basic Write]
            _LOG_("[WriteNodes(List<WriteValue> nodesToWrite)] Start", ECSLogger.LOG_LEVEL.INFO);
           
            StringBuilder logBuffer = new StringBuilder();
            logBuffer.AppendLine($"WriteNodes Count : {results.Count}");

            for (int i = 0; i < results.Count; i++)
            {
                // log 용이 될듯.. 
                // 20230330 msh : Log찍는데 너무 많은 시간이 소요 됨. 개선 필요
                //_LOG_($"[{nodesToWrite[i].NodeId}:{nodesToWrite[i].Value}] : {results[i].Message}", ECSLogger.LOG_LEVEL.INFO);
                string resultLog = (results[i].Message == "[Good]" ? "Good" : results[i].Diagnostics.SymbolicId);
                string log = string.Format($"[{nodesToWrite[i].UserData}:{nodesToWrite[i].Value}] : {resultLog}");

                if (nodesToWrite[i].UserData == null)
                {
                    ;
                }

                logBuffer.Append(log);
                if (i < results.Count - 1) logBuffer.AppendLine();
            }
            
            _LOG_(logBuffer.ToString(), ECSLogger.LOG_LEVEL.INFO);

            return results != null;

        }
        #endregion


        #region Write with Async

        public bool WriteNodeAsync(WriteValue nodeToWrite)
        {
            if (nodeToWrite == null)
            {
                return false;
            }

            List<WriteValue> nodesToWrite = new List<WriteValue>();
            nodesToWrite.Add(nodeToWrite);

            return WriteNodesAsync(nodesToWrite);
        }

        public bool WriteNodesAsync(List<WriteValue> nodesToWrite)
        {
            if (nodesToWrite.Count < 1)
            {
                // List가 적어도 하나 있어야 함.
                return false;
            }

            m_ConnectData.Session.BeginWrite(
                    nodesToWrite,
                    new RequestSettings() { OperationTimeout = 10000 },
                    OnWriteComplete,
                    new WriteState() { Session = m_ConnectData.Session, NodesToWrite = nodesToWrite });

            return true;
        }

        /// <summary>
        /// A object used to pass state with an asynchronous write call.
        /// </summary>
        private class WriteState
        {
            public Session Session { get; set; }
            public List<WriteValue> NodesToWrite { get; set; }
        }
        private void OnWriteComplete(IAsyncResult result)
        {
            // need to make sure the results are processed on the correct thread.
            if (InvokeRequired)
            {
                BeginInvoke(new AsyncCallback(OnWriteComplete), result);
                return;
            }

            // get the session used to send the request which was passed as the userData in the Begin call.
            WriteState state = (WriteState)result.AsyncState;

            try
            {
                // get the results.
                List<StatusCode> results = state.Session.EndWrite(result);

                // don't update the controls if the session has changed.
                if (!Object.ReferenceEquals(state.Session, m_ConnectData.Session))
                {
                    return;
                }

                // update the controls.
                for (int ii = 0; ii < results.Count; ii++)
                {
                    ((TextBox)state.NodesToWrite[ii].UserData).Text = results[ii].ToString();
                }
            }
            catch (Exception exception)
            {
                // don't display any error if the session has changed.
                if (Object.ReferenceEquals(state.Session, m_ConnectData.Session) && Visible)
                {
                    ExceptionDlg.ShowInnerException(this.Text, exception);
                }
            }
        }

        
        #endregion


        public void SetDataMonitoringItemByTranslatePath(List<MonitoredItem> dataMonitoredItems, string BrowsePath)
        {
            NodeId startingNode = NodeId.Parse(StartingNodeId);

            BrowsePath StatusPath = new BrowsePath();
            StatusPath = GetBrowsePath(startingNode, AbsoluteName.ToQualifiedNames(BrowsePath));
            BrowsePathResult BrowsePathResult = TranslateBrowsePathToNodeId(StatusPath);
            if (BrowsePathResult == null || BrowsePathResult.Targets.Count <1)
            {
                // log  : 경로에 node가 없다.
                _LOG_($"Fail to find MonitoredItem node by TranslatePath [{BrowsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
            else
            {
                //StatusBrowsePathResult.Targets[0].TargetId
                DataMonitoredItem monitoredItem = new DataMonitoredItem(NodeId.Parse(BrowsePathResult.Targets[0].TargetId.ToString()));
                dataMonitoredItems.Add(monitoredItem);

                return;
            }
        }

        public void SetDataMonitoringItemByNodeId(List<MonitoredItem> dataMonitoredItems, string TargetNodeId)
        {
            if(TargetNodeId == null || TargetNodeId.Length == 0)
            {
                _LOG_($"Fail to find MonitoredItem node by nodeId [{TargetNodeId}]", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
            else
            {
                DataMonitoredItem monitoredItem = new DataMonitoredItem(NodeId.Parse(TargetNodeId));
                dataMonitoredItems.Add(monitoredItem);
            }
        }

        public void SetDataMonitoringItemByBrowsePath(List<MonitoredItem> dataMonitoredItems, string targetPath)
        {
            BrowseNode targetNode = m_browseNodeList.FindTargetNodeByPath(targetPath);
            

            if (targetNode == null || targetNode.nodeId == null)
            {
                _LOG_($"Fail to find MonitoredItem node by path [{targetPath}]", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
            else
            {
                //DataMonitoredItem monitoredItem = new DataMonitoredItem(targetNode.nodeId);
                //dataMonitoredItems.Add(monitoredItem);

                // 20230323 msh : OPCUA Browsing 속도 개선
                dataMonitoredItems.Add(new DataMonitoredItem(targetNode.nodeId)
                {
                    UserData = targetPath,
                    // DataChangeTrigger.Status                 : Status 가 변경되었을 때에만 감지 (PLC와의 연결 상태에 대한 것만을 감지한다고 보면 됨)
                    // DataChangeTrigger.StatusValue            : Status 또는 Value 가 변경되었을 때에만 감지(위의 것 + 값이 변경되었을 때만 감지함.True->True 로 새로 write 하더라도 감지 안됨)
                    // DataChangeTrigger.StatusValueTimestamp   : Status, Value, 또는 Timestamp 가 변경되었을 때에만 감지(연결상태, 값, write 모두에 대해 감지함)
                    DataChangeTrigger = DataChangeTrigger.StatusValue,
                });
            }
        }


        // 20230323 msh : OPCUA Browsing 속도 개선
        public void SetBrowseNodeList(List<string> tagList, bool processDataRead = false, string skipTag = "")
        {
            int nsIdx = 0;
            int nCount = 0;

            BrowseNode temp;
            StringBuilder buffer = new StringBuilder();
            StringBuilder buffer2 = new StringBuilder();
            List<BrowsePath> pathsToTranslate = new List<BrowsePath>();

            foreach (var browsename in tagList)
            {
                // get the browse paths.
                string[] taglevel = browsename.Split('.');

                if (taglevel[1] == skipTag) continue;       // Monitoring Tag

                for (int i = 0; i < taglevel.Count() - 1; i++)
                {
                    buffer2.Append($"{string.Format("{0}", (i == 0) ? taglevel[i] : "." + taglevel[i])}");

                    temp = m_browseNodeList.FindTargetNodeByPath(buffer2.ToString());

                    buffer.Append($"/{temp.browseName}");

                    nsIdx = temp.browseName.NamespaceIndex;
                }

                if (taglevel.Count() > 1)       // 마지막 TagName
                {
                    buffer2.Append($".{taglevel[taglevel.Count() - 1]}");
                    temp = m_browseNodeList.FindTargetNodeByPath(buffer2.ToString());
                    buffer.Append($"/{nsIdx}:{taglevel[taglevel.Count() - 1]}");
                }

                pathsToTranslate.Add(GetBrowsePath(NodeId.Parse(this.StartingNodeId), AbsoluteName.ToQualifiedNames($"{buffer}"), buffer2.ToString()));
                buffer.Clear();
                buffer2.Clear();

                nCount++;

                if (nCount > 999)
                {
                    ReadTranslateBrowsePath(pathsToTranslate, processDataRead);
                    pathsToTranslate.Clear();
                    nCount = 0;
                }
            }

            ReadTranslateBrowsePath(pathsToTranslate, processDataRead);
        }

        // 20230323 msh : OPCUA Browsing 속도 개선
        #region ReadTranslateBrowsePath
        public void ReadTranslateBrowsePath(List<BrowsePath> pathsToTranslate, bool processDataRead = false)
        {
            try
            {
                // translate the references (setting a 10 second timeout).
                List<BrowsePathResult> results = m_ConnectData.Session.TranslateBrowsePath(
                   pathsToTranslate,
                   new RequestSettings() { OperationTimeout = 100000 });

                SetBrowsePathResult(results, pathsToTranslate, processDataRead);
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }

        }
        #endregion

        #region SetBrowseNodeList
        private void SetBrowsePathResult(List<BrowsePathResult> results, List<BrowsePath> pathsToTranslate, bool processDataRead = false)
        {
            for (int i = 0; i < results.Count; i++)
            {
                string browsePath = pathsToTranslate[i].UserData.ToString();

                if (results[i].Targets.Count == 0)
                {
                    _LOG_($"[results is null] : {browsePath}", ECSLogger.LOG_LEVEL.ERROR);
                    continue;
                }
                string sNodeId = results[i].Targets[0].TargetId.ToString();

                string[] tagName = browsePath.Split('.');

                BrowseNode browseNode = new BrowseNode();
                browseNode.nodeId = NodeId.Parse(sNodeId);
                browseNode.displayName = tagName[tagName.Length - 1];  //reference.DisplayName.ToString();
                browseNode.browsePath = browsePath;
                //browseNode.browseName = reference.BrowseName;
                browseNode.nodeClass = NodeClass.Variable;

                if (m_browseNodeList.NodeList.ContainsKey(browsePath) == false)
                {
                    m_browseNodeList.NodeList.Add(browsePath, browseNode);

                    if (processDataRead)
                    {
                        m_ProcessDataNodeList.NodeList.Add(new ReadValueId()
                        {
                            NodeId = NodeId.Parse(sNodeId),
                            AttributeId = Attributes.Value,
                            UserData = browsePath
                        });

                        m_ProcessDataNodeList.NodeIndex.Add(browsePath, m_ProcessDataNodeList.NodeList.Count - 1);
                    }
                }
                else
                {
                    _LOG_($"[ContainsKey] : {browsePath}", ECSLogger.LOG_LEVEL.ERROR);
                    continue;
                }
            }
        }
        #endregion
        

        #region LogForEQP
        public void _LOG_(string strDescription, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"{EQPID}, {UNITID}, {level.ToString()},  {caller}, {strDescription}";
            if(UNITID ==null)
                UNITID = String.Empty;
            string log = $"{EQPID}, {((UNITID.Length>0)?UNITID:this.Name)}, {caller}, {strDescription}";
            
            if(this.UNITID == null || this.UNITID.Length<1)
                ECSLogger.Logger.WriteLog(log, level, this.EQPID);
            else
                ECSLogger.Logger.WriteLog(log, level, this.UNITID);
        }

        


        #endregion
    }


}
