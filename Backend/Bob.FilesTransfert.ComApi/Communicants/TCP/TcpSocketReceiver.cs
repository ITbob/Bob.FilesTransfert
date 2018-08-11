using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace Bob.FilesTransfert.ComApi.TCP.Communicants
{
    public class TcpSocketReceiver
    {
        private IPEndPoint _info;
        private Socket _socket;
        private Object syncObject = new Object();
        private Queue<Byte> Bytes { get; set; } = new Queue<byte>();
        private CancellationTokenSource CancellationToken;

        public TcpSocketReceiver(IPEndPoint info)
        {
            this._info = info;
            this._socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //this._socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.AcceptConnection, true);
        }

        public void Close()
        {
            if(this.CancellationToken == null)
            {
                this.CancellationToken.Cancel();
            }
            else
            {
                this._socket.Close();
            }
        }

        public void Create()
        {
            if (this._socket.IsBound)
            {
                throw new ApplicationException("Sockect is already created");
            }

            this._socket.Bind(this._info);
            this._socket.Listen(100);
            this._socket.BeginAccept((e) => this.AsyncAccept(e), this._socket);

            this.CancellationToken = new CancellationTokenSource();
            Task.Run(() => this.Listen(this.CancellationToken.Token))
                .ContinueWith((e) => this.Close());
        }

        public void AsyncAccept(IAsyncResult ar)
        {
            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            var data = handler.EndReceive(ar);
        }

        public Byte[] GetData()
        {
            var result = new List<Byte>();
            lock (this.syncObject)
            {
                while(0 < this.Bytes.Count)
                {
                    result.Add(this.Bytes.Dequeue());
                }
            }
            return result.ToArray();
        }

        private void Listen(CancellationToken token)
        {
            var buffer = new Byte[1];

            while (!token.IsCancellationRequested)
            {
                if (this._socket.Connected)
                {
                    var count = this._socket.Receive(buffer);

                    if (0 < count)
                    {
                        Debug.WriteLine($@"received {count} {buffer.Length}");
                    }

                    lock (syncObject)
                    {
                        this.Bytes.Enqueue(buffer[0]);
                    }
                }
            }
        }

        public bool IsCreated()
        {
            throw new NotImplementedException();
        }

        public bool IsListening()
        {
            throw new NotImplementedException();
        }
    }
}
//public void AsyncAccept(IAsyncResult ar)
//{
//    // Get the socket that handles the client request.  
//    Socket listener = (Socket)ar.AsyncState;
//    Socket handler = listener.EndAccept(ar);

//    handler.BeginReceive(this.Bytes, 0, this.Bytes.Count, 0,
//        new AsyncCallback(ReadCallback), this);
//}

//public static void ReadCallback(IAsyncResult ar)
//{
//    String content = String.Empty;

//    // Retrieve the state object and the handler socket  
//    // from the asynchronous state object.  
//    TcpListener state = (TcpListener)ar.AsyncState;
//    Socket handler = state._socket;

//    // Read data from the client socket.   
//    int bytesRead = handler.EndReceive(ar);

//    if (bytesRead > 0)
//    {
//        // There  might be more data, so store the data received so far.  
//        state.sb.Append(Encoding.ASCII.GetString(
//            state.buffer, 0, bytesRead));

//        // Check for end-of-file tag. If it is not there, read   
//        // more data.  
//        content = state.sb.ToString();
//        if (content.IndexOf("<EOF>") > -1)
//        {
//            // All the data has been read from the   
//            // client. Display it on the console.  
//            Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
//                content.Length, content);
//            // Echo the data back to the client.  
//            Send(handler, content);
//        }
//        else
//        {
//            // Not all data received. Get more.  
//            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//            new AsyncCallback(ReadCallback), state);
//        }
//    }
//}