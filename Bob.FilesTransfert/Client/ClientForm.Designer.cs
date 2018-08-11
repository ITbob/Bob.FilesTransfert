namespace Bob.Transferts.Client
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.clientMainPanel1 = new Bob.Transferts.Client.ClientMainPanel();
            this.connectionPanel1 = new Bob.Transferts.Client.SubPanel.ConnectionPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.clientMainPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.connectionPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // clientMainPanel1
            // 
            this.clientMainPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientMainPanel1.Location = new System.Drawing.Point(3, 42);
            this.clientMainPanel1.Name = "clientMainPanel1";
            this.clientMainPanel1.Size = new System.Drawing.Size(794, 405);
            this.clientMainPanel1.TabIndex = 0;
            // 
            // connectionPanel1
            // 
            this.connectionPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionPanel1.Location = new System.Drawing.Point(3, 3);
            this.connectionPanel1.Name = "connectionPanel1";
            this.connectionPanel1.Size = new System.Drawing.Size(794, 33);
            this.connectionPanel1.TabIndex = 1;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public ClientMainPanel clientMainPanel1;
        public SubPanel.ConnectionPanel connectionPanel1;
    }
}