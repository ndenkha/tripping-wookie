using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ninject;
using log4net;
using System.Data.Entity;

namespace Client
{
    using DbContext = Domain.DbContext;
    using log4net.Config;

    class Program
    {
        ILog log;
        IServiceLocator serviceLocator;

        Program()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("LittleLeagueLog");

            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(log);

            serviceLocator = new ServiceLocator(kernel);
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
            using (var db = new DbContext("TestUser2", serviceLocator))
            {
                foreach (var team in db.Teams)
                {
                    db.Teams.Remove(team);
                }
                db.SaveChanges();
            }
            log.Info("Deleted teams.");
        }

        void CreateTeamsAndPlayers()
        {
            using (var db = new DbContext("TestUser2", serviceLocator))
            {
                var team = db.Teams.Add(new Team("Hawks", serviceLocator));
                team.AddPlayer(new Player("John", "Doe", team, serviceLocator));
                db.SaveChanges();
            }
            log.Info("Teams and players created.");
        }

        void RegisterPlayers()
        {
            using (var scope = new TransactionScope())
            {
                using (var db = new DbContext("TestUser1", serviceLocator))
                {

                    var team = db.Teams.Where(x => x.Name == "Hawks").Single();
                    foreach (var player in team.Players)
                    {
                        player.Register();
                    }
                    db.SaveChanges();
                }
                scope.Complete();
            }
            log.Info("Registered players.");
        }

        void ReadTeamsAndPlayers()
        {
            using (var db = new DbContext())
            {
                foreach (var team in db.Teams.Include(x => x.Players))
                {
                    log.Info("# Team: " + team.Name);
                    foreach (var player in team.Players)
                    {
                        log.InfoFormat("  - Player: {0} {1}", player.FirstName, player.LastName);
                    }
                }
            }
        }
    }
}
