namespace EQP_INTF.LeakCheck
{
    partial class LeakCheck
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.RestApiServer = new RestAPIServer.RestApiServer();
            this.FMSClient = new OPCUAClient.OPCUAClient();
            this.EQPClient = new OPCUAClient.OPCUAClient();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Command_TextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.CommandResponse_Radio = new CommonCtrls.GroupRadio();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FMS_ErrorNo_TextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.FMS_Status_Radio = new CommonCtrls.GroupRadio();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Common_Alive_Radio = new CommonCtrls.GroupRadio();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.EQP_ErrorLevel_Radio = new CommonCtrls.GroupRadio();
            this.Power_Radio = new CommonCtrls.GroupRadio();
            this.Status_TextBox = new System.Windows.Forms.TextBox();
            this.EQP_ErrorNo_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.L1_RecipeId_TextBox = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.L1_ProcessEndResponse_Radio = new CommonCtrls.GroupRadio();
            this.L1_ProcessStartResponse_Radio = new CommonCtrls.GroupRadio();
            this.L1_RequestRecipeResponse_Radio = new CommonCtrls.GroupRadio();
            this.label21 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.L1_ProcessEnd_Radio = new CommonCtrls.GroupRadio();
            this.label26 = new System.Windows.Forms.Label();
            this.L1_ProcessStart_Radio = new CommonCtrls.GroupRadio();
            this.label22 = new System.Windows.Forms.Label();
            this.L1_RequestRecipe_Radio = new CommonCtrls.GroupRadio();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.L1_TrayLoadResponse_Radio = new CommonCtrls.GroupRadio();
            this.label32 = new System.Windows.Forms.Label();
            this.L1_TrayLoad_Radio = new CommonCtrls.GroupRadio();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.L1_ProcessId_TextBox = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.L1_LotId_TextBox = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.L1_RouteId_TextBox = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.L1_Model_TextBox = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.L1_TrayExist_Radio = new CommonCtrls.GroupRadio();
            this.label34 = new System.Windows.Forms.Label();
            this.L1_TrayId_TextBox = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.L1_TrackIn_CellCount = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.L1_CellLoadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.L1_CellLoadComplete_Radio = new CommonCtrls.GroupRadio();
            this.L1_CellTrackIn_CellId_TextBox = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.L1_CellTrackIn_CellNo_TextBox = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.L1_CellUnloadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.L1_CellUnloadComplete_Radio = new CommonCtrls.GroupRadio();
            this.L1_CellTrackOut_CellId_TextBox = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.L1_CellTrackOut_CellNo_TextBox = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.L2_ProcessStart_Radio = new CommonCtrls.GroupRadio();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.L2_ProcessStartResponse_Radio = new CommonCtrls.GroupRadio();
            this.label25 = new System.Windows.Forms.Label();
            this.L2_ProcessEnd_Radio = new CommonCtrls.GroupRadio();
            this.label27 = new System.Windows.Forms.Label();
            this.L2_ProcessEndResponse_Radio = new CommonCtrls.GroupRadio();
            this.label33 = new System.Windows.Forms.Label();
            this.L2_RequestRecipe_Radio = new CommonCtrls.GroupRadio();
            this.label36 = new System.Windows.Forms.Label();
            this.L2_RequestRecipeResponse_Radio = new CommonCtrls.GroupRadio();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.L2_CellUnloadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.L2_CellUnloadComplete_Radio = new CommonCtrls.GroupRadio();
            this.L2_CellTrackOut_CellId_TextBox = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.L2_CellTrackOut_CellNo_TextBox = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.L2_CellLoadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.L2_CellLoadComplete_Radio = new CommonCtrls.GroupRadio();
            this.L2_CellTrackIn_CellId_TextBox = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.L2_CellTrackIn_CellNo_TextBox = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.L1_TrackOut_CellCount = new System.Windows.Forms.TextBox();
            this.L1_TrackInCellList_dataGridView = new System.Windows.Forms.DataGridView();
            this.label67 = new System.Windows.Forms.Label();
            this.L1_TrackOutCellList_dataGridView = new System.Windows.Forms.DataGridView();
            this.label68 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.L2_ProcessId_TextBox = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.L2_LotId_TextBox = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.L2_RouteId_TextBox = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.L2_Model_TextBox = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.L2_TrayExist_Radio = new CommonCtrls.GroupRadio();
            this.label63 = new System.Windows.Forms.Label();
            this.L2_TrayId_TextBox = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.L2_RecipeId_TextBox = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.L2_TrayLoad_Radio = new CommonCtrls.GroupRadio();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.L2_TrayLoadResponse_Radio = new CommonCtrls.GroupRadio();
            this.L2_TrackOutCellList_dataGridView = new System.Windows.Forms.DataGridView();
            this.label69 = new System.Windows.Forms.Label();
            this.L2_TrackInCellList_dataGridView = new System.Windows.Forms.DataGridView();
            this.label70 = new System.Windows.Forms.Label();
            this.L2_TrackOut_CellCount = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.L2_TrackIn_CellCount = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.Mode_Radio = new CommonCtrls.GroupRadio();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.L1_TrackInCellList_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.L1_TrackOutCellList_dataGridView)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.L2_TrackOutCellList_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.L2_TrackInCellList_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RestApiServer
            // 
            this.RestApiServer.Location = new System.Drawing.Point(768, 20);
            this.RestApiServer.Name = "RestApiServer";
            this.RestApiServer.Port = 0;
            this.RestApiServer.Size = new System.Drawing.Size(309, 112);
            this.RestApiServer.TabIndex = 27;
            // 
            // FMSClient
            // 
            this.FMSClient.Endpoint = "opc.tcp://localhost:48040";
            this.FMSClient.EQPID = null;
            this.FMSClient.EQPType = null;
            this.FMSClient.GroupName = "FMS OPCUA Server";
            this.FMSClient.ID = "fms";
            this.FMSClient.Location = new System.Drawing.Point(2, 3);
            this.FMSClient.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.FMSClient.MesAliveNodeId = null;
            this.FMSClient.Name = "FMSClient";
            this.FMSClient.Password = "fms@!";
            this.FMSClient.Size = new System.Drawing.Size(761, 62);
            this.FMSClient.StartingNodeId = "ns=2;i=22879";
            this.FMSClient.TabIndex = 26;
            this.FMSClient.UNITID = null;
            // 
            // EQPClient
            // 
            this.EQPClient.Endpoint = "opc.tcp://localhost:48001";
            this.EQPClient.EQPID = null;
            this.EQPClient.EQPType = null;
            this.EQPClient.GroupName = "EQP OPCUA Server";
            this.EQPClient.ID = null;
            this.EQPClient.Location = new System.Drawing.Point(2, 71);
            this.EQPClient.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EQPClient.MesAliveNodeId = null;
            this.EQPClient.Name = "EQPClient";
            this.EQPClient.Password = null;
            this.EQPClient.Size = new System.Drawing.Size(761, 61);
            this.EQPClient.StartingNodeId = null;
            this.EQPClient.TabIndex = 25;
            this.EQPClient.UNITID = null;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(536, 135);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(5, 800);
            this.label12.TabIndex = 50;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Command_TextBox);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.CommandResponse_Radio);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(10, 533);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(520, 81);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EqupimentControl";
            // 
            // Command_TextBox
            // 
            this.Command_TextBox.Location = new System.Drawing.Point(171, 49);
            this.Command_TextBox.Name = "Command_TextBox";
            this.Command_TextBox.ReadOnly = true;
            this.Command_TextBox.Size = new System.Drawing.Size(77, 22);
            this.Command_TextBox.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(9, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(157, 22);
            this.label16.TabIndex = 0;
            this.label16.Text = "CommandResponse";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Orchid;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(9, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(158, 22);
            this.label17.TabIndex = 0;
            this.label17.Text = "Command";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CommandResponse_Radio
            // 
            this.CommandResponse_Radio.BackColor = System.Drawing.Color.White;
            this.CommandResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.CommandResponse_Radio.ErrorOptionIndex = 98;
            this.CommandResponse_Radio.IndexChecked = 0;
            this.CommandResponse_Radio.Location = new System.Drawing.Point(169, 20);
            this.CommandResponse_Radio.MarginLeft = 16;
            this.CommandResponse_Radio.Name = "CommandResponse_Radio";
            this.CommandResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.CommandResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.CommandResponse_Radio.NormalOptionIndex = 99;
            this.CommandResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.CommandResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.CommandResponse_Radio.TabIndex = 6;
            this.CommandResponse_Radio.Text = "groupRadio1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FMS_ErrorNo_TextBox);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.FMS_Status_Radio);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(10, 451);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 76);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FmsStatus";
            // 
            // FMS_ErrorNo_TextBox
            // 
            this.FMS_ErrorNo_TextBox.Location = new System.Drawing.Point(166, 43);
            this.FMS_ErrorNo_TextBox.Name = "FMS_ErrorNo_TextBox";
            this.FMS_ErrorNo_TextBox.ReadOnly = true;
            this.FMS_ErrorNo_TextBox.Size = new System.Drawing.Size(77, 22);
            this.FMS_ErrorNo_TextBox.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.SkyBlue;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(6, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 45);
            this.label13.TabIndex = 2;
            this.label13.Text = "Trouble";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.SkyBlue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(86, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 22);
            this.label14.TabIndex = 1;
            this.label14.Text = "ErrorNo";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FMS_Status_Radio
            // 
            this.FMS_Status_Radio.BackColor = System.Drawing.Color.White;
            this.FMS_Status_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.FMS_Status_Radio.ErrorOptionIndex = 98;
            this.FMS_Status_Radio.IndexChecked = 0;
            this.FMS_Status_Radio.Location = new System.Drawing.Point(166, 18);
            this.FMS_Status_Radio.MarginLeft = 16;
            this.FMS_Status_Radio.Name = "FMS_Status_Radio";
            this.FMS_Status_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.FMS_Status_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.FMS_Status_Radio.NormalOptionIndex = 99;
            this.FMS_Status_Radio.Size = new System.Drawing.Size(226, 20);
            this.FMS_Status_Radio.StringOptions = new string[] {
        "Normal",
        "Trouble"};
            this.FMS_Status_Radio.TabIndex = 6;
            this.FMS_Status_Radio.Text = "groupRadio1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.SkyBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(86, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 22);
            this.label15.TabIndex = 1;
            this.label15.Text = "Status";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Common_Alive_Radio);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(10, 216);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(520, 68);
            this.groupBox5.TabIndex = 47;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Common";
            // 
            // Common_Alive_Radio
            // 
            this.Common_Alive_Radio.BackColor = System.Drawing.Color.White;
            this.Common_Alive_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.Common_Alive_Radio.ErrorOptionIndex = 98;
            this.Common_Alive_Radio.IndexChecked = 0;
            this.Common_Alive_Radio.Location = new System.Drawing.Point(172, 23);
            this.Common_Alive_Radio.MarginLeft = 16;
            this.Common_Alive_Radio.Name = "Common_Alive_Radio";
            this.Common_Alive_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.Common_Alive_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.Common_Alive_Radio.NormalOptionIndex = 99;
            this.Common_Alive_Radio.Size = new System.Drawing.Size(225, 20);
            this.Common_Alive_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.Common_Alive_Radio.TabIndex = 6;
            this.Common_Alive_Radio.Text = "groupRadio1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.YellowGreen;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alive";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Mode_Radio);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.EQP_ErrorLevel_Radio);
            this.groupBox1.Controls.Add(this.Power_Radio);
            this.groupBox1.Controls.Add(this.Status_TextBox);
            this.groupBox1.Controls.Add(this.EQP_ErrorNo_TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 290);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 155);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EquipmentStatus";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(352, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 107);
            this.label9.TabIndex = 7;
            this.label9.Text = "[Status Information]\r\n1 : Idle \r\n2 : Running\r\n4 : Machine Trouble\r\n8 : Pause \r\n64" +
    " : Fire (Temp only)\r\n128 : Fire (smoke + Temp)";
            // 
            // EQP_ErrorLevel_Radio
            // 
            this.EQP_ErrorLevel_Radio.BackColor = System.Drawing.Color.White;
            this.EQP_ErrorLevel_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.EQP_ErrorLevel_Radio.ErrorOptionIndex = 98;
            this.EQP_ErrorLevel_Radio.IndexChecked = 0;
            this.EQP_ErrorLevel_Radio.Location = new System.Drawing.Point(170, 124);
            this.EQP_ErrorLevel_Radio.MarginLeft = 16;
            this.EQP_ErrorLevel_Radio.Name = "EQP_ErrorLevel_Radio";
            this.EQP_ErrorLevel_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.EQP_ErrorLevel_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.EQP_ErrorLevel_Radio.NormalOptionIndex = 99;
            this.EQP_ErrorLevel_Radio.Size = new System.Drawing.Size(285, 20);
            this.EQP_ErrorLevel_Radio.StringOptions = new string[] {
        "Warning",
        "Non-Critical",
        "Critical"};
            this.EQP_ErrorLevel_Radio.TabIndex = 6;
            this.EQP_ErrorLevel_Radio.Text = "groupRadio1";
            // 
            // Power_Radio
            // 
            this.Power_Radio.BackColor = System.Drawing.Color.White;
            this.Power_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.Power_Radio.ErrorOptionIndex = 98;
            this.Power_Radio.IndexChecked = 0;
            this.Power_Radio.Location = new System.Drawing.Point(89, 21);
            this.Power_Radio.MarginLeft = 16;
            this.Power_Radio.Name = "Power_Radio";
            this.Power_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.Power_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.Power_Radio.NormalOptionIndex = 99;
            this.Power_Radio.Size = new System.Drawing.Size(225, 20);
            this.Power_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.Power_Radio.TabIndex = 6;
            this.Power_Radio.Text = "groupRadio5";
            // 
            // Status_TextBox
            // 
            this.Status_TextBox.Location = new System.Drawing.Point(90, 72);
            this.Status_TextBox.Name = "Status_TextBox";
            this.Status_TextBox.ReadOnly = true;
            this.Status_TextBox.Size = new System.Drawing.Size(77, 22);
            this.Status_TextBox.TabIndex = 5;
            // 
            // EQP_ErrorNo_TextBox
            // 
            this.EQP_ErrorNo_TextBox.Location = new System.Drawing.Point(170, 98);
            this.EQP_ErrorNo_TextBox.Name = "EQP_ErrorNo_TextBox";
            this.EQP_ErrorNo_TextBox.ReadOnly = true;
            this.EQP_ErrorNo_TextBox.Size = new System.Drawing.Size(77, 22);
            this.EQP_ErrorNo_TextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Yellow;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(10, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 45);
            this.label4.TabIndex = 2;
            this.label4.Text = "Trouble";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Yellow;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(90, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "ErrorLevel";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(90, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 22);
            this.label5.TabIndex = 1;
            this.label5.Text = "ErrorNo";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Status";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Power";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Yellow;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(10, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Mode";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.Yellow;
            this.label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label46.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(100, 161);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(109, 22);
            this.label46.TabIndex = 41;
            this.label46.Text = "* EQP Write";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.YellowGreen;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(328, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 22);
            this.label8.TabIndex = 42;
            this.label8.Text = "* FMS/EQP Write";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.SkyBlue;
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label45.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(215, 161);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(109, 22);
            this.label45.TabIndex = 43;
            this.label45.Text = "* FMS Write";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label47.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(215, 191);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(109, 22);
            this.label47.TabIndex = 44;
            this.label47.Text = "* Response";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.Orchid;
            this.label48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label48.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(100, 191);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(109, 22);
            this.label48.TabIndex = 45;
            this.label48.Text = "* Request";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(547, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1216, 17);
            this.label11.TabIndex = 39;
            this.label11.Text = "Location 1";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.YellowGreen;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(13, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(520, 17);
            this.label10.TabIndex = 40;
            this.label10.Text = "Common";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Black;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(547, 542);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1220, 17);
            this.label18.TabIndex = 39;
            this.label18.Text = "Location 2";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.SkyBlue;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(956, 153);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(395, 17);
            this.label19.TabIndex = 51;
            this.label19.Text = "FMS";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Yellow;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(547, 153);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(395, 17);
            this.label20.TabIndex = 52;
            this.label20.Text = "EQP";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_RecipeId_TextBox
            // 
            this.L1_RecipeId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_RecipeId_TextBox.Location = new System.Drawing.Point(86, 69);
            this.L1_RecipeId_TextBox.Name = "L1_RecipeId_TextBox";
            this.L1_RecipeId_TextBox.ReadOnly = true;
            this.L1_RecipeId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_RecipeId_TextBox.TabIndex = 22;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.SkyBlue;
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label38.Location = new System.Drawing.Point(6, 69);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 22);
            this.label38.TabIndex = 21;
            this.label38.Text = "RecipeId";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_ProcessEndResponse_Radio
            // 
            this.L1_ProcessEndResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_ProcessEndResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_ProcessEndResponse_Radio.ErrorOptionIndex = 98;
            this.L1_ProcessEndResponse_Radio.IndexChecked = 0;
            this.L1_ProcessEndResponse_Radio.Location = new System.Drawing.Point(591, 79);
            this.L1_ProcessEndResponse_Radio.MarginLeft = 16;
            this.L1_ProcessEndResponse_Radio.Name = "L1_ProcessEndResponse_Radio";
            this.L1_ProcessEndResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_ProcessEndResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_ProcessEndResponse_Radio.NormalOptionIndex = 99;
            this.L1_ProcessEndResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_ProcessEndResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_ProcessEndResponse_Radio.TabIndex = 12;
            this.L1_ProcessEndResponse_Radio.Text = "groupRadio1";
            // 
            // L1_ProcessStartResponse_Radio
            // 
            this.L1_ProcessStartResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_ProcessStartResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_ProcessStartResponse_Radio.ErrorOptionIndex = 98;
            this.L1_ProcessStartResponse_Radio.IndexChecked = 0;
            this.L1_ProcessStartResponse_Radio.Location = new System.Drawing.Point(591, 50);
            this.L1_ProcessStartResponse_Radio.MarginLeft = 16;
            this.L1_ProcessStartResponse_Radio.Name = "L1_ProcessStartResponse_Radio";
            this.L1_ProcessStartResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_ProcessStartResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_ProcessStartResponse_Radio.NormalOptionIndex = 99;
            this.L1_ProcessStartResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_ProcessStartResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_ProcessStartResponse_Radio.TabIndex = 12;
            this.L1_ProcessStartResponse_Radio.Text = "groupRadio1";
            // 
            // L1_RequestRecipeResponse_Radio
            // 
            this.L1_RequestRecipeResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_RequestRecipeResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_RequestRecipeResponse_Radio.ErrorOptionIndex = 98;
            this.L1_RequestRecipeResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_RequestRecipeResponse_Radio.IndexChecked = 0;
            this.L1_RequestRecipeResponse_Radio.Location = new System.Drawing.Point(589, 22);
            this.L1_RequestRecipeResponse_Radio.MarginLeft = 16;
            this.L1_RequestRecipeResponse_Radio.Name = "L1_RequestRecipeResponse_Radio";
            this.L1_RequestRecipeResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_RequestRecipeResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_RequestRecipeResponse_Radio.NormalOptionIndex = 99;
            this.L1_RequestRecipeResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_RequestRecipeResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_RequestRecipeResponse_Radio.TabIndex = 13;
            this.L1_RequestRecipeResponse_Radio.Text = "groupRadio1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Orchid;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Location = new System.Drawing.Point(7, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(157, 22);
            this.label21.TabIndex = 7;
            this.label21.Text = "ProcessEnd";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.Orchid;
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.Location = new System.Drawing.Point(7, 48);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(157, 22);
            this.label29.TabIndex = 7;
            this.label29.Text = "ProcessStart";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_ProcessEnd_Radio
            // 
            this.L1_ProcessEnd_Radio.BackColor = System.Drawing.Color.White;
            this.L1_ProcessEnd_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_ProcessEnd_Radio.ErrorOptionIndex = 98;
            this.L1_ProcessEnd_Radio.IndexChecked = 0;
            this.L1_ProcessEnd_Radio.Location = new System.Drawing.Point(167, 79);
            this.L1_ProcessEnd_Radio.MarginLeft = 16;
            this.L1_ProcessEnd_Radio.Name = "L1_ProcessEnd_Radio";
            this.L1_ProcessEnd_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_ProcessEnd_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_ProcessEnd_Radio.NormalOptionIndex = 99;
            this.L1_ProcessEnd_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_ProcessEnd_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_ProcessEnd_Radio.TabIndex = 8;
            this.L1_ProcessEnd_Radio.Text = "groupRadio1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Orchid;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label26.Location = new System.Drawing.Point(7, 21);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(158, 22);
            this.label26.TabIndex = 7;
            this.label26.Text = "RequestRecipe";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_ProcessStart_Radio
            // 
            this.L1_ProcessStart_Radio.BackColor = System.Drawing.Color.White;
            this.L1_ProcessStart_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_ProcessStart_Radio.ErrorOptionIndex = 98;
            this.L1_ProcessStart_Radio.IndexChecked = 0;
            this.L1_ProcessStart_Radio.Location = new System.Drawing.Point(167, 50);
            this.L1_ProcessStart_Radio.MarginLeft = 16;
            this.L1_ProcessStart_Radio.Name = "L1_ProcessStart_Radio";
            this.L1_ProcessStart_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_ProcessStart_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_ProcessStart_Radio.NormalOptionIndex = 99;
            this.L1_ProcessStart_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_ProcessStart_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_ProcessStart_Radio.TabIndex = 8;
            this.L1_ProcessStart_Radio.Text = "groupRadio1";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Location = new System.Drawing.Point(414, 79);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(170, 22);
            this.label22.TabIndex = 0;
            this.label22.Text = "ProcessEndResponse";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_RequestRecipe_Radio
            // 
            this.L1_RequestRecipe_Radio.BackColor = System.Drawing.Color.White;
            this.L1_RequestRecipe_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_RequestRecipe_Radio.ErrorOptionIndex = 98;
            this.L1_RequestRecipe_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_RequestRecipe_Radio.IndexChecked = 0;
            this.L1_RequestRecipe_Radio.Location = new System.Drawing.Point(167, 21);
            this.L1_RequestRecipe_Radio.MarginLeft = 16;
            this.L1_RequestRecipe_Radio.Name = "L1_RequestRecipe_Radio";
            this.L1_RequestRecipe_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_RequestRecipe_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_RequestRecipe_Radio.NormalOptionIndex = 99;
            this.L1_RequestRecipe_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_RequestRecipe_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_RequestRecipe_Radio.TabIndex = 8;
            this.L1_RequestRecipe_Radio.Text = "groupRadio1";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.Location = new System.Drawing.Point(414, 50);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(170, 22);
            this.label28.TabIndex = 0;
            this.label28.Text = "ProcessStartResponse";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label30.Location = new System.Drawing.Point(415, 22);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(168, 22);
            this.label30.TabIndex = 0;
            this.label30.Text = "RequestRecipeResponse";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrayLoadResponse_Radio
            // 
            this.L1_TrayLoadResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_TrayLoadResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_TrayLoadResponse_Radio.ErrorOptionIndex = 98;
            this.L1_TrayLoadResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_TrayLoadResponse_Radio.IndexChecked = 0;
            this.L1_TrayLoadResponse_Radio.Location = new System.Drawing.Point(550, 100);
            this.L1_TrayLoadResponse_Radio.MarginLeft = 16;
            this.L1_TrayLoadResponse_Radio.Name = "L1_TrayLoadResponse_Radio";
            this.L1_TrayLoadResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_TrayLoadResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_TrayLoadResponse_Radio.NormalOptionIndex = 99;
            this.L1_TrayLoadResponse_Radio.Size = new System.Drawing.Size(246, 20);
            this.L1_TrayLoadResponse_Radio.StringOptions = new string[] {
        "Clear",
        "OK",
        "Bypass"};
            this.L1_TrayLoadResponse_Radio.TabIndex = 6;
            this.L1_TrayLoadResponse_Radio.Text = "groupRadio1";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label32.Location = new System.Drawing.Point(414, 100);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(130, 22);
            this.label32.TabIndex = 0;
            this.label32.Text = "TrayLoadResponse";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrayLoad_Radio
            // 
            this.L1_TrayLoad_Radio.BackColor = System.Drawing.Color.White;
            this.L1_TrayLoad_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_TrayLoad_Radio.ErrorOptionIndex = 98;
            this.L1_TrayLoad_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_TrayLoad_Radio.IndexChecked = 0;
            this.L1_TrayLoad_Radio.Location = new System.Drawing.Point(141, 100);
            this.L1_TrayLoad_Radio.MarginLeft = 16;
            this.L1_TrayLoad_Radio.Name = "L1_TrayLoad_Radio";
            this.L1_TrayLoad_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_TrayLoad_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_TrayLoad_Radio.NormalOptionIndex = 99;
            this.L1_TrayLoad_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_TrayLoad_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_TrayLoad_Radio.TabIndex = 6;
            this.L1_TrayLoad_Radio.Text = "groupRadio1";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Orchid;
            this.label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label41.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label41.Location = new System.Drawing.Point(5, 98);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(130, 22);
            this.label41.TabIndex = 1;
            this.label41.Text = "TrayLoad";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.L1_ProcessId_TextBox);
            this.groupBox6.Controls.Add(this.label73);
            this.groupBox6.Controls.Add(this.L1_LotId_TextBox);
            this.groupBox6.Controls.Add(this.label74);
            this.groupBox6.Controls.Add(this.L1_RouteId_TextBox);
            this.groupBox6.Controls.Add(this.label72);
            this.groupBox6.Controls.Add(this.L1_Model_TextBox);
            this.groupBox6.Controls.Add(this.label71);
            this.groupBox6.Controls.Add(this.L1_TrayExist_Radio);
            this.groupBox6.Controls.Add(this.label34);
            this.groupBox6.Controls.Add(this.L1_TrayId_TextBox);
            this.groupBox6.Controls.Add(this.label35);
            this.groupBox6.Controls.Add(this.L1_RecipeId_TextBox);
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.L1_TrayLoad_Radio);
            this.groupBox6.Controls.Add(this.label41);
            this.groupBox6.Controls.Add(this.label32);
            this.groupBox6.Controls.Add(this.L1_TrayLoadResponse_Radio);
            this.groupBox6.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox6.Location = new System.Drawing.Point(548, 174);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(803, 132);
            this.groupBox6.TabIndex = 54;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TrayInformation";
            // 
            // L1_ProcessId_TextBox
            // 
            this.L1_ProcessId_TextBox.Location = new System.Drawing.Point(339, 42);
            this.L1_ProcessId_TextBox.Name = "L1_ProcessId_TextBox";
            this.L1_ProcessId_TextBox.ReadOnly = true;
            this.L1_ProcessId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_ProcessId_TextBox.TabIndex = 41;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.SkyBlue;
            this.label73.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label73.Location = new System.Drawing.Point(258, 42);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(77, 22);
            this.label73.TabIndex = 40;
            this.label73.Text = "ProcessId";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_LotId_TextBox
            // 
            this.L1_LotId_TextBox.Location = new System.Drawing.Point(591, 42);
            this.L1_LotId_TextBox.Name = "L1_LotId_TextBox";
            this.L1_LotId_TextBox.ReadOnly = true;
            this.L1_LotId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_LotId_TextBox.TabIndex = 39;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.SkyBlue;
            this.label74.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label74.Location = new System.Drawing.Point(511, 42);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(77, 22);
            this.label74.TabIndex = 38;
            this.label74.Text = "LotId";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_RouteId_TextBox
            // 
            this.L1_RouteId_TextBox.Location = new System.Drawing.Point(591, 17);
            this.L1_RouteId_TextBox.Name = "L1_RouteId_TextBox";
            this.L1_RouteId_TextBox.ReadOnly = true;
            this.L1_RouteId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_RouteId_TextBox.TabIndex = 37;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.SkyBlue;
            this.label72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label72.Location = new System.Drawing.Point(511, 17);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(77, 22);
            this.label72.TabIndex = 36;
            this.label72.Text = "RouteId";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_Model_TextBox
            // 
            this.L1_Model_TextBox.Location = new System.Drawing.Point(339, 17);
            this.L1_Model_TextBox.Name = "L1_Model_TextBox";
            this.L1_Model_TextBox.ReadOnly = true;
            this.L1_Model_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_Model_TextBox.TabIndex = 33;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.SkyBlue;
            this.label71.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label71.Location = new System.Drawing.Point(258, 17);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(77, 22);
            this.label71.TabIndex = 32;
            this.label71.Text = "Model";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrayExist_Radio
            // 
            this.L1_TrayExist_Radio.BackColor = System.Drawing.Color.White;
            this.L1_TrayExist_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_TrayExist_Radio.ErrorOptionIndex = 98;
            this.L1_TrayExist_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_TrayExist_Radio.IndexChecked = 0;
            this.L1_TrayExist_Radio.Location = new System.Drawing.Point(87, 18);
            this.L1_TrayExist_Radio.MarginLeft = 16;
            this.L1_TrayExist_Radio.Name = "L1_TrayExist_Radio";
            this.L1_TrayExist_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_TrayExist_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_TrayExist_Radio.NormalOptionIndex = 99;
            this.L1_TrayExist_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_TrayExist_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_TrayExist_Radio.TabIndex = 26;
            this.L1_TrayExist_Radio.Text = "groupRadio1";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Yellow;
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label34.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label34.Location = new System.Drawing.Point(6, 18);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(77, 22);
            this.label34.TabIndex = 23;
            this.label34.Text = "TrayExist";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrayId_TextBox
            // 
            this.L1_TrayId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_TrayId_TextBox.Location = new System.Drawing.Point(86, 42);
            this.L1_TrayId_TextBox.Name = "L1_TrayId_TextBox";
            this.L1_TrayId_TextBox.ReadOnly = true;
            this.L1_TrayId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_TrayId_TextBox.TabIndex = 25;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.SkyBlue;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label35.Location = new System.Drawing.Point(6, 42);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 22);
            this.label35.TabIndex = 24;
            this.label35.Text = "TrayId";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrackIn_CellCount
            // 
            this.L1_TrackIn_CellCount.Location = new System.Drawing.Point(1473, 174);
            this.L1_TrackIn_CellCount.Name = "L1_TrackIn_CellCount";
            this.L1_TrackIn_CellCount.ReadOnly = true;
            this.L1_TrackIn_CellCount.Size = new System.Drawing.Size(84, 21);
            this.L1_TrackIn_CellCount.TabIndex = 28;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.SkyBlue;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label31.Location = new System.Drawing.Point(1357, 174);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(110, 22);
            this.label31.TabIndex = 27;
            this.label31.Text = "TrackIN CellCount";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.L1_CellLoadCompleteResponse_Radio);
            this.groupBox7.Controls.Add(this.label50);
            this.groupBox7.Controls.Add(this.label49);
            this.groupBox7.Controls.Add(this.L1_CellLoadComplete_Radio);
            this.groupBox7.Controls.Add(this.L1_CellTrackIn_CellId_TextBox);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.L1_CellTrackIn_CellNo_TextBox);
            this.groupBox7.Controls.Add(this.label37);
            this.groupBox7.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox7.Location = new System.Drawing.Point(547, 425);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(393, 112);
            this.groupBox7.TabIndex = 55;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Cell Track IN";
            // 
            // L1_CellLoadCompleteResponse_Radio
            // 
            this.L1_CellLoadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_CellLoadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_CellLoadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.L1_CellLoadCompleteResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellLoadCompleteResponse_Radio.IndexChecked = 0;
            this.L1_CellLoadCompleteResponse_Radio.Location = new System.Drawing.Point(182, 78);
            this.L1_CellLoadCompleteResponse_Radio.MarginLeft = 16;
            this.L1_CellLoadCompleteResponse_Radio.Name = "L1_CellLoadCompleteResponse_Radio";
            this.L1_CellLoadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_CellLoadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_CellLoadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.L1_CellLoadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_CellLoadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_CellLoadCompleteResponse_Radio.TabIndex = 33;
            this.L1_CellLoadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label50.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label50.Location = new System.Drawing.Point(6, 78);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(170, 22);
            this.label50.TabIndex = 32;
            this.label50.Text = "CellLoadCompleteResponse";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.Orchid;
            this.label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label49.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label49.Location = new System.Drawing.Point(6, 51);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(170, 22);
            this.label49.TabIndex = 30;
            this.label49.Text = "CellLoadComplete";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_CellLoadComplete_Radio
            // 
            this.L1_CellLoadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.L1_CellLoadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_CellLoadComplete_Radio.ErrorOptionIndex = 98;
            this.L1_CellLoadComplete_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellLoadComplete_Radio.IndexChecked = 0;
            this.L1_CellLoadComplete_Radio.Location = new System.Drawing.Point(182, 53);
            this.L1_CellLoadComplete_Radio.MarginLeft = 16;
            this.L1_CellLoadComplete_Radio.Name = "L1_CellLoadComplete_Radio";
            this.L1_CellLoadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_CellLoadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_CellLoadComplete_Radio.NormalOptionIndex = 99;
            this.L1_CellLoadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_CellLoadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_CellLoadComplete_Radio.TabIndex = 31;
            this.L1_CellLoadComplete_Radio.Text = "groupRadio1";
            // 
            // L1_CellTrackIn_CellId_TextBox
            // 
            this.L1_CellTrackIn_CellId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellTrackIn_CellId_TextBox.Location = new System.Drawing.Point(222, 18);
            this.L1_CellTrackIn_CellId_TextBox.Name = "L1_CellTrackIn_CellId_TextBox";
            this.L1_CellTrackIn_CellId_TextBox.ReadOnly = true;
            this.L1_CellTrackIn_CellId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_CellTrackIn_CellId_TextBox.TabIndex = 29;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Yellow;
            this.label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label44.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label44.Location = new System.Drawing.Point(141, 18);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(77, 22);
            this.label44.TabIndex = 28;
            this.label44.Text = "Cell ID";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_CellTrackIn_CellNo_TextBox
            // 
            this.L1_CellTrackIn_CellNo_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellTrackIn_CellNo_TextBox.Location = new System.Drawing.Point(86, 18);
            this.L1_CellTrackIn_CellNo_TextBox.Name = "L1_CellTrackIn_CellNo_TextBox";
            this.L1_CellTrackIn_CellNo_TextBox.ReadOnly = true;
            this.L1_CellTrackIn_CellNo_TextBox.Size = new System.Drawing.Size(49, 22);
            this.L1_CellTrackIn_CellNo_TextBox.TabIndex = 27;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Yellow;
            this.label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label37.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label37.Location = new System.Drawing.Point(6, 18);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(77, 22);
            this.label37.TabIndex = 26;
            this.label37.Text = "Cell No";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.L1_CellUnloadCompleteResponse_Radio);
            this.groupBox9.Controls.Add(this.label51);
            this.groupBox9.Controls.Add(this.label52);
            this.groupBox9.Controls.Add(this.L1_CellUnloadComplete_Radio);
            this.groupBox9.Controls.Add(this.L1_CellTrackOut_CellId_TextBox);
            this.groupBox9.Controls.Add(this.label53);
            this.groupBox9.Controls.Add(this.L1_CellTrackOut_CellNo_TextBox);
            this.groupBox9.Controls.Add(this.label54);
            this.groupBox9.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox9.Location = new System.Drawing.Point(954, 426);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(393, 111);
            this.groupBox9.TabIndex = 56;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Cell Track OUT";
            // 
            // L1_CellUnloadCompleteResponse_Radio
            // 
            this.L1_CellUnloadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L1_CellUnloadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_CellUnloadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.L1_CellUnloadCompleteResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellUnloadCompleteResponse_Radio.IndexChecked = 0;
            this.L1_CellUnloadCompleteResponse_Radio.Location = new System.Drawing.Point(182, 77);
            this.L1_CellUnloadCompleteResponse_Radio.MarginLeft = 16;
            this.L1_CellUnloadCompleteResponse_Radio.Name = "L1_CellUnloadCompleteResponse_Radio";
            this.L1_CellUnloadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_CellUnloadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_CellUnloadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.L1_CellUnloadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_CellUnloadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_CellUnloadCompleteResponse_Radio.TabIndex = 33;
            this.L1_CellUnloadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label51.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label51.Location = new System.Drawing.Point(6, 77);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(170, 22);
            this.label51.TabIndex = 32;
            this.label51.Text = "CellUnloadCompleteResponse";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.Orchid;
            this.label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label52.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label52.Location = new System.Drawing.Point(6, 51);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(170, 22);
            this.label52.TabIndex = 30;
            this.label52.Text = "CellUnloadComplete";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_CellUnloadComplete_Radio
            // 
            this.L1_CellUnloadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.L1_CellUnloadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L1_CellUnloadComplete_Radio.ErrorOptionIndex = 98;
            this.L1_CellUnloadComplete_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellUnloadComplete_Radio.IndexChecked = 0;
            this.L1_CellUnloadComplete_Radio.Location = new System.Drawing.Point(182, 52);
            this.L1_CellUnloadComplete_Radio.MarginLeft = 16;
            this.L1_CellUnloadComplete_Radio.Name = "L1_CellUnloadComplete_Radio";
            this.L1_CellUnloadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L1_CellUnloadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L1_CellUnloadComplete_Radio.NormalOptionIndex = 99;
            this.L1_CellUnloadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.L1_CellUnloadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L1_CellUnloadComplete_Radio.TabIndex = 31;
            this.L1_CellUnloadComplete_Radio.Text = "groupRadio1";
            // 
            // L1_CellTrackOut_CellId_TextBox
            // 
            this.L1_CellTrackOut_CellId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellTrackOut_CellId_TextBox.Location = new System.Drawing.Point(222, 18);
            this.L1_CellTrackOut_CellId_TextBox.Name = "L1_CellTrackOut_CellId_TextBox";
            this.L1_CellTrackOut_CellId_TextBox.ReadOnly = true;
            this.L1_CellTrackOut_CellId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L1_CellTrackOut_CellId_TextBox.TabIndex = 29;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.Yellow;
            this.label53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label53.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label53.Location = new System.Drawing.Point(141, 18);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(77, 22);
            this.label53.TabIndex = 28;
            this.label53.Text = "Cell ID";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_CellTrackOut_CellNo_TextBox
            // 
            this.L1_CellTrackOut_CellNo_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L1_CellTrackOut_CellNo_TextBox.Location = new System.Drawing.Point(86, 18);
            this.L1_CellTrackOut_CellNo_TextBox.Name = "L1_CellTrackOut_CellNo_TextBox";
            this.L1_CellTrackOut_CellNo_TextBox.ReadOnly = true;
            this.L1_CellTrackOut_CellNo_TextBox.Size = new System.Drawing.Size(49, 22);
            this.L1_CellTrackOut_CellNo_TextBox.TabIndex = 27;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.Yellow;
            this.label54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label54.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label54.Location = new System.Drawing.Point(6, 18);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(77, 22);
            this.label54.TabIndex = 26;
            this.label54.Text = "Cell No";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.L1_ProcessStart_Radio);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.L1_ProcessStartResponse_Radio);
            this.groupBox10.Controls.Add(this.label21);
            this.groupBox10.Controls.Add(this.L1_ProcessEnd_Radio);
            this.groupBox10.Controls.Add(this.label22);
            this.groupBox10.Controls.Add(this.L1_ProcessEndResponse_Radio);
            this.groupBox10.Controls.Add(this.label30);
            this.groupBox10.Controls.Add(this.L1_RequestRecipe_Radio);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Controls.Add(this.L1_RequestRecipeResponse_Radio);
            this.groupBox10.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox10.Location = new System.Drawing.Point(547, 312);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(804, 107);
            this.groupBox10.TabIndex = 57;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Tray Process";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.L2_ProcessStart_Radio);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.L2_ProcessStartResponse_Radio);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.L2_ProcessEnd_Radio);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.L2_ProcessEndResponse_Radio);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.L2_RequestRecipe_Radio);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.L2_RequestRecipeResponse_Radio);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox4.Location = new System.Drawing.Point(547, 701);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(804, 107);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tray Process";
            // 
            // L2_ProcessStart_Radio
            // 
            this.L2_ProcessStart_Radio.BackColor = System.Drawing.Color.White;
            this.L2_ProcessStart_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_ProcessStart_Radio.ErrorOptionIndex = 98;
            this.L2_ProcessStart_Radio.IndexChecked = 0;
            this.L2_ProcessStart_Radio.Location = new System.Drawing.Point(167, 50);
            this.L2_ProcessStart_Radio.MarginLeft = 16;
            this.L2_ProcessStart_Radio.Name = "L2_ProcessStart_Radio";
            this.L2_ProcessStart_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_ProcessStart_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_ProcessStart_Radio.NormalOptionIndex = 99;
            this.L2_ProcessStart_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_ProcessStart_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_ProcessStart_Radio.TabIndex = 8;
            this.L2_ProcessStart_Radio.Text = "groupRadio1";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Orchid;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Location = new System.Drawing.Point(7, 48);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(157, 22);
            this.label23.TabIndex = 7;
            this.label23.Text = "ProcessStart";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Location = new System.Drawing.Point(414, 50);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(170, 22);
            this.label24.TabIndex = 0;
            this.label24.Text = "ProcessStartResponse";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_ProcessStartResponse_Radio
            // 
            this.L2_ProcessStartResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_ProcessStartResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_ProcessStartResponse_Radio.ErrorOptionIndex = 98;
            this.L2_ProcessStartResponse_Radio.IndexChecked = 0;
            this.L2_ProcessStartResponse_Radio.Location = new System.Drawing.Point(591, 50);
            this.L2_ProcessStartResponse_Radio.MarginLeft = 16;
            this.L2_ProcessStartResponse_Radio.Name = "L2_ProcessStartResponse_Radio";
            this.L2_ProcessStartResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_ProcessStartResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_ProcessStartResponse_Radio.NormalOptionIndex = 99;
            this.L2_ProcessStartResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_ProcessStartResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_ProcessStartResponse_Radio.TabIndex = 12;
            this.L2_ProcessStartResponse_Radio.Text = "groupRadio1";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Orchid;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Location = new System.Drawing.Point(7, 77);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(157, 22);
            this.label25.TabIndex = 7;
            this.label25.Text = "ProcessEnd";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_ProcessEnd_Radio
            // 
            this.L2_ProcessEnd_Radio.BackColor = System.Drawing.Color.White;
            this.L2_ProcessEnd_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_ProcessEnd_Radio.ErrorOptionIndex = 98;
            this.L2_ProcessEnd_Radio.IndexChecked = 0;
            this.L2_ProcessEnd_Radio.Location = new System.Drawing.Point(167, 79);
            this.L2_ProcessEnd_Radio.MarginLeft = 16;
            this.L2_ProcessEnd_Radio.Name = "L2_ProcessEnd_Radio";
            this.L2_ProcessEnd_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_ProcessEnd_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_ProcessEnd_Radio.NormalOptionIndex = 99;
            this.L2_ProcessEnd_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_ProcessEnd_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_ProcessEnd_Radio.TabIndex = 8;
            this.L2_ProcessEnd_Radio.Text = "groupRadio1";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Location = new System.Drawing.Point(414, 79);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(170, 22);
            this.label27.TabIndex = 0;
            this.label27.Text = "ProcessEndResponse";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_ProcessEndResponse_Radio
            // 
            this.L2_ProcessEndResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_ProcessEndResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_ProcessEndResponse_Radio.ErrorOptionIndex = 98;
            this.L2_ProcessEndResponse_Radio.IndexChecked = 0;
            this.L2_ProcessEndResponse_Radio.Location = new System.Drawing.Point(591, 79);
            this.L2_ProcessEndResponse_Radio.MarginLeft = 16;
            this.L2_ProcessEndResponse_Radio.Name = "L2_ProcessEndResponse_Radio";
            this.L2_ProcessEndResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_ProcessEndResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_ProcessEndResponse_Radio.NormalOptionIndex = 99;
            this.L2_ProcessEndResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_ProcessEndResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_ProcessEndResponse_Radio.TabIndex = 12;
            this.L2_ProcessEndResponse_Radio.Text = "groupRadio1";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label33.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label33.Location = new System.Drawing.Point(415, 22);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(168, 22);
            this.label33.TabIndex = 0;
            this.label33.Text = "RequestRecipeResponse";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_RequestRecipe_Radio
            // 
            this.L2_RequestRecipe_Radio.BackColor = System.Drawing.Color.White;
            this.L2_RequestRecipe_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_RequestRecipe_Radio.ErrorOptionIndex = 98;
            this.L2_RequestRecipe_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_RequestRecipe_Radio.IndexChecked = 0;
            this.L2_RequestRecipe_Radio.Location = new System.Drawing.Point(167, 21);
            this.L2_RequestRecipe_Radio.MarginLeft = 16;
            this.L2_RequestRecipe_Radio.Name = "L2_RequestRecipe_Radio";
            this.L2_RequestRecipe_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_RequestRecipe_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_RequestRecipe_Radio.NormalOptionIndex = 99;
            this.L2_RequestRecipe_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_RequestRecipe_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_RequestRecipe_Radio.TabIndex = 8;
            this.L2_RequestRecipe_Radio.Text = "groupRadio1";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.Orchid;
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label36.Location = new System.Drawing.Point(7, 21);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(158, 22);
            this.label36.TabIndex = 7;
            this.label36.Text = "RequestRecipe";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_RequestRecipeResponse_Radio
            // 
            this.L2_RequestRecipeResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_RequestRecipeResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_RequestRecipeResponse_Radio.ErrorOptionIndex = 98;
            this.L2_RequestRecipeResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_RequestRecipeResponse_Radio.IndexChecked = 0;
            this.L2_RequestRecipeResponse_Radio.Location = new System.Drawing.Point(589, 22);
            this.L2_RequestRecipeResponse_Radio.MarginLeft = 16;
            this.L2_RequestRecipeResponse_Radio.Name = "L2_RequestRecipeResponse_Radio";
            this.L2_RequestRecipeResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_RequestRecipeResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_RequestRecipeResponse_Radio.NormalOptionIndex = 99;
            this.L2_RequestRecipeResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_RequestRecipeResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_RequestRecipeResponse_Radio.TabIndex = 13;
            this.L2_RequestRecipeResponse_Radio.Text = "groupRadio1";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.L2_CellUnloadCompleteResponse_Radio);
            this.groupBox8.Controls.Add(this.label39);
            this.groupBox8.Controls.Add(this.label40);
            this.groupBox8.Controls.Add(this.L2_CellUnloadComplete_Radio);
            this.groupBox8.Controls.Add(this.L2_CellTrackOut_CellId_TextBox);
            this.groupBox8.Controls.Add(this.label42);
            this.groupBox8.Controls.Add(this.L2_CellTrackOut_CellNo_TextBox);
            this.groupBox8.Controls.Add(this.label43);
            this.groupBox8.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox8.Location = new System.Drawing.Point(954, 815);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(393, 111);
            this.groupBox8.TabIndex = 60;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Cell Track OUT";
            // 
            // L2_CellUnloadCompleteResponse_Radio
            // 
            this.L2_CellUnloadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_CellUnloadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_CellUnloadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.L2_CellUnloadCompleteResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellUnloadCompleteResponse_Radio.IndexChecked = 0;
            this.L2_CellUnloadCompleteResponse_Radio.Location = new System.Drawing.Point(182, 77);
            this.L2_CellUnloadCompleteResponse_Radio.MarginLeft = 16;
            this.L2_CellUnloadCompleteResponse_Radio.Name = "L2_CellUnloadCompleteResponse_Radio";
            this.L2_CellUnloadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_CellUnloadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_CellUnloadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.L2_CellUnloadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_CellUnloadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_CellUnloadCompleteResponse_Radio.TabIndex = 33;
            this.L2_CellUnloadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label39.Location = new System.Drawing.Point(6, 77);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(170, 22);
            this.label39.TabIndex = 32;
            this.label39.Text = "CellUnloadCompleteResponse";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.Orchid;
            this.label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label40.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label40.Location = new System.Drawing.Point(6, 51);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(170, 22);
            this.label40.TabIndex = 30;
            this.label40.Text = "CellUnloadComplete";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_CellUnloadComplete_Radio
            // 
            this.L2_CellUnloadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.L2_CellUnloadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_CellUnloadComplete_Radio.ErrorOptionIndex = 98;
            this.L2_CellUnloadComplete_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellUnloadComplete_Radio.IndexChecked = 0;
            this.L2_CellUnloadComplete_Radio.Location = new System.Drawing.Point(182, 53);
            this.L2_CellUnloadComplete_Radio.MarginLeft = 16;
            this.L2_CellUnloadComplete_Radio.Name = "L2_CellUnloadComplete_Radio";
            this.L2_CellUnloadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_CellUnloadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_CellUnloadComplete_Radio.NormalOptionIndex = 99;
            this.L2_CellUnloadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_CellUnloadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_CellUnloadComplete_Radio.TabIndex = 31;
            this.L2_CellUnloadComplete_Radio.Text = "groupRadio1";
            // 
            // L2_CellTrackOut_CellId_TextBox
            // 
            this.L2_CellTrackOut_CellId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellTrackOut_CellId_TextBox.Location = new System.Drawing.Point(222, 18);
            this.L2_CellTrackOut_CellId_TextBox.Name = "L2_CellTrackOut_CellId_TextBox";
            this.L2_CellTrackOut_CellId_TextBox.ReadOnly = true;
            this.L2_CellTrackOut_CellId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_CellTrackOut_CellId_TextBox.TabIndex = 29;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.Yellow;
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label42.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label42.Location = new System.Drawing.Point(141, 18);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(77, 22);
            this.label42.TabIndex = 28;
            this.label42.Text = "Cell ID";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_CellTrackOut_CellNo_TextBox
            // 
            this.L2_CellTrackOut_CellNo_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellTrackOut_CellNo_TextBox.Location = new System.Drawing.Point(86, 18);
            this.L2_CellTrackOut_CellNo_TextBox.Name = "L2_CellTrackOut_CellNo_TextBox";
            this.L2_CellTrackOut_CellNo_TextBox.ReadOnly = true;
            this.L2_CellTrackOut_CellNo_TextBox.Size = new System.Drawing.Size(49, 22);
            this.L2_CellTrackOut_CellNo_TextBox.TabIndex = 27;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.Yellow;
            this.label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label43.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label43.Location = new System.Drawing.Point(6, 18);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(77, 22);
            this.label43.TabIndex = 26;
            this.label43.Text = "Cell No";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.L2_CellLoadCompleteResponse_Radio);
            this.groupBox11.Controls.Add(this.label55);
            this.groupBox11.Controls.Add(this.label56);
            this.groupBox11.Controls.Add(this.L2_CellLoadComplete_Radio);
            this.groupBox11.Controls.Add(this.L2_CellTrackIn_CellId_TextBox);
            this.groupBox11.Controls.Add(this.label57);
            this.groupBox11.Controls.Add(this.L2_CellTrackIn_CellNo_TextBox);
            this.groupBox11.Controls.Add(this.label58);
            this.groupBox11.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox11.Location = new System.Drawing.Point(547, 814);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(393, 112);
            this.groupBox11.TabIndex = 59;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Cell Track IN";
            // 
            // L2_CellLoadCompleteResponse_Radio
            // 
            this.L2_CellLoadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_CellLoadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_CellLoadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.L2_CellLoadCompleteResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellLoadCompleteResponse_Radio.IndexChecked = 0;
            this.L2_CellLoadCompleteResponse_Radio.Location = new System.Drawing.Point(182, 78);
            this.L2_CellLoadCompleteResponse_Radio.MarginLeft = 16;
            this.L2_CellLoadCompleteResponse_Radio.Name = "L2_CellLoadCompleteResponse_Radio";
            this.L2_CellLoadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_CellLoadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_CellLoadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.L2_CellLoadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_CellLoadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_CellLoadCompleteResponse_Radio.TabIndex = 33;
            this.L2_CellLoadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label55.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label55.Location = new System.Drawing.Point(6, 78);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(170, 22);
            this.label55.TabIndex = 32;
            this.label55.Text = "CellLoadCompleteResponse";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.Orchid;
            this.label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label56.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label56.Location = new System.Drawing.Point(6, 51);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(170, 22);
            this.label56.TabIndex = 30;
            this.label56.Text = "CellLoadComplete";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_CellLoadComplete_Radio
            // 
            this.L2_CellLoadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.L2_CellLoadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_CellLoadComplete_Radio.ErrorOptionIndex = 98;
            this.L2_CellLoadComplete_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellLoadComplete_Radio.IndexChecked = 0;
            this.L2_CellLoadComplete_Radio.Location = new System.Drawing.Point(182, 54);
            this.L2_CellLoadComplete_Radio.MarginLeft = 16;
            this.L2_CellLoadComplete_Radio.Name = "L2_CellLoadComplete_Radio";
            this.L2_CellLoadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_CellLoadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_CellLoadComplete_Radio.NormalOptionIndex = 99;
            this.L2_CellLoadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_CellLoadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_CellLoadComplete_Radio.TabIndex = 31;
            this.L2_CellLoadComplete_Radio.Text = "groupRadio1";
            // 
            // L2_CellTrackIn_CellId_TextBox
            // 
            this.L2_CellTrackIn_CellId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellTrackIn_CellId_TextBox.Location = new System.Drawing.Point(222, 18);
            this.L2_CellTrackIn_CellId_TextBox.Name = "L2_CellTrackIn_CellId_TextBox";
            this.L2_CellTrackIn_CellId_TextBox.ReadOnly = true;
            this.L2_CellTrackIn_CellId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_CellTrackIn_CellId_TextBox.TabIndex = 29;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.Yellow;
            this.label57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label57.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label57.Location = new System.Drawing.Point(141, 18);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(77, 22);
            this.label57.TabIndex = 28;
            this.label57.Text = "Cell ID";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_CellTrackIn_CellNo_TextBox
            // 
            this.L2_CellTrackIn_CellNo_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_CellTrackIn_CellNo_TextBox.Location = new System.Drawing.Point(86, 18);
            this.L2_CellTrackIn_CellNo_TextBox.Name = "L2_CellTrackIn_CellNo_TextBox";
            this.L2_CellTrackIn_CellNo_TextBox.ReadOnly = true;
            this.L2_CellTrackIn_CellNo_TextBox.Size = new System.Drawing.Size(49, 22);
            this.L2_CellTrackIn_CellNo_TextBox.TabIndex = 27;
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.Yellow;
            this.label58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label58.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label58.Location = new System.Drawing.Point(6, 18);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(77, 22);
            this.label58.TabIndex = 26;
            this.label58.Text = "Cell No";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.SkyBlue;
            this.label65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label65.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label65.Location = new System.Drawing.Point(1563, 173);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(126, 22);
            this.label65.TabIndex = 27;
            this.label65.Text = "TrackOUT CellCount";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrackOut_CellCount
            // 
            this.L1_TrackOut_CellCount.Location = new System.Drawing.Point(1695, 173);
            this.L1_TrackOut_CellCount.Name = "L1_TrackOut_CellCount";
            this.L1_TrackOut_CellCount.ReadOnly = true;
            this.L1_TrackOut_CellCount.Size = new System.Drawing.Size(68, 21);
            this.L1_TrackOut_CellCount.TabIndex = 28;
            // 
            // L1_TrackInCellList_dataGridView
            // 
            this.L1_TrackInCellList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.L1_TrackInCellList_dataGridView.Location = new System.Drawing.Point(1357, 201);
            this.L1_TrackInCellList_dataGridView.Name = "L1_TrackInCellList_dataGridView";
            this.L1_TrackInCellList_dataGridView.RowTemplate.Height = 23;
            this.L1_TrackInCellList_dataGridView.Size = new System.Drawing.Size(200, 336);
            this.L1_TrackInCellList_dataGridView.TabIndex = 63;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.SkyBlue;
            this.label67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label67.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label67.Location = new System.Drawing.Point(1357, 153);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(200, 17);
            this.label67.TabIndex = 62;
            this.label67.Text = "TrackIn Cell Information";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1_TrackOutCellList_dataGridView
            // 
            this.L1_TrackOutCellList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.L1_TrackOutCellList_dataGridView.Location = new System.Drawing.Point(1563, 201);
            this.L1_TrackOutCellList_dataGridView.Name = "L1_TrackOutCellList_dataGridView";
            this.L1_TrackOutCellList_dataGridView.RowTemplate.Height = 23;
            this.L1_TrackOutCellList_dataGridView.Size = new System.Drawing.Size(200, 336);
            this.L1_TrackOutCellList_dataGridView.TabIndex = 65;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.Yellow;
            this.label68.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label68.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label68.Location = new System.Drawing.Point(1563, 153);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(200, 17);
            this.label68.TabIndex = 64;
            this.label68.Text = "TrackIn Cell Information";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.L2_ProcessId_TextBox);
            this.groupBox12.Controls.Add(this.label59);
            this.groupBox12.Controls.Add(this.L2_LotId_TextBox);
            this.groupBox12.Controls.Add(this.label60);
            this.groupBox12.Controls.Add(this.L2_RouteId_TextBox);
            this.groupBox12.Controls.Add(this.label61);
            this.groupBox12.Controls.Add(this.L2_Model_TextBox);
            this.groupBox12.Controls.Add(this.label62);
            this.groupBox12.Controls.Add(this.L2_TrayExist_Radio);
            this.groupBox12.Controls.Add(this.label63);
            this.groupBox12.Controls.Add(this.L2_TrayId_TextBox);
            this.groupBox12.Controls.Add(this.label64);
            this.groupBox12.Controls.Add(this.L2_RecipeId_TextBox);
            this.groupBox12.Controls.Add(this.label66);
            this.groupBox12.Controls.Add(this.L2_TrayLoad_Radio);
            this.groupBox12.Controls.Add(this.label75);
            this.groupBox12.Controls.Add(this.label76);
            this.groupBox12.Controls.Add(this.L2_TrayLoadResponse_Radio);
            this.groupBox12.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox12.Location = new System.Drawing.Point(548, 562);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(803, 132);
            this.groupBox12.TabIndex = 70;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "TrayInformation";
            // 
            // L2_ProcessId_TextBox
            // 
            this.L2_ProcessId_TextBox.Location = new System.Drawing.Point(339, 42);
            this.L2_ProcessId_TextBox.Name = "L2_ProcessId_TextBox";
            this.L2_ProcessId_TextBox.ReadOnly = true;
            this.L2_ProcessId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_ProcessId_TextBox.TabIndex = 41;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.SkyBlue;
            this.label59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label59.Location = new System.Drawing.Point(258, 42);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(77, 22);
            this.label59.TabIndex = 40;
            this.label59.Text = "ProcessId";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_LotId_TextBox
            // 
            this.L2_LotId_TextBox.Location = new System.Drawing.Point(591, 42);
            this.L2_LotId_TextBox.Name = "L2_LotId_TextBox";
            this.L2_LotId_TextBox.ReadOnly = true;
            this.L2_LotId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_LotId_TextBox.TabIndex = 39;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.SkyBlue;
            this.label60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label60.Location = new System.Drawing.Point(511, 42);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(77, 22);
            this.label60.TabIndex = 38;
            this.label60.Text = "LotId";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_RouteId_TextBox
            // 
            this.L2_RouteId_TextBox.Location = new System.Drawing.Point(591, 17);
            this.L2_RouteId_TextBox.Name = "L2_RouteId_TextBox";
            this.L2_RouteId_TextBox.ReadOnly = true;
            this.L2_RouteId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_RouteId_TextBox.TabIndex = 37;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.SkyBlue;
            this.label61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label61.Location = new System.Drawing.Point(511, 17);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(77, 22);
            this.label61.TabIndex = 36;
            this.label61.Text = "RouteId";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_Model_TextBox
            // 
            this.L2_Model_TextBox.Location = new System.Drawing.Point(339, 17);
            this.L2_Model_TextBox.Name = "L2_Model_TextBox";
            this.L2_Model_TextBox.ReadOnly = true;
            this.L2_Model_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_Model_TextBox.TabIndex = 33;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.SkyBlue;
            this.label62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label62.Location = new System.Drawing.Point(258, 17);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(77, 22);
            this.label62.TabIndex = 32;
            this.label62.Text = "Model";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrayExist_Radio
            // 
            this.L2_TrayExist_Radio.BackColor = System.Drawing.Color.White;
            this.L2_TrayExist_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_TrayExist_Radio.ErrorOptionIndex = 98;
            this.L2_TrayExist_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_TrayExist_Radio.IndexChecked = 0;
            this.L2_TrayExist_Radio.Location = new System.Drawing.Point(87, 18);
            this.L2_TrayExist_Radio.MarginLeft = 16;
            this.L2_TrayExist_Radio.Name = "L2_TrayExist_Radio";
            this.L2_TrayExist_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_TrayExist_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_TrayExist_Radio.NormalOptionIndex = 99;
            this.L2_TrayExist_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_TrayExist_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_TrayExist_Radio.TabIndex = 26;
            this.L2_TrayExist_Radio.Text = "groupRadio16";
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.Yellow;
            this.label63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label63.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label63.Location = new System.Drawing.Point(6, 18);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(77, 22);
            this.label63.TabIndex = 23;
            this.label63.Text = "TrayExist";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrayId_TextBox
            // 
            this.L2_TrayId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_TrayId_TextBox.Location = new System.Drawing.Point(86, 42);
            this.L2_TrayId_TextBox.Name = "L2_TrayId_TextBox";
            this.L2_TrayId_TextBox.ReadOnly = true;
            this.L2_TrayId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_TrayId_TextBox.TabIndex = 25;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.SkyBlue;
            this.label64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label64.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label64.Location = new System.Drawing.Point(6, 42);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(77, 22);
            this.label64.TabIndex = 24;
            this.label64.Text = "TrayId";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_RecipeId_TextBox
            // 
            this.L2_RecipeId_TextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_RecipeId_TextBox.Location = new System.Drawing.Point(86, 69);
            this.L2_RecipeId_TextBox.Name = "L2_RecipeId_TextBox";
            this.L2_RecipeId_TextBox.ReadOnly = true;
            this.L2_RecipeId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.L2_RecipeId_TextBox.TabIndex = 22;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.SkyBlue;
            this.label66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label66.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label66.Location = new System.Drawing.Point(6, 69);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(77, 22);
            this.label66.TabIndex = 21;
            this.label66.Text = "RecipeId";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrayLoad_Radio
            // 
            this.L2_TrayLoad_Radio.BackColor = System.Drawing.Color.White;
            this.L2_TrayLoad_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_TrayLoad_Radio.ErrorOptionIndex = 98;
            this.L2_TrayLoad_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_TrayLoad_Radio.IndexChecked = 0;
            this.L2_TrayLoad_Radio.Location = new System.Drawing.Point(141, 100);
            this.L2_TrayLoad_Radio.MarginLeft = 16;
            this.L2_TrayLoad_Radio.Name = "L2_TrayLoad_Radio";
            this.L2_TrayLoad_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_TrayLoad_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_TrayLoad_Radio.NormalOptionIndex = 99;
            this.L2_TrayLoad_Radio.Size = new System.Drawing.Size(162, 20);
            this.L2_TrayLoad_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.L2_TrayLoad_Radio.TabIndex = 6;
            this.L2_TrayLoad_Radio.Text = "groupRadio1";
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.Orchid;
            this.label75.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label75.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label75.Location = new System.Drawing.Point(5, 98);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(130, 22);
            this.label75.TabIndex = 1;
            this.label75.Text = "TrayLoad";
            this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label76.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label76.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label76.Location = new System.Drawing.Point(414, 100);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(130, 22);
            this.label76.TabIndex = 0;
            this.label76.Text = "TrayLoadResponse";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrayLoadResponse_Radio
            // 
            this.L2_TrayLoadResponse_Radio.BackColor = System.Drawing.Color.White;
            this.L2_TrayLoadResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.L2_TrayLoadResponse_Radio.ErrorOptionIndex = 98;
            this.L2_TrayLoadResponse_Radio.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.L2_TrayLoadResponse_Radio.IndexChecked = 0;
            this.L2_TrayLoadResponse_Radio.Location = new System.Drawing.Point(550, 100);
            this.L2_TrayLoadResponse_Radio.MarginLeft = 16;
            this.L2_TrayLoadResponse_Radio.Name = "L2_TrayLoadResponse_Radio";
            this.L2_TrayLoadResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.L2_TrayLoadResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.L2_TrayLoadResponse_Radio.NormalOptionIndex = 99;
            this.L2_TrayLoadResponse_Radio.Size = new System.Drawing.Size(246, 20);
            this.L2_TrayLoadResponse_Radio.StringOptions = new string[] {
        "Clear",
        "OK",
        "Bypass"};
            this.L2_TrayLoadResponse_Radio.TabIndex = 6;
            this.L2_TrayLoadResponse_Radio.Text = "groupRadio1";
            // 
            // L2_TrackOutCellList_dataGridView
            // 
            this.L2_TrackOutCellList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.L2_TrackOutCellList_dataGridView.Location = new System.Drawing.Point(1563, 610);
            this.L2_TrackOutCellList_dataGridView.Name = "L2_TrackOutCellList_dataGridView";
            this.L2_TrackOutCellList_dataGridView.RowTemplate.Height = 23;
            this.L2_TrackOutCellList_dataGridView.Size = new System.Drawing.Size(200, 336);
            this.L2_TrackOutCellList_dataGridView.TabIndex = 78;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.Yellow;
            this.label69.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label69.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label69.Location = new System.Drawing.Point(1563, 562);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(200, 17);
            this.label69.TabIndex = 77;
            this.label69.Text = "TrackIn Cell Information";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrackInCellList_dataGridView
            // 
            this.L2_TrackInCellList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.L2_TrackInCellList_dataGridView.Location = new System.Drawing.Point(1357, 610);
            this.L2_TrackInCellList_dataGridView.Name = "L2_TrackInCellList_dataGridView";
            this.L2_TrackInCellList_dataGridView.RowTemplate.Height = 23;
            this.L2_TrackInCellList_dataGridView.Size = new System.Drawing.Size(200, 336);
            this.L2_TrackInCellList_dataGridView.TabIndex = 76;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.SkyBlue;
            this.label70.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label70.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label70.Location = new System.Drawing.Point(1357, 562);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(200, 17);
            this.label70.TabIndex = 75;
            this.label70.Text = "TrackIn Cell Information";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrackOut_CellCount
            // 
            this.L2_TrackOut_CellCount.Location = new System.Drawing.Point(1695, 582);
            this.L2_TrackOut_CellCount.Name = "L2_TrackOut_CellCount";
            this.L2_TrackOut_CellCount.ReadOnly = true;
            this.L2_TrackOut_CellCount.Size = new System.Drawing.Size(68, 21);
            this.L2_TrackOut_CellCount.TabIndex = 73;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.SkyBlue;
            this.label77.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label77.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label77.Location = new System.Drawing.Point(1563, 582);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(126, 22);
            this.label77.TabIndex = 71;
            this.label77.Text = "TrackOUT CellCount";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L2_TrackIn_CellCount
            // 
            this.L2_TrackIn_CellCount.Location = new System.Drawing.Point(1473, 583);
            this.L2_TrackIn_CellCount.Name = "L2_TrackIn_CellCount";
            this.L2_TrackIn_CellCount.ReadOnly = true;
            this.L2_TrackIn_CellCount.Size = new System.Drawing.Size(84, 21);
            this.L2_TrackIn_CellCount.TabIndex = 74;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.SkyBlue;
            this.label78.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label78.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label78.Location = new System.Drawing.Point(1357, 583);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(110, 22);
            this.label78.TabIndex = 72;
            this.label78.Text = "TrackIN CellCount";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Mode_Radio
            // 
            this.Mode_Radio.BackColor = System.Drawing.Color.White;
            this.Mode_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.Mode_Radio.ErrorOptionIndex = 98;
            this.Mode_Radio.IndexChecked = 1;
            this.Mode_Radio.Location = new System.Drawing.Point(93, 46);
            this.Mode_Radio.MarginLeft = 16;
            this.Mode_Radio.Name = "Mode_Radio";
            this.Mode_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.Mode_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.Mode_Radio.NormalOptionIndex = 99;
            this.Mode_Radio.Size = new System.Drawing.Size(225, 20);
            this.Mode_Radio.StringOptions = new string[] {
        "Maint.",
        "Manual",
        "Control"};
            this.Mode_Radio.TabIndex = 8;
            this.Mode_Radio.Text = "groupRadio1";
            // 
            // LeakCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.L2_TrackOutCellList_dataGridView);
            this.Controls.Add(this.label69);
            this.Controls.Add(this.L2_TrackInCellList_dataGridView);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.L2_TrackOut_CellCount);
            this.Controls.Add(this.label77);
            this.Controls.Add(this.L2_TrackIn_CellCount);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.L1_TrackOutCellList_dataGridView);
            this.Controls.Add(this.label68);
            this.Controls.Add(this.L1_TrackInCellList_dataGridView);
            this.Controls.Add(this.label67);
            this.Controls.Add(this.L1_TrackOut_CellCount);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.L1_TrackIn_CellCount);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.RestApiServer);
            this.Controls.Add(this.FMSClient);
            this.Controls.Add(this.EQPClient);
            this.Controls.Add(this.groupBox6);
            this.Name = "LeakCheck";
            this.Size = new System.Drawing.Size(1773, 951);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.L1_TrackInCellList_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.L1_TrackOutCellList_dataGridView)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.L2_TrackOutCellList_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.L2_TrackInCellList_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RestAPIServer.RestApiServer RestApiServer;
        private OPCUAClient.OPCUAClient FMSClient;
        private OPCUAClient.OPCUAClient EQPClient;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Command_TextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private CommonCtrls.GroupRadio CommandResponse_Radio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox FMS_ErrorNo_TextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private CommonCtrls.GroupRadio FMS_Status_Radio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox5;
        private CommonCtrls.GroupRadio Common_Alive_Radio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private CommonCtrls.GroupRadio EQP_ErrorLevel_Radio;
        private CommonCtrls.GroupRadio Power_Radio;
        private System.Windows.Forms.TextBox Status_TextBox;
        private System.Windows.Forms.TextBox EQP_ErrorNo_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox L1_RecipeId_TextBox;
        private System.Windows.Forms.Label label38;
        private CommonCtrls.GroupRadio L1_ProcessEndResponse_Radio;
        private CommonCtrls.GroupRadio L1_ProcessStartResponse_Radio;
        private CommonCtrls.GroupRadio L1_RequestRecipeResponse_Radio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label29;
        private CommonCtrls.GroupRadio L1_ProcessEnd_Radio;
        private System.Windows.Forms.Label label26;
        private CommonCtrls.GroupRadio L1_ProcessStart_Radio;
        private System.Windows.Forms.Label label22;
        private CommonCtrls.GroupRadio L1_RequestRecipe_Radio;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private CommonCtrls.GroupRadio L1_TrayLoadResponse_Radio;
        private System.Windows.Forms.Label label32;
        private CommonCtrls.GroupRadio L1_TrayLoad_Radio;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox L1_TrackIn_CellCount;
        private System.Windows.Forms.Label label31;
        private CommonCtrls.GroupRadio L1_TrayExist_Radio;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox L1_TrayId_TextBox;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox7;
        private CommonCtrls.GroupRadio L1_CellLoadCompleteResponse_Radio;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private CommonCtrls.GroupRadio L1_CellLoadComplete_Radio;
        private System.Windows.Forms.TextBox L1_CellTrackIn_CellId_TextBox;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox L1_CellTrackIn_CellNo_TextBox;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox groupBox9;
        private CommonCtrls.GroupRadio L1_CellUnloadCompleteResponse_Radio;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private CommonCtrls.GroupRadio L1_CellUnloadComplete_Radio;
        private System.Windows.Forms.TextBox L1_CellTrackOut_CellId_TextBox;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox L1_CellTrackOut_CellNo_TextBox;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox4;
        private CommonCtrls.GroupRadio L2_ProcessStart_Radio;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private CommonCtrls.GroupRadio L2_ProcessStartResponse_Radio;
        private System.Windows.Forms.Label label25;
        private CommonCtrls.GroupRadio L2_ProcessEnd_Radio;
        private System.Windows.Forms.Label label27;
        private CommonCtrls.GroupRadio L2_ProcessEndResponse_Radio;
        private System.Windows.Forms.Label label33;
        private CommonCtrls.GroupRadio L2_RequestRecipe_Radio;
        private System.Windows.Forms.Label label36;
        private CommonCtrls.GroupRadio L2_RequestRecipeResponse_Radio;
        private System.Windows.Forms.GroupBox groupBox8;
        private CommonCtrls.GroupRadio L2_CellUnloadCompleteResponse_Radio;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private CommonCtrls.GroupRadio L2_CellUnloadComplete_Radio;
        private System.Windows.Forms.TextBox L2_CellTrackOut_CellId_TextBox;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox L2_CellTrackOut_CellNo_TextBox;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.GroupBox groupBox11;
        private CommonCtrls.GroupRadio L2_CellLoadCompleteResponse_Radio;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private CommonCtrls.GroupRadio L2_CellLoadComplete_Radio;
        private System.Windows.Forms.TextBox L2_CellTrackIn_CellId_TextBox;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox L2_CellTrackIn_CellNo_TextBox;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox L1_TrackOut_CellCount;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.DataGridView L1_TrackInCellList_dataGridView;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.DataGridView L1_TrackOutCellList_dataGridView;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox L1_Model_TextBox;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox L1_RouteId_TextBox;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox L1_ProcessId_TextBox;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox L1_LotId_TextBox;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox L2_ProcessId_TextBox;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox L2_LotId_TextBox;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox L2_RouteId_TextBox;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox L2_Model_TextBox;
        private System.Windows.Forms.Label label62;
        private CommonCtrls.GroupRadio L2_TrayExist_Radio;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox L2_TrayId_TextBox;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox L2_RecipeId_TextBox;
        private System.Windows.Forms.Label label66;
        private CommonCtrls.GroupRadio L2_TrayLoad_Radio;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private CommonCtrls.GroupRadio L2_TrayLoadResponse_Radio;
        private System.Windows.Forms.DataGridView L2_TrackOutCellList_dataGridView;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.DataGridView L2_TrackInCellList_dataGridView;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox L2_TrackOut_CellCount;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox L2_TrackIn_CellCount;
        private System.Windows.Forms.Label label78;
        private CommonCtrls.GroupRadio Mode_Radio;
    }
}
