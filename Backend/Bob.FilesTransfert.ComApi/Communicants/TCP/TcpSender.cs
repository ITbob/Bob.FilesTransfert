using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using Bob.FilesTransfert.ComApi.Communicants;

namespace Bob.FilesTransfert.ComApi.TCP.Communicants
{
    public class TcpSender: IByteSender
    {
        private Socket _socket;
        private IPEndPoint _senderInfo;
        private IPEndPoint _receiverInfo;

        public TcpSender(IPEndPoint senderInfo, IPEndPoint receiverInfo)
        {
            this._socket = new Socket(
                SocketType.Stream, //not aware of that, have to take a look
                ProtocolType.Tcp); // tcp protocol, whearas udp should be enough (internal network)

            this._senderInfo = senderInfo;
            this._receiverInfo = receiverInfo;
            this._socket.LingerState = new LingerOption(false,0);
            this._socket.LingerState.Enabled = false;
            this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this._socket.ReceiveTimeout = 500;
        }

        public void Connect()
        {
            if (this._socket.Connected)
            {
                throw new ApplicationException("Sockect is already connected");
            }
            this._socket.Bind(new IPEndPoint(this._senderInfo.Address, this._senderInfo.Port));
            this._socket.Connect(this._receiverInfo);
        }

        public Boolean IsConnected()
        {
            return this._socket.Connected;
        }

        public void Close()
        {
            if (!this._socket.Connected)
            {
                throw new ApplicationException("Sockect is not connected.");
            }
            try
            {
                //this._socket.Shutdown(SocketShutdown.Both);
                //this._socket.Disconnect(true);
                this._socket.Close();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        public void Send(IEnumerable<byte[]> packets)
        {
            if (!this._socket.IsBound)
            {
                throw new ApplicationException("socket is not bound");
            }

            if (!this._socket.Connected)
            {
                throw new ApplicationException("socket is not connected");
            }

            foreach (var packet in packets)
            {
                var count = this._socket.SendTo(packet, 0, packet.Length, SocketFlags.None, this._receiverInfo);
            }
        }

        public List<byte[]> SendWithFeedback(IEnumerable<byte[]> packets)
        {
            this.Send(packets);

            var receivedPacket = new Byte[2];
            var result = new List<Byte[]>();
            var received = false;
            do
            {
                var receivedCount = 0;
                try
                {
                    receivedCount = this._socket.Receive(receivedPacket, SocketFlags.None);
                    Debug.WriteLine($@"[Feedback] [header:{receivedPacket[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { receivedPacket[1] })}]");
                }
                catch (Exception)
                {
                    receivedCount = 0;
                }

                if (0 < receivedCount)
                {
                    received = true;
                    result.Add(receivedPacket);
                    receivedPacket = new Byte[2];
                }
                else
                {
                    received = false;
                }

            } while (received);
            //Debug.WriteLine($"Sent: {count} data: {Encoding.ASCII.GetString(data)}");

            return result;
        }
    }
}
