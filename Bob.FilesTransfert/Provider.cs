using Bob.FilesTransfert.ComApi.Service;
using Bob.FilesTransfert.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Transferts
{
    public static class Provider
    {
        private static ServiceProvider _provider = new ServiceProvider();

        public static void Init()
        {
            _provider.Add(new ServerService());
        }

        public static T GetService<T>()
            where T : IService
        {
            return _provider.GetService<T>();
        }
    }
}
