using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using Bob.FilesTransfet.PacketHandler.Resolver;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FileTransfet.ComApi.Test
{
    public partial class TcpCommunicants_Features: FeatureFixture
    {
        private TcpSender Sender { get; set; }
        private TcpReceiver Receiver { get; set; }

        private FileNamePacketMaker FileNameMaker { get; set; }
        private PacketResolver Resolver { get; set; }

        private void Set_couple(Int32 senderPort, Int32 receiverPort)
        {
            var senderInfo = new SocketCommunicantInfo()
            {
                Ip = SocketHelper.GetIPAddress(),
                Port = senderPort
            };

            var receiverInfo = new SocketCommunicantInfo()
            {
                Ip = SocketHelper.GetIPAddress(),
                Port = receiverPort
            };

            this.Receiver = new TcpReceiver(receiverInfo);
            this.Sender = new TcpSender(senderInfo, receiverInfo);

            this.Receiver.Listen();
            this.Sender.Connect();
        }

        private void Close_couple()
        {
            this.Sender.Close();
            this.Receiver.Close();
        }

        private void Sleep_200ms()
        {
            Thread.Sleep(200);
        }

        private void Send_simple_message()
        {
            this.Sender.Send(Encoding.ASCII.GetBytes("bob"));

        }

        private void Receive_simple_message()
        {
            var result = this.Receiver.GetReceivedData();
            Assert.AreEqual("bob", Encoding.ASCII.GetString(result));
        }

        private void Send_filename_packet()
        {
            this.FileNameMaker = new FileNamePacketMaker();
            this.FileNameMaker.SetContent("bob");

            this.Resolver = new PacketResolver();
            this.Resolver.Init(this.FileNameMaker);

            Sender.Send(this.FileNameMaker.GetPacket());
        }

        private void Receive_filename_packet()
        {
            var result = this.Receiver.GetReceivedData();

            Assert.AreEqual(true, this.Resolver.IsCorrectFormat(result));
            Assert.AreEqual("bob", Encoding.ASCII.GetString(this.Resolver.GetContent(result)));
        }
    }
}
