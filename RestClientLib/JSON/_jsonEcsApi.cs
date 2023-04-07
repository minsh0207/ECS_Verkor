using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    //Request
    /*
    ACTION_ID
    REQUEST_TIME
    TRAY_ID
    MODEL_ID
    ROUTE_ID
    NEXT_ROUTE_ORDER_NO
    NEXT_EQP_TYPE
    NEXT_PROCESS_TYPE
    NEXT_PROCESS_NO
    NEXT_EQP_ID
    NEXT_UNIT_ID
    CELL_COUNT
    CELL_LIST
        CELL_POSITION
        CELL_EXIST
        CELL_ID
        LOT_ID
    */

    // SetTrayInformation

    public class _jsonEcsApiSetTrayInformationRequest : __baseRequest
    {
        public string TRAY_ID;
        public string MODEL_ID;
        public string ROUTE_ID;
        public string TRAY_ZONE;
        public string LOT_ID;
        public int NEXT_ROUTE_ORDER_NO;
        public string NEXT_EQP_TYPE;
        public string NEXT_PROCESS_TYPE;
        
        public int NEXT_PROCESS_NO;
        public string NEXT_EQP_ID;
        public string NEXT_UNIT_ID;
        public int CELL_COUNT;
        public List<Cell_Basic_Info> CELL_LIST;
    }

    public class Cell_Basic_Info
    {
        public int CELL_POSITION;
        public string CELL_EXIST;
        public string CELL_ID;
        public string LOT_ID;
    }



    //Response
    public class _jsonEcsApiSetTrayInformationResponse : _baseResponse
    {

    }
    public class _jsonEcsApiCreateTrayInformationRequest : _jsonEcsApiSetTrayInformationRequest
    {
    }
    public class _jsonEcsApiCreateTrayInformationResponse : _jsonEcsApiSetTrayInformationResponse
    {

    }

    // masterRecipe
    public class _jsonEcsApiMasterRecipeRequest : __baseRequest
    {

        public string MODEL_ID;
        public string ROUTE_ID;
        public string EQP_TYPE;
        public string PROCESS_TYPE;
        public int PROCESS_NO;
    }
    public class _jsonEcsApiMasterRecipeResponse : _baseResponse
    {
        public string MODEL_ID;
        public string ROUTE_ID;
        //public string RECIPE_ID;

        //public List<RecipeBasicInfo> RECIPE_DATA;
        public RecipeBasicInfo RECIPE_DATA;
    }

    public class RecipeBasicInfo
    {
        //20221227 위에서 RECIPE_ID내려옴
        public string RECIPE_ID;

        public int PROCESS_ORDER_NO;
        public string EQP_TYPE;
        public string PROCESS_TYPE;
        public int PROCESS_NO;

        public string NEXT_PROCESS_EXIST; // Y or N
        public string OPERATION_MODE;

        //public Dictionary<string, object> RECIPE_ITEM;
        public List<RecipeItem> RECIPE_ITEM;
    }

    public class RecipeItem
    {
        public string NAME;
        public object VALUE;
        public string UNIT;
    }



    // processStart
    public class _jsonEcsApiTrayProcessStartRequest : __baseRequest
    {
        public string EQP_TYPE;
        public string EQP_ID;
        public string UNIT_ID;
        public string RETRY_FLAG; // Y or N , default: "" 재시작 command가 오면 Y로 해주면 되겠다. 그 이외에는 N
        public int TRAY_COUNT;
        public TrayLevelInfo TRAY_LIST;
        //public string ROUTE_ID; //20230302 제거
        //public string RECIPE_ID;
        public RecipeBasicInfo RECIPE_DATA;

        public string PROCESS_START_TIME;

        //public string MODEL_ID; //20230302 제거

    }

    public class TrayLevelInfo
    {
        public string TRAY_ID_1;
        public string TRAY_ID_2;
    }

    public class ProcessingData
    {
        public string TRAY_POSITION; // 1:level1, 2: level2, default : 1
        public string TRAY_ID;

        public string EQP_TYPE;
        public string PROCESS_TYPE;
        public int PROCESS_NO;

        public string PROCESS_START_TIME;
        public int CELL_COUNT;

        public List<CellBasicInfo> CELL_DATA;
    }

    public class CellBasicInfo
    {
        public int CELL_POSITION;
        public string CELL_EXIST; //Y or N
        public string CELL_ID;
        public string LOT_ID;

        public string PROCESS_START_TIME;
    }

    public class _jsonEcsApiTrayProcessStartResponse : _baseResponse
    {

    }


    // trayProcessEnd

    /// <summary>
    /// JSON Format Body : trayProcessEnd Request
    /// </summary>
    public class _jsonTrayProcessEndRequest : __baseRequest
    {
        public _jsonTrayProcessEndRequest() { }
        ~_jsonTrayProcessEndRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }

        //public string MODEL_ID { get; set; }

        //public string RETRY_FLAG { get; set; }

        public int TRAY_COUNT { get; set; }
        public _tray_list TRAY_LIST = new _tray_list();

        //public string PROCESS_START_TIME { get; set; }
        public string PROCESS_END_TIME { get; set; }

        //public string ROUTE_ID { get; set; }
        //public string RECIPE_ID { get; set; }

        //public _recipe_data RECIPE_DATA = new _recipe_data();

        public List<_processing_data> PROCESSING_DATA;

        public _next_process NEXT_PROCESS;
    }


    /// <summary>
    /// JSON Format Body : trayProcessEnd Response
    /// </summary>
    public class _jsonTrayProcessEndResponse : _baseResponse
    {
        public _jsonTrayProcessEndResponse() { }
        ~_jsonTrayProcessEndResponse() { }

        //[Newtonsoft.Json.JsonProperty(Order = 1)]
        //public _next_process NEXT_PROCESS = new _next_process();
    }

    /// <summary>
    /// JSON Format Body : trayLoadRequest Request
    /// </summary>
    public class _jsonTrayLoadRequestRequest : __baseRequest
    {
        public _jsonTrayLoadRequestRequest() { }
        ~_jsonTrayLoadRequestRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }

    }


    /// <summary>
    /// JSON Format Body : trayLoadRequest Response
    /// </summary>
    public class _jsonTrayLoadRequestResponse : _baseResponse
    {
        public _jsonTrayLoadRequestResponse() { }
        ~_jsonTrayLoadRequestResponse() { }
    }


    /// <summary>
    /// JSON Format Body : trayUnloadRequest Request
    /// </summary>
    public class _jsonTrayUnloadRequestRequest : __baseRequest
    {
        public _jsonTrayUnloadRequestRequest() { }
        ~_jsonTrayUnloadRequestRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }

    }


    /// <summary>
    /// JSON Format Body : trayUnloadRequest Response
    /// </summary>
    public class _jsonTrayUnloadRequestResponse : _baseResponse
    {
        public _jsonTrayUnloadRequestResponse() { }
        ~_jsonTrayUnloadRequestResponse() { }
    }


    //eqpStatus
    /// <summary>
    /// JSON Format Body : eqpStatus Request
    /// </summary>
    public class _jsonEqpStatusRequest : __baseRequest
    {
        public _jsonEqpStatusRequest() { }
        ~_jsonEqpStatusRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }

        //Mode
        public string MODE { get; set; }
        //Status
        public string STATUS { get; set; }
        //Temperature
        public string TEMPERATURE   { get; set; }
        //TrayCount
        public string TRAY_COUNT { get; set; }
        //TrayList
        public TrayLevelInfo TRAY_LIST;

    }


    /// <summary>
    /// JSON Format Body : eqpStatus Response
    /// </summary>
    public class _jsonEqpStatusResponse : _baseResponse
    {
        public _jsonEqpStatusResponse() { }
        ~_jsonEqpStatusResponse() { }
    }

    // eqpTrouble
    /// <summary>
    /// JSON Format Body : eqpTrouble Request
    /// </summary>
    public class _jsonEqpTroubleRequest : __baseRequest
    {
        public _jsonEqpTroubleRequest() { }
        ~_jsonEqpTroubleRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }

         public string TROUBLE_CODE { get; set; }
         public string TROUBLE_REMARK { get; set; }


    }


    /// <summary>
    /// JSON Format Body : eqpTrouble Response
    /// </summary>
    public class _jsonEqpTroubleResponse : _baseResponse
    {
        public _jsonEqpTroubleResponse() { }
        ~_jsonEqpTroubleResponse() { }
    }

    public class _jsonTrayCellOutputRequest : __baseRequest
    {
        public _jsonTrayCellOutputRequest() { }
        ~_jsonTrayCellOutputRequest() { }
        public string TRAY_ID;
        public string EQP_ID;
        public string UNIT_ID;

        public int OUTPUT_CELL_COUNT;
        public List<_OutPutCell> OUTPUT_CELL_LIST;

    }
    public class _OutPutCell
    {
        public int CELL_POSITION;
        public string CELL_ID;
        public string LOT_ID;
    }

    public class _jsonTrayCellOutputResponse : _baseResponse
    {
        public _jsonTrayCellOutputResponse() { }
        ~_jsonTrayCellOutputResponse() { }

        public string TRAY_ID;
        public int CELL_COUNT;

        public List<_BasicCell> CELL_LIST;
    }
    public class _BasicCell
    {
        public int CELL_POSITION;
        public string CELL_EXIST; // Y or N
        public string CELL_ID;
        public string LOT_ID;
    }

    public class _jsonSetTrayEmptyRequest : __baseRequest
    {
        public _jsonSetTrayEmptyRequest() { }
        ~_jsonSetTrayEmptyRequest() { }

        public string TRAY_ID;
        
    }

    public class _jsonSetTrayEmptyResponse : _baseResponse
    {
        public _jsonSetTrayEmptyResponse() { }
        ~_jsonSetTrayEmptyResponse() { }
    }

    public class _jsonCreateEmptyTrayRequest : __baseRequest
    {
        public _jsonCreateEmptyTrayRequest() { }
        ~_jsonCreateEmptyTrayRequest() { }

        public string TRAY_ID;
        public string TRAY_ZONE;
    }

    public class _jsonCreateEmptyTrayResponse : _baseResponse
    {
        public _jsonCreateEmptyTrayResponse() { }
        ~_jsonCreateEmptyTrayResponse() { }
    }




    public class _jsonMasterNextProcessRequest : __baseRequest
    {
        public _jsonMasterNextProcessRequest() { }
        ~_jsonMasterNextProcessRequest() { }

        public string MODEL_ID { get; set; }
        public string ROUTE_ID { get; set; }
        public string EQP_TYPE { get; set; }

        public string PROCESS_TYPE { get; set; }
        public string PROCESS_NO { get; set; }


    }


    /// <summary>
    /// JSON Format Body : eqpTrouble Response
    /// </summary>
    public class _jsonMasterNextProcessResponse : _baseResponse
    {
        public _jsonMasterNextProcessResponse() { }
        ~_jsonMasterNextProcessResponse() { }

        public _NextProcess NEXT_PROCESS;


    }
    public class _NextProcess
    {
        public int NEXT_ROUTE_ORDER_NO; // 이거 있는건가 없는건가...
        public string NEXT_EQP_TYPE;
        public string NEXT_PROCESS_TYPE;
        public int NEXT_PROCESS_NO;

    }

    public class _jsonCellProcessEndRequest : __baseRequest
    {
        public _jsonCellProcessEndRequest() { }
        ~_jsonCellProcessEndRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }
        public string CELL_ID { get; set; }
        //public string LOT_ID { get; set; }
        public string PROCESS_END_TIME { get; set; }
        //public string MODEL_ID { get; set; }
        //public string ROUTE_ID { get; set; }
        // 20230301 제외 LotId/Model/RouteId 세개

        //public RecipeBasicInfo RECIPE_DATA; // 20230301 제외
        public CellProcEndProcessingData PROCESSING_DATA;
    }

    public class CellProcEndProcessingData
    {
        //public string PROCESS_END_TIME;
        // 20230301 제외
        public string NG_CODE;
        public string NG_TYPE;
        public string DEFECT_TYPE; // DefectType은 설비가 주는 값이 아닌데.... 

        public Dictionary<string, object> RESULT_DATA;
    }

    public class _jsonCellProcessEndResponse : _baseResponse
    {
        public _jsonCellProcessEndResponse() { }
        ~_jsonCellProcessEndResponse() { }

    }

    //cellPakcing
    public class _jsonCellPackingRequest : __baseRequest
    {
        public _jsonCellPackingRequest() { }
        ~_jsonCellPackingRequest() { }
        public string PALLET_ID;
        public string PACKING_TIME;
        public string MODEL_ID;
        public string CELL_GRADE;
        public int PACKING_CELL_COUNT;

        public List<PackingCellInfo> PACKING_CELL_LIST;
    }
    public class PackingCellInfo
    {
        public int INDEX;           // CellNo 1~150
        public int TRAY_POSITION; // Floor 1~25
        public int CELL_POSITION; // Position 1~6
        public string CELL_ID;
        public string LOT_ID;
    }

    public class _jsonCellPackingResponse : _baseResponse
    {
        public _jsonCellPackingResponse() { }
        ~_jsonCellPackingResponse() { }
    }


    //ecs/trayArrived
    public class _jsonTrayArrivedRequest : __baseRequest
    {
        public _jsonTrayArrivedRequest() { }
        ~_jsonTrayArrivedRequest() { }

        public string EQP_TYPE { get; set; }
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }
        public int TRAY_COUNT { get; set; }

        public TrayLevelInfo TRAY_LIST;
    }
    public class _jsonTrayArrivedResponse : _baseResponse
    {
        public _jsonTrayArrivedResponse() { }
        ~_jsonTrayArrivedResponse() { }
    }

    public class _jsonTrayCellInputRequest : __baseRequest
    {
        public string EQP_ID { get; set; }
        public string UNIT_ID { get; set; }
        public string TRAY_ID { get; set; }
        public int INPUT_CELL_COUNT { get; set; }

        public List<_OutPutCell> INPUT_CELL_LIST;

    }

    public class _jsonTrayCellInputResponse : _baseResponse
    {
        public _jsonTrayCellInputResponse() { }
        ~_jsonTrayCellInputResponse() { }
    }
}
