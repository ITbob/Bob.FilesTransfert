using Bob.FilesTransfert.ComApi.Communicants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.ComApi
{
    public class FileSender
    {
        private IByteSender sender;

        public FileSender(IByteSender sender)
        {
            this.sender = sender;
        }

        public void Send(String filepath)
        {
            //step1, send txt
        }

    }
}
