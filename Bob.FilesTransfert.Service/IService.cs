using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.Service
{
    public interface IService
    {
        void Init();
        IEnumerable<IService> Dependencies { get; set; }
    }
}
