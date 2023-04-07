using CommonCtrls;
using Newtonsoft.Json;
using RestClientLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.Charger
{
    public partial class Charger : UserControl
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

        #region Update Display
        private void UpdatePower(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if ((Boolean)eventItem.LastValue.Value)
                {
                    _LOG_("EQPClient, Power off->on");
                    Power_Radio.IndexChecked = 1;
                }
                else
                {
                    _LOG_("EQPClient, Power on->off");
                    Power_Radio.IndexChecked = 0;
                }



                // FMS에 write
            }
        }

        private void UpdateMode(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                // FMS로 write
                WriteValue nodeToWrite = new WriteValue();
                BrowseNode bnode = FMSClient.m_browseNodeList.FindTargetNodeByPath("EquipmentStatus.Mode");
                nodeToWrite.NodeId = bnode.nodeId;
                nodeToWrite.AttributeId = Attributes.Value;
                nodeToWrite.Value = new DataValue();
                nodeToWrite.Value.Value = eventItem.LastValue.Value;
                nodeToWrite.UserData = bnode.browsePath;

                FMSClient.WriteNode(nodeToWrite);

                Mode_Radio.IndexChecked = eventItem.LastValue.Value != null ? (UInt16)eventItem.LastValue.Value:0 ;

            }

        }

        private void UpdateStatus(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                // FMS로 write
                WriteValue nodeToWrite = new WriteValue();
                BrowseNode bnode = FMSClient.m_browseNodeList.FindTargetNodeByPath("EquipmentStatus.Status");
                nodeToWrite.NodeId = bnode.nodeId;
                nodeToWrite.AttributeId = Attributes.Value;
                nodeToWrite.Value = new DataValue();
                nodeToWrite.Value.Value = eventItem.LastValue.Value;
                nodeToWrite.UserData = bnode.browsePath;

                FMSClient.WriteNode(nodeToWrite);

                SetTextBox(Status_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);

                // Trouble일 경우에 EQP에서 ErrorCode를 읽는다.
                // EQP Trouble 처리



            }
        }

        private void UpdateTrayExist(MonitoredItem item, string Level)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if (Level == "Level1")
                {
                    Level1TrayExist_Radio.IndexChecked = (Boolean)eventItem.LastValue.Value ? 1 : 0;
                }
                else
                {
                    Level2TrayExist_Radio.IndexChecked = (Boolean)eventItem.LastValue.Value ? 1 : 0;
                }
            }
        }

        private void UpdateCommand(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                SetTextBox(Command_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);
            }

        }
        private void UpdateCommandResponse(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                Boolean bCommandResponse = (Boolean)eventItem.LastValue.Value;
                if (bCommandResponse == true)
                {
                    ResetTextBox(Command_TextBox);
                    //Log command success
                }
                else
                {
                    //Log Command 처리 Error
                }
                SetGroupRadio(CommandResponse_Radio, eventItem.LastValue.Value!=null?(Boolean)eventItem.LastValue.Value:false);

            }
        }
        private void UpdateRecipeId(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                SetTextBox(RecipeId_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);
            }
        }

        private void UpdateOperationMode(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                SetTextBox(OperationMode_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);
            }
        }

        private void UpdateTrayId(MonitoredItem item, string Level)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if (Level == "Level1")
                    SetTextBox(Level1TrayId_TextBox, eventItem.LastValue.Value !=null ?eventItem.LastValue.Value.ToString(): String.Empty);
                else
                    SetTextBox(Level2TrayId_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);

            }
        }

        private void UpdateCellCount(MonitoredItem item, string Level)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if (Level == "Level1")
                    SetTextBox(Level1CellCount_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);
                else
                    SetTextBox(Level2CellCount_TextBox, eventItem.LastValue.Value != null ? eventItem.LastValue.Value.ToString() : String.Empty);

            }
        } 
        #endregion

        private void SequenceTrayLoad(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                Boolean bTrayLoad = (Boolean)eventItem.LastValue.Value;
                //SetGroupRadio(TrayLoad_Radio, (Boolean)eventItem.LastValue.Value);

                if (bTrayLoad) // TrayLoad : ON
                {
                    // TrayId 읽어서.. 이미 update 되었으니 TextBox에서 읽어도 되겠네.. 없으면 다시 읽어보고
                    string TrayId1 = string.Empty;
                    string TrayId2 = string.Empty;
                    UInt16 TrayCount =0;

                    _jsonDatTrayResponse Tray1Data;
                    _jsonDatTrayResponse Tray2Data;
                    _jsonDatCellResponse Tray1CellData;
                    _jsonDatCellResponse Tray2CellData;

                    UInt16 CellCountL1 = 0;
                    UInt16 CellCountL2 = 0;

                    _LOG_("Start SequenceTrayLoad:ON");

                    //OPCUA Server에 값이 없고.. DB의 EQP 테이블에만 값이 있을수 있음. 그러면 TrayLoad 요청시 DB에서 TrayID를 읽어줘야 함.
                    _mst_eqp EqpInfo = RESTClientBiz.GetRestEqpInfoByUnitId(UNITID);
                    if (EqpInfo != null && EqpInfo.TRAY_ID != null && EqpInfo.TRAY_ID.Length > 0)
                    {
                        TrayId1 = EqpInfo.TRAY_ID;
                        TrayCount = 1;
                        if (EqpInfo.TRAY_ID_2 != null && EqpInfo.TRAY_ID_2.Length > 0)
                        {
                            TrayId2 = EqpInfo.TRAY_ID_2;
                            TrayCount = 2;
                        }
                        _LOG_($"Read Tray ID from EQP information [EQP_ID:{EQPID}, UNIT_ID:{UNITID}, Tray1:{TrayId1}, Tray2:{TrayId2}, TrayCount:{TrayCount}]");
                    }
                    else
                    {
                        // tb_mst_eqp 에 Tray 정보가 들어있지 않음.
                        _LOG_($"No Tray Information at EQP_ID[{EQPID}]:UNIT_ID[{UNITID}]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }

                    // REST 불러서 Tray 정보 가져오고, REST 불러서 Cell 정보 가져오고, EQPClient로 쓰고                    
                    if (TrayId1 != null && TrayId1.Length > 0)
                    {
                        Tray1Data = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId1, UNITID);

                        Tray1CellData = RESTClientBiz.GetRestCellInfoByTrayId(TrayId1, UNITID);
                        CellCountL1 = (UInt16)Tray1CellData.DATA.Count;

                        if (Tray1Data != null && Tray1Data.DATA.Count > 0)
                        {
                            // 이 Tray가 올바른 Tray인지 여부에 대한 Check 필요함
                            
                            if(CheckTrayValidation(Tray1Data.DATA[0], Tray1CellData) == false)
                            {
                                // 들어와서는 안된는 Tray가 들어왔다.
                                return;
                            }

                            if (EQPClient.WriteBasicTrayInfoEQP("Location1.TrayInformation.Level1", Tray1Data.DATA[0]) == false)
                            {
                                _LOG_("[EQPClient] Fail to write TrayInfo [TrayInformation.Level1] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Complete to write TrayInforation to [TrayInformation.Level1] : {Tray1Data.DATA[0].TRAY_ID}");
                            if (EQPClient.WriteBasicCellInfoEQP("Location1.TrackInCellInformation.Cell", Tray1CellData.DATA, 0) == false)
                            {
                                _LOG_("[EQPClient] Fail to write TrayInfo [TrackInCellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Complete to write Cell Information to [TrackInCellInformation.Cell] : {Tray1Data.DATA[0].TRAY_ID}:CellCount:{Tray1CellData.DATA.Count}");
                            if (FMSClient.WriteBasicTrayCellInfoFMS("Location1.TrayInformation", Tray1Data.DATA[0], Tray1CellData.DATA, "Level1", 0) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write Tray/Cell Info [Location1.TrayInformation, Level1] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write Tray/Cell Information at Level 1 :: [{Tray1Data.DATA[0].TRAY_ID}:CellCount:{Tray1CellData.ROW_COUNT}]");
                            
                        }
                        if (TrayId2 != null && TrayId2.Length > 0)
                        {
                            Tray2Data = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId2, UNITID);
                            Tray2CellData = RESTClientBiz.GetRestCellInfoByTrayId(TrayId2, UNITID);
                            CellCountL2 = (UInt16)Tray2CellData.DATA.Count;

                            if (CheckTrayValidation(Tray2Data.DATA[0], Tray2CellData) == false)
                            {
                                // 들어와서는 안된는 Tray가 들어왔다.
                                _LOG_($"Tray validation check Faile : {TrayId1}, {TrayId2}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }

                            if (Tray2Data != null && Tray2Data.DATA.Count > 0)
                            {
                                if (EQPClient.WriteBasicTrayInfoEQP("Location1.TrayInformation.Level2", Tray2Data.DATA[0])==false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [TrayInformation.Level2]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write TrayInforation to [TrayInformation.Level2] : {Tray2Data.DATA[0].TRAY_ID}");
                                if (EQPClient.WriteBasicCellInfoEQP("Location1.TrackInCellInformation.Cell", Tray2CellData.DATA, 30) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayInfo [TrackInCellInformation.Cell]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Complete to write Cell Information to [TrackInCellInformation.Cell] : {Tray2Data.DATA[0].TRAY_ID}:CellCount:{Tray2CellData.DATA.Count}");
                                if (FMSClient.WriteBasicTrayCellInfoFMS("Location1.TrayInformation", Tray2Data.DATA[0], Tray2CellData.DATA, "Level2", 30) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write Tray/Cell Info [Location1.TrayInformation, Level2] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                            }


                            _LOG_($"[FMSClient] Complete to write Tray/Cell Information at Level 2 :: [{Tray2Data.DATA[0].TRAY_ID}:CellCount:{Tray2CellData.ROW_COUNT}]", ECSLogger.LOG_LEVEL.ALL);

                        }

                        //20230309 KJY - REST API - TRAY_ARRIVED 호출해야함
                        List<string> TrayIds = new List<string>();
                        TrayIds.Add(TrayId1);
                        if (TrayId2 != null && TrayId2.Length > 0) TrayIds.Add(TrayId2);
                        _jsonTrayArrivedResponse TrayArrivedResponse = RESTClientBiz.CallEcsApiTrayArrived(EQPType, EQPID, UNITID, TrayIds.Count, TrayIds);
                        if (TrayArrivedResponse.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to CallEcsApiTrayArrived. {EQPType}:{EQPID}:{UNITID}:{TrayIds.Count}:{TrayId1}:{TrayId2}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Success to CallEcsApiTrayArrived. {EQPType}:{EQPID}:{UNITID}:{TrayIds.Count}:{TrayId1}:{TrayId2}");

                        //TrayInformation.TrayCount / TrackInCellInformation.Level1.CellCount / TrackInCellInformation.Level2.CellCount / 
                        if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayCount", (UInt16)TrayCount) == false)
                            _LOG_($"[EQPClient] Fail to write TrayCount[TrayCount:{TrayCount}]", ECSLogger.LOG_LEVEL.ERROR);
                        if(EQPClient.WriteNodeByPath("Location1.TrackInCellInformation.Level1.CellCount", (UInt16)CellCountL1) ==false)
                            _LOG_($"[EQPClient] Fail to write TrayCount[TrackInCellInformation.Level1.CellCount:{CellCountL1}]", ECSLogger.LOG_LEVEL.ERROR);
                        if (EQPClient.WriteNodeByPath("Location1.TrackInCellInformation.Level2.CellCount", (UInt16)CellCountL2) == false)
                            _LOG_($"[EQPClient] Fail to write TrayCount[TrackInCellInformation.Level2.CellCount:{CellCountL2}]", ECSLogger.LOG_LEVEL.ERROR);

                        // FMS에도 TrayCount만 넣으면 된다.
                        FMSClient.WriteNodeByPath("Location1.TrayInformation.TrayCount", TrayCount);
                    }

                    // FMSClinet로도 쓰고
                    if (MesAvailable())
                    {
                        // MES 살이있으면 FMS에만 TrayLoad 쓰고
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : True]");

                        //20230222 FMS에 요청을 보냈으면 응답을 받는 Timer를 가동함.
                        // TrayLoad는 별다른 parameter 필요없음.
                        WaitMesResponse("Location1.TrayProcess.TrayLoadResponse");
                    }
                    else
                    {
                        // MES 없으면 바로 EQP로 response 준다.
                        if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1)==false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [Location1.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Complete to write [Location1.TrayInformation.TrayLoadResponse : 1]");

                        // MES가 붙어 있지 않아도 flow는 동일하게 간다.
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)true) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : True]");
                    }
                }
                else // TrayLoad : Off
                {
                    // FMS 에도 OFF
                    if(FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)false)==false)
                    {
                        _LOG_("[FMSClient] Fail to write [Location1.TrayProcess.TrayLoad : false]", ECSLogger.LOG_LEVEL.ERROR);
                    }
                    _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : false]");

                    // EQP의 Response clear
                    if(EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)0)==false)
                    {
                        _LOG_("[FMSClient] Fail to write [Location1.TrayInformation.TrayLoadResponse : 0]", ECSLogger.LOG_LEVEL.ERROR);
                    }
                    _LOG_($"[EQPClient] Complete to write [Location1.TrayInformation.TrayLoadResponse : 0]");
                }
            }
        }

        private bool CheckTrayValidation(_dat_tray Tray, _jsonDatCellResponse CellList)
        {
            // Cell Tray가 맞는지
            if (Tray.TRAY_STATUS == "E")
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] is Empty", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            } else if(Tray.TRAY_STATUS == "D")
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] is deleted", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            // 다음공정이 여기 맞는지.
            if(Tray.NEXT_EQP_TYPE != this.EQPType)
            {
                _LOG_($"Tray [{Tray.TRAY_ID}] NEXT_EQP_TYPE[{Tray.NEXT_EQP_TYPE}] is NOT [{this.EQPType}]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            // Cell 갯수 한개 이상
            if(CellList.DATA.Count < 1)
            {
                _LOG_($"Cell count in Tray [{Tray.TRAY_ID}] shoule be more than one", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            // 이전공정 종료 정보가 없으면?
            // 이건 고민좀 해보자.. 이건 확인 안해도 될것 같음. 그냥 새로 추가하는 식으로 처리

            return true;
        }


        private void SequenceRequestRecipe(MonitoredItem item)
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

                    if (Check2TrayCoidentity(out model_id, out route_id, out ProcessId) == false)
                    {
                        return;
                    }
                    _LOG_($"Sucess to check Coindentity of 2 Tray, Model:{model_id}, RouteID:{route_id}, ProcessId:{ProcessId}");
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
                    if (FMSClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipe", (Boolean)false)==false)
                    {
                        _LOG_($"[FMSClient] Fail to write [Location1.TrayProcess.RequestRecipe : false]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.RequestRecipe : false]");
                    
                    // EQP의 Response clear
                    if(EQPClient.WriteNodeByPath("Location1.TrayProcess.RequestRecipeResponse", (Boolean)false)==false)
                    {
                        _LOG_($"[EQPClient] Fail to write [Location1.TrayProcess.RequestRecipeResponse : false]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_($"[EQPClient] Complete to write [Location1.TrayProcess.RequestRecipeResponse : false]");
                }
            }

        }

        private bool Check2TrayCoidentity(out string model_id, out string route_id, out string processId)
        {
            model_id = string.Empty;
            route_id = string.Empty;
            processId = string.Empty;
            // TrayCount
            UInt16 TrayCount = 0;
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.TrayCount", out TrayCount) == false)
            {
                _LOG_("Read Fail from [Location1.TrayInformation.TrayCount]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if(TrayCount <1)
            {
                _LOG_($"TrayCount[{TrayCount}] at [Location1.TrayInformation.TrayCount] is below 1 ", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            // ProcessId 읽어옴
            string ProcessId_Tray1 = string.Empty;
            string ProcessId_Tray2 = string.Empty;
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.ProcessId", out ProcessId_Tray1) == false)
            {
                _LOG_("Read Fail from [Location1.TrayInformation.Level1.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if (TrayCount > 1)
            {
                if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level2.ProcessId", out ProcessId_Tray2) == false)
                {
                    _LOG_("Read Fail from [Location1.TrayInformation.Level2.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                if (ProcessId_Tray1 != ProcessId_Tray2)
                {
                    _LOG_($"ProcessId from Level [{ProcessId_Tray1}] and Level2 [{ProcessId_Tray2}] are different", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
            }
            //RouteID
            string RouteId_Tray1 = string.Empty;
            string RouteId_Tray2 = string.Empty;
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.RouteId", out RouteId_Tray1) == false)
            {
                _LOG_("Read Fail from [Location1.TrayInformation.Level1.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if (TrayCount > 1)
            {
                if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level2.RouteId", out RouteId_Tray2) == false)
                {
                    _LOG_("Read Fail from [Location1.TrayInformation.Level2.RouteId]", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                if (RouteId_Tray1 != RouteId_Tray2)
                {
                    _LOG_($"RouteId from Level [{RouteId_Tray1}] and Level2 [{RouteId_Tray2}] are different", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
            }
            //ProductModel
            string ProductModel_Tray1 = string.Empty;
            string ProductModel_Tray2 = string.Empty;
            if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.ProductModel", out ProductModel_Tray1) == false)
            {
                _LOG_("Read Fail from [Location1.TrayInformation.Level1.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            if (TrayCount > 1)
            {
                if (EQPClient.ReadValueByPath("Location1.TrayInformation.Level2.ProductModel", out ProductModel_Tray2) == false)
                {
                    _LOG_("Read Fail from [Location1.TrayInformation.Level2.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                if (ProductModel_Tray1 != ProductModel_Tray2)
                {
                    _LOG_($"ProductModel from Level [{ProductModel_Tray1}] and Level2 [{ProductModel_Tray2}] are different", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
            }

            if(ProductModel_Tray1.Length<1 || RouteId_Tray1.Length <1 || ProcessId_Tray1.Length<1)
            {
                _LOG_($"More than one of Tray Data is incorrect. ProductModel:[{ProductModel_Tray1}], RouteId:[{RouteId_Tray1}], ProcessId:[{ProcessId_Tray1}]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            model_id = ProductModel_Tray1;
            route_id = RouteId_Tray1;
            processId = ProcessId_Tray1;

            return true;
        }

        private void SequenceProcessStart(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            if (eventItem != null)
            {
                Boolean bProcessStart = (Boolean)eventItem.LastValue.Value;

                if (bProcessStart)
                {
                    _jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("Location1.TrayInformation.Level1","Location1.Recipe", true);
                    if(RecipeDataFromOPCUA == null)
                    {
                        _LOG_("[EQPClient] Fail to read Recipe Data", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }

                    // Tray 정보
                    List<string> TrayList = EQPClient.ReadTrayList("Location1.TrayInformation");
                    if(TrayList == null)
                    {
                        _LOG_("[EQPClient] Fail to read Tray IDs", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }

                    if (MesAvailable() == false)
                    {
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
                        parameters.Add(TrayList);

                        WaitMesResponse("Location1.TrayProcess.ProcessStartResponse", parameters);
                    }
                } else
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
        private void SequenceProcessEnd(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            try
            {

                if (eventItem != null)
                {
                    Boolean bProcessEnd = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEnd)
                    {
                        //01. EQP OPCUA Server에서 공정 데이터를 받아옴.
                        // EQP OPCUA에서 공정정보를 받아 와야 하는데...
                        // 중요한건 processData의 source_timestemp가 FMS OPCUA Server에 동일하게 쓰여야 한다는 점이다.

                        // 일단 공정진행한 Recipe 확보
                        //_jsonEcsApiMasterRecipeResponse RecipeDataFromOPCUA = EQPClient.ReadRecipeEQP("Location1.TrayInformation.Level1", "Location1.Recipe");

                        //_LOG_("Read Recipe from EQP");
                        //_LOG_($"{JsonConvert.SerializeObject(RecipeDataFromOPCUA, Formatting.Indented) }");

                        // Tray 정보
                        List<string> TrayList = EQPClient.ReadTrayList("Location1.TrayInformation");
                        // Location1.TrackOutCellInformation
                        // Level1.CellCount , Level2.CellCount, 개별 Cell 정보 
                        // CellExist, CellId, LotId, NGCode, NGType 이외에는 모두 Prcoess Data
                        _CHGTrackOutCellInformation TrackOutCellInfo = EQPClient.Read2LevelProcessDataEQP("Location1.TrackOutCellInformation", "Location1.TrayInformation.Level1");
                        _LOG_("Read Process Data from EQP");
                        _LOG_($"{JsonConvert.SerializeObject(TrackOutCellInfo, Formatting.Indented)}");

                        //03. 결과 데이터를 MES로 Write한다.
                        // 이때 timestamp 도 함께 옮겨야함 주의
                        // FMS OPCUA Server에 Write
                        if (FMSClient.WriteCHGProcessDataFMS(TrackOutCellInfo, "Location1.ProcessData") == false)
                        {
                            _LOG_($"[FMSClient] Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_("[FMSClient] Success to write Process Data");

                        // 다음공정정보 미리 받아두기.
                        // 20230324 msh : Read2LevelProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 가져온다.
                        _next_process NEXT_PROCESS = GetMasterNextProcessInfo(out string processId);
                        if(NEXT_PROCESS == null)
                        {
                            _LOG_("Fail to GetMasterNextProcessInfo()", ECSLogger.LOG_LEVEL.WARN);
                        }

                        // 20230324 msh : Read2LevelProcessDataEQP()함수 안에서 있는 ProcessId를 여기에서 할당 해준다.
                        TrackOutCellInfo.ProcessId = processId;

                        if (MesAvailable())
                        {
                            //04. ProcessEnd를 MES로 보내고 대기
                            // FMSClient에 ProcessEnd
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            // 20230320 msh : ProcessStart -> ProcessEnd 변경 
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]");

                            // 20230223 KJY - MES Response Timer 세팅
                            List<object> parameters = new List<object>();
                            //parameters.Add(RecipeDataFromOPCUA);
                            parameters.Add(TrayList);
                            parameters.Add(TrackOutCellInfo);
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
                            // 다음공정 정보는?
                            _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiCHGTrayProcessEnd(TrayList, TrackOutCellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);

                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - TRAY_PROCESS_END");

							// 20230320 msh : ProcessStart -> ProcessEnd 변경
                            //05. MES not-available이면 ProcessEndResponse EQP로 전달함.
                            // EQPClient - ProcessEndResponse
                            if (EQPClient.WriteNodeByPath("Location1.TrayProcess.ProcessEndResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEndResponse [Location1.TrayProcess.ProcessEndResponse:true]");

                            // MES가 안 붙어 있어요 FMSClient에 ProcessEnd
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
                        // FMS - ProcessEnd :off
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write RequestRecipe [Location1.TrayProcess.ProcessEnd]", ECSLogger.LOG_LEVEL.ERROR);
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
            } catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.ProductModel", out model_id);
                EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.RouteId", out route_id);
                EQPClient.ReadValueByPath("Location1.TrayInformation.Level1.ProcessId", out ProcessId);
                processType = ProcessId.Substring(3, 3);
                process_no = int.Parse(ProcessId.Substring(6));

                outProcessId = ProcessId;           // 20230324 msh : 추가

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

                outProcessId = string.Empty;           // 20230324 msh : 추가

                return null;
            }
        }

        private void SequenceTrayLoadRequest(MonitoredItem item)
        {
            // REST API - trayLoadRequest 호출
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            try
            {
                if (eventItem != null)
                {
                    Boolean bTrayLoadRequest = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoadRequest)
                    {
                        _jsonTrayLoadRequestResponse response = RESTClientBiz.CallEcsApiTrayLoadRequest(EQPType, EQPID, UNITID);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_LOAD_REQUEST.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_LOAD_REQUEST");

                        // EQPClient - TrayLoadRequestResponse
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.TrayLoadRequestResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadRequestResponse [Location1.TrayProcess.TrayLoadRequestResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadRequestResponse [Location1.TrayProcess.TrayLoadRequestResponse:true]");

                    }
                    else
                    {
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.TrayLoadRequestResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadRequestResponse [Location1.TrayProcess.TrayLoadRequestResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadRequestResponse [Location1.TrayProcess.TrayLoadRequestResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }

            //
        }

        private void SequenceTrayUnloadRequest(MonitoredItem item)
        {
            // REST API - trayUnloadRequest 호출
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            try
            {
                if (eventItem != null)
                {
                    Boolean bTrayUnloadRequest = (Boolean)eventItem.LastValue.Value;
                    if (bTrayUnloadRequest)
                    {
                        _jsonTrayUnloadRequestResponse response = RESTClientBiz.CallEcsApiTrayUnloadRequest(EQPType, EQPID, UNITID);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_UNLOAD_REQUEST.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_UNLOAD_REQUEST");

                        // EQPClient - TrayUnloadRequestResponse
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.TrayUnloadRequestResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadRequestResponse [Location1.TrayProcess.TrayUnloadRequestResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadRequestResponse [Location1.TrayProcess.TrayUnloadRequestResponse:true]");

                    }
                    else
                    {
                        if (EQPClient.WriteNodeByPath("Location1.TrayProcess.TrayUnloadRequestResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadRequestResponse [Location1.TrayProcess.TrayUnloadRequestResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadRequestResponse [Location1.TrayProcess.TrayUnloadRequestResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;
                MesResponseWaitItem = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bTrayLoadResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayLoadResponse)
                    {
                        //EQPClient의 TrayLoadResponse를 켜준다.
                        if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]");
                    }
                }
            } catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadResponseTimeOut()
        {
            try
            {
                // parameter 불필요

                // MES에서 TrayLoadResponse 응답이 오지 않으면, 그대로 EQP로 TrayLoadResponse 1을 써준다.
                if (EQPClient.WriteNodeByPath("Location1.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayLoadResponse [Location1.TrayInformation.TrayLoadResponse:1]");
            }catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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

                    if (bRequestRecipeResponse)
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
            } catch (Exception ex)
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
                if(MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 5)
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
                        // 여기에 필요하구나.... 현재 EQP 에 적혀있는 Recipe값들을 불러와서 공정시작 처리를 함.
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

            } catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                        _CHGTrackOutCellInformation TrackOutCellInfo = (_CHGTrackOutCellInformation)MesResponseWaitItem.parameters[1];
                        string EQPType = (string)MesResponseWaitItem.parameters[2];
                        string EQPID = (string)MesResponseWaitItem.parameters[3];
                        string UNITID = (string)MesResponseWaitItem.parameters[4];
                        //_next_process NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                        // FMS에서 다음공정 정보 읽어와야 함.
                        _next_process NEXT_PROCESS = FMSClient.ReadNextProcess("Location1.TrayProcess.NextDestination");

                        if(NEXT_PROCESS == null) NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];

                        _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiCHGTrayProcessEnd(TrayList, TrackOutCellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);

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

                        MesResponseWaitItem = null;
                    }                    
                }
            }
            catch (Exception ex)
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
                _CHGTrackOutCellInformation TrackOutCellInfo = (_CHGTrackOutCellInformation)MesResponseWaitItem.parameters[1];
                string EQPType = (string)MesResponseWaitItem.parameters[2];
                string EQPID = (string)MesResponseWaitItem.parameters[3];
                string UNITID = (string)MesResponseWaitItem.parameters[4];
                _next_process NEXT_PROCESS = (_next_process)MesResponseWaitItem.parameters[5];


                _jsonTrayProcessEndResponse response = RESTClientBiz.CallEcsApiCHGTrayProcessEnd(TrayList, TrackOutCellInfo, EQPType, EQPID, UNITID, NEXT_PROCESS);

                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_END.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_END");


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

        private void SetEQPPower(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            if (eventItem != null)
            {
                Boolean bPower = (Boolean)eventItem.LastValue.Value;

                if (bPower == false)
                {
                    // RestAPI eqpStatus 호출해야 함.
                    _jsonEqpStatusResponse response = RESTClientBiz.CallEcsApiEqpStatus(this.EQPType, this.EQPID, this.UNITID, "F", null, null, null, null);
                    if(response == null)
                    {
                        _LOG_("Fail to CallEcsApiEqpStatus with Mode='F'", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_("Success to CallEcsApiEqpStatus with Mode='F'");
                }
            }
        }

        private void SetEQPMode(MonitoredItem item)
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
                } else //if (Mode == 1)
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

        //SetEqpStatus
        private void SetEqpStatus(MonitoredItem item)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;

            if (eventItem != null)
            {
                UInt16 Status = (UInt16)eventItem.LastValue.Value;

                String strStatus = String.Empty;

                switch(Status)
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
                if(strStatus == "T")
                {
                    string ErrorLevel;
                    if(EQPClient.ReadValueByPath("EquipmentStatus.Trouble.ErrorLevel", out ErrorLevel) == false)
                    {
                        _LOG_("[EQPClient] Fail to read [EquipmentStatus.Trouble.ErrorLevel]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    UInt16 ErrorNo;
                    if(EQPClient.ReadValueByPath("EquipmentStatus.Trouble.ErrorNo", out ErrorNo)==false)
                    {
                        _LOG_("[EQPClient] Fail to read [EquipmentStatus.Trouble.ErrorNo]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }

                    //eqpTrouble 호출
                    _jsonEqpTroubleResponse responseEqpTrouble = RESTClientBiz.CallEcsApiEqpTrouble(this.EQPType, this.EQPID, this.UNITID, ErrorNo.ToString(), 
                        $"Machine Trouble [{EQPType}:{EQPID}:{UNITID}][{ErrorNo}:{ErrorLevel}]");
                    if(responseEqpTrouble == null)
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
                if(FMSClient.WriteNodeByPath("EquipmentStatus.Status", Status) ==false)
                {
                    _LOG_($"[FMSClient] Fail to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[FMSClient] Success to write Status [EquipmentStatus.Status] : [{Status}:{strStatus}]");
            }
        }

        private bool MesAvailable()
        {
            // EquipmentStatus.MesAlive 확인
            // MES가 살아 있는지 여부에 대한 새로운 로직에 따라 추가, 수정
            Boolean MesAlive;

            //FMSClient.ReadValueByPath("EquipmentStatus.MesAlive", out MesAlive);

            if (FMSClient.ReadValueByNodeId(FMSClient.MesAliveNodeId, out MesAlive) == false)
            {
                _LOG_($"[FMSClient] Fail to read MesAlive Node [{FMSClient.MesAliveNodeId}]", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }

            _LOG_($"[FMSClient] MesAlive is [{MesAlive}]", ECSLogger.LOG_LEVEL.INFO);

            return MesAlive;
        }

        private void ResetTextBox(TextBox targetTextBox)
        {
            if (InvokeRequired)
                Invoke(new Action(() => ResetTextBox(targetTextBox)));
            else
                targetTextBox.Text = String.Empty;
        }

        private void SetTextBox(TextBox targetTextBox, string value)
        {
            if (InvokeRequired)
                Invoke(new Action(() => SetTextBox(targetTextBox, value)));
            else
                targetTextBox.Text = value;
        }

        private void SetGroupRadio(GroupRadio groupeRadioButton, Boolean value)
        {
            if (value == true)
                groupeRadioButton.IndexChecked = 1;
            else
                groupeRadioButton.IndexChecked = 0;
        }
        private void SetGroupRadio(GroupRadio groupeRadioButton, int value)
        {
             groupeRadioButton.IndexChecked = value;
        }

        private void UpdateRadioButton(GroupRadio targetGroupRadio, MonitoredItem item, bool isBoolean=true)
        {
            DataMonitoredItem eventItem = item as DataMonitoredItem;
            if (eventItem != null)
            {
                if(isBoolean == false)
                    targetGroupRadio.IndexChecked = (UInt16)eventItem.LastValue.Value;
                else
                {
                    if ((Boolean)eventItem.LastValue.Value == true)
                        targetGroupRadio.IndexChecked = 1;
                    else
                        targetGroupRadio.IndexChecked = 0;

                }
            }
        }

    }
}
