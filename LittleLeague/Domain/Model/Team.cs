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
            this.Players.Add(player);
            log.InfoFormat("Player {0} {1} added to {2}.", player.FirstName, player.LastName, Name);
            return this;
        }

        public void RegisterPlayers()
        {
            var unregisteredPlayers = Players.Where(x => x.IsRegistered == false);
            unregisteredPlayers.ForEach(player => player.Register());
        }
    }
}
