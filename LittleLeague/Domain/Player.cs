using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player
    {
        public int PlayerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsRegistered { get; private set; }

        public Player()
        {
        }

        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Register()
        {
            IsRegistered = true;
        }

        public void UnRegister()
        {
            IsRegistered = false;
        }
    }
}
