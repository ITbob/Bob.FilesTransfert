using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Resolver
{
    public interface IPacketResolver
    {
        void Init(IPacketMaker maker);
        Boolean IsCorrectFormat(Byte[] bytes);
        Byte[] GetContent(Byte[] bytes);
    }
}
