using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfert.ComApi.Communicants.TCP;
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
        private ConcurrentQueue<PacketContext> _rawPackets = new ConcurrentQueue<PacketContext>();
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

        private void OnReceivedData(object obj, PacketContext context)
        {
            //if (this._packetHandlers.Count() == 2)
            //{
            //    Debug.WriteLine($"[Receiving] {this._rawPackets.Count}");
            //}

            this._rawPackets.Enqueue(context);
            Debug.WriteLine($@"[Dispatching:{context.CurrentPacket.Length}] [header:{context.CurrentPacket[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { context.CurrentPacket[1] })}]");


            //if (!this.SetActiveIfnot())
            //{
            //}

        }

        private void ResolvePackets()
        {
            var any = false;
            PacketContext context = null;

            while(true)
            {
                //check if packets are available
                any = this._rawPackets.Any();

                if (any)
                {
                    //get packet
                    var succeed = false;
                    do
                    {
                        succeed = this._rawPackets.TryDequeue(out context);

                        //if (this._packetHandlers.Count() == 2)
                        //{
                        //    Debug.WriteLine($"[Extracting] {this._rawPackets.Count}");
                        //}


                    } while (!succeed);

                    Debug.WriteLine($@"[Handling:{context.CurrentPacket.Length}] [header:{context.CurrentPacket[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { context.CurrentPacket[1] })}]");

                    foreach (var packetHandler in this._packetHandlers)
                    {
                        if (packetHandler.Handle(context))
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
