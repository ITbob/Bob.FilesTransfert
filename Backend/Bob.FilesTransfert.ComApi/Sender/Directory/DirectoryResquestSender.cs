using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Sender.Directory
{
    public class DirectoryResquestSender
    {
        private IByteSender _sender;

        public DirectoryResquestSender(IByteSender sender)
        {
            this._sender = sender;
        }

        public List<String> Send()
        {
            var result = this._sender.SendWithFeedback(PacketHelper.Frame(
                HeaderContainer.RequestFolder,
                new List<Byte> { 0x00 }));

            return this.Handle(result);
        }

        public List<String> Handle(List<Byte[]> packets)
        {
            var result = new List<String>();
            var bytes = new List<Byte>();

            foreach (var packet in packets)
            {
                if (HeaderContainer.FolderFile.IsFormat(packet))
                {
                    bytes.
                        AddRange(PacketHelper.Content(HeaderContainer.FolderFile, packet));
                }
                else if (HeaderContainer.FolderFileEnd.IsFormat(packet))
                {
                    var fileName = Encoding.ASCII.GetString(bytes.ToArray());
                    bytes.Clear();
                    result.Add(fileName);
                }
            }
            return result;
        }
    }
}
