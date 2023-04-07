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

namespace ECS_HPC
{
    public partial class ECS_HPC : Form
    {
        private ApplicationInstance m_application = null;

        public ECS_HPC(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            m_application.ThreadPool = new ApplicationThreadPool(1, 10000);

            InitializeComponent();

            //Logger Setting
            ECSLogger.StaticParamClass.LOGGER_LOG_ROOT = @"C:\Formation\Log\ECS\HPC";
            ECSLogger.StaticParamClass.LOGGER_LOG_BASEFILENAME = "ECS_HPC";

            _LOG_("Start HPC ECS", ECSLogger.LOG_LEVEL.ALL);

            // 각 Unit의 OPCUA Client 초기화
            HPC0110101Control.InitControl(m_application);
            HPC0110102Control.InitControl(m_application);

            HPCTrackInOutControl.InitControl(m_application);

        }

        #region Log
        private void _LOG_(string line, ECSLogger.LOG_LEVEL level = ECSLogger.LOG_LEVEL.ALL)
        {
            //string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            //string log = $"ALL, ALL, {level.ToString()}, {line}";
            string log = $"HPC, ALL, {line}";
            ECSLogger.Logger.WriteLog(log, level);
        }

        #endregion

        private void ECS_HPC_Load(object sender, EventArgs e)
        {
            //HPC0110101Control.StartECS_HPC();
            ////HPC0110102Control.StartECS_HPC();

            //HPCTrackInOutControl.StartECS_HPC();
        }

        private void ECS_HPC_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LOG_("Close HPC ECS", ECSLogger.LOG_LEVEL.ALL);

            HPC0110101Control.StopECS_HPC();
            //HPC0110102Control.StopECS_HPC();

            HPCTrackInOutControl.StopECS_HPC();


        }
    }
}
