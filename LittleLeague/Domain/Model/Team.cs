using _3rdPartyApis.Configuration;
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
    public class Team : IDependencyConsumer, IAuditable
    {
        IKernel kernel;

        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Player> Players { get; private set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        Lazy<ITeamConfiguration> teamConfiguration;
        ITeamConfiguration TeamConfiguration
        {
            get { return teamConfiguration.Value; }
        }

        Lazy<IEventPublisher> eventPublisher;
        IEventPublisher EventPublisher
        {
            get { return eventPublisher.Value; }
        }

        Team()
        {
            teamConfiguration = new Lazy<ITeamConfiguration>(() => kernel.Get<ITeamConfiguration>());
            eventPublisher = new Lazy<IEventPublisher>(() => kernel.Get<IEventPublisher>());
        }

        public Team(string name, IKernel kernel)
            : this()
        {
            this.kernel = kernel;
            this.Name = name;
            this.Players = new List<Player>();
        }

        public Team AddPlayer(Player player)
        {
            EnforceMaxPlayerLimit();
            this.Players.Add(player);
            EventPublisher.Publish(string.Format("Player {0} {1} added to {2}.", player.FirstName, player.LastName, Name));
            return this;
        }

        public void RegisterPlayers()
        {
            var unregisteredPlayers = Players.Where(x => x.IsRegistered == false);
            unregisteredPlayers.ForEach(player => player.Register());
        }

        void IDependencyConsumer.Accept(IKernel kernel)
        {
            this.kernel = kernel;
        }

        void EnforceMaxPlayerLimit()
        {
            if (this.Players.Count() >= TeamConfiguration.GetMaxPlayerCount(Name))
                throw new Exception("Player count may not exceed max configuration for team.");
        }
    }
}
