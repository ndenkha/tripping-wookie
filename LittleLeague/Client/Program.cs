using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTeamAndPlayer();
            RegisterPlayers();
        }

        static void RegisterPlayers()
        {
            using (var scope = new TransactionScope())
            {
                using (var db = new DbContext())
                {

                    var team = db.Teams.Where(x => x.Name == "Hawks").Single();
                    foreach (var player in team.Players)
                    {
                        player.Register();
                    }
                    db.SaveChanges();
                }
            }
        }

        static void CreateTeamAndPlayer()
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
