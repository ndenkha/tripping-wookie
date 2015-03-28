using System;

namespace _3rdPartyApis.Eventing
{
    public interface IEventPublisher
    {
        void Publish(string message);
    }
}
