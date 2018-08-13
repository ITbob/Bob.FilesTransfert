using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Communicants
{
    public interface IByteSender
    {
        void Connect();
        void Close();
        Boolean IsConnected();
        void Send(IEnumerable<Byte[]> packets);
        List<Byte[]> SendWithFeedback(IEnumerable<Byte[]> packets);
    }
}
