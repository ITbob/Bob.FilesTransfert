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
using Bob.FilesTransfert.ComApi.Sender.Directory;

namespace Bob.FilesTransfert.ComApi.Service
{
    public class ClientHandler
    {
        private TcpSender _sender;
        private FileSender _fileSender;
        private DirectoryResquestSender _directoryRequester;


        public void Setup(IPEndPoint clientinfo, IPEndPoint serverinfo)
        {

            this._sender = new TcpSender(clientinfo, serverinfo);
            this._fileSender = new FileSender(this._sender);
            this._directoryRequester = new DirectoryResquestSender(this._sender);
            this._sender.Connect();

        }

        public void Close()
        {
            try
            {
                this._sender.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public List<String> GetServerDirectory()
        {
            return this._directoryRequester.Send();
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
