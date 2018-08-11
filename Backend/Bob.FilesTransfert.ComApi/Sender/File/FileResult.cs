using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class FileResult
    {
        public List<Byte> FilenameBytes { get; set; } = new List<Byte>();
        public List<Byte> FileBytes { get; set; } = new List<Byte>();
    }
}
