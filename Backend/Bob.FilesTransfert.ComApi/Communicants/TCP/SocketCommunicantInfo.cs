using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.TCP.Communicants
{
    public struct SocketCommunicantInfo
    {
        public IPAddress Ip { get; set; }
        public Int32 Port { get; set; }
    }
}
