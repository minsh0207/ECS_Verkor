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

namespace EQP_INTF.NGSorter
{
    public partial class NGSorter : UserControl
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
            ReadPath.Add("PickLocation.TrayInformation.TrayExist");
            ReadPath.Add("PickLocation.TrayInformation.TrayId");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoad");
            ReadPath.Add("PickLocation.TrayInformation.TrayLoadResponse");
            ReadPath.Add("PickLocation.TrayInformation.ProcessType");
            ReadPath.Add("PickLocation.TrayInformation.ProductModel");
            ReadPath.Add("PickLocation.TrayInformation.RouteId");
            ReadPath.Add("PickLocation.TrayInformation.ProcessId");
            ReadPath.Add("PickLocation.TrayInformation.LotId");
            ReadPath.Add("PickLocation.CellInformation.CellCount");
            ReadPath.Add("PickLocation.ProcessStart");
            ReadPath.Add("PickLocation.ProcessStartResponse");
            ReadPath.Add("PickLocation.ProcessEnd");
            ReadPath.Add("PickLocation.ProcessEndResponse");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayExist");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayId");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayLoad");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayLoadResponse");
            ReadPath.Add("PlaceLocation.TrayInformation.TrayGrade");
            ReadPath.Add("PlaceLocation.TrayInformation.DefectType");
            ReadPath.Add("PlaceLocation.TrayInformation.ProductModel");
            ReadPath.Add("PlaceLocation.TrayInformation.RouteId");
            ReadPath.Add("PlaceLocation.TrayInformation.ProcessId");
            ReadPath.Add("PlaceLocation.TrayInformation.LotId");
            ReadPath.Add("PlaceLocation.CellInformation.CellCount");
            ReadPath.Add("PlaceLocation.TrayOut");
            ReadPath.Add("CellWork.FromCellPosition");
            ReadPath.Add("CellWork.FromWorkRequest");
            ReadPath.Add("CellWork.FromWorkResponse");
            ReadPath.Add("CellWork.CellId");
            ReadPath.Add("CellWork.ToWorkRequest");
            ReadPath.Add("CellWork.ToCellPosition");
            ReadPath.Add("CellWork.ToWorkResponse");
            ReadPath.Add("CellWork.WorkComplete");
            ReadPath.Add("CellWork.WorkCompleteResponse");

            //20230323 KJY - TrayRequest추가
            ReadPath.Add("PlaceLocation.TrayRequest.Grade");
            ReadPath.Add("PlaceLocation.TrayRequest.DefectType");
            ReadPath.Add("PlaceLocation.TrayRequest.TrayType");
            ReadPath.Add("PlaceLocation.TrayRequest.ProductModel");
            ReadPath.Add("PlaceLocation.TrayRequest.TrayLoadRequest");
            ReadPath.Add("PlaceLocation.TrayRequest.ReservedFlag");
            ReadPath.Add("PlaceLocation.TrayRequest.ReservedTrayId");

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
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.TrayInformation.ProcessType"))
                    SetTextBox(Pick_ProcessType_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
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
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessStart"))
                    SetGroupRadio(Pick_ProcessStart_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessStartResponse"))
                    SetGroupRadio(Pick_ProcessStartResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessEnd"))
                    SetGroupRadio(Pick_ProcessEnd_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PickLocation.ProcessEndResponse"))
                    SetGroupRadio(Pick_ProcessEndResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayExist"))
                    SetGroupRadio(Place_TrayExist_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayId"))
                    SetTextBox(Place_TrayId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoad"))
                    SetGroupRadio(Place_TrayLoad_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoadResponse"))
                    SetGroupRadio(Place_TrayLoadResponse_Radio, (UInt16)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.TrayGrade"))
                    SetTextBox(Place_TrayGrade_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.DefectType"))
                    SetTextBox(Place_DefectType_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.ProductModel"))
                    SetTextBox(Place_Model_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.RouteId"))
                    SetTextBox(Place_RouteId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.ProcessId"))
                    SetTextBox(Place_ProcessId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayInformation.LotId"))
                    SetTextBox(Place_LotId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    SetTextBox(Place_CellCount_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayOut"))
                    SetGroupRadio(Place_TrayOut_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.FromCellPosition"))
                    SetTextBox(CellWork_FromCellPosition_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.FromWorkRequest"))
                    SetGroupRadio(CellWork_FromWorkRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.FromWorkResponse"))
                    SetGroupRadio(CellWork_FromWorkResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.CellId"))
                    SetTextBox(CellWork_CellId_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.ToWorkRequest"))
                    SetGroupRadio(CellWork_ToWorkRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.ToCellPosition"))
                    SetTextBox(CellWork_ToCellPosition_TextBox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.ToWorkResponse"))
                    SetGroupRadio(CellWork_ToWorkResponse_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.WorkComplete"))
                    SetGroupRadio(CellWork_WorkComplete_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("CellWork.WorkCompleteResponse"))
                    SetGroupRadio(CellWork_WorkCompleteResponse_Radio, (Boolean)nodesReadValue[i].Value);

                //20230323 KJY - TrayRequest추가
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.Grade"))
                    SetTextBox(TrayReqest_Grade_Textbox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.DefectType"))
                    SetTextBox(TrayRequest_DefectType_Textbox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.TrayType"))
                    SetTextBox(TrayRequest_TrayType_Textbox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.ProductModel"))
                    SetTextBox(TrayRequest_Model_Textbox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.TrayLoadRequest"))
                    SetGroupRadio(TrayRequest_TrayLoadRequest_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.ReservedFlag"))
                    SetGroupRadio(TrayRequest_ReservedFlag_Radio, (Boolean)nodesReadValue[i].Value);
                else if (browseNodeList[i].browsePath.EndsWith("PlaceLocation.TrayRequest.ReservedTrayId"))
                    SetTextBox(TrayRequest_ReservedTrayId_Textbox, nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty);

            }

            _SorterCellInformation TrackInCellInfo = OPCClient.ReadEQPTrackInOutCellGradeInfomation("PickLocation.CellInformation");
            DrawDataGridViewWithCellGradeInfo(TrackInCellInfo, TrackInCellList_DataGridView);

            _SorterCellInformation TrackOutCellInfo = OPCClient.ReadEQPTrackInOutCellGradeInfomation("PlaceLocation.CellInformation");
            DrawDataGridViewWithCellGradeInfo(TrackOutCellInfo, TrackOutCellList_DataGridView);
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
                        FMSSequencePickTrayLoadResponse(item);
                    //else if (browsePath.EndsWith("Location1.TrayProcess.RequestCellGradeResponse"))
                    //    FMSSequencePickRequestCellGradeResponse(item);
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessStartResponse"))
                        FMSSequencePickProcessStartResponse(item);
                    else if (browsePath.EndsWith("Location1.TrayProcess.ProcessEndResponse"))
                        FMSSequencePickProcessEndResponse(item);
                    //else if (browsePath.EndsWith("Location2.TrayProcess.ProcessStartResponse"))
                    //    FMSSequencePlaceProcessStartResponse(item);
                    else if (browsePath.EndsWith("Location2.TrayProcess.TrayLoadResponse"))
                        FMSSequencePlaceTrayLoadResponse(item);
                    else if (browsePath.EndsWith("Location2.TrayProcess.ProcessEndResponse"))
                        FMSSequencePlaceProcessEndResponse(item);
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
                    Boolean bTrayLoadResponse = (Boolean)eventItem.LastValue.Value;

                    if (bTrayLoadResponse)
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
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }
        private void FMSSequencePlaceTrayLoadResponseTimeOut()
        {
            try
            {
                // MES 없으면 바로 EQP로 response 준다.
                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:1]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePlaceProcessEndResponse(MonitoredItem item)
        {
            if (MesResponseTimer != null) MesResponseTimer.Stop();
            MesResponseTimer = null;
            MesResponseWaitItem = null;

            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessEndResponse = (Boolean)eventItem.LastValue.Value;
                    if(bProcessEndResponse)
                    {
                        // Location2의 ProcessEnd는 별도의 처리를 해주지 않아도됨. 이미 Cell Tray구성되어 있고, Cell별로 공정종료 처리되어 있음.
                        // 단순의 Tray/Cell Binding해서 나가는 것임.
                        // EQP로 TrayOut 보냄
                        //TrayOut
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePlaceProcessEndResponseTimeOut()
        {
            try
            {
                ///TrayOut
                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                {
                    _LOG_("[EQPClient] Fail to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        //private void FMSSequencePlaceProcessStartResponse(MonitoredItem item)
        //{
        //    throw new NotImplementedException();
        //}

        private void FMSSequencePickProcessEndResponse(MonitoredItem item)
        {
            try
            {
                if (MesResponseTimer != null) MesResponseTimer.Stop();
                MesResponseTimer = null;

                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", (Boolean)true) == false)
                    {
                        _LOG_("[EQPClient] Fail to write ProcessEnd, [PickLocation.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                        return;
                    }
                    _LOG_("[EQPClient] Success to write ProcessEnd, [PickLocation.ProcessEnd:true]");
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
                if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", (Boolean)true) == false)
                {
                    _LOG_("[EQPClient] Fail to write ProcessEnd, [PickLocation.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_("[EQPClient] Success to write ProcessEnd, [PickLocation.ProcessEnd:true]");
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void FMSSequencePickProcessStartResponse(MonitoredItem item)
        {
            // MES로 부터 ProcessStartResponse 가 false일때 ProcessStart가 true면 정상 시퀀스로 판단하고 EQP로 ProcessStart요청을 보냄
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
                        // FMS ProcessStart off함
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessStart [Location1.TrayProcess.ProcessStart:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessStart [Location1.TrayProcess.ProcessStart:false]");
                    }
                    else
                    {
                        Boolean bCurrentProcess;
                        if (FMSClient.ReadValueByPath("Location1.TrayProcess.ProcessStart", out bCurrentProcess) == false)
                        {
                            _LOG_("[FMSClient] Fail to Read ProcessStart [Location1.TrayProcess.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[FMSClient] Current Location1.TrayProcess.ProcessStart:{bCurrentProcess}");

                        if (bCurrentProcess == false)
                        {
                            // FMS ProcessStart가 off인 상태이어야 정상적인 시퀀스로 간주한다.
                            //EQPClient의 ProcessStart 켜준다.
                            if (EQPClient.WriteNodeByPath($"PickLocation.ProcessStart", true) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write ProcessStart [PickLocation.ProcessStart:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Success to write ProcessStart [PickLocation.ProcessStart:true]");
                        }
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
                //TimeOut이면 바로 EQP에 ProcessStart 보냄
                if (EQPClient.WriteNodeByPath($"PickLocation.ProcessStart", true) == false)
                {
                    _LOG_($"[EQPClient] Fail to write ProcessStart [PickLocation.ProcessStart:true]", ECSLogger.LOG_LEVEL.ERROR);
                    return;
                }
                _LOG_($"[EQPClient] Success to write ProcessStart [PickLocation.ProcessStart:true]");

            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        //private void FMSSequencePickRequestCellGradeResponse(MonitoredItem item)
        //{
        //    throw new NotImplementedException();
        //}

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
                    else if (browsePath.EndsWith("PickLocation.TrayInformation.ProcessType"))
                    {
                        UpdateTextBox(Pick_ProcessType_TextBox, item);
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
                    else if (browsePath.EndsWith("PickLocation.ProcessStart"))
                    {
                        UpdateRadioButton(Pick_ProcessStart_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessStartResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessStartResponse_Radio, item);
                        if (EqpAvailable()) SequencePickProcessStartResponse(item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessEnd"))
                    {
                        UpdateRadioButton(Pick_ProcessEnd_Radio, item);
                    }
                    else if (browsePath.EndsWith("PickLocation.ProcessEndResponse"))
                    {
                        UpdateRadioButton(Pick_ProcessEndResponse_Radio, item);
                   
                        //20230404 sgh ProcessEndResponse  On을 받으면 ProcessEnd Off가 없음
                        //Logic
                        if (EqpAvailable()) SequencePickProcessEndResponse(item);
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
                        if (EqpAvailable()) SequencePlaceTrayLoad(item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayLoadResponse"))
                    {
                        UpdateRadioButton(Place_TrayLoadResponse_Radio, item, false);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.TrayGrade"))
                    {
                        UpdateTextBox(Place_TrayGrade_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.DefectType"))
                    {
                        UpdateTextBox(Place_DefectType_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.ProductModel"))
                    {
                        UpdateTextBox(Place_Model_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.RouteId"))
                    {
                        UpdateTextBox(Place_RouteId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.ProcessId"))
                    {
                        UpdateTextBox(Place_ProcessId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayInformation.LotId"))
                    {
                        UpdateTextBox(Place_LotId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.CellInformation.CellCount"))
                    {
                        UpdateTextBox(Place_CellCount_TextBox, item);
                    }
                    else if (browsePath.EndsWith("PlaceLocation.TrayOut"))
                    {
                        UpdateRadioButton(Place_TrayOut_Radio, item);
                    }
                    else if (browsePath.EndsWith("CellWork.FromCellPosition"))
                    {
                        UpdateTextBox(CellWork_FromCellPosition_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellWork.FromWorkRequest"))
                    {
                        UpdateRadioButton(CellWork_FromWorkRequest_Radio, item);
                    }
                    else if (browsePath.EndsWith("CellWork.FromWorkResponse"))
                    {
                        UpdateRadioButton(CellWork_FromWorkResponse_Radio, item);
                        if (EqpAvailable()) SequenceFromWorkResponse(item);
                    }
                    else if (browsePath.EndsWith("CellWork.CellId"))
                    {
                        UpdateTextBox(CellWork_CellId_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellWork.ToWorkRequest"))
                    {
                        UpdateRadioButton(CellWork_ToWorkRequest_Radio, item);
                        if (EqpAvailable()) SequenceToWorkRequest(item);
                    }
                    else if (browsePath.EndsWith("CellWork.ToCellPosition"))
                    {
                        UpdateTextBox(CellWork_ToCellPosition_TextBox, item);
                    }
                    else if (browsePath.EndsWith("CellWork.ToWorkResponse"))
                    {
                        UpdateRadioButton(CellWork_ToWorkResponse_Radio, item);
                    }
                    else if (browsePath.EndsWith("CellWork.WorkComplete"))
                    {
                        UpdateRadioButton(CellWork_WorkComplete_Radio, item);
                        if (EqpAvailable()) SequenceWorkComplete(item);
                    }
                    else if (browsePath.EndsWith("CellWork.WorkCompleteResponse"))
                    {
                        UpdateRadioButton(CellWork_WorkCompleteResponse_Radio, item);
                    }
                    // 20230323 KJY - TrayRequest추가
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.Grade"))
                        UpdateTextBox(TrayReqest_Grade_Textbox, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.DefectType"))
                        UpdateTextBox(TrayRequest_DefectType_Textbox, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.TrayType"))
                        UpdateTextBox(TrayRequest_TrayType_Textbox, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.ProductModel"))
                        UpdateTextBox(TrayRequest_Model_Textbox, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.TrayLoadRequest"))
                        UpdateRadioButton(TrayRequest_TrayLoadRequest_Radio, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.ReservedFlag"))
                        UpdateRadioButton(TrayRequest_ReservedFlag_Radio, item);
                    else if (browsePath.EndsWith("PlaceLocation.TrayRequest.ReservedTrayId"))
                        UpdateTextBox(TrayRequest_ReservedTrayId_Textbox, item);
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceWorkComplete(MonitoredItem item)
        {
            // 여기가 중요.
            // PlaceLocation.CellInformation.Cell._xx 에 Cell 데이터 추가
            // PickLocation.CellInformation.Cell._xx 에서 Cell 데이터 삭제
            // CellCount 잘 정리하고.. 

            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bWorkComplete = (Boolean)eventItem.LastValue.Value;
                    if (bWorkComplete)
                    {
                        NGSCellWork CellWork = EQPClient.ReadNGSCellWork("CellWork");

                        // PlaceLocation의 TrayCell정보 읽어옴.
                        bool isPickLocation = false;
                        _SorterTrayCellInformation PlaceTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);

                        // PickLocation의 TrayCell정보 읽어옴.
                        isPickLocation = true;
                        _SorterTrayCellInformation PickTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);


                        // Cell의 공정종료 
                        _jsonCellProcessEndResponse response = RESTClientBiz.CallEcsApiCellProcessEndNGS(PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1], EQPType, EQPID, UNITID);
                        if (response.RESPONSE_CODE != "200")
                        {
                            _LOG_($"Fail to call REST API CallEcsApiCellProcessEndNGS : {response.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("Success to call REST API - CallEcsApiCellProcessEndNGS");

                        // From Cell의 정보를 ToCell에 옮김
                        if (EQPClient.WriteNGSCellFromPickToPlaceEQP(PickTrayCellInfo, PlaceTrayCellInfo, CellWork) == false)
                        {
                            _LOG_("[EQPClient] Fail to write WriteNGSCellFromPickToPlaceEQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write WriteNGSCellFromPickToPlaceEQP");
                        // FMS도 동일하게
                        if (FMSClient.WriteNGSCellFromPickToPlaceFMS(PickTrayCellInfo, PlaceTrayCellInfo, CellWork) == false)
                        {
                            _LOG_("[FMSClient] Fail to write WriteNGSCellFromPickToPlaceFMS", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write WriteNGSCellFromPickToPlaceFMS");

                        // Tray에서 Cell 분리

                        if (PlaceTrayCellInfo.CellCount == 0)
                        {
                            //Tray기본정보를 From Cell정보로 세팅한다.
                            if(EQPClient.WriteNGSPlaceTrayInfoEQP("PlaceLocation.TrayInformation", PickTrayCellInfo, CellWork) == false)
                            {
                                _LOG_("[EQPClient] Fail to write WriteNGSPlaceTrayInfoEQP [PlaceLocation.TrayInformation]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to write WriteNGSPlaceTrayInfoEQP [PlaceLocation.TrayInformation]");
                            //FMS도 동일하게
                            if (FMSClient.WriteNGSPlaceTrayInfoFMS("Location2.TrayInformation", PickTrayCellInfo, CellWork) == false)
                            {
                                _LOG_("[FMSClient] Fail to write WriteNGSPlaceTrayInfoFMS [Location2.TrayInformation]", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[FMSClient] Success to write WriteNGSPlaceTrayInfoFMS [Location2.TrayInformation]");


                            // TrayCell Binding을
                            _jsonEcsApiSetTrayInformationResponse BindingResponse = RESTClientBiz.CallEcsApiSetTrayInformationNGS(PlaceTrayCellInfo, PickTrayCellInfo, CellWork, EQPID);
                            if (BindingResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API CallEcsApiSetTrayInformationNGS : {BindingResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - CallEcsApiSetTrayInformationNGS");

                        } else
                        {
                            // Cell하나를 PlaceLocation의 Tray에 trayCellInput
                            _OutPutCell cell = new _OutPutCell();
                            cell.CELL_POSITION = CellWork.ToCellPosition;
                            cell.CELL_ID = PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1].CellId;
                            cell.LOT_ID = PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1].LotId;
                            _jsonTrayCellInputResponse TrayCellInputResponse = RESTClientBiz.CallEcsApiTrayCellInput(cell, 1, PlaceTrayCellInfo.TrayId, EQPID, UNITID);
                            if (TrayCellInputResponse.RESPONSE_CODE != "200")
                            {
                                _LOG_($"Fail to call REST API CallEcsApiTrayCellInput : {TrayCellInputResponse.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("Success to call REST API - CallEcsApiTrayCellInput");

                        }

                        // From Cell의 정보를 삭제함
                        if (EQPClient.ClearOneCellInfoNGSEQP("PickLocation.CellInformation", PickTrayCellInfo, CellWork) ==false)
                        {
                            _LOG_($"[EQPClient] Fail to call ClearOneCellInfoNGSEQP, PickLocation Cell at [_{CellWork.FromCellPosition-1}]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Success to call ClearOneCellInfoNGSEQP PickLocation Cell at [_{CellWork.FromCellPosition-1}]");
                        // FMS도 동일하게
                        if (FMSClient.ClearOneCellInfoNGSFMS("Location1.CellInformation", PickTrayCellInfo, CellWork) == false)
                        {
                            _LOG_($"[FMSClient] Fail to call ClearOneCellInfoNGSEQP, Location1 Cell at [_{CellWork.FromCellPosition - 1}]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[FMSClient] Success to call ClearOneCellInfoNGSEQP Location1 Cell at [_{CellWork.FromCellPosition - 1}]");

                        

                        //WorkCompleteResponse:true
                        if (EQPClient.WriteNodeByPath("CellWork.WorkCompleteResponse", (Boolean)true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write WorkCompleteResponse [CellWork.WorkCompleteResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write WorkCompleteResponse [CellWork.WorkCompleteResponse:true]");

                    }
                    else
                    {
                        //WorkCompleteResponse:false
                        if (EQPClient.WriteNodeByPath("CellWork.WorkCompleteResponse", (Boolean)false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write WorkCompleteResponse [CellWork.WorkCompleteResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write WorkCompleteResponse [CellWork.WorkCompleteResponse:false]");

                        // 만약에 PlaceLocation의 CellCount가 30이면 MES로는 Location2.TrayProcess.ProcessEnd, EQP로는 PlaceLocation.TrayOut
                        UInt16 CurrentPlaceCellCount = 0;
                        EQPClient.ReadValueByPath("PlaceLocation.CellInformation.CellCount", out CurrentPlaceCellCount);
                        if (CurrentPlaceCellCount == 30)
                        {
                            _LOG_($"[EQPClient] CellCount of PlaceLocation is full, [PlaceLocation.CellInformation.CellCount:{CurrentPlaceCellCount}]");

                            if (MesAvailable())
                            {

                                if (FMSClient.WriteNodeByPath("Location2.TrayProcess.ProcessEnd", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write ProcessEnd [Location2.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[FMSClient] Success to write ProcessEnd [Location2.TrayProcess.ProcessEnd:true]");

                                // Timer 걸어야 함.
                                // Parameter필요없을듯.
                                WaitMesResponse("Location2.TrayProcess.ProcessEndResponse");

                            }
                            else
                            {
                                //TrayOut
                                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");
                            }
                        }

                        // 다음 CellWork.FromWorkRequest 는 어디서 주어야 하나? 여기서 주면 되나?
                        if (CellWorkStart() == false)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }

        }

        private void SequenceToWorkRequest(MonitoredItem item)
        {
            // toCellPosition을 결정해서 알려주어야 함.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bToWorkRequest = (Boolean)eventItem.LastValue.Value;
                    if (bToWorkRequest)
                    {
                        NGSCellWork CellWork = EQPClient.ReadNGSCellWork("CellWork");
                        bool isPickLocation = false;
                        _SorterTrayCellInformation PlaceTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);
                        isPickLocation = true;
                        _SorterTrayCellInformation PickTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);

                        int ToCellPosition = PlaceTrayCellInfo.CellCount + 1;

                        //PlaceLocation하고 넣을Cell하고 정보 맞는지 확인해야 하나?
                        if(PickTrayCellInfo.ProcessType == 1)//NG Sorting
                        {
                            if(PlaceTrayCellInfo.DefectType != PickTrayCellInfo._CellList[CellWork.FromCellPosition-1].DefectType)
                            {
                                _LOG_($"Information Error, Tray in PlaceLocation [DefectType:{PlaceTrayCellInfo.DefectType}] and Cell[{CellWork.FromCellPosition - 1}] DefectType:{PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1].DefectType}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                        } else
                        {
                            //Grading
                            if (PlaceTrayCellInfo.TrayGrade != PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1].NGCode)
                            {
                                _LOG_($"Information Error, Tray in PlaceLocation [TrayGrade:{PlaceTrayCellInfo.TrayGrade}] and Cell[{CellWork.FromCellPosition - 1}] NGCode:{PickTrayCellInfo._CellList[CellWork.FromCellPosition - 1].NGCode}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                        }

                        //ToCellPosition 적어주고 ToWorkResponse true로
                        if(EQPClient.WriteNodeByPath("CellWork.ToCellPosition", (UInt16)ToCellPosition) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write ToCellPosition [CellWork.ToCellPosition:{ToCellPosition}]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to write ToCellPosition [CellWork.ToCellPosition:{ToCellPosition}]");
                        if (EQPClient.WriteNodeByPath("CellWork.ToWorkResponse", (Boolean)true) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ToWorkResponse [CellWork.ToWorkResponse:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ToWorkResponse [CellWork.ToWorkResponse:true]");

                    }
                    else
                    {
                        //CellWork.ToWorkResponse : off 해주면 됨
                        if (EQPClient.WriteNodeByPath("CellWork.ToWorkResponse", (Boolean)false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ToWorkResponse [CellWork.ToWorkResponse:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ToWorkResponse [CellWork.ToWorkResponse:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequenceFromWorkResponse(MonitoredItem item)
        {
            // ToWorkRequest를 보내주어야 함.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bFromWorkResponse = (Boolean)eventItem.LastValue.Value;
                    if (bFromWorkResponse)
                    {
                        //CellWork.FromWorkRequest : off 해주면 됨
                        if (EQPClient.WriteNodeByPath("CellWork.FromWorkRequest", (Boolean)false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write FromWorkRequest [CellWork.FromWorkRequest:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write FromWorkRequest [CellWork.FromWorkRequest:false]");

                    } else
                    {
                        // 여긴 뭐 해줄께 없음.
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
            // Place Location에 Tray도착
            // 등급Tray, ReworkTray, 공Tray가 아니면 bypass해야 함.
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bTrayLoad = (Boolean)eventItem.LastValue.Value;
                    if (bTrayLoad)
                    {
                        //MES가 있으면 MES로 보고하고, 없으면 바로 PickLocation에서 CellWork를 개시할수 있도록 한다.

                        //Tray정보 읽어서 EQP Tag에 써준다.
                        string TrayId = string.Empty;
                        _jsonDatTrayResponse TrayData;
                        _jsonDatCellResponse TrayCellData;
                        UInt16 CellCount = 0;
                        _LOG_("Start SequencePlaceTrayLoad:ON");

                        //TrayId를 EQP OPCUA에서 읽는다.
                        if (EQPClient.ReadValueByPath("PlaceLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PlaceLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PlaceLocation.TrayInformation.TrayId]");

                        if (TrayId == null || TrayId.Length < 1)
                        {
                            // EQP OPCUA에 TrayId가 없는 상황
                            _LOG_("[EQPClient] No TrayId on EQPClient [PlaceLocation.TrayInformation.TrayId]", ECSLogger.LOG_LEVEL.ERROR);
                            //TODO : Trouble 처리
                            return;
                        }
                        else
                        {
                            // 설비로 부터 TrayLoad 요청이 왔고, TrayID도 정상적으로 있는 상황
                            // Tray정보를 조회한다.
                            // Tray/Cell 정보 DB에서 받아온다. REST
                            TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId, EQPID);
                            TrayCellData = RESTClientBiz.GetRestCellInfoByTrayId(TrayId, EQPID);
                            CellCount = (UInt16)TrayCellData.DATA.Count;

                            if (CellCount == 30)
                            {
                                _LOG_($"Tray {TrayData.DATA[0].TRAY_ID} in PlaceLoation is full with Cells, need to be out");
                                // 만Cell임 bypass (TrayOut);
                                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");
                                return;
                            }

                            // PlaceLocation.TrayRequest 있는 값과 비교해보자.
                            TrayRequest PlaceTrayRequest = EQPClient.ReadEQPTrayRequest("PlaceLocation.TrayRequest");

                            // Tray가 예약되어 있는 상태면 TrayID가 동일하면 OK 아니면 TrayOut
                            if (PlaceTrayRequest.ReservedFlag == true)
                            {
                                // 설비로 부터 TrayLoad 요청이 왔고, TrayID도 정상적으로 있고 TrayRequest에 Tray가 예약되어 있는 상황
                                if (TrayData.DATA[0].TRAY_ID != PlaceTrayRequest.ReservedTrayId)
                                {
                                    if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                                    {
                                        _LOG_("[EQPClient] Fail to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                                        return;
                                    }
                                    _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");
                                    return;
                                }
                                else
                                {
                                    //예약되어 있는 Tray가 정상적으로 들어왔다.
                                }

                            }
                            else
                            {
                                // 설비로 부터 TrayLoad 요청이 왔고, TrayID도 정상적으로 있고 TrayRequest에 Tray가 아직 예약되어 있지 않은 상황
                                // 예약된 Tray가 없으면 공 Tray만 가능함.
                                if (TrayData.DATA[0].TRAY_STATUS != "E")
                                {
                                    _LOG_($"Loaded Tray {TrayData.DATA[0].TRAY_ID} is not Reserved Tray or Empty Tray, need to be Bypass", ECSLogger.LOG_LEVEL.WARN);

                                    // 그냥 이상한 Tray가 들어왔네.. 그냥 bypass해야함.
                                    if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)2) == false)
                                    {
                                        _LOG_("[EQPClient] Fail to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:2, bypass]", ECSLogger.LOG_LEVEL.ERROR);
                                    }
                                    _LOG_("[EQPClient] Success to write TrayLoadResponse [PlaceLocation.TrayInformation.TrayLoadResponse:2, bypass]");
                                    return;
                                }
                                else
                                {
                                    // 예약이 되어 있지 않지만 공 Tray가 잘 들어왔다.
                                }

                            }

                            // 예약된 Tray 또는 공 Tray(예약이 없을 경우)가 잘 들어왔다.
                            //TrayReqquest를 초기화 해야 한다.
                            if (EQPClient.WriteNGSTrayRequest("PlaceLocation.TrayRequest", string.Empty, string.Empty, 0, string.Empty, false, false, string.Empty) == false)
                            {
                                _LOG_("[EQPClient] Fail to clear PlaceLocation.TrayRequest", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_("[EQPClient] Success to clear PlaceLocation.TrayRequest");


                            //EQP Tag에 정보 쓰자
                            if (EQPClient.WriteTrayInfoWithGradeEQP("PlaceLocation.TrayInformation", TrayData.DATA[0]) == false)
                            {
                                _LOG_("[EQPClient] Fail to write TrayInfo [PlaceLocation.TrayInformation] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            if (EQPClient.WriteCellInfoWidhNGCodeEQP("PlaceLocation.CellInformation.Cell", TrayCellData.DATA, 0) == false)
                            {
                                _LOG_("[EQPClient] Fail to write Cell Info with NGCode [PlaceLocation.CellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            // CellCount도 쓰자
                            if (EQPClient.WriteNodeByPath("PlaceLocation.CellInformation.CellCount", (UInt16)TrayCellData.DATA.Count) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write CellCount [PlaceLocation.CellInformation.CellCount:{TrayCellData.DATA.Count}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[EQPClient] Complete to write Cell Information to [PlaceLocation.CellInformation.Cell] : {TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.DATA.Count}");

                            // FMS에도 써주어야 한다.
                            // FMS에 Write
                            if (FMSClient.WriteBasicTrayInfoFMS("Location2.TrayInformation", "Location2.TrayInformation.CellCount", TrayData.DATA[0], TrayCellData.DATA.Count) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write Tray Info [Location2.TrayInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write Tray Information on Location2 : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");
                            if (FMSClient.WriteBasicCellInfoFMS("Location2.TrayInformation.CellInformation", TrayCellData.DATA) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [Location2.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            // NGCode정보도 써주어야 한다. PlaceLocation의 Cell은 모두 FMS가 write해야함.
                            if (FMSClient.WriteCellInfoNGCodeFMS("Location2.TrayInformation.CellGrade", TrayCellData.DATA) == false)
                            {
                                _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [Location2.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            _LOG_($"[FMSClient] Complete to write Cell Information on Location2 : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                            // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                            _SorterCellInformation TrackOutCellInfo = EQPClient.ReadEQPTrackInOutCellGradeInfomation("PlaceLocation.CellInformation");
                            DrawDataGridViewWithCellGradeInfo(TrackOutCellInfo, TrackOutCellList_DataGridView);

                            // FMS로 TrayLoad 요청해야지.
                            if (MesAvailable())
                            {
                                if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location2.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location2.TrayProcess.TrayLoad : True]");

                                // TrayLoad에는 parameter 필요없음
                                WaitMesResponse("Location2.TrayProcess.TrayLoadResponse");

                            }
                            else
                            {
                                // MES 없으면 바로 EQP로 response 준다.
                                if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)1) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadResponse ON EQPClient [PlaceLocation.TrayInformation.TrayLoadResponse:1]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [PlaceLocation.TrayInformation.TrayLoadResponse : 1]");

                                // MES 안 붙었어도 flow대로...
                                if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write TrayLoad ON FMSClient [Location2.TrayProcess.TrayLoad]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location2.TrayProcess.TrayLoad : True]");

                            }

                        }
                    }
                    else
                    {
                        //PlaceLocation.TrayLoad off

                        // FMS 에도 OFF
                        if (FMSClient.WriteNodeByPath("Location2.TrayProcess.TrayLoad", (Boolean)false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write [Location2.TrayProcess.TrayLoad : false]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[FMSClient] Complete to write [Location2.TrayProcess.TrayLoad : false]");

                        // EQP의 Response clear
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayInformation.TrayLoadResponse", (UInt16)0) == false)
                        {
                            _LOG_("[FMSClient] Fail to write [PlaceLocation.TrayInformation.TrayLoadResponse : 0]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Complete to write [PlaceLocation.TrayInformation.TrayLoadResponse : 0]");

                        //여기서 CellWork를 재개한다. 
                        // 아니지 시작인지 재개인지 알수가 없자나... 이미 공정이 시작된것인지는 어떻게 확인할까?
                        // 여기서는 공정시작을 해주는것이 우선이다.
                        // 공정시작이 이미 시작된 것이라면 CellWork를 재개하고, 공정시작이 아직 시작되지 않았다면 공정시작 신호를 주어야 한다.
                        // 공정시작여부는 Tray정보를 REST로 새로 받아서 확인해보기로 한다.
                        bool bNGSProcessAlreadyStarted = false;
                        Boolean TrayExist_Pick = false;
                        string TrayId_Pick = string.Empty;
                        _jsonDatTrayResponse TrayData=null;
                        EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayExist", out TrayExist_Pick);
                        EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayId", out TrayId_Pick);
                        _LOG_($"[EQPClient] PickLocation TrayExist:{TrayExist_Pick}, TrayId:{TrayId_Pick}");
                        if(TrayExist_Pick == true && TrayId_Pick.Length>0)
                        {
                            // REST로 부터 Tray정보를 읽는다.
                            TrayData = RESTClientBiz.GetRestTrayInfoByTrayId(TrayId_Pick, EQPID);
                            if(TrayData == null || TrayData.RESPONSE_CODE != "200" || TrayData.DATA.Count <1)
                            {
                                _LOG_($"Fail to GetRestTrayInfoByTrayId. TrayId:{TrayId_Pick}, {TrayData.RESPONSE_MESSAGE}", ECSLogger.LOG_LEVEL.ERROR);
                                return;
                            }
                            if (TrayData.DATA[0].EQP_TYPE == "NGS")
                                bNGSProcessAlreadyStarted = true;
                        } else
                        {
                            // PickLocation에 Tray가 없다면 여기까지..
                            return;
                        }

                        // 사실 PlaceLocation에 Tray가 도착했는데, PickLocation에 NGS공정을 시작하지 않은 Tray가 있을수는 없다. 
                        if(bNGSProcessAlreadyStarted==false)
                        {
                            // PickLocation에 Tray가 도착했지만, 아직 공정을 시작하지 않은 상태임.
                            // 뭔가 이상함. 에러내고 종료.
                            _LOG_($"Tray [{TrayId_Pick} on the PickLocation has not started NGS Process yet, something wrong]", ECSLogger.LOG_LEVEL.WARN);
                            return;
                        }

                        // 여기서 부터 CellWork 재개함.
                        if(CellWorkStart() == false)
                        {
                            return;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }


        }

        private bool CellWorkStart()
        {
            bool isPickLocation = true;
            _SorterTrayCellInformation PickTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);
            if (PickTrayCellInfo.TrayExist == false)
            {
                _LOG_($"[EQPClient], Failt to read Tray/Cell Information on PickLocation, TrayExist is false", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            _LOG_("[EQPClient] Start to prepare CellWork,.,");

            // 일단 PlaceLocation에 Tray가 있는지 확인해본다.
            isPickLocation = false;
            _SorterTrayCellInformation PlaceTrayCellInfo = EQPClient.ReadSorterTrayCellInformation(isPickLocation);

            if (PlaceTrayCellInfo.TrayExist == false)
            {
                // Tray 요청
                // PlaceLocation에 신규 Tray요청
                int FirstCellIndex = FindFirstCellToMove(PickTrayCellInfo);
                if (FirstCellIndex < 0)
                {
                    // 더 이상 빼낼 Cell이 없는 상황임. 이때는 PickLocation의 공정을 종료해야 함.
                    _LOG_("No Cell need to move, Tray at the PickLocation will be out", ECSLogger.LOG_LEVEL.INFO);

                    if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", (Boolean)true) == false)
                    {
                        _LOG_("[EQPClient] Fail to write ProcessEnd, [PickLocation.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                        return false;
                    }
                    _LOG_("[EQPClient] Success to write ProcessEnd, [PickLocation.ProcessEnd:true]");
                }
                else
                {
                    //Target Cell의 정보로 Tray를 요청한다.
                    //20230404 sgh ProcessType NG Sorter (1) 일때와 Grading (2)일때 DefectType, NGCode 변수에 매칭 바뀐거 수정
                    //string Grade = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                    //string DefectType = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                    string DefectType = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                    string Grade = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                    UInt16 TrayType = PickTrayCellInfo.TrayType;
                    string ReservedTrayId = string.Empty;
                    Boolean ReservedFlag = false;
                    Boolean TrayLoadRequest = true;
                    string ProductModel = PickTrayCellInfo.ProductModel;

                    if (EQPClient.WriteNGSTrayRequest("PlaceLocation.TrayRequest", Grade, DefectType, TrayType, ProductModel, TrayLoadRequest, ReservedFlag, ReservedTrayId) == false)
                    {
                        _LOG_($"[EQPClient] Fail to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}", ECSLogger.LOG_LEVEL.ERROR);
                    }
                    _LOG_($"[EQPClient] Success to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}");
                    // TrayRequest를 했으면 여기서 끝인가?
                }
            }
            else
            {
                // PlaceLocation에 Tray가 있을 경우
                // Place Location에 Tray확인하고 out할지 말지 결정
                if (CheckPlaceTrayOut(PlaceTrayCellInfo.TrayGrade, PlaceTrayCellInfo.DefectType, PlaceTrayCellInfo.CellCount, PickTrayCellInfo.ProcessType) == false)
                {
                    // PickLocation에 있는 Tray의 ProcessType에 맞지 않는 Tray가 PlaceLocation에 있는 상태임.
                    // TrayOut 요청하고 TrayReqeust도 해야 함.
                    if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                    {
                        _LOG_("[EQPClient] Failt to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                        return false;
                    }
                    _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");

                    // PlaceLocation에 신규 Tray요청
                    int FirstCellIndex = FindFirstCellToMove(PickTrayCellInfo);
                    if (FirstCellIndex < 0)
                    {
                        // 더 이상 빼낼 Cell이 없는 상황임. 이때는 PickLocation의 공정을 종료해야 함.
                        _LOG_("No Cell need to move, Tray at the PickLocation will be out", ECSLogger.LOG_LEVEL.INFO);

                        // MES로 먼저 알려야 함.
                        if (MesAvailable() == true)
                        {
                            if(FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", (Boolean)true)==false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return false;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]");


                            //Timer
                            // parameter필요없음?
                            WaitMesResponse("Location1.TrayProcess.ProcessEndResponse");

                        }
                        else
                        {
                            if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", (Boolean)true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEnd, [PickLocation.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return false;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEnd, [PickLocation.ProcessEnd:true]");

                            //FMS 로도 그냥 써준다.
                            if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", (Boolean)true) == false)
                            {
                                _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return false;
                            }
                            _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:true]");
                        }
                    }
                    else
                    {
                        //Target Cell의 정보로 Tray를 요청한다.
                        //20230404 sgh ProcessType NG Sorter (1) 일때와 Grading (2)일때 DefectType, NGCode 변수에 매칭 바뀐거 수정
                        //string Grade = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                        //string DefectType = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                        string DefectType = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                        string Grade = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                        UInt16 TrayType = PickTrayCellInfo.TrayType;
                        string ReservedTrayId = string.Empty;
                        Boolean ReservedFlag = false;
                        Boolean TrayLoadRequest = true;
                        string ProductModel = PickTrayCellInfo.ProductModel;

                        if (EQPClient.WriteNGSTrayRequest("PlaceLocation.TrayRequest", Grade, DefectType, TrayType, ProductModel, TrayLoadRequest, ReservedFlag, ReservedTrayId) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Success to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}");
                        // TrayRequest를 했으면 여기서 끝인가?
                    }
                }
                else
                {
                    // ProcessType에 맞는 Tray임.
                    _LOG_($"Tray in PlaceLocation, TrayId:{PlaceTrayCellInfo.TrayId}, TrayType:{PlaceTrayCellInfo.TrayType}, TrayGrade:{PlaceTrayCellInfo.TrayGrade}, DefectType:{PlaceTrayCellInfo.DefectType}");

                    // 해당 Grade or DefectType을 가진 Cell이 있는지를 확인하자.
                    int TargetCellIndex = CheckPlaceTrayGrade(PlaceTrayCellInfo, PickTrayCellInfo);
                    if (TargetCellIndex < 0)
                    {
                        _LOG_($"Tray in PlaceLocation should be out. no cell has [{PlaceTrayCellInfo.TrayGrade} or {PlaceTrayCellInfo.DefectType}]");
                        //TrayOut이 되어야 하고
                        if (EQPClient.WriteNodeByPath("PlaceLocation.TrayOut", (Boolean)true) == false)
                        {
                            _LOG_("[EQPClient] Failt to write TrayOut [PlaceLocation.TrayOut:true]", ECSLogger.LOG_LEVEL.ERROR);
                            return false;
                        }
                        _LOG_("[EQPClient] Success to write TrayOut [PlaceLocation.TrayOut:true]");

                        // PickLocation.TrayRequest
                        //TrayRequest에 필요한 값을 넣어주어야 한다. 
                        int FirstCellIndex = FindFirstCellToMove(PickTrayCellInfo);
                        if (FirstCellIndex < 0)
                        {
                            // 더 이상 빼낼 Cell이 없는 상황임. 이때는 PickLocation의 공정을 종료해야 함.
                            _LOG_("No Cell need to move, Tray at the PickLocation will be out", ECSLogger.LOG_LEVEL.INFO);

                            if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", (Boolean)true) == false)
                            {
                                _LOG_("[EQPClient] Fail to write ProcessEnd, [PickLocation.ProcessEnd:true]", ECSLogger.LOG_LEVEL.ERROR);
                                return false;
                            }
                            _LOG_("[EQPClient] Success to write ProcessEnd, [PickLocation.ProcessEnd:true]");
                        }
                        else
                        {
                            //Target Cell의 정보로 Tray를 요청한다.
                            //20230404 sgh ProcessType NG Sorter (1) 일때와 Grading (2)일때 DefectType, NGCode 변수에 매칭 바뀐거 수정
                            //string Grade = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                            //string DefectType = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                            string DefectType = PickTrayCellInfo.ProcessType == 1 ? PickTrayCellInfo._CellList[FirstCellIndex].DefectType : string.Empty;
                            string Grade = PickTrayCellInfo.ProcessType == 2 ? PickTrayCellInfo._CellList[FirstCellIndex].NGCode : string.Empty;
                            UInt16 TrayType = PickTrayCellInfo.TrayType;
                            string ReservedTrayId = string.Empty;
                            Boolean ReservedFlag = false;
                            Boolean TrayLoadRequest = true;
                            string ProductModel = PickTrayCellInfo.ProductModel;

                            if (EQPClient.WriteNGSTrayRequest("PlaceLocation.TrayRequest", Grade, DefectType, TrayType, ProductModel, TrayLoadRequest, ReservedFlag, ReservedTrayId) == false)
                            {
                                _LOG_($"[EQPClient] Fail to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}", ECSLogger.LOG_LEVEL.ERROR);
                            }
                            _LOG_($"[EQPClient] Success to write TrayRequest [PlaceLocation.TrayRequest], Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}");
                            // TrayRequest를 했으면 여기서 끝인가?
                        }
                    }
                    else
                    {
                        // PlaceLocation 에 Tray가 있고, 현재 PickLocation에 있는 Tray의 Cell중에 담을수 있는 Cell이 있는 상태임.
                        // 유일하게 CellWork 시작할수 있는 상황임.
                        // TargetCellIndex에 위치한 Cell 부터 CellWork 시작함.

                        //From은 TargetCellIndex에 있는 Cell정보
                        //To 는 ToWorkRequest 가 설비로 오면 결정함.

                        //20230403 sgh 연산부분 괄호 처리
                        if (EQPClient.WriteNodeByPath("CellWork.FromCellPosition", (UInt16)(TargetCellIndex + 1)) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write FromCellPosition [CellWork.FromCellPosition:{TargetCellIndex + 1}]", ECSLogger.LOG_LEVEL.ERROR);
                            return false;
                        }
                        _LOG_($"[EQPClient] Success to write FromCellPosition [CellWork.FromCellPosition:{TargetCellIndex + 1}]");
                        if (EQPClient.WriteNodeByPath("CellWork.FromWorkRequest", (Boolean)true) == false)
                        {
                            _LOG_($"[EQPClient] Fail to write FromWorkRequest [CellWork.FromWorkRequest:true]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Success to write FromWorkRequest [CellWork.FromWorkRequest:true]");

                    }
                }
            }
            return true;
        }

        private void SequencePickProcessStartResponse(MonitoredItem item)
        {
            try
            {
                // ProcessStartResponse가 오면 공정시작 처리를 함.
                DataMonitoredItem eventItem = item as DataMonitoredItem;

                if (eventItem != null)
                {
                    Boolean bProcessStartResponse = (Boolean)eventItem.LastValue.Value;

                    if(bProcessStartResponse)
                    {
                        // 공정시작 처리
                        string TrayId = string.Empty;
                        if (EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayId", out TrayId) == false)
                        {
                            _LOG_("[EQPClient] Fail to read TrayId [PickLocation.TrayInformation.TrayId] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Success to read TrayId:{TrayId} from EQP [PickLocation.TrayInformation.TrayId]");


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



                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessStart", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessStart [PickLocation.ProcessStart:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessStart [PickLocation.ProcessStart:false]");
                        
                    }
                    else
                    {                        
                        Boolean bCurrentProcessStart;
                        if(EQPClient.ReadValueByPath("PickLocation.ProcessStart", out bCurrentProcessStart) == false)
                        {
                            _LOG_("[EQPClient] Fail to Read ProcessStart [PickLocation.ProcessStart]", ECSLogger.LOG_LEVEL.ERROR);
                        }
                        _LOG_($"[EQPClient] Current ProcessStat : {bCurrentProcessStart}");

                        // ProcessStart가 false라면 정상적인 시퀀스로 간주하고 CellWork 시퀀스로 들어간다.
                        if (bCurrentProcessStart == false)
                        {
                            // CellWork 시작함.
                            // 제일 중요한 로직임.
                            // 먼저 NG Sorter인지 Grading 인지...
                            // PlaceLocation에 Tray가 정상적으로 있는지.. 
                            // DefectType별로 Cell을 구분해야 함.
                            // 그럼 로직은 아래처럼 해보자
                            // 1. 도착한 Tray의 Cell을 DefectType별로 구분함.
                            // 2. R1~R7까지 + Scrap (폐기 Cell 등급은 Scrap 또는 D)
                            // 3. PlaceLocation에 현재 Tray가 있는지 확인
                            // 3-1. Tray가 있는데 R1~R7까지 + Scrap 중 하나라면 그 Defect Type을 가지는 Cell부터 옮겨 담음
                            // 3-2. Tray가 있는데 R1~R7까지 + Scrap 중 하나이긴 하지만, 그 Defect Type을 가지는 Cell이 없을 경우, PlaceLocation의 Tray를 내보내고 새로운 Tray를 요청함.
                            // 3-3. Tray가 없으면, Tray를 요청하고 Tray가 도착할때까지 기다려야함.
                            // 
                            // 그럼 기다릴때. CellWork 명령을 기다리고 있는지 어떻게 알수 있지?
                            // Tray 가 PlaceLocation에 도착했을때 CellWork를 다시 이어서 진행시켜야 함.

                            // Tray내 Cell정보를 파악함.
                            // Cell정보는 Tag에 적혀 있음. 이걸 다시 읽자.
                            if (CellWorkStart() == false)
                                return;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }

        private void SequencePickProcessEndResponse(MonitoredItem item)
        {
            try
            {
                DataMonitoredItem eventItem = item as DataMonitoredItem;
                if (eventItem != null)
                {
                    Boolean bProcessEnd = (Boolean)eventItem.LastValue.Value;
                    if (bProcessEnd)
                    {
                        //ProcessEndResponse On을 받았을 경우 처리 개발 필요

                    }
                    else
                    {
                        /*
                        if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessEnd", false) == false)
                        {
                            _LOG_("[FMSClient] Fail to write ProcessEnd [Location1.TrayProcess.ProcessEnd:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[FMSClient] Success to write ProcessEnd [Location1.TrayProcess.ProcessEnd:false]");
                        */
                        if (EQPClient.WriteNodeByPath("PickLocation.ProcessEnd", false) == false)
                        {
                            _LOG_("[EQPClient] Fail to write ProcessEnd [PickLocation.ProcessEnd:false]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_("[EQPClient] Success to write ProcessEnd [PickLocation.ProcessEnd:false]");
                    }
                }
            }
            catch (Exception ex)
            {
                _LOG_($"[EQPClient:Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
            }
        }


        private int FindFirstCellToMove(_SorterTrayCellInformation PickTrayCellInfo)
        {
            string[] Grades = { "G1", "G2", "G3", "G4" };
            string[] DefectTypes = { "R1", "R2", "R3", "R4", "R5", "R6", "R7", "SCRAP" };

            if (PickTrayCellInfo.ProcessType ==1) // NG Sorter
            {
                for(int i = 0; DefectTypes.Length > i; i++)
                {
                    for(int j = 0; j<PickTrayCellInfo._CellList.Count;j++)
                    {
                        if (PickTrayCellInfo._CellList[j].CellExist == true && PickTrayCellInfo._CellList[j].DefectType == DefectTypes[i])
                            return j;
                    }
                }
            } else //Grader
            {
                for (int i = 0; Grades.Length > i; i++)
                {
                    for (int j = 0; j < PickTrayCellInfo._CellList.Count; j++)
                    {
                        if (PickTrayCellInfo._CellList[j].CellExist == true && PickTrayCellInfo._CellList[j].NGCode == Grades[i])
                            return j;
                    }
                }
            }

            return -1;
        }

        private int CheckPlaceTrayGrade(_SorterTrayCellInformation PlaceTrayCellInfo, _SorterTrayCellInformation PickTrayCellInfo)
        {

            if (PickTrayCellInfo.ProcessType ==1) // NG Sorting
            {
                for(int i = 0; i< PickTrayCellInfo._CellList.Count; i++)
                {
                    if (PickTrayCellInfo._CellList[i].DefectType == PlaceTrayCellInfo.DefectType)
                        return i;
                }
            } else
            {
                for (int i = 0; i < PickTrayCellInfo._CellList.Count; i++)
                {
                    //20230403 sgh Grades일 경우 TrayGrade과 비교
                    if (PickTrayCellInfo._CellList[i].DefectType == PlaceTrayCellInfo.TrayGrade)
                        return i;
                }
            }

            return -1;
        }

        private bool CheckPlaceTrayOut(string TrayGrade, string DefectType, UInt16 CellCount, UInt16 ProcessType)
        {
            string[] Grades = { "G1", "G2", "G3", "G4" };
            string[] DefectTypes = { "R1", "R2", "R3", "R4", "R5", "R6", "R7", "SCRAP" };
            if(ProcessType == 1) //NG Sorting
            {
                if (DefectTypes.Contains(DefectType.ToUpper()))
                {
                    return true;
                }
                else
                    return false;
            } else if (ProcessType == 2)
            {
                if (Grades.Contains(TrayGrade.ToUpper()))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                // 1,2중 하나가 아니면 Trouble임.
                return false;
            }
        }

        private void SequencePickTrayLoad(MonitoredItem item)
        {
            // Sorter에서는 TrayInformation.ProcessType이 추가되어 있다. 
            // 1 이면 NG Sorting
            // 2 면 Grading 임.
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
                                //ProcessType추가  1: NG Sorting, 2 : Grading
                                UInt16 ProcessType = 1;
                                if (TrayData.DATA[0].NEXT_PROCESS_TYPE == "GRD") ProcessType = 2;
                                if (EQPClient.WriteNodeByPath("PickLocation.TrayInformation.ProcessType", (UInt16)ProcessType) == false)
                                {
                                    _LOG_($"[EQPClient] Fail to write ProcessType [PickLocation.TrayInformation.ProcessType:{ProcessType}] on EQP", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[EQPClient] Success to write TrayInfo [PickLocation.TrayInformation] on EQP, {TrayId}, ProcessType:[{TrayData.DATA[0].NEXT_PROCESS_TYPE}:{ProcessType}]");

                                if (EQPClient.WriteCellInfoWidhNGCodeEQP("PickLocation.CellInformation.Cell", TrayCellData.DATA, 0) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write Cell Info with NGCode [PickLocation.CellInformation.Cell] on EQP", ECSLogger.LOG_LEVEL.ERROR);
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
                                // NGCode정보도 써주어야 한다.
                                if (FMSClient.WriteCellInfoNGCodeFMS("Location1.TrayInformation.CellGrade", TrayCellData.DATA) == false)
                                {
                                    _LOG_($"[FMSClient] Fail to write TrackIn Cell Info [Location1.TrayInformation.CellInformation] on FMS", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write Cell Information : [{TrayData.DATA[0].TRAY_ID}:CellCount:{TrayCellData.ROW_COUNT}]");

                                // Cell Data GridView update해야함. (EQP Data를 새로 읽어서 draw)
                                _SorterCellInformation TrackInCellInfo = EQPClient.ReadEQPTrackInOutCellGradeInfomation("PickLocation.CellInformation");
                                DrawDataGridViewWithCellGradeInfo(TrackInCellInfo, TrackInCellList_DataGridView);

                            }
                            else
                            {
                                // Tray 정보가 DB에 없는 상황
                                _LOG_($"No Tray Information in Database [{TrayId}]", ECSLogger.LOG_LEVEL.ERROR);
                                // TODO : Trouble 처리 해야함
                                return;
                            }

                            //기본데이터 처리 완료. 
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
                    else // PickLocation TrayLoad : Off
                    {
                        //EQP에서 Tray정보를 받고 TrayLoad 요청을 off했다. 
                        // MES Alive면 MES로 ProcessStart를 보낸다.
                        // 아니면, 바로 EQP로 ProcesStart 요청을 보낸다.
                        // 근데 TrayLoad가 Off 되었다고 무조건 보내면 안된다. 
                        // 설비로 내려준 TrayLoadResponse가 1일 경우에만 MES로/EQP로 ProcessStart요청을 보낸다.

                        UInt16 CurrentTrayLoadResponse = 0;
                        bool bProcessStartFlag = false; // True면 ProcessStart 시퀀스가 시작되어도 좋다.
                        if(EQPClient.ReadValueByPath("PickLocation.TrayInformation.TrayLoadResponse", out CurrentTrayLoadResponse)==false)
                        {
                            _LOG_("[EQPClient] Fail to Read TrayLoadResponse ON EQPClient [PickLocation.TrayInformation.TrayLoadResponse]", ECSLogger.LOG_LEVEL.ERROR);
                            return;
                        }
                        _LOG_($"[EQPClient] Current TrayLoadResponse value : [{CurrentTrayLoadResponse}]");

                        if (CurrentTrayLoadResponse == (UInt16)1) bProcessStartFlag = true;
                                                
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

                        // NG Sorter는 다른거랑 좀 다르다.. TrayLoad 시퀀스가 완료되면.. ProcessStart를 MES로 보내야한다.
                       if(bProcessStartFlag)
                        {
                            if(MesAvailable())
                            {
                                // MES가 Alive니깐 MES로 ProcessStart보낸다.
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write ProcessStart ON FMSClient [Location1.TrayProcess.ProcessStart;true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.ProcessStart : true]");

                                // Timer
                                // parameter 없음.
                                WaitMesResponse("Location1.TrayProcess.ProcessStart"); //타임아웃 이벤트가 다름...

                            } else
                            {
                                // MES가 Alive아니면 EQP 로 ProcessStart를 보낸다.
                                if (EQPClient.WriteNodeByPath("PickLocation.TrayProcess.ProcessStart", (Boolean)true) == false)
                                {
                                    _LOG_("[EQPClient] Fail to write TrayLoadResponse ON EQPClient [PickLocation.TrayProcess.ProcessStart:true]", ECSLogger.LOG_LEVEL.ERROR);
                                }
                                _LOG_($"[EQPClient] Complete to write [PickLocation.TrayProcess.ProcessStart:true]");

                                // MES에도 그냥 써줌
                                if (FMSClient.WriteNodeByPath("Location1.TrayProcess.ProcessStart", (Boolean)true) == false)
                                {
                                    _LOG_("[FMSClient] Fail to write ProcessStart ON FMSClient [Location1.TrayProcess.ProcessStart;true]", ECSLogger.LOG_LEVEL.ERROR);
                                    return;
                                }
                                _LOG_($"[FMSClient] Complete to write [Location1.TrayProcess.ProcessStart : true]");
                            }
                        }

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
                    if (response.RESPONSE_CODE != "200")
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
            //FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.RequestCellGradeResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessStartResponse");
            FMSClient.m_monitoredItemList.Add("Location1.TrayProcess.ProcessEndResponse");
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
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProcessType");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("PickLocation.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("PickLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessStart");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessStartResponse");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessEnd");
            EQPClient.m_monitoredItemList.Add("PickLocation.ProcessEndResponse");

            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayExist");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayLoad");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayLoadResponse");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.TrayGrade");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.DefectType");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.ProductModel");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.RouteId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.ProcessId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayInformation.LotId");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.CellInformation.CellCount");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayOut");

            EQPClient.m_monitoredItemList.Add("CellWork.FromCellPosition");
            EQPClient.m_monitoredItemList.Add("CellWork.FromWorkRequest");
            EQPClient.m_monitoredItemList.Add("CellWork.FromWorkResponse");
            EQPClient.m_monitoredItemList.Add("CellWork.CellId");
            EQPClient.m_monitoredItemList.Add("CellWork.ToWorkRequest");
            EQPClient.m_monitoredItemList.Add("CellWork.ToCellPosition");
            EQPClient.m_monitoredItemList.Add("CellWork.ToWorkResponse");
            EQPClient.m_monitoredItemList.Add("CellWork.WorkComplete");
            EQPClient.m_monitoredItemList.Add("CellWork.WorkCompleteResponse");

            //20230323 KJY - TrayRequest추가
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.Grade");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.DefectType");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.TrayType");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.ProductModel");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.TrayLoadRequest");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.ReservedFlag");
            EQPClient.m_monitoredItemList.Add("PlaceLocation.TrayRequest.ReservedTrayId");
        }
    }
}
