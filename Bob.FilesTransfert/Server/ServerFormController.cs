using Bob.FilesTransfert.ComApi.Service;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.Transferts.Box;
using Bob.Transferts.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Transferts.Server
{
    public class ServerFormController
    {
        public ServerForm View;


        public ServerFormController(ServerForm view)
        {
            this.View = view;
            this.SetConnected(false);
            this.View.pathPanel1.changePathButton.Click += this.ChangePathButton_Click;
            this.View.FormClosed += View_FormClosed;
            this.View.configurationPanel1.connectButton.Click += ConnectButton_Click;
            this.View.configurationPanel1.disconnectButton.Click += DisconnectButton_Click;
        }

        private void View_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            var serverService = Provider.GetService<ServerService>();
            Task.Run(() => serverService.Close());
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Close()
        {
            var service = Provider.GetService<ServerService>();
            await Task.Run(() => service.Close());
            this.SetConnected(false);
        }

        public void SetConnected(Boolean connected)
        {
            if (connected)
            {
                this.View.configurationPanel1.connectButton.Enabled = false;
                this.View.configurationPanel1.disconnectButton.Enabled = true;
                this.View.pathPanel1.Visible = true;
            }
            else
            {
                this.View.configurationPanel1.connectButton.Enabled = true;
                this.View.configurationPanel1.disconnectButton.Enabled = false;
                this.View.pathPanel1.Visible = false;
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                var serverIp = SocketHelper.GetIPAddress();
                var serverPort = Convert.ToInt32(this.View.configurationPanel1.textBox2.Text);
                var count = Convert.ToInt32(this.View.configurationPanel1.textBox3.Text);

                var serverService = Provider.GetService<ServerService>();
                serverService.Setup(new System.Net.IPEndPoint(serverIp, serverPort));
                this.SetConnected(true);
            }
            catch (Exception error)
            {
                var form = new ErrorDialogBox(error.Message);
                form.ShowDialog();
                return;
            }
        }

        private BobFileInfo _currentFile;
        private BindingList<BobFileInfo> _files = new BindingList<BobFileInfo>();

        //ugly dont respect the DRY (clientFormController)
        private void ChangePathButton_Click(object sender, EventArgs e)
        {
            var path = this.View.pathPanel1.textBox1.Text;

            if (Directory.Exists(path))
            {
                this._files = new BindingList<BobFileInfo>();
                foreach (var file in Directory.GetFiles(path))
                {
                    var fileInfo = new BobFileInfo()
                    {
                        Path = file,
                        FileName = Path.GetFileName(file),
                        Extension = Path.GetExtension(file)
                    };
                    this._files.Add(fileInfo);
                }

                this.View.dataGridView1.DataSource = this._files;

                var serverService = Provider.GetService<ServerService>();
                serverService.UpdateFolder(path);
            }
        }
    }
}
