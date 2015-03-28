﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Player : IServiceConsumer, IAuditable
    {
        ILog log;

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

        Player()
        {
            //For use by entity framework only.
        }

        public Player(string firstName, string lastName, Team team, IServiceProvider serviceProvider)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Team = team;
            ((IServiceConsumer)this).Accept(serviceProvider);
        }

        public void Register()
        {
            IsRegistered = true;
            RegistrationDate = DateTime.UtcNow;
            log.Info("Registered.");
        }

        public void UnRegister()
        {
            IsRegistered = false;
            RegistrationDate = null;
            log.Info("Unregistered.");
        }

        void IServiceConsumer.Accept(IServiceProvider serviceProvider)
        {
            log = serviceProvider.GetService<ILog>();
        }
    }
}
