using RestClientLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using static OPCUAClient.OPCUAClient;

namespace EQP_INTF.Packing
{
    public partial class Packing : UserControl
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

            ReadPath.Add("PalletLoadLocation.PalletID");
            ReadPath.Add("PalletLoadLocation.EmptyTrayCount");
            ReadPath.Add("PalletLoadLocation.PalletExist");
            ReadPath.Add("PalletLoadLocation.PalletInRequest");
            ReadPath.Add("PalletLoadLocation.PalletInResponse");
            ReadPath.Add("PickLocation.TrayInformation.TrayExist");
            ReadPath.Add("PickLocation.TrayInformation.TrayId");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoad");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoadResponse");
            ReadPath.Add("PickLocation.TrayInformation.TrayType");
            ReadPath.Add("PickLocation.TrayInformation.ProductModel");
            ReadPath.Add("PickLocation.TrayInformation.RouteId");
            ReadPath.Add("PickLocation.TrayInformation.ProcessId");
            ReadPath.Add("PickLocation.TrayInformation.LotId");
            ReadPath.Add("PickLocation.TrayInformation.TrayGrade");
            ReadPath.Add("PickLocation.CellInformation.CellCount");
            ReadPath.Add("PickLocation.ProcessStart");
            ReadPath.Add("PickLocation.ProcessStartResponse");
            ReadPath.Add("PickLocation.ProcessEnd");
            ReadPath.Add("PickLocation.ProcessEndResponse");
            ReadPath.Add("PlaceLocation.PalletInformation.PalletExist");
            ReadPath.Add("PlaceLocation.PalletInformation.PalletId");
            ReadPath.Add("PlaceLocation.PalletInformation.PalletLoad");
            ReadPath.Add("PlaceLocation.PalletInformation.PalletLoadResponse");
            ReadPath.Add("PlaceLocation.PalletInformation.PalletGrade");
            ReadPath.Add("PlaceLocation.PalletInformation.ProductModel");
            ReadPath.Add("PlaceLocation.PalletInformation.RouteId");
            ReadPath.Add("PlaceLocation.PalletInformation.ProcessId");
            ReadPath.Add("PlaceLocation.PalletInformation.LotId");
            ReadPath.Add("PlaceLocation.PalletInformation.TrayCount");
            ReadPath.Add("PlaceLocation.CellInformation.CellCount");
            ReadPath.Add("PlaceLocation.PalletOutRequest");
            ReadPath.Add("PlaceLocation.PalletOutResponse");
            ReadPath.Add("CellTrackIn.CellId");
            ReadPath.Add("CellTrackIn.FromPosition");
            ReadPath.Add("CellTrackIn.CellLoadRequest");
            ReadPath.Add("CellTrackIn.CellLoadResponse");
            ReadPath.Add("CellTrackOut.CellId");
            ReadPath.Add("CellTrackOut.FromPosition");
            ReadPath.Add("CellTrackOut.ToFloor");
            ReadPath.Add("CellTrackOut.ToPosition");
            ReadPath.Add("CellTrackOut.CellLoadComplete");
            ReadPath.Add("CellTrackOut.CellLoadResponse");
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

                else if (browseNodeList[i].browsePath.EndsWith("PalletLoadLocation.PalletID"))
                    SetTextBox(PL_PalletId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PalletLoadLocation.EmptyTrayCount"))
                    SetTextBox(PL_EmptyTrayCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : (String)"0");
                else if (browseNodeList[i].browsePath.EndsWith("PalletLoadLocation.PalletExist"))
                    SetGroupRadio(PL_PalletExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PalletLoadLocation.PalletInRequest"))
                    SetGroupRadio(PL_PalletInRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PalletLoadLocation.PalletInResponse"))
                    SetGroupRadio(PL_PalletInResponse_Radio, (Boolean)nodesReadValue[i].Value);
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
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.TrayGrade"))
                    SetTextBox(Pick_TrayGrade_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.CellInformation.CellCount"))
                    SetTextBox(TrackIn_CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : (String)"0");
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessStart"))
                    SetGroupRadio(Pick_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessStartResponse"))
                    SetGroupRadio(Pick_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessEnd"))
                    SetGroupRadio(Pick_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessEndResponse"))
                    SetGroupRadio(Pick_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.PalletExist"))
                    SetGroupRadio(Place_PalletExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.PalletId"))
                    SetTextBox(Place_PalletId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.PalletLoad"))
                    SetGroupRadio(Place_PalletLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.PalletLoadResponse"))
                    SetGroupRadio(Place_PalletLoadResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.PalletGrade"))
                    SetTextBox(Place_PalletGrade_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.ProductModel"))
                    SetTextBox(Place_Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.RouteId"))
                    SetTextBox(Place_RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.ProcessId"))
                    SetTextBox(Place_ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.LotId"))
                    SetTextBox(Place_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletInformation.TrayCount"))
                    SetTextBox(Place_TrayCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : (String)"0");
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    SetTextBox(TrackOut_CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : (String)"0");
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletOutRequest"))
                    SetGroupRadio(Place_PalletOutRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.PalletOutResponse"))
                    SetGroupRadio(Place_PalletOutResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackIn.CellId"))
                    SetTextBox(TrackIn_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackIn.FromPosition"))
                    SetTextBox(TrackIn_FromPosition_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackIn.CellLoadRequest"))
                    SetGroupRadio(TrackIn_CellLoadRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackIn.CellLoadResponse"))
                    SetGroupRadio(TrackIn_CellLoadResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.CellId"))
                    SetTextBox(TrackOut_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.FromPosition"))
                    SetTextBox(TrackOut_FromPosition_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.ToFloor"))
                    SetTextBox(TrackOut_ToFloor_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.ToPosition"))
                    SetTextBox(TrackOut_ToPosition_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.CellLoadComplete"))
                    SetGroupRadio(TrackOut_CellLoadComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellTrackOut.CellLoadResponse"))
                    SetGroupRadio(TrackOut_CellLoadResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("ManualLocation.CellManualOut.CellId"))
                    SetTextBox(TrackOut_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
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
            _CellBasicInformation TrackOutCellInfo = OPCClient.ReadEQPTrackInOutCellBasicInfomation("PlaceLocation.CellInformation", true);
            DrawDataGridViewWithCellBasicInfo(TrackOutCellInfo, TrackOutCellList_dataGridView);
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
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                    {
                        FMSSequencePickProcessStartResponse(item);
                    }
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                    {
                        FMSSequencePickProcessEndResponse(item);
                    }
                    else if (browsePath.EndsWith("Location2.PalletProcess.PalletLoadResponse"))
                    {
                        FMSSequencePlacePalletLoadResponse(item);
                    }
                    else if (browsePath.EndsWith("Location2.PalletProcess.ProcessEndResponse"))
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
                        //parameter에 object가 5개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 8)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string PalletId = (string)MesResponseWaitItem.parameters[0];
                        string ProductModel = (string)MesResponseWaitItem.parameters[1];
                        string PalletGrade = (string)MesResponseWaitItem.parameters[2];
                        UInt16 CellCount = (UInt16)MesResponseWaitItem.parameters[3];
                        List<PackingCellInfo> PACKING_CELL_LIST = (List<PackingCellInfo>)MesResponseWaitItem.parameters[4];

                        _jsonCellPackingResponse response = RESTClientBiz.CallEcsApiCellPacking(PalletId, ProductModel, PalletGrade, CellCount, PACKING_CELL_LIST);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_("Fail to call CallEcsApiCellPacking", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call CallEcsApiCellPacking");

                        // EQP - PalletOutRequesResponse : true
                        if (EQPClient.WriteNodeByPath("PlaceLocation.PalletOutResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]");

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
                //parameter에 object가 5개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 8)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                string PalletId = (string)MesResponseWaitItem.parameters[0];
                string ProductModel = (string)MesResponseWaitItem.parameters[1];
                string PalletGrade = (string)MesResponseWaitItem.parameters[2];
                UInt16 CellCount = (UInt16)MesResponseWaitItem.parameters[3];
                List<PackingCellInfo> PACKING_CELL_LIST = (List<PackingCellInfo>)MesResponseWaitItem.parameters[4];

                _jsonCellPackingResponse response = RESTClientBiz.CallEcsApiCellPacking(PalletId, ProductModel, PalletGrade, CellCount, PACKING_CELL_LIST);
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_("Fail to call CallEcsApiCellPacking", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call CallEcsApiCellPacking");

                // EQP - PalletOutRequesResponse : true
                if (EQPClient.WriteNodeByPath("PlaceLocation.PalletOutResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePlacePalletLoadResponse(MonitoredItem item)
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
                        if (EQPClient.WriteNodeByPath("PlaceLocation.PalletInformation.PalletLoadResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write PalletLoadResponse [PlaceLocation.PalletInformation.PalletLoadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write PalletLoadResponse [PlaceLocation.PalletInformation.PalletLoadResponse:true]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[FMSClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePlacePalletLoadResponseTimeOut()
        {
            try
            {
                // EQP로 response
                if (EQPClient.WriteNodeByPath("PlaceLocation.PalletInformation.PalletLoadResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write PalletLoadResponse [PlaceLocation.PalletInformation.PalletLoadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write PalletLoadResponse [PlaceLocation.PalletInformation.PalletLoadResponse:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
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
                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessEndResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]");
                    	
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

                // EQP 에 TrayLoadCompleteResponse true로
                if (EQPClient.WriteNodeByPath("PickLocation.ProcessEndResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]");
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
                        //parameter에 object가 1개 있어야함.                
                        if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 1)
                        {
                            _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        
                        string TrayId = (string)MesResponseWaitItem.parameters[0];

                        //01. Rest API 호출해서 데이터처리
                        //http://<server_name>/ecs/processStart , Recipe없음. 첫번째 parameter는 null
                        //20230330 sgh 호출함수 변경
                        string ProcessId;
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.ProcessId", out ProcessId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PickLocation.TrayInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        string targetEQPType = ProcessId.Substring(0, 3);
                        string targetProcessType = ProcessId.Substring(3, 3);
                        int.TryParse(ProcessId.Substring(6), out int targetProcessNo);

                        _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart_NoRecipe(EQPType, EQPID, UNITID, TrayId, targetProcessType, targetProcessNo);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - TRAY_PROCESS_START");

                        //EQPClient의 ProcessStartResponse 켜준다.
                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessStartResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [PickLocation.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.ProcessStartResponse:true]");
                    
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
                //parameter에 object가 1개 있어야함.                
                if (MesResponseWaitItem.parameters == null || MesResponseWaitItem.parameters.Count != 1)
                {
                    _LOG_($"[{this.Name}] Prepared Parameters for TimeOut Error [{MesResponseWaitItem.ResponsePath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }

                string TrayId = (string)MesResponseWaitItem.parameters[0];


                //01. Rest API 호출해서 데이터처리
                //http://<server_name>/ecs/processStart , Recipe없음. 첫번째 parameter는 null
                //20230330 sgh 호출함수 변경
                string ProcessId;
                if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.ProcessId", out ProcessId) == false)
                {
                    _LOG_("[EQPClient] Fail to read [PickLocation.TrayInformation.ProcessId]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                string targetEQPType = ProcessId.Substring(0, 3);
                string targetProcessType = ProcessId.Substring(3, 3);
                int.TryParse(ProcessId.Substring(6), out int targetProcessNo);

                _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart_NoRecipe(EQPType, EQPID, UNITID, TrayId, targetProcessType, targetProcessNo);
                // 성공여부
                if (response.RESPONSE_CODE != "200")
                {
                    _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("Success to call REST API - TRAY_PROCESS_START");

                // ProcessStartResponse
                if (EQPClient.WriteNodeByPath("PickLocation.ProcessStartResponse", true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [PickLocation.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.ProcessStartResponse:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return;
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
                    else if (browsePath.EndsWith("PalletLoadLocation.PalletID"))
                    {
                        UpdateTextBox(PL_PalletId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PalletLoadLocation.EmptyTrayCount"))
                    {
                        UpdateTextBox(PL_EmptyTrayCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PalletLoadLocation.PalletExist"))
                    {
                        UpdateRadioButton(PL_PalletExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("PalletLoadLocation.PalletInRequest"))
                    {
                        UpdateRadioButton(PL_PalletInRequest_Radio, item);
                        // Logic 이 필요할까?
                    }
                    else if (browsePath.EndsWith("PalletLoadLocation.PalletInResponse"))
                    {
                        UpdateRadioButton(PL_PalletInResponse_Radio, item);
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
                        //Logic
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
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.TrayGrade"))
                    {
                        UpdateTextBox(Pick_TrayGrade_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.CellInformation.CellCount"))
                    {
                        UpdateTextBox(TrackIn_CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessStart"))
                    {
                        UpdateRadioButton(Pick_ProcessStart_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePickProcessStart(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessStartResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessStartResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessEnd"))
                    {
                        UpdateRadioButton(Pick_ProcessEnd_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePickProcessEnd(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessEndResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessEndResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.PalletExist"))
                    {
                        UpdateRadioButton(Place_PalletExist_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.PalletId"))
                    {
                        UpdateTextBox(Place_PalletId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.PalletLoad"))
                    {
                        UpdateRadioButton(Place_PalletLoad_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePlacePalletLoad(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.PalletLoadResponse"))
                    {
                        UpdateRadioButton(Place_PalletLoadResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.PalletGrade"))
                    {
                        UpdateTextBox(Place_PalletGrade_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.ProductModel"))
                    {
                        UpdateTextBox(Place_Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.RouteId"))
                    {
                        UpdateTextBox(Place_RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.ProcessId"))
                    {
                        UpdateTextBox(Place_ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.LotId"))
                    {
                        UpdateTextBox(Place_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletInformation.TrayCount"))
                    {
                        UpdateTextBox(Place_TrayCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    {
                        UpdateTextBox(TrackOut_CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletOutRequest"))
                    {
                        UpdateRadioButton(Place_PalletOutRequest_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequencePlacePalletOutRequest(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.PalletOutResponse"))
                    {
                        UpdateRadioButton(Place_PalletOutResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("CellTrackIn.CellId"))
                    {
                        UpdateTextBox(TrackIn_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackIn.FromPosition"))
                    {
                        UpdateTextBox(TrackIn_FromPosition_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackIn.CellLoadRequest"))
                    {
                        UpdateRadioButton(TrackIn_CellLoadRequest_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrackInCellLoadRequest(item);
                    }
                    else if (browsePath.EndsWith("CellTrackIn.CellLoadResponse"))
                    {
                        UpdateRadioButton(TrackIn_CellLoadResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.CellId"))
                    {
                        UpdateTextBox(TrackOut_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.FromPosition"))
                    {
                        UpdateTextBox(TrackOut_FromPosition_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.ToFloor"))
                    {
                        UpdateTextBox(TrackOut_ToFloor_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.ToPosition"))
                    {
                        UpdateTextBox(TrackOut_ToPosition_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.CellLoadComplete"))
                    {
                        UpdateRadioButton(TrackOut_CellLoadComplete_Radio, item);
                        //Logic
                        if (EqpAvailable()) SequenceTrackOutCellLoadComplete(item);
                    }
                    else if (browsePath.EndsWith("CellTrackOut.CellLoadResponse"))
                    {
                        UpdateRadioButton(TrackOut_CellLoadResponse_Radio, item);
                        //logic?
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

                        _CellProcessData CellProcessData = EQPClient.ReadCellManualOutProcessDataWithoutPD("ManualLocation.CellManualOut");
                        if (CellProcessData == null)
                        {
                            _LOG_("[EQPClient] Fail to read ReadCellManualOutProcessDataWithoutPD(ManualLocation.CellManualOut)", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellManualOut Info without ProcessData [{CellId}]");


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

        private void SequenceTrackOutCellLoadComplete(MonitoredItem item)
        {
            // ecs/cellPacking 을 call 해야 한다.
            // CellTrackOut에서  CellId, FromPosition, ToFloor, ToPosition 값을 읽어야 함
            // PlaceLocation에서 PalletId, ProductModel, PalletGrade를 읽어야 함.
            // 아니네.. confirm만 해주면 되네.. PalletOutRequest에서 한꺼번에 처리해주어야 하네...
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
                        UInt16 FromPosition;
                        UInt16 ToFloor;
                        UInt16 ToPosition;
                        if (EQPClient.ReadValueByPath("CellTrackOut.CellId", out CellId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackOut.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("CellTrackOut.FromPosition", out FromPosition) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackOut.FromPosition]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("CellTrackOut.ToFloor", out ToFloor) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackOut.ToFloor]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("CellTrackOut.ToPosition", out ToPosition) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackOut.ToPosition]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackOut : CellId[{CellId}], FromPosition[{FromPosition}], ToFloor[{ToFloor}], ToPosition[{ToPosition}]");

                        
                        // TODO 20230302
                        //뭐 체크할꺼 없나?


                        // Confirm
                        if (EQPClient.WriteNodeByPath("CellTrackOut.CellLoadResponse", true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellLoadResponse on EQP [.CellTrackOut.CellLoadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Response for CellLoadResponse to EQP Sucess [.CellTrackOut.CellLoadResponse:true]");
                    }
                    else
                    {
                        // reset
                        if (EQPClient.WriteNodeByPath("CellTrackOut.CellLoadResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellLoadResponse on EQP [.CellTrackOut.CellLoadResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Clear for CellLoadResponse to EQP Sucess [CellTrackOut.CellLoadResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }



        }

        private void SequenceTrackInCellLoadRequest(MonitoredItem item)
        {
            // CellTrackIn.CellId와 FromPosition 을 읽고 PickLocation.CellInformation.Cell에 있는 정보와 차이가 없으면 confirm
            // 공정시작은 이미 Tray 단위로 처리했다.
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
                        UInt16 FromPosition;
                        String ReqCellId;
                        if (EQPClient.ReadValueByPath("CellTrackIn.FromPosition", out FromPosition) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackIn.FromPosition]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("CellTrackIn.CellId", out ReqCellId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackIn.CellId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellTrackIn FromPosition[{FromPosition}], CellId[{ReqCellId}]");

                        //PickLocation.CellInformation.Cell._xx 에 있는 Cell 정보
                        String CellId;
                        string CellIdPath = $"PickLocation.CellInformation.Cell._{FromPosition-1}.CellId";
                        if (EQPClient.ReadValueByPath(CellIdPath, out CellId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to read [{CellIdPath}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellId [{CellIdPath}],  CellId[{CellId}]");

                        if (ReqCellId == CellId)
                        {
                            // Confirm
                            if (EQPClient.WriteNodeByPath("CellTrackIn.CellLoadResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write CellLoadResponse on EQP [CellTrackIn.CellLoadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write CellLoadResponse to EQP Sucess [CellTrackIn.CellLoadResponse:true]");
                        }
                        else
                        {
                            //Fail
                            _LOG_($"Different CellId : Requested CellId;{ReqCellId}:{FromPosition}, CellInformation.CellId:{CellId}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }

                    }
                    else
                    {
                        if (EQPClient.WriteNodeByPath("CellTrackIn.CellLoadResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write CellLoadResponse on EQP [.CellTrackIn.CellLoadResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write CellLoadResponse to EQP Sucess [CellTrackIn.CellLoadResponse:false]");
                    }

                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }


    

        private void SequencePlacePalletOutRequest(MonitoredItem item)
        {
            // PlaceLocation.CellInformation의 모든 Cell정보를 읽어야함
            // PlaceLocation.PalletInformation에서 PalletId, ProductModel, PalletGrade를 읽어야 함.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bPalletOutRequest = (Boolean)eventItem.LastValue.Value;
                    if (bPalletOutRequest)
                    {
                        // From PlaceLocation.PalletInformation
                        String PalletId;
                        String ProductModel;
                        String PalletGrade;
                        if (EQPClient.ReadValueByPath("PlaceLocation.PalletInformation.PalletId", out PalletId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PlaceLocation.PalletInformation.PalletId]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PlaceLocation.PalletInformation.ProductModel", out ProductModel) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PlaceLocation.PalletInformation.ProductModel]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        if (EQPClient.ReadValueByPath("PlaceLocation.PalletInformation.PalletGrade", out PalletGrade) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [PlaceLocation.PalletInformation.PalletGrade]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read PalletInformation:  PalletId[{PalletId}], ProductModel[{ProductModel}], PalletGrade[{PalletGrade}]");

                        // From PlaceLocation.CellInformation
                        UInt16 CellCount;
                        if (EQPClient.ReadValueByPath("PlaceLocation.CellInformation.CellCount", out CellCount) == false)
                        {
                            _LOG_("[EQPClient] Fail to read [CellTrackOut.FromPosition]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read CellCount on Pallet : {CellCount}");
                        List<PackingCellInfo> PACKING_CELL_LIST = EQPClient.ReadPackingCellList("PlaceLocation.CellInformation");
                        if(PACKING_CELL_LIST == null)
                        {
                            _LOG_("[EQPClient] Failt to Read Packing Cell List [PlaceLocation.CellInformation]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to read PACKING_CELL_LIST [PlaceLocation.CellInformation]");


                        if(MesAvailable())
                        {
                            if (FMSClient.WriteNodeByPath("Location2.PalletProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.PalletProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location2.PalletProcess.ProcessEnd:true]");

                            // 20230302 KJY - MES Response Timer 세팅, para 5개
                            List<object> parameters = new List<object>
                            {
                                PalletId,
                                ProductModel,
                                PalletGrade,
                                CellCount,
                                PACKING_CELL_LIST
                            };

                            WaitMesResponse("Location2.PalletProcess.ProcessEndResponse", parameters);
                        }
                        else
                        {
                            _jsonCellPackingResponse response = RESTClientBiz.CallEcsApiCellPacking(PalletId, ProductModel, PalletGrade, CellCount, PACKING_CELL_LIST);
                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_("Fail to call CallEcsApiCellPacking", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call CallEcsApiCellPacking");

                            // EQP - PalletOutRequesResponse : true
                            if (EQPClient.WriteNodeByPath("PlaceLocation.PalletOutResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write PalletOutResponse [PlaceLocation.PalletOutResponse:true]");

                            // FMS에도 써준다.
                            if (FMSClient.WriteNodeByPath("Location2.PalletProcess.ProcessEnd", true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.PalletProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location2.PalletProcess.ProcessEnd:true]");
                        }

                    }
                    else
                    {
                        // FMS - PalletOutReques :off
                        if (FMSClient.WriteNodeByPath("Location2.PalletProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.PalletProcess.ProcessEnd:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [Location2.PalletProcess.ProcessEnd:false]");

                        // EQP - PalletOutRequesResponse : off
                        if (EQPClient.WriteNodeByPath("PlaceLocation.PalletOutResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write PalletOutResponse [PlaceLocation.PalletOutResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write PalletOutResponse [PlaceLocation.PalletOutResponse:false]");
                    }
                }

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePlacePalletLoad(MonitoredItem item)
        {
            //PalletLoad할때 해당 Pallet의 기본정보는 필요한가? model, grade 같은거... 
            // 일단, 별다른 확인 없이 response 해준다.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayLoad = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoad)
                    {

                        string PalletId = string.Empty;
                        //_jsonDatTrayResponse TrayData;
                        //_jsonDatCellResponse TrayCellData;
                        _LOG_("Start SequencePlacePalletOutRequest:ON");

                        //TrayId를 EQP OPCUA에서 읽는다.
                        if (EQPClient.ReadValueByPath("PlaceLocation.PalletInformation.PalletId", out PalletId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read PalletId [PlaceLocation.PalletInformation.PalletId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read PalletId:{PalletId} from EQP [PlaceLocation.PalletInformation.PalletId]");

                        if (PalletId != null && PalletId.Length > 0)
                        {

                            // CellCount
                            if (EQPClient.WriteNodeByPath("PlaceLocation.CellInformation.CellCount", (UInt16)0) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write CellCount [PlaceLocation.CellInformation.CellCount:0] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Complete to write CellCount to [PlaceLocation.CellInformation.Cell] : {PalletId}:CellCount:0");
                            // FMS도
                            if (FMSClient.WriteNodeByPath("Location2.PalletInformation.CellCount", (UInt16)0) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write CellCount [Location2.PalletInformation.CellCount:0] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write CellCount to [Location2.PalletInformation.CellCount] : {PalletId}:CellCount:0");


                            //모든 cell정보를 Celar 할수 있는 함수가 필요하겠다.
                            Dictionary<string, string> CellItem = new Dictionary<string, string>();
                            CellItem.Add("CellExist", "Boolean");
                            CellItem.Add("CellId", "String");
                            CellItem.Add("LotId", "String");
                            CellItem.Add("CellGrade", "String");
                            CellItem.Add("Floor", "UInt16");
                            CellItem.Add("Position", "UInt16");

                            if(EQPClient.ClearEQPAllCellInformation("PlaceLocation.CellInformation.Cell", CellItem, 150) == false)
                            {
                                _LOG_($"[EQPClient] Fail to clear all Cell Information [PlaceLocation.CellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Complete to clear all Cell Information [PlaceLocation.CellInformation.Cell] on EQP");

                            // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                            _CellBasicInformation TrackInCellInfo = EQPClient.ReadEQPTrackInOutCellBasicInfomation("PlaceLocation.CellInformation", true);
                            DrawDataGridViewWithCellBasicInfo(TrackInCellInfo, TrackInCellList_DataGridView);

                            //FMS의 Cell정보도 Clear
                            if (FMSClient.ClearFMSAllCellInformation("Location2.PalletInformation.CellInformation", CellItem, 150) == false)
                            {
                                _LOG_($"[FMSClient] Fail to clear all Cell Information [Location2.PalletInformation.CellInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to clear all Cell Information [Location2.PalletInformation.CellInformation] on EQP");


                            if (MesAvailable())
                            {
                                if (FMSClient.WriteNodeByPath("Location2.PalletProcess.PalletLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write PalletLoad ON FMSClient [Location2.PalletProcess.PalletLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location2.PalletProcess.PalletLoad : True]");

                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse("Location2.PalletProcess.PalletLoad");

                            }
                            else
                            {
                                // MES 없으면 바로 EQP로 response 준다.
                                if (EQPClient.WriteNodeByPath("PlaceLocation.PalletInformation.PalletLoadResponse", (Boolean)true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write PalletLoadResponse ON EQPClient [PlaceLocation.PalletInformation.PalletLoadResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [PlaceLocation.PalletInformation.PalletLoadResponse:true]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("Location2.PalletProcess.PalletLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write PalletLoad ON FMSClient [Location2.PalletProcess.PalletLoad:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location2.PalletProcess.PalletLoad : True]");
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
                        if (EQPClient.WriteNodeByPath("PlaceLocation.PalletInformation.PalletLoadResponse", (Boolean)false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write PalletLoadResponse ON EQPClient [PlaceLocation.PalletInformation.PalletLoadResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Complete to write [PlaceLocation.PalletInformation.PalletLoadResponse:false]");

                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location2.PalletProcess.PalletLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write PalletLoad ON FMSClient [Location2.PalletProcess.PalletLoad:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location2.PalletProcess.PalletLoad : false]");
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
                                if (EQPClient.WriteNodeByPath("PickLocation.ProcessEndResponse", true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.ProcessEndResponse:true]");

                                // MES 안 붙어있어도 써준다. flow대로
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

                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessEndResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEndResponse [PickLocation.ProcessEndResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEndResponse [PickLocation.ProcessEndResponse:false]");
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
                            //http://<server_name>/ecs/processStart  , Packing은 Recipe가 없으므로 null 보냄
                            _jsonEcsApiTrayProcessStartResponse response = RESTClientBiz.CallEcsApiTrayProcessStart(null, EQPType, EQPID, UNITID, TrayId);
                            if (response.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API [{CRestModulePath.TRAY_PROCESS_START.ToString()}] : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - TRAY_PROCESS_START");

                            // ProcessStartResponse
                            if (EQPClient.WriteNodeByPath("PickLocation.ProcessStartResponse", true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessStartResponse on EQP [PickLocation.ProcessStartResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.ProcessStartResponse:true]");

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
                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessStartResponse", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStartResponse [PickLocation.ProcessStartResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStartResponse [PickLocation.ProcessStartResponse:false]");
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
                                    _LOG_("[EQPClient] Fail to write TrayLoad ON EQPClient [PickLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
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
                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location1.TrayProcess.TrayLoad;false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.TrayLoad : false]");

                        // EQP의 Response clear
                        EQPClient.WriteNodeByPath("PickLocation.TrayInformation.TrayLoadResponse", (UInt16)0);
                        _LOG_($"[EQPClient] Complete to write [TrayInformation.TrayLoadResponse : 0]");
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
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");

            FMSClient.m_monitoredItemList.Add("Location2.PalletProcess.PalletLoadResponse");
            FMSClient.m_monitoredItemList.Add("Location2.PalletProcess.ProcessEndResponse");
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

            EQPClient.m_monitoredItemList.Add("PalletLoadLocation.PalletID");
            EQPClient.m_monitoredItemList.Add("PalletLoadLocation.EmptyTrayCount");
            EQPClient.m_monitoredItemList.Add("PalletLoadLocation.PalletExist");
            EQPClient.m_monitoredItemList.Add("PalletLoadLocation.PalletInRequest");
            EQPClient.m_monitoredItemList.Add("PalletLoadLocation.PalletInResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayGrade");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessStart");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessEndResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.PalletExist");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.PalletId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.PalletLoad");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.PalletLoadResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.PalletGrade");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.LotId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletInformation.TrayCount");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletOutRequest");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.PalletOutResponse");
            EQPClient.m_monitoredItemList.Add("CellTrackIn.CellId");
            EQPClient.m_monitoredItemList.Add("CellTrackIn.FromPosition");
            EQPClient.m_monitoredItemList.Add("CellTrackIn.CellLoadRequest");
            EQPClient.m_monitoredItemList.Add("CellTrackIn.CellLoadResponse");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.CellId");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.FromPosition");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.ToFloor");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.ToPosition");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.CellLoadComplete");
            EQPClient.m_monitoredItemList.Add("CellTrackOut.CellLoadResponse");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellId");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.LotId");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.NGCode");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.NGType");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellManualOutRequest");
            EQPClient.m_monitoredItemList.Add("ManualLocation.CellManualOut.CellManualOutRequestResponse");
        }


    }
}
