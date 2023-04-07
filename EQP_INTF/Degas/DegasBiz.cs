using RestClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.Degas
{
    public partial class Degas : UserControl
    {
        private void UpdateAive(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            SetGroupRadio(Common_Alive_Radio, eventItem.LastValue.Value != null ? (Boolean)eventItem.LastValue.Value : false);

            if ((Boolean)eventItem.LastValue.Value == true)
            {
                // false로 reset
                // Alive 로직은 별도로 떼어 낼지 여부 고민중.. timer로 걸어서 별도로 처리하는것이 맞는지.
                //if (EQPClient.WriteNodeByPath("Common.Alive", (Boolean)false) == false)
                //{
                //    _LOG_("[EQPClient] Fail to update Common.Alive : false", ECSLogger.LOG_LEVEL.ERROR);
                //    return;
                //}

            }
        }
        /// <summary>
        /// 개별공정장비와 Apro장비와 Mode 코드가 다름에 주의
        /// </summary>
        /// <param name="value"></param>
        private void SetEqpModeRadio(UInt16 value)
        {

            if (value == 1 || value == 2)
                SetGroupRadio(Mode_Radio, value-1);
            else if (value == 4)
                SetGroupRadio(Mode_Radio, 2);
            else
                SetGroupRadio(Mode_Radio, 1); // Default는 Manual?
        }
        private void InitDisplayWithCurrentValue(OPCUAClient.OPCUAClient OPCClient)
        {
            List<string> ReadPath = new List<string>();

            ReadPath.Add("Common.Alive");
            ReadPath.Add("EquipmentStatus.Power");
            ReadPath.Add("EquipmentStatus.Mode");
            ReadPath.Add("EquipmentStatus.Status");
            ReadPath.Add("EquipmentStatus.Trouble.ErrorNo");
            ReadPath.Add("EquipmentStatus.Trouble.ErrorLevel");
            ReadPath.Add("FmsStatus.Trouble.Status");
            ReadPath.Add("FmsStatus.Trouble.ErrorNo");
            ReadPath.Add("EquipmentControl.Command");
            ReadPath.Add("EquipmentControl.CommandResponse");
            ReadPath.Add("PickLocation.TrayInformation.TrayExist");
            ReadPath.Add("PickLocation.TrayInformation.TrayId");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoad");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoadResponse");
            ReadPath.Add("PickLocation.TrayInformation.ProductModel");
            ReadPath.Add("PickLocation.TrayInformation.RouteId");
            ReadPath.Add("PickLocation.TrayInformation.ProcessId");
            ReadPath.Add("PickLocation.TrayInformation.LotId");
            ReadPath.Add("PickLocation.CellInformation.CellCount");
            ReadPath.Add("PickLocation.TrayProcess.ProcessStart");
            ReadPath.Add("PickLocation.TrayProcess.ProcessStartResponse");
            ReadPath.Add("PickLocation.TrayProcess.ProcessEnd");
            ReadPath.Add("PickLocation.TrayProcess.ProcessEndResponse");
            ReadPath.Add("PickLocation.TrayProcess.RequestRecipe");
            ReadPath.Add("PickLocation.TrayProcess.RequestRecipeResponse");
            ReadPath.Add("PickLocation.Recipe.RecipeId");
            ReadPath.Add("PickLocation.CellTrackIn.CellNo");
            ReadPath.Add("PickLocation.CellTrackIn.CellId");
            ReadPath.Add("PickLocation.CellTrackIn.CellLoadComplete");
            ReadPath.Add("PickLocation.CellTrackIn.CellLoadCompleteResponse");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayExist");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayId");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayLoad");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayLoadResponse");
            ReadPath.Add("PlaceLocation.TrayProcess.ProcessEnd");
            ReadPath.Add("PlaceLocation.TrayProcess.ProcessEndResponse");
            ReadPath.Add("PlaceLocation.TrayProcess.TrayOut");
            ReadPath.Add("PlaceLocation.CellInformation.CellCount");
            ReadPath.Add("PlaceLocation.CellTrackOut.CellNo");
            ReadPath.Add("PlaceLocation.CellTrackOut.CellId");
            ReadPath.Add("PlaceLocation.CellTrackOut.CellUnloadComplete");
            ReadPath.Add("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse");
            ReadPath.Add("ManualLocation.CellManualOut.CellId");
            ReadPath.Add("ManualLocation.CellManualOut.LotId");
            ReadPath.Add("ManualLocation.CellManualOut.NGCode");
            ReadPath.Add("ManualLocation.CellManualOut.NGType");
            ReadPath.Add("ManualLocation.CellManualOut.CellManualOutRequest");
            ReadPath.Add("ManualLocation.CellManualOut.CellManualOutRequestResponse");

            List<BrowseNode> browseNodeList;
            List<ReadValueId> readValueIdList;
            List<DataValue> nodesReadValue = OPCClient.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

            if (nodesReadValue == null || nodesReadValue.Count < 1)
                return;

            for (int i = 0; i < nodesReadValue.Count; i++)
            {
                if (browseNodeList[i].browsePath.EndsWith("Common.Alive"))
                    SetGroupRadio(Common_Alive_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Power"))
                    SetGroupRadio(Power_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Mode"))
                    SetEqpModeRadio((UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Status"))
                    SetTextBox(Status_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorNo"))
                    SetTextBox(EQP_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorLevel"))
                    SetGroupRadio(EQP_ErrorLevel_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    SetGroupRadio(FMS_Status_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    SetTextBox(FMS_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.Command"))
                    SetTextBox(Command_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    SetGroupRadio(CommandResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.TrayExist"))
                    SetGroupRadio(Pick_TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.TrayId"))
                    SetTextBox(Pick_TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.TrayLoad"))
                    SetGroupRadio(Pick_TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(Pick_TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.ProductModel"))
                    SetTextBox(Pick_Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.RouteId"))
                    SetTextBox(Pick_RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.ProcessId"))
                    SetTextBox(Pick_ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.LotId"))
                    SetTextBox(Pick_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellInformation.CellCount"))
                    SetTextBox(Pick_CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.ProcessStart"))
                    SetGroupRadio(Pick_ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.ProcessStartResponse"))
                    SetGroupRadio(Pick_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.ProcessEnd"))
                    SetGroupRadio(Pick_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(Pick_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.RequestRecipe"))
                    SetGroupRadio(Pick_RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayProcess.RequestRecipeResponse"))
                    SetGroupRadio(Pick_RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.Recipe.RecipeId"))
                    SetTextBox(Pick_RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellTrackIn.CellNo"))
                    SetTextBox(CellIn_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellTrackIn.CellId"))
                    SetTextBox(CellIn_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellTrackIn.CellLoadComplete"))
                    SetGroupRadio(CellLoadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellTrackIn.CellLoadCompleteResponse"))
                    SetGroupRadio(CellLoadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayExist"))
                    SetGroupRadio(Place_TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayId"))
                    SetTextBox(Place_TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoad"))
                    SetGroupRadio(Place_TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(Place_TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayProcess.ProcessEnd"))
                    SetGroupRadio(Place_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(Place_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayProcess.TrayOut"))
                    SetGroupRadio(Place_TrayOut_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    SetTextBox(Place_CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellTrackOut.CellNo"))
                    SetTextBox(CellOut_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellTrackOut.CellId"))
                    SetTextBox(CellOut_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellTrackOut.CellUnloadComplete"))
                    SetGroupRadio(CellUnloadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse"))
                    SetGroupRadio(CellUnloadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.CellId"))
                    SetTextBox(Manual_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.LotId"))
                    SetTextBox(Manual_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.NGCode"))
                    SetTextBox(Manual_NGCode_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.NGType"))
                    SetTextBox(Manual_NGType_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.CellManualOutRequest"))
                    SetGroupRadio(CellManualOutRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.CellManualOutRequestResponse"))
                    SetGroupRadio(CellManualOutRequestResponse_Radio, (Boolean)nodesReadValue[i].Value);
            }

            //dataGridView
            // TrackInCellInformation 의 Cell 정보 
            _CellBasicInformation TrackInCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("PickLocation.CellInformation");
            DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, TrackInCellList_DataGridView);

            // TrackOutCellInformation의 Cell 정보
            _CellBasicInformation TrackOutCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("PlaceLocation.CellInformation");
            DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_DataGridView);
        }
        

        private void FMSClient_ProcessDataChanged(object data, StatusCode error)
        {
            try
            {
                DataChangedEventArgs ChangeEventArgs = data as DataChangedEventArgs;
                for (int i = 0; i < ChangeEventArgs.DataChanges.Count; i++)
                {
                    MonitoredItem item = ChangeEventArgs.DataChanges[i].MonitoredItem;
                    //BrowseNode node = FMSClient.m_browseNodeList.FindTargetNodeByNodeId(item.NodeId);
                    // 20230406 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"FMSClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("Location1.TrayProcess.TrayLoadResponse"))
                    {
                        FMSSequencePickTrayLoadResponse(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    {
                        FMSSequencePickRequestRecipeResponse(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequencePickProcessStartResponse(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequencePickProcessEndResponse(item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.TrayLoadResponse"))
                    {
                        FMSSequencePlaceTrayLoadResponse(item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequencePlaceProcessEndResponse(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePlaceTrayLoadResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;
                // 20230330 msh : ProcessStartResponse 전에 Off신호를 받는 경우 MesResponseWaitItem값이 Null처림되어 Error발생.
                //MesResponseWaitItem = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bTrayUnloadResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayUnloadResponse)
                    {
                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePlaceTrayLoadResponseTimeOut()
        {
            try
            {
                // EQP로 response
                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePlaceProcessEndResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bProcessEndResponse = (Boolean)eventItem.LastValue.Value;

                    if (bProcessEndResponse)
                    {
                        //parameter에 object가 8개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 8)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        _jsonDatCellResponse FirstCellInfo = (_jsonDatCellResponse)MesResponseWaitItem.parameters[0];
                        string TrayId = (string)MesResponseWaitItem.parameters[1];                        
                        _CellInformation AllCellProcessData = (_CellInformation)MesResponseWaitItem.parameters[2];
                        _jsonDatTrayResponse TrayData = (_jsonDatTrayResponse)MesResponseWaitItem.parameters[3];
                        string EQPType = (string)MesResponseWaitItem.parameters[4];
                        string EQPID = (string)MesResponseWaitItem.parameters[5];
                        string UNITID = (string)MesResponseWaitItem.parameters[6];
                        _jsonMasterNextProcessResponse NextProcessInfo = (_jsonMasterNextProcessResponse)MesResponseWaitItem.parameters[7];

                        // FMS에서 다음공정 정보 읽어와야 함.
                        _next_process NEXT_PROCESS = FMSClient.ReadNextProcess("Location2.TrayProcess.NextDestination");
                        if (NEXT_PROCESS == null || NEXT_PROCESS.NEXT_EQP_TYPE.Length <1 || NEXT_PROCESS.NEXT_PROCESS_TYPE.Length <1 || NEXT_PROCESS.NEXT_PROCESS_NO <1) 
                        {
                            if (NEXT_PROCESS == null) NEXT_PROCESS = new _next_process();
                            NEXT_PROCESS.NEXT_MANUAL_SET_FLAG = "N";
                            NEXT_PROCESS.NEXT_EQP_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE;
                            NEXT_PROCESS.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE;
                            NEXT_PROCESS.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO;
                            NEXT_PROCESS.NEXT_EQP_ID = "";
                            NEXT_PROCESS.NEXT_UNIT_ID = "";
                        } 

                        // 여기서 TrayBinding 한다.
                        _jsonEcsApiSetTrayInformationResponse TrayBindingResponse = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayData.DATA[0].TRAY_ZONE,
                            AllCellProcessData, FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].LOT_ID, NEXT_PROCESS);
                        if (TrayBindingResponse == null || TrayBindingResponse.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NEXT_PROCESS.NEXT_EQP_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_NO}:{NEXT_PROCESS.NEXT_EQP_ID}:{NEXT_PROCESS.NEXT_UNIT_ID}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NEXT_PROCESS.NEXT_EQP_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_NO}:{NEXT_PROCESS.NEXT_EQP_ID}:{NEXT_PROCESS.NEXT_UNIT_ID}]");
                        //  Tray/Cell Binding완료

                        //05. MES not-available이면 ProcessEndResponse EQP로 전달함.
                        // EQPClient - ProcessEndResponse
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayProcess.ProcessEndResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePlaceProcessEndResponseTimeOut()
        {
            try
            {
                //parameter에 object가 8개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 8)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }

                _jsonDatCellResponse FirstCellInfo = (_jsonDatCellResponse)MesResponseWaitItem.parameters[0];
                string TrayId = (string)MesResponseWaitItem.parameters[1];
                _CellInformation AllCellProcessData = (_CellInformation)MesResponseWaitItem.parameters[2];
                _jsonDatTrayResponse TrayData = (_jsonDatTrayResponse)MesResponseWaitItem.parameters[3];
                string EQPType = (string)MesResponseWaitItem.parameters[4];
                string EQPID = (string)MesResponseWaitItem.parameters[5];
                string UNITID = (string)MesResponseWaitItem.parameters[6];
                _jsonMasterNextProcessResponse NextProcessInfo = (_jsonMasterNextProcessResponse)MesResponseWaitItem.parameters[7];

                _next_process NEXT_PROCESS = new _next_process();
                NEXT_PROCESS.NEXT_MANUAL_SET_FLAG = "N";
                NEXT_PROCESS.NEXT_EQP_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE;
                NEXT_PROCESS.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE;
                NEXT_PROCESS.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO;
                NEXT_PROCESS.NEXT_EQP_ID = "";
                NEXT_PROCESS.NEXT_UNIT_ID = "";

                // 여기서 TrayBinding 한다.
                _jsonEcsApiSetTrayInformationResponse TrayBindingResponse = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayData.DATA[0].TRAY_ZONE,
                    AllCellProcessData, FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].LOT_ID, NEXT_PROCESS);
                if (TrayBindingResponse == null || TrayBindingResponse.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NEXT_PROCESS.NEXT_EQP_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_NO}:{NEXT_PROCESS.NEXT_EQP_ID}:{NEXT_PROCESS.NEXT_UNIT_ID}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NEXT_PROCESS.NEXT_EQP_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NEXT_PROCESS.NEXT_PROCESS_NO}:{NEXT_PROCESS.NEXT_EQP_ID}:{NEXT_PROCESS.NEXT_UNIT_ID}]");
                //  Tray/Cell Binding완료

                // EQPClient - ProcessEndResponse
                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayProcess.ProcessEndResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickProcessEndResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bTrayLoadCompleteResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayLoadCompleteResponse)
                    {

                        // parameter에 object가 1개 있어야함.
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 1)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string TrayId = (string)MesResponseWaitItem.parameters[0];
                        // 공Tray처리
                        _jsonSetTrayEmptyResponse response = RESTClientBiz.CallEcsApiSetTrayEmpty(TrayId);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.SET_TRAY_EMPTY.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }
                        _LOG_($"Success to call REST API - SET_TRAY_EMPTY [{TrayId}]");

                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessEndResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.TrayProcess.ProcessEndResponse:true]");
                    	
                    	MesResponseWaitItem = null;
                    }
                }
                
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickProcessEndResponseTimeOut()
        {
            try
            {
                // parameter에 object가 1개 있어야함.
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 1)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                string TrayId = (string)MesResponseWaitItem.parameters[0];

                //공 Tray처리
                _jsonSetTrayEmptyResponse response = RESTClientBiz.CallEcsApiSetTrayEmpty(TrayId);
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.SET_TRAY_EMPTY.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    //TODO : Trouble 처리
                    return;
                }
                _LOG_($"Success to call REST API - SET_TRAY_EMPTY [{TrayId}]");

                // EQP 에 ProcessEndResponse true로
                if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.ProcessEndResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.TrayInformation.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.TrayInformation.ProcessEndResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePickProcessStartResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessStartResponse = (Boolean)eventItem.LastValue.Value;

                    if (bProcessStartResponse)
                    {
                        //parameter에 object가 3개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 3)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = (_jsonEcsApiMasterRecipeResponse)MesResponseWaitItem.parameters[0];
                        string UNITID = (string)MesResponseWaitItem.parameters[1];
                        string TrayId = (string)MesResponseWaitItem.parameters[2];

                        //01. Rest API 호출해서 데이터처리
                        //http://<server_name>/ecs/processStart
                        _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayId);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_PROCESS_START");

                        //EQPClient의 ProcessStartResponse 켜준다.
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessStartResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [PickLocation.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.TrayProcess.ProcessStartResponse:true]");
                    
                        MesResponseWaitItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickProcessStartResponseTimeOut()
        {
            try
            {
                //parameter에 object가 3개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 3)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = (_jsonEcsApiMasterRecipeResponse)MesResponseWaitItem.parameters[0];
                string UNITID = (string)MesResponseWaitItem.parameters[1];
                string TrayId = (string)MesResponseWaitItem.parameters[2];

                //01. Rest API 호출해서 데이터처리
                //http://<server_name>/ecs/processStart
                _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayId);

                // 성공여부
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_START");

                // ProcessStartResponse
                if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessStartResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [PickLocation.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.TrayProcess.ProcessStartResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }
        private void FMSSequencePickRequestRecipeResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bRequestRecipeResponse = (Boolean)eventItem.LastValue.Value;

                    if (bRequestRecipeResponse)
                    {
                        // FMS OPCUA Server로 부터 Recipe 정보를 읽어서 EQP로 write
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromMES = FMSClient.ReadRecipeMES("Location1.Recipe");
                        if (RecipeDataFromMES == null)
                        {
                            _LOG_("[FMSClient] Fail to read Recipe [Location1.Recipe]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to read Recipe from MES [Location1.Recipe]");

                        // MES not-available이면 MaterRecipe를 설비에 Write
                        if (EQPClient.WriteRecipeEQP("PickLocation.Recipe", RecipeDataFromMES, OPCUAClient.MappingDirection.FmsToEqp) == false)
                        {
                            _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeDataFromMES.RECIPE_DATA.RECIPE_ID}]");

                        // EQP의 RequestRecipeResponse : true
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.RequestRecipeResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write RequestRecipeResponse [PickLocation.TrayProcess.RequestRecipeResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write RequestRecipeResponse [PickLocation.TrayProcess.RequestRecipeResponse:true]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }
        private void FMSSequencePickRequestRecipeResponseTimeOut()
        {
            try
            {
                //parameter에 object가 5개 있어야함.
                string model_id, route_id, targetEQPType, targetProcessType, targetProcessNo;
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 5)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }

                model_id = (string)MesResponseWaitItem.parameters[0];
                route_id = (string)MesResponseWaitItem.parameters[1];
                targetEQPType = (string)MesResponseWaitItem.parameters[2];
                targetProcessType = (string)MesResponseWaitItem.parameters[3];
                targetProcessNo = (string)MesResponseWaitItem.parameters[4];

                // 01. REST API 호출해서 Master Recipe 받아옴.
                _jsonEcsApiMasterRecipeResponse RecipeResponse = RESTClientBiz.CallEcsApiMasterRecipe(model_id, route_id, targetEQPType, targetProcessType, targetProcessNo);
                if (RecipeResponse == null)
                {
                    _LOG_($"Fail to get Master Recipe by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"Scuccess to Get Recipy by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}");

                // MES not-available이면 MaterRecipe를 설비에 Write
                if (EQPClient.WriteRecipeEQP("PickLocation.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                {
                    _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                //04. RequestRecipeResponse : ON
                if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.RequestRecipeResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [PickLocation.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [PickLocation.TrayProcess.RequestRecipeResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePickTrayLoadResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;
                // 20230330 msh : ProcessStartResponse 전에 Off신호를 받는 경우 MesResponseWaitItem값이 Null처림되어 Error발생.
                //MesResponseWaitItem = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bTrayLoadResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayLoadResponse)
                    {
                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse [PickLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [PickLocation.TrayInformation.TrayLoadResponse:1]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickTrayLoadResponseTimeOut()
        {
            try
            {
                // MES 없으면 바로 EQP로 response 준다.
                if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoadResponse [PickLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayLoadResponse [PickLocation.TrayInformation.TrayLoadResponse:1]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void EQPClient_ProcessDataChanged(object data, StatusCode error)
        {
            try
            {
                DataChangedEventArgs ChangeEventArgs = data as DataChangedEventArgs;
                for (int i = 0; i < ChangeEventArgs.DataChanges.Count; i++)
                {
                    MonitoredItem item = ChangeEventArgs.DataChanges[i].MonitoredItem;
                    //BrowseNode node = EQPClient.m_browseNodeList.FindTargetNodeByNodeId(item.NodeId);
                    // 20230406 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"EQPClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    //여기서 Event별로 분기함
                    if (browsePath.EndsWith("Common.Alive"))
                    {
                        UpdateAive(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Power"))
                    {
                        UpdateRadioButton(Power_Radio, item);
                        // EQP 상태 정보 저장 로직
                        // eqp_mode에 powerOFF 만 저장함.. on되면 control / maintaenance mode 중 하나가 됨
                        SetEQPPower(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Mode"))
                    {
                        DataMonitoredItem eventItem = item as DataMonitoredItem;
                        SetEqpModeRadio((UInt16)eventItem.LastValue.Value);
                        // EQP Mode 정보 저장 로직
                        SetEQPMode(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Status"))
                    {
                        UpdateTextBox(Status_TextBox, item);
                        // EQP Sttus 정보 저장 로직
                        SetEqpStatus(item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Trouble.ErrorNo"))
                    {
                        UpdateTextBox(EQP_ErrorNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("EquipmentStatus.Trouble.ErrorLevel"))
                    {
                        UpdateRadioButton(EQP_ErrorLevel_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    {
                        UpdateRadioButton(FMS_Status_Radio, item);
                    }
                    else if (browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    {
                        UpdateTextBox(FMS_ErrorNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("EquipmentControl.Command"))
                    {
                        UpdateTextBox(Command_TextBox, item);
                    }
                    else if (browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    {
                        UpdateRadioButton(CommandResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.TrayExist"))
                    {
                        UpdateRadioButton(Pick_TrayExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.TrayId"))
                    {
                        UpdateTextBox(Pick_TrayId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(Pick_TrayLoad_Radio, item);
                        // Tray와 Cell정보를 조회해서 EQP, FMS 모두 써준다.
                        if (EqpAvailable()) SequencePickTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(Pick_TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.ProductModel"))
                    {
                        UpdateTextBox(Pick_Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.RouteId"))
                    {
                        UpdateTextBox(Pick_RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.ProcessId"))
                    {
                        UpdateTextBox(Pick_ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.LotId"))
                    {
                        UpdateTextBox(Pick_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellInformation.CellCount"))
                    {
                        UpdateTextBox(Pick_CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.ProcessStart"))
                    {
                        UpdateRadioButton(Pick_ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePickProcessStart(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(Pick_ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePickProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(Pick_RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePickRequestRecipe(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.TrayProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(Pick_RequestRecipeResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.Recipe.RecipeId"))
                    {
                        UpdateTextBox(Pick_RecipeId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellTrackIn.CellNo"))
                    {
                        UpdateTextBox(CellIn_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellTrackIn.CellId"))
                    {
                        UpdateTextBox(CellIn_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellTrackIn.CellLoadComplete"))
                    {
                        UpdateRadioButton(CellLoadComplete_Radio, item);
                        //Logic
                        if(EqpAvailable()) SequencePickCellLoadComplete(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellTrackIn.CellLoadCompleteResponse"))
                    {
                        UpdateRadioButton(CellLoadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayExist"))
                    {
                        UpdateRadioButton(Place_TrayExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayId"))
                    {
                        UpdateTextBox(Place_TrayId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(Place_TrayLoad_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePlaceTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(Place_TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(Place_ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePlaceProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(Place_ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayProcess.TrayOut"))
                    {
                        UpdateRadioButton(Place_TrayOut_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    {
                        UpdateTextBox(Place_CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellTrackOut.CellNo"))
                    {
                        UpdateTextBox(CellOut_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellTrackOut.CellId"))
                    {
                        UpdateTextBox(CellOut_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellTrackOut.CellUnloadComplete"))
                    {
                        UpdateRadioButton(CellUnloadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePlaceCellUnloadComplete(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse"))
                    {
                        UpdateRadioButton(CellUnloadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.CellId"))
                    {
                        UpdateTextBox(Manual_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.LotId"))
                    {
                        UpdateTextBox(Manual_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.NGCode"))
                    {
                        UpdateTextBox(Manual_NGCode_TextBox, item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.NGType"))
                    {
                        UpdateTextBox(Manual_NGType_TextBox, item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.CellManualOutRequest"))
                    {
                        UpdateRadioButton(CellManualOutRequest_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceCellManualOutRequest(item);
                    }
                    else if (browsePath.EndsWith("ManualLocation.CellManualOut.CellManualOutRequestResponse"))
                    {
                        UpdateRadioButton(CellManualOutRequestResponse_Radio, item);
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickTrayLoad(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayLoad = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoad)
                    {

                        string TrayId = string.Empty;
                        _jsonDatTrayResponse TrayData;
                        _jsonDatCellResponse TrayCellData;
                        UInt16 CellCount = 0;
                        _LOG_("Start SequencePickTrayLoad:ON");

                        //TrayId를 EQP OPCUA에서 읽는다.
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PickLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PickLocation.TrayInformation.TrayId]");

                        if (TrayId != null && TrayId.Length > 0)
                        {
                            //20230309 KJY - REST API - TRAY_ARRIVED 호출해야함
                            List<string> TrayIds = new List<string>();
                            TrayIds.Add(TrayId);
                            _jsonTrayArrivedResponse TrayArrivedResponse = RESTClientBiz.CallEcsApiTrayArrived(EQPType, EQPID, UNITID, 1, TrayIds);
                            if (TrayArrivedResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to CallEcsApiTrayArrived. {EQPType}:{EQPID}:{UNITID}:{TrayIds.Count}:{TrayId}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Success to CallEcsApiTrayArrived. {EQPType}:{EQPID}:{UNITID}:{TrayIds.Count}:{TrayId}");


                            // Tray/Cell 정보 DB에서 받아온다. REST
                            TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId, EQPID);
                            TrayCellData = RESTClientBiz.GetRestCellInfoByTrayId(TrayId, EQPID);
                            CellCount = (UInt16)TrayCellData.DATA.Count;

                            if (TrayData != null && TrayData.DATA.Count > 0)
                            {
                                // 이 Tray가 올바른 Tray인지 여부에 대한 Check 필요함
                                if (CheckTrayValidation(TrayData.DATA[0], TrayCellData) == false)
                                {
                                    // 들어와서는 안된는 Tray가 들어왔다.
                                    return;
                                }
                                if (EQPClient.WriteBasicTrayInfoEQP("PickLocation.TrayInformation", TrayData.DATA[0]) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [PickLocation.TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Success to write TrayInfo [PickLocation.TrayInformation] on EQP, {TrayId}");

                                if (EQPClient.WriteBasicCellInfoEQP("PickLocation.CellInformation.Cell", TrayCellData.DATA, 0) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [TrackInCellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                // CellCount도 쓰자
                                if (EQPClient.WriteNodeByPath("PickLocation.CellInformation.CellCount", (UInt16)TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write CellCount [PickLocation.CellInformation.CellCount:{TrayCellData.DATA.Count}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write Cell Information to [PickLocation.CellInformation.Cell] : {TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.DATA.Count}");

                                // FMS에 Write
                                if (FMSClient.WriteBasicTrayInfoFMS("Location1.TrayInformation", "Location1.TrayInformation.CellCount", TrayData.DATA[0], TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write Tray Info [Location1.TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Tray Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");
                                if (FMSClient.WriteBasicCellInfoFMS("Location1.TrayInformation.CellInformation", TrayCellData.DATA) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [Location1.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                                // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                                _CellBasicInformation TrackInCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation("PickLocation.CellInformation");
                                DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, TrackInCellList_DataGridView);

                            }
                            else
                            {
                                // Tray 정보가 DB에 없는 상황
                                _LOG_($"No Tray Information in Database [{TrayId}]", ECSLogger.LOG_LEVEL.ERROR);
                                // TODO : Trouble 처리 해야함
                                return;
                            }

                            if (MesAvailable())
                            {
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : True]");

                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse("Location1.TrayProcess.TrayLoadResponse");

                            }
                            else
                            {
                                // MES 없으면 바로 EQP로 response 준다.
                                if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadResponse ON EQPClient [PickLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [PickLocation.TrayInformation.TrayLoadResponse : 1]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : True]");
                            }


                        }
                        else
                        {
                            // EQP OPCUA에 TrayId가 없는 상황
                            _LOG_("[EQPClient] No TrayId on EQPClient [PickLocation.TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }



                    }
                    else // TrayLoad : Off
                    {
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.TrayLoadResponse", (UInt16)0) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse ON EQPClient [PickLocation.TrayInformation.TrayLoadResponse:0]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Complete to write [PickLocation.TrayInformation.TrayLoadResponse : 0]");

                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad;false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private bool CheckTrayValidation(_dat_tray Tray, _jsonDatCellResponse CellList)
        {
            // Cell Tray가 맞는지
            if (Tray.TRAY_STATUS == "E")
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] is Empty", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            else if (Tray.TRAY_STATUS == "D")
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] is deleted", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            // 다음공정이 여기 맞는지.
            if (Tray.NEXT_EQP_TYPE != this.EQPType)
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] NEXT_EQP_TYPE[{Tray.NEXT_EQP_TYPE}] is NOT [{this.EQPType}]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            // Cell 갯수 한개 이상
            if (CellList.DATA.Count < 1)
            {
                _LOG_($"Cell count in Tray [{Tray.TRAY_ID}] shoule be more than one", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            // 이전공정 종료 정보가 없으면?
            // 이건 고민좀 해보자.. 이건 확인 안해도 될것 같음. 그냥 새로 추가하는 식으로 처리

            return true;
        }
        private void SequenceCellManualOutRequest(MonitoredItem item)
        {
            // Cell 수동 배출
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bManualOutRequest = (Boolean)eventItem.LastValue.Value;
                    if (bManualOutRequest)
                    {
                        //ecs/cellProcessEnd 이거호출하는것으로 해보자. => 불필요함
                        //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("PickLocation.TrayInformation", "PickLocation.Recipe");
                        
                        string CellId;
                        if (EQPClient.ReadValueByPath("ManualLocation.CellManualOut.CellId", out CellId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [ManualLocation.CellManualOut.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellManualOut  CellId[{CellId}]");

                        _CellProcessData CellProcessData = EQPClient.ReadCellManualOutProcessDataEQP("ManualLocation.CellManualOut");
                        if(CellProcessData == null)
                        {
                            _LOG_("[EQPClient] Fail to read ReadOneProcessDataEQP(ManualLocation.CellManualOut.ResultData)", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellManualOut ProcessData [{CellId}]");


                        _jsonCellProcessEndResponse response = RESTClientBiz.CallEcsApiCellProcessEnd(CellProcessData, EQPType, EQPID, UNITID);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.CELL_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - CELL_PROCESS_END");



                        if (EQPClient.WriteNodeByPath("ManualLocation.CellManualOut.CellManualOutRequestResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellManualOutRequestResponse on EQP [ManualLocation.CellManualOut.CellManualOutRequestResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Response for CellManualOutRequest to EQP Sucess [ManualLocation.CellManualOut.CellManualOutRequestResponse:true]");

                    }
                    else
                    {
                        // reset
                        if (EQPClient.WriteNodeByPath("ManualLocation.CellManualOut.CellManualOutRequestResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellManualOutRequestResponse on EQP [ManualLocation.CellManualOut.CellManualOutRequestResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Clear for CellManualOutRequestResponse to EQP Sucess [ManualLocation.CellManualOut.CellManualOutRequestResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }

        }

        private void SequencePlaceProcessEnd(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessEnd = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEnd)
                    {
                        // Cell Tray 생성하고 공정종료 처리.. TrayOut도 고려해야 한다.
                        // 이미 Cell별 공정종료가 되었기때문에 Cell Tray만 만들어주면된다. 하지만, ProcessData는 MES로 전달해야 함.
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("PlaceLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PlaceLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PlaceLocation.TrayInformation.TrayId]");

                        // Tray가 공 Tray여야 한다.
                        _jsonDatTrayResponse TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId, EQPID);
                        if(TrayData.DATA.Count <1 || TrayData.DATA[0].TRAY_STATUS != "E")
                        {
                            _LOG_($"Tray is not existed in FMS or Tray is NOT Empty [{TrayId}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        // FMS Server Tray의 LotId, Model, RouteId, processId는 PlaceLocation.CellInformation.Cell에 있는 첫번째 Cell의 정보를 사용한다.
                        // 전체 Cell의 ProcessData를 읽는다. ProcessId는 별도로 알아보자.
                        _CellInformation AllCellProcessData = EQPClient.ReadProcessDataEQP("PlaceLocation.CellInformation");
                        if(AllCellProcessData == null)
                        {
                            _LOG_("[EQPClient] Fail to read All Cell ProcessData [PlaceLocation.CellInformation]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to read All Cell ProcessData [PlaceLocation.CellInformation]");
                        if (AllCellProcessData.CellCount <= 0)
                        {
                            _LOG_("[EQPClient] Fail to read PlaceLocation.CellInformation, CellCount is 0", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        // 첫번째 Cell의 CellID로 REST 호출해서 정보를 받아온다.
                        string firstCellId = string.Empty;
                        for (int i = 0; i < 30; i++)
                        {
                            if (AllCellProcessData._CellList[i].CellExist == true)
                            {
                                firstCellId = AllCellProcessData._CellList[i].CellId;
                                break;
                            }
                        }
                        if (firstCellId.Length <= 0)
                        {
                            // Cell 정보가 하나도 없다는 얘기
                            _LOG_("No Cell Information on [PlaceLocation.CellInformation.Cell]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _jsonDatCellResponse FirstCellInfo = RESTClientBiz.GetRestCellInfoByCellId(firstCellId, EQPID);
                        if (FirstCellInfo == null || FirstCellInfo.DATA.Count < 1)
                        {
                            _LOG_($"GetRestCellInfoByCellId({firstCellId}) failed.", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        string ProcessId = FirstCellInfo.DATA[0].EQP_TYPE + FirstCellInfo.DATA[0].PROCESS_TYPE + string.Format("{0:D3}", FirstCellInfo.DATA[0].PROCESS_NO);
                        AllCellProcessData.ProcessId = ProcessId;

                        // 다음공정정보를 미리 읽어와야 한다.
                        // 2.8	masterNextProcess
                        _jsonMasterNextProcessResponse NextProcessInfo = RESTClientBiz.CallEcsApiMasterNextProcess(EQPType, EQPID, UNITID,
                            FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].PROCESS_TYPE, FirstCellInfo.DATA[0].PROCESS_NO);
                        if (NextProcessInfo == null || NextProcessInfo.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call CallEcsApiMasterNextProcess : [{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}:{EQPType}:{FirstCellInfo.DATA[0].PROCESS_TYPE}:{FirstCellInfo.DATA[0].PROCESS_NO}]",
                                ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        _LOG_($"Success to call CallEcsApiMasterNextProcess : [{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");

                        //FMS에 Location2.TrayInformation에  Tray 정보 write
                        // 현재 TrayData는 공Tray정보를 가지고 있다.
                        TrayData.DATA[0].MODEL_ID = FirstCellInfo.DATA[0].MODEL_ID;
                        TrayData.DATA[0].LOT_ID = FirstCellInfo.DATA[0].LOT_ID;
                        TrayData.DATA[0].ROUTE_ID = FirstCellInfo.DATA[0].ROUTE_ID;
                        TrayData.DATA[0].EQP_TYPE = FirstCellInfo.DATA[0].EQP_TYPE;
                        TrayData.DATA[0].PROCESS_TYPE = FirstCellInfo.DATA[0].PROCESS_TYPE;
                        TrayData.DATA[0].PROCESS_NO = FirstCellInfo.DATA[0].PROCESS_NO;
                        TrayData.DATA[0].LOT_ID = FirstCellInfo.DATA[0].LOT_ID;
                        // FMS에 Write, ProcessId도 써야 하니깐.. WriteBasicTrayInfoFMS()의 마지막 parameter는 true로
                        if (FMSClient.WriteBasicTrayInfoFMS("Location2.TrayInformation", "Location2.TrayInformation.CellCount", TrayData.DATA[0], AllCellProcessData.CellCount, true) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Tray Info [Location2.TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write Tray Information : [{TrayData.DATA[0].TRAY_ID}: CellCount:{AllCellProcessData.CellCount}]");

                        //20230406 sgh tag 경로 수정
                        //if (FMSClient.WriteBasicCellInfoFMS("Location2.TrayInformation.CellInformation.Cell", AllCellProcessData) == false)
                        if (FMSClient.WriteBasicCellInfoFMS("Location2.TrayInformation.CellInformation", AllCellProcessData) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [Location2.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{AllCellProcessData.CellCount}]");
                        //FMS에 ProcessData도 write
                        if (FMSClient.WriteProcessDataFMS(AllCellProcessData, "Location2.ProcessData") == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Process Data [Location2.ProcessData]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_("[FMSClient] Success to write Process Data [Location2.ProcessData]");
                        // 여기까지 했으면 기본 준비 끝

                        //20230328 sgh - TrackOutCellInformation의 Cell 정보 갱신 추가 
                        _CellBasicInformation TrackOutCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation("PlaceLocation.CellInformation");
                        DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_DataGridView);

                        if (MesAvailable())
                        {
                            //04-1 ProcessEnd를 MES로 보내고 대기
                            // FMSClient에 ProcessEnd
                            if (FMSClient.WriteNodeByPath("Location2.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location2.TrayProcess.ProcessEnd:true]");

                            // 20230224 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            parameters.Add(FirstCellInfo);
                            parameters.Add(TrayId);
                            parameters.Add(AllCellProcessData);
                            parameters.Add(TrayData);
                            parameters.Add(EQPType);
                            parameters.Add(EQPID);
                            parameters.Add(UNITID);
                            parameters.Add(NextProcessInfo);

                            WaitMesResponse("Location2.TrayProcess.ProcessEndResponse", parameters);
                        }
                        else
                        {
                            _next_process NEXT_PROCESS = new _next_process();
                            NEXT_PROCESS.NEXT_MANUAL_SET_FLAG = "N";
                            NEXT_PROCESS.NEXT_EQP_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE;
                            NEXT_PROCESS.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE;
                            NEXT_PROCESS.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO;
                            NEXT_PROCESS.NEXT_EQP_ID = "";
                            NEXT_PROCESS.NEXT_UNIT_ID = "";

                            // 여기서 TrayBinding 한다.
                            _jsonEcsApiSetTrayInformationResponse TrayBindingResponse = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayData.DATA[0].TRAY_ZONE,
                                AllCellProcessData, FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].LOT_ID, NEXT_PROCESS);
                            if (TrayBindingResponse == null || TrayBindingResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");
                            //  Tray/Cell Binding완료

                            //05. MES not-available이면 ProcessEndResponse EQP로 전달함.
                            // EQPClient - ProcessEndResponse
                            if (EQPClient.WriteNodeByPath("PlaceLocation.TrayProcess.ProcessEndResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:true]");

                            // FMSClient에 ProcessEnd - MES 안 붙어 있어도 flow 대로..
                            if (FMSClient.WriteNodeByPath("Location2.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location2.TrayProcess.ProcessStart:true]");
                        }
                        
                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath("Location2.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.TrayProcess.ProcessEnd]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [Location2.TrayProcess.ProcessEnd:false]");
                        // EQP - ProcessStartResponse : off

                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayProcess.ProcessEndResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PlaceLocation.TrayProcess.ProcessEndResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePlaceCellUnloadComplete(MonitoredItem item)
        {
            // Tray에 담은 Cell의 CellNo (0~29) , CellId 로 ECS로 검증요청
            // 여기서 Cell별 공정종료을 주어야 할지.. Cell을 모두 뽑은후에 Tray단위로 공정시작 종료를 해야 할지 고민중. 
            // 20230301 Cell별로 공정종료 처리하고... PlaceLocation.TrayProcess.ProcessEnd에서는 binding 만 하는 것으로 처리
            
            try
            {
                // 일단 PickLocation.CellInformation.Cell._xx 와 비교해서 데이터가 동일한지 확인한다.
                // 추후 상황에 따라서 Cell별 공정시작 처리를 해주도록한다. (이렇게 되면 ProcessStart에 공정시작처리를 하지 않고 여기서 개별 Cell 공정시작 처리를 해야 함)
                // Cell별 공정종료처리함.
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bCellUnloadComplete = (Boolean)eventItem.LastValue.Value;
                    if (bCellUnloadComplete)
                    {
                        // 요청한 CellId와 CellNo
                        UInt16 ReqCellNo;
                        String ReqCellId;
                        if (EQPClient.ReadValueByPath("PlaceLocation.CellTrackOut.CellNo", out ReqCellNo) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PlaceLocation.CellTrackOut.CellNo]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PlaceLocation.CellTrackOut.CellId", out ReqCellId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PlaceLocation.CellTrackOut.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackOut CellNo[{ReqCellNo}], CellId[{ReqCellId}]");

                        //Cell의 ProcessData를 읽음.
                        string CellProcessDataPath = $"PlaceLocation.CellInformation.Cell._{ReqCellNo}";
                        _CellProcessData CellProcessData = EQPClient.ReadOneProcessDataEQP(CellProcessDataPath);
                        if (CellProcessData == null)
                        {
                            _LOG_("[EQPClient] Fail to read ReadOneProcessDataEQP(ManualLocation.CellManualOut.ResultData)", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellManualOut ProcessData [_{ReqCellNo}.CellId : {CellProcessData.CellId}]");

                        if(ReqCellId != CellProcessData.CellId)
                        {
                            _LOG_($"[EQPClient] Difference Cell ID , TrackOut.CellId:[{ReqCellId}, {CellProcessDataPath}.CellId : {CellProcessData.CellId}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        _jsonCellProcessEndResponse response = RESTClientBiz.CallEcsApiCellProcessEnd(CellProcessData, EQPType, EQPID, UNITID);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.CELL_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - CELL_PROCESS_END");

                        // Confirm
                        if (EQPClient.WriteNodeByPath("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [PlaceLocation.CellTrackOut.CellUnloadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Response for CellUnloadComplete to EQP Sucess [PlaceLocationLocation.CellTrackOut.CellUnloadCompleteResponse:true]");
                    }
                    else
                    {
                        // reset
                        if (EQPClient.WriteNodeByPath("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [PlaceLocation.CellTrackOut.CellUnloadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Clear for CellUnloadCompleteResponse to EQP Sucess [PlaceLocation.CellTrackOut.CellUnloadCompleteResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickCellLoadComplete(MonitoredItem item)
        {
            // 뽑은 Cell의 CellNo (0~29) , CellId 로 ECS로 검증요청
            // 여기서 Cell별 공정시작을 주어야 할지.. Cell을 모두 뽑은후에 Tray단위로 공정시작 처리를 해야 할지 고민중. 
            // Cell별이 맞는것 같긴한데... 
            try
            {
                // 일단 PickLocation.CellInformation.Cell._xx 와 비교해서 데이터가 동일한지 확인한다.
                // 추후 상황에 따라서 Cell별 공정시작 처리를 해주도록한다. (이렇게 되면 ProcessStart에 공정시작처리를 하지 않고 여기서 개별 Cell 공정시작 처리를 해야 함)
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bCellLoadComplete = (Boolean)eventItem.LastValue.Value;
                    if (bCellLoadComplete)
                    {
                        // 요청한 CellId와 CellNo
                        UInt16 ReqCellNo;
                        String ReqCellId;
                        if (EQPClient.ReadValueByPath("PickLocation.CellTrackIn.CellNo", out ReqCellNo) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.CellTrackIn.CellNo]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PickLocation.CellTrackIn.CellId", out ReqCellId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.CellTrackIn.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackIn CellNo[{ReqCellNo}], CellId[{ReqCellId}]");

                        //PickLocation.CellInformation.Cell._xx 에 있는 Cell 정보
                        String CellId;
                        string CellIdPath = $"PickLocation.CellInformation.Cell._{ReqCellNo}.CellId";
                        if (EQPClient.ReadValueByPath(CellIdPath, out CellId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{CellIdPath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellId [{CellIdPath}],  CellId[{CellId}]");

                        if(ReqCellId == CellId)
                        {
                            // Confirm
                            if (EQPClient.WriteNodeByPath("PickLocation.CellTrackIn.CellLoadCompleteResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write CellLoadCompleteResponse on EQP [PickLocation.CellTrackIn.CellLoadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Response for CellLoadComplete to EQP Sucess [PickLocation.CellTrackIn.CellLoadCompleteResponse:true]");
                        }
                        else
                        {
                            //Fail
                            _LOG_($"Different CellId : Requested CellId;{ReqCellId}:{ReqCellNo}, CellInformation.CellId:{CellId}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                    }
                    else
                    {
                        if (EQPClient.WriteNodeByPath("PickLocation.CellTrackIn.CellLoadCompleteResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellLoadCompleteResponse on EQP [PickLocation.CellTrackIn.CellLoadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Response for CellLoadComplete to EQP Sucess [PickLocation.CellTrackIn.CellLoadCompleteResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickRequestRecipe(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                string model_id;
                string route_id;
                string ProcessId;
                string targetEQPType;
                string targetProcessType;
                string targetProcessNo;

                if (eventItem != null)
                {
                    Boolean bRequestRecipe = (Boolean)eventItem.LastValue.Value;
                    if (bRequestRecipe)
                    {
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.ProcessId", out ProcessId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.TrayInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.RouteId", out route_id) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.TrayInformation.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.ProductModel", out model_id) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.TrayInformation.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Start SequenceRequestRecipe , Model:{model_id}, RouteID:{route_id}, ProcessId:{ProcessId}");

                        if (ProcessId.Length < 7)
                        {
                            _LOG_($"ProcessId [{ProcessId}] is incorrect", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        targetEQPType = ProcessId.Substring(0, 3);
                        targetProcessType = ProcessId.Substring(3, 3);
                        targetProcessNo = ProcessId.Substring(6);

                        if (MesAvailable() == false)
                        {
                            // 01. REST API 호출해서 Master Recipe 받아옴.
                            _jsonEcsApiMasterRecipeResponse RecipeResponse = RESTClientBiz.CallEcsApiMasterRecipe(model_id, route_id, targetEQPType, targetProcessType, targetProcessNo);
                            if (RecipeResponse == null)
                            {
                                _LOG_($"Fail to get Master Recipe by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Scuccess to Get Recipy by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}");

                            // MES not-available이면 MaterRecipe를 설비에 Write
                            if (EQPClient.WriteRecipeEQP("PickLocation.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                            {
                                _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                            //04. RequestRecipeResponse : ON
                            if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.RequestRecipeResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [PickLocation.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [PickLocation.TrayProcess.RequestRecipeResponse:true]");

                            // FMS Server에도 그냥 써준다. flow 대로
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [Location1.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [Location1.TrayProcess.RequestRecipe:true]");

                        }
                        else
                        {   // MES가 가동중일때는 FMS 의 requestRecipe만 켜준다.
                            // FMS에 recipe 써줘야 하나?
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [Location1.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [Location1.TrayProcess.RequestRecipe:true]");

                            // 20230224 KJY - MES Timeout 세팅
                            // 전달할 parameter model_id, route_id, targetEQPType, targetProcessType, targetProcessNo
                            List<object> parameters = new List<object>();
                            parameters.Add(model_id);
                            parameters.Add(route_id);
                            parameters.Add(targetEQPType);
                            parameters.Add(targetProcessType);
                            parameters.Add(targetProcessNo);

                            WaitMesResponse("Location1.TrayProcess.RequestRecipeResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", (Boolean)false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write [Location1.TrayProcess.RequestRecipe : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.RequestRecipe : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.RequestRecipeResponse", (Boolean)false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write [PickLocation.TrayProcess.RequestRecipeResponse : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Complete to write [PickLocation.TrayProcess.RequestRecipeResponse : false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickProcessEnd(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bProcessEnd = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEnd)
                    {
                        // 모든 Cell이 설비로 투입되었음. 공Tray처리하고 내보내면 됨.
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [TrayInformation.TrayId]");

                        if (TrayId != null && TrayId.Length > 0)
                        {
                            if (MesAvailable())
                            {
                                //MES로 TrayLoadComplete 보냄.
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoadComplete [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoadComplete [Location1.TrayProcess.ProcessEnd:true]");

                                //20230224 KJY - MES Response Timer 세팅
                                List<object> parameters = new List<object>();
                                parameters.Add(TrayId);

                                WaitMesResponse("TrayProcess.TrayLoadCompleteResponse", parameters);
                            }
                            else
                            {
                                //공 Tray처리
                                _jsonSetTrayEmptyResponse response = RESTClientBiz.CallEcsApiSetTrayEmpty(TrayId);
                                if (response.RESPONSE_CODE != "200")
                                {
                                    _LOG_($"Fail to call REST API [{CRestModulePath.SET_TRAY_EMPTY.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                    //TODO : Trouble 처리
                                    return;
                                }
                                _LOG_($"Success to call REST API - SET_TRAY_EMPTY [{TrayId}]");

                                // EQP 에 TrayLoadCompleteResponse true로
                                if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessEndResponse", true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadCompleteResponse [PickLocation.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayLoadCompleteResponse [PickLocation.TrayProcess.ProcessEndResponse:true]");

                                // MES 안 붙어있어도 써준다. flow대로
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoadComplete [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoadComplete [Location1.TrayProcess.ProcessEnd:true]");
                            }
                        }
                        else
                        {
                            // EQP에 TrayId가 없음
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble처리?
                            return;
                        }

                    }
                    else
                    {
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:false]");

                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessEndResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.TrayProcess.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.TrayProcess.ProcessEndResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickProcessStart(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessStart = (Boolean)eventItem.LastValue.Value;
                    if (bProcessStart)
                    {
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("PickLocation.TrayInformation", "PickLocation.Recipe");
                        if (RecipeDataFromOPCUA == null)
                        {
                            _LOG_("[EQPClient] Fail to read Recipe Data", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PickLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PickLocation.TrayInformation.TrayId]");

                        if (MesAvailable() == false)
                        {
                            //01. Rest API 호출해서 데이터처리
                            //http://<server_name>/ecs/processStart
                            _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayId);
                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - TRAY_PROCESS_START");

                            // ProcessStartResponse
                            if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessStartResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [PickLocation.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.TrayProcess.ProcessStartResponse:true]");

                            // FMSClient에 ProcessStart (MES가 붙었든 아니든 일단 flow는 동일하게 간다.)
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe [Location1.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessStart [Location1.TrayProcess.ProcessStart:true]");
                        }
                        else
                        {
                            // FMSClient에 ProcessStart
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe [Location1.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessStart [Location1.TrayProcess.ProcessStart:true]");

                            // 20230222 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(UNITID);
                            parameters.Add(TrayId);

                            WaitMesResponse("Location1.TrayProcess.ProcessStartResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessStart [Location1.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessStart [Location1.TrayProcess.ProcessStart:false]");
                        // EQP - ProcessStartResponse : off                    
                        if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessStartResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePlaceTrayLoad(MonitoredItem item)
        {
            // 공Tray확인
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayLoad = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoad)
                    {
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("PlaceLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PlaceLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PlaceLocation.TrayInformation.TrayId]");

                        if (TrayId != null && TrayId.Length > 0)
                        {
                            _jsonDatTrayResponse TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId, EQPID);
                            if (TrayData.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API GetRestTrayInfoByTrayId({TrayId}, {EQPID}) : {TrayData.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                //TODO : Trouble 처리
                                return;
                            }
                            _LOG_($"Success to call REST API - GetRestTrayInfoByTrayId({TrayId}, {EQPID})");

                            if (TrayData.DATA[0].TRAY_STATUS != "E")
                            {
                                // 공 Tray가 아님
                                _LOG_($"Tray [{TrayId}] is not Empty Tray", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Tray [{TrayId}] is Empty Tray. Cell Move from DGS to Tray is prepared.");

                            if (MesAvailable())
                            {
                                //20230405 sgh SequencePlaceTrayLoad ON에 TrayInformation Send 추가
                                if (FMSClient.WriteBasicTrayInfoFMS("Location2.TrayInformation", String.Empty, TrayData.DATA[0], 0) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [Location2.TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }

                                //FMS로 TrayUnload 보냄
                                if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad [Location2.TrayProcess.TrayLoad:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoad [Location2.TrayProcess.TrayLoad:true]");

                                //20230224 FMS에 요청을 보냈으면 응답을 받는 Timer를 가동함.
                                // TrayUnload는 별다른 parameter 필요없음.
                                WaitMesResponse("Location2.TrayProcess.TrayLoadResponse");
                            }
                            else
                            {
                                // EQP로 Response
                                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadResponse on EQP [PlaceLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad [Location2.TrayProcess.TrayLoad:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoad [Location2.TrayProcess.TrayLoad:true]");
                            }
                        }
                        else
                        {
                            // EQP에 TrayId가 없음
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble처리?
                            return;
                        }


                    }
                    else
                    {
                        // FMS Request clear
                        if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad [Location2.TrayProcess.TrayLoad:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write TrayLoad [Location2.TrayProcess.TrayLoad:false]");

                        // EQP Response clear
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)0) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse on EQP [PlaceLocation.TrayInformation.TrayLoadResponse:0]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:0]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetEqpStatus(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    UInt16 Status = (UInt16)eventItem.LastValue.Value;
                    // 1: Idle,  2: Running, 4: Machine Trouble, 8: Pause
                    String strStatus = String.Empty;

                    switch (Status)
                    {
                        case 1:
                            strStatus = "I";
                            break;
                        case 2:
                            strStatus = "R";
                            break;
                        case 4:
                            strStatus = "T"; // Trouble - REST API - eqpTrouble 호출해야 함.
                            break;
                        case 8:
                            strStatus = "P";
                            break;
                        default:
                            strStatus = "I";
                            break;
                    }


                    // RestAPI eqpStatus 호출해야 함.
                    _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, null, strStatus, null, null, null);
                    if (response == null)
                    {
                        _LOG_($"Fail to CallEcsApiEqpStatus with STATUS='{strStatus}'", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"Success to CallEcsApiEqpStatus with STATUS='{strStatus}'");


                    // Trouble이면 eqpTrouble 호출
                    if (strStatus == "T")
                    {
                        UInt16 ErrorLevel;
                        if (EQPClient.ReadValueByPath("EquipmentStatus.Trouble.ErrorLevel", out ErrorLevel) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [EquipmentStatus.Trouble.ErrorLevel]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        UInt16 ErrorNo;
                        if (EQPClient.ReadValueByPath("EquipmentStatus.Trouble.ErrorNo", out ErrorNo) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [EquipmentStatus.Trouble.ErrorNo]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read ErrorNo:{ErrorNo}, ErrorLevel:{ErrorLevel}");

                        //eqpTrouble 호출
                        _jsonEqpTroubleResponse responseEqpTrouble = RESTClientBiz.CallEcsApiEqpTrouble(this.EQPType, this.EQPID, this.UNITID, ErrorNo.ToString(),
                            $"Machine Trouble [{EQPType}:{EQPID}:{UNITID}][{ErrorNo}:{ErrorLevel}]");
                        if (responseEqpTrouble == null)
                        {
                            _LOG_($"Fail to CallEcsApiEqpTrouble, Machine Trouble [{EQPType}:{EQPID}:{UNITID}][ErrorNO:{ErrorNo},ErrorLevel:{ErrorLevel}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Success to CallEcsApiEqpTrouble, Machine Trouble [{EQPType}:{EQPID}:{UNITID}][ErrorNO:{ErrorNo},ErrorLevel:{ErrorLevel}]");

                        // FMS OPCUA Serer에 Trouble Write
                        String strErrorLevel;
                        if (ErrorLevel == 1) strErrorLevel = "B"; // non-critical
                        else if (ErrorLevel == 2) strErrorLevel = "A"; //critical
                        else strErrorLevel = "C"; //warning

                        Dictionary<string, object> TroubleDic = new Dictionary<string, object>();
                        TroubleDic.Add("EquipmentStatus.Trouble.ErrorLevel", strErrorLevel);
                        TroubleDic.Add("EquipmentStatus.Trouble.ErrorNo", ErrorNo);
                        if (FMSClient.WriteNodeWithDic(TroubleDic) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Trouble [EquipmentStatus.Trouble] : [{ErrorNo}:{strErrorLevel}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Success to write Trouble [EquipmentStatus.Trouble] : [{ErrorNo}:{strErrorLevel}]");

                    }

                    // FMS OPCUA Serer에도 상태 변경 해줘야 하지.
                    if (FMSClient.WriteNodeByPath("EquipmentStatus.Status", Status) == false)
                    {
                        _LOG_($"[FMSClient] Fail to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[FMSClient] Success to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]");
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetEQPMode(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    UInt16 Mode = (UInt16)eventItem.LastValue.Value;
                    // 개별 공정 장비는 1, Maint. 2:Manual , 4:Contorl
                    string EqpMode = "F";
                    if (Mode == 4)
                        EqpMode = "C";
                    // RestAPI eqpStatus 호출해야 함.
                    _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, EqpMode, null, null, null, null);
                    if (response == null)
                    {
                        _LOG_($"Fail to CallEcsApiEqpStatus with Mode='{EqpMode}'", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"Success to CallEcsApiEqpStatus with Mode='{EqpMode}'");

                    // FMS OPCUA Server에도 Write
                    if (FMSClient.WriteNodeByPath("EquipmentStatus.Status", Mode) == false)
                    {
                        _LOG_($"[FMSClient] Fail to write Status Mode : {Mode}", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[FMSClient] Success to write Status Mode : {Mode}");
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetEQPPower(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bPower = (Boolean)eventItem.LastValue.Value;

                    if (bPower == false)
                    {
                        // RestAPI eqpStatus 호출해야 함. power가 꺼지면 Status가 F가 됨. C:control, M:Manual
                        _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, "F", null, null, null, null);
                        if (response == null)
                        {
                            _LOG_("Fail to CallEcsApiEqpStatus with Mode='F'", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to CallEcsApiEqpStatus with Mode='F'");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetFMSMonitoredItemList()
        {
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayLoadResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");

            FMSClient.m_monitoredItemList.Add("Location2.TrayProcess.TrayLoadResponse");
            FMSClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessEndResponse");
        }

        private void SetEQPMonitoredItemList()
        {
            EQPClient.m_monitoredItemList.Add("Common.Alive");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Power");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Mode");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Status");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorNo");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorLevel");
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.Status");
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.ErrorNo");
            EQPClient.m_monitoredItemList.Add("EquipmentControl.Command");
            EQPClient.m_monitoredItemList.Add("EquipmentControl.CommandResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayProcess.RequestRecipeResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.Recipe.RecipeId");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellTrackIn.CellNo");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellTrackIn.CellId");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellTrackIn.CellLoadComplete");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellTrackIn.CellLoadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayProcess.TrayOut");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellTrackOut.CellNo");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellTrackOut.CellId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellTrackOut.CellUnloadComplete");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellTrackOut.CellUnloadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellId");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.LotId");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.NGCode");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.NGType");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellManualOutRequest");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellManualOutRequestResponse");

        }

    }
}
