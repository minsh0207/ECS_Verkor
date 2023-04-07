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

namespace EQP_INTF.LeakCheck
{
    public partial class LeakCheck : UserControl
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
        private void SetEqpModeRadio(UInt16 value)
        {

            if (value == 1 || value == 2)
                SetGroupRadio(Mode_Radio, value - 1);
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
            ReadPath.Add("Location1.TrayInformation.TrayExist");
            ReadPath.Add("Location1.TrayInformation.TrayId");
            ReadPath.Add("Location1.TrayInformation.TrayLoad");
            ReadPath.Add("Location1.TrayInformation.TrayLoadResponse");
            ReadPath.Add("Location1.TrayInformation.TrayType");
            ReadPath.Add("Location1.TrayInformation.ProductModel");
            ReadPath.Add("Location1.TrayInformation.RouteId");
            ReadPath.Add("Location1.TrayInformation.ProcessId");
            ReadPath.Add("Location1.TrayInformation.LotId");
            ReadPath.Add("Location1.CellTrackIn.CellNo");
            ReadPath.Add("Location1.CellTrackIn.CellId");
            ReadPath.Add("Location1.CellTrackIn.CellLoadComplete");
            ReadPath.Add("Location1.CellTrackIn.CellLoadCompleteResponse");
            ReadPath.Add("Location1.CellTrackOut.CellNo");
            ReadPath.Add("Location1.CellTrackOut.CellId");
            ReadPath.Add("Location1.CellTrackOut.CellUnloadComplete");
            ReadPath.Add("Location1.CellTrackOut.CellUnloadCompleteResponse");
            ReadPath.Add("Location1.TrackInCellInformation.CellCount");
            ReadPath.Add("Location1.TrackOutCellInformation.CellCount");
            ReadPath.Add("Location1.TrayProcess.ProcessStart");
            ReadPath.Add("Location1.TrayProcess.ProcessStartResponse");
            ReadPath.Add("Location1.TrayProcess.ProcessEnd");
            ReadPath.Add("Location1.TrayProcess.ProcessEndResponse");
            ReadPath.Add("Location1.TrayProcess.RequestRecipe");
            ReadPath.Add("Location1.TrayProcess.RequestRecipeResponse");
            ReadPath.Add("Location1.Recipe.RecipeId");
            ReadPath.Add("Location2.TrayInformation.TrayExist");
            ReadPath.Add("Location2.TrayInformation.TrayId");
            ReadPath.Add("Location2.TrayInformation.TrayLoad");
            ReadPath.Add("Location2.TrayInformation.TrayLoadResponse");
            ReadPath.Add("Location2.TrayInformation.TrayType");
            ReadPath.Add("Location2.TrayInformation.ProductModel");
            ReadPath.Add("Location2.TrayInformation.RouteId");
            ReadPath.Add("Location2.TrayInformation.ProcessId");
            ReadPath.Add("Location2.TrayInformation.LotId");
            ReadPath.Add("Location2.CellTrackIn.CellNo");
            ReadPath.Add("Location2.CellTrackIn.CellId");
            ReadPath.Add("Location2.CellTrackIn.CellLoadComplete");
            ReadPath.Add("Location2.CellTrackIn.CellLoadCompleteResponse");
            ReadPath.Add("Location2.CellTrackOut.CellNo");
            ReadPath.Add("Location2.CellTrackOut.CellId");
            ReadPath.Add("Location2.CellTrackOut.CellUnloadComplete");
            ReadPath.Add("Location2.CellTrackOut.CellUnloadCompleteResponse");
            ReadPath.Add("Location2.TrackInCellInformation.CellCount");
            ReadPath.Add("Location2.TrackOutCellInformation.CellCount");
            ReadPath.Add("Location2.TrayProcess.ProcessStart");
            ReadPath.Add("Location2.TrayProcess.ProcessStartResponse");
            ReadPath.Add("Location2.TrayProcess.ProcessEnd");
            ReadPath.Add("Location2.TrayProcess.ProcessEndResponse");
            ReadPath.Add("Location2.TrayProcess.RequestRecipe");
            ReadPath.Add("Location2.TrayProcess.RequestRecipeResponse");
            ReadPath.Add("Location2.Recipe.RecipeId");

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
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayExist"))
                    SetGroupRadio(L1_TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayId"))
                    SetTextBox(L1_TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    SetGroupRadio(L1_TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(L1_TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.ProductModel"))
                    SetTextBox(L1_Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.RouteId"))
                    SetTextBox(L1_RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.ProcessId"))
                    SetTextBox(L1_ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.LotId"))
                    SetTextBox(L1_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackIn.CellNo"))
                    SetTextBox(L1_CellTrackIn_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackIn.CellId"))
                    SetTextBox(L1_CellTrackIn_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackIn.CellLoadComplete"))
                    SetGroupRadio(L1_CellLoadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackIn.CellLoadCompleteResponse"))
                    SetGroupRadio(L1_CellLoadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackOut.CellNo"))
                    SetTextBox(L1_CellTrackOut_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackOut.CellId"))
                    SetTextBox(L1_CellTrackOut_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackOut.CellUnloadComplete"))
                    SetGroupRadio(L1_CellUnloadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.CellTrackOut.CellUnloadCompleteResponse"))
                    SetGroupRadio(L1_CellUnloadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackInCellInformation.CellCount"))
                    SetTextBox(L1_TrackIn_CellCount, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.CellCount"))
                    SetTextBox(L1_TrackOut_CellCount, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    SetGroupRadio(L1_ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    SetGroupRadio(L1_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    SetGroupRadio(L1_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(L1_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    SetGroupRadio(L1_RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    SetGroupRadio(L1_RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    SetTextBox(L1_RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.TrayExist"))
                    SetGroupRadio(L2_TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.TrayId"))
                    SetTextBox(L2_TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.TrayLoad"))
                    SetGroupRadio(L2_TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(L2_TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.ProductModel"))
                    SetTextBox(L2_Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.RouteId"))
                    SetTextBox(L2_RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.ProcessId"))
                    SetTextBox(L2_ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayInformation.LotId"))
                    SetTextBox(L2_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackIn.CellNo"))
                    SetTextBox(L2_CellTrackIn_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackIn.CellId"))
                    SetTextBox(L2_CellTrackIn_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackIn.CellLoadComplete"))
                    SetGroupRadio(L2_CellLoadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackIn.CellLoadCompleteResponse"))
                    SetGroupRadio(L2_CellLoadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackOut.CellNo"))
                    SetTextBox(L2_CellTrackOut_CellNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackOut.CellId"))
                    SetTextBox(L2_CellTrackOut_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackOut.CellUnloadComplete"))
                    SetGroupRadio(L2_CellUnloadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.CellTrackOut.CellUnloadCompleteResponse"))
                    SetGroupRadio(L2_CellUnloadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrackInCellInformation.CellCount"))
                    SetTextBox(L2_TrackIn_CellCount, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrackOutCellInformation.CellCount"))
                    SetTextBox(L2_TrackOut_CellCount, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.ProcessStart"))
                    SetGroupRadio(L2_ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.ProcessStartResponse"))
                    SetGroupRadio(L2_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.ProcessEnd"))
                    SetGroupRadio(L2_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(L2_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.RequestRecipe"))
                    SetGroupRadio(L2_RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.TrayProcess.RequestRecipeResponse"))
                    SetGroupRadio(L2_RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location2.Recipe.RecipeId"))
                    SetTextBox(L2_RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
            }

            //dataGrdidView

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
                        FMSSequenceTrayLoadResponse(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    {
                        FMSSequenceRequestRecipeResponse(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequenceProcessStartResponse(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequenceProcessEndResponse(item, "Location1");
                    }
                    ///////////////////////////////////////////////////////
                    else if (browsePath.EndsWith("Location2.TrayProcess.TrayLoadResponse"))
                    {
                        FMSSequenceTrayLoadResponse(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.RequestRecipeResponse"))
                    {
                        FMSSequenceRequestRecipeResponse(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequenceProcessStartResponse(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequenceProcessEndResponse(item, "Location2");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceProcessEndResponse(MonitoredItem item, string Location)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessEndResponse = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEndResponse)
                    {
                        // 다음공정 데이터 가져와야 하는데..이거 MES가 줄수 있나 ?
                        //NextDestination.ProcessId
                        //NextDestination.EquipmentId
                        //NextDestination.UnitId

                        //parameter에 object가 6개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 6)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = (_jsonEcsApiMasterRecipeResponse)MesResponseWaitItem.parameters[0];
                        List<string> TrayList = (List<string>)MesResponseWaitItem.parameters[0];
                        _CellInformation CellInfo = (_CellInformation)MesResponseWaitItem.parameters[1];
                        string EQPType = (string)MesResponseWaitItem.parameters[2];
                        string EQPID = (string)MesResponseWaitItem.parameters[3];
                        string UNITID = (string)MesResponseWaitItem.parameters[4];
                        //_next_process NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                        // FMS에서 다음공정 정보 읽어와야 함.
                        _next_process NEXT_PROCESS = FMSClient.ReadNextProcess($"{Location}.TrayProcess.NextDestination");

                        if (NEXT_PROCESS == null) NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                        //02. Rest API 호출해서 데이터처리
                        //http://<server_name>/ecs/processEnd
                        _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiTrayProcessEnd(TrayList, CellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_PROCESS_END");


                        //EQPClient의 ProcessEndResponse 켜준다.
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEndResponse", true) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]");

                    }
                }


            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickProcessEndResponseTimeOut(string Location)
        {
            try
            {
                //parameter에 object가 6개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 6)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }

                //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = (_jsonEcsApiMasterRecipeResponse)MesResponseWaitItem.parameters[0];
                List<string> TrayList = (List<string>)MesResponseWaitItem.parameters[0];
                _CellInformation CellInfo = (_CellInformation)MesResponseWaitItem.parameters[1];
                string EQPType = (string)MesResponseWaitItem.parameters[2];
                string EQPID = (string)MesResponseWaitItem.parameters[3];
                string UNITID = (string)MesResponseWaitItem.parameters[4];
                _next_process NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                //02. Rest API 호출해서 데이터처리
                //http://<server_name>/ecs/processEnd
                // REST API - trayProcessEnd 호출해야 함
                //TODO 20230206KJY - ProcessEnd도 테스트해봐야 함.
                _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiTrayProcessEnd(TrayList, CellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_END");

                //05. MES not-available이면 ProcessEndResponse EQP로 전달함.
                // EQPClient - ProcessEndResponse
                if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEndResponse", true) == false)
                {
                    _LOG_($"[EQPClient] Fail to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Success to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceProcessStartResponse(MonitoredItem item, string Location)
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
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStartResponse", true) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:true]");
                    
                        MesResponseWaitItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickProcessStartResponseTimeOut(string Location)
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
                if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStartResponse", true) == false)
                {
                    _LOG_($"[EQPClient] Fail to write ProcessStartResponse on EQP [{Location}.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Success to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }

        private void FMSSequenceRequestRecipeResponse(MonitoredItem item, string Location)
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
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromMES = FMSClient.ReadRecipeMES($"{Location}.Recipe");
                        if (RecipeDataFromMES == null)
                        {
                            _LOG_($"[FMSClient] Fail to read Recipe [{Location}.Recipe]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Success to read Recipe from MES [{Location}.Recipe]");

                        // MES not-available이면 MaterRecipe를 설비에 Write
                        if (EQPClient.WriteRecipeEQP($"{Location}.Recipe", RecipeDataFromMES, OPCUAClient.MappingDirection.FmsToEqp) == false)
                        {
                            _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeDataFromMES.RECIPE_DATA.RECIPE_ID}]");

                        // EQP의 RequestRecipeResponse : true
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipeResponse", true) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write RequestRecipeResponse [{Location}.TrayProcess.RequestRecipeResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write RequestRecipeResponse [{Location}.TrayProcess.RequestRecipeResponse:true]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }
        private void FMSSequencePickRequestRecipeResponseTimeOut(string Location)
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
                if (EQPClient.WriteRecipeEQP($"{Location}.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                {
                    _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                //04. RequestRecipeResponse : ON
                if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipeResponse", true) == false)
                {
                    _LOG_($"[EQPClient] Fail to write RequestRecipeResponse on EQP [{Location}.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Response for ProcessStart to EQP Sucess [{Location}.TrayProcess.RequestRecipeResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadResponse(MonitoredItem item, string Location)
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
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write TrayLoadResponse [{Location}.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write TrayLoadResponse [{Location}.TrayInformation.TrayLoadResponse:1]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePickTrayLoadResponseTimeOut(string Location)
        {
            try
            {
                // MES 없으면 바로 EQP로 response 준다.
                if (EQPClient.WriteNodeByPath($"{Location}.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_($"[EQPClient] Fail to write TrayLoadResponse [{Location}.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Success to write TrayLoadResponse [{Location}.TrayInformation.TrayLoadResponse:1]");
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
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayExist"))
                    {
                        UpdateRadioButton(L1_TrayExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayId"))
                    {
                        UpdateTextBox(L1_TrayId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(L1_TrayLoad_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrayLoad(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(L1_TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.ProductModel"))
                    {
                        UpdateTextBox(L1_Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.RouteId"))
                    {
                        UpdateTextBox(L1_RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.ProcessId"))
                    {
                        UpdateTextBox(L1_ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.LotId"))
                    {
                        UpdateTextBox(L1_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackIn.CellNo"))
                    {
                        UpdateTextBox(L1_CellTrackIn_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackIn.CellId"))
                    {
                        UpdateTextBox(L1_CellTrackIn_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackIn.CellLoadComplete"))
                    {
                        UpdateRadioButton(L1_CellLoadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceCellLoadComplete(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackIn.CellLoadCompleteResponse"))
                    {
                        UpdateRadioButton(L1_CellLoadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackOut.CellNo"))
                    {
                        UpdateTextBox(L1_CellTrackOut_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackOut.CellId"))
                    {
                        UpdateTextBox(L1_CellTrackOut_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackOut.CellUnloadComplete"))
                    {
                        UpdateRadioButton(L1_CellUnloadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceCellUnloadComplete(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.CellTrackOut.CellUnloadCompleteResponse"))
                    {
                        UpdateRadioButton(L1_CellUnloadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackInCellInformation.CellCount"))
                    {
                        UpdateTextBox(L1_TrackIn_CellCount, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.CellCount"))
                    {
                        UpdateTextBox(L1_TrackOut_CellCount, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    {
                        UpdateRadioButton(L1_ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessStart(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(L1_ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(L1_ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessEnd(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(L1_ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(L1_RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceRequestRecipe(item, "Location1");
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(L1_RequestRecipeResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    {
                        UpdateTextBox(L1_RecipeId_TextBox, item);
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    else if (browsePath.EndsWith("Location2.TrayInformation.TrayExist"))
                    {
                        UpdateRadioButton(L2_TrayExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.TrayId"))
                    {
                        UpdateTextBox(L2_TrayId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(L2_TrayLoad_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrayLoad(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(L2_TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.ProductModel"))
                    {
                        UpdateTextBox(L2_Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.RouteId"))
                    {
                        UpdateTextBox(L2_RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.ProcessId"))
                    {
                        UpdateTextBox(L2_ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayInformation.LotId"))
                    {
                        UpdateTextBox(L2_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackIn.CellNo"))
                    {
                        UpdateTextBox(L2_CellTrackIn_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackIn.CellId"))
                    {
                        UpdateTextBox(L2_CellTrackIn_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackIn.CellLoadComplete"))
                    {
                        UpdateRadioButton(L2_CellLoadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceCellLoadComplete(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackIn.CellLoadCompleteResponse"))
                    {
                        UpdateRadioButton(L2_CellLoadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackOut.CellNo"))
                    {
                        UpdateTextBox(L2_CellTrackOut_CellNo_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackOut.CellId"))
                    {
                        UpdateTextBox(L2_CellTrackOut_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackOut.CellUnloadComplete"))
                    {
                        UpdateRadioButton(L2_CellUnloadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceCellUnloadComplete(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.CellTrackOut.CellUnloadCompleteResponse"))
                    {
                        UpdateRadioButton(L2_CellUnloadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrackInCellInformation.CellCount"))
                    {
                        UpdateTextBox(L2_TrackIn_CellCount, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrackOutCellInformation.CellCount"))
                    {
                        UpdateTextBox(L2_TrackOut_CellCount, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessStart"))
                    {
                        UpdateRadioButton(L2_ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessStart(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(L2_ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(L2_ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessEnd(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(L2_ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(L2_RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceRequestRecipe(item, "Location2");
                    }
                    else if (browsePath.EndsWith("Location2.TrayProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(L2_RequestRecipeResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location2.Recipe.RecipeId"))
                    {
                        UpdateTextBox(L2_RecipeId_TextBox, item);
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceRequestRecipe(MonitoredItem item, string Location)
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
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.ProcessId", out ProcessId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.TrayInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.RouteId", out route_id) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.TrayInformation.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.ProductModel", out model_id) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.TrayInformation.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
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
                            if (EQPClient.WriteRecipeEQP($"{Location}.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                            {
                                _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                            //04. RequestRecipeResponse : ON
                            if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipeResponse", true) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write RequestRecipeResponse on EQP [{Location}.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Response for ProcessStart to EQP Sucess [{Location}.TrayProcess.RequestRecipeResponse:true]");

                            // FMS Server에도 그냥 써준다. flow 대로
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write RequestRecipe on FMS [{Location}.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [{Location}.TrayProcess.RequestRecipe:true]");

                        }
                        else
                        {   // MES가 가동중일때는 FMS 의 requestRecipe만 켜준다.
                            // FMS에 recipe 써줘야 하나?
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write RequestRecipe on FMS [{Location}.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [{Location}.TrayProcess.RequestRecipe:true]");

                            // 20230224 KJY - MES Timeout 세팅
                            // 전달할 parameter model_id, route_id, targetEQPType, targetProcessType, targetProcessNo
                            List<object> parameters = new List<object>();
                            parameters.Add(model_id);
                            parameters.Add(route_id);
                            parameters.Add(targetEQPType);
                            parameters.Add(targetProcessType);
                            parameters.Add(targetProcessNo);

                            WaitMesResponse($"{Location}.TrayProcess.RequestRecipeResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipe", (Boolean)false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write [{Location}.TrayProcess.RequestRecipe : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [{Location}.TrayProcess.RequestRecipe : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.RequestRecipeResponse", (Boolean)false) == false)
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

        private void SequenceProcessEnd(MonitoredItem item, string Location)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bProcessEnd = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEnd)
                    {
                        // 일단 공정진행한 Recipe 확보
                        //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("Location1.TrayInformation", "Location1.Recipe");
                        //_LOG_("Read Recipe from EQP");
                        //_LOG_($"{JsonConvert.SerializeObject(RecipeDataFromOPCUA, Formatting.Indented) }");

                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read TrayId [{Location}.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [{Location}.TrayInformation.TrayId]");
                        List<string> TrayList = new List<string>();
                        TrayList.Add(TrayId);
                        //_CellInformation CellInfo = EQPClient.ReadProcessDataEQP($"{Location}.TrackOutCellInformation", $"{Location}.TrayInformation");
                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 가져온다.
                        _CellInformation CellInfo = EQPClient.ReadProcessDataEQP($"{Location}.TrackOutCellInformation");
                        if (CellInfo == null) return;
                        _LOG_($"[EQPClient] Success to read ProcessData from EQP:{TrayId} from EQP [{Location}.TrackOutCellInformation]");
                        // 다음공정정보 미리 받아두기.
                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 가져온다.
                        _next_process NEXT_PROCESS = GetMasterNextProcessInfo(Location, out string processId);
                        if (NEXT_PROCESS == null)
                        {
                            _LOG_("Fail to GetMasterNextProcessInfo()", ECSLogger.LOG_LEVEL.WARN);
                        }
                        _LOG_($"Success to call MasterNextProcess");

                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 할당 해준다.
                        CellInfo.ProcessId = processId;

                        if (MesAvailable())
                        {
                            //03. 결과 데이터를 MES로 Write한다.
                            // 이때 timestamp 도 함께 옮겨야함 주의
                            // FMS OPCUA Server에 Write
                            if (FMSClient.WriteProcessDataFMS(CellInfo, $"{Location}.ProcessData") == false)
                            {
                                _LOG_($"[FMSClient] Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                            }
                            _LOG_("[FMSClient] Success to write Process Data");

                            //04-1 ProcessEnd를 MES로 보내고 대기
                            // FMSClient에 ProcessEnd
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write ProcessEnd [{Location}.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Success to write ProcessStart [{Location}.TrayProcess.ProcessStart:true]");

                            // 20230224 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            //parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(TrayList);
                            parameters.Add(CellInfo);
                            parameters.Add(EQPType);
                            parameters.Add(EQPID);
                            parameters.Add(UNITID);
                            parameters.Add(NEXT_PROCESS);

                            WaitMesResponse($"{Location}.TrayProcess.ProcessEndResponse", parameters);
                        }
                        else
                        {
                            //02. Rest API 호출해서 데이터처리
                            //http://<server_name>/ecs/processEnd
                            // REST API - trayProcessEnd 호출해야 함
                            //TODO 20230206KJY - ProcessEnd도 테스트해봐야 함.
                            _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiTrayProcessEnd(TrayList, CellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);
                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - TRAY_PROCESS_END");

                            //05. MES not-available이면 ProcessEndResponse EQP로 전달함.
                            // EQPClient - ProcessEndResponse
                            if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEndResponse", true) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Success to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:true]");

                            // FMSClient에 ProcessEnd - MES 안 붙어 있어도 flow 대로..
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write ProcessEnd [{Location}.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Success to write ProcessEnd [{Location}.TrayProcess.ProcessEnd:true]");
                        }

                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write RequestRecipe [{Location}.TrayProcess.ProcessEnd]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Success to write ProcessEnd [{Location}.TrayProcess.ProcessEnd:false]");
                        // EQP - ProcessStartResponse : off
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessEndResponse", false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write ProcessEndResponse [{Location}.TrayProcess.ProcessEndResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        public _next_process GetMasterNextProcessInfo(string Location, out string outProcessId)
        {
            try
            {
                //REST로 다음 공정 정보 받아둠
                // model/route/processno 정보는 EQP OPCUA에서 읽어 두자.
                String model_id, route_id, ProcessId, processType;
                int process_no;
                EQPClient.ReadValueByPath($"{Location}.TrayInformation.ProductModel", out model_id);
                EQPClient.ReadValueByPath($"{Location}.TrayInformation.RouteId", out route_id);
                EQPClient.ReadValueByPath($"{Location}.TrayInformation.ProcessId", out ProcessId);
                processType = ProcessId.Substring(3, 3);
                process_no = int.Parse(ProcessId.Substring(6));

                outProcessId = ProcessId;       // 20230324 msh : 추가

                _jsonMasterNextProcessResponse NextProcessInfo = RESTClientBiz.CallEcsApiMasterNextProcess(EQPType, EQPID, UNITID,
                    model_id, route_id, processType, process_no);
                if (NextProcessInfo == null || NextProcessInfo.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call CallEcsApiMasterNextProcess : [{model_id}:{route_id}:{EQPType}:{processType}:{process_no}]",
                        ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                _next_process NEXT_PROCESS = new _next_process();
                NEXT_PROCESS.NEXT_MANUAL_SET_FLAG = "N";
                NEXT_PROCESS.NEXT_ROUTE_ORDER_NO = NextProcessInfo.NEXT_PROCESS.NEXT_ROUTE_ORDER_NO;
                NEXT_PROCESS.NEXT_EQP_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE;
                NEXT_PROCESS.NEXT_PROCESS_TYPE = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE;
                NEXT_PROCESS.NEXT_PROCESS_NO = NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO;
                NEXT_PROCESS.NEXT_EQP_ID = String.Empty;
                NEXT_PROCESS.NEXT_UNIT_ID = String.Empty;

                return NEXT_PROCESS;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);

                outProcessId = string.Empty;       // 20230324 msh : 추가

                return null;
            }
        }

        private void SequenceProcessStart(MonitoredItem item, string Location)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessStart = (Boolean)eventItem.LastValue.Value;
                    if (bProcessStart)
                    {
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP($"{Location}.TrayInformation", $"{Location}.Recipe");
                        if (RecipeDataFromOPCUA == null)
                        {
                            _LOG_("[EQPClient] Fail to read Recipe Data", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read TrayId [{Location}.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [{Location}.TrayInformation.TrayId]");

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
                            if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStartResponse", true) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write ProcessStartResponse on EQP [{Location}.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Success to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:true]");

                            // FMSClient에 ProcessStart (MES가 붙었든 아니든 일단 flow는 동일하게 간다.)
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStart", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write RequestRecipe [{Location}.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Success to write ProcessStart [{Location}.TrayProcess.ProcessStart:true]");
                        }
                        else
                        {
                            // FMSClient에 ProcessStart
                            if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStart", true) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write RequestRecipe [{Location}.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Success to write ProcessStart [{Location}.TrayProcess.ProcessStart:true]");

                            // 20230222 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(UNITID);
                            parameters.Add(TrayId);

                            WaitMesResponse($"{Location}.TrayProcess.ProcessStartResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStart", false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write ProcessStart [{Location}.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Success to write ProcessStart [{Location}.TrayProcess.ProcessStart:false]");
                        // EQP - ProcessStartResponse : off                    
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayProcess.ProcessStartResponse", false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write ProcessStartResponse [{Location}.TrayProcess.ProcessStartResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceCellUnloadComplete(MonitoredItem item, string Location)
        {
            //Check만 한다.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bCellLoadComplete = (Boolean)eventItem.LastValue.Value;
                    if (bCellLoadComplete)
                    {
                        // From CellTrackOut
                        String CellId;
                        UInt16 CellNo;
                        if (EQPClient.ReadValueByPath($"{Location}.CellTrackOut.CellId", out CellId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.CellTrackOut.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath($"{Location}.CellTrackOut.CellNo", out CellNo) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.CellTrackOut.CellNo]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackOut : CellId[{CellId}], CellNo[{CellNo}]");


                        // TODO 20230302
                        //뭐 체크할꺼 없나?


                        // Confirm
                        if (EQPClient.WriteNodeByPath($"{Location}.CellTrackOut.CellUnloadCompleteResponse", true) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write CellLoadResponse on EQP [{Location}.CellTrackOut.CellUnloadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Response for CellUnloadComplete to EQP Sucess [{Location}.CellTrackOut.CellUnloadCompleteResponse:true]");
                    }
                    else
                    {
                        // reset
                        if (EQPClient.WriteNodeByPath($"{Location}.CellTrackOut.CellUnloadCompleteResponse", false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write CellUnloadCompleteResponse on EQP [{Location}.CellTrackOut.CellUnloadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Clear for CellUnloadComplete to EQP Sucess [{Location}.CellTrackOut.CellUnloadCompleteResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceCellLoadComplete(MonitoredItem item, string Location)
        {
            //Check만 한다
            try
            {
                // 일단 TrackInCellInformation.Cell._xx 와 비교해서 데이터가 동일한지 확인한다.
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
                        if (EQPClient.ReadValueByPath($"{Location}.CellTrackIn.CellNo", out ReqCellNo) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.CellTrackIn.CellNo]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath($"{Location}.CellTrackIn.CellId", out ReqCellId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{Location}.CellTrackIn.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackIn CellNo[{ReqCellNo}], CellId[{ReqCellId}]");

                        //TrackInCellInformation.Cell._xx 에 있는 Cell 정보
                        String CellId;
                        string CellIdPath = $"{Location}.TrackInCellInformation.Cell._{ReqCellNo}.CellId";
                        if (EQPClient.ReadValueByPath(CellIdPath, out CellId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{CellIdPath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellId [{CellIdPath}],  CellId[{CellId}]");

                        if (ReqCellId == CellId)
                        {
                            // Confirm
                            if (EQPClient.WriteNodeByPath($"{Location}.CellTrackIn.CellLoadCompleteResponse", true) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write CellLoadCompleteResponse on EQP [{Location}.CellTrackIn.CellLoadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Response for CellLoadComplete to EQP Sucess [{Location}.CellTrackIn.CellLoadCompleteResponse:true]");
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
                        if (EQPClient.WriteNodeByPath($"{Location}.CellTrackIn.CellLoadCompleteResponse", false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write CellLoadCompleteResponse on EQP [{Location}.CellTrackIn.CellLoadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Response for CellLoadComplete to EQP Sucess [{Location}.CellTrackIn.CellLoadCompleteResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceTrayLoad(MonitoredItem item, string Location)
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
                        _LOG_("Start SequenceTrayLoad:ON");

                        //TrayId를 EQP OPCUA에서 읽는다.
                        if (EQPClient.ReadValueByPath($"{Location}.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read TrayId [{Location}.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [{Location}.TrayInformation.TrayId]");

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
                                if (EQPClient.WriteBasicTrayInfoEQP($"{Location}.TrayInformation", TrayData.DATA[0]) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write TrayInfo [{Location}.TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Success to write TrayInfo [{Location}.TrayInformation] on EQP, {TrayId}");

                                if (EQPClient.WriteBasicCellInfoEQP($"{Location}.TrackInCellInformation.Cell", TrayCellData.DATA, 0) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write TrayInfo [{Location}.TrackInCellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                // CellCount도 쓰자
                                if (EQPClient.WriteNodeByPath($"{Location}.TrackInCellInformation.CellCount", (UInt16)TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write CellCount [{Location}.TrackInCellInformation.CellCount:{TrayCellData.DATA.Count}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write Cell Information to [{Location}.TrackInCellInformation] : {TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.DATA.Count}");

                                // FMS에 Write
                                if (FMSClient.WriteBasicTrayInfoFMS($"{Location}.TrayInformation", $"{Location}.TrayInformation.CellCount", TrayData.DATA[0], TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write Tray Info [{Location}.TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Tray Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");
                                if (FMSClient.WriteBasicCellInfoFMS($"{Location}.TrayInformation.CellInformation", TrayCellData.DATA) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [{Location}.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                                // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                                _CellBasicInformation TrackInCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation($"{Location}.TrackInCellInformation");
                                if(Location == "Location1")
                                    DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, L1_TrackInCellList_dataGridView);
                                else
                                    DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, L2_TrackInCellList_dataGridView);

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
                                if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrayLoad ON FMSClient [{Location}.TrayProcess.TrayLoad:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [{Location}.TrayProcess.TrayLoad : True]");

                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse($"{Location}.TrayProcess.TrayLoadResponse");

                            }
                            else
                            {
                                // MES 없으면 바로 EQP로 response 준다.
                                if (EQPClient.WriteNodeByPath($"{Location}.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write TrayLoadResponse ON EQPClient [{Location}.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [{Location}.TrayInformation.TrayLoadResponse : 1]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrayLoad ON FMSClient [{Location}.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [{Location}.TrayProcess.TrayLoad : True]");
                            }
                        }
                        else
                        {
                            // EQP OPCUA에 TrayId가 없는 상황
                            _LOG_($"[EQPClient] No TrayId on EQPClient [{Location}.TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }
                    }
                    else // TrayLoad : Off
                    {
                        if (EQPClient.WriteNodeByPath($"{Location}.TrayInformation.TrayLoadResponse", (UInt16)0) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write TrayLoadResponse ON EQPClient [{Location}.TrayInformation.TrayLoadResponse:0]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Complete to write [{Location}.TrayInformation.TrayLoadResponse : 0]");

                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath($"{Location}.TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write TrayLoad ON FMSClient [{Location}.TrayProcess.TrayLoad;false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [{Location}.TrayProcess.TrayLoad : false]");
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
            FMSClient.m_monitoredItemList.Add("Location2.TrayProcess.RequestRecipeResponse");
            FMSClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessStartResponse");
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
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayType");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackIn.CellNo");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackIn.CellId");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackIn.CellLoadComplete");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackIn.CellLoadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackOut.CellNo");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackOut.CellId");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackOut.CellUnloadComplete");
            EQPClient.m_monitoredItemList.Add("Location1.CellTrackOut.CellUnloadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrackInCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");
            EQPClient.m_monitoredItemList.Add("Location1.Recipe.RecipeId");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.TrayType");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("Location2.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackIn.CellNo");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackIn.CellId");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackIn.CellLoadComplete");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackIn.CellLoadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackOut.CellNo");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackOut.CellId");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackOut.CellUnloadComplete");
            EQPClient.m_monitoredItemList.Add("Location2.CellTrackOut.CellUnloadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("Location2.TrackInCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("Location2.TrackOutCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("Location2.TrayProcess.RequestRecipeResponse");
            EQPClient.m_monitoredItemList.Add("Location2.Recipe.RecipeId");
        }

    }
}
