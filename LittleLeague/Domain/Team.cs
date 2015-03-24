using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Team
    {
        public int TeamId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Player> Players { get; private set; }

        public Team()
        {
            //For use by entity framework only.
        }

        public Team(string name)
        {
            this.Name = name;
            this.Players = new List<Player>();
        }
    }
}
