namespace OPCUAClient
{
    partial class OPCUAClient
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
            this.gbClient = new System.Windows.Forms.GroupBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbEndPoint = new System.Windows.Forms.Label();
            this.btStartStop = new System.Windows.Forms.Button();
            this.gbClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbClient
            // 
            this.gbClient.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gbClient.Controls.Add(this.btBrowse);
            this.gbClient.Controls.Add(this.lbStatus);
            this.gbClient.Controls.Add(this.lbEndPoint);
            this.gbClient.Controls.Add(this.btStartStop);
            this.gbClient.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbClient.Location = new System.Drawing.Point(7, 3);
            this.gbClient.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbClient.Name = "gbClient";
            this.gbClient.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbClient.Size = new System.Drawing.Size(739, 62);
            this.gbClient.TabIndex = 3;
            this.gbClient.TabStop = false;
            this.gbClient.Text = "OPCUA Server";
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(680, 19);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(50, 30);
            this.btBrowse.TabIndex = 4;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbStatus.Location = new System.Drawing.Point(510, 19);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(165, 28);
            this.lbStatus.TabIndex = 7;
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbEndPoint
            // 
            this.lbEndPoint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbEndPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEndPoint.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndPoint.Location = new System.Drawing.Point(6, 21);
            this.lbEndPoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEndPoint.Name = "lbEndPoint";
            this.lbEndPoint.Size = new System.Drawing.Size(360, 23);
            this.lbEndPoint.TabIndex = 5;
            this.lbEndPoint.Text = "Endpoint : ";
            this.lbEndPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btStartStop
            // 
            this.btStartStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btStartStop.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStartStop.Location = new System.Drawing.Point(370, 21);
            this.btStartStop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btStartStop.Name = "btStartStop";
            this.btStartStop.Size = new System.Drawing.Size(134, 25);
            this.btStartStop.TabIndex = 4;
            this.btStartStop.Text = "Connect";
            this.btStartStop.UseVisualStyleBackColor = true;
            this.btStartStop.Click += new System.EventHandler(this.btStartStop_Click);
            // 
            // OPCUAClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gbClient);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "OPCUAClient";
            this.Size = new System.Drawing.Size(754, 69);
            this.Load += new System.EventHandler(this.OPCUAClient_Load);
            this.gbClient.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbClient;
        private System.Windows.Forms.Label lbEndPoint;
        private System.Windows.Forms.Button btStartStop;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btBrowse;
    }
}
