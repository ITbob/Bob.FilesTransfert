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
        private SocketCommunicantInfo _senderInfo;
        private SocketCommunicantInfo _receiverInfo;
        private IPEndPoint _receiver;
        public TcpSender(SocketCommunicantInfo senderInfo, SocketCommunicantInfo receiverInfo)
        {
            this._socket = new Socket(
                SocketType.Stream, //not aware of that, have to take a look
                ProtocolType.Tcp); // tcp protocol, whearas udp should be enough (internal network)

            this._senderInfo = senderInfo;
            this._receiverInfo = receiverInfo;
        }

        public void Connect()
        {
            if (this._socket.Connected)
            {
                throw new ApplicationException("Sockect is already connected");
            }
            this._receiver = new IPEndPoint(this._receiverInfo.Ip, this._receiverInfo.Port);
            this._socket.Bind(new IPEndPoint(this._senderInfo.Ip, this._senderInfo.Port));
            this._socket.Connect(this._receiver);
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

            this._socket.Close();
        }

        public void Send(byte[] data)
        {
            if (!this._socket.IsBound)
            {
                throw new ApplicationException("socket is not bound");
            }

            if (!this._socket.Connected)
            {
                throw new ApplicationException("socket is not connected");
            }

            var count = this._socket.SendTo(data,0,data.Length,SocketFlags.None, this._receiver);
            Debug.WriteLine($"Send: {count} {data}");
        }
    }
}
