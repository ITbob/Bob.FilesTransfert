using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi.Handler
{
    public class FolderAckHandler : IPacketHandler
    {
        public EventHandler<List<String>> ReceivedFiles;
        public List<String> Files { get; set; } = new List<String>();
        private List<Byte> _bytes = new List<Byte>();

        private void OnReceived()
        {
            this.ReceivedFiles?.Invoke(this, this.Files);
        }

        public Boolean Handle(byte[] packet)
        {
            if (HeaderContainer.FolderFile.IsFormat(packet))
            {
                this._bytes.
                    AddRange(PacketHelper.Content(HeaderContainer.FolderFile, packet));
                return true;
            }
            else if (HeaderContainer.FolderFileEnd.IsFormat(packet))
            {
                var fileName = Encoding.ASCII.GetString(this._bytes.ToArray());
                this.Files.Add(fileName);
                this._bytes.Clear();
                return true;
            }
            else if (HeaderContainer.FolderFileFinalEnd.IsFormat(packet))
            {
                this.OnReceived();
                this.Files = new List<String>();
                return true;
            }

            return false;
        }
    }
}
