namespace RestAPIServer
{
    partial class RestApiServer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbEndPoint = new System.Windows.Forms.Label();
            this.btStartStop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbEndPoint);
            this.groupBox1.Controls.Add(this.btStartStop);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rest API Server";
            // 
            // lbEndPoint
            // 
            this.lbEndPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEndPoint.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndPoint.Location = new System.Drawing.Point(7, 19);
            this.lbEndPoint.Name = "lbEndPoint";
            this.lbEndPoint.Size = new System.Drawing.Size(282, 23);
            this.lbEndPoint.TabIndex = 5;
            this.lbEndPoint.Text = "Endpoint : ";
            this.lbEndPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btStartStop
            // 
            this.btStartStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btStartStop.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStartStop.Location = new System.Drawing.Point(6, 55);
            this.btStartStop.Name = "btStartStop";
            this.btStartStop.Size = new System.Drawing.Size(282, 25);
            this.btStartStop.TabIndex = 4;
            this.btStartStop.Text = "Start";
            this.btStartStop.UseVisualStyleBackColor = true;
            this.btStartStop.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // RestApiServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RestApiServer";
            this.Size = new System.Drawing.Size(306, 99);
            this.Load += new System.EventHandler(this.RestApiServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbEndPoint;
        private System.Windows.Forms.Button btStartStop;
    }
}
