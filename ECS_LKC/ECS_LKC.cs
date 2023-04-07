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

namespace ECS_LKC
{
    public partial class ECS_LKC : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_LKC(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\LKC";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_LKC";

            _LOG_("Start LeakCheck ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            F1LKC01.InitControl(m_application);

        }

        #region Log
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"LKC, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }

        #endregion

        private void ECS_LKC_Load(object sender, EventArgs e)
        {

        }

        private void ECS_LKC_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

