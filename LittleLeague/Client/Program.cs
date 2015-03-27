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
            using (var scope = new TransactionScope())
            {
                using (var db = new DbContext("TestUser1", serviceProvider, true))
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
            using (var db = new DbContext("TestUser2", serviceProvider, true))
            {
                var team = db.Teams.Add(new Team("Hawks", serviceProvider));
                team.AddPlayer(new Player("John", "Doe", team, serviceProvider));
                db.SaveChanges();
            }
            log.Debug("Team and player created.");
        }
    }
}
