using CommonCtrls;
using RestClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;
using UnifiedAutomation.UaClient.Controls;
//using static RestAPIServer.DataClass;

namespace OPCUAClient
{
    public enum MappingDirection
    {
        none = 0,
        EqpToFms,
        FmsToEqp
    }

    public class MappingRecipeItem
    {
        public string EqpItem;
        public string FmsItem;
        public string DataType;
        public string DefaultValue;
        public object RecipeValue; // Rest 나 FMS에서 받아온 값 (object Type으로 사용함)

        public Dictionary<string, MappingRecipeItem> LoadRecipeMappingTable(string csv_file_path, MappingDirection mapDirection)
        {
            List<MappingRecipeItem> list = new List<MappingRecipeItem>();
            string[] lines = File.ReadAllLines(csv_file_path);
            string[] headers = lines[0].Split(',');


            for (int i = 1; i < lines.Length; i++)
            {
                string[] flds = lines[i].Split(',');

                MappingRecipeItem item = new MappingRecipeItem
                {
                    EqpItem = flds[0],
                    FmsItem = flds[1],
                    DataType = flds[2],
                    DefaultValue = flds[3]
                };

                list.Add(item);

            }

            Dictionary<string, MappingRecipeItem> MappingItemDic = new Dictionary<string, MappingRecipeItem>();
            foreach (MappingRecipeItem item in list)
            {
                if (mapDirection == MappingDirection.EqpToFms)
                    MappingItemDic.Add(item.EqpItem, item);
                else if (mapDirection == MappingDirection.FmsToEqp)
                    MappingItemDic.Add(item.FmsItem, item);
                else
                    return null;
            }

            return MappingItemDic;
        }

        public static object SetObjectTypeCastByString(string dataType, string valueInString)
        {
            
            if (valueInString.Length > 0)
            {
                switch (dataType.ToUpper())
                {
                    case "UINT16":
                        return UInt16.Parse(valueInString);
                    case "UINT32":
                        return UInt32.Parse(valueInString);
                    case "STRING":
                        return (String)valueInString;
                    case "BOOLEAN":
                        return Boolean.Parse(valueInString);
                    case "FLOAT":
                        return float.Parse(valueInString);
                    default:
                        return null;
                }
            }

            return null;
        }

        public static object SetObjectTypeCast(string dataType, object value)
        {

            if (value != null)
            {
                switch (dataType.ToUpper())
                {
                    case "UINT16":
                        return (UInt16)value;
                    case "UINT32":
                        return (UInt32)value;
                    case "STRING":
                        return (String)value;
                    case "BOOLEAN":
                        return (Boolean)value;
                    case "FLOAT":
                        return (float)value;
                    default:
                        return null;
                }
            } 

            return null;
        }



        internal UInt16 GeUInt16tOperationMode(string EqpType, string ProcessType, string OperationMode)
        {
            switch(EqpType)
            {
                case "CHG":
                case "HPC":
                    if (ProcessType == "OCV")
                        return 1;
                    else if(ProcessType == "CHG")
                    {
                        if (OperationMode == "CC")
                            return 2;
                        else if (OperationMode == "CCCV")
                            return 4;
                        else
                            return 0;
                    }
                    else if(ProcessType == "DIC")
                    {
                        if (OperationMode == "CC")
                            return 8;
                        else if (OperationMode == "CCCV")
                            return 16;
                        else
                            return 0;
                    }
                    break;
                case "OCV":
                    break;
                case "DCR":
                    break;
                case "MIC":
                    break;
                default:
                    return 0;
            }
            return 0;
        }
        internal string GetStringOperationMode(string EqpType, string ProcessType, int OperationMode)
        {
            switch(EqpType)
            {
                case "CHG":
                case "HPC":
                    if (ProcessType == "OCV")
                        return "OCV";
                    else if (ProcessType == "CHG")
                    {
                        if (OperationMode == 2)
                            return "CC";
                        else if (OperationMode == 4)
                            return "CCCV";
                        else
                            return string.Empty;
                    }
                    else if (ProcessType == "DIC")
                    {
                        if (OperationMode == 8)
                            return "CC";
                        else if (OperationMode == 16)
                            return "CCCV";
                        else
                            return string.Empty;
                    }
                    break;
                case "OCV":
                    break;
                case "DCR":
                    break;
                case "MIC":
                    break;
                default:
                    return string.Empty;
            }

            return string.Empty;
        }
    }
    public class MappingProcessDataItem
    {
        public string EqpItem;
        public string FmsItem;
        public string DataType;
        public string CommonFlag; // 이게 "C" 면, 전 Cell 공통으로 들어가는 데이터임
        public object ProcessData;

        public Dictionary<string, MappingProcessDataItem> LoadPDMappingTable(string csv_file_path, MappingDirection mapDirection)
        {
            List<MappingProcessDataItem> list = new List<MappingProcessDataItem>();
            string[] lines = File.ReadAllLines(csv_file_path);
            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] flds = lines[i].Split(',');

                MappingProcessDataItem item = new MappingProcessDataItem
                {
                    EqpItem = flds[0],
                    FmsItem = flds[1],
                    DataType = flds[2],
                    CommonFlag = flds.Length>3?flds[3]:string.Empty, //{EQP_TYPE}_PD.csv 파일에 CommonFlag가 있는 경우
                    ProcessData = null
                };

                list.Add(item);
            }

            Dictionary<string, MappingProcessDataItem> MappingItemDic = new Dictionary<string, MappingProcessDataItem>();
            foreach (MappingProcessDataItem item in list)
            {
                if (mapDirection == MappingDirection.EqpToFms)
                    MappingItemDic.Add(item.EqpItem, item);
                else if (mapDirection == MappingDirection.FmsToEqp)
                    MappingItemDic.Add(item.FmsItem, item);
                else
                    return null;
            }

