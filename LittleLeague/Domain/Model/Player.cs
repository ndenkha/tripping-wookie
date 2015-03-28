using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Player : EntityBase
    {
        public int PlayerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsRegistered { get; private set; }
        public DateTime? RegistrationDate { get; private set; }
        public int TeamId { get; private set; }
        public Team Team { get; private set; }

        Player()
        {
            //For use by entity framework only.
        }

        public Player(string firstName, string lastName, Team team, IKernel kernel)
            : base(kernel)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Team = team;
        }

        public void Register()
        {
            IsRegistered = true;
            RegistrationDate = DateTime.UtcNow;
            log.InfoFormat("Registered {0} {1}.", FirstName, LastName);
        }

        public void UnRegister()
        {
            IsRegistered = false;
            RegistrationDate = null;
            log.InfoFormat("Unregistered {0} {1}.", FirstName, LastName);
        }
    }
}
