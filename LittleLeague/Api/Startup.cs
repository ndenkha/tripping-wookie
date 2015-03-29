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
            app.Map("api", apiApp =>
            {
                apiApp.UseNinjectMiddleware(() => RegisterServicesForREST(IoCConfig.CreateKernel()));
                apiApp.UseNinjectWebApi(WebApiConfig.Configure());
            }).Map("odata", odataApp =>
            {
                odataApp.UseNinjectMiddleware(() => RegisterServicesForREST(IoCConfig.CreateKernel()));
                odataApp.UseNinjectWebApi(WebApiConfig.Configure());
            });
        }

        private IKernel RegisterServicesForREST(IKernel kernel)
        {
            return kernel;
        }

        private IKernel RegisterServicesForODATA(IKernel kernel)
        {
            return kernel;
        }
    }
}