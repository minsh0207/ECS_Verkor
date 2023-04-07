namespace EQP_INTF.HPC
{
    partial class HPC
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
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Mode_Radio = new CommonCtrls.GroupRadio();
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Common_Alive_Radio = new CommonCtrls.GroupRadio();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FMS_ErrorNo_TextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.FMS_Status_Radio = new CommonCtrls.GroupRadio();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Command_TextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.CommandResponse_Radio = new CommonCtrls.GroupRadio();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.ProcessId_TextBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.LotId_TextBox = new System.Windows.Forms.TextBox();
            this.RouteId_TextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.Model_TextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.OperationMode_TextBox = new System.Windows.Forms.TextBox();
            this.RecipeId_TextBox = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.CellCount_TextBox = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.JigName_Label = new System.Windows.Forms.Label();
            this.ProcessEndResponse_Radio = new CommonCtrls.GroupRadio();
            this.ProcessStartResponse_Radio = new CommonCtrls.GroupRadio();
            this.RequestRecipeResponse_Radio = new CommonCtrls.GroupRadio();
            this.label21 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.ProcessEnd_Radio = new CommonCtrls.GroupRadio();
            this.label26 = new System.Windows.Forms.Label();
            this.ProcessStart_Radio = new CommonCtrls.GroupRadio();
            this.label20 = new System.Windows.Forms.Label();
            this.RequestRecipe_Radio = new CommonCtrls.GroupRadio();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.TrayId_TextBox = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.RestApiServer = new RestAPIServer.RestApiServer();
            this.FMSClient = new OPCUAClient.OPCUAClient();
            this.EQPClient = new OPCUAClient.OPCUAClient();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.SkyBlue;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(552, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(520, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "FMS";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Yellow;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(18, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(520, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "EQP";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.Yellow;
            this.label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label46.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(632, 166);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(109, 22);
            this.label46.TabIndex = 27;
            this.label46.Text = "* EQP Write";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.YellowGreen;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(860, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 22);
            this.label8.TabIndex = 28;
            this.label8.Text = "* FMS/EQP Write";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.SkyBlue;
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label45.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(747, 166);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(109, 22);
            this.label45.TabIndex = 29;
            this.label45.Text = "* FMS Write";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label47.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(747, 196);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(109, 22);
            this.label47.TabIndex = 30;
            this.label47.Text = "* Response";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.Orchid;
            this.label48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label48.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(632, 196);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(109, 22);
            this.label48.TabIndex = 31;
            this.label48.Text = "* Request";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Mode_Radio);
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
            this.groupBox1.Location = new System.Drawing.Point(18, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 166);
            this.groupBox1.TabIndex = 32;
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
            // Mode_Radio
            // 
            this.Mode_Radio.BackColor = System.Drawing.Color.White;
            this.Mode_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.Mode_Radio.ErrorOptionIndex = 98;
            this.Mode_Radio.IndexChecked = 1;
            this.Mode_Radio.Location = new System.Drawing.Point(89, 46);
            this.Mode_Radio.MarginLeft = 16;
            this.Mode_Radio.Name = "Mode_Radio";
            this.Mode_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.Mode_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.Mode_Radio.NormalOptionIndex = 99;
            this.Mode_Radio.Size = new System.Drawing.Size(225, 20);
            this.Mode_Radio.StringOptions = new string[] {
        "Control",
        "Manual"};
            this.Mode_Radio.TabIndex = 6;
            this.Mode_Radio.Text = "groupRadio1";
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Common_Alive_Radio);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(18, 155);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(520, 68);
            this.groupBox5.TabIndex = 34;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FMS_ErrorNo_TextBox);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.FMS_Status_Radio);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(554, 319);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 76);
            this.groupBox2.TabIndex = 35;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Command_TextBox);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.CommandResponse_Radio);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(19, 404);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1052, 54);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EqupimentControl";
            // 
            // Command_TextBox
            // 
            this.Command_TextBox.Location = new System.Drawing.Point(700, 18);
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
            this.label17.Location = new System.Drawing.Point(538, 18);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.ProcessId_TextBox);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.LotId_TextBox);
            this.groupBox4.Controls.Add(this.RouteId_TextBox);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.Model_TextBox);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.OperationMode_TextBox);
            this.groupBox4.Controls.Add(this.RecipeId_TextBox);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.CellCount_TextBox);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.JigName_Label);
            this.groupBox4.Controls.Add(this.ProcessEndResponse_Radio);
            this.groupBox4.Controls.Add(this.ProcessStartResponse_Radio);
            this.groupBox4.Controls.Add(this.RequestRecipeResponse_Radio);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.ProcessEnd_Radio);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.ProcessStart_Radio);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.RequestRecipe_Radio);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.TrayId_TextBox);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(18, 464);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1052, 341);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "JIG Process";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(14, 121);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(175, 15);
            this.label34.TabIndex = 30;
            this.label34.Text = "* TrayId from TrackInOutLocation";
            // 
            // ProcessId_TextBox
            // 
            this.ProcessId_TextBox.Location = new System.Drawing.Point(91, 96);
            this.ProcessId_TextBox.Name = "ProcessId_TextBox";
            this.ProcessId_TextBox.ReadOnly = true;
            this.ProcessId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.ProcessId_TextBox.TabIndex = 29;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.Yellow;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Location = new System.Drawing.Point(10, 96);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 22);
            this.label31.TabIndex = 28;
            this.label31.Text = "ProcessId";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LotId_TextBox
            // 
            this.LotId_TextBox.Location = new System.Drawing.Point(343, 96);
            this.LotId_TextBox.Name = "LotId_TextBox";
            this.LotId_TextBox.ReadOnly = true;
            this.LotId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.LotId_TextBox.TabIndex = 27;
            // 
            // RouteId_TextBox
            // 
            this.RouteId_TextBox.Location = new System.Drawing.Point(343, 71);
            this.RouteId_TextBox.Name = "RouteId_TextBox";
            this.RouteId_TextBox.ReadOnly = true;
            this.RouteId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.RouteId_TextBox.TabIndex = 27;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Yellow;
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Location = new System.Drawing.Point(263, 96);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 22);
            this.label30.TabIndex = 26;
            this.label30.Text = "LotId";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Yellow;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Location = new System.Drawing.Point(263, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 22);
            this.label19.TabIndex = 26;
            this.label19.Text = "RouteId";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Model_TextBox
            // 
            this.Model_TextBox.Location = new System.Drawing.Point(91, 71);
            this.Model_TextBox.Name = "Model_TextBox";
            this.Model_TextBox.ReadOnly = true;
            this.Model_TextBox.Size = new System.Drawing.Size(166, 22);
            this.Model_TextBox.TabIndex = 25;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Yellow;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Location = new System.Drawing.Point(10, 71);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 22);
            this.label18.TabIndex = 24;
            this.label18.Text = "Model";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.Location = new System.Drawing.Point(352, 229);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(157, 107);
            this.label40.TabIndex = 23;
            this.label40.Text = "[OperationMode]\r\n1: OCV\r\n2: Charge (CC)\r\n4: Charge (CCCV)\r\n8: Discharge (CC)\r\n16:" +
    " Discharge (CCCV)";
            // 
            // OperationMode_TextBox
            // 
            this.OperationMode_TextBox.Location = new System.Drawing.Point(398, 190);
            this.OperationMode_TextBox.Name = "OperationMode_TextBox";
            this.OperationMode_TextBox.ReadOnly = true;
            this.OperationMode_TextBox.Size = new System.Drawing.Size(111, 22);
            this.OperationMode_TextBox.TabIndex = 22;
            // 
            // RecipeId_TextBox
            // 
            this.RecipeId_TextBox.Location = new System.Drawing.Point(90, 190);
            this.RecipeId_TextBox.Name = "RecipeId_TextBox";
            this.RecipeId_TextBox.ReadOnly = true;
            this.RecipeId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.RecipeId_TextBox.TabIndex = 22;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.SkyBlue;
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.Location = new System.Drawing.Point(263, 190);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(132, 22);
            this.label39.TabIndex = 21;
            this.label39.Text = "OperationMode";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CellCount_TextBox
            // 
            this.CellCount_TextBox.Location = new System.Drawing.Point(343, 46);
            this.CellCount_TextBox.Name = "CellCount_TextBox";
            this.CellCount_TextBox.ReadOnly = true;
            this.CellCount_TextBox.Size = new System.Drawing.Size(166, 22);
            this.CellCount_TextBox.TabIndex = 22;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.SkyBlue;
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.Location = new System.Drawing.Point(10, 190);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 22);
            this.label38.TabIndex = 21;
            this.label38.Text = "RecipeId";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.Yellow;
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.Location = new System.Drawing.Point(262, 46);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(77, 22);
            this.label36.TabIndex = 21;
            this.label36.Text = "CellCount";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JigName_Label
            // 
            this.JigName_Label.BackColor = System.Drawing.Color.Silver;
            this.JigName_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JigName_Label.Location = new System.Drawing.Point(10, 18);
            this.JigName_Label.Name = "JigName_Label";
            this.JigName_Label.Size = new System.Drawing.Size(499, 22);
            this.JigName_Label.TabIndex = 15;
            this.JigName_Label.Text = "JIG";
            this.JigName_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessEndResponse_Radio
            // 
            this.ProcessEndResponse_Radio.BackColor = System.Drawing.Color.White;
            this.ProcessEndResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.ProcessEndResponse_Radio.ErrorOptionIndex = 98;
            this.ProcessEndResponse_Radio.IndexChecked = 0;
            this.ProcessEndResponse_Radio.Location = new System.Drawing.Point(699, 256);
            this.ProcessEndResponse_Radio.MarginLeft = 16;
            this.ProcessEndResponse_Radio.Name = "ProcessEndResponse_Radio";
            this.ProcessEndResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.ProcessEndResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.ProcessEndResponse_Radio.NormalOptionIndex = 99;
            this.ProcessEndResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.ProcessEndResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.ProcessEndResponse_Radio.TabIndex = 12;
            this.ProcessEndResponse_Radio.Text = "groupRadio1";
            // 
            // ProcessStartResponse_Radio
            // 
            this.ProcessStartResponse_Radio.BackColor = System.Drawing.Color.White;
            this.ProcessStartResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.ProcessStartResponse_Radio.ErrorOptionIndex = 98;
            this.ProcessStartResponse_Radio.IndexChecked = 0;
            this.ProcessStartResponse_Radio.Location = new System.Drawing.Point(699, 230);
            this.ProcessStartResponse_Radio.MarginLeft = 16;
            this.ProcessStartResponse_Radio.Name = "ProcessStartResponse_Radio";
            this.ProcessStartResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.ProcessStartResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.ProcessStartResponse_Radio.NormalOptionIndex = 99;
            this.ProcessStartResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.ProcessStartResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.ProcessStartResponse_Radio.TabIndex = 12;
            this.ProcessStartResponse_Radio.Text = "groupRadio1";
            // 
            // RequestRecipeResponse_Radio
            // 
            this.RequestRecipeResponse_Radio.BackColor = System.Drawing.Color.White;
            this.RequestRecipeResponse_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.RequestRecipeResponse_Radio.ErrorOptionIndex = 98;
            this.RequestRecipeResponse_Radio.IndexChecked = 0;
            this.RequestRecipeResponse_Radio.Location = new System.Drawing.Point(699, 162);
            this.RequestRecipeResponse_Radio.MarginLeft = 16;
            this.RequestRecipeResponse_Radio.Name = "RequestRecipeResponse_Radio";
            this.RequestRecipeResponse_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.RequestRecipeResponse_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.RequestRecipeResponse_Radio.NormalOptionIndex = 99;
            this.RequestRecipeResponse_Radio.Size = new System.Drawing.Size(162, 20);
            this.RequestRecipeResponse_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.RequestRecipeResponse_Radio.TabIndex = 13;
            this.RequestRecipeResponse_Radio.Text = "groupRadio1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Orchid;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Location = new System.Drawing.Point(10, 255);
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
            this.label29.Location = new System.Drawing.Point(10, 229);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(157, 22);
            this.label29.TabIndex = 7;
            this.label29.Text = "ProcessStart";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessEnd_Radio
            // 
            this.ProcessEnd_Radio.BackColor = System.Drawing.Color.White;
            this.ProcessEnd_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.ProcessEnd_Radio.ErrorOptionIndex = 98;
            this.ProcessEnd_Radio.IndexChecked = 0;
            this.ProcessEnd_Radio.Location = new System.Drawing.Point(170, 257);
            this.ProcessEnd_Radio.MarginLeft = 16;
            this.ProcessEnd_Radio.Name = "ProcessEnd_Radio";
            this.ProcessEnd_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.ProcessEnd_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.ProcessEnd_Radio.NormalOptionIndex = 99;
            this.ProcessEnd_Radio.Size = new System.Drawing.Size(162, 20);
            this.ProcessEnd_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.ProcessEnd_Radio.TabIndex = 8;
            this.ProcessEnd_Radio.Text = "groupRadio1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Orchid;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Location = new System.Drawing.Point(10, 161);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(157, 22);
            this.label26.TabIndex = 7;
            this.label26.Text = "RequestRecipe";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessStart_Radio
            // 
            this.ProcessStart_Radio.BackColor = System.Drawing.Color.White;
            this.ProcessStart_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.ProcessStart_Radio.ErrorOptionIndex = 98;
            this.ProcessStart_Radio.IndexChecked = 0;
            this.ProcessStart_Radio.Location = new System.Drawing.Point(170, 231);
            this.ProcessStart_Radio.MarginLeft = 16;
            this.ProcessStart_Radio.Name = "ProcessStart_Radio";
            this.ProcessStart_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.ProcessStart_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.ProcessStart_Radio.NormalOptionIndex = 99;
            this.ProcessStart_Radio.Size = new System.Drawing.Size(162, 20);
            this.ProcessStart_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.ProcessStart_Radio.TabIndex = 8;
            this.ProcessStart_Radio.Text = "groupRadio1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Location = new System.Drawing.Point(539, 256);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(157, 22);
            this.label20.TabIndex = 0;
            this.label20.Text = "ProcessEndResponse";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RequestRecipe_Radio
            // 
            this.RequestRecipe_Radio.BackColor = System.Drawing.Color.White;
            this.RequestRecipe_Radio.ErrorOptionColor = System.Drawing.Color.Red;
            this.RequestRecipe_Radio.ErrorOptionIndex = 98;
            this.RequestRecipe_Radio.IndexChecked = 0;
            this.RequestRecipe_Radio.Location = new System.Drawing.Point(170, 163);
            this.RequestRecipe_Radio.MarginLeft = 16;
            this.RequestRecipe_Radio.Name = "RequestRecipe_Radio";
            this.RequestRecipe_Radio.NeutralOptionColor = System.Drawing.Color.Lime;
            this.RequestRecipe_Radio.NormalOptionColor = System.Drawing.Color.LightBlue;
            this.RequestRecipe_Radio.NormalOptionIndex = 99;
            this.RequestRecipe_Radio.Size = new System.Drawing.Size(162, 20);
            this.RequestRecipe_Radio.StringOptions = new string[] {
        "OFF",
        "ON"};
            this.RequestRecipe_Radio.TabIndex = 8;
            this.RequestRecipe_Radio.Text = "groupRadio1";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.Location = new System.Drawing.Point(539, 230);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(157, 22);
            this.label28.TabIndex = 0;
            this.label28.Text = "ProcessStartResponse";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Location = new System.Drawing.Point(539, 162);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(157, 22);
            this.label27.TabIndex = 0;
            this.label27.Text = "RequestRecipeResponse";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrayId_TextBox
            // 
            this.TrayId_TextBox.Location = new System.Drawing.Point(90, 46);
            this.TrayId_TextBox.Name = "TrayId_TextBox";
            this.TrayId_TextBox.ReadOnly = true;
            this.TrayId_TextBox.Size = new System.Drawing.Size(166, 22);
            this.TrayId_TextBox.TabIndex = 5;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Yellow;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Location = new System.Drawing.Point(10, 45);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 22);
            this.label32.TabIndex = 1;
            this.label32.Text = "* TrayId";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(541, 135);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(5, 670);
            this.label12.TabIndex = 38;
            // 
            // RestApiServer
            // 
            this.RestApiServer.Location = new System.Drawing.Point(756, 15);
            this.RestApiServer.Name = "RestApiServer";
            this.RestApiServer.Port = 0;
            this.RestApiServer.Size = new System.Drawing.Size(309, 112);
            this.RestApiServer.TabIndex = 24;
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
            this.FMSClient.TabIndex = 23;
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
            this.EQPClient.TabIndex = 22;
            this.EQPClient.UNITID = null;
            // 
            // HPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.RestApiServer);
            this.Controls.Add(this.FMSClient);
            this.Controls.Add(this.EQPClient);
            this.Name = "HPC";
            this.Size = new System.Drawing.Size(1090, 820);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RestAPIServer.RestApiServer RestApiServer;
        private OPCUAClient.OPCUAClient FMSClient;
        private OPCUAClient.OPCUAClient EQPClient;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private CommonCtrls.GroupRadio Mode_Radio;
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
        private System.Windows.Forms.GroupBox groupBox5;
        private CommonCtrls.GroupRadio Common_Alive_Radio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox FMS_ErrorNo_TextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private CommonCtrls.GroupRadio FMS_Status_Radio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Command_TextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private CommonCtrls.GroupRadio CommandResponse_Radio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox OperationMode_TextBox;
        private System.Windows.Forms.TextBox RecipeId_TextBox;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox CellCount_TextBox;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label JigName_Label;
        private CommonCtrls.GroupRadio ProcessEndResponse_Radio;
        private CommonCtrls.GroupRadio ProcessStartResponse_Radio;
        private CommonCtrls.GroupRadio RequestRecipeResponse_Radio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label29;
        private CommonCtrls.GroupRadio ProcessEnd_Radio;
        private System.Windows.Forms.Label label26;
        private CommonCtrls.GroupRadio ProcessStart_Radio;
        private System.Windows.Forms.Label label20;
        private CommonCtrls.GroupRadio RequestRecipe_Radio;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox TrayId_TextBox;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox RouteId_TextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox Model_TextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox ProcessId_TextBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox LotId_TextBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label12;
    }
}
