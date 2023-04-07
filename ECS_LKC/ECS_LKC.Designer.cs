namespace ECS_LKC
{
    partial class ECS_LKC
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
            this.F1LKC01 = new EQP_INTF.LeakCheck.LeakCheck();
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
            this.tabControl1.Size = new System.Drawing.Size(1777, 983);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.F1LKC01);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1769, 957);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "F1LKC01";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // F1LKC01
            // 
            this.F1LKC01.Endpoint = "opc.tcp://210.91.148.176:48108";
            this.F1LKC01.EQPID = "F1LKC01";
            this.F1LKC01.EQPType = "LKC";
            this.F1LKC01.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.F1LKC01.FmsGroupName = "FMS OPCUA Server";
            this.F1LKC01.FmsID = "fms";
            this.F1LKC01.FmsMesAliveNodeId = "ns=3;i=7094";
            this.F1LKC01.FmsPassword = "fms@!";
            this.F1LKC01.FmsStartingNodeId = "ns=3;i=5118";
            this.F1LKC01.GroupName = "F1LKC01 OPCUA Server";
            this.F1LKC01.ID = "";
            this.F1LKC01.Location = new System.Drawing.Point(0, 0);
            this.F1LKC01.Name = "F1LKC01";
            this.F1LKC01.Password = "";
            this.F1LKC01.Port = 30601;
            this.F1LKC01.Size = new System.Drawing.Size(1780, 954);
            this.F1LKC01.StartingNodeId = "ns=2;i=5004";
            this.F1LKC01.TabIndex = 0;
            this.F1LKC01.UNITID = null;
            // 
            // ECS_LKC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1777, 982);
            this.Controls.Add(this.tabControl1);
            this.Name = "ECS_LKC";
            this.Text = "LeakCheck ECS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ECS_LKC_FormClosing);
            this.Load += new System.EventHandler(this.ECS_LKC_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private EQP_INTF.LeakCheck.LeakCheck F1LKC01;
    }
}

