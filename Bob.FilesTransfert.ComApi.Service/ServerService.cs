using Bob.FilesTransfert.ComApi.Handler;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfert.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Service
{
    public class ServerService : IService
    {
        public IEnumerable<IService> Dependencies { get; set; }

        private TcpPacketsReceiver _listener;
        private PacketDispatcher _dispatcher;
        private static readonly Int32 FolderPort = 7766;
        private FolderResquetHandler _folderRequest;
        private FileRequestHandler _fileRequest;
        public EventHandler<FileResult> ReceivedFile;

        public void OnReceivedFile(Object obj, FileResult fileResult)
        {
            var path = Encoding.ASCII.GetString(fileResult.FilenameBytes.ToArray());
            var filename = Path.GetFileName(path);
            var directory = this._folderRequest.GetDirectory();
            var destination = $@"{directory}\{filename}";
            File.WriteAllBytes(destination, fileResult.FileBytes.ToArray());
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Setup(IPEndPoint info)
        {
            this._listener = new TcpPacketsReceiver(info, 2);
            this._fileRequest = new FileRequestHandler();
            this._fileRequest.ReceivedFile += OnReceivedFile;

            this._folderRequest = new FolderResquetHandler(
                new IPEndPoint(SocketHelper.GetIPAddress(), FolderPort));

            this._dispatcher = new PacketDispatcher(this._listener,
                new List<IPacketHandler>
                {
                    this._folderRequest,
                    this._fileRequest,
                });
            this._listener.Listen();
        }

        public void Close()
        {
            try
            {
                this._fileRequest.ReceivedFile -= OnReceivedFile;
                this._listener.Close();
                this._folderRequest.Close();
            }
            catch (Exception e)
            {

            }
        }

        public void UpdateFolder(String directory)
        {
            this._folderRequest.SetDirectory(directory);
        }
    }
}
