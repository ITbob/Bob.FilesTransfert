using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.FilesTransfert.Service
{
    public class ServiceProvider
    {
        private IList<IService> Services { get; set; } = new List<IService>();

        public void Add<T>(T service)
            where T:IService
        {
            Services.Add(service);
        }

        public void Remove<T>(T service)
            where T : IService
        {
            Services.Remove(service);
        }

        public T GetService<T>()
            where T : IService
        {
            return (T) Services.Where(s => s is T).Single();
        }
    }
}
