using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Maker
{
    public class Header
    {
        public Byte Content { get; set; }

        public Boolean IsFormat(byte[] bytes)
        {
            if (bytes.Count() == 0)
            {
                return false;
            }
            return bytes[0] == this.Content;
        }
    }
}
