using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Maker
{
    public static class PacketHelper
    {
        public static List<Byte[]> Frame(Header header, IEnumerable<Byte> content)
        {
            var result = new List<byte[]>();
            
            foreach (var val in content)
            {
                var packet = new Byte[2];

                packet[0] = header.Content;
                packet[1] = val;

                result.Add(packet);
            }

            return result;
        }

        public static List<Byte> Content(Header header, Byte[] packet)
        {
            var result = new List<Byte>();

            if (header.IsFormat(packet))
            {
                for (int i = 1; i < packet.Length; i++)
                {
                    result.Add(packet[i]);
                }
            }

            return result;
        }

        public static List<Byte> Content(Header header, List<Byte[]> packets)
        {
            var result = new List<Byte>();

            foreach (var packet in packets)
            {
                if (header.IsFormat(packet))
                {
                    for (int i = 1; i < packet.Length; i++)
                    {
                        result.Add(packet[i]);
                    }
                }
            }

            return result;
        }

    }
}
