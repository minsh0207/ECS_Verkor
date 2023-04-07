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

namespace ECS_MIC
{
    public partial class ECS_MIC : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_MIC(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\MIC";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_MIC";

            _LOG_("Start MIC ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            _LOG_("Initialize F1MIC01");
            MICTrackInOutControl.InitControl(m_application);

            F1MIC01Control.InitControl(m_application);
        }


        #region Log
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"MIC, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }

        #endregion




    }
}
