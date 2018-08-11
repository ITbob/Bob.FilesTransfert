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

        public void Send(String filepath)
        {
            var namePacktes = PacketHelper.Frame(HeaderContainer.FileName, Encoding.ASCII.GetBytes(filepath));

            foreach (var packet in namePacktes)
            {
                this._sender.Send(packet);
            }

            var file = File.ReadAllBytes(filepath);
            var filePacktes = PacketHelper.Frame(HeaderContainer.FileContent, file);

            foreach (var packet in filePacktes)
            {
                this._sender.Send(packet);
            }

            var packets = PacketHelper.Frame(HeaderContainer.EndFileContent, new Byte[] { 0x22 });
            foreach (var packet in packets)
            {
                this._sender.Send(packet);
            }
        }

    }
}
