﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DbContext())
            {
                var team = db.Teams.Add(new Team("Hawks"));
                team.Players.Add(new Player("John", "Doe"));
                db.SaveChanges();
            }
        }
    }
}
