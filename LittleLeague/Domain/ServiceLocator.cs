using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ServiceLocator : IServiceLocator
    {
        IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public T GetService<T>()
        {
            return kernel.Get<T>();
        }

        public T GetService<T>(string name)
        {
            return kernel.Get<T>(name);
        }
    }
}
