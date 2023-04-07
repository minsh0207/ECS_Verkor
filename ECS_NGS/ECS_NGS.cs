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

namespace ECS_NGSorter
{
    public partial class ECS_NGS : Form
    {
        private ApplicationInstance m_application = null;
        public ECS_NGS(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\NGS";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_NGS";

            _LOG_("Start NGS ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            _LOG_("Initialize F1NGS01");
            F1NGS01.InitControl(m_application);

        }
        #region _LOG_
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"DGS, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }
        #endregion

    }
}