            return MappingItemDic;
        }

        public List<MappingProcessDataItem> LoadPDMappingTableList(string csv_file_path, MappingDirection mapDirection)
        {
            List<MappingProcessDataItem> list = new List<MappingProcessDataItem>();
            string[] lines = File.ReadAllLines(csv_file_path);
            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] flds = lines[i].Split(',');

                MappingProcessDataItem item = new MappingProcessDataItem
                {
                    EqpItem = flds[0],
                    FmsItem = flds[1],
                    DataType = flds[2],
                    ProcessData = "0"
                };

                list.Add(item);
            }

            return list;
        }
        public static object SetObjectTypeCastByString(string dataType, string valueInString)
        {

            if (valueInString.Length > 0)
            {
                switch (dataType.ToUpper())
                {
                    case "UINT16":
                        return UInt16.Parse(valueInString);
                    case "UINT32":
                        return UInt32.Parse(valueInString);
                    case "STRING":
                        return (String)valueInString;
                    case "BOOLEAN":
                        return Boolean.Parse(valueInString);
                    case "FLOAT":
                        return float.Parse(valueInString);
                    default:
                        return null;
                }
            }

            return null;
        }

        public static object SetObjectTypeCast(string dataType, object value)
        {

            if (value != null)
            {
                switch (dataType.ToUpper())
                {
                    case "UINT16":
                        return (UInt16)value;
                    case "UINT32":
                        return (UInt32)value;
                    case "STRING":
                        return (String)value;
                    case "BOOLEAN":
                        return (Boolean)value;
                    case "FLOAT":
                        return (float)value;
                    default:
                        return null;
                }
            }

            return null;
        }



        internal UInt16 GeUInt16tOperationMode(string EqpType, string ProcessType, string OperationMode)
        {
            switch (EqpType)
            {
                case "CHG":
                    if (ProcessType == "OCV")
                        return 1;
                    else if (ProcessType == "CHG")
                    {
                        if (OperationMode == "CC")
                            return 2;
                        else if (OperationMode == "CCCV")
                            return 4;
                        else
                            return 0;
                    }
                    else if (ProcessType == "DIC")
                    {
                        if (OperationMode == "CC")
                            return 8;
                        else if (OperationMode == "CCCV")
                            return 16;
                        else
                            return 0;
                    }
                    break;
                case "HPC":
                    break;
                case "OCV":
                    break;
                case "DCR":
                    break;
                case "MIC":
                    break;
                default:
                    return 0;
            }
            return 0;
        }
        internal string GetStringOperationMode(string EqpType, string ProcessType, int OperationMode)
        {
            switch (EqpType)
            {
                case "CHG":
                    if (ProcessType == "OCV")
                        return "OCV";
                    else if (ProcessType == "CHG")
                    {
                        if (OperationMode == 2)
                            return "CC";
                        else if (OperationMode == 4)
                            return "CCCV";
                        else
                            return string.Empty;
                    }
                    else if (ProcessType == "DIC")
                    {
                        if (OperationMode == 8)
                            return "CC";
                        else if (OperationMode == 16)
                            return "CCCV";
                        else
                            return string.Empty;
                    }
                    break;
                case "HPC":
                    break;
                case "OCV":
                    break;
                case "DCR":
                    break;
                case "MIC":
                    break;
                default:
                    return string.Empty;
            }

            return string.Empty;
        }
    }

    public partial class OPCUAClient : UserControl
    {
        public bool WriteBasicCellInfoEQP(string prefixPath, List<_dat_cell> CellDataList, int startIndex = 0)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string cellPath = string.Empty;
            string targetPath = string.Empty;

            foreach (_dat_cell item in CellDataList)
            {
                int nCellNo = item.CELL_NO;
                cellPath = $"{prefixPath}._{startIndex + nCellNo - 1}";
                //CellExist Boolean
                targetPath = cellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)true);
                //CellId String
                targetPath = cellPath + ".CellId";
                writeItems.Add(targetPath, (String)item.CELL_ID);
                //LotId String
                targetPath = cellPath + ".LotId";
                writeItems.Add(targetPath, (String)item.LOT_ID);
            }

            return WriteNodeWithDic(writeItems);
        }
        public bool WriteCellInfoWidhNGCodeEQP(string prefixPath, List<_dat_cell> CellDataList, int startIndex = 0)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string cellPath = string.Empty;
            string targetPath = string.Empty;

            foreach (_dat_cell item in CellDataList)
            {
                int nCellNo = item.CELL_NO;
                cellPath = $"{prefixPath}._{startIndex + nCellNo - 1}";
                //CellExist Boolean
                targetPath = cellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)true);
                //CellId String
                targetPath = cellPath + ".CellId";
                writeItems.Add(targetPath, (String)item.CELL_ID);
                //LotId String
                targetPath = cellPath + ".LotId";
                writeItems.Add(targetPath, (String)item.LOT_ID);

                //NGCode String
                targetPath = cellPath + ".NGCode";
                writeItems.Add(targetPath, (String)item.GRADE_CODE);
                //NGType String
                targetPath = cellPath + ".NGType";
                writeItems.Add(targetPath, (String)item.GRADE_NG_TYPE);

                //20230322 KJY - DefectType추가
                targetPath = cellPath + ".DefectType";
                writeItems.Add(targetPath, (String)item.GRADE_DEFECT_TYPE);
            }

            return WriteNodeWithDic(writeItems);
        }

        /// <summary>
        /// NGCode, NGType 포함, Defect Code는?
        /// </summary>
        /// <param name="prefixPath"></param>
        /// <param name="CellDataList"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public bool WriteCellNGCodeEQP(string prefixPath, List<_dat_cell> CellDataList, int startIndex = 0)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string cellPath = string.Empty;
            string targetPath = string.Empty;

            foreach (_dat_cell item in CellDataList)
            {
                int nCellNo = item.CELL_NO;
                cellPath = $"{prefixPath}._{startIndex + nCellNo - 1}";
                //NGCode String
                targetPath = cellPath + ".NGCode";
                writeItems.Add(targetPath, (String)item.GRADE_CODE);
                //NGType String
                targetPath = cellPath + ".NGType";
                writeItems.Add(targetPath, (String)item.GRADE_NG_TYPE);
            }

            return WriteNodeWithDic(writeItems);
        }

        public bool WriteBasicTrayInfoEQP(string prefixPath, _dat_tray trayData)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            //TrayID
            targetPath = prefixPath + ".TrayId";
            writeItems.Add(targetPath, (String)trayData.TRAY_ID);

            // TrayType
            targetPath = prefixPath + ".TrayType";
            //20230403 sgh 빈 트레이일 경우 TrayType 정보 입력 추가
            if (trayData.TRAY_STATUS == "E")
            {
                if (trayData.TRAY_ZONE == "AD")
                    writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Empty);
                else
                    writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Empty);

                return WriteNodeWithDic(writeItems);
            }
            if (trayData.TRAY_ZONE == "AD")
                writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Full);
            else
                writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Full);

            //ModelID
            targetPath = prefixPath + ".ProductModel";
            writeItems.Add(targetPath, (String)trayData.MODEL_ID);
            // LotId
            targetPath = prefixPath + ".LotId";
            writeItems.Add(targetPath, (String)trayData.LOT_ID);
            // ProcessId
            targetPath = prefixPath + ".ProcessId";
            writeItems.Add(targetPath, (String)trayData.NEXT_EQP_TYPE + trayData.NEXT_PROCESS_TYPE + string.Format("{0:D3}", trayData.NEXT_PROCESS_NO)); //trayData.NEXT_PROCESS_NO);// ;
            // RouteId
            targetPath = prefixPath + ".RouteId";
            writeItems.Add(targetPath, (String)trayData.ROUTE_ID);

            
            //CellCount는? 이거 있는 설비들이 있을텐데... 확인해보고 값을 넣는 것으로 해보자.

            return WriteNodeWithDic(writeItems);

        }

        public bool WriteTrayInfoWithGradeEQP(string prefixPath, _dat_tray trayData)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            //TrayID
            //targetPath = prefixPath + ".TrayId";
            //writeItems.Add(targetPath, (String)trayData.TRAY_ID);

            //ModelID
            targetPath = prefixPath + ".ProductModel";
            writeItems.Add(targetPath, (String)trayData.MODEL_ID);
            // LotId
            targetPath = prefixPath + ".LotId";
            writeItems.Add(targetPath, (String)trayData.LOT_ID);
            // ProcessId
            targetPath = prefixPath + ".ProcessId";
            writeItems.Add(targetPath, (String)trayData.NEXT_EQP_TYPE + trayData.NEXT_PROCESS_TYPE + string.Format("{0:D3}", trayData.NEXT_PROCESS_NO)); //trayData.NEXT_PROCESS_NO);// ;
            // RouteId
            targetPath = prefixPath + ".RouteId";
            writeItems.Add(targetPath, (String)trayData.ROUTE_ID);
            // TrayType
            targetPath = prefixPath + ".TrayType";
            if (trayData.TRAY_ZONE == "AD")
                writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Full);
            else
                writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Full);

            if (trayData.REWORK_FLAG == "Y")
            {
                //TrayGrade
                targetPath = prefixPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)String.Empty);
                //DefectType
                targetPath = prefixPath + ".DefectType";
                writeItems.Add(targetPath, (String)trayData.TRAY_GRADE);
            } else
            { // 등급Tray임
                //TrayGrade
                targetPath = prefixPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)trayData.TRAY_GRADE);
                //DefectType
                targetPath = prefixPath + ".DefectType";
                writeItems.Add(targetPath, (String)String.Empty);
            }

            //CellCount는? 이거 있는 설비들이 있을텐데... 확인해보고 값을 넣는 것으로 해보자.
            return WriteNodeWithDic(writeItems);

        }
        public bool ClearBasicTrayInfoEQP(string prefixPath)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            targetPath = prefixPath + ".ProductModel";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".LotId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".ProcessId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".RouteId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".TrayType";
            writeItems.Add(targetPath, (UInt16)0);

            return WriteNodeWithDic(writeItems);
        }
        public bool ClearBasicCellInfoEQP(string prefixPath)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            // CellCount
            targetPath = prefixPath + ".CellCount";
            writeItems.Add(targetPath, (UInt16)0);

            for(int i=0; i<30; i++)
            {
                targetPath = prefixPath + $".Cell._{i}.CellExist";
                writeItems.Add(targetPath, (Boolean)false);
                targetPath = prefixPath + $".Cell._{i}.CellId";
                writeItems.Add(targetPath, (String)String.Empty);
                targetPath = prefixPath + $".Cell._{i}.LotId";
                writeItems.Add(targetPath, (String)String.Empty);
            }

            return WriteNodeWithDic(writeItems);
        }

        public bool ClearBasicTrayInfoFMS(string prefixPath)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            targetPath = prefixPath + ".TrayExist";
            writeItems.Add(targetPath, (Boolean)false);
            targetPath = prefixPath + ".TrayId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".ProductModel";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".LotId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".ProcessId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".RouteId";
            writeItems.Add(targetPath, (String)String.Empty);
            targetPath = prefixPath + ".TrayType";
            writeItems.Add(targetPath, (UInt16)0);

            return WriteNodeWithDic(writeItems);
        }
        public bool ClearBasicCellInfoFMS(string prefixPath)
        {
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string targetPath = string.Empty;

            // CellCount
            targetPath = prefixPath + ".CellCount";
            writeItems.Add(targetPath, (UInt16)0);

            for (int i = 0; i < 30; i++)
            {
                targetPath = prefixPath + $".Cell.{string.Format("Cell{0:D2}", i + 1)}.CellExist";
                writeItems.Add(targetPath, (Boolean)false);
                targetPath = prefixPath + $".Cell.{string.Format("Cell{0:D2}", i + 1)}.CellId";
                writeItems.Add(targetPath, (String)String.Empty);
                targetPath = prefixPath + $".Cell.{string.Format("Cell{0:D2}", i + 1)}.LotId";
                writeItems.Add(targetPath, (String)String.Empty);
            }

            return WriteNodeWithDic(writeItems);
        }


        public bool WriteBasicTrayCellInfoFMS(string PrefixPath, _dat_tray Tray, List<_dat_cell> CellList, string TrayLevel = "Level1", int CellStartIndex = 0)
        {
            // .Level1 or .Level2
            string TrayPath = PrefixPath + "." + TrayLevel;
            string targetPath = string.Empty;
            string CellPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();

            // CellCount
            targetPath = TrayPath + ".CellCount";
            writeItems.Add(targetPath, (UInt16)CellList.Count);
            // LotId
            targetPath = TrayPath + ".LotId";
            writeItems.Add(targetPath, (String)Tray.LOT_ID);
            // Model
            targetPath = TrayPath + ".Model";
            writeItems.Add(targetPath, (String)Tray.MODEL_ID);
            // ProcessId
            targetPath = TrayPath + ".ProcessId";
            //writeItems.Add(targetPath, (String)Tray.NEXT_EQP_TYPE + Tray.NEXT_PROCESS_TYPE + Tray.NEXT_PROCESS_NO);
            writeItems.Add(targetPath, (String)Tray.NEXT_EQP_TYPE + Tray.NEXT_PROCESS_TYPE + string.Format("{0:D3}", Tray.NEXT_PROCESS_NO));
            // RouteId
            targetPath = TrayPath + ".RouteId";
            writeItems.Add(targetPath, (String)Tray.ROUTE_ID);
            // TrayExist
            targetPath = TrayPath + ".TrayExist";
            writeItems.Add(targetPath, (Boolean)true);
            // TrayGrade
            // 이건 필요없다.
            // TrayId
            targetPath = TrayPath + ".TrayId";
            writeItems.Add(targetPath, (String)Tray.TRAY_ID);
            // TrayType
            targetPath = TrayPath + ".TrayType";
            if (Tray.TRAY_ZONE == "AD")
                writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Full);
            else
                writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Full);

            // .CellInformation
            foreach (_dat_cell cell in CellList)
            {
                CellPath = PrefixPath + $".CellInformation.Cell{string.Format("{0:D2}", cell.CELL_NO)}";
                //CellExist
                targetPath = CellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)true);
                //CellId
                targetPath = CellPath + ".CellId";
                writeItems.Add(targetPath, (String)cell.CELL_ID);
                //LotId
                targetPath = CellPath + ".LotId";
                writeItems.Add(targetPath, (String)cell.LOT_ID);
            }


            return WriteNodeWithDic(writeItems);
        }

        public bool WriteBasicTrayInfoFMS(string TrayInformationPath, string CellCountPath, _dat_tray Tray, int CellCount, bool isTrackIn = true)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();

            // TrayExist
            targetPath = TrayInformationPath + ".TrayExist";
            writeItems.Add(targetPath, (Boolean)true);

            // TrayId
            targetPath = TrayInformationPath + ".TrayId";
            writeItems.Add(targetPath, (String)Tray.TRAY_ID);

            // TrayType
            targetPath = TrayInformationPath + ".TrayType";
            //20230403 sgh 빈 트레이일 경우 TrayType 정보 입력 추가
            if ((String)Tray.TRAY_STATUS == "E")
            {
                if ((String)Tray.TRAY_ZONE == "AD")
                    writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Empty);
                else
                    writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Empty);

                return WriteNodeWithDic(writeItems);
            }
            if (Tray.TRAY_ZONE == "AD")
                writeItems.Add(targetPath, (UInt16)Global.TrayType.AD_Full);
            else
                writeItems.Add(targetPath, (UInt16)Global.TrayType.BD_Full);

            // LotId
            targetPath = TrayInformationPath + ".LotId";
            writeItems.Add(targetPath, (String)Tray.LOT_ID);
            // Model
            targetPath = TrayInformationPath + ".Model";
            //targetPath = TrayInformationPath + ".ProductModel";
            writeItems.Add(targetPath, (String)Tray.MODEL_ID);
            // ProcessId
            if (isTrackIn)
            {
                targetPath = TrayInformationPath + ".ProcessId";
                //writeItems.Add(targetPath, (String)Tray.NEXT_EQP_TYPE + Tray.NEXT_PROCESS_TYPE + Tray.NEXT_PROCESS_NO);
                writeItems.Add(targetPath, (String)Tray.NEXT_EQP_TYPE + Tray.NEXT_PROCESS_TYPE + string.Format("{0:D3}", Tray.NEXT_PROCESS_NO));
            }
            // RouteId
            targetPath = TrayInformationPath + ".RouteId";
            writeItems.Add(targetPath, (String)Tray.ROUTE_ID);

            // CellCount
             writeItems.Add(CellCountPath, (UInt16)CellCount);

            return WriteNodeWithDic(writeItems);
        }

        /// <summary>
        /// CellInformationPath : .Cellxx 이전 까지 ex) TrayInformation.TrackInCellInformation
        /// </summary>
        /// <param name="CellInformationPath"></param>
        /// <param name="CellList"></param>
        /// <returns></returns>
        public bool WriteBasicCellInfoFMS(string CellInformationPath, List<_dat_cell> CellList)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            // .CellInformation
            foreach (_dat_cell cell in CellList)
            {
                CellPath = CellInformationPath + $".Cell{string.Format("{0:D2}", cell.CELL_NO)}";
                //CellExist
                targetPath = CellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)true);
                //CellId
                targetPath = CellPath + ".CellId";
                writeItems.Add(targetPath, (String)cell.CELL_ID);
                //LotId
                targetPath = CellPath + ".LotId";
                writeItems.Add(targetPath, (String)cell.LOT_ID);
            }

            return WriteNodeWithDic(writeItems);
        }
        // 20230404 msh : WriteBasicCellInfoFMS 추가
        public bool WriteBasicCellInfoFMS(string CellInformationPath, List<_CellBasicData> CellList)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            // .CellInformation
            for (int i = 0; i < CellList.Count; i++)
            {
                CellPath = CellInformationPath + $".Cell{string.Format("{0:D2}", i + 1)}";
                //CellExist
                targetPath = CellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)CellList[i].CellExist);
                //CellId
                targetPath = CellPath + ".CellId";
                writeItems.Add(targetPath, (String)CellList[i].CellId);
                //LotId
                targetPath = CellPath + ".LotId";
                writeItems.Add(targetPath, (String)CellList[i].LotId);
            }

            return WriteNodeWithDic(writeItems);
        }
        public bool WriteCellInfoNGCodeFMS(string CellInformationPath, List<_dat_cell> CellList)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            // .CellInformation
            foreach (_dat_cell cell in CellList)
            {
                CellPath = CellInformationPath + $".Cell{string.Format("{0:D2}", cell.CELL_NO)}";
                //NGCode
                targetPath = CellPath + ".NGCode";
                writeItems.Add(targetPath, (String)cell.GRADE_CODE);
                //NGType
                targetPath = CellPath + ".NGType";
                writeItems.Add(targetPath, (String)cell.GRADE_NG_TYPE);

                //DefectType
                targetPath = CellPath + ".DefectType";
                writeItems.Add(targetPath, (String)cell.GRADE_DEFECT_TYPE);
            }

            return WriteNodeWithDic(writeItems);
        }

        public bool WriteBasicCellInfoFMS(string CellInformationPath, _CellInformation AllCellProcessData)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            // .CellInformation
            for (int i=0; i< AllCellProcessData._CellList.Count; i++)
            {
                _CellProcessData cell = AllCellProcessData._CellList[i];
                CellPath = CellInformationPath + $".Cell{string.Format("{0:D2}", i+1)}";
                //CellExist
                targetPath = CellPath + ".CellExist";
                writeItems.Add(targetPath, (Boolean)cell.CellExist);
                //CellId
                targetPath = CellPath + ".CellId";
                writeItems.Add(targetPath, (String)cell.CellId);
                //LotId
                targetPath = CellPath + ".LotId";
                writeItems.Add(targetPath, (String)cell.LotId);
            }

            return WriteNodeWithDic(writeItems);
        }



        public bool WriteRecipeEQP(string RecipePath, _jsonEcsApiMasterRecipeResponse recipeResponse, MappingDirection mappingDirection = MappingDirection.EqpToFms)
        {
            try
            {
                Dictionary<string, object> writeItems = new Dictionary<string, object>();
                string targetPath = string.Empty;
                object Value = null;
                string MappingFile = $@"Mapping\FMS-{EQPType}_SV.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_SV.csv 파일이 있어야 한다.

                // 여기서 Recipe CSV 파일을 load하자. 
                MappingRecipeItem mappingRecipe = new MappingRecipeItem();
                Dictionary<string, MappingRecipeItem> RecipeDic = mappingRecipe.LoadRecipeMappingTable(MappingFile, mappingDirection);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)


                // RecipeId : 전설비 공통
                targetPath = RecipePath + ".RecipeId";
                Value = (String)recipeResponse.RECIPE_DATA.RECIPE_ID;
                writeItems.Add(targetPath, Value);
                _LOG_($"[WriteItem] {targetPath}:{Value}", ECSLogger.LOG_LEVEL.INFO);

                if (IsMultiOperationEQP(recipeResponse.RECIPE_DATA.EQP_TYPE) == true)
                {
                    // NextProcessExist 
                    targetPath = RecipePath + ".NextProcessExist";
                    if (recipeResponse.RECIPE_DATA.NEXT_PROCESS_EXIST == "Y")
                        Value = (Boolean)true;
                    else
                        Value = (Boolean)false;
                    writeItems.Add(targetPath, Value);

                    _LOG_($"[WriteItem] {targetPath}:{Value}", ECSLogger.LOG_LEVEL.INFO);

                    // OperationMode
                    targetPath = RecipePath + ".OperationMode";
                    Value = (UInt16)mappingRecipe.GeUInt16tOperationMode(recipeResponse.RECIPE_DATA.EQP_TYPE, recipeResponse.RECIPE_DATA.PROCESS_TYPE, recipeResponse.RECIPE_DATA.OPERATION_MODE);
                    writeItems.Add(targetPath, Value);
                    //Value = (UInt16)recipeResponse.RECIPE_DATA.OPERATION_MODE

                    _LOG_($"[WriteItem] {targetPath}:{recipeResponse.RECIPE_DATA.OPERATION_MODE}: {Value}", ECSLogger.LOG_LEVEL.INFO);
                }

                for (int i = 0; i < recipeResponse.RECIPE_DATA.RECIPE_ITEM.Count; i++)
                {
                    string strNAME = recipeResponse.RECIPE_DATA.RECIPE_ITEM[i].NAME;
                    object strVALUE = recipeResponse.RECIPE_DATA.RECIPE_ITEM[i].VALUE;
                    string strUNIT = recipeResponse.RECIPE_DATA.RECIPE_ITEM[i].UNIT;

                    if (RecipeDic.ContainsKey(strNAME))     // 20230331 msh : 조건 추가
                    {
                        MappingRecipeItem mappingCSV = RecipeDic[strNAME];

                        if (mappingCSV != null)
                        {
                            if (mappingDirection == MappingDirection.EqpToFms)
                                targetPath = RecipePath + $".{mappingCSV.FmsItem}";
                            else
                                targetPath = RecipePath + $".{mappingCSV.EqpItem}";
                            Value = MappingRecipeItem.SetObjectTypeCastByString(mappingCSV.DataType, strVALUE.ToString());
                            writeItems.Add(targetPath, Value);
                            _LOG_($"[WriteItem] {targetPath}:{Value}", ECSLogger.LOG_LEVEL.INFO);
                        }
                        else
                        {
                            _LOG_($"Not found recipe_item[{strNAME}] from MappingFile[{MappingFile}]", ECSLogger.LOG_LEVEL.WARN);
                        }
                    }
                    else
                    {
                        _LOG_($"Not found recipe_item[{strNAME}] from MappingFile[{MappingFile}]", ECSLogger.LOG_LEVEL.ERROR);
                    }
                }


                return WriteNodeWithDic(writeItems);
            }catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
        }

        private bool IsMultiOperationEQP(string EqpType)
        {
            switch (EqpType)
            {
                case "OCV":
                case "DCR":
                case "HPC":
                case "CHG":
                case "MIC":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 설비 OPCUA Server에 적혀있는 RecipeData를 읽는다. - 전 설비 공통
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public _jsonEcsApiMasterRecipeResponse ReadRecipeEQP(string TrayInformationPath, string RecipePath, bool aproEqp = false)
        {
            string targetPath = string.Empty;
            _jsonEcsApiMasterRecipeResponse RecipeEQP = new _jsonEcsApiMasterRecipeResponse();

            //public List<DataValue> ReadNodesByPathList(List<string> path, out List<BrowseNode> browseNodeOut, out List<ReadValueId> readValueIdOut)
            List<string> pathToReadList = new List<string>();

            // 공통
            //RecipeEQP.MODEL_ID - TrayInformation에서 가능
            pathToReadList.Add(TrayInformationPath + ".ProductModel");
            //RecipeEQP.ROUTE_ID - TrayInformation에서 가능
            pathToReadList.Add(TrayInformationPath + ".RouteId");
            //ProcessId  - TrayInformation에서 가능
            pathToReadList.Add(TrayInformationPath + ".ProcessId");

            //RecipeEQP.RECIPE_ID - Recipe에서 가능
            pathToReadList.Add(RecipePath + ".RecipeId");

            //APro장비가 아니면 레시피에 일부항목 없음
            if (aproEqp == true)
            {
                //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
                pathToReadList.Add(RecipePath + ".NextProcessExist");
                //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
                pathToReadList.Add(RecipePath + ".OperationMode");
            }


            //recipe_item 들은 CSV에서 읽는 것으로... 
            // 여기서 Recipe CSV 파일을 load하자. 
            string MappingFile = $@"Mapping\FMS-{EQPType}_SV.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_SV.csv 파일이 있어야 한다.
            MappingRecipeItem mappingRecipe = new MappingRecipeItem();
            Dictionary<string, MappingRecipeItem> RecipeDic = mappingRecipe.LoadRecipeMappingTable(MappingFile, MappingDirection.EqpToFms);
            //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)
            foreach(KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
            {
                pathToReadList.Add(RecipePath + "."+recipe_item.Key);
            }
            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
            if ( readDataValue == null)
            {
                _LOG_("Fail to read Recipe information from EQP", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            int TagIndex = 0;
            string ProcessId=string.Empty;
            UInt16 OperationMode = 0;

            //RecipeEQP.MODEL_ID - TrayInformation에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ProductModel"));
            if(TagIndex >= 0)
                RecipeEQP.MODEL_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            //RecipeEQP.ROUTE_ID - TrayInformation에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".RouteId"));
            if (TagIndex >= 0)
                RecipeEQP.ROUTE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            //ProcessId  - TrayInformation에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ProcessId"));
            if (TagIndex >= 0)
                ProcessId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            // 이건 데이터 읽은 후에
            //RecipeEQP.RECIPE_DATA.EQP_TYPE  substring of ProcessId
            //RecipeEQP.RECIPE_DATA.PROCESS_TYPE - substring 
            //RecipeEQP.RECIPE_DATA.PROCESS_NO - substring
            RecipeEQP.RECIPE_DATA = new RecipeBasicInfo();
            if(ProcessId.Length>=7)
            {
                RecipeEQP.RECIPE_DATA.EQP_TYPE = ProcessId.Substring(0,3);
                RecipeEQP.RECIPE_DATA.PROCESS_TYPE = ProcessId.Substring(3, 3);
                RecipeEQP.RECIPE_DATA.PROCESS_NO = int.Parse(ProcessId.Substring(6));
            }

            //RecipeEQP.RECIPE_ID - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".RecipeId"));
            if (TagIndex >= 0)
                //RecipeEQP.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                RecipeEQP.RECIPE_DATA.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".NextProcessExist"));
            if (TagIndex >= 0)
                RecipeEQP.RECIPE_DATA.NEXT_PROCESS_EXIST = readDataValue[TagIndex].Value != null ? ((Boolean)readDataValue[TagIndex].Value == true ? "Y" : "N") : "N";
            //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".OperationMode"));
            if (TagIndex >= 0)
                OperationMode = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
            RecipeEQP.RECIPE_DATA.OPERATION_MODE = mappingRecipe.GetStringOperationMode(RecipeEQP.RECIPE_DATA.EQP_TYPE, RecipeEQP.RECIPE_DATA.PROCESS_TYPE, OperationMode);

            //RecipeItem 들...
            //RecipeEQP.RECIPE_DATA.RECIPE_ITEM = new Dictionary<string, object>();
            //string DataType = string.Empty;
            //object Value = null;
            //foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
            //{
            //    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(recipe_item.Key));
            //    DataType = recipe_item.Value.DataType;
            //    Value = MappingRecipeItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);

            //    RecipeEQP.RECIPE_DATA.RECIPE_ITEM.Add(recipe_item.Key, Value);
            //}

            RecipeEQP.RECIPE_DATA.RECIPE_ITEM = new List<RecipeItem>();
            string DataType = string.Empty;
            object Value = null;
            foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
            {
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(recipe_item.Key));
                DataType = recipe_item.Value.DataType;
                Value = MappingRecipeItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);

                RecipeItem item = new RecipeItem();
                item.NAME = recipe_item.Key;
                item.VALUE = Value;
                item.UNIT = string.Empty;

                RecipeEQP.RECIPE_DATA.RECIPE_ITEM.Add(item);
            }



            return RecipeEQP;
        }

        public bool ClearOneCellInfoNGSEQP(string CellInformationPath, _SorterTrayCellInformation PickTrayCellInfo, NGSCellWork CellWork)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            int TargetIndex = CellWork.FromCellPosition - 1;

            targetPath = CellInformationPath + ".CellCount";
            //20230403 sgh typecase 문제로 괄호 처리
            writeItems.Add(targetPath, (UInt16)(PickTrayCellInfo.CellCount -1));

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.CellExist";
            writeItems.Add(targetPath, (Boolean)false);

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.CellId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.LotId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.NGCode";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.NGType";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = CellInformationPath + $".Cell._{TargetIndex}.DefectType";
            writeItems.Add(targetPath, (String)String.Empty);

            return WriteNodeWithDic(writeItems);
        }

        public bool ClearOneCellInfoNGSFMS(string TrayInformationPath, _SorterTrayCellInformation PickTrayCellInfo, NGSCellWork CellWork)
        {
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            int TargetIndex = CellWork.FromCellPosition - 1;

            targetPath = TrayInformationPath + ".CellCount";
            //20230403 sgh typecase 문제로 괄호 처리
            writeItems.Add(targetPath, (UInt16)(PickTrayCellInfo.CellCount - 1));

            targetPath = TrayInformationPath + $".CellInformation.Cell{string.Format("{0:D2}", CellWork.FromCellPosition)}.CellExist";
            writeItems.Add(targetPath, (Boolean)false);

            targetPath = TrayInformationPath + $".CellInformation.Cell{string.Format("{0:D2}", CellWork.FromCellPosition)}.CellId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = TrayInformationPath + $".CellInformation.Cell{string.Format("{0:D2}", CellWork.FromCellPosition)}.LotId";
            writeItems.Add(targetPath, (String)String.Empty);

            //targetPath = TrayInformationPath + $".CellInformation.Cell._{TargetIndex}.NGCode";
            //writeItems.Add(targetPath, (String)String.Empty);

            //targetPath = TrayInformationPath + $".CellInformation.Cell._{TargetIndex}.NGType";
            //writeItems.Add(targetPath, (String)String.Empty);

            //targetPath = TrayInformationPath + $".CellInformation.Cell._{TargetIndex}.DefectType";
            //writeItems.Add(targetPath, (String)String.Empty);

            return WriteNodeWithDic(writeItems);
        }

        public bool WriteNGSPlaceTrayInfoEQP(string TrayInformationPath, _SorterTrayCellInformation PickTrayCellInfo, NGSCellWork CellWork)
        {
            // TrayGrade, DefectType, ProductModel, RouteId, ProcessId, LotId
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            int FromIndex = CellWork.FromCellPosition - 1;

            if (PickTrayCellInfo.ProcessType == 1) //NG Sorting
            {
                targetPath = TrayInformationPath + ".DefectType";
                writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].DefectType);

                targetPath = TrayInformationPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)String.Empty);
            } else // Grading
            {
                targetPath = TrayInformationPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGCode);

                targetPath = TrayInformationPath + ".DefectType";
                writeItems.Add(targetPath, (String)String.Empty);
            }
            targetPath = TrayInformationPath + ".ProductModel";
            writeItems.Add(targetPath, (String)PickTrayCellInfo.ProductModel);

            targetPath = TrayInformationPath + ".RouteId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = TrayInformationPath + ".ProcessId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = TrayInformationPath + ".LotId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].LotId);


            return WriteNodeWithDic(writeItems);

        }

        public bool WriteNGSPlaceTrayInfoFMS(string TrayInformationPath, _SorterTrayCellInformation PickTrayCellInfo, NGSCellWork CellWork)
        {
            // TrayGrade, DefectType, ProductModel, RouteId, ProcessId, LotId
            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            int FromIndex = CellWork.FromCellPosition - 1;

            if (PickTrayCellInfo.ProcessType == 1) //NG Sorting
            {
                targetPath = TrayInformationPath + ".DefectType";
                writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].DefectType);

                targetPath = TrayInformationPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)String.Empty);
            }
            else // Grading
            {
                targetPath = TrayInformationPath + ".TrayGrade";
                writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGCode);

                targetPath = TrayInformationPath + ".DefectType";
                writeItems.Add(targetPath, (String)String.Empty);
            }
            targetPath = TrayInformationPath + ".Model";
            writeItems.Add(targetPath, (String)PickTrayCellInfo.ProductModel);

            targetPath = TrayInformationPath + ".RouteId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = TrayInformationPath + ".ProcessId";
            writeItems.Add(targetPath, (String)String.Empty);

            targetPath = TrayInformationPath + ".LotId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].LotId);


            return WriteNodeWithDic(writeItems);

        }

        public bool WriteNGSCellFromPickToPlaceEQP(_SorterTrayCellInformation PickTrayCellInfo, _SorterTrayCellInformation PlaceTrayCellInfo, NGSCellWork CellWork)
        {
            int FromIndex = CellWork.FromCellPosition - 1;
            int ToIndex = CellWork.ToCellPosition - 1;

            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".CellExist";
            writeItems.Add(targetPath, (Boolean)PickTrayCellInfo._CellList[FromIndex].CellExist);

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".CellId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].CellId);

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".LotId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].LotId);

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".NGCode";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGCode);

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".NGType";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGType);

            targetPath = $"PlaceLocation.CellInformation.Cell._{ToIndex}" + ".DefectType";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].DefectType);

            //CellCount
            targetPath = "PlaceLocation.CellInformation.CellCount";
            //20230403 sgh typecase 문제로 괄호 처리
            writeItems.Add(targetPath, (UInt16)(PlaceTrayCellInfo.CellCount + 1));


            return WriteNodeWithDic(writeItems);
        }

        public bool WriteNGSCellFromPickToPlaceFMS(_SorterTrayCellInformation PickTrayCellInfo, _SorterTrayCellInformation PlaceTrayCellInfo, NGSCellWork CellWork)
        {
            int FromIndex = CellWork.FromCellPosition - 1;
            int ToIndex = CellWork.ToCellPosition - 1;

            string targetPath = string.Empty;
            Dictionary<string, object> writeItems = new Dictionary<string, object>();
            string CellPath = string.Empty;

            targetPath = $"Location2.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".CellExist";
            writeItems.Add(targetPath, (Boolean)PickTrayCellInfo._CellList[FromIndex].CellExist);

            targetPath = $"Location2.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".CellId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].CellId);

            targetPath = $"Location2.TrayInformation.CellInformation.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".LotId";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].LotId);


            targetPath = $"Location2.TrayInformation.CellGrade.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".NGCode";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGCode);

            targetPath = $"Location2.TrayInformation.CellGrade.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".NGType";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].NGType);

            targetPath = $"Location2.TrayInformation.CellGrade.Cell{string.Format("{0:D2}", CellWork.ToCellPosition)}" + ".DefectType";
            writeItems.Add(targetPath, (String)PickTrayCellInfo._CellList[FromIndex].DefectType);

            //CellCount
            targetPath = "Location2.TrayInformation.CellCount";
            //20230403 sgh typecase 문제로 괄호 처리
            writeItems.Add(targetPath, (UInt16)(PlaceTrayCellInfo.CellCount + 1));

            return WriteNodeWithDic(writeItems);
        }

        public NGSCellWork ReadNGSCellWork(string CellWorkPath)
        {
            List<string> pathToReadList = new List<string>();

            NGSCellWork CellWork = new NGSCellWork();

            pathToReadList.Add(CellWorkPath + ".FromCellPosition");
            pathToReadList.Add(CellWorkPath + ".FromWorkRequest");
            pathToReadList.Add(CellWorkPath + ".FromWorkResponse");
            pathToReadList.Add(CellWorkPath + ".CellId");
            pathToReadList.Add(CellWorkPath + ".ToWorkRequest");
            pathToReadList.Add(CellWorkPath + ".ToCellPosition");
            pathToReadList.Add(CellWorkPath + ".ToWorkResponse");
            pathToReadList.Add(CellWorkPath + ".WorkComplete");
            pathToReadList.Add(CellWorkPath + ".WorkCompleteResponse");

            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
            if (readDataValue == null)
            {
                _LOG_("Fail to read Recipe information from EQP", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            int TagIndex = 0;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".FromCellPosition"));
            if (TagIndex >= 0)
                CellWork.FromCellPosition = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".FromWorkRequest"));
            if (TagIndex >= 0)
                CellWork.FromWorkRequest = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".FromWorkResponse"));
            if (TagIndex >= 0)
                CellWork.FromWorkResponse = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".CellId"));
            if (TagIndex >= 0)
                CellWork.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ToWorkRequest"));
            if (TagIndex >= 0)
                CellWork.ToWorkRequest = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ToCellPosition"));
            if (TagIndex >= 0)
                CellWork.ToCellPosition = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ToWorkResponse"));
            if (TagIndex >= 0)
                CellWork.ToWorkResponse = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".WorkComplete"));
            if (TagIndex >= 0)
                CellWork.WorkComplete = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".WorkCompleteResponse"));
            if (TagIndex >= 0)
                CellWork.WorkCompleteResponse = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;

            return CellWork;
        }

        public TrayRequest ReadEQPTrayRequest(string TrayRequestPath)
        {
            List<string> pathToReadList = new List<string>();

            TrayRequest trayRequest = new TrayRequest();

            pathToReadList.Add(TrayRequestPath + ".Grade");
            pathToReadList.Add(TrayRequestPath + ".DefectType");
            pathToReadList.Add(TrayRequestPath + ".TrayType");
            pathToReadList.Add(TrayRequestPath + ".ProductModel");
            pathToReadList.Add(TrayRequestPath + ".TrayLoadRequest");
            pathToReadList.Add(TrayRequestPath + ".ReservedFlag");
            pathToReadList.Add(TrayRequestPath + ".ReservedTrayId");

            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
            if (readDataValue == null)
            {
                _LOG_("Fail to read Recipe information from EQP", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            int TagIndex = 0;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".Grade"));
            if (TagIndex >= 0)
                trayRequest.Grade = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".DefectType"));
            if (TagIndex >= 0)
                trayRequest.DefectType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".TrayType"));
            if (TagIndex >= 0)
                trayRequest.TrayType = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ProductModel"));
            if (TagIndex >= 0)
                trayRequest.ProductModel = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".TrayLoadRequest"));
            if (TagIndex >= 0)
                trayRequest.TrayLoadRequest = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ReservedFlag"));
            if (TagIndex >= 0)
                trayRequest.ReservedFlag = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".ReservedTrayId"));
            if (TagIndex >= 0)
                trayRequest.ReservedTrayId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

            return trayRequest;

        }

        public _jsonEcsApiMasterRecipeResponse ReadRecipeEQP(string RecipePath, _dat_tray TrayInfo)
        {
            string targetPath = string.Empty;
            _jsonEcsApiMasterRecipeResponse RecipeEQP = new _jsonEcsApiMasterRecipeResponse();

            //public List<DataValue> ReadNodesByPathList(List<string> path, out List<BrowseNode> browseNodeOut, out List<ReadValueId> readValueIdOut)
            List<string> pathToReadList = new List<string>();


            //RecipeEQP.RECIPE_ID - Recipe에서 가능
            pathToReadList.Add(RecipePath + ".RecipeId");
            //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
            pathToReadList.Add(RecipePath + ".NextProcessExist");
            //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
            pathToReadList.Add(RecipePath + ".OperationMode");


            //recipe_item 들은 CSV에서 읽는 것으로... 
            // 여기서 Recipe CSV 파일을 load하자. 
            string MappingFile = $@"Mapping\FMS-{EQPType}_SV.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_SV.csv 파일이 있어야 한다.
            MappingRecipeItem mappingRecipe = new MappingRecipeItem();
            Dictionary<string, MappingRecipeItem> RecipeDic = mappingRecipe.LoadRecipeMappingTable(MappingFile, MappingDirection.EqpToFms);
            //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)
            foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
            {
                pathToReadList.Add(RecipePath + "." + recipe_item.Key);
            }
            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
            if (readDataValue == null)
            {
                _LOG_("Fail to read Recipe information from EQP", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            int TagIndex = 0;
            string ProcessId = string.Empty;
            UInt16 OperationMode = 0;

            //RecipeEQP.MODEL_ID - TrayInformation에서 가능
            RecipeEQP.MODEL_ID = TrayInfo.MODEL_ID;
            RecipeEQP.ROUTE_ID = TrayInfo.ROUTE_ID;
            RecipeEQP.RECIPE_DATA.EQP_TYPE = TrayInfo.EQP_TYPE;
            RecipeEQP.RECIPE_DATA.PROCESS_TYPE = TrayInfo.PROCESS_TYPE;
            RecipeEQP.RECIPE_DATA.PROCESS_NO = TrayInfo.PROCESS_NO;


            //RecipeEQP.RECIPE_ID - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".RecipeId"));
            if (TagIndex >= 0)
                //RecipeEQP.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                RecipeEQP.RECIPE_DATA.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
            //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".NextProcessExist"));
            if (TagIndex >= 0)
                RecipeEQP.RECIPE_DATA.NEXT_PROCESS_EXIST = readDataValue[TagIndex].Value != null ? ((Boolean)readDataValue[TagIndex].Value == true ? "Y" : "N") : "N";
            //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".OperationMode"));
            if (TagIndex >= 0)
                OperationMode = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
            RecipeEQP.RECIPE_DATA.OPERATION_MODE = mappingRecipe.GetStringOperationMode(RecipeEQP.RECIPE_DATA.EQP_TYPE, RecipeEQP.RECIPE_DATA.PROCESS_TYPE, OperationMode);


            RecipeEQP.RECIPE_DATA.RECIPE_ITEM = new List<RecipeItem>();
            string DataType = string.Empty;
            object Value = null;
            foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
            {
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(recipe_item.Key));
                DataType = recipe_item.Value.DataType;
                Value = MappingRecipeItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);

                RecipeItem item = new RecipeItem();
                item.NAME = recipe_item.Key;
                item.VALUE = Value;
                item.UNIT = string.Empty;

                RecipeEQP.RECIPE_DATA.RECIPE_ITEM.Add(item);
            }



            return RecipeEQP;
        }

        

        public _jsonEcsApiMasterRecipeResponse ReadRecipeMES(string RecipePath)
        {
            try
            {
                string targetPath = string.Empty;
                _jsonEcsApiMasterRecipeResponse RecipeMES = new _jsonEcsApiMasterRecipeResponse();

                //public List<DataValue> ReadNodesByPathList(List<string> path, out List<BrowseNode> browseNodeOut, out List<ReadValueId> readValueIdOut)
                List<string> pathToReadList = new List<string>();


                //RecipeEQP.RECIPE_ID - Recipe에서 가능
                pathToReadList.Add(RecipePath + ".RecipeId");
                //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
                pathToReadList.Add(RecipePath + ".NextProcessExist");
                //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
                pathToReadList.Add(RecipePath + ".OperationMode");


                //recipe_item 들은 CSV에서 읽는 것으로... 
                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_SV.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_SV.csv 파일이 있어야 한다.
                MappingRecipeItem mappingRecipe = new MappingRecipeItem();
                Dictionary<string, MappingRecipeItem> RecipeDic = mappingRecipe.LoadRecipeMappingTable(MappingFile, MappingDirection.FmsToEqp);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 FMS의 TagName 기준으로 저장한다)
                foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
                {
                    pathToReadList.Add(RecipePath + "." + recipe_item.Key);
                }
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_("Fail to read Recipe information from EQP", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                int TagIndex = 0;
                string ProcessId = string.Empty;
                UInt16 OperationMode = 0;

                RecipeMES.RECIPE_DATA = new RecipeBasicInfo();

                //RecipeEQP.RECIPE_ID - Recipe에서 가능
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".RecipeId"));
                if (TagIndex >= 0)
                    //RecipeEQP.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    RecipeMES.RECIPE_DATA.RECIPE_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //RecipeEQP.RECIPE_DATA.NEXT_OPERATION_EXIST - Recipe에서 가능
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".NextProcessExist"));
                if (TagIndex >= 0)
                    RecipeMES.RECIPE_DATA.NEXT_PROCESS_EXIST = readDataValue[TagIndex].Value != null ? ((Boolean)readDataValue[TagIndex].Value == true ? "Y" : "N") : "N";
                //RecipeEQP.RECIPE_DATA.OPERATION_MODE - Recipe에서 가능
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(".OperationMode"));
                if (TagIndex >= 0)
                    OperationMode = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;

                //20230203 KJY - RecipeId로 부터 parsing ModelID_RouteID_processId_version
                if(RecipeMES.RECIPE_DATA.RECIPE_ID == null || RecipeMES.RECIPE_DATA.RECIPE_ID.Length <1)
                {
                    _LOG_("Fail to read RecipeId from FMS OPCUA Server", ECSLogger.LOG_LEVEL.ERROR);
                }

                ParseRecipeId(RecipeMES);
                RecipeMES.RECIPE_DATA.OPERATION_MODE = mappingRecipe.GetStringOperationMode(RecipeMES.RECIPE_DATA.EQP_TYPE, RecipeMES.RECIPE_DATA.PROCESS_TYPE, OperationMode);

                //RecipeItem 들...
                //RecipeMES.RECIPE_DATA.RECIPE_ITEM = new Dictionary<string, object>();
                RecipeMES.RECIPE_DATA.RECIPE_ITEM = new List<RecipeItem>();
                string DataType = string.Empty;
                object Value = null;
                foreach (KeyValuePair<string, MappingRecipeItem> recipe_item in RecipeDic)
                {
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(recipe_item.Key));
                    DataType = recipe_item.Value.DataType;
                    Value = MappingRecipeItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);

                    RecipeItem item = new RecipeItem();
                    item.NAME = recipe_item.Key;
                    item.VALUE = Value;
                    item.UNIT = String.Empty;

                    //RecipeMES.RECIPE_DATA.RECIPE_ITEM.Add(recipe_item.Key, Value);
                    RecipeMES.RECIPE_DATA.RECIPE_ITEM.Add(item);
                }


                RecipeMES.RECIPE_DATA.EQP_TYPE = EQPType;

                return RecipeMES;
            } catch(Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        private void ParseRecipeId(_jsonEcsApiMasterRecipeResponse recipeMES)
        {
            string RecipeId = recipeMES.RECIPE_DATA.RECIPE_ID;

            string[] splitRecipeId = RecipeId.Split('_');

            string ProcessId = string.Empty;

            if(splitRecipeId.Length > 3)
            {
                ProcessId = splitRecipeId[2];

                if(ProcessId.Length > 7)
                {
                    recipeMES.RECIPE_DATA.EQP_TYPE = ProcessId.Substring(0, 3);
                    recipeMES.RECIPE_DATA.PROCESS_TYPE = ProcessId.Substring(3, 3);
                    recipeMES.RECIPE_DATA.PROCESS_NO = int.Parse(ProcessId.Substring(6));


                    _LOG_($"ProcessId : {ProcessId}, EQPType: {recipeMES.RECIPE_DATA.EQP_TYPE}, ProcessType : {recipeMES.RECIPE_DATA.PROCESS_TYPE}, ProcessNo : {recipeMES.RECIPE_DATA.PROCESS_NO}");
                }
            }
        }

        /// <summary>
        /// Level1, Level2로 구분되어 있을때만 사용한다.
        /// </summary>
        /// <param name="TrayPath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> ReadTrayList(string TrayPath)
        {
            List<string> pathToReadList = new List<string>();

            pathToReadList.Add(TrayPath + ".TrayCount");
            pathToReadList.Add(TrayPath + ".Level1.TrayId");
            pathToReadList.Add(TrayPath + ".Level2.TrayId");

            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);

            // TrayCount
            int TrayCount = readDataValue[0].Value !=null ? (UInt16)readDataValue[0].Value : 0;
            if(TrayCount <1)
            {
                _LOG_("Tray Count is below 1", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            List<string> TrayList = new List<string>();
            TrayList.Add(readDataValue[1].Value != null ? (string)readDataValue[1].Value : string.Empty);
            if(TrayCount > 1)
                TrayList.Add(readDataValue[2].Value != null ? (string)readDataValue[2].Value : string.Empty);

            return TrayList;
        }
        /// <summary>
        /// 설비 OPCUA의 Cell 정보를 Clear.  개발 Cell형식은 _0, _1, _2, ....
        /// </summary>
        /// <param name="CellListPath"></param>
        /// <param name="CellItemDic"></param>
        /// <param name="CellCount"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool ClearEQPAllCellInformation(string CellListPath, Dictionary<string, string>CellItemDic, int CellCount)
        {
            try
            {
                Dictionary<string, object> writeItems = new Dictionary<string, object>();
                string cellPath = string.Empty;
                string targetPath = string.Empty;

                for (int i = 0; i < CellCount; i++)
                {
                    cellPath = CellListPath + $"._{i}";
                    foreach (KeyValuePair<string, string> item in CellItemDic)
                    {
                        targetPath = cellPath + $".{item.Key}";
                        writeItems.Add(targetPath, SetClearObjectTypeCast(item.Value));
                    }
                }

                return WriteNodeWithDic(writeItems);
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
        }
        public bool ClearFMSAllCellInformation(string CellListPath, Dictionary<string, string> CellItemDic, int CellCount)
        {
            try
            {
                Dictionary<string, object> writeItems = new Dictionary<string, object>();
                string cellPath = string.Empty;
                string targetPath = string.Empty;

                for (int i = 0; i < CellCount; i++)
                {
                    if(CellCount>100)
                        cellPath = CellListPath + string.Format(".Cell{0:D3}", i + 1);
                    else
                        cellPath = CellListPath + string.Format(".Cell{0:D2}", i + 1);

                    foreach (KeyValuePair<string, string> item in CellItemDic)
                    {
                        targetPath = cellPath + $".{item.Key}";
                        writeItems.Add(targetPath, SetClearObjectTypeCast(item.Value));
                    }
                }

                return WriteNodeWithDic(writeItems);
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] : {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
        }
        public static object SetClearObjectTypeCast(string dataType)
        {

            switch (dataType.ToUpper())
            {
                case "UINT16":
                    return (UInt16)0;
                case "UINT32":
                    return (UInt32)0;
                case "STRING":
                    return (String)String.Empty;
                case "BOOLEAN":
                    return (Boolean)false;
                case "FLOAT":
                    return (float)0.0;
                default:
                    return null;
            }
        }


        /// <summary>
        /// Cell 정보 받아오는데.... ProcessData 값은 DataValue 로 그대로 가져온다. FMS에 그대로 쓸꺼야.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public _CHGTrackOutCellInformation Read2LevelProcessDataEQP(string TrackOutCellInformationPath, string TrayInformationPath)
        {
            // 20230301 KJY - Cell 공통 데이터에 대한 처리
            _CHGTrackOutCellInformation TrackOutCellInfo = new _CHGTrackOutCellInformation();

            List<string> pathToReadList = new List<string>();
            string CellPath = string.Empty;

            try
            {

                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 FMS의 TagName 기준으로 저장한다)            


                //기본정보
                //20230201 - KJY Level1, Level2 의 StartTemp, EndTemp, AvgTemp 를 읽어서 각 Cell에 써야 한다.
                pathToReadList.Add(TrackOutCellInformationPath + ".Level1.CellCount");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level1.StartTemp");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level1.EndTemp");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level1.AvgTemp");

                pathToReadList.Add(TrackOutCellInformationPath + ".Level2.CellCount");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level2.StartTemp");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level2.EndTemp");
                //pathToReadList.Add(TrackOutCellInformationPath + ".Level2.AvgTemp");
                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    if (pd_item.Value.CommonFlag == "C")
                    {
                        pathToReadList.Add(TrackOutCellInformationPath + ".Level1." + pd_item.Key);
                        pathToReadList.Add(TrackOutCellInformationPath + ".Level2." + pd_item.Key);
                    }
                }

                // 20230324 msh : Read2LevelProcessDataEQP 호출이후로 변경
                //pathToReadList.Add(TrayInformationPath + ".ProcessId");

                for (int i = 0; i < 60; i++)
                {
                    CellPath = TrackOutCellInformationPath + $".Cell._{i}";
                    pathToReadList.Add(CellPath + ".CellExist");
                    pathToReadList.Add(CellPath + ".CellId");
                    pathToReadList.Add(CellPath + ".LotId");
                    pathToReadList.Add(CellPath + ".NGCode");
                    pathToReadList.Add(CellPath + ".NGType");

                    foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    {
                        if (pd_item.Value.CommonFlag != "C") // 20230301 KJY - Cell 공통 PD 는 제외
                            pathToReadList.Add(CellPath + "." + pd_item.Key);
                    }
                }

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{TrackOutCellInformationPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }




                // 이제 TrackOutCellInfo의 값을 채워야 한다.
                int TagIndex;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level1.CellCount"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.CellCount_Level1 = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
                _LOG_($"[Level1.CellCount] : {TrackOutCellInfo.CellCount_Level1}");
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level2.CellCount"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.CellCount_Level2 = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
                // 20230201 KJY - StartTemp,EndTemp,AvgTemp
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level1.StartTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.StartTemp_Level1 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level1.EndTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.EndTemp_Level1 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level1.AvgTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.AvgTemp_Level1 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level2.StartTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.StartTemp_Level2 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level2.EndTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.EndTemp_Level2 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("Level2.AvgTemp"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.AvgTemp_Level2 = readDataValue[TagIndex].Value != null ? (float)readDataValue[TagIndex].Value : (float)0;


                // 20230324 msh : Read2LevelProcessDataEQP 호출이후로 변경
                //ProcessId
                //TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TrayInformationPath + ".ProcessId"));
                //if (TagIndex >= 0)
                //    TrackOutCellInfo.ProcessId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                TrackOutCellInfo._CellList = new List<_CellProcessData>();
                for (int i = 0; i < 60; i++)
                {
                    _CellProcessData Cell = new _CellProcessData();
                    Cell.ProcessData = new Dictionary<string, object>();
                    Cell.readDataValue = new Dictionary<string, DataValue>();


                    CellPath = TrackOutCellInformationPath + $".Cell._{i}";
                    //CellExist
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellExist"));
                    if (TagIndex >= 0)
                        Cell.CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
                    if (Cell.CellExist == false)
                    {
                        TrackOutCellInfo._CellList.Add(Cell); // CellExist가 false 일때도 add 한다.
                        continue;
                    }
                    //CellId
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellId"));
                    if (TagIndex >= 0)
                        Cell.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //LotId
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".LotId"));
                    if (TagIndex >= 0)
                        Cell.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGCode
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".NGCode"));
                    if (TagIndex >= 0)
                        Cell.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGType
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".NGType"));
                    if (TagIndex >= 0)
                        Cell.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                    string DataType;
                    object Value;
                    foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    {
                        if (pd_item.Value.CommonFlag != "C") //20230301 KJY - 공통 PD가 아닌것만
                        {
                            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + $".{pd_item.Key}"));
                            if (TagIndex >= 0)
                            {
                                DataType = pd_item.Value.DataType;
                                Value = MappingProcessDataItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);
                                Cell.ProcessData.Add(pd_item.Key, Value);
                                Cell.readDataValue.Add(pd_item.Key, readDataValue[TagIndex]);
                            }
                        }
                    }
                    TrackOutCellInfo._CellList.Add(Cell);
                }


                return TrackOutCellInfo;
            } catch(Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public List<PackingCellInfo> ReadPackingCellList(string PackingCellInformationPath)
        {
            try
            {
                List<PackingCellInfo> PACKING_CELL_LIST = new List<PackingCellInfo>();
                List<string> pathToReadList = new List<string>();
                string CellPath = string.Empty;
                string TargetPath = string.Empty;

                for (int i = 0; i < 150; i++)
                {
                    CellPath = PackingCellInformationPath + $".Cell._{i}";

                    TargetPath = CellPath + ".CellExist";
                    pathToReadList.Add(TargetPath);
                    TargetPath = CellPath + ".CellId";
                    pathToReadList.Add(TargetPath);
                    TargetPath = CellPath + ".LotId";
                    pathToReadList.Add(TargetPath);
                    TargetPath = CellPath + ".DefectType";
                    pathToReadList.Add(TargetPath);
                    TargetPath = CellPath + ".Floor";
                    pathToReadList.Add(TargetPath);
                    TargetPath = CellPath + ".Position";
                    pathToReadList.Add(TargetPath);
                }

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{PackingCellInformationPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                int TagIndex;
                Boolean CellExist;
                for (int i = 0; i < 150; i++)
                {
                    PackingCellInfo PackingCell = new PackingCellInfo();
                    CellPath = PackingCellInformationPath + $".Cell._{i}";

                    PackingCell.INDEX = i + 1;

                    TargetPath = CellPath + ".CellExist";
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TargetPath));
                    if (TagIndex >= 0)
                        CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
                    else
                        CellExist = false;

                    if (CellExist)
                    {
                        TargetPath = CellPath + ".CellId";
                        TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TargetPath));
                        if (TagIndex >= 0)
                            PackingCell.CELL_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                        TargetPath = CellPath + ".LotId";
                        TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TargetPath));
                        if (TagIndex >= 0)
                            PackingCell.LOT_ID = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                        TargetPath = CellPath + ".Floor";
                        TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TargetPath));
                        if (TagIndex >= 0)
                            PackingCell.TRAY_POSITION = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;

                        TargetPath = CellPath + ".Position";
                        TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TargetPath));
                        if (TagIndex >= 0)
                            PackingCell.CELL_POSITION = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;
                    }

                    PACKING_CELL_LIST.Add(PackingCell);
                }

                return PACKING_CELL_LIST;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public _CellInformation ReadProcessDataEQP_OCV(string TrackOutCellInformationPath)
        {
            try
            {
                // 20230301 - KJY ProcessData중 Cell별로 공통값이 들어가는 것들이 있음. 이건 EQP에서 한번 받아서 FMS에는 셀별로 모두 써줘야 함.
                //TrayInformationPath 이 null이면 ProcessId값은 따로 읽지 않는다.

                _CellInformation TrackOutCellInfo = new _CellInformation();

                List<string> pathToReadList = new List<string>();
                string CellPath = string.Empty;

                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms);

                // 한번에 읽어옴.             
                List<DataValue> readDataValue = ReadNodes(m_ProcessDataNodeList.NodeList);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{TrackOutCellInformationPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (m_ProcessDataNodeList.NodeList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, ProcessDataNodeList.NodeList[{m_ProcessDataNodeList.NodeList.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                // 이제 TrackOutCellInfo의 값을 채워야 한다.
                int TagIndex;
                TagIndex = m_ProcessDataNodeList.NodeIndex["Location1.TrackOutCellInformation.CellCount"];
                if (TagIndex >= 0)
                    TrackOutCellInfo.CellCount = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;

                //20230301 KJY - 공통 PD 처리를 위해서.... 
                string DataTypeC;
                List<string> ItemKeyC = new List<string>();
                List<object> ValueC = new List<object>();
                List<DataValue> DataValueC = new List<DataValue>();


                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    if (pd_item.Value.CommonFlag == "C")
                    {
                        // 20230327 msh : CellPath 오류 수정.
                        CellPath = TrackOutCellInformationPath + $".{pd_item.Key}";
                        TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath];
                        if (TagIndex >= 0)
                        {
                            DataTypeC = pd_item.Value.DataType;

                            ItemKeyC.Add(pd_item.Key);
                            ValueC.Add(MappingProcessDataItem.SetObjectTypeCast(DataTypeC, readDataValue[TagIndex].Value));
                            DataValueC.Add(readDataValue[TagIndex]);
                        }
                    }
                }




                TrackOutCellInfo._CellList = new List<_CellProcessData>();
                for (int i = 0; i < 30; i++)
                {
                    _CellProcessData Cell = new _CellProcessData();
                    Cell.ProcessData = new Dictionary<string, object>();
                    Cell.readDataValue = new Dictionary<string, DataValue>();


                    CellPath = TrackOutCellInformationPath + $".Cell._{i}";
                    //CellExist
                    TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + ".CellExist"];
                    if (TagIndex >= 0)
                        Cell.CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
                    if (Cell.CellExist == false)
                    {
                        TrackOutCellInfo._CellList.Add(Cell); // CellExist가 false 일때도 add 한다.
                        continue;
                    }
                    //CellId
                    TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + ".CellId"];
                    if (TagIndex >= 0)
                        Cell.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //LotId
                    TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + ".LotId"];
                    if (TagIndex >= 0)
                        Cell.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGCode
                    TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + ".NGCode"];
                    if (TagIndex >= 0)
                        Cell.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGType
                    TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + ".NGType"];
                    if (TagIndex >= 0)
                        Cell.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                    string DataType;
                    object Value;
                    foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    {
                        if (pd_item.Value.CommonFlag != "C") //20230301 KJY - 공통 PD가 아닌것만
                        {
                            TagIndex = m_ProcessDataNodeList.NodeIndex[CellPath + $".{pd_item.Key}"];
                            if (TagIndex >= 0)
                            {
                                DataType = pd_item.Value.DataType;
                                Value = MappingProcessDataItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);
                                Cell.ProcessData.Add(pd_item.Key, Value);
                                Cell.readDataValue.Add(pd_item.Key, readDataValue[TagIndex]);
                            }
                        }
                    }

                    //20230301 KJY - 공통 PD들 추가한다.
                    // 20230327 msh : i++ -> j++ 수정
                    for (int j = 0; j < ItemKeyC.Count; j++)
                    {
                        Cell.ProcessData.Add(ItemKeyC[j], ValueC[j]);
                        Cell.readDataValue.Add(ItemKeyC[j], DataValueC[j]);
                    }

                    TrackOutCellInfo._CellList.Add(Cell);
                }


                return TrackOutCellInfo;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public _CellInformation ReadProcessDataEQP(string TrackOutCellInformationPath, string TrayInformationPath = null)
        {
            try
            {
                // 20230301 - KJY ProcessData중 Cell별로 공통값이 들어가는 것들이 있음. 이건 EQP에서 한번 받아서 FMS에는 셀별로 모두 써줘야 함.
                //TrayInformationPath 이 null이면 ProcessId값은 따로 읽지 않는다.

                _CellInformation TrackOutCellInfo = new _CellInformation();

                List<string> pathToReadList = new List<string>();
                string CellPath = string.Empty;

                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)            

                //기본정보
                pathToReadList.Add(TrackOutCellInformationPath + ".CellCount");
                // 20230324 msh : GetMasterNextProcessInfo()에서 ProcessId를 가져온다.
                //if (TrayInformationPath != null) pathToReadList.Add(TrayInformationPath + ".ProcessId");

                // 20230301 KJY - Cell 공통 PD 처리를 위해.
                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    if (pd_item.Value.CommonFlag == "C")
                        pathToReadList.Add(TrackOutCellInformationPath + "." + pd_item.Key);
                }

                for (int i = 0; i < 30; i++)
                {
                    CellPath = TrackOutCellInformationPath + $".Cell._{i}";
                    pathToReadList.Add(CellPath + ".CellExist");
                    pathToReadList.Add(CellPath + ".CellId");
                    pathToReadList.Add(CellPath + ".LotId");
                    pathToReadList.Add(CellPath + ".NGCode");
                    pathToReadList.Add(CellPath + ".NGType");

                    foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    {
                        if (pd_item.Value.CommonFlag != "C") // 20230301 KJY - Cell 공통 PD 는 제외
                            pathToReadList.Add(CellPath + "." + pd_item.Key);
                    }
                }

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{TrackOutCellInformationPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                // 이제 TrackOutCellInfo의 값을 채워야 한다.
                int TagIndex;
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith("CellCount"));
                if (TagIndex >= 0)
                    TrackOutCellInfo.CellCount = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;

                //ProcessId
                // 20230324 msh : GetMasterNextProcessInfo()에서 ProcessId를 가져온다.
                //if (TrayInformationPath != null)
                //{
                //    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TrayInformationPath + ".ProcessId"));
                //    if (TagIndex >= 0)
                //        TrackOutCellInfo.ProcessId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //}

                //20230301 KJY - 공통 PD 처리를 위해서.... 
                string DataTypeC;
                List<string> ItemKeyC = new List<string>();
                List<object> ValueC = new List<object>();
                List<DataValue> DataValueC = new List<DataValue>();


                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    if (pd_item.Value.CommonFlag == "C")
                    {
                        // 20230327 msh : CellPath 오류 수정. CellPath -> TrackOutCellInformationPath
                        TagIndex = pathToReadList.FindIndex(x => x.EndsWith(TrackOutCellInformationPath + $".{pd_item.Key}"));
                        if (TagIndex >= 0)
                        {
                            DataTypeC = pd_item.Value.DataType;

                            ItemKeyC.Add(pd_item.Key);
                            ValueC.Add(MappingProcessDataItem.SetObjectTypeCast(DataTypeC, readDataValue[TagIndex].Value));
                            DataValueC.Add(readDataValue[TagIndex]);
                        }
                    }
                }




                TrackOutCellInfo._CellList = new List<_CellProcessData>();
                for (int i = 0; i < 30; i++)
                {
                    _CellProcessData Cell = new _CellProcessData();
                    Cell.ProcessData = new Dictionary<string, object>();
                    Cell.readDataValue = new Dictionary<string, DataValue>();


                    CellPath = TrackOutCellInformationPath + $".Cell._{i}";
                    //CellExist
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellExist"));
                    if (TagIndex >= 0)
                        Cell.CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
                    if (Cell.CellExist == false)
                    {
                        TrackOutCellInfo._CellList.Add(Cell); // CellExist가 false 일때도 add 한다.
                        continue;
                    }
                    //CellId
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellId"));
                    if (TagIndex >= 0)
                        Cell.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //LotId
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".LotId"));
                    if (TagIndex >= 0)
                        Cell.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGCode
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".NGCode"));
                    if (TagIndex >= 0)
                        Cell.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                    //NGType
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".NGType"));
                    if (TagIndex >= 0)
                        Cell.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                    string DataType;
                    object Value;
                    foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    {
                        if (pd_item.Value.CommonFlag != "C") //20230301 KJY - 공통 PD가 아닌것만
                        {
                            TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + $".{pd_item.Key}"));
                            if (TagIndex >= 0)
                            {
                                DataType = pd_item.Value.DataType;
                                Value = MappingProcessDataItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);
                                Cell.ProcessData.Add(pd_item.Key, Value);
                                Cell.readDataValue.Add(pd_item.Key, readDataValue[TagIndex]);
                            }
                        }
                    }

                    //20230301 KJY - 공통 PD들 추가한다.
                    for (int j = 0; j < ItemKeyC.Count; j++)
                    {
                        Cell.ProcessData.Add(ItemKeyC[j], ValueC[j]);
                        Cell.readDataValue.Add(ItemKeyC[j], DataValueC[j]);
                    }

                    TrackOutCellInfo._CellList.Add(Cell);
                }


                return TrackOutCellInfo;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }


        /// <summary>
        /// 개별공정 설비에서 필요.. Apro설비에서 필요하면 수정필요한. mapping file의 CommonFlag 관련
        /// </summary>
        /// <param name="CellProcessDataPath"></param>
        /// <returns></returns>
        public _CellProcessData ReadOneProcessDataEQP(string CellProcessDataPath)
        {
            try
            {
                _CellProcessData CellProcessData = new _CellProcessData();
                //sgh 생성
                CellProcessData.ProcessData = new Dictionary<string, object>();
                CellProcessData.readDataValue = new Dictionary<string, DataValue>();

                List<string> pathToReadList = new List<string>();

                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)            

                pathToReadList.Add(CellProcessDataPath + ".CellExist");
                pathToReadList.Add(CellProcessDataPath + ".CellId");
                pathToReadList.Add(CellProcessDataPath + ".LotId");
                pathToReadList.Add(CellProcessDataPath + ".NGCode");
                pathToReadList.Add(CellProcessDataPath + ".NGType");


                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    pathToReadList.Add(CellProcessDataPath + "." + pd_item.Key);
                }
                
                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{CellProcessDataPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                // 이제 CellProcessData 값을 채워야 한다.
                int TagIndex;
                //CellExist
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + ".CellExist"));
                if (TagIndex >= 0)
                    CellProcessData.CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;
                //CellId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + ".CellId"));
                if (TagIndex >= 0)
                    CellProcessData.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //LotId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + ".LotId"));
                if (TagIndex >= 0)
                    CellProcessData.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGCode
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + ".NGCode"));
                if (TagIndex >= 0)
                    CellProcessData.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGType
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + ".NGType"));
                if (TagIndex >= 0)
                    CellProcessData.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                string DataType;
                object Value;
                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellProcessDataPath + $".{pd_item.Key}"));
                    if (TagIndex >= 0)
                    {
                        DataType = pd_item.Value.DataType;
                        Value = MappingProcessDataItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);
                        CellProcessData.ProcessData.Add(pd_item.Key, Value);
                        CellProcessData.readDataValue.Add(pd_item.Key, readDataValue[TagIndex]);
                    }
                }
 
                return CellProcessData;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public _CellProcessData ReadCellManualOutProcessDataEQP(string CellManualOutPath)
        {
            try
            {
                string ProcessDataPath = CellManualOutPath + ".ResultData";
                _CellProcessData CellProcessData = new _CellProcessData();

                List<string> pathToReadList = new List<string>();

                // 여기서 Recipe CSV 파일을 load하자. 
                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms);
                //direction이 EqpToFms면 key 값이 EQP의 Tag Name이 들어간다. (DB에는 Eqp의 TagName 기준으로 저장한다)            

                pathToReadList.Add(CellManualOutPath + ".CellId");
                pathToReadList.Add(CellManualOutPath + ".LotId");
                pathToReadList.Add(CellManualOutPath + ".NGCode");
                pathToReadList.Add(CellManualOutPath + ".NGType");


                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    pathToReadList.Add(ProcessDataPath + "." + pd_item.Key);
                }

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{ProcessDataPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                // 이제 CellProcessData 값을 채워야 한다.
                int TagIndex;

                //CellId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".CellId"));
                if (TagIndex >= 0)
                    CellProcessData.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //LotId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".LotId"));
                if (TagIndex >= 0)
                    CellProcessData.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGCode
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".NGCode"));
                if (TagIndex >= 0)
                    CellProcessData.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGType
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".NGType"));
                if (TagIndex >= 0)
                    CellProcessData.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                string DataType;
                object Value;
                foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                {
                    TagIndex = pathToReadList.FindIndex(x => x.EndsWith(ProcessDataPath + $".{pd_item.Key}"));
                    if (TagIndex >= 0)
                    {
                        DataType = pd_item.Value.DataType;
                        Value = MappingProcessDataItem.SetObjectTypeCast(DataType, readDataValue[TagIndex].Value);
                        CellProcessData.ProcessData.Add(pd_item.Key, Value);
                        CellProcessData.readDataValue.Add(pd_item.Key, readDataValue[TagIndex]);
                    }
                }

                return CellProcessData;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public _CellProcessData ReadCellManualOutProcessDataWithoutPD(string CellManualOutPath)
        {
            try
            {
                string ProcessDataPath = CellManualOutPath + ".ResultData";
                _CellProcessData CellProcessData = new _CellProcessData();

                List<string> pathToReadList = new List<string>();

                pathToReadList.Add(CellManualOutPath + ".CellId");
                pathToReadList.Add(CellManualOutPath + ".LotId");
                pathToReadList.Add(CellManualOutPath + ".NGCode");
                pathToReadList.Add(CellManualOutPath + ".NGType");

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read ProcessData from EQP [{ProcessDataPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (pathToReadList.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                // 이제 CellProcessData 값을 채워야 한다.
                int TagIndex;

                //CellId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".CellId"));
                if (TagIndex >= 0)
                    CellProcessData.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //LotId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".LotId"));
                if (TagIndex >= 0)
                    CellProcessData.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGCode
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".NGCode"));
                if (TagIndex >= 0)
                    CellProcessData.NGCode = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //NGType
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellManualOutPath + ".NGType"));
                if (TagIndex >= 0)
                    CellProcessData.NGType = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                return CellProcessData;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public bool WriteCHGProcessDataFMS(_CHGTrackOutCellInformation trackOutCellInfo, string ProcessDataPath)
        {
            try
            {
                // 중요한건 ProcessData item들은 timestamp 까지 넣어 주어야 한다는 것..
                string targetPath = string.Empty;

                string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
                MappingProcessDataItem mappingPD = new MappingProcessDataItem();
                Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms); //EQP의 TagName이 key

                List<WriteValue> nodesToWrite = new List<WriteValue>();
                WriteValue nodeToWrite;
                for (int i = 0; i < trackOutCellInfo._CellList.Count; i++)
                {
                    string CellPath = ProcessDataPath + string.Format(".Cell{0:D2}", i + 1);
                    //CellExist
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".CellExist", (Boolean)trackOutCellInfo._CellList[i].CellExist);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                    if (trackOutCellInfo._CellList[i].CellExist == false)
                        continue;

                    //LotId
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".LotId", (String)trackOutCellInfo._CellList[i].LotId);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    //CellId
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".CellId", (String)trackOutCellInfo._CellList[i].CellId);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    //NGCode
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".NGCode", (String)trackOutCellInfo._CellList[i].NGCode);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    //NGType
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".NGType", (String)trackOutCellInfo._CellList[i].NGType);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);


                    //20230201 KJY - Level1, Level2에 있는 Cell인지에 따라 각각 StartTemp, EndTemp, AvgTemp를 적어준다.
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".StartTemp_PD", i < 30 ? (float)trackOutCellInfo.StartTemp_Level1 : (float)trackOutCellInfo.StartTemp_Level2);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".EndTemp_PD", i < 30 ? (float)trackOutCellInfo.EndTemp_Level1 : (float)trackOutCellInfo.EndTemp_Level2);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    nodeToWrite = SetWriteValueWithObject(CellPath + ".AvgTemp_PD", i < 30 ? (float)trackOutCellInfo.AvgTemp_Level1 : (float)trackOutCellInfo.AvgTemp_Level2);
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                    //int TagIndex;
                    //foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                    //{
                    //    TagIndex = trackOutCellInfo._CellList[i].ProcessData.First(x => x.Key.EndsWith(pd_item.Key));

                    //}

                    ////PD items
                    //for (int j = 0; j< trackOutCellInfo._CellList[i].ProcessData.Count; j++)
                    //{
                    //    KeyValuePair<string, MappingProcessDataItem> pd_item = processDataDic.First(x => x.Key.EndsWith(trackOutCellInfo._CellList[i].ProcessData[i].key)
                    //    nodeToWrite = SetWriteValueWithDataValue(CellPath + $".{}", trackOutCellInfo._CellList[i].readDataValue[j]);
                    //    nodesToWrite.Add(nodeToWrite);
                    //}

                    foreach (KeyValuePair<string, object> processData in trackOutCellInfo._CellList[i].ProcessData)
                    {
                        if(processData.Key == "StartTemp_PD" || processData.Key == "EndTemp_PD" || processData.Key == "AvgTemp_PD")
                        {
                            continue;
                        }
                        KeyValuePair<string, DataValue> dataValue = trackOutCellInfo._CellList[i].readDataValue.First(x => x.Key == processData.Key);
                        KeyValuePair<string, MappingProcessDataItem> pd_item = processDataDic.First(x => x.Key == processData.Key);
                        if (dataValue.Key == null)
                        {
                            _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                            return false;
                        }
                        if (pd_item.Key == null)
                        {
                            _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                            return false;
                        }
                        nodeToWrite = SetWriteValueWithDataValue(CellPath + $".{pd_item.Value.FmsItem}", dataValue.Value);
                        if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                    }




                }

                if (WriteNodes(nodesToWrite) == false)
                {
                    _LOG_("Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                _LOG_($"[{this.Name}] Complete to write Process Data");

                return true;
            } catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
        }

        public _next_process ReadNextProcess(string NextDestinationPath)
        {
            _next_process NEXT_PROCESS = new _next_process();
            NEXT_PROCESS.NEXT_EQP_ID = NEXT_PROCESS.NEXT_UNIT_ID = String.Empty;

            try
            {
                string strPath = string.Empty;
                List<string> ReadPath = new List<string>();

                ReadPath.Add(NextDestinationPath + ".ProcessId");
                ReadPath.Add(NextDestinationPath + ".EquipmentId");
                ReadPath.Add(NextDestinationPath + ".UnitId");

                // 한번에 읽어옴.             
                List<BrowseNode> browseNodeOut;
                List<ReadValueId> readValueIdOut;
                List<DataValue> readDataValue = ReadNodesByPathList(ReadPath, out browseNodeOut, out readValueIdOut);
                if (readDataValue == null)
                {
                    _LOG_($"Fail to read NextDestination from FMS [{NextDestinationPath}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }
                // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
                // 한개라도 차이가 있으면 문제가 있는 것임.
                if (ReadPath.Count != readDataValue.Count)
                {
                    _LOG_($"Read Data count Error, pathToReadList[{ReadPath.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                    return null;
                }

                for (int i = 0; i < readDataValue.Count; i++)
                {
                    if (browseNodeOut[i].browsePath.EndsWith(NextDestinationPath + ".ProcessId"))
                    {
                        string ProcessId = readDataValue[i].Value != null ? readDataValue[i].Value.ToString() : String.Empty;
                        if(ProcessId.Length<7)
                        {
                            _LOG_($"[{this.Name}] {NextDestinationPath} has no ProcessId", ECSLogger.LOG_LEVEL.ERROR);
                            return null;
                        }
                        NEXT_PROCESS.NEXT_EQP_TYPE = ProcessId.Substring(0, 3);
                        NEXT_PROCESS.NEXT_PROCESS_TYPE = ProcessId.Substring(3, 3);
                        NEXT_PROCESS.NEXT_PROCESS_NO = int.Parse(ProcessId.Substring(6));
                    }
                    else if (browseNodeOut[i].browsePath.EndsWith(NextDestinationPath + ".EquipmentId"))
                        NEXT_PROCESS.NEXT_EQP_ID = readDataValue[i].Value != null ? readDataValue[i].Value.ToString() : String.Empty;
                    else if (browseNodeOut[i].browsePath.EndsWith(NextDestinationPath + ".UnitId"))
                        NEXT_PROCESS.NEXT_UNIT_ID = readDataValue[i].Value != null ? readDataValue[i].Value.ToString() : String.Empty;
                }

                return NEXT_PROCESS;
            }
            catch (Exception ex)
            {
                _LOG_($"[Exception] {ex.Message}", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
        }

        public bool WriteProcessDataFMS(_CellInformation trackOutCellInfo, string ProcessDataPath)
        {
            // 중요한건 ProcessData item들은 timestamp 까지 넣어 주어야 한다는 것..
            string targetPath = string.Empty;

            string MappingFile = $@"Mapping\FMS-{EQPType}_PD.csv";  // 실행파일위치에 Mapping 폴더가 있어야 하고 FMS-{EqpType}_PD.csv 파일이 있어야 한다.
            MappingProcessDataItem mappingPD = new MappingProcessDataItem();
            Dictionary<string, MappingProcessDataItem> processDataDic = mappingPD.LoadPDMappingTable(MappingFile, MappingDirection.EqpToFms); //EQP의 TagName이 key

            List<WriteValue> nodesToWrite = new List<WriteValue>();
            WriteValue nodeToWrite;
            StringBuilder logBuffer = new StringBuilder();

            for (int i = 0; i < trackOutCellInfo._CellList.Count; i++)
            {
                string CellPath = ProcessDataPath + string.Format(".Cell{0:D2}", i + 1);
                //CellExist
                nodeToWrite = SetWriteValueWithObject(CellPath + ".CellExist", (Boolean)trackOutCellInfo._CellList[i].CellExist);
                logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.CellExist : {trackOutCellInfo._CellList[i].CellExist}");

                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                if (trackOutCellInfo._CellList[i].CellExist == false)
                    continue;

                //LotId
                nodeToWrite = SetWriteValueWithObject(CellPath + ".LotId", (String)trackOutCellInfo._CellList[i].LotId);
                logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.LotId : {trackOutCellInfo._CellList[i].LotId}");
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                //CellId
                nodeToWrite = SetWriteValueWithObject(CellPath + ".CellId", (String)trackOutCellInfo._CellList[i].CellId);
                logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.CellId : {trackOutCellInfo._CellList[i].CellId}");
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                //NGCode
                nodeToWrite = SetWriteValueWithObject(CellPath + ".NGCode", (String)trackOutCellInfo._CellList[i].NGCode);
                logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.NGCode : {trackOutCellInfo._CellList[i].NGCode}");
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                //NGType
                nodeToWrite = SetWriteValueWithObject(CellPath + ".NGType", (String)trackOutCellInfo._CellList[i].NGType);
                logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.NGType : {trackOutCellInfo._CellList[i].NGType}");
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                //int TagIndex;
                //foreach (KeyValuePair<string, MappingProcessDataItem> pd_item in processDataDic)
                //{
                //    TagIndex = trackOutCellInfo._CellList[i].ProcessData.First(x => x.Key.EndsWith(pd_item.Key));

                //}

                ////PD items
                //for (int j = 0; j< trackOutCellInfo._CellList[i].ProcessData.Count; j++)
                //{
                //    KeyValuePair<string, MappingProcessDataItem> pd_item = processDataDic.First(x => x.Key.EndsWith(trackOutCellInfo._CellList[i].ProcessData[i].key)
                //    nodeToWrite = SetWriteValueWithDataValue(CellPath + $".{}", trackOutCellInfo._CellList[i].readDataValue[j]);
                //    nodesToWrite.Add(nodeToWrite);
                //}

                foreach (KeyValuePair<string, object> processData in trackOutCellInfo._CellList[i].ProcessData)
                {
                    //KeyValuePair<string, DataValue> dataValue = trackOutCellInfo._CellList[i].readDataValue.First(x => x.Key == processData.Key);
                    //KeyValuePair<string, MappingProcessDataItem> pd_item = processDataDic.First(x => x.Key == processData.Key);
                    //if (dataValue.Key == null)
                    //{
                    //    _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                    //    return false;
                    //}
                    //if (pd_item.Key == null)
                    //{
                    //    _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                    //    return false;
                    //}
                    //nodeToWrite = SetWriteValueWithDataValue(CellPath + $".{pd_item.Value.FmsItem}", dataValue.Value);
                    //if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);


                    DataValue dataValue1 = trackOutCellInfo._CellList[i].readDataValue[$"{processData.Key}"];
                    MappingProcessDataItem pd_item1 = processDataDic[$"{processData.Key}"];

                    if (dataValue1.Value == null)
                    {
                        _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                        return false;
                    }
                    if (pd_item1.FmsItem == null)
                    {
                        _LOG_($"Fail to find DataValue from readDataValue : [{processData.Key}]", ECSLogger.LOG_LEVEL.ERROR);
                        return false;
                    }
                    nodeToWrite = SetWriteValueWithDataValue(CellPath + $".{pd_item1.FmsItem}", dataValue1);
                    logBuffer.AppendLine($"Prepare WriteItem : {CellPath}.{pd_item1.FmsItem} : {dataValue1}");
                    if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                }
            }

            _LOG_($"{logBuffer}");      // 20230330 msh : log추가

            if (WriteNodes(nodesToWrite) == false)
            {
                _LOG_("Fail to write Process Data", ECSLogger.LOG_LEVEL.ERROR);
                return false;
            }
            _LOG_($"[{this.Name}] Complete to write Process Data");

            return true;
        }
        /// <summary>
        /// CellInfoPath : ~.Cell 까지
        /// </summary>
        /// <param name="CellInfoPath"></param>
        /// <param name="CellCountPath"></param>
        /// <returns></returns>
        public _CellBasicInformation ReadBasicCellInformation(string CellInfoPath, string CellCountPath)
        {
            _CellBasicInformation CellInformation = new _CellBasicInformation();
            CellInformation._CellList = new List<_CellBasicData>();

            string CellPath = string.Empty;
            string targetPath = string.Empty;
            List<string> pathToReadList = new List<string>();

            //CellCount 먼저
            pathToReadList.Add(CellCountPath);

            for (int i=0; i<30; i++)
            {
                CellPath = CellInfoPath + $"._{i}";
                targetPath = CellPath + ".CellExist";
                pathToReadList.Add(targetPath);
                targetPath = CellPath + ".CellId";
                pathToReadList.Add(targetPath);
                targetPath = CellPath + ".LotId";
                pathToReadList.Add(targetPath);
            }

            // 한번에 읽어옴.             
            List<BrowseNode> browseNodeOut;
            List<ReadValueId> readValueIdOut;
            List<DataValue> readDataValue = ReadNodesByPathList(pathToReadList, out browseNodeOut, out readValueIdOut);
            if (readDataValue == null)
            {
                _LOG_($"Fail to read Cell Information from EQP [{CellInfoPath}]", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }
            // pathToReadList, browseNodeOut, readValueIdOut, readDataValue의 count는 반드시 모두 동일해야 한다.
            // 한개라도 차이가 있으면 문제가 있는 것임.
            if (pathToReadList.Count != readDataValue.Count)
            {
                _LOG_($"Read Data count Error, pathToReadList[{pathToReadList.Count}], browseNodeOut[{browseNodeOut.Count}], readValueIdOut[{readValueIdOut.Count}], readDataValue[{readDataValue.Count}]", ECSLogger.LOG_LEVEL.ERROR);
                return null;
            }

            int TagIndex;
            TagIndex = pathToReadList.FindIndex(x => x.EndsWith("CellCount"));
            if (TagIndex >= 0)
                CellInformation.CellCount = readDataValue[TagIndex].Value != null ? (UInt16)readDataValue[TagIndex].Value : (UInt16)0;

            for (int i = 0; i < 30; i++)
            {
                _CellBasicData Cell = new _CellBasicData();

                CellPath = CellInfoPath + $"._{i}";
                //CellExist
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellExist"));
                if (TagIndex >= 0)
                    Cell.CellExist = readDataValue[TagIndex].Value != null ? (Boolean)readDataValue[TagIndex].Value : (Boolean)false;

                if (Cell.CellExist == false)
                {
                    CellInformation._CellList.Add(Cell); // CellExist가 false 일때도 add 한다.
                    continue;
                }
                //CellId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".CellId"));
                if (TagIndex >= 0)
                    Cell.CellId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;
                //LotId
                TagIndex = pathToReadList.FindIndex(x => x.EndsWith(CellPath + ".LotId"));
                if (TagIndex >= 0)
                    Cell.LotId = readDataValue[TagIndex].Value != null ? (String)readDataValue[TagIndex].Value : String.Empty;

                CellInformation._CellList.Add(Cell);
            }

            return CellInformation;
        }

        public _CellBasicInformation ReadEQPTrackInOutCellBasicInfomation(string TrackInOutCellPath, bool isPackingPlaceLocation = false)
        {
            try
            {
                _CellBasicInformation CellBasicInformation = new _CellBasicInformation();
                string strPath = string.Empty;

                List<string> ReadPath = new List<string>();
                strPath = TrackInOutCellPath + ".CellCount";
                ReadPath.Add(strPath);

                int basicCellCount = 30;
                if (isPackingPlaceLocation) basicCellCount = 150;

                for (int i = 0; i < basicCellCount; i++)
                {
                    strPath = TrackInOutCellPath + $".Cell._{i}.CellExist";
                    ReadPath.Add(strPath);
                    strPath = TrackInOutCellPath + $".Cell._{i}.CellId";
                    ReadPath.Add(strPath);
                    strPath = TrackInOutCellPath + $".Cell._{i}.LotId";
                    ReadPath.Add(strPath);
                }

                List<BrowseNode> browseNodeList;
                List<ReadValueId> readValueIdList;
                List<DataValue> nodesReadValue = this.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

                if (nodesReadValue == null || nodesReadValue.Count < 1)
                    return null;

                CellBasicInformation.InitList(basicCellCount);

                for (int i = 0; i < nodesReadValue.Count; i++)
                {
                    strPath = TrackInOutCellPath + ".CellCount";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        CellBasicInformation.CellCount = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                    else
                    {
                        for (int j = 0; j < basicCellCount; j++)
                        {
                            strPath = TrackInOutCellPath + $".Cell._{j}.CellExist";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellBasicInformation._CellList[j].CellExist = (Boolean)nodesReadValue[i].Value;
                            strPath = TrackInOutCellPath + $".Cell._{j}.CellId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellBasicInformation._CellList[j].CellId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            strPath = TrackInOutCellPath + $".Cell._{j}.LotId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellBasicInformation._CellList[j].LotId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                        }
                    }
                }

                _LOG_($"Success to read All Cell Baisic Information from [{TrackInOutCellPath}]");

                return CellBasicInformation;
            }

            catch (Exception ex)
            {
                _LOG_($"[{this.Name}:Exception] {ex.Message}");
                return null;
            }
        }

        public _SorterCellInformation ReadEQPTrackInOutCellGradeInfomation(string TrackInOutCellPath, bool IncludeNGCodeFlag=true)
        {
            try
            {
                _SorterCellInformation CellGradeInformation = new _SorterCellInformation();
                string strPath = string.Empty;

                List<string> ReadPath = new List<string>();
                strPath = TrackInOutCellPath + ".CellCount";
                ReadPath.Add(strPath);

                for (int i = 0; i < 30; i++)
                {
                    strPath = TrackInOutCellPath + $".Cell._{i}.CellExist";
                    ReadPath.Add(strPath);
                    strPath = TrackInOutCellPath + $".Cell._{i}.CellId";
                    ReadPath.Add(strPath);
                    strPath = TrackInOutCellPath + $".Cell._{i}.LotId";
                    ReadPath.Add(strPath);
                    //strPath = TrackInOutCellPath + $".Cell._{i}.CellGrade";
                    //ReadPath.Add(strPath);
                    if (IncludeNGCodeFlag)
                    {
                        strPath = TrackInOutCellPath + $".Cell._{i}.NGCode";
                        ReadPath.Add(strPath);
                        strPath = TrackInOutCellPath + $".Cell._{i}.NGType";
                        ReadPath.Add(strPath);
                        strPath = TrackInOutCellPath + $".Cell._{i}.DefectType";
                        ReadPath.Add(strPath);
                    }
                }

                List<BrowseNode> browseNodeList;
                List<ReadValueId> readValueIdList;
                List<DataValue> nodesReadValue = this.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

                if (nodesReadValue == null || nodesReadValue.Count < 1)
                    return null;

                CellGradeInformation.InitList();

                for (int i = 0; i < nodesReadValue.Count; i++)
                {
                    strPath = TrackInOutCellPath + ".CellCount";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        CellGradeInformation.CellCount = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                    else
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            strPath = TrackInOutCellPath + $".Cell._{j}.CellExist";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellGradeInformation._CellList[j].CellExist = (Boolean)nodesReadValue[i].Value;
                            strPath = TrackInOutCellPath + $".Cell._{j}.CellId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellGradeInformation._CellList[j].CellId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            strPath = TrackInOutCellPath + $".Cell._{j}.LotId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                CellGradeInformation._CellList[j].LotId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            //strPath = TrackInOutCellPath + $".Cell._{j}.CellGrade";
                            //if (browseNodeList[i].browsePath.EndsWith(strPath))
                            //    CellGradeInformation._CellList[j].CellGrade = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            if (IncludeNGCodeFlag)
                            {
                                strPath = TrackInOutCellPath + $".Cell._{j}.NGCode";
                                if (browseNodeList[i].browsePath.EndsWith(strPath))
                                    CellGradeInformation._CellList[j].NGCode = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                                strPath = TrackInOutCellPath + $".Cell._{j}.NGType";
                                if (browseNodeList[i].browsePath.EndsWith(strPath))
                                    CellGradeInformation._CellList[j].NGType = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                                strPath = TrackInOutCellPath + $".Cell._{j}.DefectType";                         
                                if (browseNodeList[i].browsePath.EndsWith(strPath))
                                    CellGradeInformation._CellList[j].DefectType = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            }
                        }
                    }
                }

                _LOG_($"Success to read All Cell Grade Information from [{TrackInOutCellPath}]");

                return CellGradeInformation;
            }

            catch (Exception ex)
            {
                _LOG_($"[{this.Name}:Exception] {ex.Message}");
                return null;
            }
        }

        public _SorterTrayCellInformation ReadSorterTrayCellInformation(bool isPickLocation)
        {
            try
            {
                _SorterTrayCellInformation SorterTrayCellInformation = new _SorterTrayCellInformation();
                string TrackPath = string.Empty;
                string strPath = string.Empty;

                List<string> ReadPath = new List<string>();

                if (isPickLocation)
                    TrackPath = "PickLocation";
                else
                    TrackPath = "PlaceLocation";

                // Tray Information
                strPath = TrackPath + ".TrayInformation.TrayExist";
                ReadPath.Add(strPath);
                strPath = TrackPath + ".TrayInformation.TrayId";
                ReadPath.Add(strPath);
                strPath = TrackPath + ".TrayInformation.TrayType";
                ReadPath.Add(strPath);
                if(isPickLocation)
                {
                    strPath = TrackPath + ".TrayInformation.ProcessType";
                    ReadPath.Add(strPath);
                } else
                {
                    strPath = TrackPath + ".TrayInformation.TrayGrade";
                    ReadPath.Add(strPath);
                    strPath = TrackPath + ".TrayInformation.DefectType";
                    ReadPath.Add(strPath);
                }

                //20230404 sgh .빠짐
                //strPath = TrackPath + "CellInformation.CellCount";
                strPath = TrackPath + ".CellInformation.CellCount";
                ReadPath.Add(strPath);

                for (int i = 0; i < 30; i++)
                {
                    strPath = TrackPath + $".CellInformation.Cell._{i}.CellExist";
                    ReadPath.Add(strPath);
                    strPath = TrackPath + $".CellInformation.Cell._{i}.CellId";
                    ReadPath.Add(strPath);
                    strPath = TrackPath + $".CellInformation.Cell._{i}.LotId";
                    ReadPath.Add(strPath);
                    //strPath = TrackPath + $".CellInformation.Cell._{i}.CellGrade";
                    //ReadPath.Add(strPath);

                    strPath = TrackPath + $".CellInformation.Cell._{i}.NGCode";
                    ReadPath.Add(strPath);
                    strPath = TrackPath + $".CellInformation.Cell._{i}.NGType";
                    ReadPath.Add(strPath);

                    strPath = TrackPath + $".CellInformation.Cell._{i}.DefectType";
                    ReadPath.Add(strPath);


                }

                List<BrowseNode> browseNodeList;
                List<ReadValueId> readValueIdList;
                List<DataValue> nodesReadValue = this.ReadNodesByPathList(ReadPath, out browseNodeList, out readValueIdList);

                if (nodesReadValue == null || nodesReadValue.Count < 1)
                    return null;

                SorterTrayCellInformation.InitList();

                for (int i = 0; i < nodesReadValue.Count; i++)
                {

                    //Tray정보부터
                    strPath = TrackPath + ".TrayInformation.TrayExist";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        SorterTrayCellInformation.TrayExist = nodesReadValue[i].Value != null ? (Boolean)nodesReadValue[i].Value : (Boolean)false;

                    if(SorterTrayCellInformation.TrayExist ==false)
                    {
                        // TrayExist가 false면.. 그냥 return.
                        return SorterTrayCellInformation;
                    }

                    strPath = TrackPath + ".TrayInformation.TrayId";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        SorterTrayCellInformation.TrayId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                    strPath = TrackPath + ".TrayInformation.TrayType";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        SorterTrayCellInformation.TrayType = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                    if (isPickLocation)
                    {
                        strPath = TrackPath + ".TrayInformation.ProcessType";
                        if (browseNodeList[i].browsePath.EndsWith(strPath))
                            SorterTrayCellInformation.ProcessType = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                    }
                    else
                    {
                        strPath = TrackPath + ".TrayInformation.TrayGrade";
                        if (browseNodeList[i].browsePath.EndsWith(strPath))
                            SorterTrayCellInformation.TrayGrade = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;


                        strPath = TrackPath + ".TrayInformation.DefectType";
                        if (browseNodeList[i].browsePath.EndsWith(strPath))
                        {
                            SorterTrayCellInformation.DefectType = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;

                            //20230403 sgh TrayInformation.DefectType일때만 isReworkTray에 값을 할당 할 수 있도록 변경
                            if (SorterTrayCellInformation.DefectType.Length > 0)
                                SorterTrayCellInformation.isReworkTray = true;
                            else
                                SorterTrayCellInformation.isReworkTray = false;
                        }
                    }

                    strPath = TrackPath + ".CellInformation.CellCount";
                    if (browseNodeList[i].browsePath.EndsWith(strPath))
                        SorterTrayCellInformation.CellCount = nodesReadValue[i].Value != null ? (UInt16)nodesReadValue[i].Value : (UInt16)0;
                    else
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            strPath = TrackPath + $".CellInformation.Cell._{j}.CellExist";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].CellExist = (Boolean)nodesReadValue[i].Value;
                            strPath = TrackPath + $".CellInformation.Cell._{j}.CellId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].CellId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            strPath = TrackPath + $".CellInformation.Cell._{j}.LotId";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].LotId = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            strPath = TrackPath + $".CellInformation.Cell._{j}.CellGrade";
                            //if (browseNodeList[i].browsePath.EndsWith(strPath))
                            //    SorterTrayCellInformation._CellList[j].CellGrade = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;

                            strPath = TrackPath + $".CellInformation.Cell._{j}.NGCode";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].NGCode = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;
                            strPath = TrackPath + $".CellInformation.Cell._{j}.NGType";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].NGType = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;

                            strPath = TrackPath + $".CellInformation.Cell._{j}.DefectType";
                            if (browseNodeList[i].browsePath.EndsWith(strPath))
                                SorterTrayCellInformation._CellList[j].DefectType = nodesReadValue[i].Value != null ? nodesReadValue[i].Value.ToString() : String.Empty;

                        }
                    }
                }

                _LOG_($"Success to read All Tray/Cell Grade Information from [{TrackPath}]");

                return SorterTrayCellInformation;
            }

            catch (Exception ex)
            {
                _LOG_($"[{this.Name}:Exception] {ex.Message}");
                return null;
            }
        }

        public bool WriteFMSTrouble(string FMSTroublePath, bool Status, Global.FMSTrouble ErrorNo)
        {
            try
            {
                List<WriteValue> nodesToWrite = new List<WriteValue>();
                WriteValue nodeToWrite;

                nodeToWrite = SetWriteValueWithObject(FMSTroublePath + ".Status", (Boolean)Status);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(FMSTroublePath + ".ErrorNo", (UInt16)ErrorNo);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);


                if (WriteNodes(nodesToWrite) == false)
                {
                    _LOG_($"Fail to write FMS Trouble Data : {FMSTroublePath}, Status:{Status}, ErrorNo:{ErrorNo}", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                _LOG_($"[{this.Name}] Complete to write FMS Trouble Data : {FMSTroublePath}, Status:{Status}, ErrorNo:{ErrorNo}");

                return true;
            }
            catch (Exception ex)
            {
                _LOG_($"[{this.Name}:Exception] {ex.Message}");
                return false;
            }

        }

        public bool WriteNGSTrayRequest(string TrayRequestPath, string Grade, string DefectType, UInt16 TrayType, string ProductModel, Boolean TrayLoadRequest, Boolean ReservedFlag, string ReservedTrayId)
        {
            try
            {
                List<WriteValue> nodesToWrite = new List<WriteValue>();
                WriteValue nodeToWrite;

                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".Grade", (String)Grade);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".DefectType", (String)DefectType);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".TrayType", (UInt16)TrayType);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".ProductModel", (String)ProductModel);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".ReservedFlag", (Boolean)ReservedFlag);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".ReservedTrayId", (String)ReservedTrayId);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);
                nodeToWrite = SetWriteValueWithObject(TrayRequestPath + ".TrayLoadRequest", (Boolean)TrayLoadRequest);
                if (nodeToWrite != null) nodesToWrite.Add(nodeToWrite);

                if (WriteNodes(nodesToWrite) == false)
                {
                    _LOG_($"Fail to write NGS TrayRequest : {TrayRequestPath}, Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ProductModel:{ProductModel}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}, TrayLoadRequest:{TrayLoadRequest}", ECSLogger.LOG_LEVEL.ERROR);
                    return false;
                }
                _LOG_($"[{this.Name}] Complete to write NGS TrayRequest : {TrayRequestPath}, Grade:{Grade}, DefectType:{DefectType}, TrayType:{TrayType}, ProductModel:{ProductModel}, ReservedFlag:{ReservedFlag}, ReservedTrayId:{ReservedTrayId}, TrayLoadRequest:{TrayLoadRequest}");

                return true;

            }
            catch (Exception ex)
            {
                _LOG_($"[{this.Name}:Exception] {ex.Message}");
                return false;
            }

        }


    }
}
