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

namespace ECS_Charger
{
    public partial class ECS_CHG : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_CHG(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\CHG";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_CHG";

            _LOG_("Start Charger/Discharger ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            _LOG_("Initialize CHG0110101");
            CHG0110101Control.InitControl(m_application);
            _LOG_("Initialize CHG0110102");
            CHG0110102Control.InitControl(m_application);
            _LOG_("Initialize CHG0110103");
            CHG0110103Control.InitControl(m_application);
            _LOG_("Initialize CHG0110104");
            CHG0110104Control.InitControl(m_application);
            _LOG_("Initialize CHG0110201");
            CHG0110201Control.InitControl(m_application);
            _LOG_("Initialize CHG0110202");
            CHG0110202Control.InitControl(m_application);
            _LOG_("Initialize CHG0110203");
            CHG0110203Control.InitControl(m_application);
            _LOG_("Initialize CHG0110204");
            CHG0110204Control.InitControl(m_application);
            _LOG_("Initialize CHG0110301");
            CHG0110301Control.InitControl(m_application);
            _LOG_("Initialize CHG0110302");
            CHG0110302Control.InitControl(m_application);
            _LOG_("Initialize CHG0110303");
            CHG0110303Control.InitControl(m_application);

        }

        //private void ConnectionStatusUpdate_ClientFMS(Session sender, ServerConnectionStatusUpdateEventArgs e)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new ServerConnectionStatusUpdateEventHandler(ConnectionStatusUpdate_ClientFMS), sender, e);
        //        return;
        //    }

        //    switch (e.Status)
        //    {
        //        // Connect면 이면 DataMonitoredItem careate
        //        case ServerConnectionStatus.Connected:
        //            //CreateDataMonitoredItem(ClientFMS);
        //            break;
        //    }
        //}

        private void ECS_CDC_Load(object sender, EventArgs e)
        {
            //_LOG_("Start ECS for Unit [CHG0110101]", ECSLogger.LOG_LEVEL.ALL);
            //CHG0110101Control.StartECS_CHG();
            //_LOG_("Start ECS for Unit [CHG0110102]", ECSLogger.LOG_LEVEL.ALL);
            //CHG0110102Control.StartECS_CHG();
        }

        #region _LOG_
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"CHG, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }

        #endregion

        private void ECS_CDC_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LOG_("Close Charger/Discharger ECS", ECSLogger.LOG_LEVEL.ALL);

            _LOG_("Stop ECS for Unit [CHG0110101]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110101Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110102]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110102Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110103]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110103Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110104]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110104Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110201]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110201Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110202]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110202Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110203]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110203Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110204]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110204Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110301]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110301Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110302]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110302Control.StopECS_CHG();
            _LOG_("Stop ECS for Unit [CHG0110303]", ECSLogger.LOG_LEVEL.ALL);
            CHG0110303Control.StopECS_CHG();

        }
    }
}
