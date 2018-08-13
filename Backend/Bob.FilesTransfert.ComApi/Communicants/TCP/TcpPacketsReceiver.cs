using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfert.ComApi.Communicants.TCP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.TCP.Communicants
{
    public class TcpPacketsReceiver:IBytesPacketReceiver
    {
        private TcpListener _listener;
        private IPEndPoint _info;
        private Int32 _backlog = 5;
        public EventHandler<PacketContext> ReceivedData { get; set; }

        private void OnReceivedData(PacketContext packet)
        {
            this.ReceivedData?.Invoke(this, packet);
        }

        private CancellationTokenSource CancellationToken;
        private Task _listeningTask;
        private Int32 _count;

        public TcpPacketsReceiver(IPEndPoint info, Int32 count)
        {
            this._info = info;
            this._count = count;
            this._listener = new TcpListener(this._info.Address, this._info.Port);
            this._listener.Server.LingerState.Enabled = false;
            this._listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public Boolean Close()
        {
            if (this.IsListening())
            {
                this.CancellationToken.Cancel();
                Debug.WriteLine($"[{this._info.Port}] ask server cancellation");

                this._listener.Stop();
                Debug.WriteLine($"[{this._info.Port}] ask server stop");

                //this._listener.Server.Disconnect(true);
                //Debug.WriteLine($"[{this._info.Port}]Server socket disconnected");

                while (this._listeningTask.Status == TaskStatus.Running)
                {
                    Thread.Sleep(100);
                }
                Debug.WriteLine($"[{this._info.Port}] server task is done.");


                this._listener.Server.Close();
                Debug.WriteLine($"[{this._info.Port}]Server socket closed");

                if (this._listeningTask.IsCompleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public Boolean IsListening()
        {
            return this.CancellationToken != null && !this.CancellationToken.IsCancellationRequested;
        }

        public Boolean Listen()
        {
            this.CancellationToken = new CancellationTokenSource();
            this._listeningTask = Task.Run(() => this.Listening(this.CancellationToken.Token));
            return true;
        }

        private void Listening(CancellationToken token)
        {
            TcpClient client = null;
            this._listener.Start(5);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    client = this._listener.AcceptTcpClient();
                    Task.Run(() => Handling(token, client));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void Handling(CancellationToken token, TcpClient clientSocket)
        {
            clientSocket.LingerState.Enabled = false;
            var networkStream = clientSocket.GetStream();

            var packet = new Byte[this._count];

            while (networkStream.CanRead)
            {
                var count = networkStream.Read(packet, 0, this._count);
                if (0 < count)
                {
                    try
                    {
                        Debug.WriteLine($@"[Received:{count}] [header:{packet[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { packet[1] })}]");
                    }
                    catch (Exception)
                    {
                    }

                    this.OnReceivedData(new PacketContext(packet, networkStream));

                    //clean packet
                    packet = new Byte[this._count];
                }
            }
            clientSocket.Client.Shutdown(SocketShutdown.Both);
            clientSocket.Client.Close();
            clientSocket.Close();
        }
    }
}
