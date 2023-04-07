namespace TrayBinder
{
    partial class TrayBinder
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TrayID_textBox = new System.Windows.Forms.TextBox();
            this.Model_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TrayZone_comboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LotId_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.NextProcess_comboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LotID_textBox = new System.Windows.Forms.TextBox();
            this.Route_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateEmptyTray_checkbox = new System.Windows.Forms.CheckBox();
            this.Create_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Cell_dataGridView = new System.Windows.Forms.DataGridView();
            this.Reset_button = new System.Windows.Forms.Button();
            this.CreateCellId_button = new System.Windows.Forms.Button();
            this.CreateCell_checkbox = new System.Windows.Forms.CheckBox();
            this.SetTrayEmpty_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.normal_checkbox = new System.Windows.Forms.CheckBox();
            this.Search_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cell_dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayID_textBox
            // 
            this.TrayID_textBox.Location = new System.Drawing.Point(472, 29);
            this.TrayID_textBox.Name = "TrayID_textBox";
            this.TrayID_textBox.Size = new System.Drawing.Size(180, 21);
            this.TrayID_textBox.TabIndex = 0;
            this.TrayID_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TrayID_textBox_KeyDown);
            // 
            // Model_comboBox
            // 
            this.Model_comboBox.FormattingEnabled = true;
            this.Model_comboBox.Location = new System.Drawing.Point(113, 27);
            this.Model_comboBox.Name = "Model_comboBox";
            this.Model_comboBox.Size = new System.Drawing.Size(121, 20);
            this.Model_comboBox.TabIndex = 1;
            this.Model_comboBox.SelectedIndexChanged += new System.EventHandler(this.Model_comboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TrayZone_comboBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.LotId_button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.NextProcess_comboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LotID_textBox);
            this.groupBox1.Controls.Add(this.Route_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Model_comboBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tray Information";
            // 
            // TrayZone_comboBox
            // 
            this.TrayZone_comboBox.FormattingEnabled = true;
            this.TrayZone_comboBox.Items.AddRange(new object[] {
            "BD",
            "AD"});
            this.TrayZone_comboBox.Location = new System.Drawing.Point(113, 132);
            this.TrayZone_comboBox.Name = "TrayZone_comboBox";
            this.TrayZone_comboBox.Size = new System.Drawing.Size(71, 20);
            this.TrayZone_comboBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tray Zone";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LotId_button
            // 
            this.LotId_button.Location = new System.Drawing.Point(191, 106);
            this.LotId_button.Name = "LotId_button";
            this.LotId_button.Size = new System.Drawing.Size(118, 23);
            this.LotId_button.TabIndex = 7;
            this.LotId_button.Text = "Apply to All Cells";
            this.LotId_button.UseVisualStyleBackColor = true;
            this.LotId_button.Click += new System.EventHandler(this.LotId_button_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Next Process";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NextProcess_comboBox
            // 
            this.NextProcess_comboBox.FormattingEnabled = true;
            this.NextProcess_comboBox.Location = new System.Drawing.Point(113, 79);
            this.NextProcess_comboBox.Name = "NextProcess_comboBox";
            this.NextProcess_comboBox.Size = new System.Drawing.Size(196, 20);
            this.NextProcess_comboBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Lot ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Route ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LotID_textBox
            // 
            this.LotID_textBox.Location = new System.Drawing.Point(113, 105);
            this.LotID_textBox.Name = "LotID_textBox";
            this.LotID_textBox.Size = new System.Drawing.Size(71, 21);
            this.LotID_textBox.TabIndex = 0;
            // 
            // Route_comboBox
            // 
            this.Route_comboBox.FormattingEnabled = true;
            this.Route_comboBox.Location = new System.Drawing.Point(113, 53);
            this.Route_comboBox.Name = "Route_comboBox";
            this.Route_comboBox.Size = new System.Drawing.Size(121, 20);
            this.Route_comboBox.TabIndex = 3;
            this.Route_comboBox.SelectedIndexChanged += new System.EventHandler(this.Route_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Model ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CreateEmptyTray_checkbox
            // 
            this.CreateEmptyTray_checkbox.AutoSize = true;
            this.CreateEmptyTray_checkbox.Location = new System.Drawing.Point(6, 52);
            this.CreateEmptyTray_checkbox.Name = "CreateEmptyTray_checkbox";
            this.CreateEmptyTray_checkbox.Size = new System.Drawing.Size(131, 16);
            this.CreateEmptyTray_checkbox.TabIndex = 3;
            this.CreateEmptyTray_checkbox.Text = "Create Empty Tray";
            this.CreateEmptyTray_checkbox.UseVisualStyleBackColor = true;
            this.CreateEmptyTray_checkbox.CheckedChanged += new System.EventHandler(this.CreateEmptyTray_checkbox_CheckedChanged);
            // 
            // Create_button
            // 
            this.Create_button.Location = new System.Drawing.Point(562, 66);
            this.Create_button.Name = "Create_button";
            this.Create_button.Size = new System.Drawing.Size(222, 96);
            this.Create_button.TabIndex = 4;
            this.Create_button.Text = "Run";
            this.Create_button.UseVisualStyleBackColor = true;
            this.Create_button.Click += new System.EventHandler(this.Create_button_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(410, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tray ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(28, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "Cell Information";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cell_dataGridView
            // 
            this.Cell_dataGridView.AllowUserToAddRows = false;
            this.Cell_dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Cell_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Cell_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Cell_dataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.Cell_dataGridView.Location = new System.Drawing.Point(16, 222);
            this.Cell_dataGridView.Name = "Cell_dataGridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cell_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Cell_dataGridView.RowTemplate.Height = 23;
            this.Cell_dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Cell_dataGridView.Size = new System.Drawing.Size(768, 716);
            this.Cell_dataGridView.TabIndex = 6;
            // 
            // Reset_button
            // 
            this.Reset_button.Location = new System.Drawing.Point(267, 193);
            this.Reset_button.Name = "Reset_button";
            this.Reset_button.Size = new System.Drawing.Size(75, 23);
            this.Reset_button.TabIndex = 4;
            this.Reset_button.Text = "Reset";
            this.Reset_button.UseVisualStyleBackColor = true;
            this.Reset_button.Click += new System.EventHandler(this.Reset_button_Click);
            // 
            // CreateCellId_button
            // 
            this.CreateCellId_button.Location = new System.Drawing.Point(135, 193);
            this.CreateCellId_button.Name = "CreateCellId_button";
            this.CreateCellId_button.Size = new System.Drawing.Size(126, 23);
            this.CreateCellId_button.TabIndex = 7;
            this.CreateCellId_button.Text = "Create Cell ID";
            this.CreateCellId_button.UseVisualStyleBackColor = true;
            this.CreateCellId_button.Click += new System.EventHandler(this.CreateCellId_button_Click);
            // 
            // CreateCell_checkbox
            // 
            this.CreateCell_checkbox.AutoSize = true;
            this.CreateCell_checkbox.Checked = true;
            this.CreateCell_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateCell_checkbox.Location = new System.Drawing.Point(412, 193);
            this.CreateCell_checkbox.Name = "CreateCell_checkbox";
            this.CreateCell_checkbox.Size = new System.Drawing.Size(201, 16);
            this.CreateCell_checkbox.TabIndex = 3;
            this.CreateCell_checkbox.Text = "Create Cell Information on FMS";
            this.CreateCell_checkbox.UseVisualStyleBackColor = true;
            // 
            // SetTrayEmpty_checkbox
            // 
            this.SetTrayEmpty_checkbox.AutoSize = true;
            this.SetTrayEmpty_checkbox.Location = new System.Drawing.Point(6, 78);
            this.SetTrayEmpty_checkbox.Name = "SetTrayEmpty_checkbox";
            this.SetTrayEmpty_checkbox.Size = new System.Drawing.Size(112, 16);
            this.SetTrayEmpty_checkbox.TabIndex = 3;
            this.SetTrayEmpty_checkbox.Text = "Set Tray Empty";
            this.SetTrayEmpty_checkbox.UseVisualStyleBackColor = true;
            this.SetTrayEmpty_checkbox.CheckedChanged += new System.EventHandler(this.SetTrayEmpty_checkbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.normal_checkbox);
            this.groupBox2.Controls.Add(this.CreateEmptyTray_checkbox);
            this.groupBox2.Controls.Add(this.SetTrayEmpty_checkbox);
            this.groupBox2.Location = new System.Drawing.Point(412, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 109);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operation";
            // 
            // normal_checkbox
            // 
            this.normal_checkbox.AutoSize = true;
            this.normal_checkbox.Checked = true;
            this.normal_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normal_checkbox.Location = new System.Drawing.Point(6, 25);
            this.normal_checkbox.Name = "normal_checkbox";
            this.normal_checkbox.Size = new System.Drawing.Size(111, 16);
            this.normal_checkbox.TabIndex = 3;
            this.normal_checkbox.Text = "Normal Binding";
            this.normal_checkbox.UseVisualStyleBackColor = true;
            this.normal_checkbox.CheckedChanged += new System.EventHandler(this.normal_checkbox_CheckedChanged);
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(677, 30);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(90, 20);
            this.Search_button.TabIndex = 4;
            this.Search_button.Text = "Check Tray";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // TrayBinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 947);
            this.Controls.Add(this.CreateCellId_button);
            this.Controls.Add(this.Cell_dataGridView);
            this.Controls.Add(this.Reset_button);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.Create_button);
            this.Controls.Add(this.CreateCell_checkbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TrayID_textBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "TrayBinder";
            this.Text = "TrayBinder for Verkor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cell_dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TrayID_textBox;
        private System.Windows.Forms.ComboBox Model_comboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox NextProcess_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Route_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CreateEmptyTray_checkbox;
        private System.Windows.Forms.Button Create_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LotID_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView Cell_dataGridView;
        private System.Windows.Forms.Button Reset_button;
        private System.Windows.Forms.Button CreateCellId_button;
        private System.Windows.Forms.Button LotId_button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox TrayZone_comboBox;
        private System.Windows.Forms.CheckBox CreateCell_checkbox;
        private System.Windows.Forms.CheckBox SetTrayEmpty_checkbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox normal_checkbox;
        private System.Windows.Forms.Button Search_button;
    }
}

