using Bob.FilesTransfert.ComApi.Service;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.Transferts.Box;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bob.Transferts.Client
{
    public class ClientFormController
    {
        private ClientForm View { get; set; }
        private ClientHandler Handler { get; set; }
        public ClientFormController(ClientForm  view, Int32 count)
        {
            this.View = view;
            this.Handler = new ClientHandler();

            this.View.connectionPanel1.connectionButton.Click += Connect;
            this.View.connectionPanel1.disconnectionButton.Click += Button2_Click;
            this.View.clientMainPanel1.clientContentPanel1.sendFilePanel1.sendButton.Click += SnedButton_Click;
            this.View.clientMainPanel1.clientContentPanel1.dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            this.View.clientMainPanel1.clientContentPanel1.pathPanel1.changePathButton.Click += ChangePathButton_Click;
            this.View.clientMainPanel1.visualServerPanel1.refreshButton.Click += RefreshButton_Click;
            this.View.FormClosed += View_FormClosed;

            this.View.connectionPanel1.portTextBox.Text 
                = $"{Convert.ToInt32(this.View.connectionPanel1.portTextBox.Text)+count}";
            this.View.connectionPanel1.receiveClientPortTextBox.Text 
                = $"{Convert.ToInt32(this.View.connectionPanel1.receiveClientPortTextBox.Text)+count}";

            this.SetConnected(false);
        }

        public void SetConnected(Boolean connected)
        {
            if (connected)
            {
                this.View.clientMainPanel1.visualServerPanel1.Visible = true;
                this.View.connectionPanel1.connectionButton.Enabled = false;
                this.View.connectionPanel1.disconnectionButton.Enabled = true;
            }
            else
            {
                this.View.clientMainPanel1.visualServerPanel1.Visible = false;
                this.View.connectionPanel1.connectionButton.Enabled = true;
                this.View.connectionPanel1.disconnectionButton.Enabled = false;
            }
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            Task.Run(() => this.Handler.Close());
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            this.Handler.AskFolderInfo();
        }

        private void SnedButton_Click(object sender, EventArgs e)
        {
            this.Handler.SendFile(this._currentFile.Path);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this._files.Any())
            {
                var index = this.View.clientMainPanel1.clientContentPanel1.dataGridView1.CurrentCell.RowIndex;
                var file = this._files[index];
                this._currentFile = file;
                this.View.clientMainPanel1.
                    clientContentPanel1.sendFilePanel1.fileLabel.Text = file.FileName;
            }
        }

        private BobFileInfo _currentFile;
        private BindingList<BobFileInfo> _files = new BindingList<BobFileInfo>();

        //ugly dont respect the DRY (clientFormController)
        private void ChangePathButton_Click(object sender, EventArgs e)
        {
            var path = this.View.clientMainPanel1.clientContentPanel1.pathPanel1.textBox1.Text;

            if (Directory.Exists(path))
            {
                this._files = new BindingList<BobFileInfo>();
                foreach (var file in Directory.GetFiles(path))
                {
                    var fileInfo = new BobFileInfo() {
                        Path = file,
                        FileName = Path.GetFileName(file),
                        Extension = Path.GetExtension(file)
                    };
                    this._files.Add(fileInfo);
                }

                this.View.clientMainPanel1.clientContentPanel1.dataGridView1.DataSource = this._files;
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Close()
        {
            await Task.Run(() => this.Handler.Close());
            this.SetConnected(false);
        }

        private void Connect(object sender, EventArgs e)
        {
            try
            {
                var clientIp = SocketHelper.GetIPAddress();
                var clientPort = Convert.ToInt32(this.View.connectionPanel1.portTextBox.Text);
                var folderclientPort = Convert.ToInt32(this.View.connectionPanel1.receiveClientPortTextBox.Text);

                var serverIp = SocketHelper.GetIPAddress();
                var serverPort = Convert.ToInt32(this.View.connectionPanel1.serverPortTextbox.Text);


                this.Handler.Setup(
                    new IPEndPoint(clientIp, clientPort),
                    new IPEndPoint(serverIp, serverPort),
                    folderclientPort);

                this.Handler.ReceivedFiles += this.OnReceivedServerFiles;
                this.SetConnected(true);
            }
            catch (Exception excep)
            {
                var form = new ErrorDialogBox(excep.Message);
                form.ShowDialog();
                return;
            }
        }

        public void OnReceivedServerFiles(Object obj, List<String> filenames)
        {
            var files = new BindingList<FileItem>();
            foreach (var filename in filenames)
            {
                files.Add(new FileItem()
                {
                    Filename = filename
                });
            }
            if (this.View.InvokeRequired)
            {
                Action act = () =>
                {
                    this.View.clientMainPanel1.visualServerPanel1.dataGridView1.DataSource = files;
                };
                this.View.Invoke(act);
            }
            else
            {
                this.View.clientMainPanel1.visualServerPanel1.dataGridView1.DataSource = files;
            }
        }

        public class FileItem
        {
            public String Filename { get; set; }
        }
    }
}
