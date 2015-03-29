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
    public class Player : IDependencyConsumer, IAuditable
    {
        IKernel kernel;

        public int PlayerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsRegistered { get; private set; }
        public DateTime? RegistrationDate { get; private set; }
        public int TeamId { get; private set; }
        public Team Team { get; private set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        Lazy<IEventPublisher> eventPublisher;
        IEventPublisher EventPublisher
        {
            get { return eventPublisher.Value; }
        }

        Player()
        {
            eventPublisher = new Lazy<IEventPublisher>(() => kernel.Get<IEventPublisher>());
        }

        public Player(string firstName, string lastName, Team team, IKernel kernel)
            : this()
        {
            this.kernel = kernel;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Team = team;
        }

        public void Register()
        {
            IsRegistered = true;
            RegistrationDate = DateTime.UtcNow;
            EventPublisher.Publish(string.Format("Registered {0} {1}.", FirstName, LastName));
        }

        public void UnRegister()
        {
            IsRegistered = false;
            RegistrationDate = null;
            EventPublisher.Publish(string.Format("Unregistered {0} {1}.", FirstName, LastName));
        }

        void IDependencyConsumer.Accept(IKernel kernel)
        {
            this.kernel = kernel;
        }
    }
}
