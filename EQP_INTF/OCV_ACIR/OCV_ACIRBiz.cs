using Newtonsoft.Json;
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

namespace EQP_INTF.OCV_ACIR
{
    public partial class OCV_ACIR : UserControl
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

        private void InitDisplayWithCurrentValue(OPCUAClient.OPCUAClient OPCClient)
        {
            List<string> ReadPath = new List<string>();

            ReadPath.Add("Common.Alive");

            ReadPath.Add("EquipmentStatus.Power");
            ReadPath.Add("EquipmentStatus.Mode");
            ReadPath.Add("EquipmentStatus.Status");
            ReadPath.Add("EquipmentStatus.Trouble.ErrorNo");
            ReadPath.Add("EquipmentStatus.Trouble.ErrorLevel");

            ReadPath.Add("FmsStatus.Trouble.ErrorNo");
            ReadPath.Add("FmsStatus.Trouble.Status");

            ReadPath.Add("EquipmentControl.Command");
            ReadPath.Add("EquipmentControl.CommandResponse");

            ReadPath.Add("Location1.TrayInformation.TrayExist");
            ReadPath.Add("Location1.TrayInformation.TrayId");
            ReadPath.Add("Location1.TrayInformation.ProductModel");
            ReadPath.Add("Location1.TrayInformation.RouteId");
            ReadPath.Add("Location1.TrayInformation.ProcessId");
            ReadPath.Add("Location1.TrayInformation.LotId");

            ReadPath.Add("Location1.TrackInCellInformation.CellCount");

            ReadPath.Add("Location1.TrackOutCellInformation.CellCount");

            //for OCV
            ReadPath.Add("Location1.TrackOutCellInformation.StartFrequency");
            ReadPath.Add("Location1.TrackOutCellInformation.EndFrequency");
            ReadPath.Add("Location1.TrackOutCellInformation.NumberOfPoints");

            ReadPath.Add("Location1.TrackOutCellInformation.StartTemp");
            ReadPath.Add("Location1.TrackOutCellInformation.EndTemp");
            ReadPath.Add("Location1.TrackOutCellInformation.AvgTemp");

            ReadPath.Add("Location1.TrayInformation.TrayLoad");
            ReadPath.Add("Location1.TrayInformation.TrayLoadResponse");
            ReadPath.Add("Location1.TrayProcess.RequestRecipe");
            ReadPath.Add("Location1.TrayProcess.RequestRecipeResponse");
            ReadPath.Add("Location1.Recipe.RecipeId");
            ReadPath.Add("Location1.Recipe.OperationMode");

            ReadPath.Add("Location1.TrayProcess.ProcessStart");
            ReadPath.Add("Location1.TrayProcess.ProcessStartResponse");
            ReadPath.Add("Location1.TrayProcess.ProcessEnd");
            ReadPath.Add("Location1.TrayProcess.ProcessEndResponse");

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
                    SetGroupRadio(Mode_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Status"))
                    SetTextBox(Status_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorNo"))
                    SetTextBox(EQP_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentStatus.Trouble.ErrorLevel"))
                    SetGroupRadio(EQP_ErrorLevel_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    SetTextBox(FMS_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    SetGroupRadio(FMS_Status_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.Command"))
                    SetTextBox(Command_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    SetGroupRadio(CommandResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayExist"))
                    SetGroupRadio(TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayId"))
                    SetTextBox(TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.ProductModel"))
                    SetTextBox(Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.RouteId"))
                    SetTextBox(RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.ProcessId"))
                    SetTextBox(ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.LotId"))
                    SetTextBox(LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackInCellInformation.CellCount"))
                    SetTextBox(TrackInCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.CellCount"))
                    SetTextBox(TrackOutCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.StartFrequency"))
                    SetTextBox(StartFrequency_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.EndFrequency"))
                    SetTextBox(EndFrequency_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.NumberOfPoints"))
                    SetTextBox(NumberOfPoints_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);


                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.StartTemp"))
                    SetTextBox(StartTemp_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.EndTemp"))
                    SetTextBox(EndTemp_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrackOutCellInformation.AvgTemp"))
                    SetTextBox(AvgTemp_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    SetGroupRadio(TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    SetGroupRadio(RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    SetGroupRadio(RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    SetTextBox(RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.Recipe.OperationMode"))
                    SetTextBox(OperationMode_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    SetGroupRadio(ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    SetGroupRadio(ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    SetGroupRadio(ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    SetGroupRadio(ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
            }

            // TrackInCellInformation 의 Cell 정보 
            _CellBasicInformation TrackInCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("Location1.TrackInCellInformation");
            DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, TrackInCellList_DataGridView);

            // TrackOutCellInformation의 Cell 정보
            _CellBasicInformation TrackOutCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("Location1.TrackOutCellInformation");
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
                    // 20230323 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"FMSClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("TrayProcess.TrayLoadResponse"))
                    {
                        FMSSequenceTrayLoadResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.RequestRecipeResponse"))
                    {
                        FMSSequenceRequestRecipeResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequenceProcessStartResponse(item);
                    }
                    else if (browsePath.EndsWith("TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequenceProcessEndResponse(item);
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceProcessEndResponse(MonitoredItem item)
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
                        _next_process NEXT_PROCESS = FMSClient.ReadNextProcess("Location1.TrayProcess.NextDestination");

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
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessEndResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]");

                    }
                }


            } catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceProcessEndResponseTimeOut()
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
                if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessEndResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]");


            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceProcessStartResponse(MonitoredItem item)
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
                        List<string> TrayList = (List<string>)MesResponseWaitItem.parameters[2];

                        //01. Rest API 호출해서 데이터처리
                        //http://<server_name>/ecs/processStart
                        _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayList);

                        // 성공여부
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_PROCESS_START");

                        //EQPClient의 ProcessStartResponse 켜준다.
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessStartResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:true]");
                    
                        MesResponseWaitItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceProcessStartResponseTimeOut()
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
                List<string> TrayList = (List<string>)MesResponseWaitItem.parameters[2];

                //01. Rest API 호출해서 데이터처리
                //http://<server_name>/ecs/processStart
                _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayList);

                // 성공여부
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_START");

                // ProcessStartResponse
                if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessStartResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [Location1.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
            }
        }

        private void FMSSequenceRequestRecipeResponse(MonitoredItem item)
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
                    Boolean bRequestRecipeResponse = (Boolean)eventItem.LastValue.Value;
                    if(bRequestRecipeResponse)
                    {
                        // FMS OPCUA Server로 부터 Recipe 정보를 읽어서 EQP로 write
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromMES = FMSClient.ReadRecipeMES("Location1.Recipe");
                        if (RecipeDataFromMES == null)
                        {
                            _LOG_("[FMSClient] Fail to read Recipe [Location1.Recipe]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        // MES not-available이면 MaterRecipe를 설비에 Write
                        if (EQPClient.WriteRecipeEQP("Location1.Recipe", RecipeDataFromMES, OPCUAClient.MappingDirection.FmsToEqp) == false)
                        {
                            _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeDataFromMES.RECIPE_DATA.RECIPE_ID}]");

                        // EQP의 RequestRecipeResponse : true
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipeResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write RequestRecipeResponse [Location1.TrayProcess.RequestRecipeResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write RequestRecipeResponse [Location1.TrayProcess.RequestRecipeResponse:true]");
                    }
                }

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceRequestRecipeResponseTimeOut()
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

                // REST API 호출해서 Master Recipe 받아옴.
                _jsonEcsApiMasterRecipeResponse RecipeResponse = RESTClientBiz.CallEcsApiMasterRecipe(model_id, route_id, targetEQPType, targetProcessType, targetProcessNo);
                if (RecipeResponse.RESPONSE_CODE != "200")
                {
                    _LOG_($"[{this.Name}] Fail to call CallEcsApiMasterRecipe, model_id:{model_id}, route_id:{route_id}, eqp_type:{targetEQPType}, process_type:{targetProcessType}, process_no:{targetProcessNo} : RESPONSE_MESSAGE : {RecipeResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"Scuccess to Get Recipy by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}");

                // MES not-available이면 MaterRecipe를 설비에 Write
                if (EQPClient.WriteRecipeEQP("Location1.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                {
                    _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                //04. RequestRecipeResponse : ON
                if (EQPClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipeResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [Location1.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [Location1.TrayProcess.RequestRecipeResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadResponse(MonitoredItem item)
        {
            if (MesResponseTimer != null) MesResponseTimer.Stop();
            MesResponseTimer = null;
            MesResponseWaitItem = null;

            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bTrayLoadResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayLoadResponse)
                    {
                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceTrayLoadResponseTimeOut()
        {
            try
            {
                // MES 응답 없으면 바로 EQP로 response 준다.
                if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [Location1.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                }
                _LOG_($"[EQPClient] Complete to write [Location1.TrayInformation.TrayLoadResponse : 1]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                    // 20230323 msh : UserData에 정의된 browsePath를 사용한다.
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
                        UpdateRadioButton(Mode_Radio, item, false);
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
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(TrayLoad_Radio, item);
                        // Tray와 Cell정보를 조회해서 EQP, FMS 모두 써준다.
                        if (EqpAvailable()) SequenceTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayExist"))
                    {
                        UpdateRadioButton(TrayExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.TrayId"))
                    {
                        UpdateTextBox(TrayId_TextBox, item);
                    }

                    else if (browsePath.EndsWith("Location1.TrayInformation.ProductModel"))
                    {
                        UpdateTextBox(Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.RouteId"))
                    {
                        UpdateTextBox(RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.ProcessId"))
                    {
                        UpdateTextBox(ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayInformation.LotId"))
                    {
                        UpdateTextBox(LotId_TextBox, item);
                    }

                    else if (browsePath.EndsWith("Location1.TrackInCellInformation.CellCount"))
                    {
                        UpdateTextBox(TrackInCellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.CellCount"))
                    {
                        UpdateTextBox(TrackOutCellCount_TextBox, item);

                        SetTrackOutCellInformation(item);     // 20230324 msh : 추가
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.StartFrequency"))
                    {
                        UpdateTextBox(StartFrequency_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.EndFrequency"))
                    {
                        UpdateTextBox(EndFrequency_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.NumberOfPoints"))
                    {
                        UpdateTextBox(NumberOfPoints_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.StartTemp"))
                    {
                        UpdateTextBox(StartTemp_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.EndTemp"))
                    {
                        UpdateTextBox(EndTemp_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrackOutCellInformation.AvgTemp"))
                    {
                        UpdateTextBox(AvgTemp_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStart"))
                    {
                        UpdateRadioButton(ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessStart(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceRequestRecipe(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(RequestRecipeResponse_Radio, item);
                    }

                    else if (browsePath.EndsWith("Location1.Recipe.RecipeId"))
                    {
                        UpdateTextBox(RecipeId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Location1.Recipe.OperationMode"))
                    {
                        UpdateTextBox(OperationMode_TextBox, item);
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceRequestRecipe(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                string model_id;
                string route_id;
                string processId;
                string targetEQPType;
                string targetProcessType;
                string targetProcessNo;

                if (eventItem != null)
                {
                    Boolean bRequestRecipe = (Boolean)eventItem.LastValue.Value;
                    if (bRequestRecipe)
                    {
                        if(ReadTrayBasicInfoFromEQP(out  model_id, out  route_id, out  processId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [Location1.TrayInformation - ProductModel/RouteId/ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Success to read Tray basic information, ProductModel:{model_id}/RouteId:{route_id}/ProcessId:{processId}");
                        targetEQPType = processId.Substring(0, 3);
                        targetProcessType = processId.Substring(3, 3);
                        targetProcessNo = processId.Substring(6);

                        if (MesAvailable() == false)
                        {
                            // REST API 호출해서 Master Recipe 받아옴.
                            _jsonEcsApiMasterRecipeResponse RecipeResponse = RESTClientBiz.CallEcsApiMasterRecipe(model_id, route_id, targetEQPType, targetProcessType, targetProcessNo);
                            if (RecipeResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"[{this.Name}] Fail to call CallEcsApiMasterRecipe, model_id:{model_id}, route_id:{route_id}, eqp_type:{targetEQPType}, process_type:{targetProcessType}, process_no:{targetProcessNo} : RESPONSE_MESSAGE : {RecipeResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Scuccess to Get Recipy by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}");

                            // MES not-available이면 MaterRecipe를 설비에 Write
                            if (EQPClient.WriteRecipeEQP("Location1.Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                            {
                                _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                            //04. RequestRecipeResponse : ON
                            if (EQPClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipeResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [Location1.TrayProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [Location1.TrayProcess.RequestRecipeResponse:true]");

                            // MES가 붙어 있지 않지만 flow는 동일하게 간다.
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [Location1.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [Location1.TrayProcess.RequestRecipe:true]");
                        }
                        else
                        {
                            // MES가 붙어있으면, FMS로만 요청을 보낸다.
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [Location1.TrayProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [Location1.TrayProcess.RequestRecipe:true]");

                            // 20230222 KJY - MES Timeout 세팅
                            // 전달할 parameter는 model_id, route_id, targetEQPType, targetProcessType, targetProcessNo 모두 String.
                            List<object> parameters = new List<object>();
                            parameters.Add(model_id);
                            parameters.Add(route_id);
                            parameters.Add(targetEQPType);
                            parameters.Add(targetProcessType);
                            parameters.Add(targetProcessNo);

                            WaitMesResponse("Location1.TrayProcess.RequestRecipeResponse", parameters);
                        }

                    } else
                    {
                        // FMS RequestRecipe 먼저 OFF
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", (Boolean)false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write [Location1.TrayProcess.RequestRecipe : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.RequestRecipe : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipeResponse", (Boolean)false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write [Location1.TrayProcess.RequestRecipeResponse : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Complete to write [Location1.TrayProcess.RequestRecipeResponse : false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        public bool ReadTrayBasicInfoFromEQP(out string model_id, out string route_id, out string processId)
        {
            string ProcessId;
            string ProductModel;
            string RouteId;

            model_id = route_id = processId = string.Empty;

            if (EQPClient.ReadValueByPath("Location1.TrayInformation.ProcessId", out ProcessId) == false)
            {
                _LOG_("[EQPClient] Fail to read from [Location1.TrayInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.ProductModel", out ProductModel) == false)
            {
                _LOG_("[EQPClient] Fail to read from [Location1.TrayInformation.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.RouteId", out RouteId) == false)
            {
                _LOG_("[EQPClient] Fail to read from [Location1.TrayInformation.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            model_id = ProductModel;
            route_id = RouteId;
            processId = ProcessId;

            if (model_id.Length < 1)
            {
                _LOG_($"[EQPClient] Fail to read ProductModel form EQP : [{model_id}] length error", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            if (route_id.Length < 1)
            {
                _LOG_($"[EQPClient] Fail to read RouteId form EQP : [{route_id}] length error", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            if (processId.Length < 7)
            {
                _LOG_($"[EQPClient] Fail to read ProcessId form EQP : [{processId}] length error", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            return true;
        }

        private void SequenceProcessEnd(MonitoredItem item)
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
                        if (EQPClient.ReadValueByPath("Location1.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [Location1.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [Location1.TrayInformation.TrayId]");
                        List<string> TrayList = new List<string>();
                        TrayList.Add(TrayId);
                        _CellInformation CellInfo = EQPClient.ReadProcessDataEQP_OCV("Location1.TrackOutCellInformation");
                        if (CellInfo == null) return;
                        _LOG_($"[EQPClient] Success to read ProcessData from EQP:{TrayId} from EQP [Location1.TrackOutCellInformation]");
                        // 다음공정정보 미리 받아두기.
                        _next_process NEXT_PROCESS = GetMasterNextProcessInfo(out string processId);
                        if (NEXT_PROCESS == null)
                        {
                            _LOG_("Fail to GetMasterNextProcessInfo()", ECSLogger.LOG_LEVEL.WARN);
                        }
                        _LOG_($"Success to call MasterNextProcess");

                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 할당 해준다.
                        CellInfo.ProcessId = processId;

                        // 20230330 msh : Cell 정보를 표시한다.
                        _CellBasicInformation TrackOutCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation("Location1.TrackOutCellInformation");
                        DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_DataGridView, false);

                        if (MesAvailable())
                        {
                            //03. 결과 데이터를 MES로 Write한다.
                            // 이때 timestamp 도 함께 옮겨야함 주의
                            // FMS OPCUA Server에 Write
                            if (FMSClient.WriteProcessDataFMS(CellInfo, "Location1.ProcessData") == false)
                            {
                                _LOG_($"[FMSClient] Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                            }
                            _LOG_("[FMSClient] Success to write Process Data");

                            //04-1 ProcessEnd를 MES로 보내고 대기
                            // FMSClient에 ProcessEnd
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessStart [Location1.TrayProcess.ProcessStart:true]");

                            // 20230224 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            //parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(TrayList);
                            parameters.Add(CellInfo);
                            parameters.Add(EQPType);
                            parameters.Add(EQPID);
                            parameters.Add(UNITID);
                            parameters.Add(NEXT_PROCESS);

                            WaitMesResponse("Location1.TrayProcess.ProcessEndResponse", parameters);
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
                            if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessEndResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]");

                            // FMSClient에 ProcessEnd - MES 안 붙어 있어도 flow 대로..
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]");
                        }

                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:false]");
                        // EQP - ProcessEndResponse : off
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessEndResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        public _next_process GetMasterNextProcessInfo(out string outProcessId)
        {
            try
            {
                //REST로 다음 공정 정보 받아둠
                // model/route/processno 정보는 EQP OPCUA에서 읽어 두자.
                String model_id, route_id, ProcessId, processType;
                int process_no;
                EQPClient.ReadValueByPath("Location1.TrayInformation.ProductModel", out model_id);
                EQPClient.ReadValueByPath("Location1.TrayInformation.RouteId", out route_id);
                EQPClient.ReadValueByPath("Location1.TrayInformation.ProcessId", out ProcessId);
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

        private void SequenceProcessStart(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessStart = (Boolean)eventItem.LastValue.Value;
                    if (bProcessStart)
                    {
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("Location1.TrayInformation", "Location1.Recipe", true);
                        if (RecipeDataFromOPCUA == null)
                        {
                            _LOG_("[EQPClient] Fail to read Recipe Data", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("Location1.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [Location1.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [Location1.TrayInformation.TrayId]");

                        if (MesAvailable() == false)
                        {
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
                            if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessStartResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [Location1.TrayProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessStartResponse [Location1.TrayProcess.ProcessStartResponse:true]");

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

                            // 20230329 msh : TrayId를 List<string> 구조로 변경
                            List<string> trayList = new List<string>
                            {
                                TrayId
                            };

                            parameters.Add(trayList);       // parameters.Add(TrayId);

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
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessStartResponse", false) == false)
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

        private void SequenceTrayLoad(MonitoredItem item)
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

                        if (EQPClient.ReadValueByPath("Location1.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [Location1.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [Location1.TrayInformation.TrayId]");

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

                                if (EQPClient.WriteBasicTrayInfoEQP("Location1.TrayInformation", TrayData.DATA[0]) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [Location1.TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write TrayInforation to [Location1.TrayInformation] : {TrayData.DATA[0].TRAY_ID}");

                                if (EQPClient.WriteBasicCellInfoEQP("Location1.TrackInCellInformation.Cell", TrayCellData.DATA, 0) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [Location1.TrackInCellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                // CellCount도 쓰자
                                if (EQPClient.WriteNodeByPath("Location1.TrackInCellInformation.CellCount", (UInt16)TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write CellCount [Location1.TrackInCellInformation.CellCount:{TrayCellData.DATA.Count}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write Cell Information to [Location1.TrackInCellInformation.Cell] : {TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.DATA.Count}");

                                // FMS에 Write
                                if (FMSClient.WriteBasicTrayInfoFMS("Location1.TrayInformation", "Location1.TrayInformation.CellCount", TrayData.DATA[0], TrayCellData.DATA.Count) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write Tray Info [Location1.TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Tray Information to [Location1.TrayInformation]: [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");
                                if (FMSClient.WriteBasicCellInfoFMS("Location1.TrayInformation.CellInformation", TrayCellData.DATA) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [TrayInformation.TrackInCellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write TrackIn Cell Information to [Location1.TrayInformation.CellInformation]: [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                                DrawDataGridViewWithCellList(TrayCellData.DATA, TrackInCellList_DataGridView);
                            }
                            else
                            {
                                // Tray 정보가 DB에 없는 상황
                                _LOG_($"[EQPClient] No Tray Information in Database [{TrayId}]", ECSLogger.LOG_LEVEL.ERROR);
                                // TODO : Trouble 처리 해야함
                                return;
                            }

                            //
                            // FMSClinet로도 쓰고
                            if (MesAvailable())
                            {
                                // MES 살이있으면 FMS로 TrayLoad 쓰고
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : True]");

                                //20230227 FMS에 요청을 보냈으면 응답을 받는 Timer를 가동함.
                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse("Location1.TrayProcess.TrayLoadResponse");
                            }
                            else
                            {
                                _LOG_("MES_Alive is false", ECSLogger.LOG_LEVEL.INFO);
                                // MES 없으면 바로 EQP로 response 준다.
                                // 20230327 msh : Location1. 추가
                                if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [TrayInformation.TrayLoadResponse : 1]");

                                // MES 안 붙었어도 flow대로...
                                // 20230327 msh : Location1. 추가
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [TrayProcess.TrayLoad : True]");
                            }

                            

                        }
                        else
                        {
                            // EQP OPCUA에 TrayId가 없는 상황
                            _LOG_("[EQPClient] No TrayId on EQPClient [Location1.TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }
                    }
                    else // TrayLoad : Off
                    {
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write [Location1.TrayProcess.TrayLoad : false]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)0) == false)
                        {
                            _LOG_("[FMSClient] Fail to write [Location1.TrayInformation.TrayLoadResponse : 0]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Complete to write [Location1.TrayInformation.TrayLoadResponse : 0]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        // 20230324 msh : TrackInOutCellInfomation 표시
        private void SetTrackOutCellInformation(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                _CellBasicInformation TrackOutCellInfo;
                bool dataClear = false;

                if (eventItem != null)
                {
                    UInt16 nCellCount = (UInt16)eventItem.LastValue.Value;
                    if (nCellCount == 0)
                    {
                        dataClear = true;

                        TrackOutCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation("Location1.TrackOutCellInformation");
                        DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_DataGridView, dataClear);
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
                        case 16:
                            strStatus = "L";
                            break;
                        case 32:
                            strStatus = "F";
                            break;
                        case 64:
                            strStatus = "F2";
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
                        string ErrorLevel;
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

                        //eqpTrouble 호출
                        _jsonEqpTroubleResponse responseEqpTrouble = RESTClientBiz.CallEcsApiEqpTrouble(this.EQPType, this.EQPID, this.UNITID, ErrorNo.ToString(),
                            $"Machine Trouble [{EQPType}:{EQPID}:{UNITID}][{ErrorNo}:{ErrorLevel}]");
                        if (responseEqpTrouble == null)
                        {
                            _LOG_($"Fail to CallEcsApiEqpTrouble, Machine Trouble [{EQPType}:{EQPID}:{UNITID}][{ErrorNo}:{ErrorLevel}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Success to CallEcsApiEqpTrouble, Machine Trouble [{EQPType}:{EQPID}:{UNITID}][{ErrorNo}:{ErrorLevel}]");

                        // FMS OPCUA Serer에 Trouble Write
                        Dictionary<string, object> TroubleDic = new Dictionary<string, object>();
                        TroubleDic.Add("EquipmentStatus.Trouble.ErrorLevel", ErrorLevel);
                        TroubleDic.Add("EquipmentStatus.Trouble.ErrorNo", ErrorNo);
                        if (FMSClient.WriteNodeWithDic(TroubleDic) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Trouble [EquipmentStatus.Trouble] : [{ErrorNo}:{ErrorLevel}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Success to write Trouble [EquipmentStatus.Trouble] : [{ErrorNo}:{ErrorLevel}]");

                    }

                    // FMS OPCUA Serer에도 상태 변경 해줘야 하지.
                    if (FMSClient.WriteNodeByPath("EquipmentStatus.Status", Status) == false)
                    {
                        _LOG_($"[FMSClient] Fail to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[FMSClient] Success to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]");
                }
            }catch(Exception ex)
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
                    // Apro설비는 Mode=0:Control , Mode=1:Mannual
                    if (Mode == 0)
                    {
                        // RestAPI eqpStatus 호출해야 함.
                        _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, "C", null, null, null, null);
                        if (response == null)
                        {
                            _LOG_("Fail to CallEcsApiEqpStatus with Mode='C'", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to CallEcsApiEqpStatus with Mode='C'");
                    }
                    else //if (Mode == 1)
                    {
                        // RestAPI eqpStatus 호출해야 함.
                        _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, "M", null, null, null, null);
                        if (response == null)
                        {
                            _LOG_("Fail to CallEcsApiEqpStatus with Mode='M'", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to CallEcsApiEqpStatus with Mode='M'");
                    }

                    // FMS OPCUA Server에도 Write
                    if (FMSClient.WriteNodeByPath("EquipmentStatus.Status", Mode) == false)
                    {
                        _LOG_($"[FMSClient] Fail to write Status Mode", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[FMSClient] Success to write Status Mode");
                }
            }catch (Exception ex)
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
                        // RestAPI eqpStatus 호출해야 함.
                        _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, "F", null, null, null, null);
                        if (response == null)
                        {
                            _LOG_("Fail to CallEcsApiEqpStatus with Mode='F'", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to CallEcsApiEqpStatus with Mode='F'");
                    }
                }
            }catch(Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetFMSMonitoredItemList()
        {
            //TrayLoadResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.TrayLoadResponse");
            //RequestRecipeResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");
            //ProcessStartResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            //ProcessEndResponse
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
        }

        private void SetEQPMonitoredItemList()
        {
            //Common
            EQPClient.m_monitoredItemList.Add("Common.Alive");
            //EquipmentStatus
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Power");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Mode");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Status");

            //
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorNo");
            EQPClient.m_monitoredItemList.Add("EquipmentStatus.Trouble.ErrorLevel");

            //FmsStatus
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.Status");
            EQPClient.m_monitoredItemList.Add("FmsStatus.Trouble.ErrorNo");

            //EquipmentControl
            EQPClient.m_monitoredItemList.Add("EquipmentControl.Command");
            EQPClient.m_monitoredItemList.Add("EquipmentControl.CommandResponse");

            //TrayInformation
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("Location1.TrayInformation.TrayType");        // 20230323 msh : 추가

            EQPClient.m_monitoredItemList.Add("Location1.TrackInCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.CellCount");
            // for OCV
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.StartFrequency");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.EndFrequency");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.NumberOfPoints");

            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.StartTemp");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.EndTemp");
            EQPClient.m_monitoredItemList.Add("Location1.TrackOutCellInformation.AvgTemp");


            //TrayProcess
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestRecipeResponse");

            //Recipe
            EQPClient.m_monitoredItemList.Add("Location1.Recipe.RecipeId");
            EQPClient.m_monitoredItemList.Add("Location1.Recipe.OperationMode");
        }
    }
}
