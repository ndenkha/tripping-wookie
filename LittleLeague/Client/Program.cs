using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ninject;
using log4net;
using Domain.Model;
using System.Threading;
using System.Security.Principal;

namespace Client
{
    class Program
    {
        ILog log;
        IServiceProvider serviceProvider;

        Program()
        {
            log = log4net.LogManager.GetLogger("LittleLeagueLog"); 

            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(log);

            serviceProvider = kernel;
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.CreateTeamAndPlayer();
            program.RegisterPlayers();
        }

        void RegisterPlayers()
        {
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("TestUser1", "Generic"), new string [] { "User" });

            using (var scope = new TransactionScope())
            {
                using (var db = new DbContext(serviceProvider))
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
            log.Debug("Registered players.");
        }

        void CreateTeamAndPlayer()
        {
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("TestUser1", "Generic"), new string[] { "User" });

            using (var db = new DbContext(serviceProvider))
            {
                var team = db.Teams.Add(new Team("Hawks", serviceProvider));
                team.AddPlayer(new Player("John", "Doe", team, serviceProvider));
                db.SaveChanges();
            }
            log.Debug("Team and player created.");
        }
    }
}
