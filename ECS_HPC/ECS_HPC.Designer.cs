namespace ECS_HPC
{
    partial class ECS_HPC
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
            this.HPCTab = new System.Windows.Forms.TabControl();
            this.HPC0110101 = new System.Windows.Forms.TabPage();
            this.HPC0110101Control = new EQP_INTF.HPC.HPC();
            this.HPC0110102 = new System.Windows.Forms.TabPage();
            this.HPC0110102Control = new EQP_INTF.HPC.HPC();
            this.label11 = new System.Windows.Forms.Label();
            this.HPCTrackInOutControl = new EQP_INTF.HPC.HPC_TrackInOut();
            this.HPCTab.SuspendLayout();
            this.HPC0110101.SuspendLayout();
            this.HPC0110102.SuspendLayout();
            this.SuspendLayout();
            // 
            // HPCTab
            // 
            this.HPCTab.Controls.Add(this.HPC0110101);
            this.HPCTab.Controls.Add(this.HPC0110102);
            this.HPCTab.Location = new System.Drawing.Point(793, 0);
            this.HPCTab.Name = "HPCTab";
            this.HPCTab.SelectedIndex = 0;
            this.HPCTab.Size = new System.Drawing.Size(1099, 842);
            this.HPCTab.TabIndex = 1;
            // 
            // HPC0110101
            // 
            this.HPC0110101.Controls.Add(this.HPC0110101Control);
            this.HPC0110101.Location = new System.Drawing.Point(4, 22);
            this.HPC0110101.Name = "HPC0110101";
            this.HPC0110101.Padding = new System.Windows.Forms.Padding(3);
            this.HPC0110101.Size = new System.Drawing.Size(1091, 816);
            this.HPC0110101.TabIndex = 0;
            this.HPC0110101.Text = "HPC0110101";
            this.HPC0110101.UseVisualStyleBackColor = true;
            // 
            // HPC0110101Control
            // 
            this.HPC0110101Control.Endpoint = "opc.tcp://210.91.148.176:48102";
            this.HPC0110101Control.EQPID = "F1HPC01";
            this.HPC0110101Control.EQPType = "HPC";
            this.HPC0110101Control.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.HPC0110101Control.FmsGroupName = "FMS OPCUA Server";
            this.HPC0110101Control.FmsID = "fms";
            this.HPC0110101Control.FmsMesAliveNodeId = "ns=3;i=7094";
            this.HPC0110101Control.FmsMesResponseTimeOut = 600;
            this.HPC0110101Control.FmsPassword = "fms@!";
            this.HPC0110101Control.FmsStartingNodeId = "ns=3;i=17512";
            this.HPC0110101Control.GroupName = "HPC JIG #1";
            this.HPC0110101Control.ID = "";
            this.HPC0110101Control.Location = new System.Drawing.Point(0, 0);
            this.HPC0110101Control.Name = "HPC0110101Control";
            this.HPC0110101Control.Password = "";
            this.HPC0110101Control.Port = 30201;
            this.HPC0110101Control.Size = new System.Drawing.Size(1090, 820);
            this.HPC0110101Control.StartingNodeId = "ns=2;i=5003";
            this.HPC0110101Control.TabIndex = 0;
            this.HPC0110101Control.UNITID = "HPC0110101";
            // 
            // HPC0110102
            // 
            this.HPC0110102.Controls.Add(this.HPC0110102Control);
            this.HPC0110102.Location = new System.Drawing.Point(4, 22);
            this.HPC0110102.Name = "HPC0110102";
            this.HPC0110102.Padding = new System.Windows.Forms.Padding(3);
            this.HPC0110102.Size = new System.Drawing.Size(1091, 816);
            this.HPC0110102.TabIndex = 1;
            this.HPC0110102.Text = "HPC0110102";
            this.HPC0110102.UseVisualStyleBackColor = true;
            // 
            // HPC0110102Control
            // 
            this.HPC0110102Control.Endpoint = "opc.tcp://210.91.148.176:48102";
            this.HPC0110102Control.EQPID = "F1HPC01";
            this.HPC0110102Control.EQPType = "HPC";
            this.HPC0110102Control.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.HPC0110102Control.FmsGroupName = "FMS OPCUA Server";
            this.HPC0110102Control.FmsID = "fms";
            this.HPC0110102Control.FmsMesAliveNodeId = "ns=3;i=7094";
            this.HPC0110102Control.FmsMesResponseTimeOut = 0;
            this.HPC0110102Control.FmsPassword = "fms@!";
            this.HPC0110102Control.FmsStartingNodeId = "ns=3;i=25463";
            this.HPC0110102Control.GroupName = "HPC JIG #2";
            this.HPC0110102Control.ID = "fms";
            this.HPC0110102Control.Location = new System.Drawing.Point(0, 0);
            this.HPC0110102Control.Name = "HPC0110102Control";
            this.HPC0110102Control.Password = "fms@!";
            this.HPC0110102Control.Port = 30202;
            this.HPC0110102Control.Size = new System.Drawing.Size(1090, 820);
            this.HPC0110102Control.StartingNodeId = "ns=2;i=5226";
            this.HPC0110102Control.TabIndex = 0;
            this.HPC0110102Control.UNITID = "HPC0110102";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Silver;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Segoe UI Emoji", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(791, 39);
            this.label11.TabIndex = 13;
            this.label11.Text = "(HPC) Track In-Out Location";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HPCTrackInOutControl
            // 
            this.HPCTrackInOutControl.Endpoint = "opc.tcp://210.91.148.176:48102";
            this.HPCTrackInOutControl.EQPID = "F1HPC01";
            this.HPCTrackInOutControl.EQPType = "HPC";
            this.HPCTrackInOutControl.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.HPCTrackInOutControl.FmsGroupName = "FMS OPCUA Server";
            this.HPCTrackInOutControl.FmsID = "fms";
            this.HPCTrackInOutControl.FmsMesAliveNodeId = "ns=3;i=7094";
            this.HPCTrackInOutControl.FmsMesResponseTimeOut = 600;
            this.HPCTrackInOutControl.FmsPassword = "fms@!";
            this.HPCTrackInOutControl.FmsStartingNodeId = "ns=3;i=5004";
            this.HPCTrackInOutControl.GroupName = "HPC #1 Input/Output Station";
            this.HPCTrackInOutControl.ID = "";
            this.HPCTrackInOutControl.Location = new System.Drawing.Point(2, 42);
            this.HPCTrackInOutControl.Name = "HPCTrackInOutControl";
            this.HPCTrackInOutControl.Password = "";
            this.HPCTrackInOutControl.Size = new System.Drawing.Size(785, 820);
            this.HPCTrackInOutControl.StartingNodeId = "ns=2;i=5004";
            this.HPCTrackInOutControl.TabIndex = 0;
            this.HPCTrackInOutControl.UNITID = null;
            // 
            // ECS_HPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1895, 857);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.HPCTab);
            this.Controls.Add(this.HPCTrackInOutControl);
            this.Name = "ECS_HPC";
            this.Text = "HPC #1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ECS_HPC_FormClosing);
            this.Load += new System.EventHandler(this.ECS_HPC_Load);
            this.HPCTab.ResumeLayout(false);
            this.HPC0110101.ResumeLayout(false);
            this.HPC0110102.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQP_INTF.HPC.HPC_TrackInOut HPCTrackInOutControl;
        private System.Windows.Forms.TabControl HPCTab;
        private System.Windows.Forms.TabPage HPC0110101;
        private System.Windows.Forms.TabPage HPC0110102;
        private EQP_INTF.HPC.HPC HPC0110101Control;
        private System.Windows.Forms.Label label11;
        private EQP_INTF.HPC.HPC HPC0110102Control;
    }
}

