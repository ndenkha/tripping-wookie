﻿using System;
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
        public DateTime? RegistrationDate { get; private set; }
        public int TeamId { get; private set; }
        public Team Team { get; private set; }

        public Player()
        {
            //For use by entity framework only.
        }

        public Player(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void Register()
        {
            IsRegistered = true;
            RegistrationDate = DateTime.UtcNow;
        }

        public void UnRegister()
        {
            IsRegistered = false;
            RegistrationDate = null;
        }
    }
}
