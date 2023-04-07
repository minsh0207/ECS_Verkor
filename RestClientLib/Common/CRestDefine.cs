using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class CRestDefine
    {
        

    }

    #region _action_id define
    /// <summary>
    /// action ID
    /// </summary>
    public static class CRestModulePath
    {

        public const string POST_SQL = "ecs/SQL";

        public const string BaseUrl = "https://210.91.148.176:30011/";
        public const string LOG_PATH = @"D:\Logs\FMSSystem";

        //
        public const string SET_TRAY_INFORMATION = "ecs/setTrayInformation";
        public const string MASTER_RECIPE = "ecs/masterRecipe";
        public const string TRAY_PROCESS_START = "ecs/trayProcessStart";
        public const string TRAY_PROCESS_END = "ecs/trayProcessEnd";
        public const string TRAY_LOAD_REQUEST = "ecs/trayLoadRequest";
        public const string TRAY_UNLOAD_REQUEST = "ecs/trayUnloadRequest";
        public const string EQP_STATUS = "ecs/eqpStatus";
        public const string EQP_TROUBLE = "ecs/eqpTrouble";
        public const string TRAY_CELL_OUTPUT = "ecs/trayCellOutput";
        public const string SET_TRAY_EMPTY = "ecs/setTrayEmpty";
        public const string CREATE_TRAY_INFORMATION = "ecs/createTrayInformation";
        public const string CREATE_EMPTY_TRAY = "ecs/createEmptyTray";
        public const string CELL_PROCESS_END = "ecs/cellProcessEnd";
        public const string CELL_PACKING = "ecs/cellPacking";
        public const string TRAY_ARRIVED = "ecs/trayArrived";
        public const string MASTER_NEXT_PROCESS = "ecs/masterNextProcess";
        public const string TRAY_CELL_INPUT = "ecs/trayCellInput";

        //public const string status_eqp = "STATUS_EQP";
        //public const string status_unit = "STATUS_UNIT";
        //public const string trayLoadRequest = "TRAY_LOAD_REQUEST";
        //public const string trayArrived = "TRAY_ARRIVED";
        //public const string masterRecipe = "MASTER_RECIPE";
        //public const string processStart = "PROCESS_START";
        //public const string processEnd = "PROCESS_END";
        //public const string trayUnloadRequest = "TRAY_UNLOAD_REQUEST";
        //public const string trayUnloadComplete = "TRAY_UNLOAD_COMPLETE";
        //public const string trayNextDestination = "TRAY_NEXT_DESTINATION";
        //public const string manualTrayCellInput = "MANUAL_TRAY_CELL_INPUT";
        //public const string manualTrayCellOutput = "MANUAL_TRAY_CELL_OUTPUT";
    }
    #endregion


}
