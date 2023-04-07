using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCtrls
{
    public class MesResponseTimerClass
    {
        public string ResponsePath;
        public DateTime RequestTime;
        public List<object> parameters;
    }

    public class Global
    {
        public enum Mode
        {
            MaintenanceMode = 0,
            ManualMode = 1,
            ControlMode = 2

        }
        public enum AproMode
        {
            ControlMode = 0,
            MaintenanceMode = 1

        }

        public enum EqpStatus
        {
            Idle = 1,
            Running = 2,
            MachineTrouble = 4,
            Pause = 8,
            Loading = 16,
            Fire_Temp = 32,
            Fire_Temp_Smoke = 64
        }

        public enum Command
        {
            StopCurrentProcess = 1,
            RestartCurrentProcess = 2,
            PauseCurrentProcess = 4,
            ResumePausedProcess = 8,
            ForceOut = 16
        }

        public enum TrayLoadResponse
        {
            None = 0,
            OK = 1,
            Bypass = 2
        }

        public enum TrayType
        {
            BD_Full = 1,
            AD_Full = 2,
            BD_Empty = 4,
            AD_Empty = 8
        }

        public enum OperationMode
        {
            OCV = 1,
            Charge_CC = 2,
            Charge_CCCV = 4,
            Discharge_CC = 8,
            Discharge_CCCV = 16
        }

        /// <summary>
        /// FMS Trouble Code 정리필요
        /// </summary>
        public enum FMSTrouble
        {
            NoTray = 9001,
            EmptyTray,
            CellTray,
            

            DataError = 9101,


            OPCUAError = 9201,
            OPCUA_WriteError,
            OPCUA_ReadError,


            RestApiError = 9301


            
        }
    }
}
