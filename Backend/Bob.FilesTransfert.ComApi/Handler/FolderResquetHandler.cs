using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;

namespace Bob.FilesTransfert.ComApi.Handler
{
    public class FolderResquetHandler : IPacketHandler
    {
        private IPEndPoint _sender;
        private Dictionary<IPEndPoint, TcpSender> _senders = new Dictionary<IPEndPoint, TcpSender>();
        private String _directory = String.Empty;
        private List<Byte> _portAndIp = new List<byte>();

        public void Close()
        {
            foreach (var sender in _senders)
            {
                sender.Value.Close();
            }
        }

        public FolderResquetHandler(IPEndPoint sender)
        {
            this._sender = sender;
        }

        public void SetDirectory(String directory)
        {
            this._directory = directory;
        }

        public String GetDirectory()
        {
            return this._directory;
        }
        
        private IPEndPoint SetIpEndpoint()
        {
            Debug.WriteLine("[SetIpEndpoint]");


            var ipPort = String.Empty;
            lock (this._syncObject)
            {
                ipPort = Encoding.ASCII.GetString(this._portAndIp.ToArray());
                this._portAndIp.Clear();
            }

            var info = ipPort.Split(':');

            var bytes = new List<Byte>();
            foreach (var item in info[0].Split('.'))
            {
                bytes.Add(Convert.ToByte(Int32.Parse(item)));
            }

            var port = Convert.ToInt32(info[1]);

            var receiver = new IPEndPoint(new IPAddress(bytes.ToArray()), port);

            return receiver;
        }


        private void Reply(IPEndPoint receiver)
        {
            //if (!this._senders.ContainsKey(receiver))
            //{
            //    var tcpSender = new TcpSender(this._sender, receiver);
            //    this._senders.Add(receiver, tcpSender);
            //    tcpSender.Connect();
            //}

            //var currentTcpSender = this._senders[receiver];

            Debug.WriteLine("[Reply]");

            var currentTcpSender = new TcpSender(this._sender, receiver);
            currentTcpSender.Connect();

            foreach (var file in Directory.GetFiles(this._directory))
            {
                var packets = PacketHelper.Frame(HeaderContainer.FolderFile, Encoding.ASCII.GetBytes(Path.GetFileName(file)));
                foreach (var pa in packets)
                {
                    currentTcpSender.Send(pa);
                }
                //file end
                currentTcpSender.Send(PacketHelper.Frame(HeaderContainer.FolderFileEnd, new List<Byte> { 0x00 }).First());
            }
            currentTcpSender.Send(PacketHelper.Frame(HeaderContainer.FolderFileFinalEnd, new List<Byte> { 0x00 }).First());

            currentTcpSender.Close();
        }

        private readonly Object _syncObject = new object();

        public bool Handle(Byte[] packet)
        {
            if (HeaderContainer.RequestFolder.IsFormat(packet))
            {
                lock (_syncObject)
                {
                    this._portAndIp.AddRange(PacketHelper.Content(HeaderContainer.RequestFolder, packet));
                }
                return true;
            }
            else if (HeaderContainer.RequestFolderEnd.IsFormat(packet))
            {
                var receiver = this.SetIpEndpoint();
                this.Reply(receiver);
                return true;
            }

            return false;
        }

    }
}


//var sender = new TcpSender(
//    new SocketCommunicantInfo()
//    {
//        Ip = this._sender.Address,
//        Port = this._sender.Port
//    },
//    new SocketCommunicantInfo()
//    {
//        Ip = packet.Sender.Address,
//        Port = packet.Sender.Port
//    }
//    );

//sender.Connect();

//foreach (var file in Directory.GetFiles(this._directory))
//{
//    var packets = PacketHelper.Frame(HeaderContainer.FolderFile, Encoding.ASCII.GetBytes(Path.GetFileName(file)));
//    foreach (var pa in packets)
//    {
//        sender.Send(pa);
//    }
//    //file end
//    sender.Send(PacketHelper.Frame(HeaderContainer.FolderFileEnd, new List<Byte> { 0x00 }).First());
//}
//sender.Send(PacketHelper.Frame(HeaderContainer.FolderFileFinalEnd, new List<Byte> { 0x00 }).First());

//sender.Close();
