using Bob.FilesTransfert.ComApi.Handler;
using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class FileRequestHandler: IPacketHandler
    {
        private FileResult _fileResult { get; set; } = new FileResult();
        public EventHandler<FileResult> ReceivedFile;

        public void OnReceivedFile()
        {
            this.ReceivedFile?.Invoke(this, this._fileResult);
        }

        public FileRequestHandler()
        {
        }

        public Boolean Handle(Byte[] packet)
        {
            //check packet format
            if (HeaderContainer.FileName.IsFormat(packet))
            {
                this._fileResult.FilenameBytes.
                    AddRange(PacketHelper.Content(HeaderContainer.FileName, packet));
                return true;
            }
            else if (HeaderContainer.FileContent.IsFormat(packet))
            {
                this._fileResult.FileBytes.
                    AddRange(PacketHelper.Content(HeaderContainer.FileContent, packet));
                return true;
            }
            else if (HeaderContainer.EndFileContent.IsFormat(packet))
            {
                this.OnReceivedFile();
                this._fileResult = new FileResult();
                return true;
            }
            return false;
        }
    }
}
