using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class FileSender
    {
        private IByteSender _sender;

        public FileSender(IByteSender sender)
        {
            this._sender = sender;
        }

        public List<String> Send(String filepath)
        {
            var namePacktes = PacketHelper.Frame(HeaderContainer.FileName, Encoding.ASCII.GetBytes(filepath));

            this._sender.Send(namePacktes);

            var file = File.ReadAllBytes(filepath);
            var filePacktes = PacketHelper.Frame(HeaderContainer.FileContent, file);

            this._sender.Send(filePacktes);

            var packets = PacketHelper.Frame(HeaderContainer.EndFileContent, new Byte[] { 0x22 });
            this._sender.Send(packets);

            return null;
        }

    }
}
