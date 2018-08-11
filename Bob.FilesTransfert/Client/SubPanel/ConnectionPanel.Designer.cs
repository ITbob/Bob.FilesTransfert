namespace Bob.Transferts.Client.SubPanel
{
    partial class ConnectionPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.connectionButton = new System.Windows.Forms.Button();
            this.disconnectionButton = new System.Windows.Forms.Button();
            this.serverIpTextbox = new System.Windows.Forms.TextBox();
            this.serverPortTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.receiveClientPortTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ipTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.portTextBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.connectionButton, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.disconnectionButton, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.serverIpTextbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverPortTextbox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.receiveClientPortTextBox, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(723, 81);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client ip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ipTextBox
            // 
            this.ipTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ipTextBox.Location = new System.Drawing.Point(77, 6);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(100, 25);
            this.ipTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(194, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Client port (S/R)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portTextBox.Location = new System.Drawing.Point(318, 6);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 25);
            this.portTextBox.TabIndex = 3;
            this.portTextBox.Text = "4000";
            // 
            // connectionButton
            // 
            this.connectionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionButton.Location = new System.Drawing.Point(526, 3);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(82, 32);
            this.connectionButton.TabIndex = 4;
            this.connectionButton.Text = "Connect";
            this.connectionButton.UseVisualStyleBackColor = true;
            // 
            // disconnectionButton
            // 
            this.disconnectionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disconnectionButton.Location = new System.Drawing.Point(614, 3);
            this.disconnectionButton.Name = "disconnectionButton";
            this.disconnectionButton.Size = new System.Drawing.Size(106, 32);
            this.disconnectionButton.TabIndex = 5;
            this.disconnectionButton.Text = "Disconnect";
            this.disconnectionButton.UseVisualStyleBackColor = true;
            // 
            // serverIpTextbox
            // 
            this.serverIpTextbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverIpTextbox.Location = new System.Drawing.Point(77, 47);
            this.serverIpTextbox.Name = "serverIpTextbox";
            this.serverIpTextbox.Size = new System.Drawing.Size(100, 25);
            this.serverIpTextbox.TabIndex = 6;
            // 
            // serverPortTextbox
            // 
            this.serverPortTextbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverPortTextbox.Location = new System.Drawing.Point(318, 47);
            this.serverPortTextbox.Name = "serverPortTextbox";
            this.serverPortTextbox.Size = new System.Drawing.Size(100, 25);
            this.serverPortTextbox.TabIndex = 7;
            this.serverPortTextbox.Text = "8585";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 43);
            this.label3.TabIndex = 8;
            this.label3.Text = "Server ip";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(194, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 43);
            this.label4.TabIndex = 9;
            this.label4.Text = "Server port (R)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // receiveClientPortTextBox
            // 
            this.receiveClientPortTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.receiveClientPortTextBox.Location = new System.Drawing.Point(427, 6);
            this.receiveClientPortTextBox.Name = "receiveClientPortTextBox";
            this.receiveClientPortTextBox.Size = new System.Drawing.Size(93, 25);
            this.receiveClientPortTextBox.TabIndex = 10;
            this.receiveClientPortTextBox.Text = "5000";
            this.receiveClientPortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConnectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConnectionPanel";
            this.Size = new System.Drawing.Size(723, 81);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox ipTextBox;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox portTextBox;
        public System.Windows.Forms.Button connectionButton;
        public System.Windows.Forms.Button disconnectionButton;
        public System.Windows.Forms.TextBox serverIpTextbox;
        public System.Windows.Forms.TextBox serverPortTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox receiveClientPortTextBox;
    }
}
