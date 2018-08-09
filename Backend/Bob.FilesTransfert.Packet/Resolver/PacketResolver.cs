using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.FilesTransfet.PacketHandler.Maker;

namespace Bob.FilesTransfet.PacketHandler.Resolver
{
    public class PacketResolver : IPacketResolver
    {
        private IPacketMaker _maker;
        public byte[] GetContent(byte[] bytes)
        {
            if (!this.IsCorrectFormat(bytes))
            {
                throw new ApplicationException("wrong packet");
            }

            var result = new List<Byte>();

            for (int i = this._maker.HeaderCount(); i < bytes.Length; i++)
            {
                result.Add(bytes[i]);
            }

            return result.ToArray();
        }

        public void Init(IPacketMaker maker)
        {   
            this._maker = maker;
        }

        public Boolean IsCorrectFormat(byte[] bytes)
        {
            if(bytes.Count() < this._maker.HeaderCount())
            {
                return false;
            }

            var header = new List<byte>();
            for (int i = 0; i < this._maker.HeaderCount(); i++)
            {
                header.Add(bytes[i]);
            }

            var expectedHeader = this._maker.GetHeader();

            for (int i = 0; i < this._maker.HeaderCount(); i++)
            {
                if(header[i] != expectedHeader[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
