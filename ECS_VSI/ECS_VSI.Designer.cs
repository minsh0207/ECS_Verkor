namespace ECS_VSI
{
    partial class ECS_VSI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.F1VSI01 = new EQP_INTF.VisionInspection.VisionInspection();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1740, 935);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.F1VSI01);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1732, 909);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "F1VSI01";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // F1VSI01
            // 
            this.F1VSI01.Endpoint = "opc.tcp://210.91.148.176:48107";
            this.F1VSI01.EQPID = "F1VSI01";
            this.F1VSI01.EQPType = "VSI";
            this.F1VSI01.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.F1VSI01.FmsGroupName = "FMS OPCUA Server";
            this.F1VSI01.FmsID = "fms";
            this.F1VSI01.FmsMesAliveNodeId = "ns=3;i=7094";
            this.F1VSI01.FmsMesResponseTimeOut = 300;
            this.F1VSI01.FmsPassword = "fms@!";
            this.F1VSI01.FmsStartingNodeId = "ns=3;i=12747";
            this.F1VSI01.GroupName = "F1VSI01 OPCUA Server";
            this.F1VSI01.ID = "";
            this.F1VSI01.Location = new System.Drawing.Point(0, 0);
            this.F1VSI01.Name = "F1VSI01";
            this.F1VSI01.Password = "";
            this.F1VSI01.Port = 30701;
            this.F1VSI01.Size = new System.Drawing.Size(1732, 905);
            this.F1VSI01.StartingNodeId = "ns=2;i=5655";
            this.F1VSI01.TabIndex = 0;
            this.F1VSI01.UNITID = null;
            // 
            // ECS_VSI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1742, 933);
            this.Controls.Add(this.tabControl1);
            this.Name = "ECS_VSI";
            this.Text = "Vision Inspection ECS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ECS_VSI_FormClosing);
            this.Load += new System.EventHandler(this.ECS_VSI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private EQP_INTF.VisionInspection.VisionInspection F1VSI01;
    }
}

