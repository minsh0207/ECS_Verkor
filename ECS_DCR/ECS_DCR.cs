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

namespace ECS_DCR
{
    public partial class ECS_DCR : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_DCR(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\DCR";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_DCR";

            _LOG_("Start DCIR ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            _LOG_("Initialize F1DCR01");
            F1DCR01.InitControl(m_application);

        }

        

        #region _LOG_
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"DCR, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }

        #endregion

        private void ECS_DCR_Load(object sender, EventArgs e)
        {
            // OPCUA Client - Connect
            // REST API Server - Start
        }

        private void ECS_DCR_FormClosing(object sender, FormClosingEventArgs e)
        {
            // OPCUA Client - Disconnect
            // REST API Server - Close

            _LOG_("Close DCIR ECS", ECSLogger.LOG_LEVEL.ALL);

            _LOG_("Stop ECS for EQP [F1DCR01]", ECSLogger.LOG_LEVEL.ALL);
            F1DCR01.StopECS_DCR();
        }
    }


}
