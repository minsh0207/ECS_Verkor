namespace EQP_INTF.HPC
{
    partial class HPC_TrackInOut
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
            this.FMSClient = new OPCUAClient.OPCUAClient();
            this.EQPClient = new OPCUAClient.OPCUAClient();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TrayExist_Radio = new CommonCtrls.GroupRadio();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.TrayId_TextBox = new System.Windows.Forms.TextBox();
            this.ProcessId_TextBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.LotId_TextBox = new System.Windows.Forms.TextBox();
            this.RouteId_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Model_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EmptyTray_Radio = new CommonCtrls.GroupRadio();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TrayLoadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.label9 = new System.Windows.Forms.Label();
            this.TrayLoadComplete_Radio = new CommonCtrls.GroupRadio();
            this.label12 = new System.Windows.Forms.Label();
            this.TrayUnloadCompleteResponse_Radio = new CommonCtrls.GroupRadio();
            this.TrayUnloadResponse_Radio = new CommonCtrls.GroupRadio();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TrayUnloadComplete_Radio = new CommonCtrls.GroupRadio();
            this.TrayUnload_Radio = new CommonCtrls.GroupRadio();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TrayLoadResponse_Radio = new CommonCtrls.GroupRadio();
            this.label17 = new System.Windows.Forms.Label();
            this.TrayLoad_Radio = new CommonCtrls.GroupRadio();
            this.label22 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TrackInCellList = new System.Windows.Forms.DataGridView();
            this.TrackOutCellList = new System.Windows.Forms.DataGridView();
            this.TrackInCellCount_TextBox = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TrackOutCellCount_TextBox = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackInCellList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackOutCellList)).BeginInit();
            this.SuspendLayout();
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
            this.FMSClient.Name = "FMSClient";
            //this.FMSClient.NamespaceIndex = 0;
            this.FMSClient.Password = "fms@!";
            this.FMSClient.Size = new System.Drawing.Size(761, 62);
            this.FMSClient.StartingNodeId = "ns=2;i=22879";
            this.FMSClient.TabIndex = 23;
            this.FMSClient.UNITID = null;
            // 
            // EQPClient
            // 
            this.EQPClient.Endpoint = null;
            this.EQPClient.EQPID = null;
            this.EQPClient.EQPType = null;
            this.EQPClient.GroupName = "EQP OPCUA Server";
            this.EQPClient.ID = null;
            this.EQPClient.Location = new System.Drawing.Point(2, 71);
            this.EQPClient.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EQPClient.Name = "EQPClient";
            //this.EQPClient.NamespaceIndex = 0;
            this.EQPClient.Password = null;
            this.EQPClient.Size = new System.Drawing.Size(761, 61);
            this.EQPClient.StartingNodeId = null;
            this.EQPClient.TabIndex = 22;
            this.EQPClient.UNITID = null;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.Yellow;
            this.label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label46.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(11, 144);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(109, 22);
            this.label46.TabIndex = 26;
            this.label46.Text = "* EQP Write";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.SkyBlue;
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label45.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(126, 144);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(109, 22);
            this.label45.TabIndex = 28;
            this.label45.Text = "* FMS Write";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label47.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(380, 144);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(109, 22);
            this.label47.TabIndex = 29;
            this.label47.Text = "* FMS Response";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.Orchid;
            this.label48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label48.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(265, 144);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(109, 22);
            this.label48.TabIndex = 30;
            this.label48.Text = "* EQP Request";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.EmptyTray_Radio);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.ProcessId_TextBox);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.LotId_TextBox);
            this.groupBox4.Controls.Add(this.RouteId_TextBox);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.Model_TextBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.TrayExist_Radio);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.TrayId_TextBox);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(11, 179);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(761, 142);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tray Information";
            // 
            // TrayExist_Radio
            // 
            this.TrayExist_Radio.BackColor = System.Drawing.Color.White;
            this.TrayExist_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayExist_Radio.ErrorOptionIndex = 98;
            this.TrayExist_Radio.IndexChecked = 0;
            this.TrayExist_Radio.Location = new System.Drawing.Point(91, 28);
            this.TrayExist_Radio.MarginLeft = 16;
            this.TrayExist_Radio.Name = "TrayExist_Radio";
            this.TrayExist_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayExist_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayExist_Radio.NormalOptionIndex = 99;
            this.TrayExist_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayExist_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayExist_Radio.TabIndex = 6;
            this.TrayExist_Radio.Text = "groupRadio1";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Yellow;
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Location = new System.Drawing.Point(10, 28);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 22);
            this.label30.TabIndex = 1;
            this.label30.Text = "TrayExist";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Yellow;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Location = new System.Drawing.Point(10, 52);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 22);
            this.label32.TabIndex = 1;
            this.label32.Text = "TrayId";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayId_TextBox
            // 
            this.TrayId_TextBox.Location = new System.Drawing.Point(90, 52);
            this.TrayId_TextBox.Name = "TrayId_TextBox";
            this.TrayId_TextBox.ReadOnly = true;
            this.TrayId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.TrayId_TextBox.TabIndex = 5;
            // 
            // ProcessId_TextBox
            // 
            this.ProcessId_TextBox.Location = new System.Drawing.Point(91, 102);
            this.ProcessId_TextBox.Name = "ProcessId_TextBox";
            this.ProcessId_TextBox.ReadOnly = true;
            this.ProcessId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.ProcessId_TextBox.TabIndex = 37;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.SkyBlue;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Location = new System.Drawing.Point(10, 102);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 22);
            this.label31.TabIndex = 36;
            this.label31.Text = "ProcessId";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LotId_TextBox
            // 
            this.LotId_TextBox.Location = new System.Drawing.Point(343, 102);
            this.LotId_TextBox.Name = "LotId_TextBox";
            this.LotId_TextBox.ReadOnly = true;
            this.LotId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.LotId_TextBox.TabIndex = 34;
            // 
            // RouteId_TextBox
            // 
            this.RouteId_TextBox.Location = new System.Drawing.Point(343, 77);
            this.RouteId_TextBox.Name = "RouteId_TextBox";
            this.RouteId_TextBox.ReadOnly = true;
            this.RouteId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.RouteId_TextBox.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(263, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 22);
            this.label1.TabIndex = 32;
            this.label1.Text = "LotId";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SkyBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(263, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 22);
            this.label2.TabIndex = 33;
            this.label2.Text = "RouteId";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Model_TextBox
            // 
            this.Model_TextBox.Location = new System.Drawing.Point(91, 77);
            this.Model_TextBox.Name = "Model_TextBox";
            this.Model_TextBox.ReadOnly = true;
            this.Model_TextBox.Size = new System.Drawing.Size(166, 22);
            this.Model_TextBox.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SkyBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(10, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 22);
            this.label3.TabIndex = 30;
            this.label3.Text = "Model";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EmptyTray_Radio
            // 
            this.EmptyTray_Radio.BackColor = System.Drawing.Color.White;
            this.EmptyTray_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.EmptyTray_Radio.ErrorOptionIndex = 98;
            this.EmptyTray_Radio.IndexChecked = 0;
            this.EmptyTray_Radio.Location = new System.Drawing.Point(343, 51);
            this.EmptyTray_Radio.MarginLeft = 16;
            this.EmptyTray_Radio.Name = "EmptyTray_Radio";
            this.EmptyTray_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.EmptyTray_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.EmptyTray_Radio.NormalOptionIndex = 99;
            this.EmptyTray_Radio.Size = new System.Drawing.Size(182, 20);
            this.EmptyTray_Radio.StringOptions = new string[] {
        "Cell Tray",
        "Empty Tray"};
            this.EmptyTray_Radio.TabIndex = 39;
            this.EmptyTray_Radio.Text = "groupRadio1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.SkyBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(262, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 22);
            this.label4.TabIndex = 38;
            this.label4.Text = "Empty Tray";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TrayLoadCompleteResponse_Radio);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TrayLoadComplete_Radio);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.TrayUnloadCompleteResponse_Radio);
            this.groupBox1.Controls.Add(this.TrayUnloadResponse_Radio);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.TrayUnloadComplete_Radio);
            this.groupBox1.Controls.Add(this.TrayUnload_Radio);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.TrayLoadResponse_Radio);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.TrayLoad_Radio);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.groupBox1.Location = new System.Drawing.Point(11, 327);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(761, 190);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EQP-FMS";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.SkyBlue;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(336, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(416, 17);
            this.label7.TabIndex = 63;
            this.label7.Text = "FMS";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Yellow;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(9, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(321, 17);
            this.label8.TabIndex = 62;
            this.label8.Text = "EQP";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayLoadCompleteResponse_Radio
            // 
            this.TrayLoadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.TrayLoadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayLoadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.TrayLoadCompleteResponse_Radio.IndexChecked = 0;
            this.TrayLoadCompleteResponse_Radio.Location = new System.Drawing.Point(506, 80);
            this.TrayLoadCompleteResponse_Radio.MarginLeft = 16;
            this.TrayLoadCompleteResponse_Radio.Name = "TrayLoadCompleteResponse_Radio";
            this.TrayLoadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayLoadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayLoadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.TrayLoadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayLoadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayLoadCompleteResponse_Radio.TabIndex = 61;
            this.TrayLoadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Orchid;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(9, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 22);
            this.label9.TabIndex = 59;
            this.label9.Text = "TrayLoadComplete";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayLoadComplete_Radio
            // 
            this.TrayLoadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.TrayLoadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayLoadComplete_Radio.ErrorOptionIndex = 98;
            this.TrayLoadComplete_Radio.IndexChecked = 0;
            this.TrayLoadComplete_Radio.Location = new System.Drawing.Point(169, 81);
            this.TrayLoadComplete_Radio.MarginLeft = 16;
            this.TrayLoadComplete_Radio.Name = "TrayLoadComplete_Radio";
            this.TrayLoadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayLoadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayLoadComplete_Radio.NormalOptionIndex = 99;
            this.TrayLoadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayLoadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayLoadComplete_Radio.TabIndex = 60;
            this.TrayLoadComplete_Radio.Text = "groupRadio1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(337, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(166, 22);
            this.label12.TabIndex = 58;
            this.label12.Text = "TrayLoadCompleteResponse";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayUnloadCompleteResponse_Radio
            // 
            this.TrayUnloadCompleteResponse_Radio.BackColor = System.Drawing.Color.White;
            this.TrayUnloadCompleteResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayUnloadCompleteResponse_Radio.ErrorOptionIndex = 98;
            this.TrayUnloadCompleteResponse_Radio.IndexChecked = 0;
            this.TrayUnloadCompleteResponse_Radio.Location = new System.Drawing.Point(506, 142);
            this.TrayUnloadCompleteResponse_Radio.MarginLeft = 16;
            this.TrayUnloadCompleteResponse_Radio.Name = "TrayUnloadCompleteResponse_Radio";
            this.TrayUnloadCompleteResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayUnloadCompleteResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayUnloadCompleteResponse_Radio.NormalOptionIndex = 99;
            this.TrayUnloadCompleteResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayUnloadCompleteResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayUnloadCompleteResponse_Radio.TabIndex = 56;
            this.TrayUnloadCompleteResponse_Radio.Text = "groupRadio1";
            // 
            // TrayUnloadResponse_Radio
            // 
            this.TrayUnloadResponse_Radio.BackColor = System.Drawing.Color.White;
            this.TrayUnloadResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayUnloadResponse_Radio.ErrorOptionIndex = 98;
            this.TrayUnloadResponse_Radio.IndexChecked = 0;
            this.TrayUnloadResponse_Radio.Location = new System.Drawing.Point(506, 116);
            this.TrayUnloadResponse_Radio.MarginLeft = 16;
            this.TrayUnloadResponse_Radio.Name = "TrayUnloadResponse_Radio";
            this.TrayUnloadResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayUnloadResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayUnloadResponse_Radio.NormalOptionIndex = 99;
            this.TrayUnloadResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayUnloadResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayUnloadResponse_Radio.TabIndex = 57;
            this.TrayUnloadResponse_Radio.Text = "groupRadio1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Orchid;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(9, 141);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 22);
            this.label13.TabIndex = 53;
            this.label13.Text = "TrayUnloadComplete";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Orchid;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(9, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 22);
            this.label14.TabIndex = 52;
            this.label14.Text = "TrayUnload";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayUnloadComplete_Radio
            // 
            this.TrayUnloadComplete_Radio.BackColor = System.Drawing.Color.White;
            this.TrayUnloadComplete_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayUnloadComplete_Radio.ErrorOptionIndex = 98;
            this.TrayUnloadComplete_Radio.IndexChecked = 0;
            this.TrayUnloadComplete_Radio.Location = new System.Drawing.Point(169, 143);
            this.TrayUnloadComplete_Radio.MarginLeft = 16;
            this.TrayUnloadComplete_Radio.Name = "TrayUnloadComplete_Radio";
            this.TrayUnloadComplete_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayUnloadComplete_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayUnloadComplete_Radio.NormalOptionIndex = 99;
            this.TrayUnloadComplete_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayUnloadComplete_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayUnloadComplete_Radio.TabIndex = 54;
            this.TrayUnloadComplete_Radio.Text = "groupRadio1";
            // 
            // TrayUnload_Radio
            // 
            this.TrayUnload_Radio.BackColor = System.Drawing.Color.White;
            this.TrayUnload_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayUnload_Radio.ErrorOptionIndex = 98;
            this.TrayUnload_Radio.IndexChecked = 0;
            this.TrayUnload_Radio.Location = new System.Drawing.Point(169, 117);
            this.TrayUnload_Radio.MarginLeft = 16;
            this.TrayUnload_Radio.Name = "TrayUnload_Radio";
            this.TrayUnload_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayUnload_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayUnload_Radio.NormalOptionIndex = 99;
            this.TrayUnload_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayUnload_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayUnload_Radio.TabIndex = 55;
            this.TrayUnload_Radio.Text = "groupRadio1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(337, 142);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(166, 22);
            this.label15.TabIndex = 48;
            this.label15.Text = "TrayUnloadCompleteResponse";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(337, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 22);
            this.label16.TabIndex = 47;
            this.label16.Text = "TrayUnloadResponse";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayLoadResponse_Radio
            // 
            this.TrayLoadResponse_Radio.BackColor = System.Drawing.Color.White;
            this.TrayLoadResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayLoadResponse_Radio.ErrorOptionIndex = 98;
            this.TrayLoadResponse_Radio.IndexChecked = 0;
            this.TrayLoadResponse_Radio.Location = new System.Drawing.Point(506, 55);
            this.TrayLoadResponse_Radio.MarginLeft = 16;
            this.TrayLoadResponse_Radio.Name = "TrayLoadResponse_Radio";
            this.TrayLoadResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayLoadResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayLoadResponse_Radio.NormalOptionIndex = 99;
            this.TrayLoadResponse_Radio.Size = new System.Drawing.Size(246, 20);
            this.TrayLoadResponse_Radio.StringOptions = new string[] {
        "Clear",
        "OK",
        "Bypass"};
            this.TrayLoadResponse_Radio.TabIndex = 50;
            this.TrayLoadResponse_Radio.Text = "groupRadio1";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(336, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(166, 22);
            this.label17.TabIndex = 46;
            this.label17.Text = "TrayLoadResponse";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayLoad_Radio
            // 
            this.TrayLoad_Radio.BackColor = System.Drawing.Color.White;
            this.TrayLoad_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.TrayLoad_Radio.ErrorOptionIndex = 98;
            this.TrayLoad_Radio.IndexChecked = 0;
            this.TrayLoad_Radio.Location = new System.Drawing.Point(168, 55);
            this.TrayLoad_Radio.MarginLeft = 16;
            this.TrayLoad_Radio.Name = "TrayLoad_Radio";
            this.TrayLoad_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.TrayLoad_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.TrayLoad_Radio.NormalOptionIndex = 99;
            this.TrayLoad_Radio.Size = new System.Drawing.Size(162, 20);
            this.TrayLoad_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.TrayLoad_Radio.TabIndex = 51;
            this.TrayLoad_Radio.Text = "groupRadio1";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Orchid;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Location = new System.Drawing.Point(9, 53);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(157, 22);
            this.label22.TabIndex = 49;
            this.label22.Text = "TrayLoad";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.SkyBlue;
            this.label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label33.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label33.Location = new System.Drawing.Point(20, 520);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(360, 22);
            this.label33.TabIndex = 33;
            this.label33.Text = "TrackIn Cell Information";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.label5.Location = new System.Drawing.Point(403, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(360, 22);
            this.label5.TabIndex = 33;
            this.label5.Text = "TrackOut Cell Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrackInCellList
            // 
            this.TrackInCellList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TrackInCellList.Location = new System.Drawing.Point(20, 575);
            this.TrackInCellList.Name = "TrackInCellList";
            this.TrackInCellList.RowTemplate.Height = 23;
            this.TrackInCellList.Size = new System.Drawing.Size(360, 232);
            this.TrackInCellList.TabIndex = 34;
            // 
            // TrackOutCellList
            // 
            this.TrackOutCellList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TrackOutCellList.Location = new System.Drawing.Point(403, 575);
            this.TrackOutCellList.Name = "TrackOutCellList";
            this.TrackOutCellList.RowTemplate.Height = 23;
            this.TrackOutCellList.Size = new System.Drawing.Size(360, 232);
            this.TrackOutCellList.TabIndex = 34;
            // 
            // TrackInCellCount_TextBox
            // 
            this.TrackInCellCount_TextBox.Location = new System.Drawing.Point(101, 547);
            this.TrackInCellCount_TextBox.Name = "TrackInCellCount_TextBox";
            this.TrackInCellCount_TextBox.ReadOnly = true;
            this.TrackInCellCount_TextBox.Size = new System.Drawing.Size(166, 21);
            this.TrackInCellCount_TextBox.TabIndex = 37;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.SkyBlue;
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.Location = new System.Drawing.Point(21, 547);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(77, 22);
            this.label36.TabIndex = 36;
            this.label36.Text = "CellCount";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Yellow;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(403, 548);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 22);
            this.label6.TabIndex = 36;
            this.label6.Text = "CellCount";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrackOutCellCount_TextBox
            // 
            this.TrackOutCellCount_TextBox.Location = new System.Drawing.Point(483, 548);
            this.TrackOutCellCount_TextBox.Name = "TrackOutCellCount_TextBox";
            this.TrackOutCellCount_TextBox.ReadOnly = true;
            this.TrackOutCellCount_TextBox.Size = new System.Drawing.Size(166, 21);
            this.TrackOutCellCount_TextBox.TabIndex = 37;
            // 
            // HPC_TrackInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TrackOutCellCount_TextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TrackInCellCount_TextBox);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.TrackOutCellList);
            this.Controls.Add(this.TrackInCellList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.FMSClient);
            this.Controls.Add(this.EQPClient);
            this.Name = "HPC_TrackInOut";
            this.Size = new System.Drawing.Size(785, 820);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrackInCellList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackOutCellList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OPCUAClient.OPCUAClient FMSClient;
        private OPCUAClient.OPCUAClient EQPClient;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.GroupBox groupBox4;
        private CommonCtrls.GroupRadio TrayExist_Radio;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox TrayId_TextBox;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox ProcessId_TextBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox LotId_TextBox;
        private System.Windows.Forms.TextBox RouteId_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Model_TextBox;
        private System.Windows.Forms.Label label3;
        private CommonCtrls.GroupRadio EmptyTray_Radio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private CommonCtrls.GroupRadio TrayLoadCompleteResponse_Radio;
        private System.Windows.Forms.Label label9;
        private CommonCtrls.GroupRadio TrayLoadComplete_Radio;
        private System.Windows.Forms.Label label12;
        private CommonCtrls.GroupRadio TrayUnloadCompleteResponse_Radio;
        private CommonCtrls.GroupRadio TrayUnloadResponse_Radio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private CommonCtrls.GroupRadio TrayUnloadComplete_Radio;
        private CommonCtrls.GroupRadio TrayUnload_Radio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private CommonCtrls.GroupRadio TrayLoadResponse_Radio;
        private System.Windows.Forms.Label label17;
        private CommonCtrls.GroupRadio TrayLoad_Radio;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView TrackInCellList;
        private System.Windows.Forms.DataGridView TrackOutCellList;
        private System.Windows.Forms.TextBox TrackInCellCount_TextBox;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TrackOutCellCount_TextBox;
    }
}
