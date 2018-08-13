using Bob.FilesTransfert.ComApi.Communicants.TCP;
using Bob.FilesTransfert.ComApi.TCP.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FileTransfet.ComApi.Test
{
    public partial class TcpCommunicants_Features: FeatureFixture
    {
        private TcpSender Sender { get; set; }
        private TcpPacketsReceiver Receiver { get; set; }

        private void Set_couple(
            Int32 senderPort, 
            Int32 receiverPort, 
            Action<Object,PacketContext> func)
        {
            var senderInfo = new IPEndPoint(SocketHelper.GetIPAddress(),senderPort);
            var receiverInfo = new IPEndPoint(SocketHelper.GetIPAddress(), receiverPort);

            this.Receiver = new TcpPacketsReceiver(receiverInfo,2);
            this.Sender = new TcpSender(senderInfo, receiverInfo);

            this.Receiver.Listen();
            this.Sender.Connect();

            this.Receiver.ReceivedData += ((o,e) => func(o,e));
        }

        private void Unsubscrible(Action<Object, PacketContext> func)
        {
            this.Receiver.ReceivedData -= ((o, e) => func(o, e));
            this._packets.Clear();
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

        private List<Byte[]> _packets = new List<Byte[]>();
        private void OnReceivedPacket(Object obj, PacketContext packet)
        {
            this._packets.Add(packet.CurrentPacket);
        }

        private void Send_filename_packet()
        {
            var content = Encoding.ASCII.GetBytes("bob");

            var header = new Header() {
                Content = 0xAA,
            };

            var packets = PacketHelper.Frame(header, content);

            Sender.Send(packets);

        }

        private void Check_filename_packet()
        {
            var header = new Header()
            {
                Content = 0xAA,
            };
            var message = Encoding.ASCII.GetString(PacketHelper.Content(header, this._packets).ToArray());
            Assert.AreEqual("bob", message);
        }
    }
}
