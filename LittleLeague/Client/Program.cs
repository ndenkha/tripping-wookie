using Domain;
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

namespace Client
{
    using DbContext = Domain.DbContext;

    class Program
    {
        ILog log;
        IServiceProvider serviceProvider;

        Program()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("LittleLeagueLog"); 

            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(log);

            serviceProvider = kernel;
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
            using (var db = new DbContext(serviceProvider))
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
            using (var db = new DbContext(serviceProvider))
            {
                var team = db.Teams.Add(new Team("Hawks", serviceProvider));
                team.AddPlayer(new Player("John", "Doe", team, serviceProvider));
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
                using (var db = new DbContext(serviceProvider))
                {
                    var team = db.Teams.Where(x => x.Name == "Hawks").Include(x=>x.Players).Single();
                    team.Players.ForEach(player => player.Register());
                    db.SaveChanges();
                }
                scope.Complete();
            }
            log.Info("Registered players.");
        }

        void ReadTeamsAndPlayers()
        {
            // Injection will not be used because we are using the default constructor of DbContext.
            using (var db = new DbContext())
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
