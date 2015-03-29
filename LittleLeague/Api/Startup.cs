using Api.App_Start;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Web.Common.OwinHost;
using Ninject;

[assembly: OwinStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(() => RegisterServices(IoCConfig.CreateKernel()));
            app.UseNinjectWebApi(WebApiConfig.Configure());
        }

        private IKernel RegisterServices(IKernel kernel)
        {
            return kernel;
        }
    }
}