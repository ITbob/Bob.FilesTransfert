using Bob.FilesTransfert.ComApi.Communicants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.TCP.Communicants
{
    public class TcpReceiver:IBytesReceiver
    {
        private TcpListener _listener;
        private SocketCommunicantInfo _info;
        private Int32 _backlog = 1;
        private Queue<Byte> Bytes { get; set; } = new Queue<byte>();
        private Object syncObject = new Object();
        private CancellationTokenSource CancellationToken;
        private Task _listeningTask;

        public TcpReceiver(SocketCommunicantInfo info)
        {
            this._info = info;
            this._listener = new TcpListener(this._info.Ip, this._info.Port);
        }

        public Boolean Close()
        {
            if (this.IsListening())
            {
                this.CancellationToken.Cancel();
                this._listeningTask.Wait(TimeSpan.FromSeconds(2));

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

        public Byte[] GetReceivedData()
        {
            var result = new List<Byte>();
            lock (this.syncObject)
            {
                while (0 < this.Bytes.Count)
                {
                    result.Add(this.Bytes.Dequeue());
                }
            }
            return result.ToArray();
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
            this._listener.Start();
            while (!token.IsCancellationRequested)
            {
                var clientSocket = this._listener.AcceptTcpClient();

                if(clientSocket != null)
                {
                    var networkStream = clientSocket.GetStream();
                    var data = new Byte[1];
                    while (networkStream.CanRead)
                    {
                        var count = networkStream.Read(data, 0, 1);

                        if(0 < count)
                        {
                            Debug.WriteLine($@"count: {count}, data {Encoding.ASCII.GetString(data)}");
                        }

                        lock (syncObject)
                        {
                            this.Bytes.Enqueue(data[0]);
                        }
                    }
                    clientSocket.Close();
                }
            }
            this._listener.Stop();
        }

    }
}
