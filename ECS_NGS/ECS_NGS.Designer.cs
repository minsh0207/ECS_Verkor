namespace ECS_NGSorter
{
    partial class ECS_NGS
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.F1NGS01 = new EQP_INTF.NGSorter.NGSorter();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.F1NGS01);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1643, 955);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NG Sorter #1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1651, 981);
            this.tabControl1.TabIndex = 1;
            // 
            // F1NGS01
            // 
            this.F1NGS01.Endpoint = "opc.tcp://210.91.148.176:48109";
            this.F1NGS01.EQPID = "F1NGS01";
            this.F1NGS01.EQPType = "NGS";
            this.F1NGS01.FmsEndpoint = "opc.tcp://210.91.148.176:48040";
            this.F1NGS01.FmsGroupName = "FMS OPCUA Server";
            this.F1NGS01.FmsID = "fms";
            this.F1NGS01.FmsMesAliveNodeId = "ns=3;i=7094";
            this.F1NGS01.FmsMesResponseTimeOut = 5;
            this.F1NGS01.FmsPassword = "fms@!";
            this.F1NGS01.FmsStartingNodeId = "ns=3;i=15854";
            this.F1NGS01.GroupName = "F1NGS01 OPCUA Server";
            this.F1NGS01.ID = "";
            this.F1NGS01.Location = new System.Drawing.Point(0, 0);
            this.F1NGS01.Name = "F1NGS01";
            this.F1NGS01.Password = "";
            this.F1NGS01.Port = 30901;
            this.F1NGS01.Size = new System.Drawing.Size(1647, 964);
            this.F1NGS01.StartingNodeId = "ns=2;i=5004";
            this.F1NGS01.TabIndex = 0;
            this.F1NGS01.UNITID = null;
            // 
            // ECS_NGS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 992);
            this.Controls.Add(this.tabControl1);
            this.Name = "ECS_NGS";
            this.Text = "[ECS] NG Sorter";
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private EQP_INTF.NGSorter.NGSorter F1NGS01;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

