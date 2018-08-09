using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Maker
{
    public class FilePacketMaker : IPacketMaker
    {
        private Byte Header = 128;//1000 0000

        public byte[] GetHeader()
        {
            throw new NotImplementedException();
        }

        public byte[] GetPacket()
        {
            throw new NotImplementedException();
        }

        public int HeaderCount()
        {
            throw new NotImplementedException();
        }
    }
}
