using Bob.FilesTransfert.ComApi.Communicants;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class FolderInfoSender
    {
        private IByteSender _sender;

        public FolderInfoSender(IByteSender sender)
        {
            this._sender = sender;
        }

        public void Send(IPEndPoint receiver)
        {
            var ipPortInfo = $"{receiver.Address}:{receiver.Port}";

            foreach (var packet in PacketHelper.Frame(
                    HeaderContainer.RequestFolder,
                    Encoding.ASCII.GetBytes(ipPortInfo)))
            {
                this._sender.Send(packet);
            }

            var bytes = Encoding.ASCII.GetBytes("#");
            foreach (var item in PacketHelper.Frame(HeaderContainer.RequestFolderEnd, bytes))
            {
                this._sender.Send(item);
            }

        }
    }
}
