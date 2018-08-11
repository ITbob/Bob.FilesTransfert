using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bob.Transferts.Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            var data = new BindingList<Item>() {
                new Item(){
                    IsFile = true,
                    Name = "Hello.psd"
                },
                new Item(){
                    IsFile = true,
                    Name = "Super.txt"
                }
            };
            //this.dataGridView1.DataSource = data;
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //this.dataGridView1.ReadOnly = true;
        }

        public class Item
        {
            public Boolean IsFile { get; set; }
            public String Name { get; set; }
        }
    }
}
