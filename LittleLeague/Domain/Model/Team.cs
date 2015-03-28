using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Team : IServiceConsumer, IAuditable
    {
        ILog log;

        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Player> Players { get; private set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        private Team()
        {
            //For use by entity framework only.
        }

        public Team(string name, IServiceProvider serviceProvider)
        {
            this.Name = name;
            this.Players = new List<Player>();
            ((IServiceConsumer)this).Accept(serviceProvider);
        }

        public Player AddPlayer(Player player)
        {
            this.Players.Add(player);
            log.InfoFormat("Player {0} {1} added.", player.FirstName, player.LastName);
            return player;
        }

        void IServiceConsumer.Accept(IServiceProvider serviceProvider)
        {
            log = serviceProvider.GetService<ILog>();
        }
    }
}
