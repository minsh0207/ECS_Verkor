using RestClientLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.MicroCurrent
{
    public partial class MIC_TrackInOut : UserControl
    {

        private void InitDisplayWithCurrentValue(OPCUAClient.OPCUAClient OPCClient)
        {

            List<string> ReadPath = new List<string>();


            ReadPath.Add("TrayInformation.TrayId");
            ReadPath.Add("TrayInformation.TrayExist");
            ReadPath.Add("TrayInformation.ProductModel");
            ReadPath.Add("TrayInformation.RouteId");
            ReadPath.Add("TrayInformation.ProcessId");
            ReadPath.Add("TrayInformation.LotId");
            ReadPath.Add("TrayInformation.EmptyTrayFlag");

            ReadPath.Add("TrayInformation.TrayLoad");
            ReadPath.Add("TrayInformation.TrayLoadResponse");
            ReadPath.Add("TrayInformation.TrayLoadComplete");
            ReadPath.Add("TrayInformation.TrayLoadCompleteResponse");

            ReadPath.Add("TrayInformation.TrayUnload");
            ReadPath.Add("TrayInformation.TrayUnloadResponse");
            ReadPath.Add("TrayInformation.TrayUnloadComplete");
            ReadPath.Add("TrayInformation.TrayUnloadCompleteResponse");

            

            List<BrowseNode> browseNodeList;
            List<ReadValueId> readValueIdList;
            List<DataValue> nodesReadValue = OPCClient.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

            if (nodesReadValue == null || nodesReadValue.Count < 1)
                return;
            //--------------------------------
            //foreach(DataValue node in nodesReadValue)
            for (int i = 0; i < nodesReadValue.Count; i++)
            {
                //Common_Alive_Radio
                if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayId"))
                    SetTextBox(TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //Power_Radio
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayExist"))
                    SetGroupRadio(TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                //ProductModel
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.ProductModel"))
                    SetTextBox(Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //RouteId
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.RouteId"))
                    SetTextBox(RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //ProcessId
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.ProcessId"))
                    SetTextBox(ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //LotId
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.LotId"))
                    SetTextBox(LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                //EmptyTrayFlag
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.EmptyTrayFlag"))
                    SetGroupRadio(EmptyTray_Radio, (Boolean)nodesReadValue[i].Value);

                //TrayLoad
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayLoad"))
                    SetGroupRadio(TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayLoadResponse
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                //TrayLoadComplete
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayLoadComplete"))
                    SetGroupRadio(TrayLoadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayLoadCompleteResponse
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayLoadCompleteResponse"))
                    SetGroupRadio(TrayLoadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);

                //TrayUnload
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayUnload"))
                    SetGroupRadio(TrayUnload_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayUnloadResponse
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayUnloadResponse"))
                    SetGroupRadio(TrayUnloadResponse_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayUnloadComplete
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayUnloadComplete"))
                    SetGroupRadio(TrayUnloadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                //TrayUnloadCompleteResponse
                else if (browseNodeList[i].browsePath.EndsWith("TrayInformation.TrayUnloadCompleteResponse"))
                    SetGroupRadio(TrayUnloadCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);

                // CellInformation
                else if (browseNodeList[i].browsePath.EndsWith("TrackInCellInformation.CellCount"))
                {
                    SetTextBox(TrackInCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                }
                else if (browseNodeList[i].browsePath.EndsWith("TrackOutCellInformation.CellCount"))
                {
                    SetTextBox(TrackOutCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                }
            }

            // 20230405 msh : for문 밖으로 빼냄.
            _CellBasicInformation TrackInCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("TrackInCellInformation");
            SetTextBox(TrackInCellCount_TextBox, TrackInCellInfo.CellCount.ToString());
            DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, TrackInCellList_DataGridView);

            _CellBasicInformation TrackOutCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("TrackOutCellInformation");
            SetTextBox(TrackOutCellCount_TextBox, TrackOutCellInfo.CellCount.ToString());
            DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_DataGridView);
        }

        private void SetFMSMonitoredItemList()
        {
            FMSClient.m_monitoredItemList.Add("TrayProcess.TrayLoadResponse");
            FMSClient.m_monitoredItemList.Add("TrayProcess.TrayLoadCompleteResponse");
            FMSClient.m_monitoredItemList.Add("TrayProcess.TrayUnloadResponse");
            FMSClient.m_monitoredItemList.Add("TrayProcess.TrayUnloadCompleteResponse");
        }

        private void SetEQPMonitoredItemList()
        {
            //TrayInformation
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("TrayInformation.EmptyTrayFlag");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayLoadComplete");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayLoadCompleteResponse");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayUnload");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayUnloadResponse");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayUnloadComplete");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayUnloadCompleteResponse");
                                               
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("TrayInformation.TrayType");
            EQPClient.m_monitoredItemList.Add("TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("TrayInformation.LotId");

            //CellCount추가
            EQPClient.m_monitoredItemList.Add("TrackInCellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("TrackOutCellInformation.CellCount");



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
                    // 20230403 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"FMSClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("TrayProcess.TrayLoadResponse"))
                        FMSSequenceTrayLoadResponse(item);
                    else if (browsePath.EndsWith("TrayProcess.TrayLoadCompleteResponse"))
                        FMSSequenceTrayLoadCompleteResponse(item);
                    else if (browsePath.EndsWith("TrayProcess.TrayUnloadResponse"))
                        FMSSequenceTrayUnloadResponse(item);
                    else if (browsePath.EndsWith("TrayProcess.TrayUnloadCompleteResponse"))
                        FMSSequenceTrayUnloadCompleteResponse(item);

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayUnloadCompleteResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayUnloadCompleteResponse = (Boolean)eventItem.LastValue.Value;
                    if (bTrayUnloadCompleteResponse)
                    {
                        //parameter에 object가 7개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 7)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string TrayId = (string)MesResponseWaitItem.parameters[0];
                        string TrayZone = (string)MesResponseWaitItem.parameters[1];
                        _CellBasicInformation TrackOutCellInformation = (_CellBasicInformation)MesResponseWaitItem.parameters[2];
                        string model_id = (string)MesResponseWaitItem.parameters[3];
                        string route_id = (string)MesResponseWaitItem.parameters[4];
                        string lot_id = (string)MesResponseWaitItem.parameters[5];
                        _jsonMasterNextProcessResponse NextProcessInfo = (_jsonMasterNextProcessResponse)MesResponseWaitItem.parameters[6];

                        // 2.25	setTrayInformation
                        _jsonEcsApiSetTrayInformationResponse response = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayZone, TrackOutCellInformation,
                            model_id, route_id, lot_id, NextProcessInfo);
                        if (response == null)
                        {
                            _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{model_id}:{route_id}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{model_id}:{route_id}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");

                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadCompleteResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]");
                    }
                }


            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceTrayUnloadCompleteResponseTimeOut()
        {
            try
            {
                //parameter에 object가 7개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 7)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                string TrayId = (string)MesResponseWaitItem.parameters[0];
                string TrayZone = (string)MesResponseWaitItem.parameters[1];
                _CellBasicInformation TrackOutCellInformation = (_CellBasicInformation)MesResponseWaitItem.parameters[2];
                string model_id = (string)MesResponseWaitItem.parameters[3];
                string route_id = (string)MesResponseWaitItem.parameters[4];
                string lot_id = (string)MesResponseWaitItem.parameters[5];
                _jsonMasterNextProcessResponse NextProcessInfo = (_jsonMasterNextProcessResponse)MesResponseWaitItem.parameters[6];

                // 2.25	setTrayInformation
                _jsonEcsApiSetTrayInformationResponse response = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayZone, TrackOutCellInformation,
                    model_id, route_id, lot_id, NextProcessInfo);
                if (response == null)
                {
                    _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{model_id}:{route_id}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{model_id}:{route_id}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");

                // EQP로 response
                if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadCompleteResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceTrayUnloadResponse(MonitoredItem item)
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
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:true]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayUnloadResponseTimeOut()
        {
            try
            {
                // EQP로 response
                if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadCompleteResponse(MonitoredItem item)
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
                        _LOG_("Success to call REST API - SET_TRAY_EMPTY");

                        // EQP로 response
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadCompleteResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]");
                    
                        MesResponseWaitItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceTrayLoadCompleteResponseTimeOut()
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
                _LOG_("Success to call REST API - SET_TRAY_EMPTY");

                // EQP 에 TrayLoadCompleteResponse true로
                if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadCompleteResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequenceTrayLoadResponse(MonitoredItem item)
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
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadResponse [TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadResponse [TrayInformation.TrayLoadResponse:1]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequenceTrayLoadResponseTimeOut()
        {
            try
            {
                // MES 없으면 바로 EQP로 response 준다.
                if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                }
                _LOG_($"[EQPClient] Complete to write [TrayInformation.TrayLoadResponse : 1]");
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
                    // 20230403 msh : UserData에 정의된 browsePath를 사용한다.
                    string browsePath = item.UserData.ToString();

                    _LOG_($"EQPClient, Invoke Event : {browsePath}:{((DataMonitoredItem)item).LastValue.Value}");

                    if (browsePath.EndsWith("TrayInformation.TrayLoad"))
                    {
                        UpdateRadioButton(TrayLoad_Radio, item);
                        //Logic
                        SequenceTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("TrayInformation.EmptyTrayFlag"))
                    {
                        UpdateRadioButton(EmptyTray_Radio, item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayLoadComplete"))
                    {
                        UpdateRadioButton(TrayLoadComplete_Radio, item);
                        //Logic
                        SequenceTrayLoadComplete(item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayLoadCompleteResponse"))
                    {
                        UpdateRadioButton(TrayLoadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayUnload"))
                    {
                        UpdateRadioButton(TrayUnload_Radio, item);
                        //Logic
                        SequenceTrayUnload(item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayUnloadResponse"))
                    {
                        UpdateRadioButton(TrayUnloadResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayUnloadComplete"))
                    {
                        UpdateRadioButton(TrayUnloadComplete_Radio, item);
                        //Logic
                        SequenceTrayUnloadComplete(item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayUnloadCompleteResponse"))
                    {
                        UpdateRadioButton(TrayUnloadCompleteResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("TrayInformation.TrayExist"))
                        UpdateRadioButton(TrayExist_Radio, item);
                    else if (browsePath.EndsWith("TrayInformation.TrayId"))
                        UpdateTextBox(TrayId_TextBox, item);
                     else if (browsePath.EndsWith("TrayInformation.ProductModel"))
                        UpdateTextBox(Model_TextBox, item);
                    else if (browsePath.EndsWith("TrayInformation.RouteId"))
                        UpdateTextBox(RouteId_TextBox, item);
                    else if (browsePath.EndsWith("TrayInformation.ProcessId"))
                        UpdateTextBox(ProcessId_TextBox, item);
                    else if (browsePath.EndsWith("TrayInformation.LotId"))
                        UpdateTextBox(LotId_TextBox, item);
                    else if (browsePath.EndsWith("TrackInCellInformation.CellCount"))
                        UpdateTextBox(TrackInCellCount_TextBox, item);
                    else if (browsePath.EndsWith("TrackOutCellInformation.CellCount"))
                        UpdateTextBox(TrackOutCellCount_TextBox, item);

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceTrayUnloadComplete(MonitoredItem item)
        {
            // Cell Tray 생성
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    UpdateTrackInOutCellGridView(EQPClient);
                    Boolean bTrayUnloadComplete = (Boolean)eventItem.LastValue.Value;
                    if (bTrayUnloadComplete)
                    {
                        // TrackOutCellInformation 에서 Cell 읽어서 2.15	trayCellInput 호출해야 함.
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [TrayInformation.TrayId]");

                        // TrayID, Model, Route, LotId, NextEqptype, NextProcessType, NextProcessNo, 
                        // 이건 Cell 정보를 보고 가져와야 하지 않을까?
                        _jsonDatTrayResponse TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId, EQPID);
                        if (TrayData.DATA.Count < 1 || TrayData.DATA[0].TRAY_STATUS != "E")
                        {
                            _LOG_($"Tray is not existed in FMS or Tray is NOT Empty [{TrayId}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                        // TrackOutCellInformation 전체를 먼저 read
                        _CellBasicInformation TrackOutCellInformation = EQPClient.ReadBasicCellInformation("TrackOutCellInformation.Cell", "TrackOutCellInformation.CellCount");
                        if (TrackOutCellInformation != null)
                        {
                            if (TrackOutCellInformation.CellCount <= 0)
                            {
                                _LOG_("[EQPClient] Fail to read TrackOutCellInformation, CellCount is 0", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }

                            // 이 정보를 FMS에 써준다.
                            // 첫번째 Cell의 CellID로 REST 호출해서 정보를 받아온다.
                            string firstCellId = string.Empty;
                            for (int i = 0; i < 30; i++)
                            {
                                if (TrackOutCellInformation._CellList[i].CellExist == true)
                                {
                                    firstCellId = TrackOutCellInformation._CellList[i].CellId;
                                    break;
                                }
                            }
                            if (firstCellId.Length <= 0)
                            {
                                // Cell 정보가 하나도 없다는 얘기
                                _LOG_("No Cell Information on [TrackOutCellInformation.Cell]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _jsonDatCellResponse FirstCellInfo = RESTClientBiz.GetRestCellInfoByCellId(firstCellId, EQPID);
                            if (FirstCellInfo == null || FirstCellInfo.DATA.Count < 1)
                            {
                                _LOG_($"GetRestCellInfoByCellId({firstCellId}) failed.", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            // 일단, Tray에 적용할 Model, RouteId, ProcessId, LotId를 결정해서 EQP에 write 하자
                            Dictionary<string, object> writeItems = new Dictionary<string, object>();
                            writeItems.Add("TrayInformation.ProductModel", (String)FirstCellInfo.DATA[0].MODEL_ID);
                            writeItems.Add("TrayInformation.RouteId", (String)FirstCellInfo.DATA[0].ROUTE_ID);
                            writeItems.Add("TrayInformation.LotId", (String)FirstCellInfo.DATA[0].LOT_ID);
                            EQPClient.WriteNodeWithDic(writeItems);

                            // 2.8	masterNextProcess
                            //TODO. 20230206 KJY 아래 API parameter들 확인해야 함.
                            _jsonMasterNextProcessResponse NextProcessInfo = RESTClientBiz.CallEcsApiMasterNextProcess(EQPType, EQPID, UNITID,
                                FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].PROCESS_TYPE, FirstCellInfo.DATA[0].PROCESS_NO);
                            if (NextProcessInfo == null || NextProcessInfo.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call CallEcsApiMasterNextProcess : [{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}:{EQPType}:{FirstCellInfo.DATA[0].PROCESS_TYPE}:{FirstCellInfo.DATA[0].PROCESS_NO}]",
                                    ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"Success to call CallEcsApiMasterNextProcess : [{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");

                            //FMS에 TrackOutCell 정보 다 써야겠지.
                            TrayData.DATA[0].MODEL_ID = FirstCellInfo.DATA[0].MODEL_ID;
                            TrayData.DATA[0].LOT_ID = FirstCellInfo.DATA[0].LOT_ID;
                            TrayData.DATA[0].ROUTE_ID = FirstCellInfo.DATA[0].ROUTE_ID;
                            TrayData.DATA[0].EQP_TYPE = FirstCellInfo.DATA[0].EQP_TYPE;
                            TrayData.DATA[0].PROCESS_TYPE = FirstCellInfo.DATA[0].PROCESS_TYPE;
                            TrayData.DATA[0].PROCESS_NO = FirstCellInfo.DATA[0].PROCESS_NO;
                            TrayData.DATA[0].LOT_ID = FirstCellInfo.DATA[0].LOT_ID;


                            // FMS에 Write
                            if (FMSClient.WriteBasicTrayInfoFMS("TrayInformation", "TrayInformation.TrackOutCellInformation.CellCount", TrayData.DATA[0], TrackOutCellInformation.CellCount, false) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write Tray Info [TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write Tray Information : [{TrayData.DATA[0].TRAY_ID}:TrackOut CellCount:{TrackOutCellInformation.CellCount}]");
                            if (FMSClient.WriteBasicCellInfoFMS("TrayInformation.TrackOutCellInformation.Cell", FirstCellInfo.DATA) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [TrayInformation.TrackInCellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write TrackIn Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{FirstCellInfo.DATA.Count}]");

                            // 20230314 msh : 공Tray처리
                            string virtualTray = $"{UNITID}TRAY";

                            _jsonSetTrayEmptyResponse emptyResponse = RESTClientBiz.CallEcsApiSetTrayEmpty(virtualTray);
                            if (emptyResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.SET_TRAY_EMPTY.ToString()}] : {emptyResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                //TODO : Trouble 처리
                                return;
                            }
                            _LOG_($"Success to call REST API - SET_TRAY_EMPTY [{virtualTray}]");

                            if (MesAvailable())
                            {
                                //MES로 TrayLoadComplete 보냄.
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnloadComplete", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:true]");

                                // 20230224 KJY - MES Timeout 세팅
                                // 전달할 parameter는 TrayId, TrayData.DATA[0].TRAY_ZONE, TrackOutCellInformation,CellInfo.DATA[0].MODEL_ID, CellInfo.DATA[0].ROUTE_ID, CellInfo.DATA[0].LOT_ID, NextProcessInfo
                                List<object> parameters = new List<object>();
                                parameters.Add(TrayId);
                                parameters.Add(TrayData.DATA[0].TRAY_ZONE);
                                parameters.Add(TrackOutCellInformation);
                                parameters.Add(FirstCellInfo.DATA[0].MODEL_ID);
                                parameters.Add(FirstCellInfo.DATA[0].ROUTE_ID);
                                parameters.Add(FirstCellInfo.DATA[0].LOT_ID);
                                parameters.Add(NextProcessInfo);

                                WaitMesResponse("TrayProcess.TrayUnloadCompleteResponse", parameters);

                            }
                            else
                            {
                                // 2.25	setTrayInformation
                                _jsonEcsApiSetTrayInformationResponse response = RESTClientBiz.CallEcsApiSetTrayInformation(EQPType, EQPID, UNITID, TrayId, TrayData.DATA[0].TRAY_ZONE, TrackOutCellInformation,
                                    FirstCellInfo.DATA[0].MODEL_ID, FirstCellInfo.DATA[0].ROUTE_ID, FirstCellInfo.DATA[0].LOT_ID, NextProcessInfo);
                                if (response == null)
                                {
                                    _LOG_($"Fail to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"Success to call CallEcsApiSetTrayInformation : [{TrayId}:{FirstCellInfo.DATA[0].MODEL_ID}:{FirstCellInfo.DATA[0].ROUTE_ID}] NextProcess:[{NextProcessInfo.NEXT_PROCESS.NEXT_EQP_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_TYPE}:{NextProcessInfo.NEXT_PROCESS.NEXT_PROCESS_NO}]");


                                // EQP 에 TrayLoadCompleteResponse true로
                                if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadCompleteResponse", true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:true]");

                                // MES없더라도 flow대로...
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnloadComplete", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:true]");
                            }
                        }
                        else
                        {
                            //EQP에서 TrackOut Cell 정보를 못읽어옴
                            _LOG_("[EQPClient] Fail to read TrackOutCellInformation [TrackOutCellInformation.Cell]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble처리?
                            return;
                        }
                    }
                    else
                    {
                        // FMS의 TrayProcess.TrayLoadComplete 꺼줌
                        if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnloadComplete", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write TrayUnloadComplete [TrayProcess.TrayUnloadComplete:false]");

                        // EQP TrayLoadCompleteResponse 꺼줌.
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadCompleteResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadCompleteResponse [TrayInformation.TrayUnloadCompleteResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceTrayUnload(MonitoredItem item)
        {
            // 현재 Tray가 공Tray가 맞는지 확인
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayUnload = (Boolean)eventItem.LastValue.Value;
                    if (bTrayUnload)
                    {
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [TrayInformation.TrayId]");

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
                            _LOG_($"Tray [{TrayId}] is Empty Tray. Cell Move from Jig to Tray is prepared.");

                            if (MesAvailable())
                            {
                                //FMS로 TrayUnload 보냄
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnload", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayUnload [TrayProcess.TrayUnload:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayUnload [TrayProcess.TrayUnload:true]");

                                //20230224 FMS에 요청을 보냈으면 응답을 받는 Timer를 가동함.
                                // TrayUnload는 별다른 parameter 필요없음.
                                WaitMesResponse("TrayProcess.TrayUnloadResponse");
                            }
                            else
                            {
                                // EQP로 Response
                                if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadResponse", true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayUnloadResponse on EQP [TrayInformation.TrayUnloadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:true]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnload", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayUnload [TrayProcess.TrayUnload:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayUnload [TrayProcess.TrayUnload:true]");
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
                        if (FMSClient.WriteNodeByPath("TrayProcess.TrayUnload", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayUnload [TrayProcess.TrayUnload:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write TrayUnload [TrayProcess.TrayUnload:false]");

                        // EQP Response clear
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayUnloadResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayUnloadResponse on EQP [TrayInformation.TrayUnloadResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayUnloadResponse [TrayInformation.TrayUnloadResponse:false]");

                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceTrayLoadComplete(MonitoredItem item)
        {
            // 공 Tray처리해야 함.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayLoadComplete = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoadComplete)
                    {
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("TrayInformation.TrayId", out TrayId) == false)
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
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoadComplete", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoadComplete [TrayProcess.TrayLoadComplete:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoadComplete [TrayProcess.TrayLoadComplete:true]");

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
                                _LOG_("Success to call REST API - SET_TRAY_EMPTY");

                                // EQP 에 TrayLoadCompleteResponse true로
                                if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadCompleteResponse", true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:true]");

                                // MES 안 붙어있어도 써준다. flow대로
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoadComplete", true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoadComplete [TrayProcess.TrayLoadComplete:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write TrayLoadComplete [TrayProcess.TrayLoadComplete:true]");
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
                        // FMS의 TrayProcess.TrayLoadComplete 꺼줌
                        if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoadComplete", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoadComplete [TrayProcess.TrayLoadComplete:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write TrayLoadComplete [TrayProcess.TrayLoadComplete:false]");

                        // EQP TrayLoadCompleteResponse 꺼줌.
                        if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadCompleteResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayLoadCompleteResponse [TrayInformation.TrayLoadCompleteResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceTrayLoad(MonitoredItem item)
        {
            // Tray와 Cell정보를 조회해서 EQP, FMS 모두 써준다.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                bool isEmptyTray = false;

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
                        if (EQPClient.ReadValueByPath("TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [TrayInformation.TrayId]");

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
                                // 공Tray면 
                                if (TrayData.DATA[0].TRAY_STATUS != "E")
                                {
                                    if (EQPClient.WriteBasicTrayInfoEQP("TrayInformation", TrayData.DATA[0]) == false)
                                    {
                                        _LOG_("[EQPClient] Fail to write TrayInfo [TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }

                                    //EmptyTrayFlag에 false 쓴다.
                                    if (EQPClient.WriteNodeByPath("TrayInformation.EmptyTrayFlag", (Boolean)false) == false)
                                    {
                                        _LOG_("[EQPClient] Fail to write EmptyTrayFlag [TrayInformation.EmptyTrayFlag:false] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    _LOG_($"[EQPClient] Complete to write TrayInforation to [TrayInformation] : {TrayData.DATA[0].TRAY_ID}");
                                    if (EQPClient.WriteBasicCellInfoEQP("TrackInCellInformation.Cell", TrayCellData.DATA, 0) == false)
                                    {
                                        _LOG_("[EQPClient] Fail to write TrayInfo [TrackInCellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    // CellCount도 쓰자
                                    if (EQPClient.WriteNodeByPath("TrackInCellInformation.CellCount", (UInt16)TrayCellData.DATA.Count) == false)
                                    {
                                        _LOG_($"[EQPClient] Fail to write CellCount [TrackInCellInformation.CellCount:{TrayCellData.DATA.Count}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    _LOG_($"[EQPClient] Complete to write Cell Information to [TrackInCellInformation.Cell] : {TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.DATA.Count}");

                                    // FMS에 Write
                                    if (FMSClient.WriteBasicTrayInfoFMS("TrayInformation", "TrayInformation.TrackInCellInformation.CellCount", TrayData.DATA[0], TrayCellData.DATA.Count) == false)
                                    {
                                        _LOG_($"[FMSClient] Fail to write Tray Info [TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    _LOG_($"[FMSClient] Complete to write Tray Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");
                                    if (FMSClient.WriteBasicCellInfoFMS("TrayInformation.TrackInCellInformation.Cell", TrayCellData.DATA) == false)
                                    {
                                        _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [TrayInformation.TrackInCellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    _LOG_($"[FMSClient] Complete to write TrackIn Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                                    // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                                    UpdateTrackInOutCellGridView(EQPClient);
                                }
                                else
                                {
                                    // Empty Tray임
                                    isEmptyTray = true;
                                    if (EQPClient.WriteNodeByPath("TrayInformation.EmptyTrayFlag", (Boolean)isEmptyTray) == false)
                                    {
                                        _LOG_($"[EQPClient] Fail to write TrackIn EmptyTrayFlag [TrayInformation.EmptyTrayFlag:{isEmptyTray}]", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }

                                    _LOG_($"[EQPClient] Tray [{TrayId}] is Empty Tray");
                                }

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
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [TrayProcess.TrayLoad : True]");

                                //20230227 FMS에 요청을 보냈으면 응답을 받는 Timer를 가동함.
                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse("TrayProcess.TrayLoadResponse");
                            }
                            else
                            {
                                // MES 없으면 바로 EQP로 response 준다.
                                if (EQPClient.WriteNodeByPath("TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [TrayInformation.TrayLoadResponse : 1]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoad", (Boolean)true) == false)
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
                            _LOG_("[EQPClient] No TrayId on EQPClient [TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }
                    }
                    else // TrayLoad : Off
                    {
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [TrayProcess.TrayLoad;false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [TrayProcess.TrayLoad : false]");

                        // EQP의 Response clear
                        EQPClient.WriteNodeByPath("TrayInformation.TrayLoadResponse", (UInt16)0);
                        _LOG_($"[EQPClient] Complete to write [TrayInformation.TrayLoadResponse : 0]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void UpdateTrackInOutCellGridView(OPCUAClient.OPCUAClient OPCClient)
        {

            List<string> ReadPath = new List<string>();

            //TrackInCellInformation
            ReadPath.Add("TrackInCellInformation.CellCount");
            // 개별 Cell
            string strPath = string.Empty;
            for (int i = 0; i < 30; i++)
            {
                strPath = $"TrackInCellInformation.Cell._{i}.CellExist";
                ReadPath.Add(strPath);
                strPath = $"TrackInCellInformation.Cell._{i}.CellId";
                ReadPath.Add(strPath);
                strPath = $"TrackInCellInformation.Cell._{i}.LotId";
                ReadPath.Add(strPath);
            }

            //TrackOutCellInformation
            ReadPath.Add("TrackOutCellInformation.CellCount");
            // 개별 Cell
            for (int i = 0; i < 30; i++)
            {
                strPath = $"TrackOutCellInformation.Cell._{i}.CellExist";
                ReadPath.Add(strPath);
                strPath = $"TrackOutCellInformation.Cell._{i}.CellId";
                ReadPath.Add(strPath);
                strPath = $"TrackOutCellInformation.Cell._{i}.LotId";
                ReadPath.Add(strPath);
            }

            List<BrowseNode> browseNodeList;
            List<ReadValueId> readValueIdList;
            List<DataValue> nodesReadValue = OPCClient.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

            if (nodesReadValue == null || nodesReadValue.Count < 1)
                return;
            _CellBasicInformation TrackInCellInformation = new _CellBasicInformation();
            TrackInCellInformation.InitList();
            _CellBasicInformation TrackOutCellInformation = new _CellBasicInformation();
            TrackOutCellInformation.InitList();

            for (int i = 0; i < nodesReadValue.Count; i++)
            {
                if (browseNodeList[i].browsePath.EndsWith("TrackInCellInformation.CellCount"))
                {
                    SetTextBox(TrackInCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                    TrackInCellInformation.CellCount = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                }
                else if (browseNodeList[i].browsePath.EndsWith("TrackOutCellInformation.CellCount"))
                {
                    SetTextBox(TrackOutCellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                    TrackOutCellInformation.CellCount = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                }

                // TrackInCellInformation
                for (int j = 0; j < 30; j++)
                {
                    strPath = $"TrackInCellInformation.Cell._{j}.CellExist";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackInCellInformation._CellList[j].CellExist = (Boolean)nodesReadValue[i].Value;
                    strPath = $"TrackInCellInformation.Cell._{j}.CellId";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackInCellInformation._CellList[j].CellId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                    strPath = $"TrackInCellInformation.Cell._{j}.LotId";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackInCellInformation._CellList[j].LotId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                }

                // TrackOutCellInformation
                for (int j = 0; j < 30; j++)
                {
                    strPath = $"TrackOutCellInformation.Cell._{j}.CellExist";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackOutCellInformation._CellList[j].CellExist = (Boolean)nodesReadValue[i].Value;
                    strPath = $"TrackOutCellInformation.Cell._{j}.CellId";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackOutCellInformation._CellList[j].CellId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                    strPath = $"TrackOutCellInformation.Cell._{j}.LotId";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        TrackOutCellInformation._CellList[j].LotId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                }
            }

            DrawDataGridView(TrackInCellInformation, "TrackInCellList");
            DrawDataGridView(TrackOutCellInformation, "TrackOutCellList");

        }
        private void DrawDataGridView(_CellBasicInformation CellInformation, string CellPosition)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => DrawDataGridView(CellInformation, CellPosition)));
                else
                {

                    DataGridView targetView = null;
                    if (CellPosition == "TrackInCellList")
                        targetView = TrackInCellList_DataGridView;
                    else
                        targetView = TrackOutCellList_DataGridView;

                    targetView.Columns.Clear();
                    //targetView.Rows.Clear();
                    targetView.Refresh();

                    //Header 
                    DataTable CellInitData = new DataTable();
                    CellInitData.Columns.Add("Cell No", typeof(int));
                    CellInitData.Columns.Add("Cell ID", typeof(string));
                    CellInitData.Columns.Add("Lot ID", typeof(string));

                    for (int i = 0; i < CellInformation._CellList.Count; i++)
                    {
                        DataRow row = CellInitData.NewRow();
                        row["Cell No"] = i + 1;
                        row["Cell ID"] = CellInformation._CellList[i].CellId != null ? CellInformation._CellList[i].CellId : String.Empty;
                        row["Lot ID"] = CellInformation._CellList[i].LotId != null ? CellInformation._CellList[i].LotId : String.Empty;

                        CellInitData.Rows.Add(row);
                    }
                    targetView.DataSource = CellInitData;
                    targetView.Columns[0].Width = 60;
                    targetView.Columns[1].Width = 150;
                    targetView.Columns[2].Width = 90;

                    targetView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[0].ReadOnly = true;
                    targetView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[1].ReadOnly = true;
                    targetView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    targetView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    targetView.Columns[2].ReadOnly = true;

                    targetView.Columns[0].ReadOnly = true;
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
            //if (Tray.TRAY_STATUS == "E")
            //{
            //    _LOG_($"Tray [{Tray.TRAY_ID}] is Empty", ECSLogger.LOG_LEVEL.ERROR);
            //    return false;
            //}
            //else 
            if (Tray.TRAY_STATUS == "D")
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
    }
}
