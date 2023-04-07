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

namespace ECS_OCV
{
    public partial class ECS_OCV : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_OCV(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\OCV";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_OCV";

            _LOG_("Start OCV-ACIR ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            _LOG_("Initialize F1DCR01");
            F1OCV01.InitControl(m_application);
        }

        #region _LOG_
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"OCV, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }
        #endregion

        private void ECS_OCV_Load(object sender, EventArgs e)
        {

        }

        private void ECS_OCV_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
