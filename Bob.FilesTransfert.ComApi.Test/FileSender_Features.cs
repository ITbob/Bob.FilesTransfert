using Bob.FilesTransfert.ComApi.Handler;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Test
{
    [TestFixture]
    public class FileSender_Features
    {
        private TcpSender Sender { get; set; }
        private TcpPacketsReceiver Receiver { get; set; }

        [Test]
        [Sequential]
        [Category("integration")]
        public void Send_a_file()
        {
            var target = @"C:\Users\Freddy\Desktop\Sandbox\bestPractices.txt";
            var file = File.ReadAllBytes(target);
            var destination = @"C:\Users\Freddy\Desktop\Sandbox\Destination\bestPractices.txt";

            var senderInfo = new IPEndPoint(SocketHelper.GetIPAddress(),8781);
            var receiverInfo = new IPEndPoint(SocketHelper.GetIPAddress(), 8782);

            this.Receiver = new TcpPacketsReceiver(receiverInfo,2);
            this.Sender = new TcpSender(senderInfo, receiverInfo);

            var packetDispatcher = new PacketDispatcher(this.Receiver,
                new List<Handler.IPacketHandler>
                {
                    new FileRequestHandler()
                });
            packetDispatcher.Start();

            this.Sender.Connect();

            var namePacktes = PacketHelper.Frame(HeaderContainer.FileName, Encoding.ASCII.GetBytes(destination));

            foreach (var packet in namePacktes)
            {
                this.Sender.Send(packet);
            }

            var filePacktes = PacketHelper.Frame(HeaderContainer.FileContent, file);

            foreach (var packet in filePacktes)
            {
                this.Sender.Send(packet);
            }

            var packets = PacketHelper.Frame(HeaderContainer.EndFileContent, new Byte[] { 0x22 });
            foreach (var packet in packets)
            {
                this.Sender.Send(packet);
            }

            Assert.AreEqual(file, File.ReadAllBytes(destination));

            this.Receiver.Close();
            this.Sender.Close();
        }

        [Test]
        [Sequential]
        [Category("integration")]
        public void Send_and_receive()
        {
            var senderInfo = new IPEndPoint(SocketHelper.GetIPAddress(), 8731);
            var senderInfo2 = new IPEndPoint(SocketHelper.GetIPAddress(), 8734);
            var receiverInfo = new IPEndPoint(SocketHelper.GetIPAddress(), 8732);
            var receiverInfo2 = new IPEndPoint(SocketHelper.GetIPAddress(), 8733);

            var receiver = new TcpPacketsReceiver(receiverInfo, 2);
            var receiver2 = new TcpPacketsReceiver(receiverInfo2, 2);

            var sender = new TcpSender(senderInfo, receiverInfo);

            receiver.Listen();
            receiver2.Listen();

            sender.Connect();


            var handler2 = new FolderResquetHandler(senderInfo2);
            handler2.SetDirectory(@"C:\Users\Freddy\Desktop\Sandbox");

            var dispatcher = new PacketDispatcher(receiver,
                new List<IPacketHandler>()
                {
                    handler2
                });

            var handller = new FolderAckHandler();
            handller.ReceivedFiles += this.OnReceived;

            var dispatcher2 = new PacketDispatcher(receiver2,
                new List<IPacketHandler>()
                {
                    handller
                });



            var ipPortInfo = $"{SocketHelper.GetIPAddress().ToString()}:{8733}";

            foreach (var packet in PacketHelper.Frame(
                    HeaderContainer.RequestFolder,
                    Encoding.ASCII.GetBytes(ipPortInfo)))
            {
                sender.Send(packet);
            }

            sender.Send(PacketHelper.Frame(HeaderContainer.RequestFolderEnd, new List<Byte> { 0x00 }).First());

            while (this._files == null)
            {
                Thread.Sleep(200);
            }

            Assert.AreEqual(2, this._files.Count);

            sender.Close();
            receiver.Close();
            receiver2.Close();
        }

        private List<String> _files = null;

        private void OnReceived(Object obj, List<String> filenames)
        {
            this._files = filenames;
        }
    }
}
