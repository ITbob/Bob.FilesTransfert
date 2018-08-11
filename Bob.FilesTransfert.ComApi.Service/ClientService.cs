using Bob.FilesTransfert.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.FilesTransfert.ComApi;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Bob.FilesTransfert.ComApi.Handler;
using System.Diagnostics;

namespace Bob.FilesTransfert.ComApi.Service
{
    public class ClientHandler
    {
        private Int32 folderResquestPort = 7660;
        private TcpPacketsReceiver _listener;
        private PacketDispatcher _dispatcher;
        private FolderAckHandler _askFolder;
        private TcpSender _sender;
        private FileSender _fileSender;
        private FolderInfoSender _folderInfo;

        public EventHandler<List<String>> ReceivedFiles;
        private void OnReceivedFiles(object o, List<String> files)
        {
            this.ReceivedFiles?.Invoke(this, files);
        }

        public void Setup(IPEndPoint clientinfo, IPEndPoint serverinfo, Int32 folderRequestPort)
        {
            this.folderResquestPort = folderRequestPort;

            this._sender = new TcpSender(clientinfo, serverinfo);
            this._fileSender = new FileSender(this._sender);
            this._folderInfo = new FolderInfoSender(this._sender);
            this._listener = new TcpPacketsReceiver(new IPEndPoint(SocketHelper.GetIPAddress(),folderResquestPort), 2);
            this._listener.Listen();
            this._sender.Connect();

            this._askFolder = new FolderAckHandler();
            this._askFolder.ReceivedFiles = OnReceivedFiles;
            this._dispatcher = new PacketDispatcher(this._listener,
                new List<IPacketHandler>() { this._askFolder });
        }

        public void Close()
        {
            try
            {
                this._sender.Close();
                this._listener.Close();
                this._askFolder.ReceivedFiles -= OnReceivedFiles;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void AskFolderInfo()
        {
            this._folderInfo.Send(new IPEndPoint(SocketHelper.GetIPAddress(), folderResquestPort));
        }

        public void SendFile(String path)
        {
            if (File.Exists(path))
            {
                this._fileSender.Send(path);
            }
        }
    }
}
