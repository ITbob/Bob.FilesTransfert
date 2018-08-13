using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Communicants.TCP
{
    public class PacketContext
    {
        public Byte[] CurrentPacket { get; private set; }
        private NetworkStream Stream { get; set; }

        public PacketContext(Byte[] currentPacket, NetworkStream stream)
        {
            this.CurrentPacket = currentPacket;
            this.Stream = stream;
        }

        public void Reply(Byte[] packet)
        {
            try
            {
                Debug.WriteLine($@"[Reply] [header:{packet[0]}] [data:{Encoding.ASCII.GetString(new Byte[1] { packet[1] })}]");
            }
            catch (Exception)
            {
            }
            this.Stream.Write(packet, 0, 2);
        }
    }
}
