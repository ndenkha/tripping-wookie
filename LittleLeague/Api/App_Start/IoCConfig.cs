using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App_Start
{
    public static class IoCConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}