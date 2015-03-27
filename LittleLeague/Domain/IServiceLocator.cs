using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IServiceLocator
    {
        T GetService<T>();
        T GetService<T>(string name);
    }
}
