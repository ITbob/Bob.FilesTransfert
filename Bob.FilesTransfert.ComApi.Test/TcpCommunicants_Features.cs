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
            Action<Object,Byte[]> func)
        {
            var senderInfo = new IPEndPoint(SocketHelper.GetIPAddress(),senderPort);
            var receiverInfo = new IPEndPoint(SocketHelper.GetIPAddress(), receiverPort);

            this.Receiver = new TcpPacketsReceiver(receiverInfo,2);
            this.Sender = new TcpSender(senderInfo, receiverInfo);

            this.Receiver.Listen();
            this.Sender.Connect();

            this.Receiver.ReceivedData += ((o,e) => func(o,e));
        }

        private void Unsubscrible(Action<Object, Byte[]> func)
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

        private void Send_simple_message()
        {
            this.Sender.Send(Encoding.ASCII.GetBytes("bob"));
        }

        private void Send_simple_long_message()
        {
            this.Sender.Send(
                Encoding.ASCII.GetBytes("its a very very very long message that may provoke an issue."));
        }

        private List<Byte[]> _packets = new List<Byte[]>();
        private void OnReceivedPacket(Object obj, Byte[] packet)
        {
            this._packets.Add(packet);
        }

        private void Check_simple_long_message()
        {
            var message = new List<Byte>();
            foreach (var packet in this._packets)
            {
                foreach (var item in packet)
                {
                    message.Add(item);
                }
            }
            var mes = Encoding.ASCII.GetString(message.ToArray());
            Assert.AreEqual("its a very very very long message that may provoke an issue.", mes);
        }

        private void Check_basic_message()
        {
            var message = new List<Byte>();
            foreach (var packet in this._packets)
            {
                foreach (var item in packet)
                {
                    message.Add(item);
                }
            }
            var mes = Encoding.ASCII.GetString(message.ToArray());
            Assert.AreEqual("bob", mes);
        }

        private void Send_filename_packet()
        {
            var content = Encoding.ASCII.GetBytes("bob");

            var header = new Header() {
                Content = 0xAA,
            };

            var packets = PacketHelper.Frame(header, content);

            foreach (var packet in packets)
            {
                Sender.Send(packet);
            }
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
