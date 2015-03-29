using _3rdPartyApis.Eventing;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public abstract class EntityBase : IDependencyConsumer, IAuditable
    {
        protected IKernel kernel;

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        Lazy<ILog> log;
        protected ILog Log
        {
            get
            {
                return log.Value;
            }
        }

        Lazy<IEventPublisher> eventPublisher;
        protected IEventPublisher EventPublisher
        {
            get
            {
                return eventPublisher.Value;
            }
        }

        protected EntityBase()
        {
            eventPublisher = new Lazy<IEventPublisher>(() => kernel.Get<IEventPublisher>());
            log = new Lazy<ILog>(() => kernel.Get<ILog>());
        }

        protected EntityBase(IKernel kernel) : this()
        {
            ((IDependencyConsumer)this).Accept(kernel);
        }

        void IDependencyConsumer.Accept(IKernel kernel)
        {
            this.kernel = kernel;
        }
    }
}
