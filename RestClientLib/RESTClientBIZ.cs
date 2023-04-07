using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class RESTClientBiz
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrayId"></param>
        /// <param name="UnitID"></param> 이건 오로지 로그파일을 남기기위해 필요함
        /// <returns></returns>
        public static _jsonDatTrayResponse GetRestTrayInfoByTrayId(string TrayId, string UnitID)
        {
            RESTClient restClient = new RESTClient();
            string sql = $"SELECT * FROM tb_dat_tray WHERE tray_id = '{TrayId}';";
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);
            _jsonDatTrayResponse result = restClient.ConvertDatTray(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            return result;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrayId"></param>
        /// <param name="UnitID"></param> 이건 오로지 로그파일을 남기기위해 필요함
        /// <returns></returns>
        public static _jsonDatCellResponse GetRestCellInfoByTrayId(string TrayId, string UnitID)
        {
            RESTClient restClient = new RESTClient();
            string sql = $"SELECT * FROM tb_dat_cell WHERE tray_id = '{TrayId}';";
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);
            _jsonDatCellResponse result = restClient.ConvertDatCell(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            return result;
        }
        public static _jsonDatCellResponse GetRestCellInfoByCellId(string CellId, string UnitID)
        {
            RESTClient restClient = new RESTClient();
            string sql = $"SELECT * FROM tb_dat_cell WHERE cell_id = '{CellId}';";
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);
            _jsonDatCellResponse result = restClient.ConvertDatCell(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            return result;
        }



        /// <summary>
        /// Model ID 정보 가져오기 ModelId가 null 이면 전체 목록
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static _jsonMstModelResponse GetRestModelInformation(string UnitID, string ModelId = null)
        {
            RESTClient restClient = new RESTClient();
            string sql = string.Empty;
            if (ModelId != null)
                sql = $"SELECT * FROM tb_mst_model WHERE model_id = '{ModelId}';";
            else
                sql = "SELECT * FROM tb_mst_model;";
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);

            _jsonMstModelResponse result = restClient.ConvertMstModel(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            return result;
        }

        /// <summary>
        /// Route 정보 가져오기. model ID를 알아야 한다.
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static _jsonMstRouteResponse GetRestRouteInformation(string ModelId, string UnitID)
        {
            RESTClient restClient = new RESTClient();
            string sql = string.Empty;
            if (ModelId != null)
                sql = $"SELECT * FROM tb_mst_route WHERE model_id = '{ModelId}';";
            else
                return null;
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);

            _jsonMstRouteResponse result = restClient.ConvertMstRoute(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            return result;
        }

        

        public static _mst_eqp GetRestEqpInfoByUnitId(string UnitID)
        {
            RESTClient restClient = new RESTClient();
            string sql = string.Empty;
            if (UnitID != null)
                sql = $"SELECT * FROM tb_mst_eqp WHERE unit_id = '{UnitID}';";
            else
                return null;
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);

            _jsonMstEqpResponse result = restClient.ConvertMstEqp(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, UnitID);

            if (result.DATA.Count > 0)
                return result.DATA[0];
            else
                return null;

        }

        public static _mst_eqp GetRestEqpInfoByEqpId(string EqpID)
        {
            RESTClient restClient = new RESTClient();
            string sql = string.Empty;
            if (EqpID != null)
                sql = $"SELECT * FROM tb_mst_eqp WHERE eqp_id = '{EqpID}';";
            else
                return null;
            string jsonResult = restClient.GetJson(enActionType.SQL_SELECT, sql);

            _jsonMstEqpResponse result = restClient.ConvertMstEqp(jsonResult);

            restClient._LOG_($"Call REST API [{sql}]", ECSLogger.LOG_LEVEL.ALL, EqpID);

            if (result.DATA.Count > 0)
                return result.DATA[0];
            else
                return null;

        }

        public static _jsonEcsApiMasterRecipeResponse CallEcsApiMasterRecipe(string model_id, string route_id, string targetEQPType, string targetProcessType, string targetProcessNo)
        {
            _jsonEcsApiMasterRecipeRequest request = new _jsonEcsApiMasterRecipeRequest();
            request.MODEL_ID = model_id;
            request.ROUTE_ID = route_id;
            request.EQP_TYPE = targetEQPType;
            request.PROCESS_TYPE = targetProcessType;
            request.PROCESS_NO = int.Parse(targetProcessNo); //int 인지 string 인지 확인 필요.
            request.ACTION_ID = "MASTER_RECIPE";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.MASTER_RECIPE);

            _jsonEcsApiMasterRecipeResponse result = JsonConvert.DeserializeObject<_jsonEcsApiMasterRecipeResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonEcsApiTrayProcessStartResponse CallEcsApiTrayProcessStart(_jsonEcsApiMasterRecipeResponse recipeDataFromOPCUA, string EQPType, string EqpID, string UnitID, List<string> TrayList, string RetryFlag = "N")
        {
            _jsonEcsApiTrayProcessStartRequest request = new _jsonEcsApiTrayProcessStartRequest();
            request.ACTION_ID = "TRAY_PROCESS_START";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;


            request.EQP_TYPE = EQPType;

            request.UNIT_ID = UnitID;
            request.RETRY_FLAG = RetryFlag;
            request.TRAY_COUNT = TrayList.Count;
            request.TRAY_LIST = new TrayLevelInfo();
            request.TRAY_LIST.TRAY_ID_1 = TrayList[0];
            if(TrayList.Count > 1)
                request.TRAY_LIST.TRAY_ID_2 = TrayList[1];

            //request.ROUTE_ID = recipeDataFromOPCUA.ROUTE_ID; //20230302 제거


            if (recipeDataFromOPCUA != null) //NG Sorter, Packing등은 Recipe가 없음
            {
                request.RECIPE_DATA = new RecipeBasicInfo();
                request.RECIPE_DATA.RECIPE_ID = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ID;
                request.RECIPE_DATA.EQP_TYPE = recipeDataFromOPCUA.RECIPE_DATA.EQP_TYPE;
                request.RECIPE_DATA.PROCESS_TYPE = recipeDataFromOPCUA.RECIPE_DATA.PROCESS_TYPE;
                request.RECIPE_DATA.PROCESS_NO = recipeDataFromOPCUA.RECIPE_DATA.PROCESS_NO;
                request.RECIPE_DATA.NEXT_PROCESS_EXIST = recipeDataFromOPCUA.RECIPE_DATA.NEXT_PROCESS_EXIST;
                request.RECIPE_DATA.OPERATION_MODE = recipeDataFromOPCUA.RECIPE_DATA.OPERATION_MODE;
                request.RECIPE_DATA.RECIPE_ITEM = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ITEM;
            }

            request.PROCESS_START_TIME = dateTime;

            //request.MODEL_ID = recipeDataFromOPCUA.MODEL_ID; //20230302 제거

            request.EQP_ID = EqpID;
            

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_PROCESS_START);
            _jsonEcsApiTrayProcessStartResponse result = JsonConvert.DeserializeObject<_jsonEcsApiTrayProcessStartResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }
        public static _jsonEcsApiTrayProcessStartResponse CallEcsApiTrayProcessStart(_jsonEcsApiMasterRecipeResponse recipeDataFromOPCUA, string EQPType, string EqpID, string UnitID, string TrayId, string RetryFlag = "N")
        {
            _jsonEcsApiTrayProcessStartRequest request = new _jsonEcsApiTrayProcessStartRequest();
            request.ACTION_ID = "TRAY_PROCESS_START";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.EQP_TYPE = EQPType;

            request.UNIT_ID = UnitID;
            request.RETRY_FLAG = RetryFlag;
            request.TRAY_COUNT = 1;
            request.TRAY_LIST = new TrayLevelInfo();
            request.TRAY_LIST.TRAY_ID_1 = TrayId;
            //request.TRAY_LIST.TRAY_ID_2 = string.Empty;
            request.TRAY_LIST.TRAY_ID_2 = null;     // 2023.03.22 msh : string.Empty -> null 변경

            //request.ROUTE_ID = recipeDataFromOPCUA.ROUTE_ID; //20230302 제거

            if (recipeDataFromOPCUA != null) //NG Sorter, Packing등은 Recipe가 없음
            { 
                request.RECIPE_DATA = new RecipeBasicInfo();
                request.RECIPE_DATA.RECIPE_ID = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ID;
                request.RECIPE_DATA.EQP_TYPE = recipeDataFromOPCUA.RECIPE_DATA.EQP_TYPE;
                request.RECIPE_DATA.PROCESS_TYPE = recipeDataFromOPCUA.RECIPE_DATA.PROCESS_TYPE;
                request.RECIPE_DATA.PROCESS_NO = recipeDataFromOPCUA.RECIPE_DATA.PROCESS_NO;
                request.RECIPE_DATA.NEXT_PROCESS_EXIST = recipeDataFromOPCUA.RECIPE_DATA.NEXT_PROCESS_EXIST;
                request.RECIPE_DATA.OPERATION_MODE = recipeDataFromOPCUA.RECIPE_DATA.OPERATION_MODE;
                request.RECIPE_DATA.RECIPE_ITEM = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ITEM;
            }

            request.PROCESS_START_TIME = dateTime;
            //request.MODEL_ID = recipeDataFromOPCUA.MODEL_ID; //20230302 제거
            request.EQP_ID = EqpID;


            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_PROCESS_START);
            _jsonEcsApiTrayProcessStartResponse result = JsonConvert.DeserializeObject<_jsonEcsApiTrayProcessStartResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonEcsApiTrayProcessStartResponse CallEcsApiTrayProcessStart_NoRecipe(string EQPType, string EqpID, string UnitID, string TrayId, string ProcessType, int ProcessNo, string RetryFlag = "N")
        {
            _jsonEcsApiTrayProcessStartRequest request = new _jsonEcsApiTrayProcessStartRequest();
            request.ACTION_ID = "TRAY_PROCESS_START";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.EQP_TYPE = EQPType;

            request.UNIT_ID = UnitID;
            request.RETRY_FLAG = RetryFlag;
            request.TRAY_COUNT = 1;
            request.TRAY_LIST = new TrayLevelInfo();
            request.TRAY_LIST.TRAY_ID_1 = TrayId;
            request.TRAY_LIST.TRAY_ID_2 = null;//string.Empty;

            request.RECIPE_DATA = new RecipeBasicInfo();
            request.RECIPE_DATA.RECIPE_ID = null;
            request.RECIPE_DATA.EQP_TYPE = EQPType;
            request.RECIPE_DATA.PROCESS_TYPE = ProcessType;
            request.RECIPE_DATA.PROCESS_NO = ProcessNo;
            request.RECIPE_DATA.NEXT_PROCESS_EXIST = null;
            request.RECIPE_DATA.OPERATION_MODE = null;
            request.RECIPE_DATA.RECIPE_ITEM = null;
            
            request.PROCESS_START_TIME = dateTime;
            //request.MODEL_ID = recipeDataFromOPCUA.MODEL_ID; //20230302 제거
            request.EQP_ID = EqpID;


            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_PROCESS_START);
            _jsonEcsApiTrayProcessStartResponse result = JsonConvert.DeserializeObject<_jsonEcsApiTrayProcessStartResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }


        public static _jsonTrayCellOutputResponse CallEcsApiTrayCellOutput(string trayId, List<_OutPutCell> _OutPutCellList, string EqpID, string UnitID)
        {
            _jsonTrayCellOutputRequest request = new _jsonTrayCellOutputRequest();
            request.ACTION_ID = "TRAY_CELL_OUTPUT";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.TRAY_ID = trayId;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            request.OUTPUT_CELL_COUNT = _OutPutCellList.Count;
            request.OUTPUT_CELL_LIST = _OutPutCellList;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_CELL_OUTPUT);
            _jsonTrayCellOutputResponse result = JsonConvert.DeserializeObject<_jsonTrayCellOutputResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonCellProcessEndResponse CallEcsApiCellProcessEndNGS(_CellGradeDataWithDefectType CellData, string EQPType, string EQPID, string UNITID)
        {
            _jsonCellProcessEndRequest request = new _jsonCellProcessEndRequest();

            request.ACTION_ID = "CELL_PROCESS_END";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.EQP_TYPE = EQPType;
            request.EQP_ID = EQPID;
            request.UNIT_ID = UNITID;

            request.CELL_ID = CellData.CellId;
            //request.LOT_ID = CellInfo.DATA[0].LOT_ID;
            request.PROCESS_END_TIME = dateTime;
            //request.MODEL_ID = CellInfo.DATA[0].MODEL_ID;
            //request.ROUTE_ID = CellInfo.DATA[0].ROUTE_ID;

            //request.RECIPE_DATA = RECIPE_DATA;
            request.PROCESSING_DATA = new CellProcEndProcessingData();
            //request.PROCESSING_DATA.PROCESS_END_TIME = dateTime;
            request.PROCESSING_DATA.NG_CODE = CellData.NGCode;
            request.PROCESSING_DATA.NG_TYPE = CellData.NGType;
            request.PROCESSING_DATA.DEFECT_TYPE = CellData.DefectType;
            request.PROCESSING_DATA.RESULT_DATA = null;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.CELL_PROCESS_END);
            _jsonCellProcessEndResponse result = JsonConvert.DeserializeObject<_jsonCellProcessEndResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonCellProcessEndResponse CallEcsApiCellProcessEnd(_CellProcessData CellProcessData, string EQPType, string EQPID, string UNITID)
        {
            _jsonCellProcessEndRequest request = new _jsonCellProcessEndRequest();

            request.ACTION_ID = "CELL_PROCESS_END";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.EQP_TYPE = EQPType;
            request.EQP_ID = EQPID;
            request.UNIT_ID = UNITID;

            request.CELL_ID = CellProcessData.CellId;
            //request.LOT_ID = CellInfo.DATA[0].LOT_ID;
            request.PROCESS_END_TIME = dateTime;
            //request.MODEL_ID = CellInfo.DATA[0].MODEL_ID;
            //request.ROUTE_ID = CellInfo.DATA[0].ROUTE_ID;

            //request.RECIPE_DATA = RECIPE_DATA;
            request.PROCESSING_DATA = new CellProcEndProcessingData();
            //request.PROCESSING_DATA.PROCESS_END_TIME = dateTime;
            request.PROCESSING_DATA.NG_CODE = CellProcessData.NGCode;
            request.PROCESSING_DATA.NG_TYPE = CellProcessData.NGType;
            request.PROCESSING_DATA.DEFECT_TYPE = String.Empty;
            request.PROCESSING_DATA.RESULT_DATA = CellProcessData.ProcessData;



            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.CELL_PROCESS_END);
            _jsonCellProcessEndResponse result = JsonConvert.DeserializeObject<_jsonCellProcessEndResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonTrayProcessEndResponse CallEcsApiTrayProcessEnd( List<string> trayList, _CellInformation trackOutCellInfo, string EqpType, string EqpID, string UnitID, _next_process NEXT_PROCESS)
        {
            //throw new NotImplementedException();
            _jsonTrayProcessEndRequest request = new _jsonTrayProcessEndRequest();

            request.ACTION_ID = "TRAY_PROCESS_END";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            if (trayList != null && trayList.Count > 0)
            {
                request.TRAY_LIST.TRAY_ID_1 = trayList[0];
                request.TRAY_COUNT = 1;
                if (trayList.Count > 1)
                {
                    request.TRAY_LIST.TRAY_ID_2 = trayList[1];
                    request.TRAY_COUNT = 2;
                }
            }
            request.PROCESS_END_TIME = dateTime;
            //request.ROUTE_ID = recipeDataFromOPCUA.ROUTE_ID;
            //request.RECIPE_DATA.RECIPE_ID = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ID;

            //Recipe Data
            //processId 가 있어야 하는데.. 흠.. 
            //request.RECIPE_DATA.ROUTE_ORDER_NO = 0; // 이건 모름
            //if (trackOutCellInfo.ProcessId.Length >= 7)
            //{
            //    request.RECIPE_DATA.EQP_TYPE = trackOutCellInfo.ProcessId.Substring(0, 3);
            //    request.RECIPE_DATA.PROCESS_TYPE = trackOutCellInfo.ProcessId.Substring(3, 3);
            //    request.RECIPE_DATA.PROCESS_NO = int.Parse(trackOutCellInfo.ProcessId.Substring(6));
            //}
            //request.RECIPE_DATA.NEXT_PROCESS_EXIST = recipeDataFromOPCUA.RECIPE_DATA.NEXT_PROCESS_EXIST;
            //request.RECIPE_DATA.RECIPE_ITEM = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ITEM;

            string EQP_TYPE = trackOutCellInfo.ProcessId.Substring(0, 3);
            string PROCESS_TYPE = trackOutCellInfo.ProcessId.Substring(3, 3);
            int PROCESS_NO = int.Parse(trackOutCellInfo.ProcessId.Substring(6));

            //PROCESSING_DATA
            request.PROCESSING_DATA = new List<_processing_data>();
            // Tray별로 ProcessingData 보냄.

            for (int i = 0; i < request.TRAY_COUNT; i++)
            {
                _processing_data TrayProcessData = new _processing_data();

                TrayProcessData.TRAY_POSITION = (i + 1).ToString();
                TrayProcessData.TRAY_ID = trayList[i];
                //TrayProcessData.ROUTE_ORDER_NO = 0; // 이건 모름
                TrayProcessData.EQP_TYPE = EqpType;
                TrayProcessData.PROCESS_TYPE = PROCESS_TYPE;
                TrayProcessData.PROCESS_NO = PROCESS_NO;
                //TrayProcessData.CELL_COUNT = i==0?trackOutCellInfo.CellCount_Level1:trackOutCellInfo.CellCount_Level2;

                TrayProcessData.PROCESS_END_TIME = dateTime;

                int startIndex = i * 30;
                TrayProcessData.CELL_COUNT = 0;
                TrayProcessData.CELL_DATA = new List<_cell_data_request>();
                for (int j = startIndex; j < startIndex + 30; j++)
                {
                    if (trackOutCellInfo._CellList[j].CellExist == false)
                        continue;
                    _cell_data_request cell_data = new _cell_data_request();
                    // CELL_EXIST
                    cell_data.CELL_EXIST = "Y";
                    // CELL_POSITION
                    cell_data.CELL_POSITION = j - startIndex + 1;
                    // CELL_ID
                    cell_data.CELL_ID = trackOutCellInfo._CellList[j].CellId;
                    // LOT_ID
                    cell_data.LOT_ID = trackOutCellInfo._CellList[j].LotId;

                    // NG_CODE, NG_TYPE, DEFECT_TYPE
                    cell_data.NG_CODE = trackOutCellInfo._CellList[j].NGCode;
                    cell_data.NG_TYPE = trackOutCellInfo._CellList[j].NGType;
                    cell_data.DEFECT_TYPE = String.Empty;  // 이건 DB에서 가져와서 조회 해야 할듯합니다.

                    cell_data.PROCESS_END_TIME = dateTime;

                    // Dictionay에 ProcessData 항목쓰기
                    cell_data.RESULT_DATA = trackOutCellInfo._CellList[j].ProcessData;

                    //이게 Cell마다 필요한가?
                    //cell_data.NEXT_PROCESS = NEXT_PROCESS;

                    TrayProcessData.CELL_DATA.Add(cell_data);
                }

                request.PROCESSING_DATA.Add(TrayProcessData);
            }

            request.NEXT_PROCESS = NEXT_PROCESS;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_PROCESS_END);
            _jsonTrayProcessEndResponse result = JsonConvert.DeserializeObject<_jsonTrayProcessEndResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;
        }
        public static _jsonTrayProcessEndResponse CallEcsApiCHGTrayProcessEnd(List<string> trayList,
            _CHGTrackOutCellInformation trackOutCellInfo, string EqpType, string EqpID, string UnitID, _next_process NEXT_PROCESS)
        {
            //throw new NotImplementedException();
            _jsonTrayProcessEndRequest request = new _jsonTrayProcessEndRequest();

            request.ACTION_ID = "TRAY_PROCESS_END";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            if (trayList != null && trayList.Count > 0)
            {
                request.TRAY_LIST.TRAY_ID_1 = trayList[0];
                request.TRAY_COUNT = 1;
                if (trayList.Count > 1)
                {
                    request.TRAY_LIST.TRAY_ID_2 = trayList[1];
                    request.TRAY_COUNT = 2;
                }
            }
            request.PROCESS_END_TIME = dateTime;
            //request.ROUTE_ID = recipeDataFromOPCUA.ROUTE_ID;
            //request.RECIPE_DATA.RECIPE_ID = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ID;

            //Recipe Data
            //processId 가 있어야 하는데.. 흠.. 
            //request.RECIPE_DATA.ROUTE_ORDER_NO = 0; // 이건 모름
            //if (trackOutCellInfo.ProcessId.Length >= 7)
            //{
            //    request.RECIPE_DATA.EQP_TYPE = trackOutCellInfo.ProcessId.Substring(0, 3);
            //    request.RECIPE_DATA.PROCESS_TYPE = trackOutCellInfo.ProcessId.Substring(3, 3);
            //    request.RECIPE_DATA.PROCESS_NO = int.Parse(trackOutCellInfo.ProcessId.Substring(6));
            //}
            //request.RECIPE_DATA.NEXT_PROCESS_EXIST = recipeDataFromOPCUA.RECIPE_DATA.NEXT_PROCESS_EXIST;
            //request.RECIPE_DATA.RECIPE_ITEM = recipeDataFromOPCUA.RECIPE_DATA.RECIPE_ITEM;

            string EQP_TYPE = trackOutCellInfo.ProcessId.Substring(0, 3);
            string PROCESS_TYPE = trackOutCellInfo.ProcessId.Substring(3, 3);
            int PROCESS_NO = int.Parse(trackOutCellInfo.ProcessId.Substring(6));

            //PROCESSING_DATA
            request.PROCESSING_DATA = new List<_processing_data>();
            // Tray별로 ProcessingData 보냄.

            for (int i = 0; i < request.TRAY_COUNT; i++)
            {
                _processing_data TrayProcessData = new _processing_data();

                TrayProcessData.TRAY_POSITION = (i + 1).ToString();
                TrayProcessData.TRAY_ID = trayList[i];
                //TrayProcessData.ROUTE_ORDER_NO = 0; // 이건 모름
                TrayProcessData.EQP_TYPE = EQP_TYPE;
                TrayProcessData.PROCESS_TYPE = PROCESS_TYPE;
                TrayProcessData.PROCESS_NO = PROCESS_NO;
                //TrayProcessData.CELL_COUNT = i==0?trackOutCellInfo.CellCount_Level1:trackOutCellInfo.CellCount_Level2;

                TrayProcessData.PROCESS_END_TIME = dateTime;

                int startIndex = i * 30;
                TrayProcessData.CELL_COUNT = 0;
                TrayProcessData.CELL_DATA = new List<_cell_data_request>();
                for (int j = startIndex; j < startIndex + 30; j++)
                {
                    if (trackOutCellInfo._CellList[j].CellExist == false)
                        continue;
                    _cell_data_request cell_data = new _cell_data_request();
                    // CELL_EXIST
                    cell_data.CELL_EXIST = "Y";
                    // CELL_POSITION
                    cell_data.CELL_POSITION = j - startIndex + 1;
                    // CELL_ID
                    cell_data.CELL_ID = trackOutCellInfo._CellList[j].CellId;
                    // LOT_ID
                    cell_data.LOT_ID = trackOutCellInfo._CellList[j].LotId;

                    // NG_CODE, NG_TYPE, DEFECT_TYPE
                    cell_data.NG_CODE = trackOutCellInfo._CellList[j].NGCode;
                    cell_data.NG_TYPE = trackOutCellInfo._CellList[j].NGType;
                    cell_data.DEFECT_TYPE = String.Empty;  // 이건 DB에서 가져와서 조회 해야 할듯합니다.

                    cell_data.PROCESS_END_TIME = dateTime;

                    // Dictionay에 ProcessData 항목쓰기
                    cell_data.RESULT_DATA = trackOutCellInfo._CellList[j].ProcessData;

                    //20230201 KJY - StartTemp, EndTemp, AvgTemp
                    if (cell_data.RESULT_DATA != null)
                    {
                        cell_data.RESULT_DATA.Add("StartTemp_PD", j < 30 ? (float)trackOutCellInfo.StartTemp_Level1 : (float)trackOutCellInfo.StartTemp_Level2);
                        cell_data.RESULT_DATA.Add("EndTemp_PD", j < 30 ? (float)trackOutCellInfo.EndTemp_Level1 : (float)trackOutCellInfo.EndTemp_Level2);
                        cell_data.RESULT_DATA.Add("AvgTemp_PD", j < 30 ? (float)trackOutCellInfo.AvgTemp_Level1 : (float)trackOutCellInfo.AvgTemp_Level2);
                    }


                    TrayProcessData.CELL_DATA.Add(cell_data);

                    // 이게 Cell별로 들어가야 하는게 맞는가?
                    //cell_data.NEXT_PROCESS = NEXT_PROCESS;
                }

                request.PROCESSING_DATA.Add(TrayProcessData);
            }

            request.NEXT_PROCESS = NEXT_PROCESS;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_PROCESS_END);
            _jsonTrayProcessEndResponse result = JsonConvert.DeserializeObject<_jsonTrayProcessEndResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;
        }
        public static _jsonSetTrayEmptyResponse CallEcsApiSetTrayEmpty(string trayId)
        {
            _jsonSetTrayEmptyRequest request = new _jsonSetTrayEmptyRequest();

            request.ACTION_ID = "SET_TRAY_EMPTY";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.TRAY_ID = trayId;
            //request.TRAY_ZONE = tray_zone;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.SET_TRAY_EMPTY);
            _jsonSetTrayEmptyResponse result = JsonConvert.DeserializeObject<_jsonSetTrayEmptyResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;

        }

        public static _jsonCreateEmptyTrayResponse CallEcsApiCreateEmptyTray(string trayId, string tray_zone)
        {
            _jsonCreateEmptyTrayRequest request = new _jsonCreateEmptyTrayRequest();

            request.ACTION_ID = "CREATE_EMPTY_TRAY";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;

            request.TRAY_ID = trayId;
            request.TRAY_ZONE = tray_zone;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.CREATE_EMPTY_TRAY);
            _jsonCreateEmptyTrayResponse result = JsonConvert.DeserializeObject<_jsonCreateEmptyTrayResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;

        }

        public static _jsonTrayLoadRequestResponse CallEcsApiTrayLoadRequest(string EqpType, string EqpID, string UnitID)
        {
            _jsonTrayLoadRequestRequest request = new _jsonTrayLoadRequestRequest();
            request.ACTION_ID = "TRAY_LOAD_REQUEST";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_LOAD_REQUEST);
            _jsonTrayLoadRequestResponse result = JsonConvert.DeserializeObject<_jsonTrayLoadRequestResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;

        }

        public static _jsonTrayUnloadRequestResponse CallEcsApiTrayUnloadRequest(string EqpType, string EqpID, string UnitID)
        {
            _jsonTrayUnloadRequestRequest request = new _jsonTrayUnloadRequestRequest();
            request.ACTION_ID = "TRAY_UNLOAD_REQUEST";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_UNLOAD_REQUEST);
            _jsonTrayUnloadRequestResponse result = JsonConvert.DeserializeObject<_jsonTrayUnloadRequestResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            return result;

        }

        public static _jsonEqpStatusResponse CallEcsApiEqpStatus(string EqpType, string EqpID, string UnitID, 
            string MODE, string STATUS, string TEMPERATURE, string TRAY_COUNT, TrayLevelInfo TRAY_LIST)
        {

            _jsonEqpStatusRequest request = new _jsonEqpStatusRequest();
            request.ACTION_ID = "EQP_STATUS";
            request.ACTION_USER = "ECS";
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID = UnitID;

            request.MODE = MODE;
            request.STATUS = STATUS;
            request.TEMPERATURE = TEMPERATURE;
            request.TRAY_COUNT = TRAY_COUNT;
            request.TRAY_LIST = TRAY_LIST;

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.EQP_STATUS);
            _jsonEqpStatusResponse result = JsonConvert.DeserializeObject<_jsonEqpStatusResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }

        public static _jsonEqpTroubleResponse CallEcsApiEqpTrouble(string EqpType, string EqpID, string UnitID, string TroubleCode, string TroubleRemark)
        {
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            _jsonEqpTroubleRequest request = new _jsonEqpTroubleRequest();
            request.ACTION_ID = "EQP_TROUBLE";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;
            request.EQP_TYPE= EqpType;
            request.EQP_ID = EqpID;
            request.UNIT_ID= UnitID;
            request.TROUBLE_CODE= TroubleCode;
            request.TROUBLE_REMARK= TroubleRemark;

            RESTClient restClient = new RESTClient();
             string jsonResult = restClient.CallEcsApi(request, CRestModulePath.EQP_TROUBLE);
            _jsonEqpTroubleResponse result = JsonConvert.DeserializeObject<_jsonEqpTroubleResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }

        public static _jsonMasterNextProcessResponse CallEcsApiMasterNextProcess(string EqpType, string EqpId, string UnitId, string ProductModel, string RouteId, string ProcessType, int ProcessNo)
        {
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";


            _jsonMasterNextProcessRequest request = new _jsonMasterNextProcessRequest();
            request.ACTION_ID = "MASTER_NEXT_PROCESS";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.MODEL_ID = ProductModel;
            request.ROUTE_ID = RouteId;
            request.EQP_TYPE = EqpType;
            request.PROCESS_TYPE = ProcessType;
            request.PROCESS_NO = ProcessNo.ToString();

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.MASTER_NEXT_PROCESS);
            _jsonMasterNextProcessResponse result = JsonConvert.DeserializeObject<_jsonMasterNextProcessResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }

        public static _jsonEcsApiSetTrayInformationResponse CallEcsApiSetTrayInformation(_jsonEcsApiSetTrayInformationRequest request, string UnitID)
        {
            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.SET_TRAY_INFORMATION, UnitID);

            if (jsonResult == null)
                return null;

            _jsonEcsApiSetTrayInformationResponse result = JsonConvert.DeserializeObject<_jsonEcsApiSetTrayInformationResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonEcsApiSetTrayInformationResponse CallEcsApiSetTrayInformationNGS(_SorterTrayCellInformation PlaceTrayCellInfo, _SorterTrayCellInformation PickTrayCellInfo, NGSCellWork CellWork, string UnitID)
        {
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            int FromIndex = CellWork.FromCellPosition - 1;
            // PickLocation의 공 Tray에 최초로 첫번째 Cell이 들어갈때 SET_TRAY_INFORMATION 해준다.

            _jsonEcsApiSetTrayInformationRequest request = new _jsonEcsApiSetTrayInformationRequest();
            request.ACTION_ID = "SET_TRAY_INFORMATION";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.MODEL_ID = PickTrayCellInfo.ProductModel;
            request.ROUTE_ID = PickTrayCellInfo.RouteId;// TODO 이건 Rework용 RouteID로 mapping되어야 한다.
            request.LOT_ID = PickTrayCellInfo.LotId;
            request.NEXT_ROUTE_ORDER_NO = 0;
            request.NEXT_EQP_TYPE = ""; // TODO 어디서 받아와야 하나... DefectType에 맞는 RouteID, 다음공정 정보를 받아와야 한다.
            request.NEXT_PROCESS_TYPE = "";
            request.NEXT_PROCESS_NO = 0;
            request.NEXT_EQP_ID = String.Empty;
            request.NEXT_UNIT_ID = String.Empty;
            request.CELL_COUNT = 1;

            request.CELL_LIST = new List<Cell_Basic_Info>();
            Cell_Basic_Info cell = new Cell_Basic_Info();
            cell.CELL_POSITION = CellWork.ToCellPosition; // from 1 to 30
            cell.CELL_EXIST = "Y";
            cell.CELL_ID = PickTrayCellInfo._CellList[FromIndex].CellId;
            cell.LOT_ID = PickTrayCellInfo._CellList[FromIndex].LotId;
            request.CELL_LIST.Add(cell);

            RESTClient restClient = new RESTClient();
            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.SET_TRAY_INFORMATION);
            _jsonEcsApiSetTrayInformationResponse result = JsonConvert.DeserializeObject<_jsonEcsApiSetTrayInformationResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }


        public static _jsonEcsApiCreateTrayInformationResponse CallEcsApiCreateTrayInformation(_jsonEcsApiCreateTrayInformationRequest request, string UnitID)
        {
            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.CREATE_TRAY_INFORMATION, UnitID);

            if (jsonResult == null)
                return null;

            _jsonEcsApiCreateTrayInformationResponse result = JsonConvert.DeserializeObject<_jsonEcsApiCreateTrayInformationResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonEcsApiSetTrayInformationResponse CallEcsApiSetTrayInformation(string EqpType, string EqpId, string UnitId, String EmptyTrayId, string TrayZone,
            _CellBasicInformation CellInformation, string ProductModel, string RouteId, string LotId, _jsonMasterNextProcessResponse NextProcessInfo)
        {
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            _jsonEcsApiSetTrayInformationRequest request = new _jsonEcsApiSetTrayInformationRequest();
            request.ACTION_ID = "SET_TRAY_INFORMATION";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.TRAY_ID = EmptyTrayId;
            request.TRAY_ZONE = TrayZone;
            request.MODEL_ID = ProductModel;
            request.ROUTE_ID = RouteId;
            request.NEXT_ROUTE_ORDER_NO = NextProcessInfo.NEXT_PROCESS.NEXT_ROUTE_ORDER_NO;
            request.NEXT_EQP_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE;
            request.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE;
            request.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO;
            request.NEXT_EQP_ID = String.Empty;
            request.NEXT_UNIT_ID = String.Empty;
            request.CELL_COUNT = CellInformation.CellCount;

            request.LOT_ID = LotId;

            request.CELL_LIST = new List<Cell_Basic_Info>();
            for (int i=0; i<30; i++)
            {
                if(CellInformation._CellList[i].CellExist == true)
                {
                    Cell_Basic_Info cell = new Cell_Basic_Info();
                    cell.CELL_POSITION = i + 1;
                    cell.CELL_EXIST = "Y";
                    cell.CELL_ID = CellInformation._CellList[i].CellId;
                    cell.LOT_ID = CellInformation._CellList[i].LotId;
                    request.CELL_LIST.Add(cell);
                }
            }


            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.SET_TRAY_INFORMATION, UnitId);

            if (jsonResult == null)
                return null;

            _jsonEcsApiSetTrayInformationResponse result = JsonConvert.DeserializeObject<_jsonEcsApiSetTrayInformationResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }
        public static _jsonEcsApiSetTrayInformationResponse CallEcsApiSetTrayInformation(string EqpType, string EqpId, string UnitId, String EmptyTrayId, string TrayZone,
            _CellInformation CellInformation, string ProductModel, string RouteId, string LotId, _next_process NextProcessInfo)
        {
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            _jsonEcsApiSetTrayInformationRequest request = new _jsonEcsApiSetTrayInformationRequest();
            request.ACTION_ID = "SET_TRAY_INFORMATION";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.TRAY_ID = EmptyTrayId;
            request.TRAY_ZONE = TrayZone;
            request.MODEL_ID = ProductModel;
            request.ROUTE_ID = RouteId;
            request.NEXT_ROUTE_ORDER_NO = NextProcessInfo.NEXT_ROUTE_ORDER_NO;
            request.NEXT_EQP_TYPE = NextProcessInfo.NEXT_EQP_TYPE;
            request.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS_TYPE;
            request.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS_NO;
            request.NEXT_EQP_ID = NextProcessInfo.NEXT_EQP_ID;
            request.NEXT_UNIT_ID = NextProcessInfo.NEXT_UNIT_ID;
            request.CELL_COUNT = CellInformation.CellCount;

            request.LOT_ID = LotId;

            request.CELL_LIST = new List<Cell_Basic_Info>();
            for (int i = 0; i < 30; i++)
            {
                if (CellInformation._CellList[i].CellExist == true)
                {
                    Cell_Basic_Info cell = new Cell_Basic_Info();
                    cell.CELL_POSITION = i + 1;
                    cell.CELL_EXIST = "Y";
                    cell.CELL_ID = CellInformation._CellList[i].CellId;
                    cell.LOT_ID = CellInformation._CellList[i].LotId;
                    request.CELL_LIST.Add(cell);
                }
            }


            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.SET_TRAY_INFORMATION, UnitId);

            if (jsonResult == null)
                return null;

            _jsonEcsApiSetTrayInformationResponse result = JsonConvert.DeserializeObject<_jsonEcsApiSetTrayInformationResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public static _jsonTrayCellInputResponse CallEcsApiTrayCellInput(_OutPutCell Cell, int CellCount, string TrayId, string EqpId, string UnitId)
        {
            _jsonTrayCellInputRequest request = new _jsonTrayCellInputRequest();
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            request.ACTION_ID = "TRAY_CELL_INPUT";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.EQP_ID = EqpId;
            request.UNIT_ID = UnitId;
            request.TRAY_ID = TrayId;

            request.INPUT_CELL_COUNT = CellCount;
            request.INPUT_CELL_LIST = new List<_OutPutCell>();
            request.INPUT_CELL_LIST.Add(Cell);

            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_CELL_INPUT);

            if (jsonResult == null)
                return null;

            _jsonTrayCellInputResponse result = JsonConvert.DeserializeObject<_jsonTrayCellInputResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }

        public static _jsonCellPackingResponse CallEcsApiCellPacking(string PALLET_ID, string MODEL_ID, string CELL_GRADE, UInt16 PACKING_CELL_COUNT, List<PackingCellInfo> PACKING_CELL_LIST)
        {
            _jsonCellPackingRequest request = new _jsonCellPackingRequest();
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            request.ACTION_ID = "CELL_PACKING";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.PALLET_ID = PALLET_ID;
            request.PACKING_TIME = dateTime;
            request.MODEL_ID = MODEL_ID;
            request.CELL_GRADE = CELL_GRADE;
            request.PACKING_CELL_COUNT = PACKING_CELL_COUNT;
            request.PACKING_CELL_LIST = PACKING_CELL_LIST;

            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.CELL_PACKING);

            if (jsonResult == null)
                return null;

            _jsonCellPackingResponse result = JsonConvert.DeserializeObject<_jsonCellPackingResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }

        public static _jsonTrayArrivedResponse CallEcsApiTrayArrived(string EqpType, string EqpId, string UnitId, int TrayCount, List<string> TrayList)
        {
            _jsonTrayArrivedRequest request = new _jsonTrayArrivedRequest();
            string dateTime = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            request.ACTION_ID = "TRAY_ARRIVED";
            request.ACTION_USER = "ECS";
            request.REQUEST_TIME = dateTime;

            request.EQP_TYPE = EqpType;
            request.EQP_ID = EqpId;
            request.UNIT_ID = UnitId;
            request.TRAY_COUNT = TrayCount;

            request.TRAY_LIST = new TrayLevelInfo();
            request.TRAY_LIST.TRAY_ID_1 = TrayList[0];
            if(TrayList.Count > 1)
            {
                request.TRAY_LIST.TRAY_ID_2 = TrayList[1];
            }

            RESTClient restClient = new RESTClient();

            string jsonResult = restClient.CallEcsApi(request, CRestModulePath.TRAY_ARRIVED);

            if (jsonResult == null)
                return null;

            _jsonTrayArrivedResponse result = JsonConvert.DeserializeObject<_jsonTrayArrivedResponse>(jsonResult, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;

        }
    }
}
