using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace _3rdPartyApis.Eventing
{
    public class EventPublisher : _3rdPartyApis.Eventing.IEventPublisher
    {
        public void Publish(string message)
        {
            LogManager
                .GetLogger("Event")
                .Info("Published: " + message);
        }
    }
}
