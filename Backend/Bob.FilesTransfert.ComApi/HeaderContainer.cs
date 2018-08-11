using Bob.FilesTransfet.PacketHandler.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public static class HeaderContainer
    {

        public static Header FileName { get; } = new Header() { Content = 0x01 };
        public static Header FileContent { get; } = new Header() { Content = 0x02 };
        public static Header EndFileContent { get; } = new Header() { Content = 0x03 };

        //ugly
        public static Header RequestFolder { get; } = new Header() { Content = 0x04 };
        public static Header RequestFolderEnd { get; } = new Header() { Content = 0x05 };

        public static Header FolderFile { get; } = new Header() { Content = 0x06 };
        public static Header FolderFileEnd { get; } = new Header() { Content = 0x07 };
        public static Header FolderFileFinalEnd { get; } = new Header() { Content = 0x08 };

    }
}
