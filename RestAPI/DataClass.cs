using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIServer
{
    public class DataClass
    {
        

        public static string UNIT_ID;
        public static string EQP_TYPE;
        public static string EQP_ID;

        public static OPCUAClient.OPCUAClient EQPClient;
        public static string CommandPath;


        //1	STOP_PROCESS
        //2	RESTART_PROCESS
        //4	PAUSE_PROCESS
        //8	RESUME_PROCESS
        //16	FORCE_OUT_TRAY


    }
}
