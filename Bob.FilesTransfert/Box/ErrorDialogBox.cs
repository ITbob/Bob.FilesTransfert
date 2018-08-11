using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bob.Transferts.Box
{
    public partial class ErrorDialogBox : Form
    {
        public ErrorDialogBox(String message)
        {
            InitializeComponent();

            this.label1.Text = message;
            this.button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
