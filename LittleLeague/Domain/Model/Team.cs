using _3rdPartyApis.Configuration;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Team : EntityBase
    {
        ITeamConfiguration teamConfiguration;

        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Player> Players { get; private set; }

        Team()
        {
            //For use by entity framework only.
        }

        public Team(string name, IKernel kernel)
            : base(kernel)
        {
            this.Name = name;
            this.Players = new List<Player>();
        }

        public Team AddPlayer(Player player)
        {
            EnforceMaxPlayerLimit();
            this.Players.Add(player);
            eventPublisher.Publish(string.Format("Player {0} {1} added to {2}.", player.FirstName, player.LastName, Name));
            return this;
        }

        public void RegisterPlayers()
        {
            var unregisteredPlayers = Players.Where(x => x.IsRegistered == false);
            unregisteredPlayers.ForEach(player => player.Register());
        }

        protected override void Accept(IKernel kernel)
        {
            teamConfiguration = (ITeamConfiguration)kernel.Get(typeof(ITeamConfiguration));
            base.Accept(kernel);
        }

        void EnforceMaxPlayerLimit()
        {
            if (this.Players.Count() >= teamConfiguration.GetMaxPlayerCount(Name))
                throw new Exception("Player count may not exceed max configuration for team.");
        }
    }
}
