﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ninject;
using log4net;
using log4net.Config;
using Domain.Model;
using System.Threading;
using System.Security.Principal;
using System.Data.Entity;
using _3rdPartyApis.Configuration;
using _3rdPartyApis.Eventing;

namespace Client
{
    using DbContext = Domain.DbContext;

    class Program
    {
        ILog log;
        IKernel kernel;

        Program()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("Default"); 

            kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(log);
            kernel.Bind<ITeamConfiguration>().To<TeamConfiguration>();
            kernel.Bind<IEventPublisher>().To<EventPublisher>();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.DeleteTeams();
            program.CreateTeamsAndPlayers();
            program.RegisterPlayers();
            program.ReadTeamsAndPlayers();

            Console.WriteLine();
            Console.WriteLine("Done.");
            Console.ReadKey(true);
        }

        void DeleteTeams()
        {
            using (var db = new DbContext(kernel))
            {
                db.Teams.ForEach(team => db.Teams.Remove(team));
                db.SaveChanges();
            }
            log.Info("Deleted teams.");
        }

        void CreateTeamsAndPlayers()
        {
            // Setting up a different principle to illustrate how createBy and lastUpdateBy varies in the database.
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("TestUser1", "Generic"), new string[] { "User" });
            using (var db = new DbContext(kernel))
            {
                var team = db.Teams.Add(new Team("Cowboys", kernel));
                team.AddPlayer(new Player("Ray", "Agnew", team, kernel))
                    .AddPlayer(new Player("Dan", "Bailey", team, kernel))
                    .AddPlayer(new Player("Cole", "Beasley", team, kernel))
                    .AddPlayer(new Player("Machenzy", "Bernadeau", team, kernel))
                    .AddPlayer(new Player("Ken", "Bishop", team, kernel));

                team = db.Teams.Add(new Team("Giants", kernel));
                team.AddPlayer(new Player("Prince", "Amukamara", team, kernel))
                    .AddPlayer(new Player("Robert", "Ayers Jr.", team, kernel))
                    .AddPlayer(new Player("Michael", "Bamiro", team, kernel))
                    .AddPlayer(new Player("Jon", "Beason", team, kernel))
                    .AddPlayer(new Player("Will", "Beatty", team, kernel));

                db.SaveChanges();
            }
            log.Info("Teams and players created.");
        }

        void RegisterPlayers()
        {
            // Setting up a different principle to illustrate how createBy and lastUpdateBy varies in the database.
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("TestUser2", "Generic"), new string[] { "User" });

            // An example of transaction scope being used.
            using (var scope = new TransactionScope())
            {
                using (var db = new DbContext(kernel))
                {
                    //db.Teams.Include(x=>x.Players).ForEach(team => team.RegisterPlayers());
                    db.Teams
                        .Include(team=>team.Players)
                        .ForEach(team => team.RegisterPlayers());
                    db.SaveChanges();
                }
                scope.Complete();
            }
            log.Info("Registered players.");
        }

        void ReadTeamsAndPlayers()
        {
            log.Info("Reading players...");
            // Injection will not be used because we are using the default constructor of DbContext.
            using (var db = new DbContext(kernel))
            {
                foreach (var team in db.Teams.Include(x => x.Players))
                {
                    log.InfoFormat("# Team: {0}", team.Name);
                    foreach (var player in team.Players)
                    {
                        log.InfoFormat("  - Player: {0} {1}", player.FirstName, player.LastName);
                    }
                }
            }
        }
    }
}
