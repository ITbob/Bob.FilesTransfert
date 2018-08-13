using Bob.FilesTransfert.ComApi.Communicants.TCP;
using Bob.FilesTransfert.ComApi.Handler;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class DirectoryRequestHandler: IPacketHandler
    {
        private FileResult _fileResult { get; set; } = new FileResult();
        public EventHandler<FileResult> ReceivedFile;

        public void OnReceivedFile()
        {
            this.ReceivedFile?.Invoke(this, this._fileResult);
        }

        public DirectoryRequestHandler()
        {
        }

        public Boolean Handle(PacketContext context)
        {
            //check packet format
            if (HeaderContainer.FileName.IsFormat(context.CurrentPacket))
            {
                this._fileResult.FilenameBytes.
                    AddRange(PacketHelper.Content(HeaderContainer.FileName, context.CurrentPacket));
                return true;
            }
            else if (HeaderContainer.FileContent.IsFormat(context.CurrentPacket))
            {
                this._fileResult.FileBytes.
                    AddRange(PacketHelper.Content(HeaderContainer.FileContent, context.CurrentPacket));
                return true;
            }
            else if (HeaderContainer.EndFileContent.IsFormat(context.CurrentPacket))
            {
                this.OnReceivedFile();
                this._fileResult = new FileResult();
                return true;
            }
            return false;
        }
    }
}
