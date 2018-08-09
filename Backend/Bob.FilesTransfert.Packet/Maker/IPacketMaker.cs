using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Maker
{
    public interface IPacketMaker
    {
        Byte[] GetPacket();
        Byte[] GetHeader();
        Int32 HeaderCount();
    }
}
