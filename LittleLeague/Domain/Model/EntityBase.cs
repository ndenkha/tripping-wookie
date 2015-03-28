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

        ILog log;
        protected ILog Log
        {
            get
            {
                if (null == log)
                    log = (ILog)kernel.Get(typeof(ILog));
                return log;
            }
        }

        IEventPublisher eventPublisher;
        protected IEventPublisher EventPublisher
        {
            get
            {
                if (null == eventPublisher)
                    eventPublisher = (IEventPublisher)kernel.Get(typeof(IEventPublisher));
                return eventPublisher;
            }
        }

        protected EntityBase()
        {
            // Needed for the private default contructors of entities.
        }

        protected EntityBase(IKernel kernel)
        {
            ((IDependencyConsumer)this).Accept(kernel);
        }

        void IDependencyConsumer.Accept(IKernel kernel)
        {
            this.kernel = kernel;
        }
    }
}
