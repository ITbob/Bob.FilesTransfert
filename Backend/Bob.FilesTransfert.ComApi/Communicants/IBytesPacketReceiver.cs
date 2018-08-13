using Bob.FilesTransfert.ComApi.Communicants.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Communicants
{
    public interface IBytesPacketReceiver
    {
        Boolean Listen();
        Boolean IsListening();
        Boolean Close();
        EventHandler<PacketContext> ReceivedData { get; set; }
    }
}
