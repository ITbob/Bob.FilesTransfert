using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Handler
{
    public interface IPacketHandler
    {
        Boolean Handle(Byte[] packet);
    }
}
