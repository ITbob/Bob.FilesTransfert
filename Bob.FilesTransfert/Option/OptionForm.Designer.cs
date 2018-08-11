namespace Bob.Transferts.Option
{
    partial class OptionForm
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
            this.clientButton = new System.Windows.Forms.Button();
            this.serverButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 318F));
            this.tableLayoutPanel1.Controls.Add(this.clientButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.serverButton, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 53);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.clientButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientButton.Location = new System.Drawing.Point(3, 3);
            this.clientButton.Name = "button1";
            this.clientButton.Size = new System.Drawing.Size(342, 47);
            this.clientButton.TabIndex = 0;
            this.clientButton.Text = "Client";
            this.clientButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.serverButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverButton.Location = new System.Drawing.Point(485, 3);
            this.serverButton.Name = "button2";
            this.serverButton.Size = new System.Drawing.Size(312, 47);
            this.serverButton.TabIndex = 1;
            this.serverButton.Text = "Server";
            this.serverButton.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 53);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OptionForm";
            this.Text = "OptionForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button clientButton;
        public System.Windows.Forms.Button serverButton;
    }
}