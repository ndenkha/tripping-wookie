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
        ILog log;

        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Player> Players { get; private set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        Team()
        {
            //For use by entity framework only.
        }

        public Team(string name, IKernel kernel)
        {
            this.Name = name;
            this.Players = new List<Player>();
            ((IDependencyConsumer)this).Accept(kernel);
        }

        public Player AddPlayer(Player player)
        {
            this.Players.Add(player);
            log.InfoFormat("Player {0} {1} added.", player.FirstName, player.LastName);
            return player;
        }

        public void RegisterPlayers()
        {
            var unregisteredPlayers = Players.Where(x => x.IsRegistered == false);
            unregisteredPlayers.ForEach(player => player.Register());
        }

        void IDependencyConsumer.Accept(IKernel kernel)
        {
            log = (ILog)kernel.GetService(typeof(ILog));
        }
    }
}
