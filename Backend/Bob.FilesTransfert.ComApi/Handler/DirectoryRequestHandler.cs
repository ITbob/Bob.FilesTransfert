using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bob.FilesTransfert.ComApi.Communicants.TCP;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;

namespace Bob.FilesTransfert.ComApi.Handler
{
    public class DirectoryRequestHandler : IPacketHandler
    {
        private String _directory = String.Empty;

        public void SetDirectory(String directory)
        {
            this._directory = directory;
        }

        public String GetDirectory()
        {
            return this._directory;
        }

        private void Reply(PacketContext context)
        {
            Debug.WriteLine("[Reply]");

            foreach (var file in Directory.GetFiles(this._directory))
            {
                var packets = PacketHelper.Frame(HeaderContainer.FolderFile, Encoding.ASCII.GetBytes(Path.GetFileName(file)));
                foreach (var pa in packets)
                {
                    context.Reply(pa);
                }
                //file end
                context.Reply(PacketHelper.Frame(HeaderContainer.FolderFileEnd, new List<Byte> { 0x00 }).First());
            }
            //context.Reply(PacketHelper.Frame(HeaderContainer.FolderFileFinalEnd, new List<Byte> { 0x00 }).First());
        }

        private readonly Object _syncObject = new object();

        public bool Handle(PacketContext context)
        {
            if (HeaderContainer.RequestFolder.IsFormat(context.CurrentPacket))
            {
                this.Reply(context);
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
