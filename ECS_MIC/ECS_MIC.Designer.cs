namespace ECS_MIC
{
    partial class ECS_MIC
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
            this.MICTrackInOutControl = new EQP_INTF.MicroCurrent.MIC_TrackInOut();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.F1MIC01Control = new EQP_INTF.MicroCurrent.MicroCurrent();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MICTrackInOutControl
            // 
            this.MICTrackInOutControl.Endpoint = "opc.tcp://210.91.148.176:48105";
            this.MICTrackInOutControl.EQPID = "F1MIC01";
            this.MICTrackInOutControl.EQPType = "MIC";
            this.MICTrackInOutControl.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.MICTrackInOutControl.FmsGroupName = "FMS OPCUA Server";
            this.MICTrackInOutControl.FmsID = "fms";
            this.MICTrackInOutControl.FmsMesAliveNodeId = "ns=3;i=7094";
            this.MICTrackInOutControl.FmsMesResponseTimeOut = 10;
            this.MICTrackInOutControl.FmsPassword = "fms@!";
            this.MICTrackInOutControl.FmsStartingNodeId = "ns=3;i=11461";
            this.MICTrackInOutControl.GroupName = "MIC #1 Input/Output Station";
            this.MICTrackInOutControl.ID = "";
            this.MICTrackInOutControl.Location = new System.Drawing.Point(-1, 38);
            this.MICTrackInOutControl.Name = "MICTrackInOutControl";
            this.MICTrackInOutControl.Password = "";
            this.MICTrackInOutControl.Size = new System.Drawing.Size(785, 820);
            this.MICTrackInOutControl.StartingNodeId = "ns=2;i=5007";
            this.MICTrackInOutControl.TabIndex = 0;
            this.MICTrackInOutControl.UNITID = "MIC0110101";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(793, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1099, 842);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.F1MIC01Control);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1091, 816);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "F1MIC01";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // F1MIC01Control
            // 
            this.F1MIC01Control.Endpoint = "opc.tcp://210.91.148.176:48105";
            this.F1MIC01Control.EQPID = "F1MIC01";
            this.F1MIC01Control.EQPType = "MIC";
            this.F1MIC01Control.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.F1MIC01Control.FmsGroupName = "FMS OPCUA Server";
            this.F1MIC01Control.FmsID = "fms";
            this.F1MIC01Control.FmsMesAliveNodeId = "ns=3;i=7094";
            this.F1MIC01Control.FmsMesResponseTimeOut = 10;
            this.F1MIC01Control.FmsPassword = "fms@!";
            this.F1MIC01Control.FmsStartingNodeId = "ns=3;i=14565";
            this.F1MIC01Control.GroupName = "F1MIC01 OPCUA Server";
            this.F1MIC01Control.ID = "";
            this.F1MIC01Control.Location = new System.Drawing.Point(0, 0);
            this.F1MIC01Control.Name = "F1MIC01Control";
            this.F1MIC01Control.Password = "";
            this.F1MIC01Control.Port = 30501;
            this.F1MIC01Control.Size = new System.Drawing.Size(1090, 820);
            this.F1MIC01Control.StartingNodeId = "ns=2;i=5073";
            this.F1MIC01Control.TabIndex = 0;
            this.F1MIC01Control.UNITID = "MIC0110101";
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
            this.label11.TabIndex = 14;
            this.label11.Text = "(Micro-Current) Track In-Out Location";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ECS_MIC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1895, 857);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MICTrackInOutControl);
            this.Name = "ECS_MIC";
            this.Text = "MicroCurrent #1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQP_INTF.MicroCurrent.MIC_TrackInOut MICTrackInOutControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private EQP_INTF.MicroCurrent.MicroCurrent F1MIC01Control;
        private System.Windows.Forms.Label label11;
    }
}

