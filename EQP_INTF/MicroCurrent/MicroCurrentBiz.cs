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

namespace EQP_INTF.MicroCurrent
{
    public partial class MicroCurrent : UserControl
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
            ReadPath.Add("FmsStatus.Trouble.Status");
            ReadPath.Add("FmsStatus.Trouble.ErrorNo");
            ReadPath.Add("EquipmentControl.Command");
            ReadPath.Add("EquipmentControl.CommandResponse");
            ReadPath.Add("CellInformation.CellCount");
            ReadPath.Add("JigInformation.TrackInTrayId");
            ReadPath.Add("JigInformation.ProductModel");
            ReadPath.Add("JigInformation.RouteId");
            ReadPath.Add("JigInformation.ProcessId");
            ReadPath.Add("JigInformation.LotId");
            ReadPath.Add("JigInformation.JigName");
            ReadPath.Add("JigProcess.ProcessStart");
            ReadPath.Add("JigProcess.ProcessStartResponse");
            ReadPath.Add("JigProcess.ProcessEnd");
            ReadPath.Add("JigProcess.ProcessEndResponse");
            ReadPath.Add("JigProcess.RequestRecipe");
            ReadPath.Add("JigProcess.RequestRecipeResponse");
            ReadPath.Add("Recipe.RecipeId");
            ReadPath.Add("Recipe.OperationMode");

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
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.Status"))
                    SetGroupRadio(FMS_Status_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("FmsStatus.Trouble.ErrorNo"))
                    SetTextBox(FMS_ErrorNo_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.Command"))
                    SetTextBox(Command_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("EquipmentControl.CommandResponse"))
                    SetGroupRadio(CommandResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellInformation.CellCount"))
                    SetTextBox(CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.TrackInTrayId"))
                    SetTextBox(TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.ProductModel"))
                    SetTextBox(Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.RouteId"))
                    SetTextBox(RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.ProcessId"))
                    SetTextBox(ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.LotId"))
                    SetTextBox(LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("JigInformation.JigName"))
                    JigName_Label.Text = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.ProcessStart"))
                    SetGroupRadio(ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.ProcessStartResponse"))
                    SetGroupRadio(ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.ProcessEnd"))
                    SetGroupRadio(ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.ProcessEndResponse"))
                    SetGroupRadio(ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.RequestRecipe"))
                    SetGroupRadio(RequestRecipe_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("JigProcess.RequestRecipeResponse"))
                    SetGroupRadio(RequestRecipeResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("Recipe.RecipeId"))
                    SetTextBox(RecipeId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("Recipe.OperationMode"))
                    SetTextBox(OperationMode_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
            }


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
                    // 20230405 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"FMSClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("Jig1.JigProcess.RequestRecipeResponse"))
                        FMSSequenceRequestRecipeResponse(item);
                    else if (browsePath.EndsWith("Jig1.JigProcess.ProcessStartResponse"))
                        FMSSequenceProcessStartResponse(item);
                    else if (browsePath.EndsWith("Jig1.JigProcess.ProcessEndResponse"))
                        FMSSequenceProcessEndResponse(item);

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
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bProcessEndResponse = (Boolean)eventItem.LastValue.Value;

                    if (bProcessEndResponse)
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
                        //_next_process NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                        // FMS에서 다음공정 정보 읽어와야 함.
                        _next_process NEXT_PROCESS = FMSClient.ReadNextProcess("Jig1.JigProcess.NextDestination");

                        if (NEXT_PROCESS == null) NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[6];

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
                        if (EQPClient.WriteNodeByPath("JigProcess.ProcessEndResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                if (EQPClient.WriteNodeByPath("JigProcess.ProcessEndResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]");


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
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_PROCESS_START");

                        //EQPClient의 ProcessStartResponse 켜준다.
                        if (EQPClient.WriteNodeByPath("JigProcess.ProcessStartResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [JigProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [JigProcess.ProcessStartResponse:true]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_START");

                // ProcessStartResponse
                if (EQPClient.WriteNodeByPath("JigProcess.ProcessStartResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [JigProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessStartResponse [JigProcess.ProcessStartResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceRequestRecipeResponse(MonitoredItem item)
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
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromMES = FMSClient.ReadRecipeMES("Jig1.Recipe");
                        if (RecipeDataFromMES == null)
                        {
                            _LOG_("[FMSClient] Fail to read Recipe [Recipe]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to read Recipe from MES [Recipe]");

                        // MES not-available이면 MaterRecipe를 설비에 Write
                        if (EQPClient.WriteRecipeEQP("Recipe", RecipeDataFromMES, OPCUAClient.MappingDirection.FmsToEqp) == false)
                        {
                            _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeDataFromMES.RECIPE_DATA.RECIPE_ID}]");

                        // EQP의 RequestRecipeResponse : true
                        if (EQPClient.WriteNodeByPath("JigProcess.RequestRecipeResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write RequestRecipeResponse [JigProcess.RequestRecipeResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write RequestRecipeResponse [JigProcess.RequestRecipeResponse:true]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
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

                // 01. REST API 호출해서 Master Recipe 받아옴.
                _jsonEcsApiMasterRecipeResponse RecipeResponse = RESTClientBiz.CallEcsApiMasterRecipe(model_id, route_id, targetEQPType, targetProcessType, targetProcessNo);
                if (RecipeResponse == null)
                {
                    _LOG_($"Fail to get Master Recipe by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"Scuccess to Get Recipy by FMS REST API, Model:{model_id}, RouteId;{route_id}, EqpType:{targetEQPType}, ProcessType:{targetProcessType}, ProcessNo:{targetProcessNo}");

                // MES not-available이면 MaterRecipe를 설비에 Write
                if (EQPClient.WriteRecipeEQP("Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                {
                    _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                //04. RequestRecipeResponse : ON
                if (EQPClient.WriteNodeByPath("JigProcess.RequestRecipeResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [JigProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [JigProcess.RequestRecipeResponse:true]");

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
                    // 20230405 msh : UserData에 정의된 browsePath를 사용한다.
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
                    else if (browsePath.EndsWith("CellInformation.CellCount"))
                    {
                        UpdateTextBox(CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigInformation.TrackInTrayId"))
                    {
                        UpdateTextBox(TrayId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigInformation.ProductModel"))
                    {
                        UpdateTextBox(Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigInformation.RouteId"))
                    {
                        UpdateTextBox(RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigInformation.ProcessId"))
                    {
                        UpdateTextBox(ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigInformation.LotId"))
                    {
                        UpdateTextBox(LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("JigProcess.ProcessStart"))
                    {
                        UpdateRadioButton(ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessStart(item);
                    }
                    else if (browsePath.EndsWith("JigProcess.ProcessStartResponse"))
                    {
                        UpdateRadioButton(ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("JigProcess.ProcessEnd"))
                    {
                        UpdateRadioButton(ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("JigProcess.ProcessEndResponse"))
                    {
                        UpdateRadioButton(ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("JigProcess.RequestRecipe"))
                    {
                        UpdateRadioButton(RequestRecipe_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceRequestRecipe(item);
                    }
                    else if (browsePath.EndsWith("JigProcess.RequestRecipeResponse"))
                    {
                        UpdateRadioButton(RequestRecipeResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("Recipe.RecipeId"))
                    {
                        UpdateTextBox(RecipeId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("Recipe.OperationMode"))
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
                string ProcessId;
                string targetEQPType;
                string targetProcessType;
                string targetProcessNo;

                if (eventItem != null)
                {
                    Boolean bRequestRecipe = (Boolean)eventItem.LastValue.Value;

                    if (bRequestRecipe)
                    {
                        if (EQPClient.ReadValueByPath("JigInformation.ProcessId", out ProcessId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [JigInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("JigInformation.RouteId", out route_id) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [JigInformation.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("JigInformation.ProductModel", out model_id) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [JigInformation.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
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
                            if (EQPClient.WriteRecipeEQP("Recipe", RecipeResponse, OPCUAClient.MappingDirection.FmsToEqp) == false)
                            {
                                _LOG_("[EQPClient] Fail to write Recipe to EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Write Recipe to EQP Sucess [RecipeId:{RecipeResponse.RECIPE_DATA.RECIPE_ID}]");

                            //04. RequestRecipeResponse : ON
                            if (EQPClient.WriteNodeByPath("JigProcess.RequestRecipeResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write RequestRecipeResponse on EQP [JigProcess.RequestRecipeResponse]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Response for ProcessStart to EQP Sucess [JigProcess.RequestRecipeResponse:true]");

                            // FMS Server에도 그냥 써준다. flow 대로
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [JigProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [JigProcess.RequestRecipe:true]");
                        }
                        else
                        {   // MES가 가동중일때는 FMS 의 requestRecipe만 켜준다.
                            // FMS에 recipe 써줘야 하나?
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.RequestRecipe", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe on FMS [JigProcess.RequestRecipe]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Write RequestRecipe to FMS Sucess [JigProcess.RequestRecipe:true]");

                            // 20230224 KJY - MES Timeout 세팅
                            // 전달할 parameter model_id, route_id, targetEQPType, targetProcessType, targetProcessNo
                            List<object> parameters = new List<object>();
                            parameters.Add(model_id);
                            parameters.Add(route_id);
                            parameters.Add(targetEQPType);
                            parameters.Add(targetProcessType);
                            parameters.Add(targetProcessNo);

                            WaitMesResponse("JigProcess.RequestRecipeResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Jig1.JigProcess.RequestRecipe", (Boolean)false) == false)
                        {
                            _LOG_($"[FMSClient] Fail to write [JigProcess.RequestRecipe : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [JigProcess.RequestRecipe : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath("JigProcess.RequestRecipeResponse", (Boolean)false) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write [JigProcess.RequestRecipeResponse : false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Complete to write [JigProcess.RequestRecipeResponse : false]");
                    }
                }

            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
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
                        //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("JigInformation", "Recipe");
                        // MIC 면 Jig 에 대한 가상 TrayID를 사용한다.
                        // UnitID가 MIC0110101 면 MIC0110101TRAY
                        List<string> TrayList = new List<string>();
                        TrayList.Add($"{UNITID}TRAY");

                        // CellExist, CellId, LotId, NGCode, NGType 이외에는 모두 Prcoess Data
                        //_CellInformation CellInfo = EQPClient.ReadProcessDataEQP("CellInformation", "JigInformation");
                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 가져온다.
                        _CellInformation CellInfo = EQPClient.ReadProcessDataEQP("CellInformation");

                        // 다음공정정보 미리 받아두기.
                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 가져온다.
                        _next_process NEXT_PROCESS = GetMasterNextProcessInfo(out string processId);
                        if (NEXT_PROCESS == null)
                        {
                            _LOG_("Fail to GetMasterNextProcessInfo()", ECSLogger.LOG_LEVEL.WARN);
                        }

                        // 20230324 msh : ReadProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 할당 해준다.
                        CellInfo.ProcessId = processId;

                        //03. 결과 데이터를 MES로 Write한다.
                        // 이때 timestamp 도 함께 옮겨야함 주의
                        // FMS OPCUA Server에 Write
                        if (FMSClient.WriteProcessDataFMS(CellInfo, "Jig1.ProcessData") == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_("[FMSClient] Success to write Process Data");

                        if (MesAvailable())
                        {
                            //04-1 ProcessEnd를 MES로 보내고 대기
                            // FMSClient에 ProcessEnd
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [JigProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [JigProcess.ProcessEnd:true]");

                            // 20230224 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            //parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(TrayList);
                            parameters.Add(CellInfo);
                            parameters.Add(EQPType);
                            parameters.Add(EQPID);
                            parameters.Add(UNITID);
                            parameters.Add(NEXT_PROCESS);

                            WaitMesResponse("JigProcess.ProcessEndResponse", parameters);

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
                            if (EQPClient.WriteNodeByPath("JigProcess.ProcessEndResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEndResponse [JigProcess.ProcessEndResponse:true]");

                            // FMSClient에 ProcessEnd - MES 안 붙어 있어도 flow 대로..
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [JigProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [JigProcess.ProcessEnd:true]");
                        }


                    }
                    else
                    {
                        // FMS - ProcessEnd :off
                        if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [JigProcess.ProcessEnd]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [JigProcess.ProcessEnd:false]");
                        // EQP - ProcessEndResponse : off

                        if (EQPClient.WriteNodeByPath("JigProcess.ProcessEndResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [JigProcess.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [JigProcess.ProcessEndResponse:false]");
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
                EQPClient.ReadValueByPath("JigInformation.ProductModel", out model_id);
                EQPClient.ReadValueByPath("JigInformation.RouteId", out route_id);
                EQPClient.ReadValueByPath("JigInformation.ProcessId", out ProcessId);
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
                        _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("JigInformation", "Recipe", true);
                        if (RecipeDataFromOPCUA == null)
                        {
                            _LOG_("[EQPClient] Fail to read Recipe Data [Recipe]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to read Recipe Data [Recipe]");

                        // MIC 면 Jig 에 대한 가상 TrayID를 사용한다.
                        // UnitID가 MIC0110101 면 MIC0110101TRAY
                        List<string> TrayList = new List<string>();
                        TrayList.Add($"{UNITID}TRAY");

                        // EQP로 부터 Cell List를 받아야 한다.
                        _CellBasicInformation CellList = EQPClient.ReadBasicCellInformation("CellInformation.Cell", "CellInformation.CellCount");

                        if (CellList.CellCount > 0)
                        {
                            string firstCellId = CellList._CellList[0].CellId;
                            _jsonDatCellResponse CellInfo = RESTClientBiz.GetRestCellInfoByCellId(firstCellId, EQPID);
                            if (CellInfo == null)
                            {
                                _LOG_($"[RESTClientBiz] Fail to call GetRestCellInfoByCellId", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[RESTClientBiz] Success to call GetRestCellInfoByCellId with first CellId [{firstCellId}]");

                            //Model/Route/ProcessType,ProcessNo
                            // Model 과 Route는 EPQ에서
                            // process Type하고 processNo는 RecipeID읽어서 parsing해야 겠다.
                            // 아니다 다 RecipeID로 부터
                            string ModelId;
                            string RouteId;
                            string ProcessType;
                            int ProcessNo;

                            string RecipeId;
                            EQPClient.ReadValueByPath("Recipe.RecipeId", out RecipeId);
                            string[] RecipeInfo = RecipeId.Split('_');
                            ModelId = RecipeInfo[0];
                            RouteId = RecipeInfo[1];
                            ProcessType = RecipeInfo[2].Substring(3, 3);
                            ProcessNo = int.Parse(RecipeInfo[2].Substring(6));

                            _jsonMasterNextProcessResponse NextProcessInfo = new _jsonMasterNextProcessResponse();
                            NextProcessInfo.NEXT_PROCESS = new _NextProcess();
                            NextProcessInfo.NEXT_PROCESS.NEXT_ROUTE_ORDER_NO = 0;
                            NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE = EQPType;
                            NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE = ProcessType;
                            NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO = ProcessNo;

                            // MIC는 JIG에서 가상 Tray로 binding부터 해주어야 한다.
                            _jsonEcsApiSetTrayInformationResponse SetTrayInformationResponse = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayList[0], "AD", CellList,
                               CellInfo.DATA[0].MODEL_ID, CellInfo.DATA[0].ROUTE_ID, CellInfo.DATA[0].LOT_ID, NextProcessInfo);
                            if (SetTrayInformationResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call CallEcsApiSetTrayInformation : {SetTrayInformationResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call SetTrayInformationResponse");
                            // TODO 20230226 KJY - 여기서 2.19	trayCellInput 호출하는것으로 바꾸어야 하나?
                        }
                        else
                        {
                            _LOG_("[EQPClient] Fail to read Cell Data from EQP, CellCount Error", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        if (MesAvailable() == false)
                        {
                            //01. Rest API 호출해서 데이터처리
                            //http://<server_name>/ecs/processStart
                            _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(RecipeDataFromOPCUA, EQPType, EQPID, UNITID, TrayList);
                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - TRAY_PROCESS_START");

                            // ProcessStartResponse
                            if (EQPClient.WriteNodeByPath("JigProcess.ProcessStartResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [JigProcess.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessStartResponse [JigProcess.ProcessStartResponse:true]");

                            // FMSClient에 ProcessStart - MES가 안 붙어있어도 flow 대로..
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessStart", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe [JigProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessStart [JigProcess.ProcessStart:true]");
                        }
                        else
                        {
                            // FMSClient에 ProcessStart
                            if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessStart", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write RequestRecipe [JigProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessStart [JigProcess.ProcessStart:true]");

                            // 20230224 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(UNITID);
                            parameters.Add(TrayList);

                            WaitMesResponse("JigProcess.ProcessStartResponse", parameters);
                        }
                    }
                    else
                    {
                        // FMS - ProcessStart :off
                        if (FMSClient.WriteNodeByPath("Jig1.JigProcess.ProcessStart", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessStart [JigProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessStart [JigProcess.ProcessStart:false]");
                        // EQP - ProcessStartResponse : off

                        if (EQPClient.WriteNodeByPath("JigProcess.ProcessStartResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [JigProcess.ProcessStartResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [JigProcess.ProcessStartResponse:false]");
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
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SetFMSMonitoredItemList()
        {
            FMSClient.m_monitoredItemList.Add("Jig1.JigProcess.RequestRecipeResponse");
            FMSClient.m_monitoredItemList.Add("Jig1.JigProcess.ProcessStartResponse");
            FMSClient.m_monitoredItemList.Add("Jig1.JigProcess.ProcessEndResponse");
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

            //CellInformation
            EQPClient.m_monitoredItemList.Add("CellInformation.CellCount");

            //JigInformation
            EQPClient.m_monitoredItemList.Add("JigInformation.TrackInTrayId");
            EQPClient.m_monitoredItemList.Add("JigInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("JigInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("JigInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("JigInformation.LotId");
            EQPClient.m_monitoredItemList.Add("JigInformation.JigName");

            //JigProcess
            EQPClient.m_monitoredItemList.Add("JigProcess.ProcessStart");
            EQPClient.m_monitoredItemList.Add("JigProcess.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("JigProcess.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("JigProcess.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("JigProcess.RequestRecipe");
            EQPClient.m_monitoredItemList.Add("JigProcess.RequestRecipeResponse");


            //Recipe
            EQPClient.m_monitoredItemList.Add("Recipe.RecipeId");
            EQPClient.m_monitoredItemList.Add("Recipe.OperationMode");

        }
    }
}
