using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfet.PacketHandler.Maker
{
    public class FileNamePacketMaker : IPacketMaker
    {
        private String _content { get; set; }
        private Byte Header = 128;//1000 0000

        public FileNamePacketMaker()
        {
        }

        public void SetContent(string fileName)
        {
            this._content = fileName;
        }

        public byte[] GetPacket()
        {
            var result = new List<Byte>();
            result.Add(Header);
            result.AddRange(Encoding.ASCII.GetBytes(this._content));
            return result.ToArray();
        }

        public byte[] GetHeader()
        {
            return new byte[] { Header };
        }

        public int HeaderCount()
        {
            return 1;
        }
    }
}
