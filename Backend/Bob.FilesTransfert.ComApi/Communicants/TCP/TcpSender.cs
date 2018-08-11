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

            var count = this._socket.SendTo(data,0,data.Length,SocketFlags.None, this._receiverInfo);
            //Debug.WriteLine($"Sent: {count} data: {Encoding.ASCII.GetString(data)}");
        }
    }
}
