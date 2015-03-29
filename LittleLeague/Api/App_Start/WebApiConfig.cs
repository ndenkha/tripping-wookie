using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.App_Start
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Configure()
        {
            HttpConfiguration config = new HttpConfiguration();

            //Make the json formatter the first formatter because json is just better.
            var xmlFormatter = config.Formatters.XmlFormatter;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(xmlFormatter);

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            config.MapHttpAttributeRoutes();

            return config;
        }
    }
}