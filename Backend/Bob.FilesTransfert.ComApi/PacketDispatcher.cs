using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfert.ComApi.Handler;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class PacketDispatcher
    {
        private ConcurrentQueue<Byte[]> _rawPackets = new ConcurrentQueue<Byte[]>();
        private IEnumerable<IPacketHandler> _packetHandlers;
        private Task _resolvingTask;
        private IBytesPacketReceiver _receiver;
        private Boolean IsActive = false;
        private readonly Object _syncObject = new object();
        private void SetActive(Boolean val)
        {
            lock (this._syncObject)
            {
                this.IsActive = val;
            }
        }

        private Boolean SetActiveIfnot()
        {
            lock (this._syncObject)
            {
                if (!this.IsActive)
                {
                    this.IsActive = true;
                }
                else
                {
                    return true;
                }

                return false;
            }
        }

        public PacketDispatcher(IBytesPacketReceiver receiver, IEnumerable<IPacketHandler> handlers)
        {
            this._receiver = receiver;
            this._packetHandlers = handlers;
            this._receiver.ReceivedData += OnReceivedData;
            this._resolvingTask = Task.Run(() => this.ResolvePackets());
        }

        private void OnReceivedData(object obj, Byte[] packet)
        {
            //if (this._packetHandlers.Count() == 2)
            //{
            //    Debug.WriteLine($"[Receiving] {this._rawPackets.Count}");
            //}

            this._rawPackets.Enqueue(packet);
            Debug.WriteLine($@"[Dispatching:{packet.Length}] [header:{packet[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { packet[1] })}]");


            //if (!this.SetActiveIfnot())
            //{
            //}

        }

        private void ResolvePackets()
        {
            var any = false;
            Byte[] packet = null;

            while(true)
            {
                //check if packets are available
                any = this._rawPackets.Any();
                //if (any)
                //{
                //    Debug.WriteLine("[Detecting]");
                //}

                if (this._packetHandlers.Count() == 2)
                {
                    Debug.WriteLine($"[Alive] {this._rawPackets.Count}");
                }

                if (any)
                {
                    //get packet
                    var succeed = false;
                    do
                    {
                        succeed = this._rawPackets.TryDequeue(out packet);

                        //if (this._packetHandlers.Count() == 2)
                        //{
                        //    Debug.WriteLine($"[Extracting] {this._rawPackets.Count}");
                        //}


                    } while (!succeed);

                    Debug.WriteLine($@"[Handling:{packet.Length}] [header:{packet[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { packet[1] })}]");

                    foreach (var packetHandler in this._packetHandlers)
                    {
                        if (packetHandler.Handle(packet))
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            } //while (any);

            this.SetActive(false);
        }

        public void Start()
        {
            this._receiver.Listen();
        }

        public void Stop()
        {
            this._receiver.Close();
        }

    }
}
